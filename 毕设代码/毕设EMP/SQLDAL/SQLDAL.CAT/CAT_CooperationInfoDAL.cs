
// ===================================================================
// 文件： CAT_CooperationInfoDAL.cs
// 项目名称：
// 创建时间：2011/1/20
// 作者:	   
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.CAT;


namespace MCSFramework.SQLDAL.CAT
{
	/// <summary>
	///CAT_CooperationInfo数据访问DAL类
	/// </summary>
	public class CAT_CooperationInfoDAL : BaseSimpleDAL<CAT_CooperationInfo>
	{
		#region 构造函数
		///<summary>
		///
		///</summary>
		public CAT_CooperationInfoDAL()
		{
			_ProcePrefix = "MCS_CAT.dbo.sp_CAT_CooperationInfo";
		}
		#endregion
		
		
		/// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(CAT_CooperationInfo m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Activity", SqlDbType.Int, 4, m.Activity),
				SQLDatabase.MakeInParam("@Cooperation", SqlDbType.Int, 4, m.Cooperation),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 500, m.Remark),
				SQLDatabase.MakeInParam("@People", SqlDbType.VarChar, 50, m.People),
				SQLDatabase.MakeInParam("@TeleNum", SqlDbType.VarChar, 50, m.TeleNum)
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
        public override int Update(CAT_CooperationInfo m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@Activity", SqlDbType.Int, 4, m.Activity),
				SQLDatabase.MakeInParam("@Cooperation", SqlDbType.Int, 4, m.Cooperation),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 500, m.Remark),
				SQLDatabase.MakeInParam("@People", SqlDbType.VarChar, 50, m.People),
				SQLDatabase.MakeInParam("@TeleNum", SqlDbType.VarChar, 50, m.TeleNum)
			};
			#endregion
			
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
			
			return ret;
        }
		
        protected override CAT_CooperationInfo FillModel(IDataReader dr)
		{
			CAT_CooperationInfo m = new CAT_CooperationInfo();
			if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (int)dr["ID"];
			if (!string.IsNullOrEmpty(dr["Activity"].ToString()))	m.Activity = (int)dr["Activity"];
			if (!string.IsNullOrEmpty(dr["Cooperation"].ToString()))	m.Cooperation = (int)dr["Cooperation"];
			if (!string.IsNullOrEmpty(dr["Remark"].ToString()))	m.Remark = (string)dr["Remark"];
			if (!string.IsNullOrEmpty(dr["People"].ToString()))	m.People = (string)dr["People"];
			if (!string.IsNullOrEmpty(dr["TeleNum"].ToString()))	m.TeleNum = (string)dr["TeleNum"];
						
			return m;
		}

        public void DeleteByCooperationIDS(string IDS)
        {
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@IDS",SqlDbType.VarChar, 50,  IDS)
			};
            SQLDatabase.RunProc(_ProcePrefix + "_DeleteByIDS", prams);
        }
    }
}

