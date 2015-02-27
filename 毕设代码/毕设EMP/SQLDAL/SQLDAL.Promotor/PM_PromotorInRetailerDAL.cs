
// ===================================================================
// 文件： PM_PromotorInRetailerDAL.cs
// 项目名称：
// 创建时间：2009-4-29
// 作者:	   shh
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.Promotor;


namespace MCSFramework.SQLDAL.Promotor
{
	/// <summary>
	///PM_PromotorInRetailer数据访问DAL类
	/// </summary>
	public class PM_PromotorInRetailerDAL : BaseSimpleDAL<PM_PromotorInRetailer>
	{
		#region 构造函数
		///<summary>
		///
		///</summary>
		public PM_PromotorInRetailerDAL()
		{
			_ProcePrefix = "MCS_Promotor.dbo.sp_PM_PromotorInRetailer";
		}
		#endregion
		
		
		/// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(PM_PromotorInRetailer m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Promotor", SqlDbType.Int, 4, m.Promotor),
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, m.Client)
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
        public override int Update(PM_PromotorInRetailer m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@Promotor", SqlDbType.Int, 4, m.Promotor),
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, m.Client)
			};
			#endregion
			
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
			
			return ret;
        }
		
        protected override PM_PromotorInRetailer FillModel(IDataReader dr)
		{
			PM_PromotorInRetailer m = new PM_PromotorInRetailer();
			if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (int)dr["ID"];
			if (!string.IsNullOrEmpty(dr["Promotor"].ToString()))	m.Promotor = (int)dr["Promotor"];
			if (!string.IsNullOrEmpty(dr["Client"].ToString()))	m.Client = (int)dr["Client"];
						
			return m;
		}
    }
}

