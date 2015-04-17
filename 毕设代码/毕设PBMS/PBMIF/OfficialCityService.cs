using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using MCSFramework.BLL;
using MCSFramework.Model;
using MCSFramework.Common;
using Newtonsoft.Json;
using System.Web.Script.Services;
using MCSFramework.BLL.Pub;
using MCSFramework.GIS;
using MCSFramework.WSI.Model;
using MCSFramework.Model.CM;
using MCSFramework.BLL.CM;

namespace MCSFramework.WSI
{
    public class OfficialCityService
    {
        public OfficialCityService()
        {
            LogWriter.FILE_PATH = "C:\\MCSLog_PBMIF";
        }

        public string HelloWorld()
        {
            return "Hello World";
        }

        #region 城市信息获取
        /// <summary>
        /// 获取指定城市的下级城市，CityID=1时获取全国省份
        /// </summary>
        /// <param name="AuthKey">匿名用户登录后的认证码</param>
        /// <param name="CityID">上级城市ID</param>
        /// <returns></returns>
        public static List<OfficialCity> GetSubCitysBySuper(UserInfo User, int CityID)
        {
            LogWriter.WriteLog("OfficialCityService.GetSubCitysBySuper:UserName=" + User.UserName + ",CityID=" + CityID.ToString());

            if (User == null) return null;

            IList<Addr_OfficialCity> citys = Addr_OfficialCityBLL.GetModelList("SuperID=" + CityID.ToString());

            List<OfficialCity> lists = new List<OfficialCity>(citys.Count);
            foreach (Addr_OfficialCity item in citys)
            {
                lists.Add(new OfficialCity(item));
            }
            return lists;
        }

        /// <summary>
        /// 获取所有行政城市
        /// </summary>
        /// <param name="AuthKey"></param>
        /// <returns></returns>
        public static List<OfficialCity> GetAllOfficialCitys(UserInfo User)
        {
            LogWriter.WriteLog("OfficialCitySerice.GetAllOfficialCitys:UserName=" + User.UserName);
            if (User == null) return null;

            IList<Addr_OfficialCity> citys = null;
            if (User.ClientID > 0)
            {
                CM_Client c = new CM_ClientBLL(User.ClientID).Model;
                if (c != null)
                {
                    int prov = TreeTableBLL.GetSuperIDByLevel("MCS_SYS.dbo.Addr_OfficialCity", c.OfficialCity, 1);
                    if (prov > 0)
                        citys = Addr_OfficialCityBLL.GetModelList("Level<=3 AND Level1_SuperID=" + prov.ToString());
                }
            }

            if (citys == null) citys = Addr_OfficialCityBLL.GetModelList("Level<=3");

            List<OfficialCity> lists = new List<OfficialCity>(citys.Count);
            foreach (Addr_OfficialCity item in citys)
            {
                lists.Add(new OfficialCity(item));
            }
            return lists;
        }

        /// <summary>
        /// 获取指定城市的全名
        /// </summary>
        /// <param name="AuthKey">匿名用户登录后的认证码</param>
        /// <param name="CityID"></param>
        /// <returns></returns>

        public static string GetCityFullName(UserInfo User, int CityID)
        {
            LogWriter.WriteLog("OfficialCityService.GetCityFullName:UserName=" + User.UserName + ",CityID=" + CityID.ToString());

            if (CityID <= 1) return "中国";

            return TreeTableBLL.GetFullPathName("MCS_SYS.dbo.Addr_OfficialCity", CityID).Replace("->", " ");
        }

        /// <summary>
        /// 获取行政城市对应的省市县ID
        /// </summary>
        /// <param name="AuthKey">匿名用户登录后的认证码</param>
        /// <param name="OfficialCity">行政城市ID</param>
        /// <param name="ProvID">省</param>
        /// <param name="CityID">市</param>
        /// <param name="AreaID">县</param>
        /// <returns>当前城市的级别，1:省 2:市 3:县 4:镇，-1:城市ID不存在，-2:获取城市信息出错</returns>
        public static int GetSuperOfficialCity(UserInfo User, int OfficialCity, out int ProvID, out int CityID, out int AreaID)
        {
            ProvID = 0; CityID = 0; AreaID = 0;
            LogWriter.WriteLog("OfficialCityService.GetSuperCityID:UserName=" + User.UserName + ",OfficialCity=" + OfficialCity.ToString());

            Addr_OfficialCityBLL city = new Addr_OfficialCityBLL(OfficialCity);
            if (city.Model == null) return -1;

            try
            {
                if (city.Model.Level == 4)
                {
                    //当前城市为乡镇
                    AreaID = city.Model.SuperID;
                    CityID = new Addr_OfficialCityBLL(AreaID).Model.SuperID;
                    ProvID = new Addr_OfficialCityBLL(CityID).Model.SuperID;

                }
                else if (city.Model.Level == 3)
                {
                    //当前城市为区/县
                    AreaID = city.Model.ID;
                    CityID = city.Model.SuperID;
                    ProvID = new Addr_OfficialCityBLL(CityID).Model.SuperID;

                }
                else if (city.Model.Level == 2)
                {
                    //当前城市为地级市
                    CityID = city.Model.ID;
                    ProvID = city.Model.SuperID;
                }
                else if (city.Model.Level == 1)
                {
                    //当前城市为省
                    ProvID = city.Model.ID;
                }
                else
                {
                    return -2;
                }
            }
            catch (System.Exception err)
            {
                LogWriter.WriteLog("OfficialCityService.GetSuperCityID，Exception!", err);
                return -2;
            }

            return city.Model.Level;
        }

