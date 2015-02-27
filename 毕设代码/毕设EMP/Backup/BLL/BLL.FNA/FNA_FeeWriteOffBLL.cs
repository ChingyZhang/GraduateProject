
// ===================================================================
// 文件： FNA_FeeWriteOffDAL.cs
// 项目名称：
// 创建时间：2009/2/22
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
    ///FNA_FeeWriteOffBLL业务逻辑BLL类
    /// </summary>
    public class FNA_FeeWriteOffBLL : BaseComplexBLL<FNA_FeeWriteOff, FNA_FeeWriteOffDetail>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.FNA.FNA_FeeWriteOffDAL";
        private FNA_FeeWriteOffDAL _dal;

        #region 构造函数
        ///<summary>
        ///FNA_FeeWriteOffBLL
        ///</summary>
        public FNA_FeeWriteOffBLL()
            : base(DALClassName)
        {
            _dal = (FNA_FeeWriteOffDAL)_DAL;
            _m = new FNA_FeeWriteOff();
        }

        public FNA_FeeWriteOffBLL(int id)
            : base(DALClassName)
        {
            _dal = (FNA_FeeWriteOffDAL)_DAL;
            FillModel(id);
        }

        public FNA_FeeWriteOffBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (FNA_FeeWriteOffDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<FNA_FeeWriteOff> GetModelList(string condition)
        {
            return new FNA_FeeWriteOffBLL()._GetModelList(condition);
        }
        #endregion

        /// <summary>
        /// 生成费用核消单号 格式：FYHX+管理单元编号+'-'+申请日期+'-'+4位流水号
        /// </summary>
        /// <param name="organizecity"></param>
        /// <returns></returns>
        public static string GenerateSheetCode(int organizecity)
        {
            FNA_FeeWriteOffDAL dal = (FNA_FeeWriteOffDAL)DataAccess.CreateObject(DALClassName);
            return dal.GenerateSheetCode(organizecity);
        }

        /// <summary>
        /// 提交费用申请
        /// </summary>
        /// <param name="id"></param>
        /// <param name="staff"></param>
        /// <returns></returns>
        public int Submit(int staff, int taskid)
        {
            return _dal.Submit(_m.ID, staff, taskid);
        }

        public static void UpdateAdjustRecord(int ID, int Staff, int Client, int AccountTitle, string OldAdjustCost, string AdjustCost, string AdjustReason)
        {
            FNA_FeeWriteOffDAL dal = (FNA_FeeWriteOffDAL)DataAccess.CreateObject(DALClassName);
            dal.UpdateAdjustRecord(ID, Staff, Client, AccountTitle, OldAdjustCost, AdjustCost, AdjustReason);
        }

        /// <summary>
        /// 获取费用核消单的总金额（含调整）
        /// </summary>
        /// <param name="detailid"></param>
        /// <returns></returns>
        public static decimal GetSumCost(int ID)
        {
            FNA_FeeWriteOffDAL dal = (FNA_FeeWriteOffDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetSumCost(ID);
        }

        /// <summary>
        /// 汇总显示指定区域(包括子区域)所有费用核消单汇总信息
        /// </summary>
        /// <param name="AccountMonth"></param>
        /// <param name="OrganizeCity"></param>
        /// <param name="FeeType"></param>
        /// <param name="State">0:所有已提交及已批复 1：已提交待我审批 2：已批复</param>
        /// <param name="TaskIDs"></param>
        /// <returns></returns>
        public static DataTable GetSummaryTotal(int AccountMonth, int OrganizeCity, int Level, int FeeType, int State, int Staff, string ExtCondition)
        {
            FNA_FeeWriteOffDAL dal = (FNA_FeeWriteOffDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetSummaryTotal(AccountMonth, OrganizeCity, Level, FeeType, State, Staff, ExtCondition);
        }
        /// <summary>
        /// 验证核销增值税发票号及验收凭证号是否唯一,如果存在返回1,不存在返回-1
        /// </summary>
        /// <param name="NOName">票号类型1.为增值税发票号 2.验收凭证号</param>
        /// <param name="NOValue">值</param>
        /// <returns></returns>
        public static int VerifyNO(int ID, int NOType, string NOValue)
        {
            FNA_FeeWriteOffDAL dal = (FNA_FeeWriteOffDAL)DataAccess.CreateObject(DALClassName);
            return dal.VerifyNO(ID, NOType,NOValue);
        }

        /// <summary>
        /// 判断指定电话的电话费是否已经核销
        /// </summary>
        /// <param name="AccountTitle"></param>
        /// <param name="Month"></param>
        /// <param name="RelateTelephone"></param>
        /// <returns></returns>
        public static bool CheckTeleFeeHasWriteOff(int AccountTitle, int Month, int RelateTelephone)
        {
            FNA_FeeWriteOffDAL dal = (FNA_FeeWriteOffDAL)DataAccess.CreateObject(DALClassName);
            return dal.CheckTeleFeeHasWriteOff(AccountTitle, Month, RelateTelephone);
        }

        /// <summary>
        /// 判断指定员工的手机费是否已经核销
        /// </summary>
        /// <param name="AccountTitle"></param>
        /// <param name="Month"></param>
        /// <param name="MobileFeeRelateStaff"></param>
        /// <returns></returns>
        public static bool CheckMobileFeeHasWriteOff(int AccountTitle, int Month, int MobileFeeRelateStaff)
        {
            FNA_FeeWriteOffDAL dal = (FNA_FeeWriteOffDAL)DataAccess.CreateObject(DALClassName);
            return dal.CheckMobileFeeHasWriteOff(AccountTitle, Month, MobileFeeRelateStaff);
        }
        /// <summary>
        /// 根据核销单号获取进货
        /// </summary>
        /// <param name="WriteOffID"></param>
        /// <returns></returns>
        public static DataTable GetPurchaseVolume(int WriteOffID)
        {
            FNA_FeeWriteOffDAL dal = (FNA_FeeWriteOffDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetPurchaseVolume(WriteOffID);
        }
    }
}