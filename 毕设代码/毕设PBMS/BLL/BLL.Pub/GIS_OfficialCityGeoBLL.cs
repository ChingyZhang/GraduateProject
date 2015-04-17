
// ===================================================================
// 文件： GIS_OfficialCityGeoDAL.cs
// 项目名称：
// 创建时间：2010/9/11
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model.Pub;
using MCSFramework.SQLDAL.Pub;
using MCSFramework.Model;

namespace MCSFramework.BLL.Pub
{
    /// <summary>
    ///GIS_OfficialCityGeoBLL业务逻辑BLL类
    /// </summary>
    public class GIS_OfficialCityGeoBLL : BaseSimpleBLL<GIS_OfficialCityGeo>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.Pub.GIS_OfficialCityGeoDAL";
        private GIS_OfficialCityGeoDAL _dal;

        #region 构造函数
        ///<summary>
        ///GIS_OfficialCityGeoBLL
        ///</summary>
        public GIS_OfficialCityGeoBLL()
            : base(DALClassName)
        {
            _dal = (GIS_OfficialCityGeoDAL)_DAL;
            _m = new GIS_OfficialCityGeo();
        }

        public GIS_OfficialCityGeoBLL(int id)
            : base(DALClassName)
        {
            _dal = (GIS_OfficialCityGeoDAL)_DAL;
            FillModel(id);
        }

        public GIS_OfficialCityGeoBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (GIS_OfficialCityGeoDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<GIS_OfficialCityGeo> GetModelList(string condition)
        {
            return new GIS_OfficialCityGeoBLL()._GetModelList(condition);
        }
        #endregion

        /// <summary>
        /// 查询指定城市的Geo地理信息
        /// </summary>
        /// <param name="OfficialCity"></param>
        /// <returns></returns>
        public static GIS_OfficialCityGeo FindGeoByOfficialCity(int OfficialCity)
        {
            IList<GIS_OfficialCityGeo> Geos = GetModelList("OfficialCity=" + OfficialCity.ToString());
            if (Geos.Count > 0)
                return Geos[0];
            else
                return null;
        }


        #region 计算经纬度的距离（米）
        public static double DistanceByLatLong(double latitude1, double longitude1, double latitude2, double longitude2)
        {
            const double EarthRadius = 6378137;

            double radLat1 = latitude1 * Math.PI / 180;
            double radLat2 = latitude2 * Math.PI / 180;

            double x = (latitude1 * Math.PI / 180) - (latitude2 * Math.PI / 180);
            double y = (longitude1 * Math.PI / 180) - (longitude2 * Math.PI / 180);

            double distance = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(x / 2), 2) + Math.Cos(radLat1) * Math.Cos(radLat2) * Math.Pow(Math.Sin(y / 2), 2))) * EarthRadius;
            return Math.Round(distance, 1);
        }
        #endregion

        #region 根据经纬度获取最近的城市信息
        /// <summary>
        /// 根据经纬度获取最近的城市信息（区或市级）
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns></returns>
        public static int GetNearOfficialCityByLatLong(double latitude, double longitude)
        {
            IList<GIS_OfficialCityGeo> lists = GetModelList("(" + latitude.ToString() + " BETWEEN LatLonBox_south AND LatLonBox_north) AND ("
                + longitude.ToString() + " BETWEEN LatLonBox_west AND LatLonBox_east) AND OfficialCity IN (SELECT ID FROM MCS_SYS.dbo.Addr_OfficialCity WHERE [Level]=3)");
            if (lists.Count == 0)
            {
                lists = GetModelList("(" + latitude.ToString() + " BETWEEN LatLonBox_south AND LatLonBox_north) AND ("
                + longitude.ToString() + " BETWEEN LatLonBox_west AND LatLonBox_east) AND OfficialCity IN (SELECT ID FROM MCS_SYS.dbo.Addr_OfficialCity WHERE [Level]=2)");

                if (lists.Count == 0) return -1;
            }

            double min_distanc = -1;
            int city = 0;
            foreach (GIS_OfficialCityGeo item in lists)
            {
                double _d = DistanceByLatLong(latitude, longitude, item.Latitude, item.Longitude);
                if (min_distanc == -1 || min_distanc > _d)
                {
                    min_distanc = _d;
                    city = item.OfficialCity;
                }
            }

            return city;
        }
        #endregion

        #region 查询指定范围内的城市列表
        /// <summary>
        /// 查询指定范围内的城市列表
        /// </summary>
        /// <param name="latitude1"></param>
        /// <param name="longitude1"></param>
        /// <param name="latitude2"></param>
        /// <param name="longitude2"></param>
        /// <param name="Level"></param>
        /// <returns></returns>
        public static IList<Addr_OfficialCity> GetOfficialCityByLatiLongitudeScope(double latitude1, double longitude1, double latitude2, double longitude2, int Level)
        {
            double latitude = 0;
            if (latitude1 > latitude2)
            {
                latitude = latitude1;
                latitude1 = latitude2;
                latitude2 = latitude;
            }

            double longitude = 0;
            if (longitude1 > longitude2)
            {
                longitude = longitude1;
                longitude1 = longitude2;
                longitude2 = longitude;
            }

            return Addr_OfficialCityBLL.GetModelList("[Level]=" + Level.ToString() +
                " AND ID IN (SELECT OfficialCity FROM MCS_GIS.dbo.GIS_OfficialCityGeo WHERE Latitude BETWEEN " +
                latitude1.ToString() + " AND " + latitude2.ToString() + " AND Longitude BETWEEN " +
                longitude1.ToString() + " AND " + longitude2.ToString() + ")");
        }
        #endregion
        
    }
}