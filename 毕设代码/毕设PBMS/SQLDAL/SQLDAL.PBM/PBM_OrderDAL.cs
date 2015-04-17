
// ===================================================================
// 文件： PBM_OrderDAL.cs
// 项目名称：
// 创建时间：2015/1/27
// 作者:	   lv
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
	///PBM_Order数据访问DAL类
	/// </summary>
	public class PBM_OrderDAL : BaseComplexDAL<PBM_Order,PBM_OrderDetail>
	{
		#region 构造函数
		///<summary>
		///
		///</summary>
		public PBM_OrderDAL()
		{
			_ProcePrefix = "MCS_PBM.dbo.sp_PBM_Order";
		}
		#endregion
		
		
		/// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(PBM_Order m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@SheetCode", SqlDbType.VarChar, 100, m.SheetCode),
				SQLDatabase.MakeInParam("@Supplier", SqlDbType.Int, 4, m.Supplier),
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, m.Client),
				SQLDatabase.MakeInParam("@SalesMan", SqlDbType.Int, 4, m.SalesMan),
				SQLDatabase.MakeInParam("@StandardPrice", SqlDbType.Int, 4, m.StandardPrice),
				SQLDatabase.MakeInParam("@Classify", SqlDbType.Int, 4, m.Classify),
				SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, m.State),
				SQLDatabase.MakeInParam("@DiscountAmount", SqlDbType.Decimal, 9, m.DiscountAmount),
				SQLDatabase.MakeInParam("@WipeAmount", SqlDbType.Decimal, 9, m.WipeAmount),
				SQLDatabase.MakeInParam("@ActAmount", SqlDbType.Decimal, 9, m.ActAmount),
				SQLDatabase.MakeInParam("@ArriveTime", SqlDbType.DateTime, 8, m.ArriveTime),
				SQLDatabase.MakeInParam("@ArriveWarehouse", SqlDbType.Int, 4, m.ArriveWarehouse),
				SQLDatabase.MakeInParam("@SubmitTime", SqlDbType.DateTime, 8, m.SubmitTime),
				SQLDatabase.MakeInParam("@ConfirmTime", SqlDbType.DateTime, 8, m.ConfirmTime),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 1000, m.Remark),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@InsertStaff", SqlDbType.Int, 4, m.InsertStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName)),
				SQLDatabase.MakeInParam("@WorkList", SqlDbType.Int, 4, m.WorkList)
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
        public override int Update(PBM_Order m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@SheetCode", SqlDbType.VarChar, 100, m.SheetCode),
				SQLDatabase.MakeInParam("@Supplier", SqlDbType.Int, 4, m.Supplier),
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, m.Client),
				SQLDatabase.MakeInParam("@SalesMan", SqlDbType.Int, 4, m.SalesMan),
				SQLDatabase.MakeInParam("@StandardPrice", SqlDbType.Int, 4, m.StandardPrice),
				SQLDatabase.MakeInParam("@Classify", SqlDbType.Int, 4, m.Classify),
				SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, m.State),
				SQLDatabase.MakeInParam("@DiscountAmount", SqlDbType.Decimal, 9, m.DiscountAmount),
				SQLDatabase.MakeInParam("@WipeAmount", SqlDbType.Decimal, 9, m.WipeAmount),
				SQLDatabase.MakeInParam("@ActAmount", SqlDbType.Decimal, 9, m.ActAmount),
				SQLDatabase.MakeInParam("@ArriveTime", SqlDbType.DateTime, 8, m.ArriveTime),
				SQLDatabase.MakeInParam("@ArriveWarehouse", SqlDbType.Int, 4, m.ArriveWarehouse),
				SQLDatabase.MakeInParam("@SubmitTime", SqlDbType.DateTime, 8, m.SubmitTime),
				SQLDatabase.MakeInParam("@ConfirmTime", SqlDbType.DateTime, 8, m.ConfirmTime),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 1000, m.Remark),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@UpdateStaff", SqlDbType.Int, 4, m.UpdateStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName)),
				SQLDatabase.MakeInParam("@WorkList", SqlDbType.Int, 4, m.WorkList)
			};
			#endregion
			
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
			
			return ret;
        }
		
        protected override PBM_Order FillModel(IDataReader dr)
		{
			PBM_Order m = new PBM_Order();
			if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (int)dr["ID"];
			if (!string.IsNullOrEmpty(dr["SheetCode"].ToString()))	m.SheetCode = (string)dr["SheetCode"];
			if (!string.IsNullOrEmpty(dr["Supplier"].ToString()))	m.Supplier = (int)dr["Supplier"];
			if (!string.IsNullOrEmpty(dr["Client"].ToString()))	m.Client = (int)dr["Client"];
			if (!string.IsNullOrEmpty(dr["SalesMan"].ToString()))	m.SalesMan = (int)dr["SalesMan"];
			if (!string.IsNullOrEmpty(dr["StandardPrice"].ToString()))	m.StandardPrice = (int)dr["StandardPrice"];
			if (!string.IsNullOrEmpty(dr["Classify"].ToString()))	m.Classify = (int)dr["Classify"];
			if (!string.IsNullOrEmpty(dr["State"].ToString()))	m.State = (int)dr["State"];
			if (!string.IsNullOrEmpty(dr["DiscountAmount"].ToString()))	m.DiscountAmount = (decimal)dr["DiscountAmount"];
			if (!string.IsNullOrEmpty(dr["WipeAmount"].ToString()))	m.WipeAmount = (decimal)dr["WipeAmount"];
			if (!string.IsNullOrEmpty(dr["ActAmount"].ToString()))	m.ActAmount = (decimal)dr["ActAmount"];
			if (!string.IsNullOrEmpty(dr["ArriveTime"].ToString()))	m.ArriveTime = (DateTime)dr["ArriveTime"];
			if (!string.IsNullOrEmpty(dr["ArriveWarehouse"].ToString()))	m.ArriveWarehouse = (int)dr["ArriveWarehouse"];
			if (!string.IsNullOrEmpty(dr["SubmitTime"].ToString()))	m.SubmitTime = (DateTime)dr["SubmitTime"];
			if (!string.IsNullOrEmpty(dr["ConfirmTime"].ToString()))	m.ConfirmTime = (DateTime)dr["ConfirmTime"];
			if (!string.IsNullOrEmpty(dr["Remark"].ToString()))	m.Remark = (string)dr["Remark"];
			if (!string.IsNullOrEmpty(dr["ApproveFlag"].ToString()))	m.ApproveFlag = (int)dr["ApproveFlag"];
			if (!string.IsNullOrEmpty(dr["InsertTime"].ToString()))	m.InsertTime = (DateTime)dr["InsertTime"];
			if (!string.IsNullOrEmpty(dr["InsertStaff"].ToString()))	m.InsertStaff = (int)dr["InsertStaff"];
			if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString()))	m.UpdateTime = (DateTime)dr["UpdateTime"];
			if (!string.IsNullOrEmpty(dr["UpdateStaff"].ToString()))	m.UpdateStaff = (int)dr["UpdateStaff"];
			if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString()))	m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);
			if (!string.IsNullOrEmpty(dr["WorkList"].ToString()))	m.WorkList = (int)dr["WorkList"];
						
			return m;
		}
		
		public override int AddDetail(PBM_OrderDetail m)
        {
			m.OrderID = HeadID;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@OrderID", SqlDbType.Int, 4, m.OrderID),
				SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, m.Product),
				SQLDatabase.MakeInParam("@Price", SqlDbType.Decimal, 9, m.Price),
				SQLDatabase.MakeInParam("@DiscountRate", SqlDbType.Decimal, 9, m.DiscountRate),
                SQLDatabase.MakeInParam("@ConvertFactor", SqlDbType.Int, 4, m.ConvertFactor),
				SQLDatabase.MakeInParam("@BookQuantity", SqlDbType.Int, 4, m.BookQuantity),
				SQLDatabase.MakeInParam("@ConfirmQuantity", SqlDbType.Int, 4, m.ConfirmQuantity),
				SQLDatabase.MakeInParam("@AdjustReason", SqlDbType.VarChar, 200, m.AdjustReason),
				SQLDatabase.MakeInParam("@DeliveredQuantity", SqlDbType.Int, 4, m.DeliveredQuantity),
				SQLDatabase.MakeInParam("@SalesMode", SqlDbType.Int, 4, m.SalesMode),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 1000, m.Remark),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            m.ID = SQLDatabase.RunProc(_ProcePrefix + "_AddDetail", prams);

            return m.ID;
        }

        public override int UpdateDetail(PBM_OrderDetail m)
        {
            m.OrderID = HeadID;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@OrderID", SqlDbType.Int, 4, m.OrderID),
				SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, m.Product),
				SQLDatabase.MakeInParam("@Price", SqlDbType.Decimal, 9, m.Price),
				SQLDatabase.MakeInParam("@DiscountRate", SqlDbType.Decimal, 9, m.DiscountRate),
                SQLDatabase.MakeInParam("@ConvertFactor", SqlDbType.Int, 4, m.ConvertFactor),
				SQLDatabase.MakeInParam("@BookQuantity", SqlDbType.Int, 4, m.BookQuantity),
				SQLDatabase.MakeInParam("@ConfirmQuantity", SqlDbType.Int, 4, m.ConfirmQuantity),
				SQLDatabase.MakeInParam("@AdjustReason", SqlDbType.VarChar, 200, m.AdjustReason),
				SQLDatabase.MakeInParam("@DeliveredQuantity", SqlDbType.Int, 4, m.DeliveredQuantity),
				SQLDatabase.MakeInParam("@SalesMode", SqlDbType.Int, 4, m.SalesMode),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 1000, m.Remark),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix+"_UpdateDetail", prams);

            return ret;
        }

        protected override PBM_OrderDetail FillDetailModel(IDataReader dr)
        {
            PBM_OrderDetail m = new PBM_OrderDetail();
            if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (int)dr["ID"];
			if (!string.IsNullOrEmpty(dr["OrderID"].ToString()))	m.OrderID = (int)dr["OrderID"];
			if (!string.IsNullOrEmpty(dr["Product"].ToString()))	m.Product = (int)dr["Product"];
			if (!string.IsNullOrEmpty(dr["Price"].ToString()))	m.Price = (decimal)dr["Price"];
			if (!string.IsNullOrEmpty(dr["DiscountRate"].ToString()))	m.DiscountRate = (decimal)dr["DiscountRate"];
            if (!string.IsNullOrEmpty(dr["ConvertFactor"].ToString())) m.ConvertFactor = (int)dr["ConvertFactor"];
            if (!string.IsNullOrEmpty(dr["BookQuantity"].ToString()))	m.BookQuantity = (int)dr["BookQuantity"];
			if (!string.IsNullOrEmpty(dr["ConfirmQuantity"].ToString()))	m.ConfirmQuantity = (int)dr["ConfirmQuantity"];
			if (!string.IsNullOrEmpty(dr["AdjustReason"].ToString()))	m.AdjustReason = (string)dr["AdjustReason"];
			if (!string.IsNullOrEmpty(dr["DeliveredQuantity"].ToString()))	m.DeliveredQuantity = (int)dr["DeliveredQuantity"];
			if (!string.IsNullOrEmpty(dr["SalesMode"].ToString()))	m.SalesMode = (int)dr["SalesMode"];
			if (!string.IsNullOrEmpty(dr["Remark"].ToString()))	m.Remark = (string)dr["Remark"];
			if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString()))	m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);
					

            return m;
        }


        /// <summary>
        /// 确认提交订货单
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>-1:未找到符合条件的出库单</returns>
        public int Submit(int ID, int Staff)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, ID),
                SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4, Staff)
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Submit", prams);

            return ret;
        }
        /// <summary>
        /// 取消订货单
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Staff"></param>
        /// <param name="CancelReason"></param>
        /// <returns></returns>
        public int Cancel(int ID, int Staff, string CancelReason)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, ID),
                SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4, Staff),
				SQLDatabase.MakeInParam("@CancelReason", SqlDbType.VarChar, 200, CancelReason),
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Cancel", prams);

            return ret;
        }

        /// <summary>
        /// 清除付款信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public int ClearPayInfo(int ID)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@OrderID", SqlDbType.Int, 4, ID)
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_ClearPayInfo", prams);

            return ret;
        }

        /// <summary>
        /// 根据订单创建发货单
        /// </summary>
        /// <param name="OrderID"></param>
        /// <param name="SupplierWareHouse"></param>
        /// <param name="DeliveryMan"></param>
        /// <param name="DeliveryVehicle"></param>
        /// <param name="PreArrivalDate"></param>
        /// <param name="Staff"></param>
        /// <returns>大于0:发货单号 -1:无符合条件的订单 -2:无效的出库仓库 </returns>
        public int CreateDelivery(int OrderID, int SupplierWareHouse, int DeliveryMan, int DeliveryVehicle, DateTime PreArrivalDate, int Staff)
        { 
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@OrderID", SqlDbType.Int, 4, OrderID),
                SQLDatabase.MakeInParam("@SupplierWareHouse", SqlDbType.Int, 4, SupplierWareHouse),
                SQLDatabase.MakeInParam("@DeliveryMan", SqlDbType.Int, 4, DeliveryMan),
                SQLDatabase.MakeInParam("@DeliveryVehicle", SqlDbType.Int, 4, DeliveryVehicle),
                SQLDatabase.MakeInParam("@PreArrivalDate", SqlDbType.DateTime, 8, PreArrivalDate),
                SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4, Staff)
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_CreateDelivery", prams);

            return ret;
        }

        /// <summary>
        /// 按零售门店汇总销售单
        /// </summary>
        /// <param name="Supplier"></param>
        /// <param name="SalesMan"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public DataTable GetOrderSummary_ByClient(int Supplier, int SalesMan, DateTime BeginDate, DateTime EndDate)
        {
            SqlDataReader dr = null;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Supplier", SqlDbType.Int, 4, Supplier),
				SQLDatabase.MakeInParam("@SalesMan", SqlDbType.Int, 4, SalesMan),
                SQLDatabase.MakeInParam("@BeginDate", SqlDbType.DateTime, 8, BeginDate),
                SQLDatabase.MakeInParam("@EndDate", SqlDbType.DateTime, 8, EndDate)
			};
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_GetOrderSummary_ByClient", prams, out dr);

            return MCSFramework.Common.Tools.ConvertDataReaderToDataTable(dr);
        }

        /// <summary>
        /// 按产品汇总销售单
        /// </summary>
        /// <param name="Supplier"></param>
        /// <param name="SalesMan"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public DataTable GetOrderSummary_ByProduct(int Supplier, int SalesMan, DateTime BeginDate, DateTime EndDate)
        {
            SqlDataReader dr = null;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Supplier", SqlDbType.Int, 4, Supplier),
				SQLDatabase.MakeInParam("@SalesMan", SqlDbType.Int, 4, SalesMan),
                SQLDatabase.MakeInParam("@BeginDate", SqlDbType.DateTime, 8, BeginDate),
                SQLDatabase.MakeInParam("@EndDate", SqlDbType.DateTime, 8, EndDate)
			};
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_GetOrderSummary_ByProduct", prams, out dr);

            return MCSFramework.Common.Tools.ConvertDataReaderToDataTable(dr);
        }
    }
}

