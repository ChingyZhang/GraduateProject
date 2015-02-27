
// ===================================================================
// 文件： DB_IDMapTableDAL.cs
// 项目名称：
// 创建时间：2010/1/19
// 作者:	   Shen Gang
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
    ///DB_IDMapTableBLL业务逻辑BLL类
    /// </summary>
    public class DB_IDMapTableBLL : BaseSimpleBLL<DB_IDMapTable>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.DB_IDMapTableDAL";
        private DB_IDMapTableDAL _dal;

        #region 构造函数
        ///<summary>
        ///DB_IDMapTableBLL
        ///</summary>
        public DB_IDMapTableBLL()
            : base(DALClassName)
        {
            _dal = (DB_IDMapTableDAL)_DAL;
            _m = new DB_IDMapTable();
        }

        public DB_IDMapTableBLL(int id)
            : base(DALClassName)
        {
            _dal = (DB_IDMapTableDAL)_DAL;
            FillModel(id);
        }

        public DB_IDMapTableBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (DB_IDMapTableDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<DB_IDMapTable> GetModelList(string condition)
        {
            return new DB_IDMapTableBLL()._GetModelList(condition);
        }
        #endregion

        /// <summary>
        /// 增加一条映射记录
        /// </summary>
        /// <param name="tablename"></param>
        /// <param name="idv3"></param>
        /// <param name="namev3"></param>
        /// <param name="idv4"></param>
        /// <param name="namev4"></param>
        /// <returns></returns>
        public static int AddMap(string tablename, int idv3, string namev3, int idv4, string namev4)
        {
            DB_IDMapTableBLL bll = new DB_IDMapTableBLL();

            bll.Model.TableName = tablename;
            bll.Model.IDV3 = idv3;
            bll.Model.IDV4 = idv4;
            bll.Model.NameV3 = namev3;
            bll.Model.NameV4 = namev4;

            return bll.Add();
        }

        public static int AddMap(string tablename, int idv3, string name, int idv4)
        {
            return AddMap(tablename, idv3, name, idv4, name);
        }

        public static int AddMap(string tablename, int idv3, int idv4)
        {
            return AddMap(tablename, idv3, "", idv4, "");
        }

        /// <summary>
        /// 根据V3版的ID获取V4版的ID值
        /// </summary>
        /// <param name="tablename"></param>
        /// <param name="idv3"></param>
        /// <returns></returns>
        public static int FindIDV4(string tablename, int idv3)
        {
            string CacheKey = "Cache-DB_IDMapTable-" + tablename.ToString() + "-" + idv3.ToString();
            int idv4 = 0;


            if (DataCache.GetCache(CacheKey) != null)
            {
                idv4 = (int)DataCache.GetCache(CacheKey);
            }
            else
            {
                IList<DB_IDMapTable> list = GetModelList("TableName='" + tablename + "' AND IDV3=" + idv3.ToString());

                if (list.Count == 0)
                    idv4 = 0;
                else
                    idv4 = list[0].IDV4;

                if (tablename != "CRM_CM_ClientInfo" && tablename != "CRM_SV_Service_Accept" && tablename != "CRM_SV_Service_Task" &&
                    tablename != "CRM_CM_LinkMan")
                    DataCache.SetCache(CacheKey, idv4);
            }

            return idv4;
        }
    }
}