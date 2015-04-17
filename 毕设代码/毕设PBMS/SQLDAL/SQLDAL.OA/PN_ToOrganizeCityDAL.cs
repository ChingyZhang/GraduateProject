
// ===================================================================
// 文件： PN_ToOrganizeCityDAL.cs
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
	///PN_ToOrganizeCity数据访问DAL类
	/// </summary>
	public class PN_ToOrganizeCityDAL : BaseSimpleDAL<PN_ToOrganizeCity>
	{
		#region 构造函数
		///<summary>
		///
		///</summary>
		public PN_ToOrganizeCityDAL()
		{
			_ProcePrefix = "MCS_OA.dbo.sp_PN_ToOrganizeCity";
		}
		#endregion
		
		
		/// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(PN_ToOrganizeCity m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@NoticeID", SqlDbType.Int, 4, m.NoticeID),
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, m.OrganizeCity)
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
        public override int Update(PN_ToOrganizeCity m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@NoticeID", SqlDbType.Int, 4, m.NoticeID),
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, m.OrganizeCity)
			};
			#endregion
			
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
			
			return ret;
        }
		
        protected override PN_ToOrganizeCity FillModel(IDataReader dr)
		{
			PN_ToOrganizeCity m = new PN_ToOrganizeCity();
			if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (int)dr["ID"];
			if (!string.IsNullOrEmpty(dr["NoticeID"].ToString()))	m.NoticeID = (int)dr["NoticeID"];
			if (!string.IsNullOrEmpty(dr["OrganizeCity"].ToString()))	m.OrganizeCity = (int)dr["OrganizeCity"];
						
			return m;
		}

        #region 根据片区获取公告ID
        public List<int> GetNoticeIDByOrganizeCity(int organizeCity)
        {
            #region 设置数据集
            SqlParameter[] prams = 
            {
                 SQLDatabase.MakeInParam("@OrganizeCity",SqlDbType.Int,4,organizeCity),
            };
            #endregion
            SqlDataReader dr = null;
            SQLDatabase.RunProc(_ProcePrefix + "_GetNoticeIDByOrganizeCity", prams, out dr);
            List<int> list = new List<int>();
            while (dr.Read())
                list.Add((int)dr[0]);
            return list;
        }
        #endregion

        #region 根据公告ID获取片区
        public List<int> GetOrganizeCityByNoticeID(int noticeID)
        {
            #region 设置数据集
            SqlParameter[] prams = 
            {
                 SQLDatabase.MakeInParam("@NoticeID",SqlDbType.Int,4,noticeID),
            };
            #endregion
            SqlDataReader dr = null;
            SQLDatabase.RunProc(_ProcePrefix + "_GetOrganizeCityByNoticeID", prams, out dr);
            List<int> list = new List<int>();
            while (dr.Read())
                list.Add((int)dr[0]);
            return list;
        }
        #endregion

        #region 根据公告ID删除所有片区
        public int DeleteByNoticeID(int noticeID)
        {
            #region 设置数据集
            SqlParameter[] prams = 
            {
                 SQLDatabase.MakeInParam("@NoticeID",SqlDbType.Int,4,noticeID),
            };
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_DeleteByNoticeID", prams);
            return ret;

        }
        #endregion

        #region 根据公告ID删除相关一个片区
        public int DeleteOrganizeCity(int noticeID, int OrganizeCity)
        {
            #region 设置数据集
            SqlParameter[] prams = 
            {
                 SQLDatabase.MakeInParam("@NoticeID",SqlDbType.Int,4,noticeID),
                 SQLDatabase.MakeInParam("@OrganizeCity",SqlDbType.Int,4,OrganizeCity)
            };
            #endregion

            int ret=SQLDatabase.RunProc(_ProcePrefix + "_DeleteOrganizeCity", prams);
            return ret;
           
        }
        #endregion
    }
}

