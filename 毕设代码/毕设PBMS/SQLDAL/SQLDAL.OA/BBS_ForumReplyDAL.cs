
// ===================================================================
// 文件： BBS_ForumReplyDAL.cs
// 项目名称：
// 创建时间：2009-3-16
// 作者:	   cl
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
    ///BBS_ForumReply数据访问DAL类
    /// </summary>
    public class BBS_ForumReplyDAL : BaseSimpleDAL<BBS_ForumReply>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public BBS_ForumReplyDAL()
        {
            _ProcePrefix = "MCS_OA.dbo.sp_BBS_ForumReply";
        }
        #endregion


        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(BBS_ForumReply m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Item", SqlDbType.Int, 4, m.ItemID),
				SQLDatabase.MakeInParam("@Title", SqlDbType.VarChar, 300, m.Title),
				SQLDatabase.MakeInParam("@Content", SqlDbType.Text, 0, m.Content),
				SQLDatabase.MakeInParam("@Replyer", SqlDbType.NVarChar, 256, m.Replyer),
				SQLDatabase.MakeInParam("@ReplyTime", SqlDbType.DateTime, 8, m.ReplyTime),
				SQLDatabase.MakeInParam("@IPAddress", SqlDbType.VarChar, 100, m.IPAddress),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 8000, CombineExtProperty(m.ExtPropertys,m.ModelName))
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
        public override int Update(BBS_ForumReply m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@Item", SqlDbType.Int, 4, m.ItemID),
				SQLDatabase.MakeInParam("@Title", SqlDbType.VarChar, 300, m.Title),
				SQLDatabase.MakeInParam("@Content", SqlDbType.Text, 0, m.Content),
				SQLDatabase.MakeInParam("@Replyer", SqlDbType.NVarChar, 256, m.Replyer),
				SQLDatabase.MakeInParam("@ReplyTime", SqlDbType.DateTime, 8, m.ReplyTime),
				SQLDatabase.MakeInParam("@IPAddress", SqlDbType.VarChar, 100, m.IPAddress),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 8000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override BBS_ForumReply FillModel(IDataReader dr)
        {
            BBS_ForumReply m = new BBS_ForumReply();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["Item"].ToString())) m.ItemID = (int)dr["Item"];
            if (!string.IsNullOrEmpty(dr["Title"].ToString())) m.Title = (string)dr["Title"];
            if (!string.IsNullOrEmpty(dr["Content"].ToString())) m.Content = (string)dr["Content"];
            if (!string.IsNullOrEmpty(dr["Replyer"].ToString())) m.Replyer = (string)dr["Replyer"];
            if (!string.IsNullOrEmpty(dr["ReplyTime"].ToString())) m.ReplyTime = (DateTime)dr["ReplyTime"];
            if (!string.IsNullOrEmpty(dr["IPAddress"].ToString())) m.IPAddress = (string)dr["IPAddress"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);

            return m;
        }

        public SqlDataReader GetTopReplyLatest(int Num)
        {
            SqlDataReader dr = null;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Num", SqlDbType.Int, 4,Num)
			};
            #endregion
            SQLDatabase.RunProc(_ProcePrefix + "_GetTopReplyLatest", prams, out dr);
            return dr;
        }

    }
}

