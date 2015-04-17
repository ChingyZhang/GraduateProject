﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MCSFramework.WSI.GIS
{
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
    public class RootObject
    {
        public List<Result> results { get; set; }
        public string status { get; set; }
    }
}


