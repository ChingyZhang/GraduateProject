
// ===================================================================
// 文件： PN_HasReadStaffDAL.cs
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
	///PN_HasReadStaff数据访问DAL类
	/// </summary>
	public class PN_HasReadStaffDAL : BaseSimpleDAL<PN_HasReadStaff>
	{
		#region 构造函数
		///<summary>
		///
		///</summary>
		public PN_HasReadStaffDAL()
		{
			_ProcePrefix = "MCS_OA.dbo.sp_PN_HasReadStaff";
		}
		#endregion
		
		
		/// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(PN_HasReadStaff m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Notice", SqlDbType.Int, 4, m.Notice),
				SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4, m.Staff),
				SQLDatabase.MakeInParam("@ReadTime", SqlDbType.DateTime, 8, m.ReadTime)
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
        public override int Update(PN_HasReadStaff m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@Notice", SqlDbType.Int, 4, m.Notice),
				SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4, m.Staff),
				SQLDatabase.MakeInParam("@ReadTime", SqlDbType.DateTime, 8, m.ReadTime)
			};
			#endregion
			
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
			
			return ret;
        }
		
        protected override PN_HasReadStaff FillModel(IDataReader dr)
		{
			PN_HasReadStaff m = new PN_HasReadStaff();
			if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (int)dr["ID"];
			if (!string.IsNullOrEmpty(dr["Notice"].ToString()))	m.Notice = (int)dr["Notice"];
			if (!string.IsNullOrEmpty(dr["Staff"].ToString()))	m.Staff = (int)dr["Staff"];
			if (!string.IsNullOrEmpty(dr["ReadTime"].ToString()))	m.ReadTime = (DateTime)dr["ReadTime"];
						
			return m;
		}

        #region 根据邮件ID获得阅读人数
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

