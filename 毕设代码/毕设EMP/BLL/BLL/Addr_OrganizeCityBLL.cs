
// ===================================================================
// 文件： Addr_OrganizeCityDAL.cs
// 项目名称：
// 创建时间：2008-12-17
// 作者:	   
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model;
using MCSFramework.SQLDAL;
using System.Collections.Generic;

namespace MCSFramework.BLL
{
    /// <summary>
    ///Addr_OrganizeCityBLL业务逻辑BLL类
    /// </summary>
    public class Addr_OrganizeCityBLL : BaseSimpleBLL<Addr_OrganizeCity>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.Addr_OrganizeCityDAL";
        private Addr_OrganizeCityDAL _dal;

        #region 构造函数
        ///<summary>
        ///Addr_OrganizeCityBLL
        ///</summary>
        public Addr_OrganizeCityBLL()
            : base(DALClassName)
        {
            _dal = (Addr_OrganizeCityDAL)_DAL;
            _m = new Addr_OrganizeCity();
        }

        public Addr_OrganizeCityBLL(int id)
            : base(DALClassName)
        {
            _dal = (Addr_OrganizeCityDAL)_DAL;
            FillModel(id);
        }

        public Addr_OrganizeCityBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (Addr_OrganizeCityDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<Addr_OrganizeCity> GetModelList(string condition)
        {
            return new Addr_OrganizeCityBLL()._GetModelList(condition);
        }
        #endregion

        public DataTable GetAllChildNode()
        {
            return TreeTableBLL.GetAllChildByNodes("MCS_SYS.dbo.Addr_OrganizeCity", "ID", "SuperID", _m.ID.ToString());
        }

        public DataTable GetAllChildNodeIncludeSelf()
        {
            DataTable citys = TreeTableBLL.GetAllChildNodeByNodes("MCS_SYS.dbo.Addr_OrganizeCity", "ID", "SuperID", _m.ID.ToString()).Copy();
            DataRow dr = citys.NewRow();
            dr["ID"] = _m.ID;
            dr["Name"] = _m.Name;
            dr["SuperID"] = _m.SuperID;
            citys.Rows.Add(dr);
            return citys;
        }

        public string GetAllChildNodeIDs()
        {
            DataTable dt = TreeTableBLL.GetAllChildByNodes("MCS_SYS.dbo.Addr_OrganizeCity", "ID", "SuperID", _m.ID.ToString());
            string ids = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (ids != "") ids += ",";
                ids += dt.Rows[i]["ID"];
            }

            return ids;
        }

        public string GetAllSuperNodeIDs()
        {
            DataTable dt = TreeTableBLL.GetAllSuperNodeIDs("MCS_SYS.dbo.Addr_OrganizeCity", "SuperID", "ID", _m.ID.ToString());
            string ids = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (ids != "") ids += ",";
                ids += dt.Rows[i]["SuperID"];
            }

            return ids;
        }

        public bool IsChildOrganizeCity(int Superid)
        {
            DataTable dt = GetFullPath();
            return dt.Select("ID=" + Superid.ToString()).Length > 0;
        }
        public DataTable GetFullPath()
        {
            DataTable dt = TreeTableBLL.GetFullPath("MCS_SYS.dbo.Addr_OrganizeCity", "ID", "SuperID", _m.ID);

            return dt;
        }

        public static DataTable GetAllOrganizeCity()
        {
            Addr_OrganizeCityDAL dal = (Addr_OrganizeCityDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetAllOrganizeCity();
        }

        /// <summary>
        /// 与员工表同步办事区、区域、城市的负责经理
        /// </summary>
        /// <returns></returns>
        public static int SyncManager()
        {
            Addr_OrganizeCityDAL dal = (Addr_OrganizeCityDAL)DataAccess.CreateObject(DALClassName);
            return dal.SyncManager();
        }
    }
}