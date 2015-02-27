
// ===================================================================
// 文件： CM_KPIDAL.cs
// 项目名称：
// 创建时间：2009/3/15
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model.CM;
using MCSFramework.SQLDAL.CM;

namespace MCSFramework.BLL.CM
{
	/// <summary>
	///CM_KPIBLL业务逻辑BLL类
	/// </summary>
	public class CM_KPIBLL : BaseSimpleBLL<CM_KPI>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.CM.CM_KPIDAL";
        private CM_KPIDAL _dal;
		
		#region 构造函数
		///<summary>
		///CM_KPIBLL
		///</summary>
		public CM_KPIBLL()
			: base(DALClassName)
		{
			_dal = (CM_KPIDAL)_DAL;
            _m = new CM_KPI(); 
		}
		
		public CM_KPIBLL(int id)
            : base(DALClassName)
        {
            _dal = (CM_KPIDAL)_DAL;
            FillModel(id);
        }

        public CM_KPIBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (CM_KPIDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<CM_KPI> GetModelList(string condition)
        {
            return new CM_KPIBLL()._GetModelList(condition);
        }
		#endregion
	}
}