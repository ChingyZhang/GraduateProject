
// ===================================================================
// 文件： ORD_GiftApplyAmountDAL.cs
// 项目名称：
// 创建时间：2011/12/12
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
    ///ORD_GiftApplyAmountBLL业务逻辑BLL类
    /// </summary>
    public class ORD_GiftApplyAmountBLL : BaseSimpleBLL<ORD_GiftApplyAmount>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.Logistics.ORD_GiftApplyAmountDAL";
        private ORD_GiftApplyAmountDAL _dal;

        #region 构造函数
        ///<summary>
        ///ORD_GiftApplyAmountBLL
        ///</summary>
        public ORD_GiftApplyAmountBLL()
            : base(DALClassName)
        {
            _dal = (ORD_GiftApplyAmountDAL)_DAL;
            _m = new ORD_GiftApplyAmount();
        }

        public ORD_GiftApplyAmountBLL(int id)
            : base(DALClassName)
        {
            _dal = (ORD_GiftApplyAmountDAL)_DAL;
            FillModel(id);
        }

        public ORD_GiftApplyAmountBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (ORD_GiftApplyAmountDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<ORD_GiftApplyAmount> GetModelList(string condition)
        {
            return new ORD_GiftApplyAmountBLL()._GetModelList(condition);
        }
        #endregion

        /// <summary>
        /// 更新赠品可请购余额
        /// </summary>
        /// <param name="AccountMonth"></param>
        /// <param name="Client"></param>
        /// <param name="Brand"></param>
        /// <param name="Classify"></param>
        /// <param name="ChangeAmount"></param>
        /// <param name="Staff"></param>
        /// <returns></returns>
        public static int ChangeBalanceAmount(int AccountMonth, int Client, int Brand, int Classify, decimal ChangeAmount, int Staff)
        {
            ORD_GiftApplyAmountDAL dal = (ORD_GiftApplyAmountDAL)DataAccess.CreateObject(DALClassName);
            return dal.ChangeBalanceAmount(AccountMonth, Client, Brand, Classify, ChangeAmount, Staff);
        }

        /// <summary>
        /// 获取指定客户赠品可请购余额
        /// </summary>
        /// <param name="AccountMonth"></param>
        /// <param name="Client"></param>
        /// <param name="Brand"></param>
        /// <param name="Classify"></param>
        /// <returns></returns>
        public static decimal GetBalanceAmount(int AccountMonth, int Client, int Brand, int Classify)
        {
            ORD_GiftApplyAmountDAL dal = (ORD_GiftApplyAmountDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetBalanceAmount(AccountMonth, Client, Brand, Classify);
        }

        #region 计算各经销商下月可请购赠品金额
        /// <summary>
        /// 计算各经销商下月可请购赠品金额
        /// </summary>
        /// <param name="AccountMonth"></param>
        /// <param name="Client"></param>
        /// <returns></returns>
        public static int ComputAvailableAmount(int AccountMonth, int Client)
        {
            ORD_GiftApplyAmountDAL dal = (ORD_GiftApplyAmountDAL)DataAccess.CreateObject(DALClassName);
            return dal.ComputAvailableAmount(AccountMonth, Client);
        }
        /// <summary>
        /// 计算各经销商下月可请购赠品金额
        /// </summary>
        /// <returns></returns>
        public static int ComputAvailableAmount()
        {
            return ComputAvailableAmount(0, 0);
        }
        #endregion

        public static DataTable GetUsedInfo(int AccountMonth, int OrganizeCity)
        {
            ORD_GiftApplyAmountDAL dal = (ORD_GiftApplyAmountDAL)DataAccess.CreateObject(DALClassName);
            return Tools.ConvertDataReaderToDataTable(dal.GetUsedInfo(AccountMonth, OrganizeCity));
        }

        public DataTable GetChangeHistory()
        {
            ORD_GiftApplyAmountDAL dal = (ORD_GiftApplyAmountDAL)DataAccess.CreateObject(DALClassName);
            return Tools.ConvertDataReaderToDataTable(dal.GetChangeHistory(this.Model.AccountMonth, this.Model.Client, this.Model.Brand, this.Model.Classify));
        }

    }
}