
// ===================================================================
// 文件： PDT_StandardPriceDAL.cs
// 项目名称：
// 创建时间：2015/1/27
// 作者:	   lv
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
	///PDT_StandardPrice数据访问DAL类
	/// </summary>
	public class PDT_StandardPriceDAL : BaseComplexDAL<PDT_StandardPrice,PDT_StandardPrice_Detail>
	{
		#region 构造函数
		///<summary>
		///
		///</summary>
		public PDT_StandardPriceDAL()
		{
			_ProcePrefix = "MCS_PUB.dbo.sp_PDT_StandardPrice";
		}
		#endregion
		
		
		/// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(PDT_StandardPrice m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Name", SqlDbType.VarChar, 100, m.Name),
				SQLDatabase.MakeInParam("@Supplier", SqlDbType.Int, 4, m.Supplier),
				SQLDatabase.MakeInParam("@FitSalesArea", SqlDbType.Int, 4, m.FitSalesArea),
				SQLDatabase.MakeInParam("@FitRTChannel", SqlDbType.Int, 4, m.FitRTChannel),
                SQLDatabase.MakeInParam("@IsDefault", SqlDbType.Char, 1, m.IsDefault),
				SQLDatabase.MakeInParam("@IsEnabled", SqlDbType.Char, 1, m.IsEnabled),
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
        public override int Update(PDT_StandardPrice m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@Name", SqlDbType.VarChar, 100, m.Name),
				SQLDatabase.MakeInParam("@Supplier", SqlDbType.Int, 4, m.Supplier),
				SQLDatabase.MakeInParam("@FitSalesArea", SqlDbType.Int, 4, m.FitSalesArea),
				SQLDatabase.MakeInParam("@FitRTChannel", SqlDbType.Int, 4, m.FitRTChannel),
                SQLDatabase.MakeInParam("@IsDefault", SqlDbType.Char, 1, m.IsDefault),
				SQLDatabase.MakeInParam("@IsEnabled", SqlDbType.Char, 1, m.IsEnabled),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 1000, m.Remark),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@UpdateStaff", SqlDbType.Int, 4, m.UpdateStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
			#endregion
			
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
			
			return ret;
        }
		
        protected override PDT_StandardPrice FillModel(IDataReader dr)
		{
			PDT_StandardPrice m = new PDT_StandardPrice();
			if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (int)dr["ID"];
			if (!string.IsNullOrEmpty(dr["Name"].ToString()))	m.Name = (string)dr["Name"];
			if (!string.IsNullOrEmpty(dr["Supplier"].ToString()))	m.Supplier = (int)dr["Supplier"];
			if (!string.IsNullOrEmpty(dr["FitSalesArea"].ToString()))	m.FitSalesArea = (int)dr["FitSalesArea"];
			if (!string.IsNullOrEmpty(dr["FitRTChannel"].ToString()))	m.FitRTChannel = (int)dr["FitRTChannel"];
            if (!string.IsNullOrEmpty(dr["IsDefault"].ToString())) m.IsDefault = (string)dr["IsDefault"];
            if (!string.IsNullOrEmpty(dr["IsEnabled"].ToString())) m.IsEnabled = (string)dr["IsEnabled"];
			if (!string.IsNullOrEmpty(dr["Remark"].ToString()))	m.Remark = (string)dr["Remark"];
			if (!string.IsNullOrEmpty(dr["ApproveFlag"].ToString()))	m.ApproveFlag = (int)dr["ApproveFlag"];
			if (!string.IsNullOrEmpty(dr["InsertTime"].ToString()))	m.InsertTime = (DateTime)dr["InsertTime"];
			if (!string.IsNullOrEmpty(dr["InsertStaff"].ToString()))	m.InsertStaff = (int)dr["InsertStaff"];
			if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString()))	m.UpdateTime = (DateTime)dr["UpdateTime"];
			if (!string.IsNullOrEmpty(dr["UpdateStaff"].ToString()))	m.UpdateStaff = (int)dr["UpdateStaff"];
			if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString()))	m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);
						
			return m;
		}
		
		public override int AddDetail(PDT_StandardPrice_Detail m)
        {
			m.PriceID = HeadID;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@PriceID", SqlDbType.Int, 4, m.PriceID),
				SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, m.Product),
				SQLDatabase.MakeInParam("@Price", SqlDbType.Decimal, 9, m.Price),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 1000, m.Remark),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            m.ID = SQLDatabase.RunProc(_ProcePrefix + "_AddDetail", prams);

            return m.ID;
        }

        public override int UpdateDetail(PDT_StandardPrice_Detail m)
        {
            m.PriceID = HeadID;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@PriceID", SqlDbType.Int, 4, m.PriceID),
				SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, m.Product),
				SQLDatabase.MakeInParam("@Price", SqlDbType.Decimal, 9, m.Price),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 1000, m.Remark),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix+"_UpdateDetail", prams);

            return ret;
        }

        protected override PDT_StandardPrice_Detail FillDetailModel(IDataReader dr)
        {
            PDT_StandardPrice_Detail m = new PDT_StandardPrice_Detail();
            if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (int)dr["ID"];
			if (!string.IsNullOrEmpty(dr["PriceID"].ToString()))	m.PriceID = (int)dr["PriceID"];
			if (!string.IsNullOrEmpty(dr["Product"].ToString()))	m.Product = (int)dr["Product"];
			if (!string.IsNullOrEmpty(dr["Price"].ToString()))	m.Price = (decimal)dr["Price"];
			if (!string.IsNullOrEmpty(dr["Remark"].ToString()))	m.Remark = (string)dr["Remark"];
			if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString()))	m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);
					

            return m;
        }

        #region 创建经销商的默认价表
        /// <summary>
        /// 创建经销商的默认价表
        /// </summary>
        /// <param name="Supplier">供货商</param>
        /// <returns>创建的默认价表ID，如果已有默认价表，则返回默认价表ID</returns>
        public int CreateDefaultPrice(int Supplier)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Supplier", SqlDbType.Int, 4, Supplier)
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_CreateDefaultPrice", prams);

            return ret;
        }
        #endregion

        #region 获取经销商适用价表
        /// <summary>
        /// 获取经销商指定渠道及区域的适用价表
        /// </summary>
        /// <param name="Supplier">供货商</param>
        /// <param name="FitRTChannel">TDP自分渠道</param>
        /// <param name="FitSalesArea">TDP自分区域</param>
        /// <returns></returns>
        public int GetFitPrice(int Supplier, int FitRTChannel, int FitSalesArea)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Supplier", SqlDbType.Int, 4, Supplier),
                SQLDatabase.MakeInParam("@FitRTChannel", SqlDbType.Int, 4, FitRTChannel),
                SQLDatabase.MakeInParam("@FitSalesArea", SqlDbType.Int, 4, FitSalesArea)
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_GetFitPrice", prams);

            return ret;
        }
        #endregion

        /// <summary>
        /// 获取向门店销售指定产品里的销售价
        /// </summary>
        /// <param name="Client">门店</param>
        /// <param name="Supplier">经销商</param>
        /// <param name="Product">产品</param>
        /// <returns>销售价，小于0失败</returns>
        public decimal GetSalePrice(int Client, int Supplier, int Product)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, Client),
                SQLDatabase.MakeInParam("@Supplier", SqlDbType.Int, 4, Supplier),
                SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, Product),
                new SqlParameter("@SalePrice", SqlDbType.Decimal,9, ParameterDirection.Output,false,18,4,"SalePrice", DataRowVersion.Current,0)
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_GetSalePrice", prams);

            if (ret == 0 && prams[3].Value != DBNull.Value) return (decimal)prams[3].Value;
            return ret;
        }
    }
}

