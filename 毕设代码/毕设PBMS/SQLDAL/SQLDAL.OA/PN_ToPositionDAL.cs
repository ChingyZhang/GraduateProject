
// ===================================================================
// 文件： PN_ToPositionDAL.cs
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
using System.Collections.Generic;


namespace MCSFramework.SQLDAL.OA
{
	/// <summary>
	///PN_ToPosition数据访问DAL类
	/// </summary>
	public class PN_ToPositionDAL : BaseSimpleDAL<PN_ToPosition>
	{
		#region 构造函数
		///<summary>
		///
		///</summary>
		public PN_ToPositionDAL()
		{
			_ProcePrefix = "MCS_OA.dbo.sp_PN_ToPosition";
		}
		#endregion
		
		
		/// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(PN_ToPosition m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@NoticeID", SqlDbType.Int, 4, m.NoticeID),
				SQLDatabase.MakeInParam("@Position", SqlDbType.Int, 4, m.Position)
			};
			#endregion
			
            m.ID =  SQLDatabase.RunProc(_ProcePrefix + "_Add", prams);
			
            return m.ID;
        }
		
		/// <summary>
        /// 更新项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Update(PN_ToPosition m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@NoticeID", SqlDbType.Int, 4, m.NoticeID),
				SQLDatabase.MakeInParam("@Position", SqlDbType.Int, 4, m.Position)
			};
			#endregion
			
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
			
			return ret;
        }
		
        protected override PN_ToPosition FillModel(IDataReader dr)
		{
			PN_ToPosition m = new PN_ToPosition();
			if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (int)dr["ID"];
			if (!string.IsNullOrEmpty(dr["NoticeID"].ToString()))	m.NoticeID = (int)dr["NoticeID"];
			if (!string.IsNullOrEmpty(dr["Position"].ToString()))	m.Position = (int)dr["Position"];
						
			return m;
		}

        #region 根据公告ID获取职位
        public List<int> GetPositionByNoticeID(int noticeID)
        {
            #region 设置数据集
            SqlParameter[] prams = 
            {
                 SQLDatabase.MakeInParam("@NoticeID",SqlDbType.Int,4,noticeID),
            };
            #endregion
            SqlDataReader dr = null;
            SQLDatabase.RunProc(_ProcePrefix + "_GetPositionByNoticeID", prams, out dr);
            List<int> list = new List<int>();
            while (dr.Read())
                list.Add((int)dr[0]);
            return list;
        }
        #endregion

        #region 根据职位获取公告ID
        public List<int> GetNoticeIDByPosition(int position)
        {
            #region 设置数据集
            SqlParameter[] prams = 
            {
                 SQLDatabase.MakeInParam("@Position",SqlDbType.Int,4,position),
            };
            #endregion
            SqlDataReader dr = null;
            SQLDatabase.RunProc(_ProcePrefix + "_GetNoticeIDByPosition", prams, out dr);
            List<int> list = new List<int>();
            while (dr.Read())
                list.Add((int)dr[0]);
            return list;
        }
        #endregion

        #region 根据公告ID删除所有职位
        public int DeleteByNoticeID(int noticeID)
        {
            #region 设置数据集
            SqlParameter[] prams = 
            {
                 SQLDatabase.MakeInParam("@NoticeID",SqlDbType.Int,4,noticeID)
            };
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_DeleteByNoticeID", prams);
            return ret;

        }
        #endregion

        #region 根据公告ID删除一个职位
        public int DeletePosition(int noticeID, int position)
        {
            #region 设置数据集
            SqlParameter[] prams = 
            {
                 SQLDatabase.MakeInParam("@NoticeID",SqlDbType.Int,4,noticeID),
                 SQLDatabase.MakeInParam("@Position",SqlDbType.Int,4,position)
            };
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_DeletePosition", prams);
            return ret;

        }
        #endregion
    }
}

