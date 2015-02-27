
// ===================================================================
// 文件： QNA_ToPositionDAL.cs
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
    ///QNA_ToPosition数据访问DAL类
    /// </summary>
    public class QNA_ToPositionDAL : BaseSimpleDAL<QNA_ToPosition>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public QNA_ToPositionDAL()
        {
            _ProcePrefix = "MCS_QNA.dbo.sp_QNA_ToPosition";
        }
        #endregion


        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(QNA_ToPosition m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ProjectID", SqlDbType.Int, 4, m.ProjectID),
				SQLDatabase.MakeInParam("@Position", SqlDbType.Int, 4, m.Position)
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
        public override int Update(QNA_ToPosition m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@ProjectID", SqlDbType.Int, 4, m.ProjectID),
				SQLDatabase.MakeInParam("@Position", SqlDbType.Int, 4, m.Position)
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override QNA_ToPosition FillModel(IDataReader dr)
        {
            QNA_ToPosition m = new QNA_ToPosition();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["ProjectID"].ToString())) m.ProjectID = (int)dr["ProjectID"];
            if (!string.IsNullOrEmpty(dr["Position"].ToString())) m.Position = (int)dr["Position"];

            return m;
        }


        #region 根据问卷ID获取职位
        public List<int> GetPositionByProjectID(int projectID)
        {
            #region 设置数据集
            SqlParameter[] prams = 
            {
                 SQLDatabase.MakeInParam("@ProjectID",SqlDbType.Int,4,projectID),
            };
            #endregion
            SqlDataReader dr = null;
            SQLDatabase.RunProc(_ProcePrefix + "_GetPositionByProjectID", prams, out dr);
            List<int> list = new List<int>();
            while (dr.Read())
                list.Add((int)dr[0]);
            return list;
        }
        #endregion

        #region 根据职位获取问卷ID
        public List<int> GetProjectIDByPosition(int position)
        {
            #region 设置数据集
            SqlParameter[] prams = 
            {
                 SQLDatabase.MakeInParam("@Position",SqlDbType.Int,4,position),
            };
            #endregion
            SqlDataReader dr = null;
            SQLDatabase.RunProc(_ProcePrefix + "_GetProjectIDByPosition", prams, out dr);
            List<int> list = new List<int>();
            while (dr.Read())
                list.Add((int)dr[0]);
            return list;
        }
        #endregion

        #region 根据问卷ID删除所有职位
        public int DeleteByProjectID(int projectID)
        {
            #region 设置数据集
            SqlParameter[] prams = 
            {
                 SQLDatabase.MakeInParam("@ProjectID",SqlDbType.Int,4,projectID)
            };
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_DeleteByProjectID", prams);
            return ret;

        }
        #endregion

        #region 根据问卷ID删除一个职位
        public int DeletePosition(int projectID, int position)
        {
            #region 设置数据集
            SqlParameter[] prams = 
            {
                 SQLDatabase.MakeInParam("@ProjectID",SqlDbType.Int,4,projectID),
                 SQLDatabase.MakeInParam("@Position",SqlDbType.Int,4,position)
            };
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_DeletePosition", prams);
            return ret;

        }
        #endregion
    }
}

