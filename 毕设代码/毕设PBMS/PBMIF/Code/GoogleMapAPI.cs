using System;
using System.Net;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Linq;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using MCSFramework.Common;

namespace MCSFramework.GIS
{
    public class GoogleMapAPI
    {
        /// <summary> 
        /// 经纬度取得行政区 
        /// </summary> 
        /// <returns></returns> 
        public static int latLngToChineseDistrict(float Latitude, float Longitude, out string Provname, out string CityName, out string AreaName, out string FullAddress)
        {
            Provname = "";
            CityName = "";
            AreaName = "";
            FullAddress = "";

            try
            {
                string result = string.Empty;//要回传的字符串 
                string url = ConfigHelper.GetConfigString("GoogleMapAPIURL");
                if (string.IsNullOrEmpty(url)) url = "http://maps.googleapis.com/maps/api/";
                if (!url.EndsWith("/")) url += "/";
                url += "geocode/json?latlng=" + Latitude.ToString() + "," + Longitude.ToString() + "&sensor=true&language=zh-CN";

                string json = String.Empty;
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                //指定语言，否则Google预设回传英文 
                request.Headers.Add("Accept-Language", "zh-CN");
                using (var response = request.GetResponse())
                {
                    using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                    {
                        json = sr.ReadToEnd();
                    }
                }
                GeoRootObject rootObj = JsonConvert.DeserializeObject<GeoRootObject>(json);

                if (rootObj == null || rootObj.status != "OK" || rootObj.results.Count == 0) return -1;

                AddressComponent addc = null;

                addc = rootObj.results[0].address_components.Where(c => c.types[0] == "administrative_area_level_1" && c.types[1] == "political").FirstOrDefault();
                if (addc != null) Provname = addc.long_name;

                addc = rootObj.results[0].address_components.Where(c => c.types[0] == "locality" && c.types[1] == "political").FirstOrDefault();
                if (addc != null) CityName = addc.long_name;

                addc = rootObj.results[0].address_components.Where(c => c.types[0] == "sublocality" && c.types[1] == "political").FirstOrDefault();
                if (addc != null) AreaName = addc.long_name;

                FullAddress = rootObj.results[0].formatted_address;

                return 0;

            }
            catch (System.Exception err)
            {
                return -100;
            }
        }

        /// <summary> 
        /// 经纬度转中文地址：https://developers.google.com/maps/documentation/geocoding/?hl=zh-TW#ReverseGeocoding 
        /// </summary>  www.it165.net
        /// <param name="latLng"></param> 
        public static string latLngToChineseAddress(float Latitude, float Longitude)
        {
            string url = ConfigHelper.GetConfigString("GoogleMapAPIURL");
            if (string.IsNullOrEmpty(url)) url = "http://maps.googleapis.com/maps/api/";
            if (!url.EndsWith("/")) url += "/";
            url += "geocode/json?latlng=" + Latitude.ToString() + "," + Longitude.ToString() + "&sensor=true&language=zh-CN";
            string json = String.Empty;
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            //指定语言，否则Google预设回传英文 
            request.Headers.Add("Accept-Language", "zh-CN");
            using (var response = request.GetResponse())
            {
                using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                {
                    json = sr.ReadToEnd();
                }
            }
            GeoRootObject rootObj = JsonConvert.DeserializeObject<GeoRootObject>(json);

            return rootObj.results[0].formatted_address;

        }




    }

    public class AddressComponent
    {
        public string long_name { get; set; }
        public string short_name { get; set; }
        public List<string> types { get; set; }
    }
    public class Location
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }
    public class Northeast
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }
    public class Southwest
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }
    public class Viewport
    {
        public Northeast northeast { get; set; }
        public Southwest southwest { get; set; }
    }
    public class Geometry
    {
        public Location location { get; set; }
        public string location_type { get; set; }
        public Viewport viewport { get; set; }
    }
    public class Result
    {
        public List<AddressComponent> address_components { get; set; }
        public string formatted_address { get; set; }
        public Geometry geometry { get; set; }
        public bool partial_match { get; set; }
        public List<string> types { get; set; }
    }
    public class GeoRootObject
    {
        public List<Result> results { get; set; }
        public string status { get; set; }
    }
}

