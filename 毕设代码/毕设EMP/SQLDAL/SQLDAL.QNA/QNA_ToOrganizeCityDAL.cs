
// ===================================================================
// 文件： QNA_ToOrganizeCityDAL.cs
// 项目名称：
// 创建时间：2011/9/7
// 作者:	   TT
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.QNA;
using System.Collections.Generic;


namespace MCSFramework.SQLDAL.QNA
{
    /// <summary>
    ///QNA_ToOrganizeCity数据访问DAL类
    /// </summary>
    public class QNA_ToOrganizeCityDAL : BaseSimpleDAL<QNA_ToOrganizeCity>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public QNA_ToOrganizeCityDAL()
        {
            _ProcePrefix = "MCS_QNA.dbo.sp_QNA_ToOrganizeCity";
        }
        #endregion


        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(QNA_ToOrganizeCity m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ProjectID", SqlDbType.Int, 4, m.ProjectID),
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, m.OrganizeCity)
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
        public override int Update(QNA_ToOrganizeCity m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@ProjectID", SqlDbType.Int, 4, m.ProjectID),
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, m.OrganizeCity)
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override QNA_ToOrganizeCity FillModel(IDataReader dr)
        {
            QNA_ToOrganizeCity m = new QNA_ToOrganizeCity();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["ProjectID"].ToString())) m.ProjectID = (int)dr["ProjectID"];
            if (!string.IsNullOrEmpty(dr["OrganizeCity"].ToString())) m.OrganizeCity = (int)dr["OrganizeCity"];

            return m;
        }


        #region 根据片区获取问卷ID
        public  List<int> GetProjectIDByOrganizeCity(int organizeCity)
        {
            #region 设置数据集
            SqlParameter[] prams = 
            {
                 SQLDatabase.MakeInParam("@OrganizeCity",SqlDbType.Int,4,organizeCity),
            };
            #endregion
            SqlDataReader dr = null;
            SQLDatabase.RunProc(_ProcePrefix + "_GetProjectIDByOrganizeCity", prams, out dr);
            List<int> list = new List<int>();
            while (dr.Read())
                list.Add((int)dr[0]);
            return list;
        }
        #endregion

        #region 根据问卷ID获取片区
        public List<int> GetOrganizeCityByProjectID(int projectID)
        {
            #region 设置数据集
            SqlParameter[] prams = 
            {
                 SQLDatabase.MakeInParam("@ProjectID",SqlDbType.Int,4,projectID),
            };
            #endregion
            SqlDataReader dr = null;
            SQLDatabase.RunProc(_ProcePrefix + "_GetOrganizeCityByProjectID", prams, out dr);
            List<int> list = new List<int>();
            while (dr.Read())
                list.Add((int)dr[0]);
            return list;
        }
        #endregion

        #region 根据问卷ID删除所有片区
        public int DeleteByProjectID(int projectID)
        {
            #region 设置数据集
            SqlParameter[] prams = 
            {
                 SQLDatabase.MakeInParam("@ProjectID",SqlDbType.Int,4,projectID),
            };
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_DeleteByProjectID", prams);
            return ret;

        }
        #endregion

        #region 根据问卷ID删除相关一个片区
        public int DeleteOrganizeCity(int projectID, int OrganizeCity)
        {
            #region 设置数据集
            SqlParameter[] prams = 
            {
                 SQLDatabase.MakeInParam("@ProjectID",SqlDbType.Int,4,projectID),
                 SQLDatabase.MakeInParam("@OrganizeCity",SqlDbType.Int,4,OrganizeCity)
            };
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_DeleteOrganizeCity", prams);
            return ret;

        }
        #endregion
    }
}

