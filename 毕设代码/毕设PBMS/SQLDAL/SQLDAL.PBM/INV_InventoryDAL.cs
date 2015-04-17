
// ===================================================================
// 文件： INV_InventoryDAL.cs
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
using System.Collections.Generic;


namespace MCSFramework.SQLDAL.PBM
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
            _ProcePrefix = "MCS_PBM.dbo.sp_INV_Inventory";
        }
        #endregion


        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(INV_Inventory m)
        {
            return -1;
            //库存不支持直接修改数量
            //#region	设置参数集
            //SqlParameter[] prams = {
            //    SQLDatabase.MakeInParam("@WareHouse", SqlDbType.Int, 4, m.WareHouse),
            //    SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, m.Product),
            //    SQLDatabase.MakeInParam("@LotNumber", SqlDbType.VarChar, 100, m.LotNumber),
            //    SQLDatabase.MakeInParam("@ProductDate", SqlDbType.DateTime, 8, m.ProductDate),
            //    SQLDatabase.MakeInParam("@Quantity", SqlDbType.Int, 4, m.Quantity),
            //    SQLDatabase.MakeInParam("@Price", SqlDbType.Decimal, 9, m.Price),
            //    SQLDatabase.MakeInParam("@LastUpdateTime", SqlDbType.DateTime, 8, m.LastUpdateTime),
            //    SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 1000, m.Remark),
            //    SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
            //};
            //#endregion

            //m.ID = SQLDatabase.RunProc(_ProcePrefix + "_Add", prams);

            //return m.ID;
        }

        /// <summary>
        /// 更新项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Update(INV_Inventory m)
        {
            return -1;
            //库存不支持直接修改数量
            //#region	设置参数集
            //SqlParameter[] prams = {
            //    SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
            //    SQLDatabase.MakeInParam("@WareHouse", SqlDbType.Int, 4, m.WareHouse),
            //    SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, m.Product),
            //    SQLDatabase.MakeInParam("@LotNumber", SqlDbType.VarChar, 100, m.LotNumber),
            //    SQLDatabase.MakeInParam("@ProductDate", SqlDbType.DateTime, 8, m.ProductDate),
            //    SQLDatabase.MakeInParam("@Quantity", SqlDbType.Int, 4, m.Quantity),
            //    SQLDatabase.MakeInParam("@Price", SqlDbType.Decimal, 9, m.Price),
            //    SQLDatabase.MakeInParam("@LastUpdateTime", SqlDbType.DateTime, 8, m.LastUpdateTime),
            //    SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 1000, m.Remark),
            //    SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
            //};
            //#endregion

            //int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);

            //return ret;
        }

        protected override INV_Inventory FillModel(IDataReader dr)
        {
            INV_Inventory m = new INV_Inventory();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (long)dr["ID"];
            if (!string.IsNullOrEmpty(dr["WareHouse"].ToString())) m.WareHouse = (int)dr["WareHouse"];
            if (!string.IsNullOrEmpty(dr["Product"].ToString())) m.Product = (int)dr["Product"];
            if (!string.IsNullOrEmpty(dr["LotNumber"].ToString())) m.LotNumber = (string)dr["LotNumber"];
            if (!string.IsNullOrEmpty(dr["ProductDate"].ToString())) m.ProductDate = (DateTime)dr["ProductDate"];
            if (!string.IsNullOrEmpty(dr["Quantity"].ToString())) m.Quantity = (int)dr["Quantity"];
            if (!string.IsNullOrEmpty(dr["Price"].ToString())) m.Price = (decimal)dr["Price"];
            if (!string.IsNullOrEmpty(dr["LastUpdateTime"].ToString())) m.LastUpdateTime = (DateTime)dr["LastUpdateTime"];
            if (!string.IsNullOrEmpty(dr["InsertTime"].ToString())) m.InsertTime = (DateTime)dr["InsertTime"];
            if (!string.IsNullOrEmpty(dr["Remark"].ToString())) m.Remark = (string)dr["Remark"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);

            return m;
        }

        /// <summary>
        /// 增加指定仓库商品库存
        /// </summary>
        /// <param name="WareHouse"></param>
        /// <param name="Product"></param>
        /// <param name="LotNumber"></param>
        /// <param name="Price"></param>
        /// <param name="Quantity"></param>
        /// <returns>0:成功</returns>
        public int IncreaseQuantity(int WareHouse, int Product, string LotNumber, decimal Price, int Quantity)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@WareHouse", SqlDbType.Int, 4, WareHouse),
				SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, Product),
				SQLDatabase.MakeInParam("@LotNumber", SqlDbType.VarChar, 100, LotNumber),
				SQLDatabase.MakeInParam("@Price", SqlDbType.Decimal, 9, Price),
                SQLDatabase.MakeInParam("@Quantity", SqlDbType.Int, 4, Quantity)
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_IncreaseQuantity", prams);

            return ret;
        }

        /// <summary>
        /// 扣减指定仓库商品库存
        /// </summary>
        /// <param name="WareHouse"></param>
        /// <param name="Product"></param>
        /// <param name="LotNumber"></param>
        /// <param name="Quantity"></param>
        /// <returns>0:成功 -1：库存不够</returns>
        public int DecreaseQuantity(int WareHouse, int Product, string LotNumber, int Quantity)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@WareHouse", SqlDbType.Int, 4, WareHouse),
				SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, Product),
				SQLDatabase.MakeInParam("@LotNumber", SqlDbType.VarChar, 100, LotNumber),
                SQLDatabase.MakeInParam("@Quantity", SqlDbType.Int, 4, Quantity)
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_DecreaseQuantity", prams);

            return ret;
        }

        /// <summary>
        /// 查询指定客户的指定仓库内某产品的库存
        /// </summary>
        /// <param name="WareHouse"></param>
        /// <param name="Product"></param>
        /// <param name="LotNumber"></param>
        /// <returns>库存数量</returns>
        public int GetProductQuantity(int WareHouse, int Product, string LotNumber)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@WareHouse", SqlDbType.Int, 4, WareHouse),
				SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, Product),
				SQLDatabase.MakeInParam("@LotNumber", SqlDbType.VarChar, 100, LotNumber)
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_GetProductQuantity", prams);

            return ret;
        }

        /// <summary>
        /// 查询指定客户的所有仓库内某产品的库存
        /// </summary>
        /// <param name="Client"></param>
        /// <param name="Product"></param>
        /// <param name="LotNumber"></param>
        /// <returns>库存数量</returns>
        public int GetProductQuantityAllWareHouse(int Client, int Product, string LotNumber)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, Client),
				SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, Product),
				SQLDatabase.MakeInParam("@LotNumber", SqlDbType.VarChar, 100, LotNumber)
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_GetProductQuantityAllWareHouse", prams);

            return ret;
        }


        /// <summary>
        /// 获取指定客户两年以内指定产品所有的库存批号信息（以便销售退货时可以选择判断）
        /// </summary>
        /// <param name="Client"></param>
        /// <param name="Product"></param>
        /// <returns></returns>
        public IList<INV_Inventory> GetListByClientAndProduct(int Client, int Product)
        {
            SqlDataReader dr = null;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, Client),
				SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, Product)
			};
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_GetListByClientAndProduct", prams, out dr);

            return FillModelList(dr);
        }

        /// <summary>
        /// 获取指定客户所有仓库内指定产品及批号的库存信息（以便销售退货时可以选择判断）
        /// </summary>
        /// <param name="Client"></param>
        /// <param name="Product"></param>
        /// <param name="LotNumber"></param>
        /// <returns></returns>
        public INV_Inventory GetInventoryByClientAndProduct(int Client, int Product, string LotNumber)
        {
            SqlDataReader dr = null;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, Client),
				SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, Product),
                SQLDatabase.MakeInParam("@LotNumber", SqlDbType.VarChar, 100, LotNumber)
			};
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_GetInventoryByClientAndProduct", prams, out dr);

            if (dr != null && dr.Read())
            {
                return FillModel(dr);
            }
            return null;
        }
    }
}

