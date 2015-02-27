
// ===================================================================
// 文件： SVM_InventoryDifferencesDAL.cs
// 项目名称：
// 创建时间：2011/12/14
// 作者:	   chf
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.SVM;


namespace MCSFramework.SQLDAL.SVM
{
	/// <summary>
	///SVM_InventoryDifferences数据访问DAL类
	/// </summary>
	public class SVM_InventoryDifferencesDAL : BaseComplexDAL<SVM_InventoryDifferences,SVM_InventoryDifferences_Detail>
	{
		#region 构造函数
		///<summary>
		///
		///</summary>
		public SVM_InventoryDifferencesDAL()
		{
			_ProcePrefix = "MCS_SVM.dbo.sp_SVM_InventoryDifferences";
		}
		#endregion
		
		
		/// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(SVM_InventoryDifferences m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, m.Client),
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, m.OrganizeCity),
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, m.AccountMonth),
				SQLDatabase.MakeInParam("@InventoryDate", SqlDbType.DateTime, 8, m.InventoryDate),
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
        public override int Update(SVM_InventoryDifferences m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, m.Client),
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, m.OrganizeCity),
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, m.AccountMonth),
				SQLDatabase.MakeInParam("@InventoryDate", SqlDbType.DateTime, 8, m.InventoryDate),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@UpdateStaff", SqlDbType.Int, 4, m.UpdateStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
			#endregion
			
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
			
			return ret;
        }
		
        protected override SVM_InventoryDifferences FillModel(IDataReader dr)
		{
			SVM_InventoryDifferences m = new SVM_InventoryDifferences();
			if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (int)dr["ID"];
			if (!string.IsNullOrEmpty(dr["Client"].ToString()))	m.Client = (int)dr["Client"];
			if (!string.IsNullOrEmpty(dr["OrganizeCity"].ToString()))	m.OrganizeCity = (int)dr["OrganizeCity"];
			if (!string.IsNullOrEmpty(dr["AccountMonth"].ToString()))	m.AccountMonth = (int)dr["AccountMonth"];
			if (!string.IsNullOrEmpty(dr["InventoryDate"].ToString()))	m.InventoryDate = (DateTime)dr["InventoryDate"];
			if (!string.IsNullOrEmpty(dr["ApproveFlag"].ToString()))	m.ApproveFlag = (int)dr["ApproveFlag"];
			if (!string.IsNullOrEmpty(dr["InsertTime"].ToString()))	m.InsertTime = (DateTime)dr["InsertTime"];
			if (!string.IsNullOrEmpty(dr["InsertStaff"].ToString()))	m.InsertStaff = (int)dr["InsertStaff"];
			if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString()))	m.UpdateTime = (DateTime)dr["UpdateTime"];
			if (!string.IsNullOrEmpty(dr["UpdateStaff"].ToString()))	m.UpdateStaff = (int)dr["UpdateStaff"];
			if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString()))	m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);
						
			return m;
		}
		
		public override int AddDetail(SVM_InventoryDifferences_Detail m)
        {
			m.InventoryID = HeadID;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@InventoryID", SqlDbType.Int, 4, m.InventoryID),
				SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, m.Product),
				SQLDatabase.MakeInParam("@LotNumber", SqlDbType.VarChar, 50, m.LotNumber),
				SQLDatabase.MakeInParam("@Quantity", SqlDbType.Int, 4, m.Quantity),
				SQLDatabase.MakeInParam("@FactoryPrice", SqlDbType.Decimal, 9, m.FactoryPrice),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 2000, m.Remark)
			};
            #endregion

            m.ID = SQLDatabase.RunProc(_ProcePrefix + "_AddDetail", prams);

            return m.ID;
        }

        public override int UpdateDetail(SVM_InventoryDifferences_Detail m)
        {
            m.InventoryID = HeadID;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@InventoryID", SqlDbType.Int, 4, m.InventoryID),
				SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, m.Product),
				SQLDatabase.MakeInParam("@LotNumber", SqlDbType.VarChar, 50, m.LotNumber),
				SQLDatabase.MakeInParam("@Quantity", SqlDbType.Int, 4, m.Quantity),
				SQLDatabase.MakeInParam("@FactoryPrice", SqlDbType.Decimal, 9, m.FactoryPrice),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 2000, m.Remark)
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix+"_UpdateDetail", prams);

            return ret;
        }

        protected override SVM_InventoryDifferences_Detail FillDetailModel(IDataReader dr)
        {
            SVM_InventoryDifferences_Detail m = new SVM_InventoryDifferences_Detail();
            if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (int)dr["ID"];
			if (!string.IsNullOrEmpty(dr["InventoryID"].ToString()))	m.InventoryID = (int)dr["InventoryID"];
			if (!string.IsNullOrEmpty(dr["Product"].ToString()))	m.Product = (int)dr["Product"];
			if (!string.IsNullOrEmpty(dr["LotNumber"].ToString()))	m.LotNumber = (string)dr["LotNumber"];
			if (!string.IsNullOrEmpty(dr["Quantity"].ToString()))	m.Quantity = (int)dr["Quantity"];
			if (!string.IsNullOrEmpty(dr["FactoryPrice"].ToString()))	m.FactoryPrice = (decimal)dr["FactoryPrice"];
			if (!string.IsNullOrEmpty(dr["Remark"].ToString()))	m.Remark = (string)dr["Remark"];
					

            return m;
        }

        /// <summary>
        /// 获取按出厂价计算的总销售额，不含DDF
        /// </summary>
        /// <returns></returns>
        public decimal GetTotalFactoryPriceValue()
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, HeadID),
                 new SqlParameter("@TotalValue", SqlDbType.Decimal,9, ParameterDirection.Output,false,18,3,"TotalValue", DataRowVersion.Current,0)
			};
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_GetTotalFactoryPriceValue", prams);

            return (decimal)prams[1].Value;
        }

        public void Approve(int StaffID)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, HeadID),
				SQLDatabase.MakeInParam("@StaffID", SqlDbType.Int, 4, StaffID)
			};
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_Approve", prams);
        }

        public void Cancel_Approve(int StaffID)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, HeadID),
				SQLDatabase.MakeInParam("@StaffID", SqlDbType.Int, 4, StaffID)
			};
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_Cancel_Approve", prams);
        }

        #region 静态方法
        /// <summary>
        /// 初始化客户库存产品列表
        /// </summary>
        /// <param name="Month"></param>
        /// <param name="ClientID"></param>
        /// <param name="Staff"></param>
        /// <returns></returns>
        public int InitProductList(int Month, int ClientID, DateTime InventoryDate, int Staff, bool IsCXP)
        {
            #region	设置参数集
            SqlParameter[] prams = {
                SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4,Month),
                SQLDatabase.MakeInParam("@ClientID", SqlDbType.Int, 4,ClientID),
                SQLDatabase.MakeInParam("@InventoryDate", SqlDbType.DateTime, 8,InventoryDate),
                SQLDatabase.MakeInParam("@StaffID", SqlDbType.Int, 4,Staff),
                SQLDatabase.MakeInParam("@IsCXP", SqlDbType.Int, 4,IsCXP ? 1 : 0)
			};
            #endregion

            return SQLDatabase.RunProc(_ProcePrefix + "_InitProductList", prams);
        }


        public SqlDataReader GetSummary(int OrganizeCity, int ClientID, int beginMonth, int endMonth, int ClientType)
        {
            #region	设置参数集
            SqlParameter[] prams = {
                SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4,OrganizeCity),
				SQLDatabase.MakeInParam("@ClientID", SqlDbType.Int, 4,ClientID),
              	SQLDatabase.MakeInParam("@beginMonth", SqlDbType.Int, 4,beginMonth),
                SQLDatabase.MakeInParam("@endMonth", SqlDbType.Int, 4,endMonth),
                SQLDatabase.MakeInParam("@ClientType", SqlDbType.Int, 4,ClientType)
			};
            #endregion

            SqlDataReader dr = null;
            SQLDatabase.RunProc(_ProcePrefix + "_GetSummary", prams, out dr);
            return dr;
        }
        public decimal GetOPIInventory(int Month, int ClientID)
        {
            #region	设置参数集
            SqlParameter[] prams = {
                SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4,Month),
                SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4,ClientID),
                SQLDatabase.MakeOutParam("@Invetory",SqlDbType.Decimal,9)
			};
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_GetOPIInventory", prams);
            return (decimal)prams[2].Value;
        }
        #endregion
    }
}

