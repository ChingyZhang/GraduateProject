
// ===================================================================
// 文件： PBM_DeliveryBLL.cs
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
    ///PBM_Delivery数据访问DAL类
    /// </summary>
    public class PBM_DeliveryDAL : BaseComplexDAL<PBM_Delivery, PBM_DeliveryDetail>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public PBM_DeliveryDAL()
        {
            _ProcePrefix = "MCS_PBM.dbo.sp_PBM_Delivery";
        }
        #endregion


        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(PBM_Delivery m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@SheetCode", SqlDbType.VarChar, 100, m.SheetCode),
				SQLDatabase.MakeInParam("@Supplier", SqlDbType.Int, 4, m.Supplier),
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, m.Client),
				SQLDatabase.MakeInParam("@SupplierWareHouse", SqlDbType.Int, 4, m.SupplierWareHouse),
				SQLDatabase.MakeInParam("@ClientWareHouse", SqlDbType.Int, 4, m.ClientWareHouse),
				SQLDatabase.MakeInParam("@SalesMan", SqlDbType.Int, 4, m.SalesMan),
				SQLDatabase.MakeInParam("@DeliveryMan", SqlDbType.Int, 4, m.DeliveryMan),
				SQLDatabase.MakeInParam("@Classify", SqlDbType.Int, 4, m.Classify),
				SQLDatabase.MakeInParam("@PrepareMode", SqlDbType.Int, 4, m.PrepareMode),
				SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, m.State),
				SQLDatabase.MakeInParam("@StandardPrice", SqlDbType.Int, 4, m.StandardPrice),
				SQLDatabase.MakeInParam("@OrderId", SqlDbType.Int, 4, m.OrderId),
				SQLDatabase.MakeInParam("@DiscountAmount", SqlDbType.Decimal, 9, m.DiscountAmount),
				SQLDatabase.MakeInParam("@WipeAmount", SqlDbType.Decimal, 9, m.WipeAmount),
				SQLDatabase.MakeInParam("@ActAmount", SqlDbType.Decimal, 9, m.ActAmount),
				SQLDatabase.MakeInParam("@DeliveryVehicle", SqlDbType.Int, 4, m.DeliveryVehicle),
				SQLDatabase.MakeInParam("@PreArrivalDate", SqlDbType.DateTime, 8, m.PreArrivalDate),
				SQLDatabase.MakeInParam("@LoadingTime", SqlDbType.DateTime, 8, m.LoadingTime),
				SQLDatabase.MakeInParam("@DepartTime", SqlDbType.DateTime, 8, m.DepartTime),
				SQLDatabase.MakeInParam("@ActArriveTime", SqlDbType.DateTime, 8, m.ActArriveTime),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 1000, m.Remark),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@InsertStaff", SqlDbType.Int, 4, m.InsertStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName)),
				SQLDatabase.MakeInParam("@WorkList", SqlDbType.Int, 4, m.WorkList)
			};
            #endregion

            m.ID = SQLDatabase.RunProc(_ProcePrefix + "_Add", prams);

            return m.ID;
        }

        /// <summary>
        /// 更新项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Update(PBM_Delivery m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@SheetCode", SqlDbType.VarChar, 100, m.SheetCode),
				SQLDatabase.MakeInParam("@Supplier", SqlDbType.Int, 4, m.Supplier),
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, m.Client),
				SQLDatabase.MakeInParam("@SupplierWareHouse", SqlDbType.Int, 4, m.SupplierWareHouse),
				SQLDatabase.MakeInParam("@ClientWareHouse", SqlDbType.Int, 4, m.ClientWareHouse),
				SQLDatabase.MakeInParam("@SalesMan", SqlDbType.Int, 4, m.SalesMan),
				SQLDatabase.MakeInParam("@DeliveryMan", SqlDbType.Int, 4, m.DeliveryMan),
				SQLDatabase.MakeInParam("@Classify", SqlDbType.Int, 4, m.Classify),
				SQLDatabase.MakeInParam("@PrepareMode", SqlDbType.Int, 4, m.PrepareMode),
				SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, m.State),
				SQLDatabase.MakeInParam("@StandardPrice", SqlDbType.Int, 4, m.StandardPrice),
				SQLDatabase.MakeInParam("@OrderId", SqlDbType.Int, 4, m.OrderId),
				SQLDatabase.MakeInParam("@DiscountAmount", SqlDbType.Decimal, 9, m.DiscountAmount),
				SQLDatabase.MakeInParam("@WipeAmount", SqlDbType.Decimal, 9, m.WipeAmount),
				SQLDatabase.MakeInParam("@ActAmount", SqlDbType.Decimal, 9, m.ActAmount),
				SQLDatabase.MakeInParam("@DeliveryVehicle", SqlDbType.Int, 4, m.DeliveryVehicle),
				SQLDatabase.MakeInParam("@PreArrivalDate", SqlDbType.DateTime, 8, m.PreArrivalDate),
				SQLDatabase.MakeInParam("@LoadingTime", SqlDbType.DateTime, 8, m.LoadingTime),
				SQLDatabase.MakeInParam("@DepartTime", SqlDbType.DateTime, 8, m.DepartTime),
				SQLDatabase.MakeInParam("@ActArriveTime", SqlDbType.DateTime, 8, m.ActArriveTime),
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

        protected override PBM_Delivery FillModel(IDataReader dr)
        {
            PBM_Delivery m = new PBM_Delivery();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["SheetCode"].ToString())) m.SheetCode = (string)dr["SheetCode"];
            if (!string.IsNullOrEmpty(dr["Supplier"].ToString())) m.Supplier = (int)dr["Supplier"];
            if (!string.IsNullOrEmpty(dr["Client"].ToString())) m.Client = (int)dr["Client"];
            if (!string.IsNullOrEmpty(dr["SupplierWareHouse"].ToString())) m.SupplierWareHouse = (int)dr["SupplierWareHouse"];
            if (!string.IsNullOrEmpty(dr["ClientWareHouse"].ToString())) m.ClientWareHouse = (int)dr["ClientWareHouse"];
            if (!string.IsNullOrEmpty(dr["SalesMan"].ToString())) m.SalesMan = (int)dr["SalesMan"];
            if (!string.IsNullOrEmpty(dr["DeliveryMan"].ToString())) m.DeliveryMan = (int)dr["DeliveryMan"];
            if (!string.IsNullOrEmpty(dr["Classify"].ToString())) m.Classify = (int)dr["Classify"];
            if (!string.IsNullOrEmpty(dr["PrepareMode"].ToString())) m.PrepareMode = (int)dr["PrepareMode"];
            if (!string.IsNullOrEmpty(dr["State"].ToString())) m.State = (int)dr["State"];
            if (!string.IsNullOrEmpty(dr["StandardPrice"].ToString())) m.StandardPrice = (int)dr["StandardPrice"];
            if (!string.IsNullOrEmpty(dr["OrderId"].ToString())) m.OrderId = (int)dr["OrderId"];
            if (!string.IsNullOrEmpty(dr["DiscountAmount"].ToString())) m.DiscountAmount = (decimal)dr["DiscountAmount"];
            if (!string.IsNullOrEmpty(dr["WipeAmount"].ToString())) m.WipeAmount = (decimal)dr["WipeAmount"];
            if (!string.IsNullOrEmpty(dr["ActAmount"].ToString())) m.ActAmount = (decimal)dr["ActAmount"];
            if (!string.IsNullOrEmpty(dr["DeliveryVehicle"].ToString())) m.DeliveryVehicle = (int)dr["DeliveryVehicle"];
            if (!string.IsNullOrEmpty(dr["PreArrivalDate"].ToString())) m.PreArrivalDate = (DateTime)dr["PreArrivalDate"];
            if (!string.IsNullOrEmpty(dr["LoadingTime"].ToString())) m.LoadingTime = (DateTime)dr["LoadingTime"];
            if (!string.IsNullOrEmpty(dr["DepartTime"].ToString())) m.DepartTime = (DateTime)dr["DepartTime"];
            if (!string.IsNullOrEmpty(dr["ActArriveTime"].ToString())) m.ActArriveTime = (DateTime)dr["ActArriveTime"];
            if (!string.IsNullOrEmpty(dr["Remark"].ToString())) m.Remark = (string)dr["Remark"];
            if (!string.IsNullOrEmpty(dr["ApproveFlag"].ToString())) m.ApproveFlag = (int)dr["ApproveFlag"];
            if (!string.IsNullOrEmpty(dr["InsertTime"].ToString())) m.InsertTime = (DateTime)dr["InsertTime"];
            if (!string.IsNullOrEmpty(dr["InsertStaff"].ToString())) m.InsertStaff = (int)dr["InsertStaff"];
            if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString())) m.UpdateTime = (DateTime)dr["UpdateTime"];
            if (!string.IsNullOrEmpty(dr["UpdateStaff"].ToString())) m.UpdateStaff = (int)dr["UpdateStaff"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);
            if (!string.IsNullOrEmpty(dr["WorkList"].ToString())) m.WorkList = (int)dr["WorkList"];

            return m;
        }

        public override int AddDetail(PBM_DeliveryDetail m)
        {
            m.DeliveryID = HeadID;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@DeliveryID", SqlDbType.Int, 4, m.DeliveryID),
				SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, m.Product),
                SQLDatabase.MakeInParam("@LotNumber", SqlDbType.VarChar, 100, m.LotNumber),
				SQLDatabase.MakeInParam("@CostPrice", SqlDbType.Decimal, 9, m.CostPrice),
				SQLDatabase.MakeInParam("@Price", SqlDbType.Decimal, 9, m.Price),
				SQLDatabase.MakeInParam("@DiscountRate", SqlDbType.Decimal, 9, m.DiscountRate),
                SQLDatabase.MakeInParam("@ConvertFactor", SqlDbType.Int, 4, m.ConvertFactor),
				SQLDatabase.MakeInParam("@DeliveryQuantity", SqlDbType.Int, 4, m.DeliveryQuantity),
				SQLDatabase.MakeInParam("@SignInQuantity", SqlDbType.Int, 4, m.SignInQuantity),
				SQLDatabase.MakeInParam("@ReturnQuantity", SqlDbType.Int, 4, m.ReturnQuantity),
				SQLDatabase.MakeInParam("@BadQuantity", SqlDbType.Int, 4, m.BadQuantity),
				SQLDatabase.MakeInParam("@LostQuantity", SqlDbType.Int, 4, m.LostQuantity),
				SQLDatabase.MakeInParam("@SalesMode", SqlDbType.Int, 4, m.SalesMode),
                SQLDatabase.MakeInParam("@ProductDate", SqlDbType.DateTime, 8, m.ProductDate),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 1000, m.Remark),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            m.ID = SQLDatabase.RunProc(_ProcePrefix + "_AddDetail", prams);

            return m.ID;
        }

        public override int UpdateDetail(PBM_DeliveryDetail m)
        {
            m.DeliveryID = HeadID;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@DeliveryID", SqlDbType.Int, 4, m.DeliveryID),
				SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, m.Product),
                SQLDatabase.MakeInParam("@LotNumber", SqlDbType.VarChar, 100, m.LotNumber),
				SQLDatabase.MakeInParam("@CostPrice", SqlDbType.Decimal, 9, m.CostPrice),
				SQLDatabase.MakeInParam("@Price", SqlDbType.Decimal, 9, m.Price),
				SQLDatabase.MakeInParam("@DiscountRate", SqlDbType.Decimal, 9, m.DiscountRate),
                SQLDatabase.MakeInParam("@ConvertFactor", SqlDbType.Int, 4, m.ConvertFactor),
				SQLDatabase.MakeInParam("@DeliveryQuantity", SqlDbType.Int, 4, m.DeliveryQuantity),
				SQLDatabase.MakeInParam("@SignInQuantity", SqlDbType.Int, 4, m.SignInQuantity),
				SQLDatabase.MakeInParam("@ReturnQuantity", SqlDbType.Int, 4, m.ReturnQuantity),
				SQLDatabase.MakeInParam("@BadQuantity", SqlDbType.Int, 4, m.BadQuantity),
				SQLDatabase.MakeInParam("@LostQuantity", SqlDbType.Int, 4, m.LostQuantity),
				SQLDatabase.MakeInParam("@SalesMode", SqlDbType.Int, 4, m.SalesMode),
                SQLDatabase.MakeInParam("@ProductDate", SqlDbType.DateTime, 8, m.ProductDate),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 1000, m.Remark),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_UpdateDetail", prams);

            return ret;
        }

        protected override PBM_DeliveryDetail FillDetailModel(IDataReader dr)
        {
            PBM_DeliveryDetail m = new PBM_DeliveryDetail();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["DeliveryID"].ToString())) m.DeliveryID = (int)dr["DeliveryID"];
            if (!string.IsNullOrEmpty(dr["Product"].ToString())) m.Product = (int)dr["Product"];
            if (!string.IsNullOrEmpty(dr["LotNumber"].ToString())) m.LotNumber = (string)dr["LotNumber"];
            if (!string.IsNullOrEmpty(dr["CostPrice"].ToString())) m.CostPrice = (decimal)dr["CostPrice"];
            if (!string.IsNullOrEmpty(dr["Price"].ToString())) m.Price = (decimal)dr["Price"];
            if (!string.IsNullOrEmpty(dr["DiscountRate"].ToString())) m.DiscountRate = (decimal)dr["DiscountRate"];
            if (!string.IsNullOrEmpty(dr["ConvertFactor"].ToString())) m.ConvertFactor = (int)dr["ConvertFactor"];
            if (!string.IsNullOrEmpty(dr["DeliveryQuantity"].ToString())) m.DeliveryQuantity = (int)dr["DeliveryQuantity"];
            if (!string.IsNullOrEmpty(dr["SignInQuantity"].ToString())) m.SignInQuantity = (int)dr["SignInQuantity"];
            if (!string.IsNullOrEmpty(dr["ReturnQuantity"].ToString())) m.ReturnQuantity = (int)dr["ReturnQuantity"];
            if (!string.IsNullOrEmpty(dr["BadQuantity"].ToString())) m.BadQuantity = (int)dr["BadQuantity"];
            if (!string.IsNullOrEmpty(dr["LostQuantity"].ToString())) m.LostQuantity = (int)dr["LostQuantity"];
            if (!string.IsNullOrEmpty(dr["SalesMode"].ToString())) m.SalesMode = (int)dr["SalesMode"];
            if (!string.IsNullOrEmpty(dr["ProductDate"].ToString())) m.ProductDate = (DateTime)dr["ProductDate"];
            if (!string.IsNullOrEmpty(dr["Remark"].ToString())) m.Remark = (string)dr["Remark"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);


            return m;
        }

        /// <summary>
        /// 确认提交发货单
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>-1:未找到符合条件的出库单 -2:出库单未指定供库仓库 -10:库存不足</returns>
        public int Approve(int ID)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, ID)
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Approve", prams);

            return ret;
        }
        /// <summary>
        /// 取消发货单
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
				SQLDatabase.MakeInParam("@DeliveryID", SqlDbType.Int, 4, ID)
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_ClearPayInfo", prams);

            return ret;
        }

        /// <summary>
        /// 将预售模式发货单设为派单状态
        /// </summary>
        /// <param name="DeliveryID"></param>
        /// <param name="DeliveryMan"></param>
        /// <param name="DeliveryVehicle"></param>
        /// <param name="PreArrivalDate"></param>
        /// <param name="Staff"></param>
        /// <returns></returns>
        public int Assign(int DeliveryID, int DeliveryMan, int DeliveryVehicle, DateTime PreArrivalDate, int Staff)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@OrderID", SqlDbType.Int, 4, DeliveryID),
                SQLDatabase.MakeInParam("@DeliveryMan", SqlDbType.Int, 4, DeliveryMan),
                SQLDatabase.MakeInParam("@DeliveryVehicle", SqlDbType.Int, 4, DeliveryVehicle),
                SQLDatabase.MakeInParam("@PreArrivalDate", SqlDbType.DateTime, 8, PreArrivalDate),
                SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4, Staff)
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Assign", prams);

            return ret;
        }
        /// <summary>
        /// 采购入库单确认入库
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Staff"></param>
        /// <returns></returns>
        public int ConfirmPurchaseIn(int ID, int Staff)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, ID),
				SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4, Staff),
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_ConfirmPurchaseIn", prams);

            return ret;
        }

        /// <summary>
        /// 采购退库单确认退库
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Staff"></param>
        /// <returns></returns>
        public int ConfirmPurchaseOut(int ID, int Staff)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, ID),
				SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4, Staff),
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_ConfirmPurchaseOut", prams);

            return ret;
        }

        /// <summary>
        /// 销售单确认出库
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Staff"></param>
        /// <returns></returns>
        public int ConfirmSaleOut(int ID, int Staff)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, ID),
				SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4, Staff),
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_ConfirmSaleOut", prams);

            return ret;
        }

        /// <summary>
        /// 销售单确认退货
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Staff"></param>
        /// <returns></returns>
        public int ConfirmSaleReturn(int ID, int Staff)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, ID),
				SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4, Staff),
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_ConfirmSaleReturn", prams);

            return ret;
        }

        /// <summary>
        /// 移库单确认调拨
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Staff"></param>
        /// <returns></returns>
        public int ConfirmTrans(int ID, int Staff)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, ID),
				SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4, Staff),
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_ConfirmTrans", prams);

            return ret;
        }

        /// <summary>
        /// 盘点单确认
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Staff"></param>
        /// <returns></returns>
        public int ConfirmAdjust(int ID, int Staff)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, ID),
				SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4, Staff),
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_ConfirmAdjust", prams);

            return ret;
        }

        /// <summary>
        /// 初始化库存盘点单
        /// </summary>
        /// <param name="WareHouse"></param>
        /// <param name="OpStaff"></param>
        /// <param name="InventoryIDs"></param>
        /// <returns></returns>
        public int AdjustInit(int WareHouse, int OpStaff, string InventoryIDs)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@WareHouse", SqlDbType.Int, 4, WareHouse),
				SQLDatabase.MakeInParam("@OpStaff", SqlDbType.Int, 4, OpStaff),
                SQLDatabase.MakeInParam("@InventoryIDs", SqlDbType.VarChar, InventoryIDs.Length, InventoryIDs)
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_AdjustInit", prams);

            return ret;
        }

        /// <summary>
        /// 汇总预售待送货列表
        /// </summary>
        /// <param name="Supplier"></param>
        /// <param name="DeliveryMan"></param>
        /// <param name="DeliveryVehicle"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public DataTable GetNeedDeliverySummary(DateTime PreArrivalBeginDate, DateTime PreArrivalEndDate,
            int Supplier, int SupplierWareHouse, int SalesMan, int DeliveryMan)
        {
            SqlDataReader dr = null;
            #region	设置参数集
            SqlParameter[] prams = {
                SQLDatabase.MakeInParam("@PreArrivalBeginDate", SqlDbType.DateTime, 8, PreArrivalBeginDate),
                SQLDatabase.MakeInParam("@PreArrivalEndDate", SqlDbType.DateTime, 8, PreArrivalEndDate),
				SQLDatabase.MakeInParam("@Supplier", SqlDbType.Int, 4, Supplier),
                SQLDatabase.MakeInParam("@SupplierWareHouse", SqlDbType.Int, 4, SupplierWareHouse),
				SQLDatabase.MakeInParam("@SalesMan", SqlDbType.Int, 4, SalesMan),
                SQLDatabase.MakeInParam("@DeliveryMan", SqlDbType.Int, 4, DeliveryMan)
			};
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_GetNeedDeliverySummary", prams, out dr);

            return MCSFramework.Common.Tools.ConvertDataReaderToDataTable(dr);
        }

        /// <summary>
        /// 按零售门店汇总销售单
        /// </summary>
        /// <param name="Supplier"></param>
        /// <param name="DeliveryMan"></param>
        /// <param name="DeliveryVehicle"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public DataTable GetDeliverySummary_ByClient(int Supplier, int SalesMan, int DeliveryMan, int DeliveryVehicle, DateTime BeginDate, DateTime EndDate)
        {
            SqlDataReader dr = null;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Supplier", SqlDbType.Int, 4, Supplier),
				SQLDatabase.MakeInParam("@SalesMan", SqlDbType.Int, 4, SalesMan),
                SQLDatabase.MakeInParam("@DeliveryMan", SqlDbType.Int, 4, DeliveryMan),
                SQLDatabase.MakeInParam("@DeliveryVehicle", SqlDbType.Int, 4, DeliveryVehicle),
                SQLDatabase.MakeInParam("@BeginDate", SqlDbType.DateTime, 8, BeginDate),
                SQLDatabase.MakeInParam("@EndDate", SqlDbType.DateTime, 8, EndDate)
			};
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_GetDeliverySummary_ByClient", prams, out dr);

            return MCSFramework.Common.Tools.ConvertDataReaderToDataTable(dr);
        }

        /// <summary>
        /// 按产品汇总销售单
        /// </summary>
        /// <param name="Supplier"></param>
        /// <param name="DeliveryMan"></param>
        /// <param name="DeliveryVehicle"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public DataTable GetDeliverySummary_ByProduct(int Supplier, int SalesMan, int DeliveryMan, int DeliveryVehicle, DateTime BeginDate, DateTime EndDate)
        {
            SqlDataReader dr = null;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Supplier", SqlDbType.Int, 4, Supplier),
				SQLDatabase.MakeInParam("@SalesMan", SqlDbType.Int, 4, SalesMan),
                SQLDatabase.MakeInParam("@DeliveryMan", SqlDbType.Int, 4, DeliveryMan),
                SQLDatabase.MakeInParam("@DeliveryVehicle", SqlDbType.Int, 4, DeliveryVehicle),
                SQLDatabase.MakeInParam("@BeginDate", SqlDbType.DateTime, 8, BeginDate),
                SQLDatabase.MakeInParam("@EndDate", SqlDbType.DateTime, 8, EndDate)
			};
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_GetDeliverySummary_ByProduct", prams, out dr);

            return MCSFramework.Common.Tools.ConvertDataReaderToDataTable(dr);
        }

        /// <summary>
        /// 按送货人查询当日收款汇总
        /// </summary>
        /// <param name="Supplier"></param>
        /// <param name="DeliveryMan"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public DataTable GetPayInfoSummary(int Supplier, int SalesMan, int DeliveryMan, DateTime BeginDate, DateTime EndDate)
        {
            SqlDataReader dr = null;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Supplier", SqlDbType.Int, 4, Supplier),
				SQLDatabase.MakeInParam("@SalesMan", SqlDbType.Int, 4, SalesMan),
                SQLDatabase.MakeInParam("@DeliveryMan", SqlDbType.Int, 4, DeliveryMan),
                SQLDatabase.MakeInParam("@BeginDate", SqlDbType.DateTime, 8, BeginDate),
                SQLDatabase.MakeInParam("@EndDate", SqlDbType.DateTime, 8, EndDate)
			};
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_GetPayInfoSummary", prams, out dr);

            return MCSFramework.Common.Tools.ConvertDataReaderToDataTable(dr);
        }

        /// <summary>
        /// 按送货人查询当时收款明细
        /// </summary>
        /// <param name="Supplier"></param>
        /// <param name="DeliveryMan"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public DataTable GetPayInfoDetail(int Supplier, int SalesMan, int DeliveryMan, DateTime BeginDate, DateTime EndDate)
        {
            SqlDataReader dr = null;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Supplier", SqlDbType.Int, 4, Supplier),
				SQLDatabase.MakeInParam("@SalesMan", SqlDbType.Int, 4, SalesMan),
                SQLDatabase.MakeInParam("@DeliveryMan", SqlDbType.Int, 4, DeliveryMan),
                SQLDatabase.MakeInParam("@BeginDate", SqlDbType.DateTime, 8, BeginDate),
                SQLDatabase.MakeInParam("@EndDate", SqlDbType.DateTime, 8, EndDate)
			};
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_GetPayInfoDetail", prams, out dr);

            return MCSFramework.Common.Tools.ConvertDataReaderToDataTable(dr);
        }
    }
}

