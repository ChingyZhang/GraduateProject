
// ===================================================================
// 文件： Rpt_FolderDAL.cs
// 项目名称：
// 创建时间：2010/9/25
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model.RPT;
using MCSFramework.SQLDAL.RPT;

namespace MCSFramework.BLL.RPT
{
	/// <summary>
	///Rpt_FolderBLL业务逻辑BLL类
	/// </summary>
	public class Rpt_FolderBLL : BaseSimpleBLL<Rpt_Folder>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.RPT.Rpt_FolderDAL";
        private Rpt_FolderDAL _dal;
		
		#region 构造函数
		///<summary>
		///Rpt_FolderBLL
		///</summary>
		public Rpt_FolderBLL()
			: base(DALClassName)
		{
			_dal = (Rpt_FolderDAL)_DAL;
            _m = new Rpt_Folder(); 
		}
		
		public Rpt_FolderBLL(int id)
            : base(DALClassName)
        {
            _dal = (Rpt_FolderDAL)_DAL;
            FillModel(id);
        }

        public Rpt_FolderBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (Rpt_FolderDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<Rpt_Folder> GetModelList(string condition)
        {
            return new Rpt_FolderBLL()._GetModelList(condition);
        }
		#endregion
	}
}