        /// <summary>
        /// 获取行政城市对应的省市县JSON对象
        /// </summary>
        /// <param name="AuthKey">匿名用户登录后的认证码</param>
        /// <param name="OfficialCity">行政城市ID</param>
        /// <param name="Prov">省JSON</param>
        /// <param name="City">市JSON</param>
        /// <param name="Area">县JSON</param>
        /// <returns>当前城市的级别，1:省 2:市 3:县 4:镇，-1:城市ID不存在，-2:获取城市信息出错</returns>
        public static int GetSuperOfficialCityJson(UserInfo User, int OfficialCity, out string Prov, out string City, out string Area)
        {
            Prov = ""; City = ""; Area = "";
            int ProvID = 0, CityID = 0, AreaID = 0;

            int ret = GetSuperOfficialCity(User, OfficialCity, out ProvID, out CityID, out AreaID);
            if (ret < 0) return ret;

            if (ProvID > 0) Prov = JsonConvert.SerializeObject(new OfficialCity(ProvID));
            if (CityID > 0) City = JsonConvert.SerializeObject(new OfficialCity(CityID));
            if (AreaID > 0) Area = JsonConvert.SerializeObject(new OfficialCity(AreaID));

            return ret;
        }

        #endregion

        #region GIS地理信息操作

        #region 根据当前经纬度获取所在的城市信息
        /// <summary>
        /// 根据当前经纬度获取所在的城市信息
        /// </summary>
        /// <param name="AuthKey">授权码</param>
        /// <param name="Latitude">纬度</param>
        /// <param name="Longitude">经度</param>
        /// <returns></returns>
        public static OfficialCity GetCurrentOfficialCityByGPS(UserInfo User, float Latitude, float Longitude)
        {
            LogWriter.WriteLog("MemberService.GetCurrentOfficialCityByGPS:UserName=" + User.UserName + ",Latitude=" + Latitude.ToString() + ",Longitude=" + Longitude.ToString());
            if (User == null) return null;
            try
            {
                double mgLat, mgLon;
                //转火星坐标
                WGSToGCJAPI.Transform((double)Latitude, (double)Longitude, out mgLat, out mgLon);

                Addr_OfficialCity city = null;

                ///默认根据Google地图API获取所在地
                string Provname, CityName, AreaName, FullAddress;
                int ret = GoogleMapAPI.latLngToChineseDistrict((float)mgLat, (float)mgLon, out Provname, out  CityName, out  AreaName, out FullAddress);
                if (ret == 0)
                {
                    //省
                    IList<Addr_OfficialCity> _lists = Addr_OfficialCityBLL.GetModelList("Name LIKE '" + Provname + "%' AND [LEVEL]=1");
                    if (_lists.Count > 0)
                    {
                        city = _lists[0];

                        //市
                        _lists = Addr_OfficialCityBLL.GetModelList("Name LIKE '" + CityName + "%' AND SuperID=" + city.ID.ToString() + " AND [LEVEL]=2");
                        if (_lists.Count > 0)
                        {
                            city = _lists[0];

                            //区、县
                            _lists = Addr_OfficialCityBLL.GetModelList("Name LIKE '" + AreaName + "%' AND SuperID=" + city.ID.ToString() + " AND [LEVEL]=3");
                            if (_lists.Count > 0) city = _lists[0];
                        }
                    }

                }
                else
                {
                    int cityid = GIS_OfficialCityGeoBLL.GetNearOfficialCityByLatLong(Latitude, Longitude);
                    city = new Addr_OfficialCityBLL(cityid).Model;
                }

                return new OfficialCity(city);
            }
            catch (System.Exception err)
            {
                LogWriter.WriteLog("MemberService.GetCurrentOfficialCityByGPS Exception！UserName=" + User.UserName, err);
                return null;
            }
        }
        #endregion


        #region 通过GoogleAPI获取所在城市名称
        public string GetAddressByGPS(float Latitude, float Longitude)
        {
            string Provname, CityName, AreaName, FullAddress;
            double mgLat, mgLon;
            WGSToGCJAPI.Transform((double)Latitude, (double)Longitude, out mgLat, out mgLon);

            int ret = GoogleMapAPI.latLngToChineseDistrict((float)mgLat, (float)mgLon, out Provname, out  CityName, out  AreaName, out FullAddress);
            if (ret == 0)
            {
                return FullAddress;
            }
            return "faild!";
        }
        #endregion

        /// <summary>
        /// WGS坐标转火星坐标(World Geodetic System ==> Mars Geodetic System)
        /// </summary>
        /// <param name="Latitude">纬度</param>
        /// <param name="Longitude">经度</param>
        /// <param name="mgLat">输出火星坐标：纬度</param>
        /// <param name="mgLon">输出火星坐标：经度</param>
        /// <returns></returns>
        public static int WGS2GCJ(float Latitude, float Longitude, out float mgLat, out float mgLon)
        {
            mgLat = Latitude; mgLon = Longitude;
            double lat, lng;
            WGSToGCJAPI.Transform((double)Latitude, (double)Longitude, out lat, out lng);
            mgLat = (float)lat;
            mgLon = (float)lng;

            return 0;
        }
        #endregion

        #region 计算经纬度的距离（米）
        /// <summary>
        /// 计算经纬度的距离（米）
        /// </summary>
        /// <param name="latitude1">纬度1</param>
        /// <param name="longitude1">经度1</param>
        /// <param name="latitude2">纬度2</param>
        /// <param name="longitude2">经度2</param>
        /// <returns>返回两坐标间的距离(米)</returns>
        public static int DistanceByLatLong(double latitude1, double longitude1, double latitude2, double longitude2)
        {
            return (int)GIS_OfficialCityGeoBLL.DistanceByLatLong(latitude1, longitude1, latitude2, longitude2);
        }
        #endregion
    }
}