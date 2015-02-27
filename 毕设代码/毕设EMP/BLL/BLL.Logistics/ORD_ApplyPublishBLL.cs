
// ===================================================================
// 文件： ORD_ApplyPublishDAL.cs
// 项目名称：
// 创建时间：2009/9/5
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model.Logistics;
using MCSFramework.SQLDAL.Logistics;

namespace MCSFramework.BLL.Logistics
{
    /// <summary>
    ///ORD_ApplyPublishBLL业务逻辑BLL类
    /// </summary>
    public class ORD_ApplyPublishBLL : BaseComplexBLL<ORD_ApplyPublish, ORD_ApplyPublishDetail>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.Logistics.ORD_ApplyPublishDAL";
        private ORD_ApplyPublishDAL _dal;

        #region 构造函数
        ///<summary>
        ///ORD_ApplyPublishBLL
        ///</summary>
        public ORD_ApplyPublishBLL()
            : base(DALClassName)
        {
            _dal = (ORD_ApplyPublishDAL)_DAL;
            _m = new ORD_ApplyPublish();
        }

        public ORD_ApplyPublishBLL(int id)
            : base(DALClassName)
        {
            _dal = (ORD_ApplyPublishDAL)_DAL;
            FillModel(id);
        }

        public ORD_ApplyPublishBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (ORD_ApplyPublishDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<ORD_ApplyPublish> GetModelList(string condition)
        {
            return new ORD_ApplyPublishBLL()._GetModelList(condition);
        }
        #endregion

        /// <summary>
        /// 产品请购发布
        /// </summary>
        /// <param name="state"></param>
        /// <param name="staff"></param>
        /// <returns></returns>
        public int Publish(int state, int staff)
        {
            return _dal.Publish(_m.ID, state, staff);
        }

        /// <summary>
        /// 复制请购发布目录
        /// </summary>
        /// <param name="staff"></param>
        /// <returns></returns>
        public int Copy(int staff)
        {
            return _dal.Copy(_m.ID, staff);
        }

        public void GetMinApplyAmount(out decimal MinApplyAmount, out decimal MaxApplyAmount)
        {
            _dal.GetMinApplyAmount(_m.ID, out MinApplyAmount, out MaxApplyAmount);
        }
        public static DataTable GetbyOrganizeCity(int OrganizeCity, int Type)
        {
            ORD_ApplyPublishDAL dal = (ORD_ApplyPublishDAL)DataAccess.CreateObject(DALClassName);
            return Tools.ConvertDataReaderToDataTable(dal.GetbyOrganizeCity(OrganizeCity, Type));
        }
    }
}