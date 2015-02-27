
// ===================================================================
// 文件： Addr_OrganizeCityParamDAL.cs
// 项目名称：
// 创建时间：2012/11/27 星期二
// 作者:	   Jace
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model;
using MCSFramework.SQLDAL;

namespace MCSFramework.BLL
{
    /// <summary>
    ///Addr_OrganizeCityParamBLL业务逻辑BLL类
    /// </summary>
    public class Addr_OrganizeCityParamBLL : BaseSimpleBLL<Addr_OrganizeCityParam>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.Addr_OrganizeCityParamDAL";
        private Addr_OrganizeCityParamDAL _dal;

        #region 构造函数
        ///<summary>
        ///Addr_OrganizeCityParamBLL
        ///</summary>
        public Addr_OrganizeCityParamBLL()
            : base(DALClassName)
        {
            _dal = (Addr_OrganizeCityParamDAL)_DAL;
            _m = new Addr_OrganizeCityParam();
        }

        public Addr_OrganizeCityParamBLL(int id)
            : base(DALClassName)
        {
            _dal = (Addr_OrganizeCityParamDAL)_DAL;
            FillModel(id);
        }

        public Addr_OrganizeCityParamBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (Addr_OrganizeCityParamDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<Addr_OrganizeCityParam> GetModelList(string condition)
        {
            return new Addr_OrganizeCityParamBLL()._GetModelList(condition);
        }
        #endregion

        public static DataTable GetByOrganizeCity(int OrganizeCity, int ParamType, int Include)
        {
            Addr_OrganizeCityParamDAL dal = (Addr_OrganizeCityParamDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetByOrganizeCity(OrganizeCity, ParamType, Include);
        }
        public static string GetValueByType(int OrganizeCity, int ParamType)
        {
            Addr_OrganizeCityParamDAL dal = (Addr_OrganizeCityParamDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetValueByType(OrganizeCity, ParamType);
        }
    }
}