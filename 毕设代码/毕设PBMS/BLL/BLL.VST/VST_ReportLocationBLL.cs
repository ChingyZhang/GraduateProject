
// ===================================================================
// 文件： VST_ReportLocationDAL.cs
// 项目名称：
// 创建时间：2015-04-12
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
	///VST_ReportLocationBLL业务逻辑BLL类
	/// </summary>
	public class VST_ReportLocationBLL : BaseSimpleBLL<VST_ReportLocation>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.VST.VST_ReportLocationDAL";
        private VST_ReportLocationDAL _dal;
		
		#region 构造函数
		///<summary>
		///VST_ReportLocationBLL
		///</summary>
		public VST_ReportLocationBLL()
			: base(DALClassName)
		{
			_dal = (VST_ReportLocationDAL)_DAL;
            _m = new VST_ReportLocation(); 
		}
		
		public VST_ReportLocationBLL(int id)
            : base(DALClassName)
        {
            _dal = (VST_ReportLocationDAL)_DAL;
            FillModel(id);
        }

        public VST_ReportLocationBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (VST_ReportLocationDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<VST_ReportLocation> GetModelList(string condition)
        {
            return new VST_ReportLocationBLL()._GetModelList(condition);
        }
		#endregion
	}
}