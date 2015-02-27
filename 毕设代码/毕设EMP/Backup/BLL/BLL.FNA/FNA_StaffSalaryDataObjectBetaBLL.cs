
// ===================================================================
// 文件： FNA_StaffSalaryDataObjectBetaBLL.cs
// 项目名称：
// 创建时间：2014/7/14
// 作者:	   JACE
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
    ///FNA_StaffSalaryDataObjectBetaBLL业务逻辑BLL类
    /// </summary>
    public class FNA_StaffSalaryDataObjectBetaBLL : BaseSimpleBLL<FNA_StaffSalaryDataObjectBeta>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.FNA.FNA_StaffSalaryDataObjectBetaDAL";
        private FNA_StaffSalaryDataObjectBetaDAL _dal;

        #region 构造函数
        ///<summary>
        ///FNA_StaffSalaryDataObjectBetaBLL
        ///</summary>
        public FNA_StaffSalaryDataObjectBetaBLL()
            : base(DALClassName)
        {
            _dal = (FNA_StaffSalaryDataObjectBetaDAL)_DAL;
            _m = new FNA_StaffSalaryDataObjectBeta();
        }

        public FNA_StaffSalaryDataObjectBetaBLL(int id)
            : base(DALClassName)
        {
            _dal = (FNA_StaffSalaryDataObjectBetaDAL)_DAL;
            FillModel(id);
        }

        public FNA_StaffSalaryDataObjectBetaBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (FNA_StaffSalaryDataObjectBetaDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<FNA_StaffSalaryDataObjectBeta> GetModelList(string condition)
        {
            return new FNA_StaffSalaryDataObjectBetaBLL()._GetModelList(condition);
        }
        #endregion

        public static int SubmitFlag(int AccountMonth, int OrganizeCity, int PositionType)
        {
            FNA_StaffSalaryDataObjectBetaDAL dal = (FNA_StaffSalaryDataObjectBetaDAL)DataAccess.CreateObject(DALClassName);
            return dal.SubmitFlag(AccountMonth, OrganizeCity, PositionType);
        }
        public static int ApproveFlag(int AccountMonth, int OrganizeCity, int PositionType)
        {
            FNA_StaffSalaryDataObjectBetaDAL dal = (FNA_StaffSalaryDataObjectBetaDAL)DataAccess.CreateObject(DALClassName);
            return dal.ApproveFlag(AccountMonth, OrganizeCity, PositionType);
        }
        public static int UnApproveFlag(int AccountMonth, int OrganizeCity, int PositionType)
        {
            FNA_StaffSalaryDataObjectBetaDAL dal = (FNA_StaffSalaryDataObjectBetaDAL)DataAccess.CreateObject(DALClassName);
            return dal.UnApproveFlag(AccountMonth, OrganizeCity, PositionType);
        }
        public static int Approve(int AccountMonth, int OrganizeCity, int PositionType, int ApproveFlag)
        {
            FNA_StaffSalaryDataObjectBetaDAL dal = (FNA_StaffSalaryDataObjectBetaDAL)DataAccess.CreateObject(DALClassName);
            return dal.Approve(AccountMonth, OrganizeCity, PositionType, ApproveFlag);
        }
        public int Approve(int ApproveFlag)
        {
            FNA_StaffSalaryDataObjectBetaDAL dal = (FNA_StaffSalaryDataObjectBetaDAL)DataAccess.CreateObject(DALClassName);
            return dal.SigleApprove(this.Model.ID,ApproveFlag);
        }
        public static int Adjust(int AccountMonth, decimal AdjustRate, int ClientManager)
        {
            FNA_StaffSalaryDataObjectBetaDAL dal = (FNA_StaffSalaryDataObjectBetaDAL)DataAccess.CreateObject(DALClassName);
            return dal.Adjust(AccountMonth, AdjustRate, ClientManager);
        }
    }
}