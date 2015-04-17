
// ===================================================================
// 文件： ORD_OrderDAL.cs
// 项目名称：
// 创建时间：2012-7-21
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.EBM;
using System.Collections.Generic;
using MCSFramework.Common;


namespace MCSFramework.SQLDAL.EBM
{
    /// <summary>
    ///ORD_Order数据访问DAL类
    /// </summary>
    public class ORD_OrderDAL : BaseComplexDAL<ORD_Order, ORD_OrderDetail>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public ORD_OrderDAL()
        {
            _ProcePrefix = "MCS_EBM.dbo.sp_ORD_Order";
        }
        #endregion

        #region 基本操作
        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(ORD_Order m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@SheetCode", SqlDbType.VarChar, 100, m.SheetCode),
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, m.AccountMonth),
				SQLDatabase.MakeInParam("@Supplier", SqlDbType.Int, 4, m.Supplier),
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, m.Client),
				SQLDatabase.MakeInParam("@PublishID", SqlDbType.Int, 4, m.PublishID),
				SQLDatabase.MakeInParam("@Type", SqlDbType.Int, 4, m.Type),
				SQLDatabase.MakeInParam("@Classify", SqlDbType.Int, 4, m.Classify),
				SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, m.State),
				SQLDatabase.MakeInParam("@PayMode", SqlDbType.Int, 4, m.PayMode),
				SQLDatabase.MakeInParam("@BalanceFlag", SqlDbType.Int, 4, m.BalanceFlag),
				SQLDatabase.MakeInParam("@ReqArrivalDate", SqlDbType.DateTime, 8, m.ReqArrivalDate),
				SQLDatabase.MakeInParam("@ReqWarehouse", SqlDbType.Int, 4, m.ReqWarehouse),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 2000, m.Remark),
				SQLDatabase.MakeInParam("@SubmitTime", SqlDbType.DateTime, 8, m.SubmitTime),
				SQLDatabase.MakeInParam("@ConfirmTime", SqlDbType.DateTime, 8, m.ConfirmTime),
				SQLDatabase.MakeInParam("@BalanceTime", SqlDbType.DateTime, 8, m.BalanceTime),
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
        public override int Update(ORD_Order m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@SheetCode", SqlDbType.VarChar, 100, m.SheetCode),
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, m.AccountMonth),
				SQLDatabase.MakeInParam("@Supplier", SqlDbType.Int, 4, m.Supplier),
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, m.Client),
				SQLDatabase.MakeInParam("@PublishID", SqlDbType.Int, 4, m.PublishID),
				SQLDatabase.MakeInParam("@Type", SqlDbType.Int, 4, m.Type),
				SQLDatabase.MakeInParam("@Classify", SqlDbType.Int, 4, m.Classify),
				SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, m.State),
				SQLDatabase.MakeInParam("@PayMode", SqlDbType.Int, 4, m.PayMode),
				SQLDatabase.MakeInParam("@BalanceFlag", SqlDbType.Int, 4, m.BalanceFlag),
				SQLDatabase.MakeInParam("@ReqArrivalDate", SqlDbType.DateTime, 8, m.ReqArrivalDate),
				SQLDatabase.MakeInParam("@ReqWarehouse", SqlDbType.Int, 4, m.ReqWarehouse),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 2000, m.Remark),
				SQLDatabase.MakeInParam("@SubmitTime", SqlDbType.DateTime, 8, m.SubmitTime),
				SQLDatabase.MakeInParam("@ConfirmTime", SqlDbType.DateTime, 8, m.ConfirmTime),
				SQLDatabase.MakeInParam("@BalanceTime", SqlDbType.DateTime, 8, m.BalanceTime),
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

        protected override ORD_Order FillModel(IDataReader dr)
        {
            ORD_Order m = new ORD_Order();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["SheetCode"].ToString())) m.SheetCode = (string)dr["SheetCode"];
            if (!string.IsNullOrEmpty(dr["AccountMonth"].ToString())) m.AccountMonth = (int)dr["AccountMonth"];
            if (!string.IsNullOrEmpty(dr["Supplier"].ToString())) m.Supplier = (int)dr["Supplier"];
            if (!string.IsNullOrEmpty(dr["Client"].ToString())) m.Client = (int)dr["Client"];
            if (!string.IsNullOrEmpty(dr["PublishID"].ToString())) m.PublishID = (int)dr["PublishID"];
            if (!string.IsNullOrEmpty(dr["Type"].ToString())) m.Type = (int)dr["Type"];
            if (!string.IsNullOrEmpty(dr["Classify"].ToString())) m.Classify = (int)dr["Classify"];
            if (!string.IsNullOrEmpty(dr["State"].ToString())) m.State = (int)dr["State"];
            if (!string.IsNullOrEmpty(dr["PayMode"].ToString())) m.PayMode = (int)dr["PayMode"];
            if (!string.IsNullOrEmpty(dr["BalanceFlag"].ToString())) m.BalanceFlag = (int)dr["BalanceFlag"];
            if (!string.IsNullOrEmpty(dr["ReqArrivalDate"].ToString())) m.ReqArrivalDate = (DateTime)dr["ReqArrivalDate"];
            if (!string.IsNullOrEmpty(dr["ReqWarehouse"].ToString())) m.ReqWarehouse = (int)dr["ReqWarehouse"];
            if (!string.IsNullOrEmpty(dr["Remark"].ToString())) m.Remark = (string)dr["Remark"];
            if (!string.IsNullOrEmpty(dr["SubmitTime"].ToString())) m.SubmitTime = (DateTime)dr["SubmitTime"];
            if (!string.IsNullOrEmpty(dr["ConfirmTime"].ToString())) m.ConfirmTime = (DateTime)dr["ConfirmTime"];
            if (!string.IsNullOrEmpty(dr["BalanceTime"].ToString())) m.BalanceTime = (DateTime)dr["BalanceTime"];
            if (!string.IsNullOrEmpty(dr["ApproveTask"].ToString())) m.ApproveTask = (int)dr["ApproveTask"];
            if (!string.IsNullOrEmpty(dr["ApproveFlag"].ToString())) m.ApproveFlag = (int)dr["ApproveFlag"];
            if (!string.IsNullOrEmpty(dr["InsertTime"].ToString())) m.InsertTime = (DateTime)dr["InsertTime"];
            if (!string.IsNullOrEmpty(dr["InsertUser"].ToString())) m.InsertUser = (Guid)dr["InsertUser"];
            if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString())) m.UpdateTime = (DateTime)dr["UpdateTime"];
            if (!string.IsNullOrEmpty(dr["UpdateUser"].ToString())) m.UpdateUser = (Guid)dr["UpdateUser"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);

            return m;
        }

        public override int AddDetail(ORD_OrderDetail m)
        {
            m.OrderID = HeadID;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@OrderID", SqlDbType.Int, 4, m.OrderID),
				SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, m.Product),
				SQLDatabase.MakeInParam("@Price", SqlDbType.Decimal, 9, m.Price),
                SQLDatabase.MakeInParam("@Points", SqlDbType.Decimal, 9, m.Points),
				SQLDatabase.MakeInParam("@BookQuantity", SqlDbType.Int, 4, m.BookQuantity),
				SQLDatabase.MakeInParam("@ConfirmQuantity", SqlDbType.Int, 4, m.ConfirmQuantity),
				SQLDatabase.MakeInParam("@AdjustReason", SqlDbType.VarChar, 2000, m.AdjustReason),
				SQLDatabase.MakeInParam("@DeliveredQuantity", SqlDbType.Int, 4, m.DeliveredQuantity),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 1000, m.Remark),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            m.ID = SQLDatabase.RunProc(_ProcePrefix + "_AddDetail", prams);

            return m.ID;
        }

        public override int UpdateDetail(ORD_OrderDetail m)
        {
            m.OrderID = HeadID;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@OrderID", SqlDbType.Int, 4, m.OrderID),
				SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, m.Product),
				SQLDatabase.MakeInParam("@Price", SqlDbType.Decimal, 9, m.Price),
                SQLDatabase.MakeInParam("@Points", SqlDbType.Decimal, 9, m.Points),
				SQLDatabase.MakeInParam("@BookQuantity", SqlDbType.Int, 4, m.BookQuantity),
				SQLDatabase.MakeInParam("@ConfirmQuantity", SqlDbType.Int, 4, m.ConfirmQuantity),
				SQLDatabase.MakeInParam("@AdjustReason", SqlDbType.VarChar, 2000, m.AdjustReason),
				SQLDatabase.MakeInParam("@DeliveredQuantity", SqlDbType.Int, 4, m.DeliveredQuantity),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 1000, m.Remark),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_UpdateDetail", prams);

            return ret;
        }

        protected override ORD_OrderDetail FillDetailModel(IDataReader dr)
        {
            ORD_OrderDetail m = new ORD_OrderDetail();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["OrderID"].ToString())) m.OrderID = (int)dr["OrderID"];
            if (!string.IsNullOrEmpty(dr["Product"].ToString())) m.Product = (int)dr["Product"];
            if (!string.IsNullOrEmpty(dr["Price"].ToString())) m.Price = (decimal)dr["Price"];
            if (!string.IsNullOrEmpty(dr["Points"].ToString())) m.Points = (decimal)dr["Points"];
            if (!string.IsNullOrEmpty(dr["BookQuantity"].ToString())) m.BookQuantity = (int)dr["BookQuantity"];
            if (!string.IsNullOrEmpty(dr["ConfirmQuantity"].ToString())) m.ConfirmQuantity = (int)dr["ConfirmQuantity"];
            if (!string.IsNullOrEmpty(dr["AdjustReason"].ToString())) m.AdjustReason = (string)dr["AdjustReason"];
            if (!string.IsNullOrEmpty(dr["DeliveredQuantity"].ToString())) m.DeliveredQuantity = (int)dr["DeliveredQuantity"];
            if (!string.IsNullOrEmpty(dr["Remark"].ToString())) m.Remark = (string)dr["Remark"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);


            return m;
        }
        #endregion

        #region 生成订货单号
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

        /// <summary>
        /// 设置订单状态
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="State"></param>
        /// <param name="User"></param>
        /// <returns></returns>
        public int SetState(int ID, int State, Guid User)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, ID),
                SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, State),
				SQLDatabase.MakeInParam("@User", SqlDbType.UniqueIdentifier, 16,User)                
			};
            #endregion

            return SQLDatabase.RunProc(_ProcePrefix + "_SetState", prams);
        }

        /// <summary>
        /// 设置订单完成结算
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="PayMode"></param>
        /// <param name="User"></param>
        /// <returns></returns>
        public int SetBalanced(int ID, int PayMode, Guid User)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, ID),
                SQLDatabase.MakeInParam("@PayMode", SqlDbType.Int, 4, PayMode),
				SQLDatabase.MakeInParam("@User", SqlDbType.UniqueIdentifier, 16,User)                
			};
            #endregion

            return SQLDatabase.RunProc(_ProcePrefix + "_SetBalanced", prams);
        }

        /// <summary>
        /// 获取可以发货的订单列表
        /// </summary>
        /// <param name="Supplier"></param>
        /// <param name="Client"></param>
        /// <returns></returns>
        public IList<ORD_Order> GetCanDeliveryOrderList(int Supplier, int Client)
        {
            SqlDataReader dr = null;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Supplier", SqlDbType.Int, 4, Supplier),
                SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, Client)               
			};
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_GetCanDeliveryOrderList", prams, out dr);

            return FillModelList(dr);
        }

        /// <summary>
        /// 获取指定订单中可以发货的产品明细
        /// </summary>
        /// <param name="OrderID"></param>
        /// <returns></returns>
        public DataTable GetCanDeliveryOrderDetail(int OrderID)
        {
            SqlDataReader dr = null;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@OrderID", SqlDbType.Int, 4, OrderID)             
			};
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_GetCanDeliveryOrderDetail", prams, out dr);

            return Tools.ConvertDataReaderToDataTable(dr);
        }

        #region 积分兑换订单的积分扣减情况
        /// <summary>
        /// 增加订单的扣减积分情况
        /// </summary>
        /// <param name="OrderID"></param>
        /// <param name="AccountType"></param>
        /// <param name="Flag"></param>
        /// <param name="Points"></param>
        /// <returns></returns>
        public int AddSpentPoints(int OrderID, int AccountType, int Flag, decimal Points)
        {
            #region 设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@OrderID", SqlDbType.Int, 4, OrderID),             
                SQLDatabase.MakeInParam("@AccountType", SqlDbType.Int, 4, AccountType),             
                SQLDatabase.MakeInParam("@Flag", SqlDbType.Int, 4, Flag),             
                SQLDatabase.MakeInParam("@Points", SqlDbType.Decimal, 18, Points),             
			};
            #endregion

            return SQLDatabase.RunProc(_ProcePrefix + "_AddSpentPoints", prams);
        }

        /// <summary>
        /// 获取订单的积分扣减(或返还)信息
        /// </summary>
        /// <param name="OrderID"></param>
        /// <returns></returns>
        public IList<ORD_OrderSpentPoints> GetSpentPointsList(int OrderID)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@OrderID", SqlDbType.Int, 4, OrderID)
			};
            #endregion

            SqlDataReader dr = null;
            SQLDatabase.RunProc(_ProcePrefix + "_GetSpentPoints", prams, out dr);

            IList<ORD_OrderSpentPoints> list = new List<ORD_OrderSpentPoints>();
            if (dr != null)
            {
                while (dr.Read())
                {
                    list.Add(FillModel_SpentPoints(dr));
                }
                dr.Close();
            }
            return list;
        }

        protected ORD_OrderSpentPoints FillModel_SpentPoints(IDataReader dr)
        {
            ORD_OrderSpentPoints m = new ORD_OrderSpentPoints();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["OrderID"].ToString())) m.OrderID = (int)dr["OrderID"];
            if (!string.IsNullOrEmpty(dr["AccountType"].ToString())) m.AccountType = (int)dr["AccountType"];
            if (!string.IsNullOrEmpty(dr["Flag"].ToString())) m.Flag = (int)dr["Flag"];
            if (!string.IsNullOrEmpty(dr["Points"].ToString())) m.Points = (decimal)dr["Points"];
            if (!string.IsNullOrEmpty(dr["InsertTime"].ToString())) m.InsertTime = (DateTime)dr["InsertTime"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);

            return m;
        }
        #endregion

        #region 获取某个客户指定月份、指定产品已订货数量
        public int GetSubmitQuantity(int AccountMonth, int Client, int Product, int OrderClassify)
        {
            #region	设置参数集
            SqlParameter[] prams = {
                SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, AccountMonth),
                SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, Client), 
                SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, Product),
				SQLDatabase.MakeInParam("@OrderClassify", SqlDbType.Int, 4, OrderClassify)             
			};
            #endregion

            return SQLDatabase.RunProc(_ProcePrefix + "_GetSubmitQuantity", prams);
        }
        #endregion

        /// <summary>
        /// 统计各客户订货汇总信息
        /// </summary>
        /// <param name="OrganizeCity"></param>
        /// <param name="Supplier"></param>
        /// <param name="Client"></param>
        /// <param name="ClientType"></param>
        /// <param name="OrderType"></param>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public DataTable GetClientSummary(int OrganizeCity, int Supplier, int Client, int ClientType, int OrderType, DateTime BeginDate, DateTime EndDate)
        {
            SqlDataReader dr = null;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, OrganizeCity),
				SQLDatabase.MakeInParam("@Supplier", SqlDbType.Int, 4, Supplier),
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, Client),
                SQLDatabase.MakeInParam("@ClientType", SqlDbType.Int, 4, ClientType),
                SQLDatabase.MakeInParam("@OrderType", SqlDbType.Int, 4, OrderType),
				SQLDatabase.MakeInParam("@BeginDate", SqlDbType.DateTime, 8, BeginDate),
				SQLDatabase.MakeInParam("@EndDate", SqlDbType.DateTime, 8, EndDate)
			};
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_GetClientSummary", prams, out dr);
            return MCSFramework.Common.Tools.ConvertDataReaderToDataTable(dr);
        }
    }
}

