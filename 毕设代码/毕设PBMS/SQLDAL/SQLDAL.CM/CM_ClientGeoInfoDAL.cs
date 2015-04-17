
// ===================================================================
// 文件： CM_ClientGeoInfoDAL.cs
// 项目名称：
// 创建时间：2014-03-09
// 作者:	   ShenGang
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
	///CM_ClientGeoInfo数据访问DAL类
	/// </summary>
	public class CM_ClientGeoInfoDAL : BaseSimpleDAL<CM_ClientGeoInfo>
	{
		#region 构造函数
		///<summary>
		///
		///</summary>
		public CM_ClientGeoInfoDAL()
		{
			_ProcePrefix = "MCS_CM.dbo.sp_CM_ClientGeoInfo";
		}
		#endregion
		
		
		/// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(CM_ClientGeoInfo m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, m.Client),
				SQLDatabase.MakeInParam("@Latitude", SqlDbType.Decimal, 9, m.Latitude),
				SQLDatabase.MakeInParam("@Longitude", SqlDbType.Decimal, 9, m.Longitude),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 2000, m.Remark),
				SQLDatabase.MakeInParam("@InsertUser", SqlDbType.UniqueIdentifier, 16, m.InsertUser),
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
        public override int Update(CM_ClientGeoInfo m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, m.Client),
				SQLDatabase.MakeInParam("@Latitude", SqlDbType.Decimal, 9, m.Latitude),
				SQLDatabase.MakeInParam("@Longitude", SqlDbType.Decimal, 9, m.Longitude),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 2000, m.Remark),
				SQLDatabase.MakeInParam("@UpdateUser", SqlDbType.UniqueIdentifier, 16, m.UpdateUser),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
			#endregion
			
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
			
			return ret;
        }
		
        protected override CM_ClientGeoInfo FillModel(IDataReader dr)
		{
			CM_ClientGeoInfo m = new CM_ClientGeoInfo();
			if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (int)dr["ID"];
			if (!string.IsNullOrEmpty(dr["Client"].ToString()))	m.Client = (int)dr["Client"];
			if (!string.IsNullOrEmpty(dr["Latitude"].ToString()))	m.Latitude = (decimal)dr["Latitude"];
			if (!string.IsNullOrEmpty(dr["Longitude"].ToString()))	m.Longitude = (decimal)dr["Longitude"];
			if (!string.IsNullOrEmpty(dr["Remark"].ToString()))	m.Remark = (string)dr["Remark"];
			if (!string.IsNullOrEmpty(dr["InsertTime"].ToString()))	m.InsertTime = (DateTime)dr["InsertTime"];
			if (!string.IsNullOrEmpty(dr["InsertUser"].ToString()))	m.InsertUser = (Guid)dr["InsertUser"];
			if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString()))	m.UpdateTime = (DateTime)dr["UpdateTime"];
			if (!string.IsNullOrEmpty(dr["UpdateUser"].ToString()))	m.UpdateUser = (Guid)dr["UpdateUser"];
			if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString()))	m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);
						
			return m;
		}
    }
}

