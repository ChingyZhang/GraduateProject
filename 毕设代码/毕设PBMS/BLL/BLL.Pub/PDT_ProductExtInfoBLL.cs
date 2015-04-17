
// ===================================================================
// 文件： PDT_ProductExtInfoDAL.cs
// 项目名称：
// 创建时间：2015/1/27
// 作者:	   lv
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
    ///PDT_ProductExtInfoBLL业务逻辑BLL类
    /// </summary>
    public class PDT_ProductExtInfoBLL : BaseSimpleBLL<PDT_ProductExtInfo>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.Pub.PDT_ProductExtInfoDAL";
        private PDT_ProductExtInfoDAL _dal;

        #region 构造函数
        ///<summary>
        ///PDT_ProductExtInfoBLL
        ///</summary>
        public PDT_ProductExtInfoBLL()
            : base(DALClassName)
        {
            _dal = (PDT_ProductExtInfoDAL)_DAL;
            _m = new PDT_ProductExtInfo();
        }

        public PDT_ProductExtInfoBLL(int id)
            : base(DALClassName)
        {
            _dal = (PDT_ProductExtInfoDAL)_DAL;
            FillModel(id);
        }

        public PDT_ProductExtInfoBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (PDT_ProductExtInfoDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<PDT_ProductExtInfo> GetModelList(string condition)
        {
            return new PDT_ProductExtInfoBLL()._GetModelList(condition);
        }
        #endregion

        public static IList<PDT_ProductExtInfo> GetProductExtInfoList_BySupplier(int Supplier)
        {
            return GetModelList("Supplier=" + Supplier.ToString() + " AND SalesState=1");
        }

        /// <summary>
        /// 根据产品编码获取产品扩展信息
        /// </summary>
        /// <param name="Supplier"></param>
        /// <param name="Code"></param>
        /// <returns></returns>
        public static PDT_ProductExtInfo GetProductExtInfo_ByCode(int Supplier, string Code)
        {
            IList<PDT_ProductExtInfo> list = GetModelList("Supplier=" + Supplier.ToString() + " AND Code='" + Code + "'" + " AND SalesState=1");
            if (list.Count > 0)
                return list[0];
            else
                return null;
        }

        public static DataTable GetByClient(int OwnerClient, int State, int Category, string ExtCondition)
        {
            PDT_ProductExtInfoDAL dal = (PDT_ProductExtInfoDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetByClient(OwnerClient, State, Category, ExtCondition);
        }
        public static DataTable GetByClient(int OwnerClient, string ExtCondition)
        {
            return GetByClient(OwnerClient, 1, 0, ExtCondition);
        }
    }
}