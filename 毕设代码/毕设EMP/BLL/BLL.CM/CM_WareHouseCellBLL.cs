
// ===================================================================
// 文件： CM_WareHouseCellDAL.cs
// 项目名称：
// 创建时间：2012-7-21
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model.CM;
using MCSFramework.SQLDAL.CM;

namespace MCSFramework.BLL.CM
{
    /// <summary>
    ///CM_WareHouseCellBLL业务逻辑BLL类
    /// </summary>
    public class CM_WareHouseCellBLL : BaseSimpleBLL<CM_WareHouseCell>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.CM.CM_WareHouseCellDAL";
        private CM_WareHouseCellDAL _dal;

        #region 构造函数
        ///<summary>
        ///CM_WareHouseCellBLL
        ///</summary>
        public CM_WareHouseCellBLL()
            : base(DALClassName)
        {
            _dal = (CM_WareHouseCellDAL)_DAL;
            _m = new CM_WareHouseCell();
        }

        public CM_WareHouseCellBLL(int id)
            : base(DALClassName)
        {
            _dal = (CM_WareHouseCellDAL)_DAL;
            FillModel(id);
        }

        public CM_WareHouseCellBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (CM_WareHouseCellDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<CM_WareHouseCell> GetModelList(string condition)
        {
            return new CM_WareHouseCellBLL()._GetModelList(condition);
        }
        #endregion

        /// <summary>
        /// 获取指定仓库的库位列表
        /// </summary>
        /// <param name="WareHouse"></param>
        /// <returns></returns>
        public static IList<CM_WareHouseCell> GetByWareHouse(int WareHouse)
        {
            return GetModelList("WareHouse=" + WareHouse.ToString());
        }

        public static IList<CM_WareHouseCell> GetEnabledByWareHouse(int WareHouse)
        {
            return GetModelList("WareHouse=" + WareHouse.ToString() + " AND ActiveState = 1");
        }
    }
}