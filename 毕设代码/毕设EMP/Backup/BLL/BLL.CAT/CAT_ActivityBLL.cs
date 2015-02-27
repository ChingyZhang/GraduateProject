
// ===================================================================
// 文件： CAT_ActivityDAL.cs
// 项目名称：
// 创建时间：2009/11/29
// 作者:	   Shen Gang
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
    ///CAT_ActivityBLL业务逻辑BLL类
    /// </summary>
    public class CAT_ActivityBLL : BaseSimpleBLL<CAT_Activity>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.CAT.CAT_ActivityDAL";
        private CAT_ActivityDAL _dal;

        #region 构造函数
        ///<summary>
        ///CAT_ActivityBLL
        ///</summary>
        public CAT_ActivityBLL()
            : base(DALClassName)
        {
            _dal = (CAT_ActivityDAL)_DAL;
            _m = new CAT_Activity();
        }

        public CAT_ActivityBLL(int id)
            : base(DALClassName)
        {
            _dal = (CAT_ActivityDAL)_DAL;
            FillModel(id);//根据ID值提供Model对象
        }

        public CAT_ActivityBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (CAT_ActivityDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<CAT_Activity> GetModelList(string condition)
        {
            return new CAT_ActivityBLL()._GetModelList(condition);
        }
        #endregion

        /// <summary>
        /// 获取当前活动的消费者参与记录
        /// </summary>
        /// <returns></returns>
        public IList<CAT_ClientJoinInfo> GetClientJoinInfo()
        {
            return CAT_ClientJoinInfoBLL.GetModelList("Activity = " + _m.ID.ToString());
        }

        /// <summary>
        /// 活动审核
        /// </summary>
        /// <param name="State"></param>
        /// <param name="ApproveStaff"></param>
        /// <returns></returns>
        public int Approve(int State, int ApproveStaff)
        {
            return _dal.Approve(_m.ID, State, ApproveStaff);
        }
    }
}