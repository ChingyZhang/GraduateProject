
// ===================================================================
// 文件： VST_WorkListDAL.cs
// 项目名称：
// 创建时间：2015-02-01
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model.VST;
using MCSFramework.SQLDAL.VST;

namespace MCSFramework.BLL.VST
{
	/// <summary>
	///VST_WorkListBLL业务逻辑BLL类
	/// </summary>
	public class VST_WorkListBLL : BaseComplexBLL<VST_WorkList,VST_WorkItem>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.VST.VST_WorkListDAL";
        private VST_WorkListDAL _dal;
		
		#region 构造函数
		///<summary>
		///VST_WorkListBLL
		///</summary>
		public VST_WorkListBLL()
			: base(DALClassName)
		{
			_dal = (VST_WorkListDAL)_DAL;
            _m = new VST_WorkList(); 
		}
		
		public VST_WorkListBLL(int id)
            : base(DALClassName)
        {
            _dal = (VST_WorkListDAL)_DAL;
            FillModel(id);
        }

        public VST_WorkListBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (VST_WorkListDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<VST_WorkList> GetModelList(string condition)
        {
            return new VST_WorkListBLL()._GetModelList(condition);
        }
		#endregion
	}
}