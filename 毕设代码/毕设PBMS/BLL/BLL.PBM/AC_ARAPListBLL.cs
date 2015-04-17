
// ===================================================================
// 文件： AC_ARAPListDAL.cs
// 项目名称：
// 创建时间：2015-01-27
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model.PBM;
using MCSFramework.SQLDAL.PBM;

namespace MCSFramework.BLL.PBM
{
	/// <summary>
	///AC_ARAPListBLL业务逻辑BLL类
	/// </summary>
	public class AC_ARAPListBLL : BaseSimpleBLL<AC_ARAPList>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.PBM.AC_ARAPListDAL";
        private AC_ARAPListDAL _dal;
		
		#region 构造函数
		///<summary>
		///AC_ARAPListBLL
		///</summary>
		public AC_ARAPListBLL()
			: base(DALClassName)
		{
			_dal = (AC_ARAPListDAL)_DAL;
            _m = new AC_ARAPList(); 
		}
		
		public AC_ARAPListBLL(int id)
            : base(DALClassName)
        {
            _dal = (AC_ARAPListDAL)_DAL;
            FillModel(id);
        }

        public AC_ARAPListBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (AC_ARAPListDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<AC_ARAPList> GetModelList(string condition)
        {
            return new AC_ARAPListBLL()._GetModelList(condition);
        }
		#endregion
	}
}