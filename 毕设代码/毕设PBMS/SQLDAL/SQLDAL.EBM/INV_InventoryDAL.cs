
// ===================================================================
// 文件： INV_InventoryDAL.cs
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
using MCSFramework.Common;


namespace MCSFramework.SQLDAL.EBM
{
    /// <summary>
    ///INV_Inventory数据访问DAL类
    /// </summary>
    public class INV_InventoryDAL : BaseSimpleDAL<INV_Inventory>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public INV_InventoryDAL()
        {
            _ProcePrefix = "MCS_EBM.dbo.sp_INV_Inventory";
            _ConnectionStirng = "MCS_EBM_ConnectionString";
        }
        #endregion


        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(INV_Inventory m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@WareHouse", SqlDbType.Int, 4, m.WareHouse),
				SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, m.Product),
				SQLDatabase.MakeInParam("@LotNumber", SqlDbType.VarChar, 50, m.LotNumber),
				SQLDatabase.MakeInParam("@Quantity", SqlDbType.Int, 4, m.Quantity),
				SQLDatabase.MakeInParam("@Price", SqlDbType.Decimal, 9, m.Price),
				SQLDatabase.MakeInParam("@LastUpdateTime", SqlDbType.DateTime, 8, m.LastUpdateTime),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 2000, m.Remark),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            m.ID = SQLDatabase.RunProc(_ConnectionStirng, _ProcePrefix + "_Add", prams);

            return m.ID;
        }

        /// <summary>
        /// 更新项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Update(INV_Inventory m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@WareHouse", SqlDbType.Int, 4, m.WareHouse),
				SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, m.Product),
				SQLDatabase.MakeInParam("@LotNumber", SqlDbType.VarChar, 50, m.LotNumber),
				SQLDatabase.MakeInParam("@Quantity", SqlDbType.Int, 4, m.Quantity),
				SQLDatabase.MakeInParam("@Price", SqlDbType.Decimal, 9, m.Price),
				SQLDatabase.MakeInParam("@LastUpdateTime", SqlDbType.DateTime, 8, m.LastUpdateTime),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 2000, m.Remark),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ConnectionStirng, _ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override INV_Inventory FillModel(IDataReader dr)
        {
            INV_Inventory m = new INV_Inventory();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["WareHouse"].ToString())) m.WareHouse = (int)dr["WareHouse"];
            if (!string.IsNullOrEmpty(dr["Product"].ToString())) m.Product = (int)dr["Product"];
            if (!string.IsNullOrEmpty(dr["LotNumber"].ToString())) m.LotNumber = (string)dr["LotNumber"];
            if (!string.IsNullOrEmpty(dr["Quantity"].ToString())) m.Quantity = (int)dr["Quantity"];
            if (!string.IsNullOrEmpty(dr["Price"].ToString())) m.Price = (decimal)dr["Price"];
            if (!string.IsNullOrEmpty(dr["LastUpdateTime"].ToString())) m.LastUpdateTime = (DateTime)dr["LastUpdateTime"];
            if (!string.IsNullOrEmpty(dr["Remark"].ToString())) m.Remark = (string)dr["Remark"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);

            return m;
        }

        #region 增加或扣减库存
        public int IncreaseQuantity(int WareHouse, int Product, int Quantity, string LotNumber, Decimal Price)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@WareHouse", SqlDbType.Int, 4, WareHouse),
				SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, Product),
				SQLDatabase.MakeInParam("@LotNumber", SqlDbType.VarChar, 50, LotNumber),
				SQLDatabase.MakeInParam("@Quantity", SqlDbType.Int, 4, Quantity),
				SQLDatabase.MakeInParam("@Price", SqlDbType.Decimal, 9, Price)
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ConnectionStirng, _ProcePrefix + "_IncreaseQuantity", prams);

            return ret;
        }
        public int IncreaseQuantity(int WareHouse, int Product, int Quantity, string LotNumber)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@WareHouse", SqlDbType.Int, 4, WareHouse),
				SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, Product),
				SQLDatabase.MakeInParam("@LotNumber", SqlDbType.VarChar, 50, LotNumber),
				SQLDatabase.MakeInParam("@Quantity", SqlDbType.Int, 4, Quantity)
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ConnectionStirng, _ProcePrefix + "_IncreaseQuantity", prams);

            return ret;
        }

        public int DecreaseQuantity(int WareHouse, int Product, int Quantity, string LotNumber)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@WareHouse", SqlDbType.Int, 4, WareHouse),
				SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, Product),
				SQLDatabase.MakeInParam("@LotNumber", SqlDbType.VarChar, 50, LotNumber),
				SQLDatabase.MakeInParam("@Quantity", SqlDbType.Int, 4, Quantity)
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ConnectionStirng, _ProcePrefix + "_DecreaseQuantity", prams);

            return ret;
        }
        #endregion

        #region 根据指定的物流码增减库存(零售时或零售后退货时用)
        /// <summary>
        /// 根据产品物流码增加门店库存(零售后退货时用)
        /// </summary>
        /// <param name="PieceCode">物流码</param>
        /// <returns>0:成功 -1:物流码无效 -2:状态不为已零售状态</returns>
        public int IncreaseByPieceCode(string PieceCode)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@PieceCode", SqlDbType.VarChar, 50, PieceCode)
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ConnectionStirng, _ProcePrefix + "_IncreaseByPieceCode", prams);

            return ret;
        }

        /// <summary>
        /// 根据产品物流码扣减门店库存(零售时用)
        /// </summary>
        /// <param name="PieceCode">物流码</param>
        /// <returns>0:成功 -1:物流码无效 -2:状态不为在库状态</returns>
        public int DecreaseByPieceCode(string PieceCode)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@PieceCode", SqlDbType.VarChar, 50, PieceCode)
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ConnectionStirng, _ProcePrefix + "_DecreaseByPieceCode", prams);

            return ret;
        }

        /// <summary>
        /// 根据产品物流码扣减门店库存
        /// </summary>
        /// <param name="PieceCode">物流码</param>
        /// <param name="InvCodeLibState">9:零售 10:礼品兑换 11:作废 12:直销配货</param>
        /// <returns>0:成功 -1:物流码无效 -2:状态不为在库状态</returns>
        public int DecreaseByPieceCode(string PieceCode, int InvCodeLibState)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@PieceCode", SqlDbType.VarChar, 50, PieceCode),
                SQLDatabase.MakeInParam("@InvCodeLibState", SqlDbType.Int, 4, InvCodeLibState),
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ConnectionStirng, _ProcePrefix + "_DecreaseByPieceCode", prams);

            return ret;
        }
        #endregion

        #region 获取指定客户某产品的库存
        public int GetQuantityByProduct(int Client, int Product, int WareHouse, string LotNumber)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, Client),
                SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, Product),
				SQLDatabase.MakeInParam("@WareHouse", SqlDbType.Int, 4, WareHouse),
				SQLDatabase.MakeInParam("@LotNumber", SqlDbType.VarChar, 50, LotNumber)
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ConnectionStirng, _ProcePrefix + "_GetQuantityByProduct", prams);

            return ret;
        }
        #endregion

        #region 获取指定客户某产品的价格
        public decimal GetPriceByProduct(int Client, int Product, int WareHouse, string LotNumber)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, Client),
                SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, Product),
				SQLDatabase.MakeInParam("@WareHouse", SqlDbType.Int, 4, WareHouse),
				SQLDatabase.MakeInParam("@LotNumber", SqlDbType.VarChar, 50, LotNumber),
                new SqlParameter("@Price",SqlDbType.Decimal,9, ParameterDirection.Output,false,10,3,"Price", DataRowVersion.Default,0)
			};
            #endregion

            SQLDatabase.RunProc(_ConnectionStirng, _ProcePrefix + "_GetPriceByProduct", prams);

            decimal price = 0;
            try { price = (decimal)prams[4].Value; }
            catch { }
            return price;
        }
        #endregion

        /// <summary>
        /// 查询库存中的产品物流码
        /// </summary>
        /// <param name="WareHouse"></param>
        /// <param name="DisplayMode">1:按产品统计 2:按箱码统计 3:产品码明细</param>
        /// <param name="Product"></param>
        /// <param name="LotNumber"></param>
        /// <param name="State"></param>
        /// <returns></returns>
        public DataTable GetCodeLib(int WareHouse, int DisplayMode, int Product, string LotNumber, int State)
        {
            SqlDataReader dr = null;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@WareHouse", SqlDbType.Int, 4, WareHouse),
                SQLDatabase.MakeInParam("@DisplayMode", SqlDbType.Int, 4, DisplayMode),
                SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, Product),
                SQLDatabase.MakeInParam("@LotNumber", SqlDbType.VarChar, 50, LotNumber),
                SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, State)
			};
            #endregion

            SQLDatabase.RunProc(_ConnectionStirng, _ProcePrefix + "_GetCodeLib", prams, out dr);

            return Tools.ConvertDataReaderToDataTable(dr);
        }

        /// <summary>
        /// 查询门店进销存
        /// </summary>
        public DataTable GetClientJXC(int Retailer)
        {
            SqlDataReader dr = null;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Retailer", SqlDbType.Int, 4, Retailer)
			};
            #endregion

            SQLDatabase.RunProc(_ConnectionStirng, _ProcePrefix + "_GetClientJXC", prams, out dr);

            return Tools.ConvertDataReaderToDataTable(dr);
        }

        /// <summary>
        /// 统计指定范围内客户的进销存数据
        /// </summary>
        /// <param name="Month">查询月份，0:当前月</param>
        /// <param name="OrganizeCity">会员店所在的区域</param>
        /// <param name="ClientType">查询的客户类型</param>
        /// <param name="Supplier">0:不限供货商 大于0：按供货商查询</param>
        /// <param name="Client">0:所有客户  大于0：按客户查询</param>
        /// <returns></returns>
        public DataTable GetClientJXCSummary(int Month, int OrganizeCity, int ClientType, int Supplier, int Client)
        {
            SqlDataReader dr = null;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Month", SqlDbType.Int, 4, Month),
                SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, OrganizeCity),
                SQLDatabase.MakeInParam("@ClientType", SqlDbType.Int, 4, ClientType),
                SQLDatabase.MakeInParam("@Supplier", SqlDbType.Int, 4, Supplier),
                SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, Client)
			};
            #endregion

            SQLDatabase.RunProc(_ConnectionStirng, _ProcePrefix + "_GetClientJXCSummary", prams, out dr);

            return Tools.ConvertDataReaderToDataTable(dr);
        }

        /// <summary>
        /// 根据物流码查询追溯
        /// </summary>
        /// <param name="PieceCode"></param>
        /// <returns></returns>
        public DataTable TraceQuery(string PieceCode)
        {
            SqlDataReader dr = null;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@PieceCode", SqlDbType.VarChar, 50, PieceCode)
			};
            #endregion

            SQLDatabase.RunProc(_ConnectionStirng, _ProcePrefix + "_TraceQuery", prams, out dr);

            return Tools.ConvertDataReaderToDataTable(dr);
        }

        #region 获取客户库存明细(含有效期信息)
        /// <summary>
        /// 获取客户库存明细(含有效期信息)
        /// </summary>
        /// <param name="Client"></param>
        /// <param name="WareHouse">仓库</param>
        /// <param name="PDTBrand">产品品牌</param>
        /// <param name="PDTClassify">产品系列</param>
        /// <param name="Product">产品</param>
        /// <returns></returns>
        public DataTable GetProductList(int OrganizeCity, int ClientType, int Supplier, int Client, int WareHouse, int PDTBrand, int PDTClassify, int Product)
        {
            SqlDataReader dr = null;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, OrganizeCity),
                SQLDatabase.MakeInParam("@ClientType", SqlDbType.Int, 4, ClientType),
                SQLDatabase.MakeInParam("@Supplier", SqlDbType.Int, 4, Supplier),
                SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, Client),
                SQLDatabase.MakeInParam("@WareHouse", SqlDbType.Int, 4, WareHouse),
                SQLDatabase.MakeInParam("@PDTBrand", SqlDbType.Int, 4, PDTBrand),
                SQLDatabase.MakeInParam("@PDTClassify", SqlDbType.Int, 4, PDTClassify),
                SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, Product)
               
			};
            #endregion

            SQLDatabase.RunProc(_ConnectionStirng, _ProcePrefix + "_GetProductList", prams, out dr);

            return Tools.ConvertDataReaderToDataTable(dr);
        }
        #endregion

    }
}

