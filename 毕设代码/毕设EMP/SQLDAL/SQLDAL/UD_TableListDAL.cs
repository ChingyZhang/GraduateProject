
// ===================================================================
// 文件： UD_TableListDAL.cs
// 项目名称：
// 创建时间：2008-11-25
// 作者:	   
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
//using MCSFramework.Common;
using MCSFramework.DBUtility;
using MCSFramework.Model;


namespace MCSFramework.SQLDAL
{
    /// <summary>
    ///UD_TableList数据访问DAL类
    /// </summary>
    public class UD_TableListDAL : BaseSimpleDAL<UD_TableList>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public UD_TableListDAL()
        {
            _ProcePrefix = "MCS_SYS.dbo.sp_UD_TableList";
        }
        #endregion

        #region 成员方法
        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(UD_TableList m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.UniqueIdentifier, 16, m.ID),
				SQLDatabase.MakeInParam("@Name", SqlDbType.VarChar, 100, m.Name),
				SQLDatabase.MakeInParam("@DisplayName", SqlDbType.VarChar, 50, m.DisplayName),
				SQLDatabase.MakeInParam("@ExtFlag", SqlDbType.Char, 1, m.ExtFlag),
                SQLDatabase.MakeInParam("@ModelName", SqlDbType.VarChar,50, m.ModelClassName),
                SQLDatabase.MakeInParam("@TreeFlag", SqlDbType.Char, 1, m.TreeFlag),
			};
            #endregion

            return SQLDatabase.RunProc(_ProcePrefix + "_Add", prams);
        }

        /// <summary>
        /// 更新项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Update(UD_TableList m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.UniqueIdentifier, 16, m.ID),
				SQLDatabase.MakeInParam("@Name", SqlDbType.VarChar, 100, m.Name),
				SQLDatabase.MakeInParam("@DisplayName", SqlDbType.VarChar, 50, m.DisplayName),
				SQLDatabase.MakeInParam("@ExtFlag", SqlDbType.Char, 1, m.ExtFlag),
                SQLDatabase.MakeInParam("@ModelName", SqlDbType.VarChar,50, m.ModelClassName),
                SQLDatabase.MakeInParam("@TreeFlag", SqlDbType.Char, 1, m.TreeFlag),
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
            return ret;
        }

        protected override UD_TableList FillModel(IDataReader dr)
        {
            UD_TableList m = new UD_TableList();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (Guid)dr["ID"];
            if (!string.IsNullOrEmpty(dr["Name"].ToString())) m.Name = (string)dr["Name"];
            if (!string.IsNullOrEmpty(dr["DisplayName"].ToString())) m.DisplayName = (string)dr["DisplayName"];
            if (!string.IsNullOrEmpty(dr["ExtFlag"].ToString())) m.ExtFlag = (string)dr["ExtFlag"];
            if (!string.IsNullOrEmpty(dr["ModelName"].ToString())) m.ModelClassName = (string)dr["ModelName"];
            if (!string.IsNullOrEmpty(dr["TreeFlag"].ToString())) m.TreeFlag = (string)dr["TreeFlag"];
            return m;
        }

        public override UD_TableList GetModel(string name)
        {
            SqlParameter[] parameters = {
					SQLDatabase.MakeInParam("@Name", SqlDbType.VarChar, 100, name)				};
            SqlDataReader dr = null;
            SQLDatabase.RunProc(_ProcePrefix + "_GetModelByName", parameters, out dr);

            UD_TableList m = default(UD_TableList);
            if (dr.Read())
            {
                m = FillModel(dr);
            }
            dr.Close();

            return m;
        }
        #endregion
    }
}

