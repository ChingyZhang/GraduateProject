
// ===================================================================
// 文件： JN_JournalDAL.cs
// 项目名称：
// 创建时间：2009/6/21
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model.OA;
using MCSFramework.SQLDAL.OA;

namespace MCSFramework.BLL.OA
{
	/// <summary>
	///JN_JournalBLL业务逻辑BLL类
	/// </summary>
	public class JN_JournalBLL : BaseSimpleBLL<JN_Journal>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.OA.JN_JournalDAL";
        private JN_JournalDAL _dal;
		
		#region 构造函数
		///<summary>
		///JN_JournalBLL
		///</summary>
		public JN_JournalBLL()
			: base(DALClassName)
		{
			_dal = (JN_JournalDAL)_DAL;
            _m = new JN_Journal(); 
		}
		
		public JN_JournalBLL(int id)
            : base(DALClassName)
        {
            _dal = (JN_JournalDAL)_DAL;
            FillModel(id);
        }

        public JN_JournalBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (JN_JournalDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<JN_Journal> GetModelList(string condition)
        {
            return new JN_JournalBLL()._GetModelList(condition);
        }
		#endregion
	}
}