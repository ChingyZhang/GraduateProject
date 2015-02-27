
// ===================================================================
// 文件： CM_ContractDAL.cs
// 项目名称：
// 创建时间：2011/3/9
// 作者:	   chf
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
    ///CM_ContractBLL业务逻辑BLL类
    /// </summary>
    public class CM_ContractBLL : BaseComplexBLL<CM_Contract, CM_ContractDetail>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.CM.CM_ContractDAL";
        private CM_ContractDAL _dal;

        #region 构造函数
        ///<summary>
        ///CM_ContractBLL
        ///</summary>
        public CM_ContractBLL()
            : base(DALClassName)
        {
            _dal = (CM_ContractDAL)_DAL;
            _m = new CM_Contract();
        }

        public CM_ContractBLL(int id)
            : base(DALClassName)
        {
            _dal = (CM_ContractDAL)_DAL;
            FillModel(id);
        }

        public CM_ContractBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (CM_ContractDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<CM_Contract> GetModelList(string condition)
        {
            return new CM_ContractBLL()._GetModelList(condition);
        }
        #endregion

        public int Approve(int State, int ApproveStaff)
        {
            return _dal.Approve(_m.ID, State, ApproveStaff);
        }
        public int Disable(int Staff)
        {
            return _dal.Disable(_m.ID, Staff);
        }
        public static DataTable GetOwners(int ClientID)
        {
            CM_ContractDAL dal = (CM_ContractDAL)DataAccess.CreateObject(DALClassName);
            return Tools.ConvertDataReaderToDataTable(dal.GetOwners(ClientID));
        }

        public static DataTable GetPartyC(int ClientID)
        {
            CM_ContractDAL dal = (CM_ContractDAL)DataAccess.CreateObject(DALClassName);
            return Tools.ConvertDataReaderToDataTable(dal.GetPartyC(ClientID));
        }

        /// <summary>
        /// 将合同陈列协议批量生成费用申请单
        /// </summary>
        /// <param name="OrganizeCity"></param>
        /// <param name="AccountMonth"></param>
        /// <param name="DIClient"></param>
        /// <param name="Staff"></param>
        /// <param name="IsNKA"></param>
        /// <returns></returns>
        public static int CreateCLFeeApply(int OrganizeCity, int AccountMonth, int DIClient, int Staff, bool IsNKA, int FeeType)
        {
            CM_ContractDAL dal = (CM_ContractDAL)DataAccess.CreateObject(DALClassName);
            return dal.CreateCLFeeApply(OrganizeCity, AccountMonth, DIClient, Staff, IsNKA, FeeType);
        }

        /// <summary>
        /// 批量申请门店返利费用
        /// </summary>
        /// <param name="OrganizeCity"></param>
        /// <param name="AccountMonth"></param>
        /// <param name="DIClient"></param>
        /// <param name="Staff"></param>
        /// <param name="FeeType"></param>
        /// <returns></returns>
        public static int CreateFLFeeApply(int OrganizeCity, int AccountMonth, int DIClient, int Staff, int FeeType)
        {
            CM_ContractDAL dal = (CM_ContractDAL)DataAccess.CreateObject(DALClassName);
            return dal.CreateFLFeeApply(OrganizeCity, AccountMonth, DIClient, Staff, FeeType);
        }

        /// <summary>
        /// 批量申请导购管理费用
        /// </summary>
        /// <param name="OrganizeCity"></param>
        /// <param name="AccountMonth"></param>
        /// <param name="DIClient"></param>
        /// <param name="Staff"></param>
        /// <param name="FeeType"></param>
        /// <returns></returns>
        public static int CreatePMFeeApply(int OrganizeCity, int AccountMonth, int DIClient, int Staff, int FeeType)
        {
            CM_ContractDAL dal = (CM_ContractDAL)DataAccess.CreateObject(DALClassName);
            return dal.CreatePMFeeApply(OrganizeCity, AccountMonth, DIClient, Staff, FeeType);
        }
        /// <summary>
        /// 获取批量审批数据
        /// </summary>
        /// <param name="OrganizeCity">片区</param>
        /// <param name="State">状态：0所有 1待我审批 2我已审批</param>
        /// <param name="Staff">审批人</param>
        /// <param name="Classify">合同类型</param>
        /// <returns></returns>
        public static DataTable GetApproveSummary(int OrganizeCity, int State, int Staff, int Classify,int PayMode,string ExtCondition)
        {
            CM_ContractDAL dal = (CM_ContractDAL)DataAccess.CreateObject(DALClassName);
            return Tools.ConvertDataReaderToDataTable(dal.GetApproveSummary(OrganizeCity, State, Staff, Classify, PayMode, ExtCondition));
        }
        /// <summary>
        /// 获取导购合同申请的最大月份 指定当前合同
        /// </summary>
        /// <returns>最大月份</returns>
        public int CheckPMFeeApplyLastMonth()
        {
            return CheckPMFeeApplyLastMonth(0);
        }
        /// <summary>
        /// 获取导购合同申请的最大月份 指定门店不指定合同
        /// </summary>
        /// <param name="Client">门店ID</param>
        /// <returns>最大月份</returns>
        public int CheckPMFeeApplyLastMonth(int Client)
        {
            CM_ContractDAL dal = (CM_ContractDAL)DataAccess.CreateObject(DALClassName);
            return dal.CheckPMFeeApplyLastMonth(this.Model.ID, Client);
        }

        /// <summary>
        /// 获取导购协议汇总
        /// </summary>
        /// <param name="OrganizeCity">管理片区</param>
        /// <param name="Level">等级</param>
        /// <param name="State">状态</param>
        /// <param name="BeginDate">开始日期</param>
        /// <param name="EndDate">结束日期</param>
        /// <param name="StaffID">员工ID</param>
        /// <param name="RTChannel">门店渠道</param>
        /// <returns></returns>
        public static DataTable GetPMSummary(int OrganizeCity, int Level, int State, int StaffID, int RTChannel)
        {
            CM_ContractDAL dal = (CM_ContractDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetPMSummary(OrganizeCity, Level, State, StaffID, RTChannel);
        }
        /// <summary>
        /// 获取导购协议列表
        /// </summary>
        /// <param name="OrganizeCity">管理片区</param>
        /// <param name="Level">等级</param>
        /// <param name="State">状态</param>
        /// <param name="BeginDate">开始日期</param>
        /// <param name="EndDate">结束日期</param>
        /// <param name="StaffID">员工ID</param>
        /// <param name="RTChannel">门店渠道</param>
        /// <returns></returns>
        public static DataTable GetPMList(int OrganizeCity, int Level, int State, int StaffID, int RTChannel)
        {
            CM_ContractDAL dal = (CM_ContractDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetPMList(OrganizeCity, Level, State, StaffID, RTChannel);
        }
        /// <summary>
        /// 获取导购协议列表详细信息
        /// </summary>
        /// <param name="OrganizeCity">管理片区</param>
        /// <param name="Level">等级</param>
        /// <param name="State">状态</param>
        /// <param name="BeginDate">开始日期</param>
        /// <param name="EndDate">结束日期</param>
        /// <param name="StaffID">员工ID</param>
        /// <param name="RTChannel">门店渠道</param>
        /// <returns></returns>
        public static DataTable GetPMListDetail(int OrganizeCity, int Level, int State, int StaffID, int RTChannel)
        {
            CM_ContractDAL dal = (CM_ContractDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetPMListDetail(OrganizeCity, Level, State,  StaffID, RTChannel);
        }
    }
}