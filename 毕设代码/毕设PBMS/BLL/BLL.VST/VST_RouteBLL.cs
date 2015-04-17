
// ===================================================================
// 文件： VST_RouteBLL.cs
// 项目名称：
// 创建时间：2015/1/27
// 作者:	   ChingyZhang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model.VST;
using MCSFramework.SQLDAL.VST;

namespace MCSFramework.BLL.VST
{
    /// <summary>
    ///VST_RouteDAL业务逻辑BLL类
    /// </summary>
    public class VST_RouteBLL : BaseSimpleBLL<VST_Route>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.VST.VST_RouteDAL";
        private VST_RouteDAL _dal;

        #region 构造函数
        ///<summary>
        ///VST_RouteDAL
        ///</summary>
        public VST_RouteBLL()
            : base(DALClassName)
        {
            _dal = (VST_RouteDAL)_DAL;
            _m = new VST_Route();
        }

        public VST_RouteBLL(int id)
            : base(DALClassName)
        {
            _dal = (VST_RouteDAL)_DAL;
            FillModel(id);
        }

        public VST_RouteBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (VST_RouteDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<VST_Route> GetModelList(string condition)
        {
            return new VST_RouteBLL()._GetModelList(condition);
        }
        #endregion

        #region 获取指定员工的拜访线路
        /// <summary>
        /// 获取指定员工的拜访线路
        /// </summary>
        /// <param name="RelateStaff"></param>
        /// <returns></returns>
        public static IList<VST_Route> GetRouteListByStaff(int RelateStaff)
        {
            return GetModelList("RelateStaff = " + RelateStaff.ToString() + " AND EnableFlag='Y'");
        }

        /// <summary>
        /// 获取指定TDP的拜访线路
        /// </summary>
        /// <param name="TDP"></param>
        /// <returns></returns>
        public static IList<VST_Route> GetRouteListByTDP(int TDP)
        {
            return GetModelList("OwnerClient = " + TDP.ToString() + " AND EnableFlag='Y'");
        }
        #endregion

        /// <summary>
        /// 获取指定区域内的线路列表
        /// </summary>
        /// <param name="OrganizeCity"></param>
        /// <returns></returns>
        public static IList<VST_Route> GetByOrganizeCity(int OrganizeCity)
        {
            VST_RouteDAL dal = (VST_RouteDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetByOrganizeCity(OrganizeCity);
        }
    }
}