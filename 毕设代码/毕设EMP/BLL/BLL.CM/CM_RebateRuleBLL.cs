
// ===================================================================
// 文件： CM_RebateRuleDAL.cs
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
	///CM_RebateRuleBLL业务逻辑BLL类
	/// </summary>
	public class CM_RebateRuleBLL : BaseSimpleBLL<CM_RebateRule>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.CM.CM_RebateRuleDAL";
        private CM_RebateRuleDAL _dal;
		
		#region 构造函数
		///<summary>
		///CM_RebateRuleBLL
		///</summary>
		public CM_RebateRuleBLL()
			: base(DALClassName)
		{
			_dal = (CM_RebateRuleDAL)_DAL;
            _m = new CM_RebateRule(); 
		}
		
		public CM_RebateRuleBLL(int id)
            : base(DALClassName)
        {
            _dal = (CM_RebateRuleDAL)_DAL;
            FillModel(id);
        }

        public CM_RebateRuleBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (CM_RebateRuleDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<CM_RebateRule> GetModelList(string condition)
        {
            return new CM_RebateRuleBLL()._GetModelList(condition);
        }
		#endregion
	}
}