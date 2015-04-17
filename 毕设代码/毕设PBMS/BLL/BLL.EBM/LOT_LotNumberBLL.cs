
// ===================================================================
// 文件： LOT_LotNumberDAL.cs
// 项目名称：
// 创建时间：2012-11-11
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model.EBM;
using MCSFramework.SQLDAL.EBM;

namespace MCSFramework.BLL.EBM
{
    /// <summary>
    ///LOT_LotNumberBLL业务逻辑BLL类
    /// </summary>
    public class LOT_LotNumberBLL : BaseComplexBLL<LOT_LotNumber, LOT_MaterialDetail>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.EBM.LOT_LotNumberDAL";
        private LOT_LotNumberDAL _dal;

        #region 构造函数
        ///<summary>
        ///LOT_LotNumberBLL
        ///</summary>
        public LOT_LotNumberBLL()
            : base(DALClassName)
        {
            _dal = (LOT_LotNumberDAL)_DAL;
            _m = new LOT_LotNumber();
        }

        public LOT_LotNumberBLL(int id)
            : base(DALClassName)
        {
            _dal = (LOT_LotNumberDAL)_DAL;
            FillModel(id);
        }

        public LOT_LotNumberBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (LOT_LotNumberDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<LOT_LotNumber> GetModelList(string condition)
        {
            return new LOT_LotNumberBLL()._GetModelList(condition);
        }
        #endregion

        #region 获取某产品的批号信息
        public static IList<LOT_LotNumber> GetListByProduct(int Product)
        {
            LOT_LotNumberDAL dal = (LOT_LotNumberDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetListByProduct(Product);
        }

        public static IList<LOT_LotNumber> GetListByProduct(int Product, DateTime BeginProductionDate, DateTime EndProductionDate)
        {
            LOT_LotNumberDAL dal = (LOT_LotNumberDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetListByProduct(Product, BeginProductionDate, EndProductionDate);
        }
        #endregion
        
    }
}