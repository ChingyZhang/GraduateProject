
// ===================================================================
// 文件： PM_PromotorNumberLimitDAL.cs
// 项目名称：
// 创建时间：2010/5/6
// 作者:	   
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model.Promotor;
using MCSFramework.SQLDAL.Promotor;

namespace MCSFramework.BLL.Promotor
{
    /// <summary>
    ///PM_PromotorNumberLimitBLL业务逻辑BLL类
    /// </summary>
    public class PM_PromotorNumberLimitBLL : BaseSimpleBLL<PM_PromotorNumberLimit>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.Promotor.PM_PromotorNumberLimitDAL";
        private PM_PromotorNumberLimitDAL _dal;

        #region 构造函数
        ///<summary>
        ///PM_PromotorNumberLimitBLL
        ///</summary>
        public PM_PromotorNumberLimitBLL()
            : base(DALClassName)
        {
            _dal = (PM_PromotorNumberLimitDAL)_DAL;
            _m = new PM_PromotorNumberLimit();
        }

        public PM_PromotorNumberLimitBLL(int id)
            : base(DALClassName)
        {
            _dal = (PM_PromotorNumberLimitDAL)_DAL;
            FillModel(id);
        }

        public PM_PromotorNumberLimitBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (PM_PromotorNumberLimitDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<PM_PromotorNumberLimit> GetModelList(string condition)
        {
            return new PM_PromotorNumberLimitBLL()._GetModelList(condition);
        }
        #endregion

        public static int Init(int OrganizeCity, int Level, int Classify, int InsertStaff)
        {
            PM_PromotorNumberLimitDAL dal = (PM_PromotorNumberLimitDAL)DataAccess.CreateObject(DALClassName);
            return dal.Init(OrganizeCity, Level, Classify, InsertStaff);
        }

        public static IList<PM_PromotorNumberLimit> GetByOrganizeCity(int OrganizeCity, int Classify)
        {
            PM_PromotorNumberLimitDAL dal = (PM_PromotorNumberLimitDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetByOrganizeCity(OrganizeCity, Classify);
        }

        public static int GetActualNumber(int OrganizeCity, int Classify)
        {
            PM_PromotorNumberLimitDAL dal = (PM_PromotorNumberLimitDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetActualNumber(OrganizeCity, Classify);
        }

        /// <summary>
        /// 判断两个城市及类别是否同属于一条限制记录
        /// </summary>
        /// <param name="OrganizeCity1"></param>
        /// <param name="OrganizeCity2"></param>
        /// <returns>true:同属于一个限制记录</returns>
        public static bool IsSameLimit(int OrganizeCity1, int OrganizeCity2, int Classify1, int Classify2)
        {
            PM_PromotorNumberLimitDAL dal = (PM_PromotorNumberLimitDAL)DataAccess.CreateObject(DALClassName);
            return dal.IsSameLimit(OrganizeCity1, OrganizeCity2, Classify1, Classify2);
        }

        /// <summary>
        /// 判断该城市限制了录入促销员的数量
        /// </summary>
        /// <param name="OrganizeCity"></param>
        /// <returns>未限制返回100,否则返回还可增加人数</returns>
        public static int CheckAllowAdd(int OrganizeCity, int Classify)
        {
            PM_PromotorNumberLimitDAL dal = (PM_PromotorNumberLimitDAL)DataAccess.CreateObject(DALClassName);
            return dal.CheckAllowAdd(OrganizeCity, Classify);
        }

        /// <summary>
        /// 判断该城市是否设定了促销员的预算人数
        /// </summary>
        /// <param name="OrganizeCity"></param>
        /// <param name="Classify"></param>
        /// <returns>未限制返回100,否则返回预算内还可增加人数</returns>
        public static int CheckOverBudget(int OrganizeCity, int Classify)
        {
            PM_PromotorNumberLimitDAL dal = (PM_PromotorNumberLimitDAL)DataAccess.CreateObject(DALClassName);
            return dal.CheckOverBudget(OrganizeCity, Classify);
        }

    }
}