
// ===================================================================
// 文件： QNA_ResultDAL.cs
// 项目名称：
// 创建时间：2009/11/29
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.QNA;


namespace MCSFramework.SQLDAL.QNA
{
	/// <summary>
	///QNA_Result数据访问DAL类
	/// </summary>
	public class QNA_ResultDAL : BaseComplexDAL<QNA_Result,QNA_Result_Detail>
	{
		#region 构造函数
		///<summary>
		///
		///</summary>
		public QNA_ResultDAL()
		{
			_ProcePrefix = "MCS_QNA.dbo.sp_QNA_Result";
		}
		#endregion
		
		
		/// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(QNA_Result m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Project", SqlDbType.Int, 4, m.Project),
				SQLDatabase.MakeInParam("@RelateClient", SqlDbType.Int, 4, m.RelateClient),
				SQLDatabase.MakeInParam("@RelateTask", SqlDbType.Int, 4, m.RelateTask),
				SQLDatabase.MakeInParam("@IsCommit", SqlDbType.Char, 1, m.IsCommit),
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
        public override int Update(QNA_Result m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@Project", SqlDbType.Int, 4, m.Project),
				SQLDatabase.MakeInParam("@RelateClient", SqlDbType.Int, 4, m.RelateClient),
				SQLDatabase.MakeInParam("@RelateTask", SqlDbType.Int, 4, m.RelateTask),
				SQLDatabase.MakeInParam("@IsCommit", SqlDbType.Char, 1, m.IsCommit),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
			#endregion
			
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
			
			return ret;
        }
		
        protected override QNA_Result FillModel(IDataReader dr)
		{
			QNA_Result m = new QNA_Result();
			if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (int)dr["ID"];
			if (!string.IsNullOrEmpty(dr["Project"].ToString()))	m.Project = (int)dr["Project"];
			if (!string.IsNullOrEmpty(dr["RelateClient"].ToString()))	m.RelateClient = (int)dr["RelateClient"];
			if (!string.IsNullOrEmpty(dr["RelateTask"].ToString()))	m.RelateTask = (int)dr["RelateTask"];
			if (!string.IsNullOrEmpty(dr["IsCommit"].ToString()))	m.IsCommit = (string)dr["IsCommit"];
			if (!string.IsNullOrEmpty(dr["InsertTime"].ToString()))	m.InsertTime = (DateTime)dr["InsertTime"];
			if (!string.IsNullOrEmpty(dr["InsertStaff"].ToString()))	m.InsertStaff = (int)dr["InsertStaff"];
			if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString()))	m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);
						
			return m;
		}
		
		public override int AddDetail(QNA_Result_Detail m)
        {
			m.Result = HeadID;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Result", SqlDbType.Int, 4, m.Result),
				SQLDatabase.MakeInParam("@Question", SqlDbType.Int, 4, m.Question),
				SQLDatabase.MakeInParam("@Option", SqlDbType.Int, 4, m.Option),
				SQLDatabase.MakeInParam("@OptionText", SqlDbType.VarChar, 1000, m.OptionText),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            m.ID = SQLDatabase.RunProc(_ProcePrefix + "_AddDetail", prams);

            return m.ID;
        }

        public override int UpdateDetail(QNA_Result_Detail m)
        {
            m.Result = HeadID;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@Result", SqlDbType.Int, 4, m.Result),
				SQLDatabase.MakeInParam("@Question", SqlDbType.Int, 4, m.Question),
				SQLDatabase.MakeInParam("@Option", SqlDbType.Int, 4, m.Option),
				SQLDatabase.MakeInParam("@OptionText", SqlDbType.VarChar, 1000, m.OptionText),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix+"_UpdateDetail", prams);

            return ret;
        }

        protected override QNA_Result_Detail FillDetailModel(IDataReader dr)
        {
            QNA_Result_Detail m = new QNA_Result_Detail();
            if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (int)dr["ID"];
			if (!string.IsNullOrEmpty(dr["Result"].ToString()))	m.Result = (int)dr["Result"];
			if (!string.IsNullOrEmpty(dr["Question"].ToString()))	m.Question = (int)dr["Question"];
			if (!string.IsNullOrEmpty(dr["Option"].ToString()))	m.Option = (int)dr["Option"];
			if (!string.IsNullOrEmpty(dr["OptionText"].ToString()))	m.OptionText = (string)dr["OptionText"];
			if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString()))	m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);
					

            return m;
        }
    }
}

