
// ===================================================================
// 文件： INV_ReportedLossDAL.cs
// 项目名称：
// 创建时间：2012-7-23
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.EBM;


namespace MCSFramework.SQLDAL.EBM
{
	/// <summary>
	///INV_ReportedLoss数据访问DAL类
	/// </summary>
	public class INV_ReportedLossDAL : BaseSimpleDAL<INV_ReportedLoss>
	{
		#region 构造函数
		///<summary>
		///
		///</summary>
		public INV_ReportedLossDAL()
		{
			_ProcePrefix = "MCS_EBM.dbo.sp_INV_ReportedLoss";
		}
		#endregion

        #region 基本操作
        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(INV_ReportedLoss m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@SheetCode", SqlDbType.VarChar, 100, m.SheetCode),
				SQLDatabase.MakeInParam("@WareHouse", SqlDbType.Int, 4, m.WareHouse),
				SQLDatabase.MakeInParam("@RelateDeliveryID", SqlDbType.Int, 4, m.RelateDeliveryID),
				SQLDatabase.MakeInParam("@Classify", SqlDbType.Int, 4, m.Classify),
				SQLDatabase.MakeInParam("@Reason", SqlDbType.VarChar, 500, m.Reason),
				SQLDatabase.MakeInParam("@ConfirmTime", SqlDbType.DateTime, 8, m.ConfirmTime),
				SQLDatabase.MakeInParam("@ConfirmUser", SqlDbType.UniqueIdentifier, 16, m.ConfirmUser),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 2000, m.Remark),
				SQLDatabase.MakeInParam("@ApproveTask", SqlDbType.Int, 4, m.ApproveTask),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@InsertUser", SqlDbType.UniqueIdentifier, 16, m.InsertUser),
				SQLDatabase.MakeInParam("@UpdateUser", SqlDbType.UniqueIdentifier, 16, m.UpdateUser),
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
        public override int Update(INV_ReportedLoss m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@SheetCode", SqlDbType.VarChar, 100, m.SheetCode),
				SQLDatabase.MakeInParam("@WareHouse", SqlDbType.Int, 4, m.WareHouse),
				SQLDatabase.MakeInParam("@RelateDeliveryID", SqlDbType.Int, 4, m.RelateDeliveryID),
				SQLDatabase.MakeInParam("@Classify", SqlDbType.Int, 4, m.Classify),
				SQLDatabase.MakeInParam("@Reason", SqlDbType.VarChar, 500, m.Reason),
				SQLDatabase.MakeInParam("@ConfirmTime", SqlDbType.DateTime, 8, m.ConfirmTime),
				SQLDatabase.MakeInParam("@ConfirmUser", SqlDbType.UniqueIdentifier, 16, m.ConfirmUser),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 2000, m.Remark),
				SQLDatabase.MakeInParam("@ApproveTask", SqlDbType.Int, 4, m.ApproveTask),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@InsertUser", SqlDbType.UniqueIdentifier, 16, m.InsertUser),
				SQLDatabase.MakeInParam("@UpdateUser", SqlDbType.UniqueIdentifier, 16, m.UpdateUser),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
			#endregion
			
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
			
			return ret;
        }
		
        protected override INV_ReportedLoss FillModel(IDataReader dr)
		{
			INV_ReportedLoss m = new INV_ReportedLoss();
			if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (int)dr["ID"];
			if (!string.IsNullOrEmpty(dr["SheetCode"].ToString()))	m.SheetCode = (string)dr["SheetCode"];
			if (!string.IsNullOrEmpty(dr["WareHouse"].ToString()))	m.WareHouse = (int)dr["WareHouse"];
			if (!string.IsNullOrEmpty(dr["RelateDeliveryID"].ToString()))	m.RelateDeliveryID = (int)dr["RelateDeliveryID"];
			if (!string.IsNullOrEmpty(dr["Classify"].ToString()))	m.Classify = (int)dr["Classify"];
			if (!string.IsNullOrEmpty(dr["Reason"].ToString()))	m.Reason = (string)dr["Reason"];
			if (!string.IsNullOrEmpty(dr["ConfirmTime"].ToString()))	m.ConfirmTime = (DateTime)dr["ConfirmTime"];
			if (!string.IsNullOrEmpty(dr["ConfirmUser"].ToString()))	m.ConfirmUser = (Guid)dr["ConfirmUser"];
			if (!string.IsNullOrEmpty(dr["Remark"].ToString()))	m.Remark = (string)dr["Remark"];
			if (!string.IsNullOrEmpty(dr["ApproveTask"].ToString()))	m.ApproveTask = (int)dr["ApproveTask"];
			if (!string.IsNullOrEmpty(dr["ApproveFlag"].ToString()))	m.ApproveFlag = (int)dr["ApproveFlag"];
			if (!string.IsNullOrEmpty(dr["InsertTime"].ToString()))	m.InsertTime = (DateTime)dr["InsertTime"];
			if (!string.IsNullOrEmpty(dr["InsertUser"].ToString()))	m.InsertUser = (Guid)dr["InsertUser"];
			if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString()))	m.UpdateTime = (DateTime)dr["UpdateTime"];
			if (!string.IsNullOrEmpty(dr["UpdateUser"].ToString()))	m.UpdateUser = (Guid)dr["UpdateUser"];
			if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString()))	m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);
						
			return m;
        }

        #endregion

        #region 生成报损单号
        public string GenerateSheetCode(int WareHouse)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@WareHouse", SqlDbType.Int, 4, WareHouse),
				SQLDatabase.MakeOutParam("@SheetCode", SqlDbType.VarChar, 100)                
			};
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_GenerateSheetCode", prams);

            if (prams[1].Value != DBNull.Value)
                return prams[1].Value.ToString();
            else
                return "";
        }
        #endregion

        #region 确认审核报损单
        public int Approve(int ID, Guid User)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, ID),
				SQLDatabase.MakeInParam("@User", SqlDbType.UniqueIdentifier, 16, User)                
			};
            #endregion

            return SQLDatabase.RunProc(_ProcePrefix + "_Approve", prams);
        }
        #endregion

        #region 逐码扫描产品
        /// <summary>
        /// 逐码扫描新增报损产品(按物流码)
        /// </summary>
        /// <param name="LossID"></param>
        /// <param name="Code"></param>
        /// <returns></returns>
        public int LossByOneCode(int LossID, string Code)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@LossID", SqlDbType.Int, 4, LossID),
				SQLDatabase.MakeInParam("@Code", SqlDbType.VarChar, 50, Code)
			};
            #endregion

            return SQLDatabase.RunProc(_ProcePrefix + "_LossByOneCode", prams);
        }
        #endregion
    }
}

