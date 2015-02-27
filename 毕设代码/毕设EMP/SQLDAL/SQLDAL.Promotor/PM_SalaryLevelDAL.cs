
// ===================================================================
// 文件： PM_SalaryLevelDAL.cs
// 项目名称：
// 创建时间：2009/3/19
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.Promotor;


namespace MCSFramework.SQLDAL.Promotor
{
	/// <summary>
	///PM_SalaryLevel数据访问DAL类
	/// </summary>
	public class PM_SalaryLevelDAL : BaseComplexDAL<PM_SalaryLevel,PM_SalaryLevelDetail>
	{
		#region 构造函数
		///<summary>
		///
		///</summary>
		public PM_SalaryLevelDAL()
		{
			_ProcePrefix = "MCS_Promotor.dbo.sp_PM_SalaryLevel";
		}
		#endregion
		
		
		/// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(PM_SalaryLevel m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, m.OrganizeCity),
				SQLDatabase.MakeInParam("@Name", SqlDbType.VarChar, 50, m.Name),
				SQLDatabase.MakeInParam("@BonusMode", SqlDbType.Int, 4, m.BonusMode),
				SQLDatabase.MakeInParam("@ComputMethd", SqlDbType.Int, 4, m.ComputMethd),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@InputStaff", SqlDbType.Int, 4, m.InputStaff),
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
        public override int Update(PM_SalaryLevel m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, m.OrganizeCity),
				SQLDatabase.MakeInParam("@Name", SqlDbType.VarChar, 50, m.Name),
				SQLDatabase.MakeInParam("@BonusMode", SqlDbType.Int, 4, m.BonusMode),
				SQLDatabase.MakeInParam("@ComputMethd", SqlDbType.Int, 4, m.ComputMethd),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@UpdateStaff", SqlDbType.Int, 4, m.UpdateStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 8000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
			#endregion
			
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
			
			return ret;
        }
		
        protected override PM_SalaryLevel FillModel(IDataReader dr)
		{
			PM_SalaryLevel m = new PM_SalaryLevel();
			if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (int)dr["ID"];
			if (!string.IsNullOrEmpty(dr["OrganizeCity"].ToString()))	m.OrganizeCity = (int)dr["OrganizeCity"];
			if (!string.IsNullOrEmpty(dr["Name"].ToString()))	m.Name = (string)dr["Name"];
			if (!string.IsNullOrEmpty(dr["BonusMode"].ToString()))	m.BonusMode = (int)dr["BonusMode"];
			if (!string.IsNullOrEmpty(dr["ComputMethd"].ToString()))	m.ComputMethd = (int)dr["ComputMethd"];
			if (!string.IsNullOrEmpty(dr["ApproveFlag"].ToString()))	m.ApproveFlag = (int)dr["ApproveFlag"];
			if (!string.IsNullOrEmpty(dr["InputTime"].ToString()))	m.InputTime = (DateTime)dr["InputTime"];
			if (!string.IsNullOrEmpty(dr["InputStaff"].ToString()))	m.InputStaff = (int)dr["InputStaff"];
			if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString()))	m.UpdateTime = (DateTime)dr["UpdateTime"];
			if (!string.IsNullOrEmpty(dr["UpdateStaff"].ToString()))	m.UpdateStaff = (int)dr["UpdateStaff"];
			if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString()))	m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);
						
			return m;
		}
		
		public override int AddDetail(PM_SalaryLevelDetail m)
        {
			m.LevelID = HeadID;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@LevelID", SqlDbType.Int, 4, m.LevelID),
				SQLDatabase.MakeInParam("@Complete1", SqlDbType.Decimal, 9, m.Complete1),
				SQLDatabase.MakeInParam("@Complete2", SqlDbType.Decimal, 9, m.Complete2),
				SQLDatabase.MakeInParam("@Rate", SqlDbType.Decimal, 9, m.Rate)
			};
            #endregion

            m.ID = SQLDatabase.RunProc(_ProcePrefix + "_AddDetail", prams);

            return m.ID;
        }

        public override int UpdateDetail(PM_SalaryLevelDetail m)
        {
            m.LevelID = HeadID;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@LevelID", SqlDbType.Int, 4, m.LevelID),
				SQLDatabase.MakeInParam("@Complete1", SqlDbType.Decimal, 9, m.Complete1),
				SQLDatabase.MakeInParam("@Complete2", SqlDbType.Decimal, 9, m.Complete2),
				SQLDatabase.MakeInParam("@Rate", SqlDbType.Decimal, 9, m.Rate)
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix+"_UpdateDetail", prams);

            return ret;
        }

        protected override PM_SalaryLevelDetail FillDetailModel(IDataReader dr)
        {
            PM_SalaryLevelDetail m = new PM_SalaryLevelDetail();
            if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (int)dr["ID"];
			if (!string.IsNullOrEmpty(dr["LevelID"].ToString()))	m.LevelID = (int)dr["LevelID"];
			if (!string.IsNullOrEmpty(dr["Complete1"].ToString()))	m.Complete1 = (decimal)dr["Complete1"];
			if (!string.IsNullOrEmpty(dr["Complete2"].ToString()))	m.Complete2 = (decimal)dr["Complete2"];
			if (!string.IsNullOrEmpty(dr["Rate"].ToString()))	m.Rate = (decimal)dr["Rate"];
					

            return m;
        }
    }
}

