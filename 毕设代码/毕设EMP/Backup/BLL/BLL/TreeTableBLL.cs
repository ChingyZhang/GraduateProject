using System;
using System.Collections.Generic;
using System.Text;
using MCSFramework.SQLDAL;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.Common;
using System.Web.Caching;
namespace MCSFramework.BLL
{
    [Serializable]
    public class TreeTableBLL
    {
        /// <summary>
        /// 获取所有树结构表数据
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="IDName"></param>
        /// <param name="NameColumn"></param>
        /// <param name="SuperIDName"></param>
        /// <returns></returns>
        public static DataTable GetAllNode(string TableName, string IDName, string NameColumn, string SuperIDName)
        {
            string CacheKey = "Cache-TreeTableBLL-GetAllNode-" + TableName;
            DataTable dt = (DataTable)DataCache.GetCache(CacheKey);

            if (dt == null)
            {
                dt = Tools.ConvertDataReaderToDataTable(TreeTableDAL.GetAllNode(TableName, IDName, NameColumn, SuperIDName));

                #region 写入缓存
                int ModelCache = ConfigHelper.GetConfigInt("ModelCache");
                DataCache.SetCache(CacheKey, dt, DateTime.Now.AddMinutes(ModelCache), Cache.NoSlidingExpiration);
                #endregion
            }
            return dt;
        }
        /// <summary>
        /// 获取一个（或多个）节点的所有子节点，仅ID列
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="IDName"></param>
        /// <param name="SuperIDName"></param>
        /// <param name="IDs"></param>
        /// <returns></returns>
        public static DataTable GetAllChildByNodes(string TableName, string IDName, string SuperIDName, string IDs)
        {
            string CacheKey = "Cache-TreeTableBLL-GetAllChildByNodes-" + TableName + "-" + IDs;
            DataTable dt = (DataTable)DataCache.GetCache(CacheKey);

            if (dt == null)
            {
                dt = Tools.ConvertDataReaderToDataTable(TreeTableDAL.GetAllChildByNodes(TableName, IDName, SuperIDName, IDs));

                #region 写入缓存
                int ModelCache = ConfigHelper.GetConfigInt("ModelCache");
                DataCache.SetCache(CacheKey, dt, DateTime.Now.AddMinutes(ModelCache), Cache.NoSlidingExpiration);
                #endregion
            }
            return dt;
        }

        /// <summary>
        ///  获取一个（或多个）节点的所有子节点，全表列
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="IDName"></param>
        /// <param name="SuperIDName"></param>
        /// <param name="IDs"></param>
        /// <returns></returns>
        public static DataTable GetAllChildNodeByNodes(string TableName, string IDName, string SuperIDName, string IDs)
        {
            string CacheKey = "Cache-TreeTableBLL-GetAllChildNodeByNodes-" + TableName + "-" + IDs;
            DataTable dt = (DataTable)DataCache.GetCache(CacheKey);

            if (dt == null)
            {
                dt = Tools.ConvertDataReaderToDataTable(TreeTableDAL.GetAllChildNodeByNodes(TableName, IDName, SuperIDName, IDs));

                #region 写入缓存
                int ModelCache = ConfigHelper.GetConfigInt("ModelCache");
                DataCache.SetCache(CacheKey, dt, DateTime.Now.AddMinutes(ModelCache), Cache.NoSlidingExpiration);
                #endregion
            }
            return dt;

        }
        public static DataTable GetAllSuperNodeIDs(string TableName, string IDName, string LowIDName, string IDs)
        {
            string CacheKey = "Cache-TreeTableBLL-GetAllSuperNodeIDs-" + TableName + "-" + IDs;
            DataTable dt = (DataTable)DataCache.GetCache(CacheKey);

            if (dt == null)
            {
                dt = Tools.ConvertDataReaderToDataTable(TreeTableDAL.GetAllSuperNodeIDs(TableName, IDName, LowIDName, IDs));

                #region 写入缓存
                int ModelCache = ConfigHelper.GetConfigInt("ModelCache");
                DataCache.SetCache(CacheKey, dt, DateTime.Now.AddMinutes(ModelCache), Cache.NoSlidingExpiration);
                #endregion
            }
            return dt;
        }


