
// ===================================================================
// 文件： ORD_OrderApplyDAL.cs
// 项目名称：
// 创建时间：2009/3/2
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model.Logistics;
using MCSFramework.SQLDAL.Logistics;

namespace MCSFramework.BLL.Logistics
{
    /// <summary>
    ///ORD_OrderApplyBLL业务逻辑BLL类
    /// </summary>
    public class ORD_OrderApplyBLL : BaseComplexBLL<ORD_OrderApply, ORD_OrderApplyDetail>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.Logistics.ORD_OrderApplyDAL";
        private ORD_OrderApplyDAL _dal;

        #region 构造函数
        ///<summary>
        ///ORD_OrderApplyBLL
        ///</summary>
        public ORD_OrderApplyBLL()
            : base(DALClassName)
        {
            _dal = (ORD_OrderApplyDAL)_DAL;
            _m = new ORD_OrderApply();
        }

        public ORD_OrderApplyBLL(int id)
            : base(DALClassName)
        {
            _dal = (ORD_OrderApplyDAL)_DAL;
            FillModel(id);
        }

        public ORD_OrderApplyBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (ORD_OrderApplyDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<ORD_OrderApply> GetModelList(string condition)
        {
            return new ORD_OrderApplyBLL()._GetModelList(condition);
        }
        #endregion

        /// <summary>
        /// 提交定单请购申请
        /// </summary>
        /// <param name="id"></param>
        /// <param name="staff"></param>
        /// <returns></returns>
        public int Submit(int staff, int taskid)
        {
            return _dal.Submit(_m.ID, staff, taskid);
        }

        /// <summary>
        /// 取消申请单并返还未发放的费用
        /// </summary>
        /// <param name="ID"></param>
        public void Finish(int staff)
        {
            ORD_OrderApplyDAL dal = (ORD_OrderApplyDAL)DataAccess.CreateObject(DALClassName);
            dal.Finish(_m.ID, staff);
        }

        /// <summary>
        /// 生成定单请购申请单号 格式：ODSQ+管理单元编号+'-'+申请日期+'-'+4位流水号
        /// </summary>
        /// <param name="organizecity"></param>
        /// <returns></returns>
        public static string GenerateSheetCode(int organizecity, int accountmonth)
        {
            ORD_OrderApplyDAL dal = (ORD_OrderApplyDAL)DataAccess.CreateObject(DALClassName);
            return dal.GenerateSheetCode(organizecity, accountmonth);
        }

        /// <summary>
        /// 根据定单请购申请明细的获取定单请购申请单号
        /// </summary>
        /// <param name="detailid"></param>
        /// <returns></returns>
        public static string GetSheetCodeByDetailID(int DetailID)
        {
            ORD_OrderApplyDAL dal = (ORD_OrderApplyDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetSheetCodeByDetailID(DetailID);
        }

        /// <summary>
        /// 获取定单请购申请单的总金额（含调整）
        /// </summary>
        /// <param name="detailid"></param>
        /// <returns></returns>
        public static decimal GetSumCost(int ID)
        {
            ORD_OrderApplyDAL dal = (ORD_OrderApplyDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetSumCost(ID);
        }


        /// <summary>
        /// 获取指定发布单已提交的请购产品数量
        /// </summary>
        /// <param name="PublishID"></param>
        /// <param name="Product"></param>
        /// <returns></returns>
        public static int GetSubmitQuantity(int PublishID, int Product)
        {
            ORD_OrderApplyDAL dal = (ORD_OrderApplyDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetSubmitQuantity(PublishID, Product);
        }

        /// <summary>
        /// 获取成品订单汇总
        /// </summary>
        /// <param name="AccountMonth"></param>
        /// <param name="OrganizeCity"></param>
        /// <param name="Level"></param>
        /// <param name="ProductType"></param>
        /// <param name="Brand"></param>
        /// <param name="Classify"></param>
        /// <param name="Product"></param>
        /// <param name="State"></param>
        /// <returns></returns>
        public static DataTable GetSummaryTotal(int AccountMonth, int OrganizeCity, int Level, int ProductType, int Brand, int Classify, int Product, int State)
        {
            ORD_OrderApplyDAL dal = (ORD_OrderApplyDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetSummaryTotal(AccountMonth, OrganizeCity, Level, ProductType, Brand, Classify, Product, State);
        }

        /// <summary>
        /// 赠品请购申请汇总
        /// </summary>
        /// <param name="AccountMonth"></param>
        /// <param name="OrganizeCity"></param>
        /// <param name="Level"></param>
        /// <param name="State"></param>
        /// <param name="GiftClassify"></param>
        /// <param name="Staff"></param>
        /// <returns></returns>
        public static DataTable GetGiftSummary(int AccountMonth, int OrganizeCity, int Level, int State, int GiftClassify, int Staff, int Brand)
        {
            ORD_OrderApplyDAL dal = (ORD_OrderApplyDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetGiftSummary(AccountMonth, OrganizeCity, Level, State, GiftClassify, Staff, Brand);
        }

        /// <summary>
        /// 赠品申请申请汇总(以经销商为单位,相同赠品合并统计
        /// </summary>
        /// <param name="AccountMonth"></param>
        /// <param name="OrganizeCity"></param>
        /// <param name="State"></param>
        /// <param name="Staff"></param>
        /// <returns></returns>
        public static DataTable GetGiftSummaryTotal(int AccountMonth, int OrganizeCity, int Client, int State, int Staff,int Brand)
        {
            ORD_OrderApplyDAL dal = (ORD_OrderApplyDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetGiftSummaryTotal(AccountMonth, OrganizeCity, Client, State, Staff, Brand);
        }

        public static int CheckClientCanApply(int AccountMonth, int Client, int PublishID)
        {
            ORD_OrderApplyDAL dal = (ORD_OrderApplyDAL)DataAccess.CreateObject(DALClassName);
            return dal.CheckClientCanApply(AccountMonth, Client, PublishID);
        }


        /// <summary>
        /// 根据员工获取负责的产品系列
        /// </summary>
        /// <param name="Staff">员工ID</param>
        /// <returns></returns>
        public static string GetBrandsByStaff(int Staff)
        {
            ORD_OrderApplyDAL dal = (ORD_OrderApplyDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetBrandsByStaff(Staff);
        }
        public static DataTable GetGiftDetail(int AccountMonth, int OrganizeCity, int Client, int State, int Staff, int Brand, int GiftClassify)
        {
            ORD_OrderApplyDAL dal = (ORD_OrderApplyDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetGiftDetail(AccountMonth, OrganizeCity, Client, State, Staff, Brand, GiftClassify);
        }

        public static DataTable GetRPTOrderApplyPayTrack(int AccountMonth, int Client, int Classify, int OrganizeCity)
        {
            ORD_OrderApplyDAL dal = (ORD_OrderApplyDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetRPTOrderApplyPayTrack(AccountMonth, Client, Classify, OrganizeCity);
        }
        public static DataTable GetRPTOrderCurProcess(int OrganizeCity, int Client, DateTime BeginDate, DateTime EndDate, string SheetCode)
        {
             ORD_OrderApplyDAL dal = (ORD_OrderApplyDAL)DataAccess.CreateObject(DALClassName);
            DataTable dt_process = dal.GetRPTOrderCurProcess(OrganizeCity, Client, BeginDate, EndDate, SheetCode);
            dt_process.Columns.Add("No");
            for(int i=0;i<dt_process.Rows.Count;i++)
                dt_process.Rows[i]["No"] = i+1;
            return dt_process;
        }

        public static DataTable GetRPTOrderList(int OrganizeCity, int Client, DateTime BeginDate, DateTime EndDate, string SheetCode)
        {
            ORD_OrderApplyDAL dal = (ORD_OrderApplyDAL)DataAccess.CreateObject(DALClassName);
            DataTable dt_process = dal.GetRPTOrderList(OrganizeCity, Client, BeginDate, EndDate, SheetCode);
            dt_process.Columns.Add("No");
            for (int i = 0; i < dt_process.Rows.Count; i++)
                dt_process.Rows[i]["No"] = i + 1;
            return dt_process;
        }
        public static DataTable GetRPTOrderDetail(string SheetCode)
        {
            ORD_OrderApplyDAL dal = (ORD_OrderApplyDAL)DataAccess.CreateObject(DALClassName);
            DataTable dt_process = dal.GetRPTOrderDetail(SheetCode);
            dt_process.Columns.Add("No");
            for (int i = 0; i < dt_process.Rows.Count; i++)
                dt_process.Rows[i]["No"] = i + 1;
            return dt_process;
        }
    }
}