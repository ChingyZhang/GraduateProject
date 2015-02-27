
// ===================================================================
// 文件： CM_FLApply_BaseBLL.cs
// 项目名称：
// 创建时间：2013-06-20
// 作者:	   Jace
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
    ///CM_FLApply_BaseBLL业务逻辑BLL类
    /// </summary>
    public class CM_FLApply_BaseBLL : BaseSimpleBLL<CM_FLApply_Base>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.CM.CM_FLApply_BaseDAL";
        private CM_FLApply_BaseDAL _dal;

        #region 构造函数
        ///<summary>
        ///CM_FLApply_BaseBLL
        ///</summary>
        public CM_FLApply_BaseBLL()
            : base(DALClassName)
        {
            _dal = (CM_FLApply_BaseDAL)_DAL;
            _m = new CM_FLApply_Base();
        }

        public CM_FLApply_BaseBLL(int id)
            : base(DALClassName)
        {
            _dal = (CM_FLApply_BaseDAL)_DAL;
            FillModel(id);
        }

        public CM_FLApply_BaseBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (CM_FLApply_BaseDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<CM_FLApply_Base> GetModelList(string condition)
        {
            return new CM_FLApply_BaseBLL()._GetModelList(condition);
        }
        #endregion

        public static DataTable GetByDIClient(int OrganizeCIty, int AccountMonth, int DIClient)
        {
            CM_FLApply_BaseDAL dal = (CM_FLApply_BaseDAL)DataAccess.CreateObject(DALClassName);
            return Tools.ConvertDataReaderToDataTable(dal.GetByDIClient(OrganizeCIty, AccountMonth, DIClient));
        }

        public static DataTable GetByOrganizeCity(int OrganizeCIty, int AccountMonth, int Client, int ISMYD, int FLType)
        {
            CM_FLApply_BaseDAL dal = (CM_FLApply_BaseDAL)DataAccess.CreateObject(DALClassName);
            return Tools.ConvertDataReaderToDataTable(dal.GetByOrganizeCity(OrganizeCIty, AccountMonth, Client, ISMYD, FLType));
        }
    }
}