        /// <summary>
        /// 获取某节点的完整路径
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="IDName"></param>
        /// <param name="SuperIDName"></param>
        /// <param name="RootID"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static DataTable GetFullPath(string TableName, string IDName, string SuperIDName, int RootID, int ID)
        {
            return Tools.ConvertDataReaderToDataTable(TreeTableDAL.GetFullPath(TableName, IDName, SuperIDName, RootID, ID));
        }

        /// <summary>
        /// 获取某节点的完整路径
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="IDName"></param>
        /// <param name="SuperIDName"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static DataTable GetFullPath(string TableName, string IDName, string SuperIDName, int ID)
        {
            return Tools.ConvertDataReaderToDataTable(TreeTableDAL.GetFullPath(TableName, IDName, SuperIDName, 1, ID));
        }


        /// <summary>
        /// 获取某节点的子节点（只获取一层子节点）
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="IDName"></param>
        /// <param name="SuperIDName"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static DataTable GetChild(string TableName, string IDName, string SuperIDName, int ID)
        {
            return Tools.ConvertDataReaderToDataTable(TreeTableDAL.GetChild(TableName, IDName, SuperIDName, ID));
        }

        /// <summary>
        /// 获取指定节点的所有从根到本节点的全路径名称
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="IDName"></param>
        /// <param name="NameColumnName"></param>
        /// <param name="SuperIDName"></param>
        /// <param name="RootID"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static string GetFullPathName(string TableName, string IDName, string NameColumnName, string SuperIDName, int RootID, int ID)
        {
            string CacheKey = "Cache-TreeTableBLL-GetFullPathName-" + TableName + "-" + IDName + "-" + NameColumnName + "-" + SuperIDName + "-" + RootID.ToString() + "-" + ID.ToString();
            string fullname = (string)DataCache.GetCache(CacheKey);

            if (fullname == null)
            {
                fullname = TreeTableDAL.GetFullPathName(TableName, IDName, NameColumnName, SuperIDName, RootID, ID);

                #region 写入缓存
                int ModelCache = ConfigHelper.GetConfigInt("ModelCache");
                DataCache.SetCache(CacheKey, fullname, DateTime.Now.AddMinutes(ModelCache), Cache.NoSlidingExpiration);
                #endregion
            }
            return fullname;
        }

        /// <summary>
        /// 获取指定节点的所有从根到本节点的全路径名称
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static string GetFullPathName(string TableName, int ID)
        {
            return TreeTableDAL.GetFullPathName(TableName, ID);
        }

        /// <summary>
        /// 获取树结构指定父级的ID
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="IDName"></param>
        /// <param name="SuperIDName"></param>
        /// <param name="ID"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public static int GetSuperIDByLevel(string TableName, string IDName, string SuperIDName, int ID, int Level)
        {
            string CacheKey = "Cache-TreeTableBLL-GetSuperIDByLevel-" + TableName + "-" + IDName + "-" + SuperIDName + "-" + ID.ToString() + "-" + Level.ToString();
            int superid = -1;

            object o = DataCache.GetCache(CacheKey);
            if (o == null)
            {
                DataTable dt = GetFullPath(TableName, IDName, SuperIDName, ID);
                if (dt.Rows.Count > Level) superid = (int)dt.Rows[Level][0];

                #region 写入缓存
                int ModelCache = ConfigHelper.GetConfigInt("ModelCache");
                DataCache.SetCache(CacheKey, superid, DateTime.Now.AddMinutes(ModelCache), Cache.NoSlidingExpiration);
                #endregion
            }
            else
            {
                superid = (int)o;
            }
            return superid;


        }

        public static int GetSuperIDByLevel(string TableName, int ID, int Level)
        {
            return GetSuperIDByLevel(TableName, "ID", "SuperID", ID, Level);
        }
        /// <summary>
        /// 获取树结构指定父级的名称
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="IDName"></param>
        /// <param name="NameColumnName"></param>
        /// <param name="SuperIDName"></param>
        /// <param name="ID"></param>
        /// <param name="Level"></param>
        /// <returns></returns>
        public static string GetSuperNameByLevel(string TableName, string IDName, string NameColumnName, string SuperIDName, int ID, int Level)
        {
            string CacheKey = "Cache-TreeTableBLL-GetSuperNameByLevel-" + TableName + "-" + IDName + "-" + NameColumnName + "-" + SuperIDName + "-" + ID.ToString() + "-" + Level.ToString();
            string supername = (string)DataCache.GetCache(CacheKey);

            if (supername == null)
            {
                supername = "";
                int superid = GetSuperIDByLevel(TableName, IDName, SuperIDName, ID, Level);
                if (superid > 0) supername = GetRelationTableDataValue(TableName, IDName, NameColumnName, superid.ToString());

                #region 写入缓存
                int ModelCache = ConfigHelper.GetConfigInt("ModelCache");
                DataCache.SetCache(CacheKey, supername, DateTime.Now.AddMinutes(ModelCache), Cache.NoSlidingExpiration);
                #endregion
            }
            return supername;
        }

