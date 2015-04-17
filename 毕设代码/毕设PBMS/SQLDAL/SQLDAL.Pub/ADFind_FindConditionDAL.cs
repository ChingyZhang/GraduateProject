
// ===================================================================
// 文件： ADFind_FindConditionDAL.cs
// 项目名称：
// 创建时间：2008-12-23
// 作者:	   
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
	///ADFind_FindCondition数据访问DAL类
	/// </summary>
	public class ADFind_FindConditionDAL : BaseSimpleDAL<ADFind_FindCondition>
	{
		#region 构造函数
		///<summary>
		///
		///</summary>
		public ADFind_FindConditionDAL()
		{
			_ProcePrefix = "MCS_Pub.dbo.sp_ADFind_FindCondition";
		}
		#endregion
		
		
		/// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(ADFind_FindCondition m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Name", SqlDbType.VarChar, 255, m.Name),
				SQLDatabase.MakeInParam("@ConditionText", SqlDbType.VarChar, 8000, m.ConditionText),
				SQLDatabase.MakeInParam("@ConditionValue", SqlDbType.VarChar, 8000, m.ConditionValue),
				SQLDatabase.MakeInParam("@ConditionSQL", SqlDbType.VarChar, 8000, m.ConditionSQL),
				SQLDatabase.MakeInParam("@CreateDate", SqlDbType.DateTime, 8, m.CreateDate),
				SQLDatabase.MakeInParam("@OpStaff", SqlDbType.Int, 4, m.OpStaff),
				SQLDatabase.MakeInParam("@Panel", SqlDbType.UniqueIdentifier, 16, m.Panel),
				SQLDatabase.MakeInParam("@IsPublic", SqlDbType.Char, 1, m.IsPublic)
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
        public override int Update(ADFind_FindCondition m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@Name", SqlDbType.VarChar, 255, m.Name),
				SQLDatabase.MakeInParam("@ConditionText", SqlDbType.VarChar, 8000, m.ConditionText),
				SQLDatabase.MakeInParam("@ConditionValue", SqlDbType.VarChar,8000, m.ConditionValue),
				SQLDatabase.MakeInParam("@ConditionSQL", SqlDbType.VarChar, 8000, m.ConditionSQL),
				SQLDatabase.MakeInParam("@CreateDate", SqlDbType.DateTime, 8, m.CreateDate),
				SQLDatabase.MakeInParam("@OpStaff", SqlDbType.Int, 4, m.OpStaff),
				SQLDatabase.MakeInParam("@Panel", SqlDbType.UniqueIdentifier, 16, m.Panel),
				SQLDatabase.MakeInParam("@IsPublic", SqlDbType.Char, 1, m.IsPublic)
			};
			#endregion
			
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
			
			return ret;
        }
		
        protected override ADFind_FindCondition FillModel(IDataReader dr)
		{
			ADFind_FindCondition m = new ADFind_FindCondition();
			if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (int)dr["ID"];
			if (!string.IsNullOrEmpty(dr["Name"].ToString()))	m.Name = (string)dr["Name"];
			if (!string.IsNullOrEmpty(dr["ConditionText"].ToString()))	m.ConditionText = (string)dr["ConditionText"];
			if (!string.IsNullOrEmpty(dr["ConditionValue"].ToString()))	m.ConditionValue = (string)dr["ConditionValue"];
			if (!string.IsNullOrEmpty(dr["ConditionSQL"].ToString()))	m.ConditionSQL = (string)dr["ConditionSQL"];
			if (!string.IsNullOrEmpty(dr["CreateDate"].ToString()))	m.CreateDate = (DateTime)dr["CreateDate"];
			if (!string.IsNullOrEmpty(dr["OpStaff"].ToString()))	m.OpStaff = (int)dr["OpStaff"];
			if (!string.IsNullOrEmpty(dr["Panel"].ToString()))	m.Panel = (Guid)dr["Panel"];
			if (!string.IsNullOrEmpty(dr["IsPublic"].ToString()))	m.IsPublic = (string)dr["IsPublic"];
						
			return m;
		}
    }
}

