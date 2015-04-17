
// ===================================================================
// 文件： CM_RTChannel_SYSDAL.cs
// 项目名称：
// 创建时间：2015/1/27
// 作者:	   
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
	///CM_RTChannel_SYS数据访问DAL类
	/// </summary>
	public class CM_RTChannel_SYSDAL : BaseSimpleDAL<CM_RTChannel_SYS>
	{
		#region 构造函数
		///<summary>
		///
		///</summary>
		public CM_RTChannel_SYSDAL()
		{
			_ProcePrefix = "MCS_CM.dbo.sp_CM_RTChannel_SYS";
		}
		#endregion
		
		
		/// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(CM_RTChannel_SYS m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Code", SqlDbType.VarChar, 50, m.Code),
				SQLDatabase.MakeInParam("@Name", SqlDbType.VarChar, 50, m.Name),
				SQLDatabase.MakeInParam("@SuperID", SqlDbType.Int, 4, m.SuperID),
				SQLDatabase.MakeInParam("@Enabled", SqlDbType.Char, 1, m.Enabled),
				SQLDatabase.MakeInParam("@Manufacturer", SqlDbType.Int, 4, m.Manufacturer),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 200, m.Remark),
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
        public override int Update(CM_RTChannel_SYS m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@Code", SqlDbType.VarChar, 50, m.Code),
				SQLDatabase.MakeInParam("@Name", SqlDbType.VarChar, 50, m.Name),
				SQLDatabase.MakeInParam("@SuperID", SqlDbType.Int, 4, m.SuperID),
				SQLDatabase.MakeInParam("@Enabled", SqlDbType.Char, 1, m.Enabled),
				SQLDatabase.MakeInParam("@Manufacturer", SqlDbType.Int, 4, m.Manufacturer),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 200, m.Remark),
				SQLDatabase.MakeInParam("@InsertUser", SqlDbType.UniqueIdentifier, 16, m.InsertUser),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
			#endregion
			
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
			
			return ret;
        }
		
        protected override CM_RTChannel_SYS FillModel(IDataReader dr)
		{
			CM_RTChannel_SYS m = new CM_RTChannel_SYS();
			if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (int)dr["ID"];
			if (!string.IsNullOrEmpty(dr["Code"].ToString()))	m.Code = (string)dr["Code"];
			if (!string.IsNullOrEmpty(dr["Name"].ToString()))	m.Name = (string)dr["Name"];
			if (!string.IsNullOrEmpty(dr["SuperID"].ToString()))	m.SuperID = (int)dr["SuperID"];
			if (!string.IsNullOrEmpty(dr["Enabled"].ToString()))	m.Enabled = (string)dr["Enabled"];
			if (!string.IsNullOrEmpty(dr["Manufacturer"].ToString()))	m.Manufacturer = (int)dr["Manufacturer"];
			if (!string.IsNullOrEmpty(dr["Remark"].ToString()))	m.Remark = (string)dr["Remark"];
			if (!string.IsNullOrEmpty(dr["InsertTime"].ToString()))	m.InsertTime = (DateTime)dr["InsertTime"];
			if (!string.IsNullOrEmpty(dr["InsertUser"].ToString()))	m.InsertUser = (Guid)dr["InsertUser"];
			if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString()))	m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);
						
			return m;
		}
    }
}