        public static string GetSuperNameByLevel(string TableName, int ID, int Level)
        {
            return GetSuperNameByLevel(TableName, "ID", "Name", "SuperID", ID, Level);
        }
        /// <summary>
        /// get relation value of the special fieldname of the table
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="KeyName"></param>
        /// <param name="ValueName"></param>
        /// <returns></returns>
        public static DataTable GetRelationTableSourceData(string TableName, string KeyName, string ValueName)
        {
            string CacheKey = "Cache-TreeTableBLL-GetRelationTableSourceData-" + TableName + "-" + TableName + "-" + KeyName + "-" + ValueName;
            DataTable dt = (DataTable)DataCache.GetCache(CacheKey);

            if (dt == null)
            {
                dt = Tools.ConvertDataReaderToDataTable(TreeTableDAL.GetRelationTableSourceData(TableName, KeyName, ValueName));

                #region 写入缓存
                if (dt.Rows.Count < 10000)      //1万行以下的才进行缓存
                {
                    int ModelCache = ConfigHelper.GetConfigInt("ModelCache");
                    DataCache.SetCache(CacheKey, dt, DateTime.Now.AddMinutes(ModelCache), Cache.NoSlidingExpiration);
                }
                #endregion
            }
            return dt;
        }

        /// <summary>
        /// 获取指定表里Key值对应的Value列的值
        /// </summary>
        /// <param name="TableName">表名</param>
        /// <param name="KeyName">ID列名</param>
        /// <param name="ValueName">Name列名</param>
        /// <param name="KeyValue">ID值</param>
        /// <returns></returns>
        public static string GetRelationTableDataValue(string TableName, string KeyName, string ValueName, string KeyValue)
        {
            if (string.IsNullOrEmpty(KeyValue)) return "";
            string CacheKey = "Cache-TreeTableBLL-GetRelationTableDataValue-" + TableName + "-" + TableName + "-" + KeyName + "-" + ValueName + "-" + KeyValue;
            string s = (string)DataCache.GetCache(CacheKey);

            if (string.IsNullOrEmpty(s))
            {
                s = TreeTableDAL.GetRelationTableDataValue(TableName, KeyName, ValueName, KeyValue);

                #region 写入缓存
                int ModelCache = ConfigHelper.GetConfigInt("ModelCache");
                DataCache.SetCache(CacheKey, s, DateTime.Now.AddMinutes(ModelCache), Cache.NoSlidingExpiration);
                #endregion
            }
            return s;
        }

        public static DataTable ExecSqlString(string SqlString, int PageSize, int PageIndex, out int TotalRecordCount)
        {
            return TreeTableDAL.ExecSqlString(SqlString, PageSize, PageIndex, out TotalRecordCount);
        }

        public static DataTable ExecSqlString(string SqlString, int PageSize, int PageIndex, string OrderFields, out int TotalRecordCount)
        {
            return TreeTableDAL.ExecSqlString(SqlString, PageSize, PageIndex, OrderFields, out TotalRecordCount);
        }

        public static DataTable ExecSqlString(string DBConnectString, string SqlString, int PageSize, int PageIndex, string OrderFields, out int TotalRecordCount)
        {
            return TreeTableDAL.ExecSqlString(DBConnectString, SqlString, PageSize, PageIndex, OrderFields, out TotalRecordCount);
        }

        public static int ExecSqlStringByTotalCount(string DBConnectString,string SqlString, out int TotalRecordCount)
        {
            return TreeTableDAL.ExecSqlStringByTotalCount( DBConnectString, SqlString, out TotalRecordCount);
        }
    }
}
