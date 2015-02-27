
// ===================================================================
// 文件： QNA_QuestionDAL.cs
// 项目名称：
// 创建时间：2009/11/29
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model.QNA;
using MCSFramework.SQLDAL.QNA;

namespace MCSFramework.BLL.QNA
{
	/// <summary>
	///QNA_QuestionBLL业务逻辑BLL类
	/// </summary>
	public class QNA_QuestionBLL : BaseComplexBLL<QNA_Question,QNA_QuestionOption>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.QNA.QNA_QuestionDAL";
        private QNA_QuestionDAL _dal;
		
		#region 构造函数
		///<summary>
		///QNA_QuestionBLL
		///</summary>
		public QNA_QuestionBLL()
			: base(DALClassName)
		{
			_dal = (QNA_QuestionDAL)_DAL;
            _m = new QNA_Question(); 
		}
		
		public QNA_QuestionBLL(int id)
            : base(DALClassName)
        {
            _dal = (QNA_QuestionDAL)_DAL;
            FillModel(id);
        }

        public QNA_QuestionBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (QNA_QuestionDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<QNA_Question> GetModelList(string condition)
        {
            return new QNA_QuestionBLL()._GetModelList(condition);
        }
		#endregion

        public IList<QNA_Question> GetQuestionList()
        {
            return QNA_QuestionBLL.GetModelList("Project=" + _m.ID.ToString());
        }
	}
}