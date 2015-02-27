
// ===================================================================
// 文件： PM_PromotorDAL.cs
// 项目名称：
// 创建时间：2008-12-30
// 作者:	   yangwei
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model;
using MCSFramework.Model.Promotor;
using MCSFramework.SQLDAL.Promotor;
using System.Collections.Generic;

namespace MCSFramework.BLL.Promotor
{
    /// <summary>
    ///PM_PromotorBLL业务逻辑BLL类
    /// </summary>
    public class PM_PromotorBLL : BaseSimpleBLL<PM_Promotor>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.Promotor.PM_PromotorDAL";
        private PM_PromotorDAL _dal;

        #region 构造函数
        ///<summary>
        ///PM_PromotorBLL
        ///</summary>
        public PM_PromotorBLL()
            : base(DALClassName)
        {
            _dal = (PM_PromotorDAL)_DAL;
            _m = new PM_Promotor();
        }

        public PM_PromotorBLL(int id)
            : base(DALClassName)
        {
            _dal = (PM_PromotorDAL)_DAL;
            FillModel(id);
        }

        public PM_PromotorBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (PM_PromotorDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<PM_Promotor> GetModelList(string condition)
        {
            return new PM_PromotorBLL()._GetModelList(condition);
        }
        #endregion

        /// <summary>
        /// 导购入职提交申请
        /// </summary>
        /// <param name="TaskID"></param>
        /// <param name="Staff"></param>
        /// <returns></returns>
        public int Submit(int TaskID, int Staff)
        {
            return _dal.Submit(_m.ID, TaskID, Staff);
        }
        /// <summary>
        /// 根据导购员所在的门店，获取其底薪标准、保底标准、门店管理费
        /// </summary>
        /// <param name="Promotor"></param>
        /// <returns></returns>
        public int GetStdPay(out decimal BasePay, out decimal MinimumWage, out decimal RTManageCost)
        {
            return _dal.GetStdPay(_m.ID, out BasePay, out MinimumWage, out RTManageCost);
        }

        public static DataTable GetClientList(int Promotor)
        {
            PM_PromotorDAL dal = (PM_PromotorDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetClientList(Promotor);
        }

        public static DataTable GetSalaryConsult(int Promotor, int AccountMonth)
        {
            PM_PromotorDAL dal = (PM_PromotorDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetSalaryConsult(Promotor, AccountMonth);
        }
        /// <summary>
        /// 获取待审批入职的导购列表
        /// </summary>
        /// <param name="OrganizeCity"></param>
        /// <param name="AccountMonth"></param>
        /// <param name="App"></param>
        /// <param name="Staff"></param>
        /// <returns></returns>
        public static DataTable GetApproveList(int OrganizeCity, string AppCode, int Staff)
        {
            PM_PromotorDAL dal = (PM_PromotorDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetApproveList(OrganizeCity, AppCode, Staff);
        }

        /// <summary>
        /// 在导购入职等审批流程中，获取指定区域的经营情况供审批参考
        /// </summary>
        /// <param name="OrganizeCity"></param>
        /// <param name="AccountMonth"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public static DataTable GetApproveConsult(int OrganizeCity, int level)
        {
            PM_PromotorDAL dal = (PM_PromotorDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetApproveConsult(OrganizeCity, level);
        }

        /// <summary>
        ///  根据经销商获取促销员资料及门店
        /// </summary>
        /// <param name="Client"></param>
        /// <param name="Month"></param>
        /// <returns></returns>
        public static DataTable GetByDIClient(int OrganizeCity, int DIClient, int Month)
        {
            PM_PromotorDAL dal = (PM_PromotorDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetByDIClient(OrganizeCity, DIClient, Month);
        }

        public static DataTable GetAnalysisOverview(int OrganizeCity, int Month)
        {
            PM_PromotorDAL dal = (PM_PromotorDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetAnalysisOverview(OrganizeCity, Month);
        }
    }
}