
// ===================================================================
// 文件： ORD_OrderDeliveryDAL.cs
// 项目名称：
// 创建时间：2009/4/26
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.Logistics;
using MCSFramework.Common;

namespace MCSFramework.SQLDAL.Logistics
{
    /// <summary>
    ///ORD_OrderDelivery数据访问DAL类
    /// </summary>
    public class ORD_OrderDeliveryDAL : BaseComplexDAL<ORD_OrderDelivery, ORD_OrderDeliveryDetail>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public ORD_OrderDeliveryDAL()
        {
            _ProcePrefix = "MCS_Logistics.dbo.sp_ORD_OrderDelivery";
        }
        #endregion


        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(ORD_OrderDelivery m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@SheetCode", SqlDbType.VarChar, 100, m.SheetCode),
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, m.AccountMonth),
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, m.OrganizeCity),
				SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, m.State),
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, m.Client),
				SQLDatabase.MakeInParam("@Store", SqlDbType.Int, 4, m.Store),
				SQLDatabase.MakeInParam("@PreArrivalDate", SqlDbType.DateTime, 8, m.PreArrivalDate),
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
        public override int Update(ORD_OrderDelivery m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@SheetCode", SqlDbType.VarChar, 100, m.SheetCode),
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, m.AccountMonth),
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, m.OrganizeCity),
				SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, m.State),
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, m.Client),
				SQLDatabase.MakeInParam("@Store", SqlDbType.Int, 4, m.Store),
				SQLDatabase.MakeInParam("@PreArrivalDate", SqlDbType.DateTime, 8, m.PreArrivalDate),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@UpdateStaff", SqlDbType.Int, 4, m.UpdateStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override ORD_OrderDelivery FillModel(IDataReader dr)
        {
            ORD_OrderDelivery m = new ORD_OrderDelivery();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["SheetCode"].ToString())) m.SheetCode = (string)dr["SheetCode"];
            if (!string.IsNullOrEmpty(dr["AccountMonth"].ToString())) m.AccountMonth = (int)dr["AccountMonth"];
            if (!string.IsNullOrEmpty(dr["OrganizeCity"].ToString())) m.OrganizeCity = (int)dr["OrganizeCity"];
            if (!string.IsNullOrEmpty(dr["Client"].ToString())) m.Client = (int)dr["Client"];
            if (!string.IsNullOrEmpty(dr["Store"].ToString())) m.Store = (int)dr["Store"];
            if (!string.IsNullOrEmpty(dr["State"].ToString())) m.State = (int)dr["State"];
            if (!string.IsNullOrEmpty(dr["PreArrivalDate"].ToString())) m.PreArrivalDate = (DateTime)dr["PreArrivalDate"];
            if (!string.IsNullOrEmpty(dr["ApproveFlag"].ToString())) m.ApproveFlag = (int)dr["ApproveFlag"];
            if (!string.IsNullOrEmpty(dr["InsertTime"].ToString())) m.InsertTime = (DateTime)dr["InsertTime"];
            if (!string.IsNullOrEmpty(dr["InsertStaff"].ToString())) m.InsertStaff = (int)dr["InsertStaff"];
            if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString())) m.UpdateTime = (DateTime)dr["UpdateTime"];
            if (!string.IsNullOrEmpty(dr["UpdateStaff"].ToString())) m.UpdateStaff = (int)dr["UpdateStaff"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);

            return m;
        }

        public override int AddDetail(ORD_OrderDeliveryDetail m)
        {
            m.DeliveryID = HeadID;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@DeliveryID", SqlDbType.Int, 4, m.DeliveryID),
				SQLDatabase.MakeInParam("@ApplyDetailID", SqlDbType.Int, 4, m.ApplyDetailID),
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, m.Client),
				SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, m.Product),
				SQLDatabase.MakeInParam("@Price", SqlDbType.Decimal, 9, m.Price),
				SQLDatabase.MakeInParam("@FactoryPrice", SqlDbType.Decimal, 9, m.FactoryPrice),
				SQLDatabase.MakeInParam("@DeliveryQuantity", SqlDbType.Int, 4, m.DeliveryQuantity),
				SQLDatabase.MakeInParam("@SignInQuantity", SqlDbType.Int, 4, m.SignInQuantity),
				SQLDatabase.MakeInParam("@BadQuantity", SqlDbType.Int, 4, m.BadQuantity),
				SQLDatabase.MakeInParam("@LostQuantity", SqlDbType.Int, 4, m.LostQuantity),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 1000, m.Remark),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 8000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            m.ID = SQLDatabase.RunProc(_ProcePrefix + "_AddDetail", prams);

            return m.ID;
        }

        public override int UpdateDetail(ORD_OrderDeliveryDetail m)
        {
            m.DeliveryID = HeadID;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@DeliveryID", SqlDbType.Int, 4, m.DeliveryID),
				SQLDatabase.MakeInParam("@ApplyDetailID", SqlDbType.Int, 4, m.ApplyDetailID),
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, m.Client),
				SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, m.Product),
				SQLDatabase.MakeInParam("@Price", SqlDbType.Decimal, 9, m.Price),
				SQLDatabase.MakeInParam("@FactoryPrice", SqlDbType.Decimal, 9, m.FactoryPrice),
				SQLDatabase.MakeInParam("@DeliveryQuantity", SqlDbType.Int, 4, m.DeliveryQuantity),
				SQLDatabase.MakeInParam("@SignInQuantity", SqlDbType.Int, 4, m.SignInQuantity),
				SQLDatabase.MakeInParam("@BadQuantity", SqlDbType.Int, 4, m.BadQuantity),
				SQLDatabase.MakeInParam("@LostQuantity", SqlDbType.Int, 4, m.LostQuantity),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 1000, m.Remark),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 8000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_UpdateDetail", prams);

            return ret;
        }

        protected override ORD_OrderDeliveryDetail FillDetailModel(IDataReader dr)
        {
            ORD_OrderDeliveryDetail m = new ORD_OrderDeliveryDetail();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["DeliveryID"].ToString())) m.DeliveryID = (int)dr["DeliveryID"];
            if (!string.IsNullOrEmpty(dr["ApplyDetailID"].ToString())) m.ApplyDetailID = (int)dr["ApplyDetailID"];
            if (!string.IsNullOrEmpty(dr["Client"].ToString())) m.Client = (int)dr["Client"];
            if (!string.IsNullOrEmpty(dr["Product"].ToString())) m.Product = (int)dr["Product"];
            if (!string.IsNullOrEmpty(dr["Price"].ToString())) m.Price = (decimal)dr["Price"];
            if (!string.IsNullOrEmpty(dr["FactoryPrice"].ToString())) m.FactoryPrice = (decimal)dr["FactoryPrice"];
            if (!string.IsNullOrEmpty(dr["DeliveryQuantity"].ToString())) m.DeliveryQuantity = (int)dr["DeliveryQuantity"];
            if (!string.IsNullOrEmpty(dr["SignInQuantity"].ToString())) m.SignInQuantity = (int)dr["SignInQuantity"];
            if (!string.IsNullOrEmpty(dr["BadQuantity"].ToString())) m.BadQuantity = (int)dr["BadQuantity"];
            if (!string.IsNullOrEmpty(dr["LostQuantity"].ToString())) m.LostQuantity = (int)dr["LostQuantity"];
            if (!string.IsNullOrEmpty(dr["Remark"].ToString())) m.Remark = (string)dr["Remark"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);


            return m;
        }


        /// <summary>
        /// 生成费用报销单号 格式：ODXF+管理单元编号+'-'+申请日期+'-'+4位流水号
        /// </summary>
        /// <param name="organizecity"></param>
        /// <returns></returns>
        public string GenerateSheetCode(int organizecity, int accountmonth)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, organizecity),
                SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, accountmonth),
				SQLDatabase.MakeOutParam("@SheetCode", SqlDbType.VarChar, 100)
			};
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_GenerateSheetCode", prams);

            return (string)prams[2].Value;
        }

        /// <summary>
        /// 提交定单请购发放
        /// </summary>
        /// <param name="id"></param>
        /// <param name="staff"></param>
        /// <returns></returns>
        public int Submit(int id, int staff)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, id),
				SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4,staff)
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Submit", prams);

            return ret;
        }

        /// <summary>
        /// 获取定单请购发放单的总金额（含调整）
        /// </summary>
        /// <param name="detailid"></param>
        /// <returns></returns>
        public decimal GetSumCost(int ID)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, ID),
				new SqlParameter("@SumCost", SqlDbType.Decimal,9, ParameterDirection.Output,false,18,3,"SumCost", DataRowVersion.Current,0)
			};
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_GetSumCost", prams);

            return (decimal)prams[1].Value;
        }

        public int Approve(int ID, int StaffID)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, ID),
				SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4, StaffID)
			};
            #endregion

            return SQLDatabase.RunProc(_ProcePrefix + "_Approve", prams);
        }

        public int Delivery(int ID, int StaffID)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, ID),
				SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4, StaffID)
			};
            #endregion

            return SQLDatabase.RunProc(_ProcePrefix + "_Delivery", prams);
        }

        public int SignIn(int ID, int StaffID)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, ID),
				SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4, StaffID)
			};
            #endregion

            return SQLDatabase.RunProc(_ProcePrefix + "_SignIn", prams);
        }

        /// <summary>
        /// 初始化录入发货单产品列表
        /// </summary>
        /// <param name="Client">收货客户</param>
        /// <param name="IsCXP">是否是促销品，0：成品，取价表目录 1：促销品，取促销品库目录</param>
        /// <returns></returns>
        public DataTable InitProductList(int Client, int IsCXP)
        {
            SqlDataReader dr = null;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, Client),
				SQLDatabase.MakeInParam("@IsCXP", SqlDbType.Int, 4, IsCXP)
			};
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_InitProductList", prams, out dr);

            return Tools.ConvertDataReaderToDataTable(dr);
        }

        /// <summary>
        /// -经销商出库条码明细
        /// </summary>
        /// <param name="jxsID"></param>
        /// <param name="sDate"></param>
        /// <param name="eDate"></param>
        /// <returns></returns>
        public DataTable InitOrderDeliveryList(string jxsID, string sDate, string eDate)
        {
            SqlDataReader dr = null;
            string strRun = "ERP26.YSL_DMS.dbo.emp_Barcode_Send2D_Info";

            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@jxsID", SqlDbType.VarChar,50,  jxsID),
                SQLDatabase.MakeInParam("@sDate", SqlDbType.VarChar, 50, sDate),
				SQLDatabase.MakeInParam("@eDate", SqlDbType.VarChar,50, eDate)
			};
            #endregion

            SQLDatabase.RunProc(strRun, prams, out dr);

            return Tools.ConvertDataReaderToDataTable(dr);
        }

        /// <summary>
        /// 初始化录入出库反馈
        /// </summary>
        /// <returns></returns>
        public DataTable InitOrderAlibrayList()
        {
            SqlDataReader dr = null;
            //string strSql = "SELECT OrderNo ,下级客户全称 , 经销商名称 , SUBSTRING(CONVERT(VARCHAR(20), Adate, 23), 1, 10) Adate , COUNT(1) CountMun FROM  MCS_Logistics.dbo.ORD_OrderAlibrary GROUP BY OrderNo , 下级客户全称 , 经销商名称 , SUBSTRING(CONVERT(VARCHAR(20), Adate, 23), 1, 10)";

            SQLDatabase.RunProc("MCS_Logistics.dbo.sp_ORD_OrderAlibrary_GetList", out dr);

            return Tools.ConvertDataReaderToDataTable(dr);
        }

        public DataTable GetOrderAlibrayDetailList(string OrderNo)
        {
            SqlDataReader dr = null;
            string strSql = " SELECT  PDT_Product.Code,PDT_Product.FullName,COUNT(1) CountMun,SUM(FactoryPrice) SumPrice FROM MCS_Logistics.dbo.ORD_OrderAlibrary JOIN MCS_Pub.dbo.PDT_Product on [MCS_Logistics].dbo.ORD_OrderAlibrary.Milkid=PDT_Product.Code AND PDT_Product.Brand IN (SELECT ID FROM MCS_Pub.dbo.PDT_Brand WHERE IsOpponent='1') WHERE OrderNo=@OrderNo GROUP BY PDT_Product.Code,PDT_Product.FullName,OrderId";
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@OrderNo", SqlDbType.VarChar,50,  OrderNo)
			};
            #endregion
            SQLDatabase.RunSQL(strSql,prams, out dr);

            return Tools.ConvertDataReaderToDataTable(dr);
        }

        public DataTable GetOrderStorageDetailList(string BillNo)
        {
            SqlDataReader dr = null;
            //string strSql = " SELECT  PDT_Product.Code ,PDT_Product.FullName ,COUNT(1) CountMun ,SUM(FactoryPrice) SumPrice ,SUBSTRING( CONVERT(VARCHAR(20),scantime,23),1,10) scantime FROM [MCS_Logistics].dbo.ORD_OrderStorage JOIN MCS_Pub.dbo.PDT_Product ON ORD_OrderStorage.ProdID = PDT_Product.Code AND PDT_Product.Brand IN ( SELECT ID FROM MCS_Pub.dbo.PDT_Brand WHERE IsOpponent = '1' ) WHERE BillNO=@BillNo GROUP BY PDT_Product.Code , PDT_Product.FullName ,SUBSTRING( CONVERT(VARCHAR(20),scantime,23),1,10)";
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@BillNo", SqlDbType.VarChar,50,  BillNo)
			};
            #endregion
            SQLDatabase.RunProc("MCS_Logistics.dbo.sp_ORD_OrderStorage_GetStorageList", prams, out dr);

            return Tools.ConvertDataReaderToDataTable(dr);
        }

        /// <summary>
        /// 获取入库实发数量
        /// </summary>
        /// <param name="detailid"></param>
        /// <returns></returns>
        public int GetSumNumber(int ID)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, ID),
				new SqlParameter("@SumNumber", SqlDbType.Int,9, ParameterDirection.Output,false,18,3,"SumNumber", DataRowVersion.Current,0)
			};
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_GetSumNumber", prams);

            return int.Parse(prams[1].Value.ToString()); 
        }

        /// <summary>
        /// 获取入库详细应反馈数量
        /// </summary>
        /// <param name="detailid"></param>
        /// <returns></returns>
        public int GetSumNum(string ProdID)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ProdID", SqlDbType.VarChar, 50, ProdID),
				new SqlParameter("@SumQuantity", SqlDbType.Int,9, ParameterDirection.Output,false,18,3,"SumQuantity", DataRowVersion.Current,0)
			};
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_GetSumNum", prams);

            return int.Parse(prams[1].Value.ToString());
        }

        /// <summary>
        /// 获取入库应反馈数量
        /// </summary>
        /// <param name="detailid"></param>
        /// <returns></returns>
        public int GetSumQuantity(int ID)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, ID),
				new SqlParameter("@SumQuantity", SqlDbType.Int,9, ParameterDirection.Output,false,18,3,"SumQuantity", DataRowVersion.Current,0)
			};
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_GetSumQuantity", prams);

            return int.Parse(prams[1].Value.ToString());
        }
    }
}

