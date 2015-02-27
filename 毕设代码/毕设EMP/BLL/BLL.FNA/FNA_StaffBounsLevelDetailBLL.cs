
// ===================================================================
// 文件： FNA_StaffBounsLevelDetailBLL.cs
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
    ///FNA_StaffBounsLevelDetailBLL业务逻辑BLL类
    /// </summary>
    public class FNA_StaffBounsLevelDetailBLL : BaseSimpleBLL<FNA_StaffBounsLevelDetail>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.FNA.FNA_StaffBounsLevelDetailDAL";
        private FNA_StaffBounsLevelDetailDAL _dal;

        #region 构造函数
        ///<summary>
        ///FNA_StaffBounsLevelDetailBLL
        ///</summary>
        public FNA_StaffBounsLevelDetailBLL()
            : base(DALClassName)
        {
            _dal = (FNA_StaffBounsLevelDetailDAL)_DAL;
            _m = new FNA_StaffBounsLevelDetail();
        }

        public FNA_StaffBounsLevelDetailBLL(int id)
            : base(DALClassName)
        {
            _dal = (FNA_StaffBounsLevelDetailDAL)_DAL;
            FillModel(id);
        }

        public FNA_StaffBounsLevelDetailBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (FNA_StaffBounsLevelDetailDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<FNA_StaffBounsLevelDetail> GetModelList(string condition)
        {
            return new FNA_StaffBounsLevelDetailBLL()._GetModelList(condition);
        }
        #endregion

        public static int Init(int AccountQuarter, int Staff)
        {
            FNA_StaffBounsLevelDetailDAL dal = (FNA_StaffBounsLevelDetailDAL)DataAccess.CreateObject(DALClassName);
           return dal.Init(AccountQuarter, Staff);
        }
    }
}