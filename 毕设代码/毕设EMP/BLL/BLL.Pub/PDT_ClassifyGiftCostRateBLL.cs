
// ===================================================================
// 文件： PDT_ClassifyGiftCostRateDAL.cs
// 项目名称：
// 创建时间：2012-08-07
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
    ///PDT_ClassifyGiftCostRateBLL业务逻辑BLL类
    /// </summary>
    public class PDT_ClassifyGiftCostRateBLL : BaseSimpleBLL<PDT_ClassifyGiftCostRate>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.Pub.PDT_ClassifyGiftCostRateDAL";
        private PDT_ClassifyGiftCostRateDAL _dal;

        #region 构造函数
        ///<summary>
        ///PDT_ClassifyGiftCostRateBLL
        ///</summary>
        public PDT_ClassifyGiftCostRateBLL()
            : base(DALClassName)
        {
            _dal = (PDT_ClassifyGiftCostRateDAL)_DAL;
            _m = new PDT_ClassifyGiftCostRate();
        }

        public PDT_ClassifyGiftCostRateBLL(int id)
            : base(DALClassName)
        {
            _dal = (PDT_ClassifyGiftCostRateDAL)_DAL;
            FillModel(id);
        }

        public PDT_ClassifyGiftCostRateBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (PDT_ClassifyGiftCostRateDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<PDT_ClassifyGiftCostRate> GetModelList(string condition)
        {
            return new PDT_ClassifyGiftCostRateBLL()._GetModelList(condition);
        }
        #endregion
    }
}