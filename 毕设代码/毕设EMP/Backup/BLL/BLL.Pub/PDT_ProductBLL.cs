
// ===================================================================
// 文件： PDT_ProductDAL.cs
// 项目名称：
// 创建时间：2008-12-22
// 作者:	   
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model.Pub;
using MCSFramework.SQLDAL.Pub;
using System.Collections.Generic;

namespace MCSFramework.BLL.Pub
{
    /// <summary>
    ///PDT_ProductBLL业务逻辑BLL类
    /// </summary>
    public class PDT_ProductBLL : BaseSimpleBLL<PDT_Product>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.Pub.PDT_ProductDAL";
        private PDT_ProductDAL _dal;

        #region 构造函数
        ///<summary>
        ///PDT_ProductBLL
        ///</summary>
        public PDT_ProductBLL()
            : base(DALClassName)
        {
            _dal = (PDT_ProductDAL)_DAL;
            _m = new PDT_Product();
        }

        public PDT_ProductBLL(int id)
            : base(DALClassName)
        {
            _dal = (PDT_ProductDAL)_DAL;
            FillModel(id);
        }

        public PDT_ProductBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (PDT_ProductDAL)_DAL;
            FillModel(id, bycache);
        }

        public PDT_ProductBLL(string code)
            : base(DALClassName)
        {
            _dal = (PDT_ProductDAL)_DAL;
            _m = _dal.GetModel(code);
        }
        #endregion

        public static IList<PDT_Product> GetModelList(string condition)
        {
            PDT_ProductBLL b = new PDT_ProductBLL();
            return b._GetModelList(condition);
        }
        #region	根据推荐方式获得
        public static IList<PDT_Product> GetProductlListByOrderMode(int ordermode)
        {
            ordermode = ordermode + 5;
            return new PDT_ProductBLL()._GetModelList("MCS_SYS.dbo.UF_Spilt(ExtPropertys,'|'," + ordermode + ")='Y'");
        }
        #endregion
    }
}