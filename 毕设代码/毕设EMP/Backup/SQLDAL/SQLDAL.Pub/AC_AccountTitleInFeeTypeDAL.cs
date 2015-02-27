
// ===================================================================
// 文件： AC_AccountTitleInFeeTypeDAL.cs
// 项目名称：
// 创建时间：2010/7/20
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.Pub;


namespace MCSFramework.SQLDAL.Pub
{
	/// <summary>
	///AC_AccountTitleInFeeType数据访问DAL类
	/// </summary>
	public class AC_AccountTitleInFeeTypeDAL : BaseSimpleDAL<AC_AccountTitleInFeeType>
	{
		#region 构造函数
		///<summary>
		///
		///</summary>
		public AC_AccountTitleInFeeTypeDAL()
		{
			_ProcePrefix = "MCS_Pub.dbo.sp_AC_AccountTitleInFeeType";
		}
		#endregion
		
		
		/// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(AC_AccountTitleInFeeType m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@AccountTitle", SqlDbType.Int, 4, m.AccountTitle),
				SQLDatabase.MakeInParam("@FeeType", SqlDbType.Int, 4, m.FeeType)
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
        public override int Update(AC_AccountTitleInFeeType m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@AccountTitle", SqlDbType.Int, 4, m.AccountTitle),
				SQLDatabase.MakeInParam("@FeeType", SqlDbType.Int, 4, m.FeeType)
			};
			#endregion
			
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
			
			return ret;
        }
		
        protected override AC_AccountTitleInFeeType FillModel(IDataReader dr)
		{
			AC_AccountTitleInFeeType m = new AC_AccountTitleInFeeType();
			if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (int)dr["ID"];
			if (!string.IsNullOrEmpty(dr["AccountTitle"].ToString()))	m.AccountTitle = (int)dr["AccountTitle"];
			if (!string.IsNullOrEmpty(dr["FeeType"].ToString()))	m.FeeType = (int)dr["FeeType"];
						
			return m;
		}
    }
}

