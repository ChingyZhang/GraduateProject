
// ===================================================================
// 文件： PBM_OrderPayInfoDAL.cs
// 项目名称：
// 创建时间：2015-03-17
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.PBM;


namespace MCSFramework.SQLDAL.PBM
{
	/// <summary>
	///PBM_OrderPayInfo数据访问DAL类
	/// </summary>
	public class PBM_OrderPayInfoDAL : BaseSimpleDAL<PBM_OrderPayInfo>
	{
		#region 构造函数
		///<summary>
		///
		///</summary>
		public PBM_OrderPayInfoDAL()
		{
			_ProcePrefix = "MCS_PBM.dbo.sp_PBM_OrderPayInfo";
		}
		#endregion
		
		
		/// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(PBM_OrderPayInfo m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@OrderID", SqlDbType.Int, 4, m.OrderID),
				SQLDatabase.MakeInParam("@PayMode", SqlDbType.Int, 4, m.PayMode),
				SQLDatabase.MakeInParam("@Amount", SqlDbType.Decimal, 9, m.Amount),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 1000, m.Remark),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@InsertStaff", SqlDbType.Int, 4, m.InsertStaff),
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
        public override int Update(PBM_OrderPayInfo m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@OrderID", SqlDbType.Int, 4, m.OrderID),
				SQLDatabase.MakeInParam("@PayMode", SqlDbType.Int, 4, m.PayMode),
				SQLDatabase.MakeInParam("@Amount", SqlDbType.Decimal, 9, m.Amount),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 1000, m.Remark),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@UpdateStaff", SqlDbType.Int, 4, m.UpdateStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
			#endregion
			
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
			
			return ret;
        }
		
        protected override PBM_OrderPayInfo FillModel(IDataReader dr)
		{
			PBM_OrderPayInfo m = new PBM_OrderPayInfo();
			if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (int)dr["ID"];
			if (!string.IsNullOrEmpty(dr["OrderID"].ToString()))	m.OrderID = (int)dr["OrderID"];
			if (!string.IsNullOrEmpty(dr["PayMode"].ToString()))	m.PayMode = (int)dr["PayMode"];
			if (!string.IsNullOrEmpty(dr["Amount"].ToString()))	m.Amount = (decimal)dr["Amount"];
			if (!string.IsNullOrEmpty(dr["Remark"].ToString()))	m.Remark = (string)dr["Remark"];
			if (!string.IsNullOrEmpty(dr["ApproveFlag"].ToString()))	m.ApproveFlag = (int)dr["ApproveFlag"];
			if (!string.IsNullOrEmpty(dr["InsertStaff"].ToString()))	m.InsertStaff = (int)dr["InsertStaff"];
			if (!string.IsNullOrEmpty(dr["InsertTime"].ToString()))	m.InsertTime = (DateTime)dr["InsertTime"];
			if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString()))	m.UpdateTime = (DateTime)dr["UpdateTime"];
			if (!string.IsNullOrEmpty(dr["UpdateStaff"].ToString()))	m.UpdateStaff = (int)dr["UpdateStaff"];
			if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString()))	m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);
						
			return m;
		}
    }
}

