using MCSFramework.BLL;
using MCSFramework.BLL.CM;
using MCSFramework.Common;
using MCSFramework.Model;
using MCSFramework.Model.CM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MCSFramework.WSI.Model
{
    /// <summary>
    /// 车辆信息
    /// </summary>
    [Serializable]
    public class Vehicle
    {
        /// <summary>
        /// 车辆ID
        /// </summary>
        public int ID = 0;

        /// <summary>
        /// 车牌号
        /// </summary>
        public string VehicleNo = "";

        /// <summary>
        /// 车辆类别(1.客用车 2.货用车)
        /// </summary>
        public int VehicleClassify = 0;
        public string VehicleClassifyName = "";

        /// <summary>
        /// 关联车销员工
        /// </summary>
        public int RelateStaff = 0;
        public string RelateStaffName = "";

        /// <summary>
        /// 行驶公里数
        /// </summary>
        public int Kilometres = 0;

        /// <summary>
        /// 关联车辆仓库
        /// </summary>
        public int RelateWareHouse = 0;
        public string RelateWareHouseName = "";

        public Vehicle() { }

        public Vehicle(int VehicleID) { }

        public Vehicle(CM_Vehicle m)
        {
            FillModel(m);
        }

        private void FillModel(CM_Vehicle m)
        {
            if (m == null) return;

            ID = m.ID;
            VehicleNo = m.VehicleNo;
            RelateStaff = m.RelateStaff;
            RelateWareHouse = m.RelateWareHouse;

            if (RelateStaff != 0)
            {
                Org_Staff s = new Org_StaffBLL(RelateStaff).Model;
                if (s != null) RelateStaffName = s.RealName;
            }

            if (RelateWareHouse != 0)
            {
                CM_WareHouse w = new CM_WareHouseBLL(RelateWareHouse).Model;
                if (w != null) RelateWareHouseName = w.Name;
            }

            #region 获取字典表名称
            try
            {
                if (m.VehicleClassify > 0)
                {
                    Dictionary_Data dic = DictionaryBLL.GetDicCollections("CM_VehicleClassify")[m.VehicleClassify.ToString()];
                    if (dic != null) VehicleClassifyName = dic.Name;
                }
            }
            catch (System.Exception err)
            {
                LogWriter.WriteLog("MCSFramework.WSI.Vehicle", err);
            }
            #endregion
        }

    }
}