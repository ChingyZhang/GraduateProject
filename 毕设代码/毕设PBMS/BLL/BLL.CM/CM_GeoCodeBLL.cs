
// ===================================================================
// 文件： CM_GeoCodeDAL.cs
// 项目名称：
// 创建时间：2015-03-24
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

namespace MCSFramework.BLL.CM
{
    /// <summary>
    ///CM_GeoCodeBLL业务逻辑BLL类
    /// </summary>
    public class CM_GeoCodeBLL : BaseSimpleBLL<CM_GeoCode>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.CM.CM_GeoCodeDAL";
        private CM_GeoCodeDAL _dal;

        #region 构造函数
        ///<summary>
        ///CM_GeoCodeBLL
        ///</summary>
        public CM_GeoCodeBLL()
            : base(DALClassName)
        {
            _dal = (CM_GeoCodeDAL)_DAL;
            _m = new CM_GeoCode();
        }

        public CM_GeoCodeBLL(int id)
            : base(DALClassName)
        {
            _dal = (CM_GeoCodeDAL)_DAL;
            FillModel(id);
        }

        public CM_GeoCodeBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (CM_GeoCodeDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<CM_GeoCode> GetModelList(string condition)
        {
            return new CM_GeoCodeBLL()._GetModelList(condition);
        }
        #endregion

        /// <summary>
        /// 根据城市编码获取所属地理编码
        /// </summary>
        /// <param name="CityCode"></param>
        /// <returns></returns>
        public static IList<CM_GeoCode> GetListByCityCode(string CityCode)
        {
            return GetModelList("CityCode='" + CityCode + "'");
        }
    }
}