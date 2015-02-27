
// ===================================================================
// 文件： PM_PromotorSalaryDAL.cs
// 项目名称：
// 创建时间：2011/9/25
// 作者:	   Shen Gang
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
    ///PM_PromotorSalaryBLL业务逻辑BLL类
    /// </summary>
    public class PM_PromotorSalaryBLL : BaseSimpleBLL<PM_PromotorSalary>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.Promotor.PM_PromotorSalaryDAL";
        private PM_PromotorSalaryDAL _dal;

        #region 构造函数
        ///<summary>
        ///PM_PromotorSalaryBLL
        ///</summary>
        public PM_PromotorSalaryBLL()
            : base(DALClassName)
        {
            _dal = (PM_PromotorSalaryDAL)_DAL;
            _m = new PM_PromotorSalary();
        }

        public PM_PromotorSalaryBLL(int id)
            : base(DALClassName)
        {
            _dal = (PM_PromotorSalaryDAL)_DAL;
            FillModel(id);
        }

        public PM_PromotorSalaryBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (PM_PromotorSalaryDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<PM_PromotorSalary> GetModelList(string condition)
        {
            return new PM_PromotorSalaryBLL()._GetModelList(condition);
        }
        #endregion

        public int Approve(int State)
        {
            return _dal.Approve(_m.ID, State);
        }

        public int GetFloatingInfo(int Promotor, int AccountMonth, out decimal AvgSales, out decimal BaseFeeRate)
        {
            PM_PromotorSalaryDAL dal = (PM_PromotorSalaryDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetFloatingInfo(Promotor, AccountMonth, out AvgSales, out BaseFeeRate);
        }
    }
}