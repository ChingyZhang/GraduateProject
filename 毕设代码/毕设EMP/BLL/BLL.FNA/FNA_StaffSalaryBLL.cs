
// ===================================================================
// 文件： FNA_StaffSalaryDAL.cs
// 项目名称：
// 创建时间：2009/3/22
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
using MCSFramework.BLL.Pub;
using MCSFramework.Model.Pub;
using MCSFramework.Model;


namespace MCSFramework.BLL.FNA
{
    /// <summary>
    ///FNA_StaffSalaryBLL业务逻辑BLL类
    /// </summary>
    public class FNA_StaffSalaryBLL : BaseComplexBLL<FNA_StaffSalary, FNA_StaffSalaryDetail>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.FNA.FNA_StaffSalaryDAL";
        private FNA_StaffSalaryDAL _dal;

        #region 构造函数
        ///<summary>
        ///FNA_StaffSalaryBLL
        ///</summary>
        public FNA_StaffSalaryBLL()
            : base(DALClassName)
        {
            _dal = (FNA_StaffSalaryDAL)_DAL;
            _m = new FNA_StaffSalary();
        }

        public FNA_StaffSalaryBLL(int id)
            : base(DALClassName)
        {
            _dal = (FNA_StaffSalaryDAL)_DAL;
            FillModel(id);
        }

        public FNA_StaffSalaryBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (FNA_StaffSalaryDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<FNA_StaffSalary> GetModelList(string condition)
        {
            return new FNA_StaffSalaryBLL()._GetModelList(condition);
        }
        #endregion

        /// <summary>
        /// 生成指定管理片区指定月份的员工工资
        /// </summary>
        /// <param name="OrganizeCity"></param>
        /// <param name="AccountMonth"></param>
        /// <param name="Staff"></param>
        /// <returns></returns>
        public static int GenerateStaffSalary(int AccountMonth, int PositionType)
        {

            FNA_StaffSalaryDAL dal = (FNA_StaffSalaryDAL)DataAccess.CreateObject(DALClassName);
            return dal.Generate(AccountMonth, PositionType);
        }

        public int Submit(int staff, int taskid, int feetype)
        {
            return _dal.Submit(_m.ID, staff, taskid, feetype);
        }

        public decimal GetSumSalary()
        {
            return _dal.GetSumSalary(_m.ID);
        }

        /// <summary>
        /// 获取促销员工资申请单的总金额
        /// </summary>
        /// <param name="detailid"></param>
        /// <returns></returns>
        public static decimal GetSumSalary(int ID)
        {
            FNA_StaffSalaryDAL dal = (FNA_StaffSalaryDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetSumSalary(ID);
        }

        public static void UpdateAdjustRecord(int ID, int Staff, string OldAdjustCost, string AdjustCost, string staffName)
        {
            FNA_StaffSalaryDAL dal = (FNA_StaffSalaryDAL)DataAccess.CreateObject(DALClassName);
            dal.UpdateAdjustRecord(ID, Staff, OldAdjustCost, AdjustCost, staffName);
        }

        /// <summary>
        /// 计算个人所得税
        /// </summary>
        /// <param name="Income"></param>
        /// <returns></returns>
        public static decimal ComputeIncomeTax(decimal Income)
        {
            FNA_StaffSalaryDAL dal = (FNA_StaffSalaryDAL)DataAccess.CreateObject(DALClassName);
            return dal.ComputeIncomeTax(Income);
        }

        public int UpdateDetail(FNA_StaffSalaryDetail m)
        {
            FNA_StaffSalaryDAL dal = (FNA_StaffSalaryDAL)DataAccess.CreateObject(DALClassName);
            return dal.UpdateDetail(m);
        }

        /// <summary>
        /// 获取员工工资详细
        /// </summary>
        /// <param name="Condition">存储过程筛选条件</param>
        /// <param name="StaffType">员工类型（1：办事处主任；2：业务代表）</param>
        /// <returns></returns>
        public DataTable GetStaffSalaryDetail(string Condition, int StaffType)
        {
            FNA_StaffSalaryDAL dal = (FNA_StaffSalaryDAL)DataAccess.CreateObject(DALClassName);
            DataTable dt = Tools.ConvertDataReaderToDataTable(dal.GetStaffSalaryDetail(Condition, StaffType));
            dt.Columns.Remove("StaffID");
            dt.Columns.Remove("SalaryID");
            dt.Columns.Remove("StaffOrganizeCity");
            return dt;
        }
    }
}