
// ===================================================================
// 文件： FNA_FeeWriteOffDetail_AdjustInfoDAL.cs
// 项目名称：
// 创建时间：2013-01-26
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.FNA;


namespace MCSFramework.SQLDAL.FNA
{
	/// <summary>
	///FNA_FeeWriteOffDetail_AdjustInfo数据访问DAL类
	/// </summary>
	public class FNA_FeeWriteOffDetail_AdjustInfoDAL : BaseSimpleDAL<FNA_FeeWriteOffDetail_AdjustInfo>
	{
		#region 构造函数
		///<summary>
		///
		///</summary>
		public FNA_FeeWriteOffDetail_AdjustInfoDAL()
		{
			_ProcePrefix = "MCS_FNA.dbo.sp_FNA_FeeWriteOffDetail_AdjustInfo";
		}
		#endregion
		
		
		/// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(FNA_FeeWriteOffDetail_AdjustInfo m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@WriteOffDetailID", SqlDbType.Int, 4, m.WriteOffDetailID),
				SQLDatabase.MakeInParam("@AdjustMode", SqlDbType.Int, 4, m.AdjustMode),
				SQLDatabase.MakeInParam("@AdjustCost", SqlDbType.Decimal, 9, m.AdjustCost),
				SQLDatabase.MakeInParam("@AdjustReason", SqlDbType.VarChar, 500, m.AdjustReason),
				SQLDatabase.MakeInParam("@InsertStaff", SqlDbType.Int, 4, m.InsertStaff),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 2000, m.Remark),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
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
        public override int Update(FNA_FeeWriteOffDetail_AdjustInfo m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@WriteOffDetailID", SqlDbType.Int, 4, m.WriteOffDetailID),
				SQLDatabase.MakeInParam("@AdjustMode", SqlDbType.Int, 4, m.AdjustMode),
				SQLDatabase.MakeInParam("@AdjustCost", SqlDbType.Decimal, 9, m.AdjustCost),
				SQLDatabase.MakeInParam("@AdjustReason", SqlDbType.VarChar, 500, m.AdjustReason),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 2000, m.Remark),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
			#endregion
			
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
			
			return ret;
        }
		
        protected override FNA_FeeWriteOffDetail_AdjustInfo FillModel(IDataReader dr)
		{
			FNA_FeeWriteOffDetail_AdjustInfo m = new FNA_FeeWriteOffDetail_AdjustInfo();
			if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (int)dr["ID"];
			if (!string.IsNullOrEmpty(dr["WriteOffDetailID"].ToString()))	m.WriteOffDetailID = (int)dr["WriteOffDetailID"];
			if (!string.IsNullOrEmpty(dr["AdjustMode"].ToString()))	m.AdjustMode = (int)dr["AdjustMode"];
			if (!string.IsNullOrEmpty(dr["AdjustCost"].ToString()))	m.AdjustCost = (decimal)dr["AdjustCost"];
			if (!string.IsNullOrEmpty(dr["AdjustReason"].ToString()))	m.AdjustReason = (string)dr["AdjustReason"];
			if (!string.IsNullOrEmpty(dr["InsertStaff"].ToString()))	m.InsertStaff = (int)dr["InsertStaff"];
			if (!string.IsNullOrEmpty(dr["InsertTime"].ToString()))	m.InsertTime = (DateTime)dr["InsertTime"];
			if (!string.IsNullOrEmpty(dr["Remark"].ToString()))	m.Remark = (string)dr["Remark"];
			if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString()))	m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);
						
			return m;
		}
    }
}

