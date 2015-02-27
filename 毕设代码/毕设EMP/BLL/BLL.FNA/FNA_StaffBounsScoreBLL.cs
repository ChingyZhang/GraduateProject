
// ===================================================================
// 文件： FNA_StaffBounsScoreBLL.cs
// 项目名称：
// 创建时间：2013-11-13
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
    ///FNA_StaffBounsScoreBLL业务逻辑BLL类
    /// </summary>
    public class FNA_StaffBounsScoreBLL : BaseComplexBLL<FNA_StaffBounsScore, FNA_StaffBounsScore_Detail>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.FNA.FNA_StaffBounsScoreDAL";
        private FNA_StaffBounsScoreDAL _dal;

        #region 构造函数
        ///<summary>
        ///FNA_StaffBounsScoreBLL
        ///</summary>
        public FNA_StaffBounsScoreBLL()
            : base(DALClassName)
        {
            _dal = (FNA_StaffBounsScoreDAL)_DAL;
            _m = new FNA_StaffBounsScore();
        }

        public FNA_StaffBounsScoreBLL(int id)
            : base(DALClassName)
        {
            _dal = (FNA_StaffBounsScoreDAL)_DAL;
            FillModel(id);
        }

        public FNA_StaffBounsScoreBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (FNA_StaffBounsScoreDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<FNA_StaffBounsScore> GetModelList(string condition)
        {
            return new FNA_StaffBounsScoreBLL()._GetModelList(condition);
        }
        #endregion
    }
}