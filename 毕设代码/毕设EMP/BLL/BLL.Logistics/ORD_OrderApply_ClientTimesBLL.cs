
// ===================================================================
// 文件： ORD_OrderApply_ClientTimesBLL.cs
// 项目名称：
// 创建时间：2013-03-20
// 作者:	   Jace
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
    ///ORD_OrderApply_ClientTimesBLL业务逻辑BLL类
    /// </summary>
    public class ORD_OrderApply_ClientTimesBLL : BaseSimpleBLL<ORD_OrderApply_ClientTimes>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.Logistics.ORD_OrderApply_ClientTimesDAL";
        private ORD_OrderApply_ClientTimesDAL _dal;

        #region 构造函数
        ///<summary>
        ///ORD_OrderApply_ClientTimesBLL
        ///</summary>
        public ORD_OrderApply_ClientTimesBLL()
            : base(DALClassName)
        {
            _dal = (ORD_OrderApply_ClientTimesDAL)_DAL;
            _m = new ORD_OrderApply_ClientTimes();
        }

        public ORD_OrderApply_ClientTimesBLL(int id)
            : base(DALClassName)
        {
            _dal = (ORD_OrderApply_ClientTimesDAL)_DAL;
            FillModel(id);
        }

        public ORD_OrderApply_ClientTimesBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (ORD_OrderApply_ClientTimesDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<ORD_OrderApply_ClientTimes> GetModelList(string condition)
        {
            return new ORD_OrderApply_ClientTimesBLL()._GetModelList(condition);
        }
        #endregion

        public static DataTable GetByOrganizeCity(int OrganizeCity, int Client)
        {
            ORD_OrderApply_ClientTimesDAL dal = (ORD_OrderApply_ClientTimesDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetByOrganizeCity(OrganizeCity, Client);
        }
    }
}