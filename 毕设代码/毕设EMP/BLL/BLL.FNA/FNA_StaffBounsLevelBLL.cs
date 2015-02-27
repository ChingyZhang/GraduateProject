
// ===================================================================
// 文件： FNA_StaffBounsLevelBLL.cs
// 项目名称：
// 创建时间：2013-08-02
// 作者:	   Jace
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
    ///FNA_StaffBounsLevelBLL业务逻辑BLL类
    /// </summary>
    public class FNA_StaffBounsLevelBLL : BaseSimpleBLL<FNA_StaffBounsLevel>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.FNA.FNA_StaffBounsLevelDAL";
        private FNA_StaffBounsLevelDAL _dal;

        #region 构造函数
        ///<summary>
        ///FNA_StaffBounsLevelBLL
        ///</summary>
        public FNA_StaffBounsLevelBLL()
            : base(DALClassName)
        {
            _dal = (FNA_StaffBounsLevelDAL)_DAL;
            _m = new FNA_StaffBounsLevel();
        }

        public FNA_StaffBounsLevelBLL(int id)
            : base(DALClassName)
        {
            _dal = (FNA_StaffBounsLevelDAL)_DAL;
            FillModel(id);
        }

        public FNA_StaffBounsLevelBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (FNA_StaffBounsLevelDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<FNA_StaffBounsLevel> GetModelList(string condition)
        {
            return new FNA_StaffBounsLevelBLL()._GetModelList(condition);
        }
        #endregion

        public static DataTable GetData(int quarter)
        {
            FNA_StaffBounsLevelDAL dal = (FNA_StaffBounsLevelDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetData(quarter);
        }
        public static int ChageApproveState(int quarter, int approveflag)
        {
            FNA_StaffBounsLevelDAL dal = (FNA_StaffBounsLevelDAL)DataAccess.CreateObject(DALClassName);
            return dal.ChageApproveState(quarter, approveflag);
        }
        public static int GetApproveState(int quarter)
        {
            FNA_StaffBounsLevelDAL dal = (FNA_StaffBounsLevelDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetApproveState(quarter);
        }
    }
}