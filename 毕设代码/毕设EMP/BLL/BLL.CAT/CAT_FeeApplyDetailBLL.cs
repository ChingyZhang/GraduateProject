
// ===================================================================
// 文件： CAT_FeeApplyDetailBLL.cs
// 项目名称：
// 创建时间：2012/8/13
// 作者:	   chf
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model.CAT;
using MCSFramework.SQLDAL.CAT;

namespace MCSFramework.BLL.CAT
{
    /// <summary>
    ///CAT_FeeApplyDetailBLL业务逻辑BLL类
    /// </summary>
    public class CAT_FeeApplyDetailBLL : BaseSimpleBLL<CAT_FeeApplyDetail>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.CAT.CAT_FeeApplyDetailDAL";
        private CAT_FeeApplyDetailDAL _dal;

        #region 构造函数
        ///<summary>
        ///CAT_FeeApplyDetailBLL
        ///</summary>
        public CAT_FeeApplyDetailBLL()
            : base(DALClassName)
        {
            _dal = (CAT_FeeApplyDetailDAL)_DAL;
            _m = new CAT_FeeApplyDetail();
        }

        public CAT_FeeApplyDetailBLL(int id)
            : base(DALClassName)
        {
            _dal = (CAT_FeeApplyDetailDAL)_DAL;
            FillModel(id);
        }

        public CAT_FeeApplyDetailBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (CAT_FeeApplyDetailDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<CAT_FeeApplyDetail> GetModelList(string condition)
        {
            return new CAT_FeeApplyDetailBLL()._GetModelList(condition);
        }
        #endregion
    }
}