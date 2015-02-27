
// ===================================================================
// 文件： CM_RebateRule_ApplyCityDAL.cs
// 项目名称：
// 创建时间：2011/9/18
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
	///CM_RebateRule_ApplyCityBLL业务逻辑BLL类
	/// </summary>
	public class CM_RebateRule_ApplyCityBLL : BaseSimpleBLL<CM_RebateRule_ApplyCity>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.CM.CM_RebateRule_ApplyCityDAL";
        private CM_RebateRule_ApplyCityDAL _dal;
		
		#region 构造函数
		///<summary>
		///CM_RebateRule_ApplyCityBLL
		///</summary>
		public CM_RebateRule_ApplyCityBLL()
			: base(DALClassName)
		{
			_dal = (CM_RebateRule_ApplyCityDAL)_DAL;
            _m = new CM_RebateRule_ApplyCity(); 
		}
		
		public CM_RebateRule_ApplyCityBLL(int id)
            : base(DALClassName)
        {
            _dal = (CM_RebateRule_ApplyCityDAL)_DAL;
            FillModel(id);
        }

        public CM_RebateRule_ApplyCityBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (CM_RebateRule_ApplyCityDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<CM_RebateRule_ApplyCity> GetModelList(string condition)
        {
            return new CM_RebateRule_ApplyCityBLL()._GetModelList(condition);
        }
		#endregion
	}
}