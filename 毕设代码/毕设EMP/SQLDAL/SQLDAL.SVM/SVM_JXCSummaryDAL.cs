
// ===================================================================
// 文件： SVM_JXCSummaryDAL.cs
// 项目名称：
// 创建时间：2010/7/8
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.SVM;
using MCSFramework.Common;
using MCSFramework.Model.Pub;
using MCSFramework.SQLDAL.Pub;


namespace MCSFramework.SQLDAL.SVM
{
    /// <summary>
    ///SVM_JXCSummary数据访问DAL类
    /// </summary>
    public class SVM_JXCSummaryDAL : BaseSimpleDAL<SVM_JXCSummary>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public SVM_JXCSummaryDAL()
        {
            _ProcePrefix = "MCS_SVM.dbo.sp_SVM_JXCSummary";
        }
        #endregion


        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(SVM_JXCSummary m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, m.AccountMonth),
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, m.Client),
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, m.OrganizeCity),
				SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, m.Product),
				SQLDatabase.MakeInParam("@FactoryPrice", SqlDbType.Decimal, 9, m.FactoryPrice),
				SQLDatabase.MakeInParam("@SalesPrice", SqlDbType.Decimal, 9, m.SalesPrice),
				SQLDatabase.MakeInParam("@RetailPrice", SqlDbType.Decimal, 9, m.RetailPrice),
				SQLDatabase.MakeInParam("@BeginningInventory", SqlDbType.Int, 4, m.BeginningInventory),
				SQLDatabase.MakeInParam("@PurchaseVolume", SqlDbType.Int, 4, m.PurchaseVolume),
				SQLDatabase.MakeInParam("@SalesVolume", SqlDbType.Int, 4, m.SalesVolume),
				SQLDatabase.MakeInParam("@RecallVolume", SqlDbType.Int, 4, m.RecallVolume),
				SQLDatabase.MakeInParam("@ReturnedVolume", SqlDbType.Int, 4, m.ReturnedVolume),
				SQLDatabase.MakeInParam("@GiftVolume", SqlDbType.Int, 4, m.GiftVolume),
				SQLDatabase.MakeInParam("@EndingInventory", SqlDbType.Int, 4, m.EndingInventory),
				SQLDatabase.MakeInParam("@ComputInventory", SqlDbType.Int, 4, m.ComputInventory),
				SQLDatabase.MakeInParam("@TransitInventory", SqlDbType.Int, 4, m.TransitInventory),
				SQLDatabase.MakeInParam("@StaleInventory", SqlDbType.Int, 4, m.StaleInventory),
				SQLDatabase.MakeInParam("@ExpiredInventory", SqlDbType.Int, 4, m.ExpiredInventory),
                SQLDatabase.MakeInParam("@TransferInVolume", SqlDbType.Int, 4, m.TransferInVolume),
                SQLDatabase.MakeInParam("@TransferOutVolume", SqlDbType.Int, 4, m.TransferOutVolume),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@ApproveStaff", SqlDbType.Int, 4, m.ApproveStaff),
				SQLDatabase.MakeInParam("@InsertStaff", SqlDbType.Int, 4, m.InsertStaff),
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
        public override int Update(SVM_JXCSummary m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, m.AccountMonth),
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, m.Client),
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, m.OrganizeCity),
				SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, m.Product),
				SQLDatabase.MakeInParam("@FactoryPrice", SqlDbType.Decimal, 9, m.FactoryPrice),
				SQLDatabase.MakeInParam("@SalesPrice", SqlDbType.Decimal, 9, m.SalesPrice),
				SQLDatabase.MakeInParam("@RetailPrice", SqlDbType.Decimal, 9, m.RetailPrice),
				SQLDatabase.MakeInParam("@BeginningInventory", SqlDbType.Int, 4, m.BeginningInventory),
				SQLDatabase.MakeInParam("@PurchaseVolume", SqlDbType.Int, 4, m.PurchaseVolume),
				SQLDatabase.MakeInParam("@SalesVolume", SqlDbType.Int, 4, m.SalesVolume),
				SQLDatabase.MakeInParam("@RecallVolume", SqlDbType.Int, 4, m.RecallVolume),
				SQLDatabase.MakeInParam("@ReturnedVolume", SqlDbType.Int, 4, m.ReturnedVolume),
				SQLDatabase.MakeInParam("@GiftVolume", SqlDbType.Int, 4, m.GiftVolume),
				SQLDatabase.MakeInParam("@EndingInventory", SqlDbType.Int, 4, m.EndingInventory),
				SQLDatabase.MakeInParam("@ComputInventory", SqlDbType.Int, 4, m.ComputInventory),
				SQLDatabase.MakeInParam("@TransitInventory", SqlDbType.Int, 4, m.TransitInventory),
				SQLDatabase.MakeInParam("@StaleInventory", SqlDbType.Int, 4, m.StaleInventory),
				SQLDatabase.MakeInParam("@ExpiredInventory", SqlDbType.Int, 4, m.ExpiredInventory),
                SQLDatabase.MakeInParam("@TransferInVolume", SqlDbType.Int, 4, m.TransferInVolume),
                SQLDatabase.MakeInParam("@TransferOutVolume", SqlDbType.Int, 4, m.TransferOutVolume),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@ApproveStaff", SqlDbType.Int, 4, m.ApproveStaff),
				SQLDatabase.MakeInParam("@UpdateStaff", SqlDbType.Int, 4, m.UpdateStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override SVM_JXCSummary FillModel(IDataReader dr)
        {
            SVM_JXCSummary m = new SVM_JXCSummary();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["AccountMonth"].ToString())) m.AccountMonth = (int)dr["AccountMonth"];
            if (!string.IsNullOrEmpty(dr["Client"].ToString())) m.Client = (int)dr["Client"];
            if (!string.IsNullOrEmpty(dr["OrganizeCity"].ToString())) m.OrganizeCity = (int)dr["OrganizeCity"];
            if (!string.IsNullOrEmpty(dr["Product"].ToString())) m.Product = (int)dr["Product"];
            if (!string.IsNullOrEmpty(dr["FactoryPrice"].ToString())) m.FactoryPrice = (decimal)dr["FactoryPrice"];
            if (!string.IsNullOrEmpty(dr["SalesPrice"].ToString())) m.SalesPrice = (decimal)dr["SalesPrice"];
            if (!string.IsNullOrEmpty(dr["RetailPrice"].ToString())) m.RetailPrice = (decimal)dr["RetailPrice"];
            if (!string.IsNullOrEmpty(dr["BeginningInventory"].ToString())) m.BeginningInventory = (int)dr["BeginningInventory"];
            if (!string.IsNullOrEmpty(dr["PurchaseVolume"].ToString())) m.PurchaseVolume = (int)dr["PurchaseVolume"];
            if (!string.IsNullOrEmpty(dr["SignInVolume"].ToString())) m.SignInVolume = (int)dr["SignInVolume"];
            if (!string.IsNullOrEmpty(dr["SalesVolume"].ToString())) m.SalesVolume = (int)dr["SalesVolume"];
            if (!string.IsNullOrEmpty(dr["RecallVolume"].ToString())) m.RecallVolume = (int)dr["RecallVolume"];
            if (!string.IsNullOrEmpty(dr["ReturnedVolume"].ToString())) m.ReturnedVolume = (int)dr["ReturnedVolume"];
            if (!string.IsNullOrEmpty(dr["GiftVolume"].ToString())) m.GiftVolume = (int)dr["GiftVolume"];
            if (!string.IsNullOrEmpty(dr["EndingInventory"].ToString())) m.EndingInventory = (int)dr["EndingInventory"];
            if (!string.IsNullOrEmpty(dr["ComputInventory"].ToString())) m.ComputInventory = (int)dr["ComputInventory"];
            if (!string.IsNullOrEmpty(dr["TransitInventory"].ToString())) m.TransitInventory = (int)dr["TransitInventory"];
            if (!string.IsNullOrEmpty(dr["StaleInventory"].ToString())) m.StaleInventory = (int)dr["StaleInventory"];
            if (!string.IsNullOrEmpty(dr["ExpiredInventory"].ToString())) m.ExpiredInventory = (int)dr["ExpiredInventory"];
            if (!string.IsNullOrEmpty(dr["TransferInVolume"].ToString())) m.TransferInVolume = (int)dr["TransferInVolume"];
            if (!string.IsNullOrEmpty(dr["TransferOutVolume"].ToString())) m.TransferOutVolume = (int)dr["TransferOutVolume"];
            if (!string.IsNullOrEmpty(dr["ApproveFlag"].ToString())) m.ApproveFlag = (int)dr["ApproveFlag"];
            if (!string.IsNullOrEmpty(dr["ApproveStaff"].ToString())) m.ApproveStaff = (int)dr["ApproveStaff"];
            if (!string.IsNullOrEmpty(dr["InsertTime"].ToString())) m.InsertTime = (DateTime)dr["InsertTime"];
            if (!string.IsNullOrEmpty(dr["InsertStaff"].ToString())) m.InsertStaff = (int)dr["InsertStaff"];
            if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString())) m.UpdateTime = (DateTime)dr["UpdateTime"];
            if (!string.IsNullOrEmpty(dr["UpdateStaff"].ToString())) m.UpdateStaff = (int)dr["UpdateStaff"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);

            PDT_Product p = new PDT_ProductDAL().GetModel(m.Product);
            if (p != null)
            {
                m.ProductName = p.ShortName;
                m.ProductCode = p.Code;
                m.ConvertFactor = p.ConvertFactor;
                m.SubUnit = p.SubUnit;
            }
            return m;
        }

        #region 初始化指定客户的进销存
        /// <summary>
        /// 初始化指定客户的进销存
        /// </summary>
        /// <param name="Client"></param>
        /// <param name="AccountMonth"></param>
        /// <returns></returns>
        public int Init(int Client, int AccountMonth, int IsOpponent)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, Client),
                SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int,4, AccountMonth),
                SQLDatabase.MakeInParam("@IsOpponent", SqlDbType.Int,4, IsOpponent)
             };
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Init", prams);

            return ret;
        }

        /// <summary>
        /// 初始化指定客户的进销存
        /// </summary>
        /// <param name="Client"></param>
        /// <returns></returns>
        public int Init(int Client, int IsOpponent)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, Client),
                SQLDatabase.MakeInParam("@IsOpponent", SqlDbType.Int,4, IsOpponent)
             };
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Init", prams);

            return ret;
        }
        #endregion

        #region 按客户显示进销存汇总列表
        /// <summary>
        /// 按客户显示进销存汇总列表
        /// </summary>
        /// <param name="BeginMonth"></param>
        /// <param name="EndMonth"></param>
        /// <param name="PriceLevel">价格级别 1:出厂价 2:批发价 3:零售价</param>
        /// <param name="Supplier">供货商</param>
        /// <returns></returns>
        public DataTable GetSummaryListBySupplier(int BeginMonth, int EndMonth, int PriceLevel, int Supplier, int IsOpponent)
        {
            SqlDataReader dr = null;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@BeginMonth", SqlDbType.Int, 4, BeginMonth),
                SQLDatabase.MakeInParam("@EndMonth", SqlDbType.Int, 4, EndMonth),
                SQLDatabase.MakeInParam("@PriceLevel", SqlDbType.Int, 4, PriceLevel),
                SQLDatabase.MakeInParam("@Supplier", SqlDbType.Int, 4, Supplier),
                SQLDatabase.MakeInParam("@IsOpponent", SqlDbType.Int, 4, IsOpponent)
             };
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_GetSummaryList", prams, out dr);

            return Tools.ConvertDataReaderToDataTable(dr);
        }

        /// <summary>
        /// 按客户显示进销存汇总列表
        /// </summary>
        /// <param name="BeginMonth"></param>
        /// <param name="EndMonth"></param>
        /// <param name="PriceLevel">价格级别 1:出厂价 2:批发价 3:零售价</param>
        /// <param name="Client">客户</param>
        /// <returns></returns>
        public DataTable GetSummaryListByClient(int BeginMonth, int EndMonth, int PriceLevel, int Client, int IsOpponent)
        {
            SqlDataReader dr = null;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@BeginMonth", SqlDbType.Int, 4, BeginMonth),
                SQLDatabase.MakeInParam("@EndMonth", SqlDbType.Int, 4, EndMonth),
                SQLDatabase.MakeInParam("@PriceLevel", SqlDbType.Int, 4, PriceLevel),
                SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, Client),
                SQLDatabase.MakeInParam("@IsOpponent", SqlDbType.Int, 4, IsOpponent)
             };
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_GetSummaryList", prams, out dr);

            return Tools.ConvertDataReaderToDataTable(dr);
        }

        /// <summary>
        /// 按客户类别显示进销存汇总列表
        /// </summary>
        /// <param name="BeginMonth"></param>
        /// <param name="EndMonth"></param>
        /// <param name="PriceLevel"></param>
        /// <param name="Supplier"></param>
        /// <param name="ClientClassify"></param>
        /// <returns></returns>
        public DataTable GetSummaryListBySupplier(int BeginMonth, int EndMonth, int PriceLevel, int Supplier, int ClientType, int IsOpponent)
        {
            SqlDataReader dr = null;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@BeginMonth", SqlDbType.Int, 4, BeginMonth),
                SQLDatabase.MakeInParam("@EndMonth", SqlDbType.Int, 4, EndMonth),
                SQLDatabase.MakeInParam("@PriceLevel", SqlDbType.Int, 4, PriceLevel),
                SQLDatabase.MakeInParam("@Supplier", SqlDbType.Int, 4, Supplier),
                SQLDatabase.MakeInParam("@ClientType", SqlDbType.Int, 4, ClientType),
                SQLDatabase.MakeInParam("@IsOpponent", SqlDbType.Int, 4, IsOpponent)
             };
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_GetSummaryList", prams, out dr);

            return Tools.ConvertDataReaderToDataTable(dr);
        }
        /// <summary>
        /// 按客户类别显示进销存汇总列表
        /// </summary>
        /// <param name="BeginMonth"></param>
        /// <param name="EndMonth"></param>
        /// <param name="PriceLevel">价格级别 1:出厂价 2:批发价 3:零售价</param>
        /// <param name="ClientClassify">1:总经销商 2:分销商 3:促销店 4:返利店</param>
        /// <returns></returns>
        public DataTable GetSummaryListByClientClassify(int BeginMonth, int EndMonth, int PriceLevel, int OrganizeCity, int ClientClassify, int IsOpponent)
        {
            SqlDataReader dr = null;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@BeginMonth", SqlDbType.Int, 4, BeginMonth),
                SQLDatabase.MakeInParam("@EndMonth", SqlDbType.Int, 4, EndMonth),
                SQLDatabase.MakeInParam("@PriceLevel", SqlDbType.Int, 4, PriceLevel),
                SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, OrganizeCity),
                SQLDatabase.MakeInParam("@ClientClassify", SqlDbType.Int, 4, ClientClassify),
                SQLDatabase.MakeInParam("@IsOpponent", SqlDbType.Int, 4, IsOpponent)
             };
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_GetSummaryList", prams, out dr);

            return Tools.ConvertDataReaderToDataTable(dr);
        }
        #endregion

        #region 按供货商查询下游客户累计进销存统计
        /// <summary>
        /// 按供货商查询下游客户累计进销存统计
        /// </summary>
        /// <param name="Supplier">供货商</param>
        /// <param name="AccountMonth"></param>
        /// <param name="PriceLevel">0:数量 1:出厂价 2:批发价 3:零售价</param>
        /// <returns></returns>
        public DataTable GetProductListBySupplier(int Supplier, int AccountMonth, int PriceLevel, int IsOpponent)
        {
            SqlDataReader dr = null;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Supplier", SqlDbType.Int, 4, Supplier),
                SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, AccountMonth),
                SQLDatabase.MakeInParam("@PriceLevel", SqlDbType.Int, 4, PriceLevel),
                SQLDatabase.MakeInParam("@IsOpponent", SqlDbType.Int, 4, IsOpponent)
             };
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_GetProductListBySupplier", prams, out dr);

            return Tools.ConvertDataReaderToDataTable(dr);
        }
        /// <summary>
        ///  按供货商,下游客户类别查询累计进销存统计
        /// </summary>
        /// <param name="Supplier"></param>
        /// <param name="AccountMonth"></param>
        /// <param name="PriceLevel"></param>
        /// <returns></returns>
        public DataTable GetProductListBySupplier(int Supplier, int AccountMonth, int PriceLevel, int ClientType, int IsOpponent)
        {
            SqlDataReader dr = null;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Supplier", SqlDbType.Int, 4, Supplier),
                SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, AccountMonth),
                SQLDatabase.MakeInParam("@PriceLevel", SqlDbType.Int, 4, PriceLevel),
                SQLDatabase.MakeInParam("@ClientType", SqlDbType.Int, 4, ClientType),
                SQLDatabase.MakeInParam("@IsOpponent", SqlDbType.Int, 4, IsOpponent)
             };
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_GetProductListBySupplier", prams, out dr);

            return Tools.ConvertDataReaderToDataTable(dr);
        }
        #endregion

        #region 根据供应商获取下游客户按月汇总进销存
        public decimal GetMonthSummaryBySupplier(int Month, int PriceLevel, int Supplier, string FieldName, int Product, int IsOpponent)
        {
            SqlDataReader dr = null;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Month", SqlDbType.Int, 4, Month),
                SQLDatabase.MakeInParam("@PriceLevel", SqlDbType.Int, 4, PriceLevel),
                SQLDatabase.MakeInParam("@Supplier", SqlDbType.Int, 4, Supplier),
                SQLDatabase.MakeInParam("@FieldName",SqlDbType.VarChar,100,FieldName),
                SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, Product),
                SQLDatabase.MakeInParam("@IsOpponent", SqlDbType.Int, 4, IsOpponent)
             };
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_GetMonthSummaryBySupplier", prams, out dr);

            decimal summary = 0;
            try
            {
                if (dr.Read())
                {
                    summary = (decimal)dr[0];
                }
            }
            catch { }
            finally
            {
                dr.Close();
            }
            return summary;
        }
        #endregion

        #region 计算进销存总表中的计算库存数量
        /// <summary>
        /// 计算进销存总表中的计算库存数量
        /// </summary>
        /// <param name="Client"></param>
        /// <param name="AccountMonth"></param>
        /// <returns></returns>
        public int ComputInventory(int Client, int AccountMonth)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, Client),
                SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int,4, AccountMonth)
             };
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_ComputInventory", prams);

            return ret;
        }
        #endregion

        #region 审核通过
        public int Approve(int Client, int AccountMonth, int Staff, int IsOpponent)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, Client),
                SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int,4, AccountMonth),
                SQLDatabase.MakeInParam("@Staff", SqlDbType.Int,4, Staff),
                SQLDatabase.MakeInParam("@IsOpponent", SqlDbType.Int,4, IsOpponent),
             };
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Approve", prams);

            return ret;
        }

        public int BatApprove(string Clients, int AccountMonth, int Staff, int IsOpponent)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Client", SqlDbType.VarChar, Clients.Length, Clients),
                SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int,4, AccountMonth),
                SQLDatabase.MakeInParam("@Staff", SqlDbType.Int,4, Staff),
                SQLDatabase.MakeInParam("@IsOpponent", SqlDbType.Int,4, IsOpponent),
             };
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "BatApprove", prams);

            return ret;
        }

        public int CancelApprove(int Client, int AccountMonth, int Staff)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, Client),
                SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int,4, AccountMonth),
                SQLDatabase.MakeInParam("@Staff", SqlDbType.Int,4, Staff)
             };
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_CancelApprove", prams);

            return ret;
        }
        #endregion

        #region 删除进销存
        /// <summary>
        /// 删除进销存
        /// </summary>
        /// <param name="Client"></param>
        /// <param name="AccountMonth"></param>
        /// <returns></returns>
        public int DeleteJXC(int Client, int AccountMonth)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int,4, AccountMonth),
                SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, Client)
             };
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_DeleteJXC", prams);

            return ret;
        }
        #endregion

        /// <summary>
        /// 获取指定管理片区合计实际销量
        /// </summary>
        /// <param name="AccountMonth"></param>
        /// <param name="OrganizeCity"></param>
        /// <param name="IncludeChildOrganizeCity"></param>
        /// <returns></returns>
        public decimal GetTotalPurchaseVolume(int AccountMonth, int OrganizeCity, bool IncludeChildOrganizeCity)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, AccountMonth),
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, OrganizeCity),
                SQLDatabase.MakeInParam("@IncludeChildCity", SqlDbType.Int, 4, IncludeChildOrganizeCity ? 1 : 0),
                new SqlParameter("@TotalPurchaseVolume", SqlDbType.Decimal,18, ParameterDirection.Output,false,18,3,"TotalPurchaseVolume", DataRowVersion.Current,0)
            };
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_GetTotalPurchaseVolume", prams);
            return (decimal)prams[3].Value;
        }
    }
}

