
// ===================================================================
// 文件： FNA_FeeApplyDAL.cs
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
    ///FNA_FeeApplyBLL业务逻辑BLL类
    /// </summary>
    public class FNA_FeeApplyBLL : BaseComplexBLL<FNA_FeeApply, FNA_FeeApplyDetail>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.FNA.FNA_FeeApplyDAL";
        private FNA_FeeApplyDAL _dal;

        #region 构造函数
        ///<summary>
        ///FNA_FeeApplyBLL
        ///</summary>
        public FNA_FeeApplyBLL()
            : base(DALClassName)
        {
            _dal = (FNA_FeeApplyDAL)_DAL;
            _m = new FNA_FeeApply();
        }

        public FNA_FeeApplyBLL(int id)
            : base(DALClassName)
        {
            _dal = (FNA_FeeApplyDAL)_DAL;
            FillModel(id);
        }

        public FNA_FeeApplyBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (FNA_FeeApplyDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<FNA_FeeApply> GetModelList(string condition)
        {
            return new FNA_FeeApplyBLL()._GetModelList(condition);
        }
        #endregion

        #region 费用明细所细分的品牌
        public static int AddBrandRate(FNA_FeeApplyDetail_BrandRate m)
        {
            FNA_FeeApplyDAL dal = (FNA_FeeApplyDAL)DataAccess.CreateObject(DALClassName);
            return dal.AddBrandRate(m);
        }
        public static IList<FNA_FeeApplyDetail_BrandRate> GetBrandRates(int DetailID)
        {
            FNA_FeeApplyDAL dal = (FNA_FeeApplyDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetBrandRates(DetailID);
        }
        #endregion
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

        /// <summary>
        /// 生成费用申请单号 格式：FYSQ+管理单元编号+'-'+申请日期+'-'+4位流水号
        /// </summary>
        /// <param name="organizecity"></param>
        /// <returns></returns>
        public static string GenerateSheetCode(int organizecity, int month)
        {
            FNA_FeeApplyDAL dal = (FNA_FeeApplyDAL)DataAccess.CreateObject(DALClassName);
            return dal.GenerateSheetCode(organizecity, month);
        }

        /// <summary>
        /// 根据费用申请明细的获取费用申请单号
        /// </summary>
        /// <param name="detailid"></param>
        /// <returns></returns>
        public static string GetSheetCodeByDetailID(int DetailID)
        {
            FNA_FeeApplyDAL dal = (FNA_FeeApplyDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetSheetCodeByDetailID(DetailID);
        }

        /// <summary>
        /// 获取费用申请单的总金额（含调整）
        /// </summary>
        /// <param name="detailid"></param>
        /// <returns></returns>
        public static decimal GetSumCost(int ID)
        {
            FNA_FeeApplyDAL dal = (FNA_FeeApplyDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetSumCost(ID);
        }

        /// <summary>
        /// 获取费用申请单的可核销金额
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static decimal GetAvailCost(int ID)
        {
            FNA_FeeApplyDAL dal = (FNA_FeeApplyDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetAvailCost(ID);
        }

        #region 获取指定地区内所有费用申请单的总金额
        public static decimal GetApplyTotalCost(int AccountMonth, int OrganizeCity, int FeeType, bool IncludeChildOrganizeCity)
        {
            FNA_FeeApplyDAL dal = (FNA_FeeApplyDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetApplyTotalCost(AccountMonth, OrganizeCity, FeeType, IncludeChildOrganizeCity);
        }
        public static decimal GetApplyTotalCost(int AccountMonth, int OrganizeCity, int FeeType)
        {
            FNA_FeeApplyDAL dal = (FNA_FeeApplyDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetApplyTotalCost(AccountMonth, OrganizeCity, FeeType, false);
        }
        #endregion

        public static DataTable GetFeeApplyOrWriteoffByClient(int Client, int BeginMonth, int EndMonth)
        {
            FNA_FeeApplyDAL dal = (FNA_FeeApplyDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetFeeApplyOrWriteoffByClient(Client, BeginMonth, EndMonth);
        }

        public static void UpdateAdjustRecord(int ID, int Staff, int AccountTitle, string OldAdjustCost, string AdjustCost, string AdjustReason)
        {
            FNA_FeeApplyDAL dal = (FNA_FeeApplyDAL)DataAccess.CreateObject(DALClassName);
            dal.UpdateAdjustRecord(ID, Staff, AccountTitle, OldAdjustCost, AdjustCost, AdjustReason);
        }

        public static bool CheckFNAByAccontTitle(int ClientID, int AccountMonth, int AccountTitle, decimal ApplyCost)
        {
            FNA_FeeApplyDAL dal = (FNA_FeeApplyDAL)DataAccess.CreateObject(DALClassName);
            return dal.CheckFNAByAccontTitle(ClientID, AccountMonth, AccountTitle, ApplyCost);
        }

        /// <summary>
        /// 复制费用申请单,新单为未提交状态
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Staff"></param>
        /// <returns></returns>
        public static int Copy(int ID, int Staff, int Month)
        {
            FNA_FeeApplyDAL dal = (FNA_FeeApplyDAL)DataAccess.CreateObject(DALClassName);
            return dal.Copy(ID, Staff, Month);
        }

        /// <summary>
        /// 取消已审批通过待核销部分的费用申请
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Staff"></param>
        /// <returns></returns>
        public static int Cancel(int ID, int Staff)
        {
            FNA_FeeApplyDAL dal = (FNA_FeeApplyDAL)DataAccess.CreateObject(DALClassName);
            return dal.Cancel(ID, Staff);
        }
        /// <summary>
        /// 汇总显示指定区域(包括子区域)所有费用申请单汇总信息
        /// </summary>
        /// <param name="AccountMonth"></param>
        /// <param name="OrganizeCity"></param>
        /// <param name="FeeType"></param>
        /// <param name="State">0:所有已提交及已批复 1：已提交待我审批 2：已批复</param>
        /// <param name="Flag">0:所有(后三者之和) 1:本月申请的所有费用 2:本月申请的预支费用 3:仅发生在本月的费用</param>
        /// <param name="Staff"></param>
        /// <returns></returns>
        public static DataTable GetSummaryTotal(int AccountMonth, int OrganizeCity, int Level, int FeeType, int State, int Flag, int Staff)
        {
            FNA_FeeApplyDAL dal = (FNA_FeeApplyDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetSummaryTotal(AccountMonth, OrganizeCity, Level, FeeType, State, Flag, Staff);
        }
        public static DataTable GetSummaryTotal_Sub(int AccountMonth, int OrganizeCity, int Level, int FeeType, int State, int Flag, int Staff)
        {
            FNA_FeeApplyDAL dal = (FNA_FeeApplyDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetSummaryTotal_Sub(AccountMonth, OrganizeCity, Level, FeeType, State, Flag, Staff);
        }
        /// <summary>
        /// 按经销商查看下游零售商费用申请汇总信息
        /// </summary>
        /// <param name="Client">经销商ID</param>
        /// <param name="BeginMonth"></param>
        /// <param name="EndMonth"></param>
        /// <param name="FeeType"></param>
        /// <returns></returns>
        public static DataTable GetSummaryTotalByDistributor(int Client, int BeginMonth, int EndMonth, int FeeType)
        {
            FNA_FeeApplyDAL dal = (FNA_FeeApplyDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetSummaryTotalByDistributor(Client, BeginMonth, EndMonth, FeeType);
        }

        #region 获取指定客户指定科目前一次费用申请信息
        public static FNA_FeeApplyDetail GetPreApplyInfoByClient(int DetailID)
        {
            FNA_FeeApplyDAL dal = (FNA_FeeApplyDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetPreApplyInfo(DetailID);
        }
        #endregion

        #region 获取指定管理片区指定月的终结预算额度
        /// <summary>
        ///  获取指定管理片区指定月的终结预算额度
        /// </summary>
        /// <param name="AccountMonth">会计月</param>
        /// <param name="OrganizeCity">管理片区</param>
        /// <param name="FeeType">费用类型</param>
        /// <param name="IncludeChildOrganizeCity">是否包括下级子管理片区</param>
        /// <returns>终结费用</returns>
        public static decimal GetCancelCost(int AccountMonth, int OrganizeCity, int FeeType, bool IncludeChildOrganizeCity)
        {
            FNA_FeeApplyDAL dal = (FNA_FeeApplyDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetCancelCost(AccountMonth, OrganizeCity, FeeType, IncludeChildOrganizeCity);
        }

        /// <summary>
        /// 获取指定管理片区(不含下级片区)指定月的终结预算额度
        /// </summary>
        /// <param name="AccountMonth">会计月</param>
        /// <param name="OrganizeCity">管理片区</param>
        /// <param name="FeeType">费用类型</param>
        /// <returns>终结费用</returns>
        public static decimal GetCancelCost(int AccountMonth, int OrganizeCity, int FeeType)
        {
            FNA_FeeApplyDAL dal = (FNA_FeeApplyDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetCancelCost(AccountMonth, OrganizeCity, FeeType, false);
        }
        #endregion

        #region 获取陈列费用
        /// <summary>
        /// 汇总省区所有陈列费用申请
        /// </summary>
        /// <param name="AccountMonth"></param>
        /// <param name="OrganizeCity"></param>
        /// <param name="State">-0:所有已提交及已批复 1：已提交待我审批 2：已批复 3:由我批复的</param>
        /// <param name="Staff"></param>
        /// <returns></returns>
        public static DataTable GetDiaplayFeeSummary(int AccountMonth, int OrganizeCity, int level, int State, int Staff)
        {
            FNA_FeeApplyDAL dal = (FNA_FeeApplyDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetDiaplayFeeSummary(AccountMonth, OrganizeCity, level, State, Staff);
        }

        /// <summary>
        /// 获取陈列费用明细表
        /// </summary>
        /// <param name="AccountMonth"></param>
        /// <param name="OrganizeCity"></param>
        /// <param name="State"></param>
        /// <param name="Staff"></param>
        /// <param name="RTChannel"></param>
        /// <param name="ApplyCostCondition"></param>
        /// <param name="RTType"></param>
        /// <param name="ATSuppierIDs"></param>
        /// <returns></returns>
        public static DataTable GetRTChannelDiaplayFee(int AccountMonth, int OrganizeCity, int State, int Staff, int RTChannel, string ApplyCostCondition, int RTType, string ATSuppierIDs)
        {
            FNA_FeeApplyDAL dal = (FNA_FeeApplyDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetRTChannelDiaplayFee(AccountMonth, OrganizeCity, State, Staff, RTChannel, ApplyCostCondition, RTType, ATSuppierIDs);
        }
        /// <summary>
        /// 获取陈列费用明细表
        /// </summary>
        /// <param name="AccountMonth"></param>
        /// <param name="OrganizeCity"></param>
        /// <param name="State"></param>
        /// <param name="Staff"></param>
        /// <param name="RTChannel"></param>
        /// <returns></returns>
        public static DataTable GetRTChannelDiaplayFee(int AccountMonth, int OrganizeCity, int State, int Staff, int RTChannel)
        {
            FNA_FeeApplyDAL dal = (FNA_FeeApplyDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetRTChannelDiaplayFee(AccountMonth, OrganizeCity, State, Staff, RTChannel, "", 0, "");
        }
        /// <summary>
        /// 按陈列方式汇总陈列费
        /// </summary>
        /// <param name="AccountMonth"></param>
        /// <param name="OrganizeCity"></param>
        /// <param name="level"></param>
        /// <param name="State"></param>
        /// <param name="Staff"></param>
        /// <returns></returns>
        public static DataTable GetDiaplayFeeByDisplay(int AccountMonth, int OrganizeCity, int level, int State, int Staff)
        {
            FNA_FeeApplyDAL dal = (FNA_FeeApplyDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetDiaplayFeeByDisplay(AccountMonth, OrganizeCity, level, State, Staff);
        }
        /// <summary>
        /// 按付款周期汇总陈列费
        /// </summary>
        /// <param name="AccountMonth"></param>
        /// <param name="OrganizeCity"></param>
        /// <param name="level"></param>
        /// <param name="State"></param>
        /// <param name="Staff"></param>
        /// <returns></returns>
        public static DataTable GetByPayMode(int AccountMonth, int OrganizeCity, int level, int State, int Staff)
        {
            FNA_FeeApplyDAL dal = (FNA_FeeApplyDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetByPayMode(AccountMonth, OrganizeCity, level, State, Staff);
        }
        #endregion

        #region 获取返利费用
        /// <summary>
        /// 获取返利费用汇总表
        /// </summary>
        /// <param name="AccountMonth"></param>
        /// <param name="OrganizeCity"></param>
        /// <param name="level"></param>
        /// <param name="State"></param>
        /// <param name="Staff"></param>
        /// <returns></returns>
        public static DataTable GetFLFeeSummary(int AccountMonth, int OrganizeCity, int level, int State, int Staff)
        {
            FNA_FeeApplyDAL dal = (FNA_FeeApplyDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetFLFeeSummary(AccountMonth, OrganizeCity, level, State, Staff);
        }
        public static DataTable GetFLFeeSummary2(int AccountMonth, int OrganizeCity, int level, int State, int Staff)
        {
            FNA_FeeApplyDAL dal = (FNA_FeeApplyDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetFLFeeSummary2(AccountMonth, OrganizeCity, level, State, Staff);
        }

        /// <summary>
        /// 获取返利费用明细
        /// </summary>
        /// <param name="AccountMonth"></param>
        /// <param name="OrganizeCity"></param>
        /// <param name="State"></param>
        /// <param name="Staff"></param>
        /// <param name="RTChannel"></param>
        /// <param name="ApplyCostCondition"></param>
        /// <param name="RTType"></param>
        /// <returns></returns>
        public static DataTable GetRTChannelFLFee(int AccountMonth, int OrganizeCity, int State, int Staff, int RTChannel, string ApplyCostCondition, int RTType)
        {
            FNA_FeeApplyDAL dal = (FNA_FeeApplyDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetRTChannelFLFee(AccountMonth, OrganizeCity, State, Staff, RTChannel, ApplyCostCondition, RTType);
        }
        /// <summary>
        /// 获取返利费用明细
        /// </summary>
        /// <param name="AccountMonth"></param>
        /// <param name="OrganizeCity"></param>
        /// <param name="State"></param>
        /// <param name="Staff"></param>
        /// <param name="RTChannel"></param>
        /// <returns></returns>
        public static DataTable GetRTChannelFLFee(int AccountMonth, int OrganizeCity, int State, int Staff, int RTChannel)
        {
            FNA_FeeApplyDAL dal = (FNA_FeeApplyDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetRTChannelFLFee(AccountMonth, OrganizeCity, State, Staff, RTChannel, "", 0);
        }
        #endregion

        
    }
}