
// ===================================================================
// 文件： PBM_DeliveryPayInfoDAL.cs
// 项目名称：
// 创建时间：2015-03-15
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
	///PBM_DeliveryPayInfo数据访问DAL类
	/// </summary>
	public class PBM_DeliveryPayInfoDAL : BaseSimpleDAL<PBM_DeliveryPayInfo>
	{
		#region 构造函数
		///<summary>
		///
		///</summary>
		public PBM_DeliveryPayInfoDAL()
		{
			_ProcePrefix = "MCS_PBM.dbo.sp_PBM_DeliveryPayInfo";
		}
		#endregion
		
		
		/// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(PBM_DeliveryPayInfo m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@DeliveryID", SqlDbType.Int, 4, m.DeliveryID),
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
        public override int Update(PBM_DeliveryPayInfo m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@DeliveryID", SqlDbType.Int, 4, m.DeliveryID),
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
		
        protected override PBM_DeliveryPayInfo FillModel(IDataReader dr)
		{
			PBM_DeliveryPayInfo m = new PBM_DeliveryPayInfo();
			if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (int)dr["ID"];
			if (!string.IsNullOrEmpty(dr["DeliveryID"].ToString()))	m.DeliveryID = (int)dr["DeliveryID"];
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

