
// ===================================================================
// 文件： FNA_StaffSalaryDataObjectDAL.cs
// 项目名称：
// 创建时间：2013/4/25
// 作者:	   chf
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
    ///FNA_StaffSalaryDataObjectBLL业务逻辑BLL类
    /// </summary>
    public class FNA_StaffSalaryDataObjectBLL : BaseSimpleBLL<FNA_StaffSalaryDataObject>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.FNA.FNA_StaffSalaryDataObjectDAL";
        private FNA_StaffSalaryDataObjectDAL _dal;

        #region 构造函数
        ///<summary>
        ///FNA_StaffSalaryDataObjectBLL
        ///</summary>
        public FNA_StaffSalaryDataObjectBLL()
            : base(DALClassName)
        {
            _dal = (FNA_StaffSalaryDataObjectDAL)_DAL;
            _m = new FNA_StaffSalaryDataObject();
        }

        public FNA_StaffSalaryDataObjectBLL(int id)
            : base(DALClassName)
        {
            _dal = (FNA_StaffSalaryDataObjectDAL)_DAL;
            FillModel(id);
        }

        public FNA_StaffSalaryDataObjectBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (FNA_StaffSalaryDataObjectDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<FNA_StaffSalaryDataObject> GetModelList(string condition)
        {
            return new FNA_StaffSalaryDataObjectBLL()._GetModelList(condition);
        }
        #endregion


        public static int SubmitFlag(int AccountMonth, int OrganizeCity, int PositionType)
        {
            FNA_StaffSalaryDataObjectDAL dal = (FNA_StaffSalaryDataObjectDAL)DataAccess.CreateObject(DALClassName);
            return dal.SubmitFlag(AccountMonth, OrganizeCity, PositionType);
        }
        public static int ApproveFlag(int AccountMonth, int OrganizeCity, int PositionType)
        {
            FNA_StaffSalaryDataObjectDAL dal = (FNA_StaffSalaryDataObjectDAL)DataAccess.CreateObject(DALClassName);
            return dal.ApproveFlag(AccountMonth, OrganizeCity, PositionType);
        }
        public static int UnApproveFlag(int AccountMonth, int OrganizeCity, int PositionType)
        {
            FNA_StaffSalaryDataObjectDAL dal = (FNA_StaffSalaryDataObjectDAL)DataAccess.CreateObject(DALClassName);
            return dal.UnApproveFlag(AccountMonth, OrganizeCity, PositionType);
        }
        public static int Approve(int AccountMonth, int OrganizeCity, int PositionType, int ApproveFlag)
        {
            FNA_StaffSalaryDataObjectDAL dal = (FNA_StaffSalaryDataObjectDAL)DataAccess.CreateObject(DALClassName);
            return dal.Approve(AccountMonth, OrganizeCity, PositionType, ApproveFlag);
        }
        public static int Adjust(int AccountMonth, decimal AdjustRate, int ClientManager)
        {
            FNA_StaffSalaryDataObjectDAL dal = (FNA_StaffSalaryDataObjectDAL)DataAccess.CreateObject(DALClassName);
            return dal.Adjust(AccountMonth, AdjustRate, ClientManager);
        }
    }
}