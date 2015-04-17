
// ===================================================================
// 文件： PDT_ProductExtInfoDAL.cs
// 项目名称：
// 创建时间：2015-02-02
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.Pub;
using MCSFramework.Common;


namespace MCSFramework.SQLDAL.Pub
{
    /// <summary>
    ///PDT_ProductExtInfo数据访问DAL类
    /// </summary>
    public class PDT_ProductExtInfoDAL : BaseSimpleDAL<PDT_ProductExtInfo>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public PDT_ProductExtInfoDAL()
        {
            _ProcePrefix = "MCS_PUB.dbo.sp_PDT_ProductExtInfo";
        }
        #endregion


        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(PDT_ProductExtInfo m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Supplier", SqlDbType.Int, 4, m.Supplier),
				SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, m.Product),
				SQLDatabase.MakeInParam("@Code", SqlDbType.VarChar, 50, m.Code),
				SQLDatabase.MakeInParam("@Category", SqlDbType.Int, 4, m.Category),
				SQLDatabase.MakeInParam("@BuyPrice", SqlDbType.Decimal, 9, m.BuyPrice),
				SQLDatabase.MakeInParam("@SalesPrice", SqlDbType.Decimal, 9, m.SalesPrice),
				SQLDatabase.MakeInParam("@MaxSalesPrice", SqlDbType.Decimal, 9, m.MaxSalesPrice),
				SQLDatabase.MakeInParam("@MinSalesPrice", SqlDbType.Decimal, 9, m.MinSalesPrice),
				SQLDatabase.MakeInParam("@MaxInventory", SqlDbType.Int, 4, m.MaxInventory),
				SQLDatabase.MakeInParam("@MinInventory", SqlDbType.Int, 4, m.MinInventory),
				SQLDatabase.MakeInParam("@SalesState", SqlDbType.Int, 4, m.SalesState),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 1000, m.Remark),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
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
        public override int Update(PDT_ProductExtInfo m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@Supplier", SqlDbType.Int, 4, m.Supplier),
				SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, m.Product),
				SQLDatabase.MakeInParam("@Code", SqlDbType.VarChar, 50, m.Code),
				SQLDatabase.MakeInParam("@Category", SqlDbType.Int, 4, m.Category),
				SQLDatabase.MakeInParam("@BuyPrice", SqlDbType.Decimal, 9, m.BuyPrice),
				SQLDatabase.MakeInParam("@SalesPrice", SqlDbType.Decimal, 9, m.SalesPrice),
				SQLDatabase.MakeInParam("@MaxSalesPrice", SqlDbType.Decimal, 9, m.MaxSalesPrice),
				SQLDatabase.MakeInParam("@MinSalesPrice", SqlDbType.Decimal, 9, m.MinSalesPrice),
				SQLDatabase.MakeInParam("@MaxInventory", SqlDbType.Int, 4, m.MaxInventory),
				SQLDatabase.MakeInParam("@MinInventory", SqlDbType.Int, 4, m.MinInventory),
				SQLDatabase.MakeInParam("@SalesState", SqlDbType.Int, 4, m.SalesState),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 1000, m.Remark),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@UpdateStaff", SqlDbType.Int, 4, m.UpdateStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override PDT_ProductExtInfo FillModel(IDataReader dr)
        {
            PDT_ProductExtInfo m = new PDT_ProductExtInfo();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["Supplier"].ToString())) m.Supplier = (int)dr["Supplier"];
            if (!string.IsNullOrEmpty(dr["Product"].ToString())) m.Product = (int)dr["Product"];
            if (!string.IsNullOrEmpty(dr["Code"].ToString())) m.Code = (string)dr["Code"];
            if (!string.IsNullOrEmpty(dr["Category"].ToString())) m.Category = (int)dr["Category"];
            if (!string.IsNullOrEmpty(dr["BuyPrice"].ToString())) m.BuyPrice = (decimal)dr["BuyPrice"];
            if (!string.IsNullOrEmpty(dr["SalesPrice"].ToString())) m.SalesPrice = (decimal)dr["SalesPrice"];
            if (!string.IsNullOrEmpty(dr["MaxSalesPrice"].ToString())) m.MaxSalesPrice = (decimal)dr["MaxSalesPrice"];
            if (!string.IsNullOrEmpty(dr["MinSalesPrice"].ToString())) m.MinSalesPrice = (decimal)dr["MinSalesPrice"];
            if (!string.IsNullOrEmpty(dr["MaxInventory"].ToString())) m.MaxInventory = (int)dr["MaxInventory"];
            if (!string.IsNullOrEmpty(dr["MinInventory"].ToString())) m.MinInventory = (int)dr["MinInventory"];
            if (!string.IsNullOrEmpty(dr["SalesState"].ToString())) m.SalesState = (int)dr["SalesState"];
            if (!string.IsNullOrEmpty(dr["Remark"].ToString())) m.Remark = (string)dr["Remark"];
            if (!string.IsNullOrEmpty(dr["ApproveFlag"].ToString())) m.ApproveFlag = (int)dr["ApproveFlag"];
            if (!string.IsNullOrEmpty(dr["InsertTime"].ToString())) m.InsertTime = (DateTime)dr["InsertTime"];
            if (!string.IsNullOrEmpty(dr["InsertStaff"].ToString())) m.InsertStaff = (int)dr["InsertStaff"];
            if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString())) m.UpdateTime = (DateTime)dr["UpdateTime"];
            if (!string.IsNullOrEmpty(dr["UpdateStaff"].ToString())) m.UpdateStaff = (int)dr["UpdateStaff"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);

            return m;
        }

        /// <summary>
        /// 获取指定客户经营产品列表
        /// </summary>
        /// <param name="OwnerClient"></param>
        /// <param name="State"></param>
        /// <param name="Category"></param>
        /// <param name="ExtCondition"></param>
        /// <returns></returns>
        public DataTable GetByClient(int OwnerClient, int State, int Category, string ExtCondition)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@OwnerClient", SqlDbType.Int, 4, OwnerClient),
                SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, State),
                SQLDatabase.MakeInParam("@Category", SqlDbType.Int, 4, Category),
                SQLDatabase.MakeInParam("@ExtCondition", SqlDbType.VarChar, 500, ExtCondition)
			};
            #endregion
            SqlDataReader sdr = null;
            SQLDatabase.RunProc(_ProcePrefix + "_GetByClient", prams, out sdr);
            return Tools.ConvertDataReaderToDataTable(sdr);
        }
    }
}

