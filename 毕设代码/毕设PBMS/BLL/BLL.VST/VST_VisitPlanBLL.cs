
// ===================================================================
// 文件： VST_VisitPlanDAL.cs
// 项目名称：
// 创建时间：2015-03-13
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
	///VST_VisitPlanBLL业务逻辑BLL类
	/// </summary>
	public class VST_VisitPlanBLL : BaseComplexBLL<VST_VisitPlan,VST_VisitPlan_Detail>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.VST.VST_VisitPlanDAL";
        private VST_VisitPlanDAL _dal;
		
		#region 构造函数
		///<summary>
		///VST_VisitPlanBLL
		///</summary>
		public VST_VisitPlanBLL()
			: base(DALClassName)
		{
			_dal = (VST_VisitPlanDAL)_DAL;
            _m = new VST_VisitPlan(); 
		}
		
		public VST_VisitPlanBLL(int id)
            : base(DALClassName)
        {
            _dal = (VST_VisitPlanDAL)_DAL;
            FillModel(id);
        }

        public VST_VisitPlanBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (VST_VisitPlanDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<VST_VisitPlan> GetModelList(string condition)
        {
            return new VST_VisitPlanBLL()._GetModelList(condition);
        }
		#endregion
	}
}