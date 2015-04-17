
// ===================================================================
// 文件： CM_VehicleInStaffDAL.cs
// 项目名称：
// 创建时间：2015-02-01
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
using MCSFramework.Model;

namespace MCSFramework.BLL.CM
{
    /// <summary>
    ///CM_VehicleInStaffBLL业务逻辑BLL类
    /// </summary>
    public class CM_VehicleInStaffBLL : BaseSimpleBLL<CM_VehicleInStaff>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.CM.CM_VehicleInStaffDAL";
        private CM_VehicleInStaffDAL _dal;

        #region 构造函数
        ///<summary>
        ///CM_VehicleInStaffBLL
        ///</summary>
        public CM_VehicleInStaffBLL()
            : base(DALClassName)
        {
            _dal = (CM_VehicleInStaffDAL)_DAL;
            _m = new CM_VehicleInStaff();
        }

        public CM_VehicleInStaffBLL(int id)
            : base(DALClassName)
        {
            _dal = (CM_VehicleInStaffDAL)_DAL;
            FillModel(id);
        }

        public CM_VehicleInStaffBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (CM_VehicleInStaffDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<CM_VehicleInStaff> GetModelList(string condition)
        {
            return new CM_VehicleInStaffBLL()._GetModelList(condition);
        }
        #endregion


        #region 获取指定员工关联的车辆
        /// <summary>
        /// 获取指定员工关联的车辆
        /// </summary>
        /// <param name="Staff">员工ID</param>
        /// <returns></returns>
        public static IList<CM_Vehicle> GetVehicleByStaff(int Staff)
        {
            return CM_VehicleBLL.GetModelList("ID IN (SELECT Vehicle FROM MCS_CM.dbo.CM_VehicleInStaff WHERE Staff = " + Staff.ToString() + ") AND State=1");
        }
        #endregion

        #region 获取车辆关联的员工
        /// <summary>
        /// 获取车辆关联的员工
        /// </summary>
        /// <param name="Vehicle">车辆ID</param>
        /// <returns></returns>
        public static IList<Org_Staff> GetStaffByVehicle(int Vehicle)
        {
            return Org_StaffBLL.GetStaffList("ID IN (SELECT Staff FROM MCS_CM.dbo.CM_VehicleInStaff WHERE Vehicle = " + Vehicle.ToString() + ") AND Dimission=1");
        }
        #endregion

    }
}