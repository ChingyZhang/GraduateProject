
// ===================================================================
// 文件： ORD_DeliveryDAL.cs
// 项目名称：
// 创建时间：2012-7-22
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.EBM;
using MCSFramework.Common;


namespace MCSFramework.SQLDAL.EBM
{
    /// <summary>
    ///ORD_Delivery数据访问DAL类
    /// </summary>
    public class ORD_DeliveryDAL : BaseComplexDAL<ORD_Delivery, ORD_DeliveryDetail>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public ORD_DeliveryDAL()
        {
            _ProcePrefix = "MCS_EBM.dbo.sp_ORD_Delivery";
        }
        #endregion

        #region 基本操作
        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(ORD_Delivery m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@SheetCode", SqlDbType.VarChar, 100, m.SheetCode),
				SQLDatabase.MakeInParam("@Supplier", SqlDbType.Int, 4, m.Supplier),
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, m.Client),
				SQLDatabase.MakeInParam("@SupplierWareHouse", SqlDbType.Int, 4, m.SupplierWareHouse),
				SQLDatabase.MakeInParam("@ClientWareHouse", SqlDbType.Int, 4, m.ClientWareHouse),
				SQLDatabase.MakeInParam("@Classify", SqlDbType.Int, 4, m.Classify),
				SQLDatabase.MakeInParam("@PrepareMode", SqlDbType.Int, 4, m.PrepareMode),
				SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, m.State),
				SQLDatabase.MakeInParam("@OrderID", SqlDbType.Int, 4, m.OrderID),
				SQLDatabase.MakeInParam("@TruckingID", SqlDbType.Int, 4, m.TruckingID),
				SQLDatabase.MakeInParam("@PreArrivalDate", SqlDbType.DateTime, 8, m.PreArrivalDate),
				SQLDatabase.MakeInParam("@LoadingTime", SqlDbType.DateTime, 8, m.LoadingTime),
				SQLDatabase.MakeInParam("@DepartTime", SqlDbType.DateTime, 8, m.DepartTime),
				SQLDatabase.MakeInParam("@ActArriveTime", SqlDbType.DateTime, 8, m.ActArriveTime),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 2000, m.Remark),
				SQLDatabase.MakeInParam("@ApproveTask", SqlDbType.Int, 4, m.ApproveTask),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@InsertUser", SqlDbType.UniqueIdentifier, 16, m.InsertUser),
				SQLDatabase.MakeInParam("@UpdateUser", SqlDbType.UniqueIdentifier, 16, m.UpdateUser),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
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
        public override int Update(ORD_Delivery m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@SheetCode", SqlDbType.VarChar, 100, m.SheetCode),
				SQLDatabase.MakeInParam("@Supplier", SqlDbType.Int, 4, m.Supplier),
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, m.Client),
				SQLDatabase.MakeInParam("@SupplierWareHouse", SqlDbType.Int, 4, m.SupplierWareHouse),
				SQLDatabase.MakeInParam("@ClientWareHouse", SqlDbType.Int, 4, m.ClientWareHouse),
				SQLDatabase.MakeInParam("@Classify", SqlDbType.Int, 4, m.Classify),
				SQLDatabase.MakeInParam("@PrepareMode", SqlDbType.Int, 4, m.PrepareMode),
				SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, m.State),
				SQLDatabase.MakeInParam("@OrderID", SqlDbType.Int, 4, m.OrderID),
				SQLDatabase.MakeInParam("@TruckingID", SqlDbType.Int, 4, m.TruckingID),
				SQLDatabase.MakeInParam("@PreArrivalDate", SqlDbType.DateTime, 8, m.PreArrivalDate),
				SQLDatabase.MakeInParam("@LoadingTime", SqlDbType.DateTime, 8, m.LoadingTime),
				SQLDatabase.MakeInParam("@DepartTime", SqlDbType.DateTime, 8, m.DepartTime),
				SQLDatabase.MakeInParam("@ActArriveTime", SqlDbType.DateTime, 8, m.ActArriveTime),
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

        protected override ORD_Delivery FillModel(IDataReader dr)
        {
            ORD_Delivery m = new ORD_Delivery();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["SheetCode"].ToString())) m.SheetCode = (string)dr["SheetCode"];
            if (!string.IsNullOrEmpty(dr["Supplier"].ToString())) m.Supplier = (int)dr["Supplier"];
            if (!string.IsNullOrEmpty(dr["Client"].ToString())) m.Client = (int)dr["Client"];
            if (!string.IsNullOrEmpty(dr["SupplierWareHouse"].ToString())) m.SupplierWareHouse = (int)dr["SupplierWareHouse"];
            if (!string.IsNullOrEmpty(dr["ClientWareHouse"].ToString())) m.ClientWareHouse = (int)dr["ClientWareHouse"];
            if (!string.IsNullOrEmpty(dr["Classify"].ToString())) m.Classify = (int)dr["Classify"];
            if (!string.IsNullOrEmpty(dr["PrepareMode"].ToString())) m.PrepareMode = (int)dr["PrepareMode"];
            if (!string.IsNullOrEmpty(dr["State"].ToString())) m.State = (int)dr["State"];
            if (!string.IsNullOrEmpty(dr["OrderID"].ToString())) m.OrderID = (int)dr["OrderID"];
            if (!string.IsNullOrEmpty(dr["TruckingID"].ToString())) m.TruckingID = (int)dr["TruckingID"];
            if (!string.IsNullOrEmpty(dr["PreArrivalDate"].ToString())) m.PreArrivalDate = (DateTime)dr["PreArrivalDate"];
            if (!string.IsNullOrEmpty(dr["LoadingTime"].ToString())) m.LoadingTime = (DateTime)dr["LoadingTime"];
            if (!string.IsNullOrEmpty(dr["DepartTime"].ToString())) m.DepartTime = (DateTime)dr["DepartTime"];
            if (!string.IsNullOrEmpty(dr["ActArriveTime"].ToString())) m.ActArriveTime = (DateTime)dr["ActArriveTime"];
            if (!string.IsNullOrEmpty(dr["Remark"].ToString())) m.Remark = (string)dr["Remark"];
            if (!string.IsNullOrEmpty(dr["ApproveTask"].ToString())) m.ApproveTask = (int)dr["ApproveTask"];
            if (!string.IsNullOrEmpty(dr["ApproveFlag"].ToString())) m.ApproveFlag = (int)dr["ApproveFlag"];
            if (!string.IsNullOrEmpty(dr["InsertTime"].ToString())) m.InsertTime = (DateTime)dr["InsertTime"];
            if (!string.IsNullOrEmpty(dr["InsertUser"].ToString())) m.InsertUser = (Guid)dr["InsertUser"];
            if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString())) m.UpdateTime = (DateTime)dr["UpdateTime"];
            if (!string.IsNullOrEmpty(dr["UpdateUser"].ToString())) m.UpdateUser = (Guid)dr["UpdateUser"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);

            return m;
        }

        public override int AddDetail(ORD_DeliveryDetail m)
        {
            m.DeliveryID = HeadID;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@DeliveryID", SqlDbType.Int, 4, m.DeliveryID),
				SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, m.Product),
				SQLDatabase.MakeInParam("@Price", SqlDbType.Decimal, 9, m.Price),
				SQLDatabase.MakeInParam("@DeliveryQuantity", SqlDbType.Int, 4, m.DeliveryQuantity),
				SQLDatabase.MakeInParam("@SignInQuantity", SqlDbType.Int, 4, m.SignInQuantity),
				SQLDatabase.MakeInParam("@ReturnQuantity", SqlDbType.Int, 4, m.ReturnQuantity),
				SQLDatabase.MakeInParam("@BadQuantity", SqlDbType.Int, 4, m.BadQuantity),
				SQLDatabase.MakeInParam("@LostQuantity", SqlDbType.Int, 4, m.LostQuantity),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 1000, m.Remark),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            m.ID = SQLDatabase.RunProc(_ProcePrefix + "_AddDetail", prams);

            return m.ID;
        }

        public override int UpdateDetail(ORD_DeliveryDetail m)
        {
            m.DeliveryID = HeadID;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@DeliveryID", SqlDbType.Int, 4, m.DeliveryID),
				SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, m.Product),
				SQLDatabase.MakeInParam("@Price", SqlDbType.Decimal, 9, m.Price),
				SQLDatabase.MakeInParam("@DeliveryQuantity", SqlDbType.Int, 4, m.DeliveryQuantity),
				SQLDatabase.MakeInParam("@SignInQuantity", SqlDbType.Int, 4, m.SignInQuantity),
				SQLDatabase.MakeInParam("@ReturnQuantity", SqlDbType.Int, 4, m.ReturnQuantity),
				SQLDatabase.MakeInParam("@BadQuantity", SqlDbType.Int, 4, m.BadQuantity),
				SQLDatabase.MakeInParam("@LostQuantity", SqlDbType.Int, 4, m.LostQuantity),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 1000, m.Remark),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_UpdateDetail", prams);

            return ret;
        }

        protected override ORD_DeliveryDetail FillDetailModel(IDataReader dr)
        {
            ORD_DeliveryDetail m = new ORD_DeliveryDetail();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["DeliveryID"].ToString())) m.DeliveryID = (int)dr["DeliveryID"];
            if (!string.IsNullOrEmpty(dr["Product"].ToString())) m.Product = (int)dr["Product"];
            if (!string.IsNullOrEmpty(dr["Price"].ToString())) m.Price = (decimal)dr["Price"];
            if (!string.IsNullOrEmpty(dr["DeliveryQuantity"].ToString())) m.DeliveryQuantity = (int)dr["DeliveryQuantity"];
            if (!string.IsNullOrEmpty(dr["SignInQuantity"].ToString())) m.SignInQuantity = (int)dr["SignInQuantity"];
            if (!string.IsNullOrEmpty(dr["ReturnQuantity"].ToString())) m.ReturnQuantity = (int)dr["ReturnQuantity"];
            if (!string.IsNullOrEmpty(dr["BadQuantity"].ToString())) m.BadQuantity = (int)dr["BadQuantity"];
            if (!string.IsNullOrEmpty(dr["LostQuantity"].ToString())) m.LostQuantity = (int)dr["LostQuantity"];
            if (!string.IsNullOrEmpty(dr["Remark"].ToString())) m.Remark = (string)dr["Remark"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);


            return m;
        }
        #endregion

        #region 生成发货单号
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

        #region 发货单操作
        /// <summary>
        /// 发货单从备单状态完成装车
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="User"></param>
        /// <returns>-1:发货单ID不为备单状态 -2:已装车产品码明细与备单明细数量不相符 -100:更新数据库失败</returns>
        public int Loading(int ID, Guid User)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, ID),
				SQLDatabase.MakeInParam("@User", SqlDbType.UniqueIdentifier, 16, User)                
			};
            #endregion

            return SQLDatabase.RunProc(_ProcePrefix + "_Loading", prams);
        }

        /// <summary>
        /// 发货单确认发车
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="User"></param>
        /// <returns>-1:发货单ID不为装车状态 -2:发货单产品码明细中，有部分产品码在仓库不为装车状态 -100:更新数据库失败</returns>
        public int Depart(int ID, Guid User)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, ID),
				SQLDatabase.MakeInParam("@User", SqlDbType.UniqueIdentifier, 16, User)                
			};
            #endregion

            return SQLDatabase.RunProc(_ProcePrefix + "_Depart", prams);
        }

        /// <summary>
        /// 确认签收整个发货单
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="WareHouseCell"></param>
        /// <param name="User"></param>
        /// <returns>-1:发货单ID不为在途状态 >0:签收产品数量</returns>
        public int SignAll(int ID, int WareHouseCell, Guid User)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, ID),
				SQLDatabase.MakeInParam("@WareHouseCell", SqlDbType.Int, 4, WareHouseCell),
				SQLDatabase.MakeInParam("@User", SqlDbType.UniqueIdentifier, 16, User)                
			};
            #endregion

            return SQLDatabase.RunProc(_ProcePrefix + "_SignAll", prams);
        }

        /// <summary>
        /// 发货单确认退单
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="User"></param>
        /// <returns>-1:发货单ID不为在途状态 -2:发货单产品码明细中，有部分产品码不为在途状态 -100:更新数据库失败</returns>
        public int ReturnAll(int ID, int WareHouseCell, Guid User)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, ID),
				SQLDatabase.MakeInParam("@User", SqlDbType.UniqueIdentifier, 16, User),
                SQLDatabase.MakeInParam("@WareHouseCell", SqlDbType.Int, 4, WareHouseCell)    
			};
            #endregion

            return SQLDatabase.RunProc(_ProcePrefix + "_ReturnAll", prams);
        }

        /// <summary>
        /// 取消发货单
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="User"></param>
        /// <returns>-1:发货单ID不为备单或装车状态 -2:发货单产品码明细中，有部分产品码不为装车状态 -100:更新数据库失败</returns>
        public int Cancel(int ID, Guid User)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, ID),
				SQLDatabase.MakeInParam("@User", SqlDbType.UniqueIdentifier, 16, User)   
			};
            #endregion

            return SQLDatabase.RunProc(_ProcePrefix + "_Cancel", prams);
        }
        #endregion

        #region 逐码扫描物流码操作
        /// <summary>
        /// 按单个物流码装车
        /// </summary>
        /// <param name="DeliveryID"></param>
        /// <param name="Code"></param>
        /// <returns>-1:发货单ID不为可装车状态 -2:物流码无效 -3:实际装车数量超过备单数量 -4:订货单中无该产品 -5:上架目录中无该产品 -6:扣减库存失败 >0:装车产品数量</returns>
        public int LoadingByOneCode(int DeliveryID, string Code)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@DeliveryID", SqlDbType.Int, 4, DeliveryID),
				SQLDatabase.MakeInParam("@Code", SqlDbType.VarChar, 50, Code)                
			};
            #endregion

            return SQLDatabase.RunProc(_ProcePrefix + "_LoadingByOneCode", prams);
        }

        /// <summary>
        /// 按单个物流码签收发货单
        /// </summary>
        /// <param name="DeliveryID"></param>
        /// <param name="Code"></param>
        /// <param name="WareHouseCell"></param>
        /// <returns>-1:发货单ID不为在途状态 -2:物流码无效 >0:签收产品数量</returns>
        public int SignInByOneCode(int DeliveryID, string Code, int WareHouseCell)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@DeliveryID", SqlDbType.Int, 4, DeliveryID),
				SQLDatabase.MakeInParam("@Code", SqlDbType.VarChar, 50, Code),
                SQLDatabase.MakeInParam("@WareHouseCell", SqlDbType.Int, 4, WareHouseCell)
			};
            #endregion

            return SQLDatabase.RunProc(_ProcePrefix + "_SignInByOneCode", prams);
        }

        /// <summary>
        /// 按单个物流码退货发货单
        /// </summary>
        /// <param name="DeliveryID"></param>
        /// <param name="Code"></param>
        /// <param name="WareHouseCell"></param>
        /// <returns>-1:发货单ID不为在途状态 -2:物流码无效 >0:退货产品数量</returns>
        public int ReturnByOneCode(int DeliveryID, string Code, int WareHouseCell)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@DeliveryID", SqlDbType.Int, 4, DeliveryID),
				SQLDatabase.MakeInParam("@Code", SqlDbType.VarChar, 50, Code),
                SQLDatabase.MakeInParam("@WareHouseCell", SqlDbType.Int, 4, WareHouseCell)            
			};
            #endregion

            return SQLDatabase.RunProc(_ProcePrefix + "_ReturnByOneCode", prams);
        }

        /// <summary>
        /// 按单个物流码破损
        /// </summary>
        /// <param name="DeliveryID"></param>
        /// <param name="Code"></param>
        /// <returns>-1:发货单ID不为在途状态 -2:物流码无效 >0:退货产品数量</returns>
        public int BrokenByOneCode(int DeliveryID, string Code)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@DeliveryID", SqlDbType.Int, 4, DeliveryID),
				SQLDatabase.MakeInParam("@Code", SqlDbType.VarChar, 50, Code)                
			};
            #endregion

            return SQLDatabase.RunProc(_ProcePrefix + "_BrokenByOneCode", prams);
        }

        /// <summary>
        /// 按单个物流码丢失
        /// </summary>
        /// <param name="DeliveryID"></param>
        /// <param name="Code"></param>
        /// <returns>-1:发货单ID不为在途状态 -2:物流码无效 >0:丢失产品数量</returns>
        public int LostByOneCode(int DeliveryID, string Code)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@DeliveryID", SqlDbType.Int, 4, DeliveryID),
				SQLDatabase.MakeInParam("@Code", SqlDbType.VarChar, 50, Code)                
			};
            #endregion

            return SQLDatabase.RunProc(_ProcePrefix + "_LostByOneCode", prams);
        }
        #endregion

        #region 直接签收到下游商户库存
        /// <summary>
        /// 根据物流码直接从指定发货商签收入库到下游商户库存
        /// </summary>
        /// <param name="SupplierWareHouse">供货商仓库</param>
        /// <param name="ClientWareHouse">收货商仓库</param>
        /// <param name="ClientWareHouseCell">签收库位</param>
        /// <param name="Codes">签收物流码</param>
        /// <param name="SignInUser">签收操作用户</param>
        /// <returns>大于0:发货单号 -10:物流码不是归属同一供货商仓库 -11:供货商与收货商间不存在供货关系	-12:无产品码符合收货规则</returns>
        public int SignInNoDeliverySheet(int SupplierWareHouse, int ClientWareHouse, int ClientWareHouseCell, string Codes, Guid SignInUser)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@SupplierWareHouse", SqlDbType.Int, 4, SupplierWareHouse),
                SQLDatabase.MakeInParam("@ClientWareHouse", SqlDbType.Int, 4, ClientWareHouse),
                SQLDatabase.MakeInParam("@ClientWareHouseCell", SqlDbType.Int, 4, ClientWareHouseCell),
				SQLDatabase.MakeInParam("@Codes", SqlDbType.VarChar, Codes.Length, Codes)   ,
                SQLDatabase.MakeInParam("@SignInUser", SqlDbType.UniqueIdentifier, 16, SignInUser)
			};
            #endregion

            return SQLDatabase.RunProc(_ProcePrefix + "_SignInNoDeliverySheet", prams);
        }
        #endregion

        #region 查询已装车扫描的产品码
        /// <summary>
        /// 查询已装车扫描的产品码
        /// </summary>
        /// <param name="PutInID">入库单号</param>
        /// <param name="DisplayMode">1:按产品统计 2:按箱码统计 3:产品码明细</param>
        /// <returns></returns>
        public DataTable GetDeliveryCodeLib(int DeliveryID, int DisplayMode)
        {
            SqlDataReader dr = null;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@DeliveryID", SqlDbType.Int, 4, DeliveryID),
                SQLDatabase.MakeInParam("@DisplayMode", SqlDbType.Int, 4, DisplayMode)
			};
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_GetDeliveryCodeLib", prams, out dr);

            return Tools.ConvertDataReaderToDataTable(dr);
        }
        #endregion

        #region 获取指定客户某产品的备单状态下待发货数量
        public int GetNeedDeliveryQuantityByProduct(int Supplier, int Product, int WareHouse)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Supplier", SqlDbType.Int, 4, Supplier),
                SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, Product),
				SQLDatabase.MakeInParam("@WareHouse", SqlDbType.Int, 4, WareHouse)
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_GetNeedDeliveryQuantityByProduct", prams);

            return ret;
        }
        #endregion

        #region 获取指定客户某时间段内的收货及发货明细
        /// <summary>
        /// 获取指定客户的收货明细
        /// </summary>
        /// <param name="Client">收货客户</param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="DeliveryClassify">货单类别</param>
        /// <param name="PDTBrand">产品品牌</param>
        /// <param name="PDTClassify">产品系列</param>
        /// <param name="Product">产品</param>
        /// <returns></returns>
        public DataTable GetDeliveryInList(int Client, DateTime BeginDate, DateTime EndDate, int DeliveryClassify, int PDTBrand, int PDTClassify, int Product)
        {
            SqlDataReader dr = null;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, Client),
                SQLDatabase.MakeInParam("@BeginDate", SqlDbType.DateTime, 8, BeginDate),
				SQLDatabase.MakeInParam("@EndDate", SqlDbType.DateTime, 8, EndDate),
                SQLDatabase.MakeInParam("@DeliveryClassify", SqlDbType.Int, 4, DeliveryClassify),
                SQLDatabase.MakeInParam("@PDTBrand", SqlDbType.Int, 4, PDTBrand),
                SQLDatabase.MakeInParam("@PDTClassify", SqlDbType.Int, 4, PDTClassify),
                SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, Product)
                
			};
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_GetDeliveryInList", prams, out dr);

            return Tools.ConvertDataReaderToDataTable(dr);
        }

        /// <summary>
        /// 获取指定客户的发货明细
        /// </summary>
        /// <param name="Supplier">发货客户</param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="DeliveryClassify">货单类别</param>
        /// <param name="PDTBrand">产品品牌</param>
        /// <param name="PDTClassify">产品系列</param>
        /// <param name="Product">产品</param>
        /// <param name="Client">收货客户</param>
        /// <returns></returns>
        public DataTable GetDeliveryOutList(int Supplier, DateTime BeginDate, DateTime EndDate, int DeliveryClassify, int PDTBrand, int PDTClassify, int Product, int Client)
        {
            SqlDataReader dr = null;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Supplier", SqlDbType.Int, 4, Supplier),
                SQLDatabase.MakeInParam("@BeginDate", SqlDbType.DateTime, 8, BeginDate),
				SQLDatabase.MakeInParam("@EndDate", SqlDbType.DateTime, 8, EndDate),
                SQLDatabase.MakeInParam("@DeliveryClassify", SqlDbType.Int, 4, DeliveryClassify),
                SQLDatabase.MakeInParam("@PDTBrand", SqlDbType.Int, 4, PDTBrand),
                SQLDatabase.MakeInParam("@PDTClassify", SqlDbType.Int, 4, PDTClassify),
                SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, Product),
                SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, Client)
                
			};
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_GetDeliveryOutList", prams, out dr);

            return Tools.ConvertDataReaderToDataTable(dr);
        }

        /// <summary>
        /// 获取指定客户的收退货明细
        /// </summary>
        /// <param name="Client">收货客户</param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="DeliveryClassify">货单类别</param>
        /// <param name="PDTBrand">产品品牌</param>
        /// <param name="PDTClassify">产品系列</param>
        /// <param name="Product">产品</param>
        /// <param name="Supplier">退货客户</param>
        /// <returns></returns>
        public DataTable GetReturnInList(int Client, DateTime BeginDate, DateTime EndDate, int PDTBrand, int PDTClassify, int Product, int Supplier)
        {
            SqlDataReader dr = null;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, Client),
                SQLDatabase.MakeInParam("@BeginDate", SqlDbType.DateTime, 8, BeginDate),
				SQLDatabase.MakeInParam("@EndDate", SqlDbType.DateTime, 8, EndDate),
                SQLDatabase.MakeInParam("@PDTBrand", SqlDbType.Int, 4, PDTBrand),
                SQLDatabase.MakeInParam("@PDTClassify", SqlDbType.Int, 4, PDTClassify),
                SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, Product),
                SQLDatabase.MakeInParam("@Supplier", SqlDbType.Int, 4, Supplier)
               
			};
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_GetReturnInList", prams, out dr);

            return Tools.ConvertDataReaderToDataTable(dr);
        }

        /// <summary>
        /// 获取指定客户的发出退货明细
        /// </summary>
        /// <param name="Supplier">发出退货客户</param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="DeliveryClassify">货单类别</param>
        /// <param name="PDTBrand">产品品牌</param>
        /// <param name="PDTClassify">产品系列</param>
        /// <param name="Product">产品</param>
        /// <returns></returns>
        public DataTable GetReturnOutList(int Supplier, DateTime BeginDate, DateTime EndDate, int PDTBrand, int PDTClassify, int Product)
        {
            SqlDataReader dr = null;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Supplier", SqlDbType.Int, 4, Supplier),
                SQLDatabase.MakeInParam("@BeginDate", SqlDbType.DateTime, 8, BeginDate),
				SQLDatabase.MakeInParam("@EndDate", SqlDbType.DateTime, 8, EndDate),
                SQLDatabase.MakeInParam("@PDTBrand", SqlDbType.Int, 4, PDTBrand),
                SQLDatabase.MakeInParam("@PDTClassify", SqlDbType.Int, 4, PDTClassify),
                SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, Product)                        
			};
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_GetReturnOutList", prams, out dr);

            return Tools.ConvertDataReaderToDataTable(dr);
        }
        #endregion

        #region 获取发货出错的物流码(应发客户与实际签收客户不一致的物流码)
        public DataTable GetDeliveryErrorCodeLibList(int Supplier, int ActClient, DateTime BeginDate, DateTime EndDate)
        {
            SqlDataReader dr = null;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Supplier", SqlDbType.Int, 4, Supplier),
                SQLDatabase.MakeInParam("@ActClient", SqlDbType.Int, 4, ActClient),                 
                SQLDatabase.MakeInParam("@BeginDate", SqlDbType.DateTime, 8, BeginDate),
				SQLDatabase.MakeInParam("@EndDate", SqlDbType.DateTime, 8, EndDate)
			};
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_GetDeliveryErrorCodeLibList", prams, out dr);

            return Tools.ConvertDataReaderToDataTable(dr);
        }
        #endregion
    }
}

