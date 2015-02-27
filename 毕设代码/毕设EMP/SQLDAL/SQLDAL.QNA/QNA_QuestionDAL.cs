
// ===================================================================
// 文件： QNA_QuestionDAL.cs
// 项目名称：
// 创建时间：2009/12/13
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
	///QNA_Question数据访问DAL类
	/// </summary>
	public class QNA_QuestionDAL : BaseComplexDAL<QNA_Question,QNA_QuestionOption>
	{
		#region 构造函数
		///<summary>
		///
		///</summary>
		public QNA_QuestionDAL()
		{
			_ProcePrefix = "MCS_QNA.dbo.sp_QNA_Question";
		}
		#endregion
		
		
		/// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(QNA_Question m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Project", SqlDbType.Int, 4, m.Project),
                SQLDatabase.MakeInParam("@SortCode", SqlDbType.VarChar, 20, m.SortCode),
				SQLDatabase.MakeInParam("@Title", SqlDbType.VarChar, 400, m.Title),
				SQLDatabase.MakeInParam("@Description", SqlDbType.VarChar, 2000, m.Description),
				SQLDatabase.MakeInParam("@OptionMode", SqlDbType.Int, 4, m.OptionMode),
				SQLDatabase.MakeInParam("@DefaultNextQ", SqlDbType.Int, 4, m.DefaultNextQ),
				SQLDatabase.MakeInParam("@IsFirstQ", SqlDbType.Char, 1, m.IsFirstQ),
				SQLDatabase.MakeInParam("@IsLastQ", SqlDbType.Char, 1, m.IsLastQ),
				SQLDatabase.MakeInParam("@TextRegularExp", SqlDbType.VarChar, 200, m.TextRegularExp),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
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
        public override int Update(QNA_Question m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@Project", SqlDbType.Int, 4, m.Project),
                SQLDatabase.MakeInParam("@SortCode", SqlDbType.VarChar, 20, m.SortCode),
				SQLDatabase.MakeInParam("@Title", SqlDbType.VarChar, 400, m.Title),
				SQLDatabase.MakeInParam("@Description", SqlDbType.VarChar, 2000, m.Description),
				SQLDatabase.MakeInParam("@OptionMode", SqlDbType.Int, 4, m.OptionMode),
				SQLDatabase.MakeInParam("@DefaultNextQ", SqlDbType.Int, 4, m.DefaultNextQ),
				SQLDatabase.MakeInParam("@IsFirstQ", SqlDbType.Char, 1, m.IsFirstQ),
				SQLDatabase.MakeInParam("@IsLastQ", SqlDbType.Char, 1, m.IsLastQ),
				SQLDatabase.MakeInParam("@TextRegularExp", SqlDbType.VarChar, 200, m.TextRegularExp),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@UpdateStaff", SqlDbType.Int, 4, m.UpdateStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
			#endregion
			
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
			
			return ret;
        }
		
        protected override QNA_Question FillModel(IDataReader dr)
		{
			QNA_Question m = new QNA_Question();
			if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (int)dr["ID"];
			if (!string.IsNullOrEmpty(dr["Project"].ToString()))	m.Project = (int)dr["Project"];
            if (!string.IsNullOrEmpty(dr["SortCode"].ToString())) m.SortCode = (string)dr["SortCode"];
			if (!string.IsNullOrEmpty(dr["Title"].ToString()))	m.Title = (string)dr["Title"];
			if (!string.IsNullOrEmpty(dr["Description"].ToString()))	m.Description = (string)dr["Description"];
			if (!string.IsNullOrEmpty(dr["OptionMode"].ToString()))	m.OptionMode = (int)dr["OptionMode"];
			if (!string.IsNullOrEmpty(dr["DefaultNextQ"].ToString()))	m.DefaultNextQ = (int)dr["DefaultNextQ"];
			if (!string.IsNullOrEmpty(dr["IsFirstQ"].ToString()))	m.IsFirstQ = (string)dr["IsFirstQ"];
			if (!string.IsNullOrEmpty(dr["IsLastQ"].ToString()))	m.IsLastQ = (string)dr["IsLastQ"];
			if (!string.IsNullOrEmpty(dr["TextRegularExp"].ToString()))	m.TextRegularExp = (string)dr["TextRegularExp"];
			if (!string.IsNullOrEmpty(dr["ApproveFlag"].ToString()))	m.ApproveFlag = (int)dr["ApproveFlag"];
			if (!string.IsNullOrEmpty(dr["InsertTime"].ToString()))	m.InsertTime = (DateTime)dr["InsertTime"];
			if (!string.IsNullOrEmpty(dr["InsertStaff"].ToString()))	m.InsertStaff = (int)dr["InsertStaff"];
			if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString()))	m.UpdateTime = (DateTime)dr["UpdateTime"];
			if (!string.IsNullOrEmpty(dr["UpdateStaff"].ToString()))	m.UpdateStaff = (int)dr["UpdateStaff"];
			if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString()))	m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);
						
			return m;
		}
		
		public override int AddDetail(QNA_QuestionOption m)
        {
			m.Question = HeadID;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Question", SqlDbType.Int, 4, m.Question),
				SQLDatabase.MakeInParam("@OptionName", SqlDbType.VarChar, 2000, m.OptionName),
				SQLDatabase.MakeInParam("@NextQuestion", SqlDbType.Int, 4, m.NextQuestion),
				SQLDatabase.MakeInParam("@CanInputText", SqlDbType.Char, 1, m.CanInputText),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            m.ID = SQLDatabase.RunProc(_ProcePrefix + "_AddDetail", prams);

            return m.ID;
        }

        public override int UpdateDetail(QNA_QuestionOption m)
        {
            m.Question = HeadID;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@Question", SqlDbType.Int, 4, m.Question),
				SQLDatabase.MakeInParam("@OptionName", SqlDbType.VarChar, 2000, m.OptionName),
				SQLDatabase.MakeInParam("@NextQuestion", SqlDbType.Int, 4, m.NextQuestion),
				SQLDatabase.MakeInParam("@CanInputText", SqlDbType.Char, 1, m.CanInputText),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix+"_UpdateDetail", prams);

            return ret;
        }

        protected override QNA_QuestionOption FillDetailModel(IDataReader dr)
        {
            QNA_QuestionOption m = new QNA_QuestionOption();
            if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (int)dr["ID"];
			if (!string.IsNullOrEmpty(dr["Question"].ToString()))	m.Question = (int)dr["Question"];
			if (!string.IsNullOrEmpty(dr["OptionName"].ToString()))	m.OptionName = (string)dr["OptionName"];
			if (!string.IsNullOrEmpty(dr["NextQuestion"].ToString()))	m.NextQuestion = (int)dr["NextQuestion"];
			if (!string.IsNullOrEmpty(dr["CanInputText"].ToString()))	m.CanInputText = (string)dr["CanInputText"];
			if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString()))	m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);
					

            return m;
        }
    }
}

