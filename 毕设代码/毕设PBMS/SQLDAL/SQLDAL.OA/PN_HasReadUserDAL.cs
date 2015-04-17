
// ===================================================================
// 文件： PN_HasReadUserDAL.cs
// 项目名称：
// 创建时间：2009-3-11
// 作者:	   zhousongqin
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.OA;


namespace MCSFramework.SQLDAL.OA
{
    /// <summary>
    ///PN_HasReadUser数据访问DAL类
    /// </summary>
    public class PN_HasReadUserDAL : BaseSimpleDAL<PN_HasReadUser>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public PN_HasReadUserDAL()
        {
            _ProcePrefix = "MCS_OA.dbo.sp_PN_HasReadUser";
        }
        #endregion


        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(PN_HasReadUser m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Notice", SqlDbType.Int, 4, m.Notice),
				SQLDatabase.MakeInParam("@Username", SqlDbType.VarChar, 200, m.Username),
				SQLDatabase.MakeInParam("@ReadTime", SqlDbType.DateTime, 8, m.ReadTime),
				SQLDatabase.MakeInParam("@ReadInfo", SqlDbType.VarChar, 200, m.ReadInfo),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            m.ID = SQLDatabase.RunProc(_ProcePrefix + "_Add", prams);

            return m.ID;
        }

        /// <summary>
        /// 更新项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Update(PN_HasReadUser m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@Notice", SqlDbType.Int, 4, m.Notice),
				SQLDatabase.MakeInParam("@Username", SqlDbType.VarChar, 200, m.Username),
				SQLDatabase.MakeInParam("@ReadTime", SqlDbType.DateTime, 8, m.ReadTime),
				SQLDatabase.MakeInParam("@ReadInfo", SqlDbType.VarChar, 200, m.ReadInfo),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override PN_HasReadUser FillModel(IDataReader dr)
        {
            PN_HasReadUser m = new PN_HasReadUser();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["Notice"].ToString())) m.Notice = (int)dr["Notice"];
            if (!string.IsNullOrEmpty(dr["Username"].ToString())) m.Username = (string)dr["Username"];
            if (!string.IsNullOrEmpty(dr["ReadTime"].ToString())) m.ReadTime = (DateTime)dr["ReadTime"];
            if (!string.IsNullOrEmpty(dr["ReadInfo"].ToString())) m.ReadInfo = (string)dr["ReadInfo"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);

            return m;
        }

        #region 根据公告ID获得阅读人数
        public int GetReadCountByNotice(int notice)
        {
            #region 设置数据集
            SqlParameter[] prams = 
            {
                 SQLDatabase.MakeInParam("@Notice",SqlDbType.Int,4,notice),
            };
            #endregion
            SqlDataReader dr = null;
            SQLDatabase.RunProc(_ProcePrefix + "_GetReadCountByNotice", prams, out dr);
            int ret = 0;
            if (dr.Read())
                ret = (int)dr[0];
            dr.Close();
            return ret;
        }
        #endregion
    }
}

