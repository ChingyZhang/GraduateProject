
// ===================================================================
// 文件： FNA_BudgetTransferApplyDAL.cs
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
    ///FNA_BudgetTransferApplyBLL业务逻辑BLL类
    /// </summary>
    public class FNA_BudgetTransferApplyBLL : BaseSimpleBLL<FNA_BudgetTransferApply>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.FNA.FNA_BudgetTransferApplyDAL";
        private FNA_BudgetTransferApplyDAL _dal;

        #region 构造函数
        ///<summary>
        ///FNA_BudgetTransferApplyBLL
        ///</summary>
        public FNA_BudgetTransferApplyBLL()
            : base(DALClassName)
        {
            _dal = (FNA_BudgetTransferApplyDAL)_DAL;
            _m = new FNA_BudgetTransferApply();
        }

        public FNA_BudgetTransferApplyBLL(int id)
            : base(DALClassName)
        {
            _dal = (FNA_BudgetTransferApplyDAL)_DAL;
            FillModel(id);
        }

        public FNA_BudgetTransferApplyBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (FNA_BudgetTransferApplyDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<FNA_BudgetTransferApply> GetModelList(string condition)
        {
            return new FNA_BudgetTransferApplyBLL()._GetModelList(condition);
        }
        #endregion

        public int Submit(int Staff, int ApproveTask)
        {
            return _dal.Submit(_m.ID, Staff, ApproveTask);
        }
    }
}