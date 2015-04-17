
// ===================================================================
// 文件： AC_AccountTitleDAL.cs
// 项目名称：
// 创建时间：2008-12-22
// 作者:	   
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.Pub;
using MCSFramework.Common;


namespace MCSFramework.SQLDAL.Pub
{
	/// <summary>
	///AC_AccountTitle数据访问DAL类
	/// </summary>
	public class AC_AccountTitleDAL : BaseSimpleDAL<AC_AccountTitle>
	{
		#region 构造函数
		///<summary>
		///
		///</summary>
		public AC_AccountTitleDAL()
		{
			_ProcePrefix = "MCS_Pub.dbo.sp_AC_AccountTitle";
		}
		#endregion
		
		
		/// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(AC_AccountTitle m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Code", SqlDbType.VarChar, 20, m.Code),
				SQLDatabase.MakeInParam("@Name", SqlDbType.VarChar, 50, m.Name),
				SQLDatabase.MakeInParam("@SuperID", SqlDbType.Int, 4, m.SuperID),
				SQLDatabase.MakeInParam("@Level", SqlDbType.Int, 4, m.Level),
				SQLDatabase.MakeInParam("@Department", SqlDbType.Int, 4, m.Department),
				SQLDatabase.MakeInParam("@Description", SqlDbType.VarChar, 500, m.Description),
                SQLDatabase.MakeInParam("@FeeType", SqlDbType.Int, 4, m.FeeType),
                SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 8000, CombineExtProperty(m.ExtPropertys,m.ModelName))
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
        public override int Update(AC_AccountTitle m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@Code", SqlDbType.VarChar, 20, m.Code),
				SQLDatabase.MakeInParam("@Name", SqlDbType.VarChar, 50, m.Name),
				SQLDatabase.MakeInParam("@SuperID", SqlDbType.Int, 4, m.SuperID),
				SQLDatabase.MakeInParam("@Level", SqlDbType.Int, 4, m.Level),
				SQLDatabase.MakeInParam("@Department", SqlDbType.Int, 4, m.Department),
				SQLDatabase.MakeInParam("@Description", SqlDbType.VarChar, 500, m.Description),
                SQLDatabase.MakeInParam("@FeeType", SqlDbType.Int, 4, m.FeeType),
                SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 8000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
			#endregion
			
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
			
			return ret;
        }
		
        protected override AC_AccountTitle FillModel(IDataReader dr)
		{
			AC_AccountTitle m = new AC_AccountTitle();
			if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (int)dr["ID"];
			if (!string.IsNullOrEmpty(dr["Code"].ToString()))	m.Code = (string)dr["Code"];
			if (!string.IsNullOrEmpty(dr["Name"].ToString()))	m.Name = (string)dr["Name"];
			if (!string.IsNullOrEmpty(dr["SuperID"].ToString()))	m.SuperID = (int)dr["SuperID"];
			if (!string.IsNullOrEmpty(dr["Level"].ToString()))	m.Level = (int)dr["Level"];
			if (!string.IsNullOrEmpty(dr["Department"].ToString()))	m.Department = (int)dr["Department"];
			if (!string.IsNullOrEmpty(dr["Description"].ToString()))	m.Description = (string)dr["Description"];
            if (!string.IsNullOrEmpty(dr["FeeType"].ToString())) m.FeeType = (int)dr["FeeType"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);
						
			return m;
		}

        /// <summary>
        /// 获取指定费用类型的会计科目
        /// </summary>
        /// <param name="FeeType"></param>
        /// <returns></returns>
        public DataTable GetListByFeeType(int FeeType)
        {
            SqlDataReader dr = null;
            #region	设置参数集
            SqlParameter[] prams = {
                SQLDatabase.MakeInParam("@Condition", SqlDbType.VarChar, 2000, "ISNULL(FeeType,"+FeeType.ToString()+")="+FeeType.ToString())
			};
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_GetByCondition", prams,out dr);

            return Tools.ConvertDataReaderToDataTable(dr);
        }

        public DataTable GetAllChild(int AccountTitle)
        {
            SqlDataReader dr = null;
            #region	设置参数集
            SqlParameter[] prams = {
                SQLDatabase.MakeInParam("@AccountTitle", SqlDbType.Int, 4,AccountTitle)
			};
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_GetAllChild", prams, out dr);

            return Tools.ConvertDataReaderToDataTable(dr);
        }
    }
}

