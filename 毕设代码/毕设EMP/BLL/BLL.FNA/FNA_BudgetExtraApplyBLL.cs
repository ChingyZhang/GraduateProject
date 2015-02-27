
// ===================================================================
// 文件： FNA_BudgetExtraApplyDAL.cs
// 项目名称：
// 创建时间：2010/8/19
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model.FNA;
using MCSFramework.SQLDAL.FNA;

namespace MCSFramework.BLL.FNA
{
	/// <summary>
	///FNA_BudgetExtraApplyBLL业务逻辑BLL类
	/// </summary>
	public class FNA_BudgetExtraApplyBLL : BaseSimpleBLL<FNA_BudgetExtraApply>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.FNA.FNA_BudgetExtraApplyDAL";
        private FNA_BudgetExtraApplyDAL _dal;
		
		#region 构造函数
		///<summary>
		///FNA_BudgetExtraApplyBLL
		///</summary>
		public FNA_BudgetExtraApplyBLL()
			: base(DALClassName)
		{
			_dal = (FNA_BudgetExtraApplyDAL)_DAL;
            _m = new FNA_BudgetExtraApply(); 
		}
		
		public FNA_BudgetExtraApplyBLL(int id)
            : base(DALClassName)
        {
            _dal = (FNA_BudgetExtraApplyDAL)_DAL;
            FillModel(id);
        }

        public FNA_BudgetExtraApplyBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (FNA_BudgetExtraApplyDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<FNA_BudgetExtraApply> GetModelList(string condition)
        {
            return new FNA_BudgetExtraApplyBLL()._GetModelList(condition);
        }
		#endregion

        public int Submit(int Staff, int ApproveTask)
        {
            return _dal.Submit(_m.ID, Staff, ApproveTask);
        }

        /// <summary>
        /// 获取指定管理片区指定月的已批复扩充的总预算额度
        /// </summary>
        /// <param name="AccountMonth"></param>
        /// <param name="OrganizeCity"></param>
        /// <param name="FeeType"></param>
        /// <param name="IncludeChildOrganizeCity">是否包括子管理片区 0:不包括 1:包括</param>
        /// <returns></returns>
        public static decimal GetExtraAmount(int AccountMonth, int OrganizeCity, int FeeType, bool IncludeChildOrganizeCity)
        {
            FNA_BudgetExtraApplyDAL dal = (FNA_BudgetExtraApplyDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetExtraAmount(AccountMonth, OrganizeCity, FeeType, IncludeChildOrganizeCity);
        }

        /// <summary>
        /// 生成费用申请单号 格式：KZSQ+管理单元编号+'-'+申请日期+'-'+4位流水号
        /// </summary>
        /// <param name="organizecity"></param>
        /// <returns></returns>
        public static string GenerateSheetCode(int organizecity)
        {
            FNA_BudgetExtraApplyDAL dal = (FNA_BudgetExtraApplyDAL)DataAccess.CreateObject(DALClassName);
            return dal.GenerateSheetCode(organizecity);
        }

        public static void UpdateAdjustRecord(int ID, int Staff, int FeeType, string OldAdjustCost, string AdjustCost, string AdjustReason)
        {
            FNA_BudgetExtraApplyDAL dal = (FNA_BudgetExtraApplyDAL)DataAccess.CreateObject(DALClassName);
            dal.UpdateAdjustRecord(ID, Staff, FeeType, OldAdjustCost, AdjustCost, AdjustReason);
        }

	}
}