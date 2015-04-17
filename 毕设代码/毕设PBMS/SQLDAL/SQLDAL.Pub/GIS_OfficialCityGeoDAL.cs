
// ===================================================================
// 文件： GIS_OfficialCityGeoDAL.cs
// 项目名称：
// 创建时间：2010/9/11
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.Pub;


namespace MCSFramework.SQLDAL.Pub
{
    /// <summary>
    ///GIS_OfficialCityGeo数据访问DAL类
    /// </summary>
    public class GIS_OfficialCityGeoDAL : BaseSimpleDAL<GIS_OfficialCityGeo>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public GIS_OfficialCityGeoDAL()
        {
            _ProcePrefix = "MCS_PUB.dbo.sp_GIS_OfficialCityGeo";
        }
        #endregion


        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(GIS_OfficialCityGeo m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@OfficialCity", SqlDbType.Int, 4, m.OfficialCity),
				SQLDatabase.MakeInParam("@Latitude", SqlDbType.Decimal, 9, m.Latitude),
				SQLDatabase.MakeInParam("@Longitude", SqlDbType.Decimal, 9, m.Longitude),
				SQLDatabase.MakeInParam("@LatLonBox_north", SqlDbType.Decimal, 9, m.LatLonBox_north),
				SQLDatabase.MakeInParam("@LatLonBox_east", SqlDbType.Decimal, 9, m.LatLonBox_east),
				SQLDatabase.MakeInParam("@LatLonBox_south", SqlDbType.Decimal, 9, m.LatLonBox_south),
				SQLDatabase.MakeInParam("@LatLonBox_west", SqlDbType.Decimal, 9, m.LatLonBox_west),
				SQLDatabase.MakeInParam("@Address", SqlDbType.VarChar, 200, m.Address),
				SQLDatabase.MakeInParam("@Accuracy", SqlDbType.VarChar, 50, m.Accuracy),
				SQLDatabase.MakeInParam("@CountryNameCode", SqlDbType.VarChar, 200, m.CountryNameCode),
				SQLDatabase.MakeInParam("@CountryName", SqlDbType.VarChar, 200, m.CountryName),
				SQLDatabase.MakeInParam("@AdministrativeAreaName", SqlDbType.VarChar, 200, m.AdministrativeAreaName),
				SQLDatabase.MakeInParam("@LocalityName", SqlDbType.VarChar, 200, m.LocalityName),
				SQLDatabase.MakeInParam("@DependentLocalityName", SqlDbType.VarChar, 200, m.DependentLocalityName),
				SQLDatabase.MakeInParam("@ThoroughfareName", SqlDbType.VarChar, 200, m.ThoroughfareName),
				SQLDatabase.MakeInParam("@AddressLine", SqlDbType.VarChar, 200, m.AddressLine),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            m.ID = SQLDatabase.RunProc(_ProcePrefix + "_Add", prams);

            return m.ID;
        }

        /// <summary>
        /// 更新项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Update(GIS_OfficialCityGeo m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@OfficialCity", SqlDbType.Int, 4, m.OfficialCity),
				SQLDatabase.MakeInParam("@Latitude", SqlDbType.Decimal, 9, m.Latitude),
				SQLDatabase.MakeInParam("@Longitude", SqlDbType.Decimal, 9, m.Longitude),
				SQLDatabase.MakeInParam("@LatLonBox_north", SqlDbType.Decimal, 9, m.LatLonBox_north),
				SQLDatabase.MakeInParam("@LatLonBox_east", SqlDbType.Decimal, 9, m.LatLonBox_east),
				SQLDatabase.MakeInParam("@LatLonBox_south", SqlDbType.Decimal, 9, m.LatLonBox_south),
				SQLDatabase.MakeInParam("@LatLonBox_west", SqlDbType.Decimal, 9, m.LatLonBox_west),
				SQLDatabase.MakeInParam("@Address", SqlDbType.VarChar, 200, m.Address),
				SQLDatabase.MakeInParam("@Accuracy", SqlDbType.VarChar, 50, m.Accuracy),
				SQLDatabase.MakeInParam("@CountryNameCode", SqlDbType.VarChar, 200, m.CountryNameCode),
				SQLDatabase.MakeInParam("@CountryName", SqlDbType.VarChar, 200, m.CountryName),
				SQLDatabase.MakeInParam("@AdministrativeAreaName", SqlDbType.VarChar, 200, m.AdministrativeAreaName),
				SQLDatabase.MakeInParam("@LocalityName", SqlDbType.VarChar, 200, m.LocalityName),
				SQLDatabase.MakeInParam("@DependentLocalityName", SqlDbType.VarChar, 200, m.DependentLocalityName),
				SQLDatabase.MakeInParam("@ThoroughfareName", SqlDbType.VarChar, 200, m.ThoroughfareName),
				SQLDatabase.MakeInParam("@AddressLine", SqlDbType.VarChar, 200, m.AddressLine),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override GIS_OfficialCityGeo FillModel(IDataReader dr)
        {
            GIS_OfficialCityGeo m = new GIS_OfficialCityGeo();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["OfficialCity"].ToString())) m.OfficialCity = (int)dr["OfficialCity"];
            if (!string.IsNullOrEmpty(dr["Latitude"].ToString())) m.Latitude = double.Parse(dr["Latitude"].ToString());
            if (!string.IsNullOrEmpty(dr["Longitude"].ToString())) m.Longitude = double.Parse(dr["Longitude"].ToString());
            if (!string.IsNullOrEmpty(dr["LatLonBox_north"].ToString())) m.LatLonBox_north = double.Parse(dr["LatLonBox_north"].ToString());
            if (!string.IsNullOrEmpty(dr["LatLonBox_east"].ToString())) m.LatLonBox_east = double.Parse(dr["LatLonBox_east"].ToString());
            if (!string.IsNullOrEmpty(dr["LatLonBox_south"].ToString())) m.LatLonBox_south = double.Parse(dr["LatLonBox_south"].ToString());
            if (!string.IsNullOrEmpty(dr["LatLonBox_west"].ToString())) m.LatLonBox_west = double.Parse(dr["LatLonBox_west"].ToString());
            if (!string.IsNullOrEmpty(dr["Address"].ToString())) m.Address = (string)dr["Address"];
            if (!string.IsNullOrEmpty(dr["Accuracy"].ToString())) m.Accuracy = (string)dr["Accuracy"];
            if (!string.IsNullOrEmpty(dr["CountryNameCode"].ToString())) m.CountryNameCode = (string)dr["CountryNameCode"];
            if (!string.IsNullOrEmpty(dr["CountryName"].ToString())) m.CountryName = (string)dr["CountryName"];
            if (!string.IsNullOrEmpty(dr["AdministrativeAreaName"].ToString())) m.AdministrativeAreaName = (string)dr["AdministrativeAreaName"];
            if (!string.IsNullOrEmpty(dr["LocalityName"].ToString())) m.LocalityName = (string)dr["LocalityName"];
            if (!string.IsNullOrEmpty(dr["DependentLocalityName"].ToString())) m.DependentLocalityName = (string)dr["DependentLocalityName"];
            if (!string.IsNullOrEmpty(dr["ThoroughfareName"].ToString())) m.ThoroughfareName = (string)dr["ThoroughfareName"];
            if (!string.IsNullOrEmpty(dr["AddressLine"].ToString())) m.AddressLine = (string)dr["AddressLine"];
            if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString())) m.UpdateTime = (DateTime)dr["UpdateTime"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);

            return m;
        }
    }
}

