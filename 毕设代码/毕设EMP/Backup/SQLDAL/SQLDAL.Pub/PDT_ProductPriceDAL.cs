
// ===================================================================
// 文件： PDT_ProductPriceDAL.cs
// 项目名称：
// 创建时间：2009-3-10
// 作者:	   
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.Pub;
using MCSFramework.Common;
using System.Collections.Generic;


namespace MCSFramework.SQLDAL.Pub
{
    /// <summary>
    ///PDT_ProductPrice数据访问DAL类
    /// </summary>
    public class PDT_ProductPriceDAL : BaseComplexDAL<PDT_ProductPrice, PDT_ProductPrice_Detail>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public PDT_ProductPriceDAL()
        {
            _ProcePrefix = "MCS_Pub.dbo.sp_PDT_ProductPrice";
        }
        #endregion


        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(PDT_ProductPrice m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, m.Client),
				SQLDatabase.MakeInParam("@StandardPrice", SqlDbType.Int, 4, m.StandardPrice),
				SQLDatabase.MakeInParam("@BeginDate", SqlDbType.DateTime, 8, m.BeginDate),
				SQLDatabase.MakeInParam("@EndDate", SqlDbType.DateTime, 8, m.EndDate),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@InsertTime", SqlDbType.DateTime, 8, m.InsertTime),
				SQLDatabase.MakeInParam("@InsertStaff", SqlDbType.Int, 4, m.InsertStaff),
				SQLDatabase.MakeInParam("@UpdateTime", SqlDbType.DateTime, 8, m.UpdateTime),
				SQLDatabase.MakeInParam("@UpdateStaff", SqlDbType.Int, 4, m.UpdateStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 8000, CombineExtProperty(m.ExtPropertys,m.ModelName))
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
        public override int Update(PDT_ProductPrice m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, m.Client),
                SQLDatabase.MakeInParam("@StandardPrice", SqlDbType.Int, 4, m.StandardPrice),
				SQLDatabase.MakeInParam("@BeginDate", SqlDbType.DateTime, 8, m.BeginDate),
				SQLDatabase.MakeInParam("@EndDate", SqlDbType.DateTime, 8, m.EndDate),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@InsertTime", SqlDbType.DateTime, 8, m.InsertTime),
				SQLDatabase.MakeInParam("@InsertStaff", SqlDbType.Int, 4, m.InsertStaff),
				SQLDatabase.MakeInParam("@UpdateTime", SqlDbType.DateTime, 8, m.UpdateTime),
				SQLDatabase.MakeInParam("@UpdateStaff", SqlDbType.Int, 4, m.UpdateStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 8000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override PDT_ProductPrice FillModel(IDataReader dr)
        {
            PDT_ProductPrice m = new PDT_ProductPrice();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["Client"].ToString())) m.Client = (int)dr["Client"];
            if (!string.IsNullOrEmpty(dr["StandardPrice"].ToString())) m.StandardPrice = (int)dr["StandardPrice"];
            if (!string.IsNullOrEmpty(dr["BeginDate"].ToString())) m.BeginDate = (DateTime)dr["BeginDate"];
            if (!string.IsNullOrEmpty(dr["EndDate"].ToString())) m.EndDate = (DateTime)dr["EndDate"];
            if (!string.IsNullOrEmpty(dr["ApproveFlag"].ToString())) m.ApproveFlag = (int)dr["ApproveFlag"];
            if (!string.IsNullOrEmpty(dr["InsertTime"].ToString())) m.InsertTime = (DateTime)dr["InsertTime"];
            if (!string.IsNullOrEmpty(dr["InsertStaff"].ToString())) m.InsertStaff = (int)dr["InsertStaff"];
            if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString())) m.UpdateTime = (DateTime)dr["UpdateTime"];
            if (!string.IsNullOrEmpty(dr["UpdateStaff"].ToString())) m.UpdateStaff = (int)dr["UpdateStaff"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);

            return m;
        }

        public override int AddDetail(PDT_ProductPrice_Detail m)
        {
            m.PriceID = HeadID;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@PriceID", SqlDbType.Int, 4, m.PriceID),
				SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, m.Product),
				SQLDatabase.MakeInParam("@BuyingPrice", SqlDbType.Decimal, 9, m.BuyingPrice),
				SQLDatabase.MakeInParam("@SalesPrice", SqlDbType.Decimal, 9, m.SalesPrice),
                SQLDatabase.MakeInParam("@FactoryPrice", SqlDbType.Decimal, 9, m.FactoryPrice),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 2000, m.Remark)
			};
            #endregion

            m.ID = SQLDatabase.RunProc(_ProcePrefix + "_AddDetail", prams);

            return m.ID;
        }

        public override int UpdateDetail(PDT_ProductPrice_Detail m)
        {
            m.PriceID = HeadID;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@PriceID", SqlDbType.Int, 4, m.PriceID),
				SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, m.Product),
				SQLDatabase.MakeInParam("@BuyingPrice", SqlDbType.Decimal, 9, m.BuyingPrice),
				SQLDatabase.MakeInParam("@SalesPrice", SqlDbType.Decimal, 9, m.SalesPrice),
                SQLDatabase.MakeInParam("@FactoryPrice", SqlDbType.Decimal, 9, m.FactoryPrice),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 2000, m.Remark)
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_UpdateDetail", prams);

            return ret;
        }

        protected override PDT_ProductPrice_Detail FillDetailModel(IDataReader dr)
        {
            PDT_ProductPrice_Detail m = new PDT_ProductPrice_Detail();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["PriceID"].ToString())) m.PriceID = (int)dr["PriceID"];
            if (!string.IsNullOrEmpty(dr["Product"].ToString())) m.Product = (int)dr["Product"];
            if (!string.IsNullOrEmpty(dr["BuyingPrice"].ToString())) m.BuyingPrice = (decimal)dr["BuyingPrice"];
            if (!string.IsNullOrEmpty(dr["SalesPrice"].ToString())) m.SalesPrice = (decimal)dr["SalesPrice"];
            if (!string.IsNullOrEmpty(dr["FactoryPrice"].ToString())) m.FactoryPrice = (decimal)dr["FactoryPrice"];
            if (!string.IsNullOrEmpty(dr["Remark"].ToString())) m.Remark = (string)dr["Remark"];
            return m;
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

        public void UnApprove(int StaffID)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, HeadID),
				SQLDatabase.MakeInParam("@StaffID", SqlDbType.Int, 4, StaffID)
			};
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_UnApprove", prams);
        }

        public SqlDataReader GetProductPriceList(int ClientID)
        {

            #region	设置参数集
            SqlParameter[] prams = {
                SQLDatabase.MakeInParam("@ClientID", SqlDbType.Int, 4,ClientID)
			};
            #endregion
            SqlDataReader dr = null;
            SQLDatabase.RunProc(_ProcePrefix + "_GetProductPriceList", prams, out dr);
            return dr;
        }

        /// <summary>
        /// 从价表中取某个客户指定产品的价格
        /// </summary>
        /// <param name="ClientID"></param>
        /// <param name="Product"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public int GetPriceByClientAndType(int ClientID, int Product, int Type, out decimal FactoryPrice, out decimal Price)
        {

            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ClientID", SqlDbType.Int, 4, ClientID),
                SQLDatabase.MakeInParam("@Type", SqlDbType.Int, 4, Type),
                SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, Product),
                new SqlParameter("@FactoryPrice", SqlDbType.Decimal,9, ParameterDirection.Output,false,10,3,"FactoryPrice", DataRowVersion.Current,0),
                new SqlParameter("@Price", SqlDbType.Decimal,9, ParameterDirection.Output,false,10,3,"Price", DataRowVersion.Current,0)
			};
            #endregion

            int i = SQLDatabase.RunProc(_ProcePrefix + "_GetPriceByClientAndType", prams);
            FactoryPrice = (decimal)prams[3].Value;
            Price = (decimal)prams[4].Value;
            return i;
        }

        /// <summary>
        /// 从上级供货商复制价表
        /// </summary>
        /// <param name="Client"></param>
        /// <param name="Staff"></param>
        /// <returns></returns>
        public int CopyFromSupplier(int Client, int Staff)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, Client),
                SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4, Staff)
			};
            #endregion

            return SQLDatabase.RunProc(_ProcePrefix + "_CopyFromSupplier", prams);
        }
        /// <summary>
        /// 从标准价表复制价表
        /// </summary>
        /// <param name="StandardPrice"></param>
        /// <param name="Client"></param>
        /// <param name="Staff"></param>
        /// <returns></returns>
        public int CopyFromStandardPrice(int StandardPrice, int Client, int Staff)
        {
            #region	设置参数集
            SqlParameter[] prams = {
                SQLDatabase.MakeInParam("@StandardPrice", SqlDbType.Int, 4, StandardPrice),
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, Client),
                SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4, Staff)
			};
            #endregion
            return SQLDatabase.RunProc(_ProcePrefix + "_CopyFromStandardPrice", prams);
        }
    }
}

