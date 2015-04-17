
// ===================================================================
// 文件： CM_VehicleDAL.cs
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
using MCSFramework.Model.CM;
using MCSFramework.SQLDAL.CM;

namespace MCSFramework.BLL.CM
{
    /// <summary>
    ///CM_VehicleBLL业务逻辑BLL类
    /// </summary>
    public class CM_VehicleBLL : BaseSimpleBLL<CM_Vehicle>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.CM.CM_VehicleDAL";
        private CM_VehicleDAL _dal;

        #region 构造函数
        ///<summary>
        ///CM_VehicleBLL
        ///</summary>
        public CM_VehicleBLL()
            : base(DALClassName)
        {
            _dal = (CM_VehicleDAL)_DAL;
            _m = new CM_Vehicle();
        }

        public CM_VehicleBLL(int id)
            : base(DALClassName)
        {
            _dal = (CM_VehicleDAL)_DAL;
            FillModel(id);
        }

        public CM_VehicleBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (CM_VehicleDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<CM_Vehicle> GetModelList(string condition)
        {
            return new CM_VehicleBLL()._GetModelList(condition);
        }
        #endregion

        /// <summary>
        /// 获取指定客户的可用车辆
        /// </summary>
        /// <param name="Client"></param>
        /// <returns></returns>
        public static IList<CM_Vehicle> GetVehicleByClient(int Client)
        {
            return GetModelList("Client=" + Client.ToString() + " AND State IN (1,2)");
        }
        /// <summary>
        /// 获取指定车辆关联的仓库
        /// </summary>
        /// <param name="Vehicle"></param>
        /// <returns></returns>
        public CM_WareHouse GetRelateWareHouse()
        {
            CM_Vehicle v = new CM_VehicleBLL(_m.ID).Model;
            if (v != null)
            {
                IList<CM_WareHouse> list = CM_WareHouseBLL.GetModelList("ID = " + v.RelateWareHouse.ToString());
                if (list.Count > 0) return list[0];
            }
            return null;
        }
    }
}