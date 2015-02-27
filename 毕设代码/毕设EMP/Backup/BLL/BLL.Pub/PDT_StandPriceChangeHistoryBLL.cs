
// ===================================================================
// 文件： PDT_StandPriceChangeHistoryBLL.cs
// 项目名称：
// 创建时间：2013-10-08
// 作者:	   Jace
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model.Pub;
using MCSFramework.SQLDAL.Pub;

namespace MCSFramework.BLL.Pub
{
    /// <summary>
    ///PDT_StandPriceChangeHistoryBLL业务逻辑BLL类
    /// </summary>
    public class PDT_StandPriceChangeHistoryBLL : BaseSimpleBLL<PDT_StandPriceChangeHistory>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.Pub.PDT_StandPriceChangeHistoryDAL";
        private PDT_StandPriceChangeHistoryDAL _dal;

        #region 构造函数
        ///<summary>
        ///PDT_StandPriceChangeHistoryBLL
        ///</summary>
        public PDT_StandPriceChangeHistoryBLL()
            : base(DALClassName)
        {
            _dal = (PDT_StandPriceChangeHistoryDAL)_DAL;
            _m = new PDT_StandPriceChangeHistory();
        }

        public PDT_StandPriceChangeHistoryBLL(int id)
            : base(DALClassName)
        {
            _dal = (PDT_StandPriceChangeHistoryDAL)_DAL;
            FillModel(id);
        }

        public PDT_StandPriceChangeHistoryBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (PDT_StandPriceChangeHistoryDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<PDT_StandPriceChangeHistory> GetModelList(string condition)
        {
            return new PDT_StandPriceChangeHistoryBLL()._GetModelList(condition);
        }
        #endregion
    }
}