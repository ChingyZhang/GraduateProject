using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using MCSFramework.DBUtility;
using MCSFramework.Common;

namespace MCSFramework.SQLDAL
{
    public class TreeTableDAL
    {
        /// <summary>
        /// 获取所有树结构表数据
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="IDName"></param>
        /// <param name="NameColumn"></param>
        /// <param name="SuperIDName"></param>
        /// <returns></returns>
        public static SqlDataReader GetAllNode(string TableName, string IDName, string NameColumn, string SuperIDName)
        {
            SqlDataReader dr = null;
            SqlParameter[] prams =
            {
                SQLDatabase.MakeInParam("@TableName",SqlDbType.VarChar,100,TableName),
                SQLDatabase.MakeInParam("@IDColumnn",SqlDbType.VarChar,50,IDName),
                SQLDatabase.MakeInParam("@NameColumn",SqlDbType.VarChar,50,NameColumn),
                SQLDatabase.MakeInParam("@SuperIDColumn",SqlDbType.VarChar,50,SuperIDName)                
            };

            SQLDatabase.RunProc("MCS_SYS.dbo.sp_TreeTable_GetAllNode", prams, out dr);
            return dr;
        }
        /// <summary>
        /// 获取一个（或多个）节点的所有子节点，仅ID列
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="IDName"></param>
        /// <param name="SuperIDName"></param>
        /// <param name="IDs"></param>
        /// <returns></returns>
        public static SqlDataReader GetAllChildByNodes(string TableName, string IDName, string SuperIDName, string IDs)
        {
            SqlDataReader dr = null;
            SqlParameter[] prams =
            {
                SQLDatabase.MakeInParam("@TableName",SqlDbType.VarChar,100,TableName),
                SQLDatabase.MakeInParam("@IDName",SqlDbType.VarChar,50,IDName),
                SQLDatabase.MakeInParam("@SuperIDName",SqlDbType.VarChar,50,SuperIDName),
                SQLDatabase.MakeInParam("@IDs",SqlDbType.VarChar,2000,IDs)
            };

            SQLDatabase.RunProc("MCS_SYS.dbo.sp_TreeTable_GetAllChildByNodes", prams, out dr);
            return dr;
        }

        /// <summary>
        /// 获取一个（或多个）节点的所有子节点，全表列
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="IDName"></param>
        /// <param name="SuperIDName"></param>
        /// <param name="IDs"></param>
        /// <returns></returns>
        public static SqlDataReader GetAllChildNodeByNodes(string TableName, string IDName, string SuperIDName, string IDs)
        {
            SqlDataReader dr = null;
            SqlParameter[] prams =
            {
                SQLDatabase.MakeInParam("@TableName",SqlDbType.VarChar,100,TableName),
                SQLDatabase.MakeInParam("@IDName",SqlDbType.VarChar,50,IDName),
                SQLDatabase.MakeInParam("@SuperIDName",SqlDbType.VarChar,50,SuperIDName),
                SQLDatabase.MakeInParam("@IDs",SqlDbType.VarChar,2000,IDs)
            };

            SQLDatabase.RunProc("MCS_SYS.dbo.sp_TreeTable_GetAllChildNodeByNodes", prams, out dr);
            return dr;
        }

