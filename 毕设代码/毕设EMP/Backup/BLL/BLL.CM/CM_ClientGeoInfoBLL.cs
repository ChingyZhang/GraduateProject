
// ===================================================================
// 文件： CM_ClientGeoInfoDAL.cs
// 项目名称：
// 创建时间：2014-03-09
// 作者:	   ShenGang
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
    ///CM_ClientGeoInfoBLL业务逻辑BLL类
    /// </summary>
    public class CM_ClientGeoInfoBLL : BaseSimpleBLL<CM_ClientGeoInfo>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.CM.CM_ClientGeoInfoDAL";
        private CM_ClientGeoInfoDAL _dal;

        #region 构造函数
        ///<summary>
        ///CM_ClientGeoInfoBLL
        ///</summary>
        public CM_ClientGeoInfoBLL()
            : base(DALClassName)
        {
            _dal = (CM_ClientGeoInfoDAL)_DAL;
            _m = new CM_ClientGeoInfo();
        }

        public CM_ClientGeoInfoBLL(int id)
            : base(DALClassName)
        {
            _dal = (CM_ClientGeoInfoDAL)_DAL;
            FillModel(id);
        }

        public CM_ClientGeoInfoBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (CM_ClientGeoInfoDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<CM_ClientGeoInfo> GetModelList(string condition)
        {
            return new CM_ClientGeoInfoBLL()._GetModelList(condition);
        }
        #endregion

        /// <summary>
        /// 获取指定客户的地理信息
        /// </summary>
        /// <param name="Client"></param>
        /// <returns></returns>
        public static CM_ClientGeoInfo GetGeoInfoByClient(int Client)
        {
            IList<CM_ClientGeoInfo> lists = GetModelList("Client=" + Client.ToString());
            if (lists.Count == 0)
                return null;
            else
                return lists[0];
        }
    }
}