
// ===================================================================
// 文件： QNA_ResultDAL.cs
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
	///QNA_ResultBLL业务逻辑BLL类
	/// </summary>
	public class QNA_ResultBLL : BaseComplexBLL<QNA_Result,QNA_Result_Detail>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.QNA.QNA_ResultDAL";
        private QNA_ResultDAL _dal;
		
		#region 构造函数
		///<summary>
		///QNA_ResultBLL
		///</summary>
		public QNA_ResultBLL()
			: base(DALClassName)
		{
			_dal = (QNA_ResultDAL)_DAL;
            _m = new QNA_Result(); 
		}
		
		public QNA_ResultBLL(int id)
            : base(DALClassName)
        {
            _dal = (QNA_ResultDAL)_DAL;
            FillModel(id);
        }

        public QNA_ResultBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (QNA_ResultDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<QNA_Result> GetModelList(string condition)
        {
            return new QNA_ResultBLL()._GetModelList(condition);
        }
		#endregion
	}
}