        public static SqlDataReader GetAllSuperNodeIDs(string TableName, string IDName, string LowIDName, string IDs)
        {
            SqlDataReader dr = null;
            SqlParameter[] prams =
            {
                SQLDatabase.MakeInParam("@TableName",SqlDbType.VarChar,100,TableName),
                SQLDatabase.MakeInParam("@IDName",SqlDbType.VarChar,50,IDName),
                SQLDatabase.MakeInParam("@LowIDName",SqlDbType.VarChar,50,LowIDName),
                SQLDatabase.MakeInParam("@IDs",SqlDbType.VarChar,2000,IDs)
            };

            SQLDatabase.RunProc("MCS_SYS.dbo.sp_TreeTable_GetAllSuperNodeIDs", prams, out dr);
            return dr;
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
        public static SqlDataReader GetFullPath(string TableName, string IDName, string SuperIDName, int RootID, int ID)
        {
            SqlDataReader dr = null;
            SqlParameter[] prams =
            {
                SQLDatabase.MakeInParam("@TableName",SqlDbType.VarChar,100,TableName),
                SQLDatabase.MakeInParam("@IDName",SqlDbType.VarChar,50,IDName),
                SQLDatabase.MakeInParam("@SuperIDName",SqlDbType.VarChar,50,SuperIDName),
                SQLDatabase.MakeInParam("@RootID",SqlDbType.Int,4,RootID),
                SQLDatabase.MakeInParam("@ID",SqlDbType.Int,4,ID)
            };

            SQLDatabase.RunProc("MCS_SYS.dbo.sp_TreeTable_GetFullPath", prams, out dr);
            return dr;
        }

        /// <summary>
        /// 获取某节点的完整路径,依次获取指定节点的所有从根到本节点的所有父节点
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="IDName"></param>
        /// <param name="SuperIDName"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static SqlDataReader GetFullPath(string TableName, string IDName, string SuperIDName, int ID)
        {
            return GetFullPath(TableName, IDName, SuperIDName, 1, ID);
        }


        /// <summary>
        /// 获取某节点的子节点（只获取一层子节点）
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="IDName"></param>
        /// <param name="SuperIDName"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static SqlDataReader GetChild(string TableName, string IDName, string SuperIDName, int ID)
        {
            SqlDataReader dr = null;
            SqlParameter[] prams =
            {
                SQLDatabase.MakeInParam("@TableName",SqlDbType.VarChar,100,TableName),
                SQLDatabase.MakeInParam("@IDName",SqlDbType.VarChar,50,IDName),
                SQLDatabase.MakeInParam("@SuperIDName",SqlDbType.VarChar,50,SuperIDName),
                SQLDatabase.MakeInParam("@ID",SqlDbType.Int,4,ID)
            };

            SQLDatabase.RunProc("MCS_SYS.dbo.sp_TreeTable_GetChild", prams, out dr);
            return dr;
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
            string fullpathname = "";
            SqlDataReader dr = null;
            SqlParameter[] prams =
            {
                SQLDatabase.MakeInParam("@TableName",SqlDbType.VarChar,100,TableName),
                SQLDatabase.MakeInParam("@IDName",SqlDbType.VarChar,50,IDName),
                SQLDatabase.MakeInParam("@NameColumnName",SqlDbType.VarChar,50,NameColumnName),
                SQLDatabase.MakeInParam("@SuperIDName",SqlDbType.VarChar,50,SuperIDName),
                SQLDatabase.MakeInParam("@RootID",SqlDbType.Int,4,RootID),
                SQLDatabase.MakeInParam("@ID",SqlDbType.Int,4,ID)
            };

            SQLDatabase.RunProc("MCS_SYS.dbo.sp_TreeTable_GetFullPathName", prams, out dr);

            if (dr.Read())
            {
                fullpathname = dr[0].ToString();
            }
            dr.Close();
            return fullpathname;
        }

        /// <summary>
        /// 获取指定节点的所有从根到本节点的全路径名称
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static string GetFullPathName(string TableName, int ID)
        {
            return GetFullPathName(TableName, "ID", "Name", "SuperID", 1, ID);
        }

        /// <summary>
        /// get relation value of the special fieldname of the table
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="KeyName"></param>
        /// <param name="ValueName"></param>
        /// <returns></returns>
        public static SqlDataReader GetRelationTableSourceData(string TableName, string KeyName, string ValueName)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@TableName", SqlDbType.VarChar,50, TableName),
                SQLDatabase.MakeInParam("@KeyName", SqlDbType.VarChar,50, KeyName),
                SQLDatabase.MakeInParam("@ValueName", SqlDbType.VarChar,50, ValueName)
			};
            #endregion

            SqlDataReader dr = null;
            SQLDatabase.RunProc("MCS_SYS.dbo.sp_UD_SYS" + "_GetRelationTableSourceData", prams, out dr);
            return dr;
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
            if (KeyValue == null)
                KeyValue = "1";
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@TableName", SqlDbType.VarChar,50, TableName),
                SQLDatabase.MakeInParam("@KeyName", SqlDbType.VarChar,50, KeyName),
                SQLDatabase.MakeInParam("@ValueName", SqlDbType.VarChar,50, ValueName),
                SQLDatabase.MakeInParam("@KeyValue", SqlDbType.VarChar,50, KeyValue)
			};
            #endregion

            SqlDataReader dr = null;
            SQLDatabase.RunProc("MCS_SYS.dbo.sp_UD_SYS" + "_GetRelationTableDataValue", prams, out dr);

            string value = "";
            if (dr.Read())
            {
                value = dr.GetString(0);
            }
            dr.Close();
            return value;
        }

        public static DataTable ExecSqlString(string SqlString, int PageSize, int PageIndex, out int TotalRecordCount)
        {
            return ExecSqlString(SqlString, PageSize, PageIndex, "", out TotalRecordCount);
        }

        public static DataTable ExecSqlString(string SqlString, int PageSize, int PageIndex, string OrderFields, out int TotalRecordCount)
        {
            return ExecSqlString(null, SqlString, PageSize, PageIndex, "", out TotalRecordCount);
        }

        public static DataTable ExecSqlString(string DBConnectString, string SqlString, int PageSize, int PageIndex, string OrderFields, out int TotalRecordCount)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@SqlString", SqlDbType.VarChar,SqlString.Length, SqlString),
                SQLDatabase.MakeInParam("@PageSize", SqlDbType.Int,4, PageSize),
                SQLDatabase.MakeInParam("@PageIndex", SqlDbType.Int,4, PageIndex),
                SQLDatabase.MakeInParam("@OrderFields",SqlDbType.VarChar,200,OrderFields),
                SQLDatabase.MakeOutParam("@TotalRecordCount", SqlDbType.Int,4)
			};
            #endregion

            SqlDataReader dr = null;
            SQLDatabase.RunProc(DBConnectString, "MCS_SYS.dbo.sp_UD_SYS_ExecSqlString", prams, out dr, SQLDatabase.DefaultTimeout);
            DataTable dt = Tools.ConvertDataReaderToDataTable(dr);

            TotalRecordCount = (int)prams[4].Value;

            return dt;
        }

        public static int ExecSqlStringByTotalCount(string DBConnectString, string SqlString, out int TotalRecordCount)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@SqlString", SqlDbType.VarChar,SqlString.Length, SqlString),
                SQLDatabase.MakeOutParam("@TotalRecordCount", SqlDbType.Int,4)
			};
            #endregion

            SqlDataReader dr = null;
            int  ret =   SQLDatabase.RunProc(DBConnectString, "MCS_SYS.dbo.sp_UD_SYS_ExecSqlStringByTotalCount", prams);
            

            TotalRecordCount = (int)prams[1].Value;

            return ret;
        }
    }
}
