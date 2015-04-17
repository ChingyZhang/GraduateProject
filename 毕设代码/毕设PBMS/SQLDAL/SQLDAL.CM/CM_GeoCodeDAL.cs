
// ===================================================================
// 文件： CM_GeoCodeDAL.cs
// 项目名称：
// 创建时间：2015-03-24
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.CM;


namespace MCSFramework.SQLDAL.CM
{
	/// <summary>
	///CM_GeoCode数据访问DAL类
	/// </summary>
	public class CM_GeoCodeDAL : BaseSimpleDAL<CM_GeoCode>
	{
		#region 构造函数
		///<summary>
		///
		///</summary>
		public CM_GeoCodeDAL()
		{
			_ProcePrefix = "MCS_CM.dbo.sp_CM_GeoCode";
		}
		#endregion
		
		
		/// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(CM_GeoCode m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Name", SqlDbType.VarChar, 100, m.Name),
				SQLDatabase.MakeInParam("@Code", SqlDbType.VarChar, 50, m.Code),
				SQLDatabase.MakeInParam("@CityCode", SqlDbType.VarChar, 50, m.CityCode),
				SQLDatabase.MakeInParam("@OfficialCity", SqlDbType.Int, 4, m.OfficialCity),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 1000, m.Remark),
				SQLDatabase.MakeInParam("@InsertStaff", SqlDbType.Int, 4, m.InsertStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
			#endregion
			
            m.ID =  SQLDatabase.RunProc(_ProcePrefix + "_Add", prams);
			
            return m.ID;
        }
		
		/// <summary>
        /// 更新项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Update(CM_GeoCode m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@Name", SqlDbType.VarChar, 100, m.Name),
				SQLDatabase.MakeInParam("@Code", SqlDbType.VarChar, 50, m.Code),
				SQLDatabase.MakeInParam("@CityCode", SqlDbType.VarChar, 50, m.CityCode),
				SQLDatabase.MakeInParam("@OfficialCity", SqlDbType.Int, 4, m.OfficialCity),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 1000, m.Remark),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
			#endregion
			
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
			
			return ret;
        }
		
        protected override CM_GeoCode FillModel(IDataReader dr)
		{
			CM_GeoCode m = new CM_GeoCode();
			if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (int)dr["ID"];
			if (!string.IsNullOrEmpty(dr["Name"].ToString()))	m.Name = (string)dr["Name"];
			if (!string.IsNullOrEmpty(dr["Code"].ToString()))	m.Code = (string)dr["Code"];
			if (!string.IsNullOrEmpty(dr["CityCode"].ToString()))	m.CityCode = (string)dr["CityCode"];
			if (!string.IsNullOrEmpty(dr["OfficialCity"].ToString()))	m.OfficialCity = (int)dr["OfficialCity"];
			if (!string.IsNullOrEmpty(dr["Remark"].ToString()))	m.Remark = (string)dr["Remark"];
			if (!string.IsNullOrEmpty(dr["InsertTime"].ToString()))	m.InsertTime = (DateTime)dr["InsertTime"];
			if (!string.IsNullOrEmpty(dr["InsertStaff"].ToString()))	m.InsertStaff = (int)dr["InsertStaff"];
			if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString()))	m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);
						
			return m;
		}
    }
}

