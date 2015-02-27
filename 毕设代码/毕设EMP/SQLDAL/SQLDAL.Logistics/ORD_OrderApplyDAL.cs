
// ===================================================================
// 文件： ORD_OrderApplyDAL.cs
// 项目名称：
// 创建时间：2009/3/2
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
    ///ORD_OrderApply数据访问DAL类
    /// </summary>
    public class ORD_OrderApplyDAL : BaseComplexDAL<ORD_OrderApply, ORD_OrderApplyDetail>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public ORD_OrderApplyDAL()
        {
            _ProcePrefix = "MCS_Logistics.dbo.sp_ORD_OrderApply";
        }
        #endregion


        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(ORD_OrderApply m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@SheetCode", SqlDbType.VarChar, 100, m.SheetCode),
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, m.AccountMonth),
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, m.OrganizeCity),
				SQLDatabase.MakeInParam("@PublishID", SqlDbType.Int, 4, m.PublishID),
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, m.Client),
				SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, m.State),
				SQLDatabase.MakeInParam("@Type", SqlDbType.Int, 4, m.Type),
				SQLDatabase.MakeInParam("@PreArrivalDate", SqlDbType.DateTime, 8, m.PreArrivalDate),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
                SQLDatabase.MakeInParam("@InsertTime", SqlDbType.DateTime, 8, m.InsertTime),
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
        public override int Update(ORD_OrderApply m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@SheetCode", SqlDbType.VarChar, 100, m.SheetCode),
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, m.AccountMonth),
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, m.OrganizeCity),
				SQLDatabase.MakeInParam("@PublishID", SqlDbType.Int, 4, m.PublishID),
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, m.Client),
				SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, m.State),
				SQLDatabase.MakeInParam("@Type", SqlDbType.Int, 4, m.Type),
				SQLDatabase.MakeInParam("@PreArrivalDate", SqlDbType.DateTime, 8, m.PreArrivalDate),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@UpdateStaff", SqlDbType.Int, 4, m.UpdateStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override ORD_OrderApply FillModel(IDataReader dr)
        {
            ORD_OrderApply m = new ORD_OrderApply();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["SheetCode"].ToString())) m.SheetCode = (string)dr["SheetCode"];
            if (!string.IsNullOrEmpty(dr["AccountMonth"].ToString())) m.AccountMonth = (int)dr["AccountMonth"];
            if (!string.IsNullOrEmpty(dr["OrganizeCity"].ToString())) m.OrganizeCity = (int)dr["OrganizeCity"];
            if (!string.IsNullOrEmpty(dr["PublishID"].ToString())) m.PublishID = (int)dr["PublishID"];
            if (!string.IsNullOrEmpty(dr["Client"].ToString())) m.Client = (int)dr["Client"];
            if (!string.IsNullOrEmpty(dr["State"].ToString())) m.State = (int)dr["State"];
            if (!string.IsNullOrEmpty(dr["Type"].ToString())) m.Type = (int)dr["Type"];
            if (!string.IsNullOrEmpty(dr["PreArrivalDate"].ToString())) m.PreArrivalDate = (DateTime)dr["PreArrivalDate"];
            if (!string.IsNullOrEmpty(dr["ApproveFlag"].ToString())) m.ApproveFlag = (int)dr["ApproveFlag"];
            if (!string.IsNullOrEmpty(dr["InsertTime"].ToString())) m.InsertTime = (DateTime)dr["InsertTime"];
            if (!string.IsNullOrEmpty(dr["InsertStaff"].ToString())) m.InsertStaff = (int)dr["InsertStaff"];
            if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString())) m.UpdateTime = (DateTime)dr["UpdateTime"];
            if (!string.IsNullOrEmpty(dr["UpdateStaff"].ToString())) m.UpdateStaff = (int)dr["UpdateStaff"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);

            return m;
        }

        public override int AddDetail(ORD_OrderApplyDetail m)
        {
            m.ApplyID = HeadID;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ApplyID", SqlDbType.Int, 4, m.ApplyID),
				SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, m.Product),
				SQLDatabase.MakeInParam("@Price", SqlDbType.Decimal, 9, m.Price),
				SQLDatabase.MakeInParam("@BookQuantity", SqlDbType.Int, 4, m.BookQuantity),
				SQLDatabase.MakeInParam("@AdjustQuantity", SqlDbType.Int, 4, m.AdjustQuantity),
				SQLDatabase.MakeInParam("@AdjustReason", SqlDbType.VarChar, 2000, m.AdjustReason),
				SQLDatabase.MakeInParam("@DeliveryQuantity", SqlDbType.Int, 4, m.DeliveryQuantity),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 1000, m.Remark),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            m.ID = SQLDatabase.RunProc(_ProcePrefix + "_AddDetail", prams);

            return m.ID;
        }

        public override int UpdateDetail(ORD_OrderApplyDetail m)
        {
            m.ApplyID = HeadID;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@ApplyID", SqlDbType.Int, 4, m.ApplyID),
				SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, m.Product),
				SQLDatabase.MakeInParam("@Price", SqlDbType.Decimal, 9, m.Price),
				SQLDatabase.MakeInParam("@BookQuantity", SqlDbType.Int, 4, m.BookQuantity),
				SQLDatabase.MakeInParam("@AdjustQuantity", SqlDbType.Int, 4, m.AdjustQuantity),
				SQLDatabase.MakeInParam("@AdjustReason", SqlDbType.VarChar, 2000, m.AdjustReason),
				SQLDatabase.MakeInParam("@DeliveryQuantity", SqlDbType.Int, 4, m.DeliveryQuantity),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 1000, m.Remark),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_UpdateDetail", prams);

            return ret;
        }

        protected override ORD_OrderApplyDetail FillDetailModel(IDataReader dr)
        {
            ORD_OrderApplyDetail m = new ORD_OrderApplyDetail();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["ApplyID"].ToString())) m.ApplyID = (int)dr["ApplyID"];
            if (!string.IsNullOrEmpty(dr["Product"].ToString())) m.Product = (int)dr["Product"];
            if (!string.IsNullOrEmpty(dr["Price"].ToString())) m.Price = (decimal)dr["Price"];
            if (!string.IsNullOrEmpty(dr["BookQuantity"].ToString())) m.BookQuantity = (int)dr["BookQuantity"];
            if (!string.IsNullOrEmpty(dr["AdjustQuantity"].ToString())) m.AdjustQuantity = (int)dr["AdjustQuantity"];
            if (!string.IsNullOrEmpty(dr["AdjustReason"].ToString())) m.AdjustReason = (string)dr["AdjustReason"];
            if (!string.IsNullOrEmpty(dr["DeliveryQuantity"].ToString())) m.DeliveryQuantity = (int)dr["DeliveryQuantity"];
            if (!string.IsNullOrEmpty(dr["Remark"].ToString())) m.Remark = (string)dr["Remark"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);


            return m;
        }

        /// <summary>
        /// 提交定单请购申请
        /// </summary>
        /// <param name="id"></param>
        /// <param name="staff"></param>
        /// <returns></returns>
        public int Submit(int id, int staff, int taskid)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, id),
				SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4,staff),
                SQLDatabase.MakeInParam("@TaskID", SqlDbType.Int, 4,taskid)
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Submit", prams);

            return ret;
        }

        /// <summary>
        /// 生成定单请购申请单号 格式：ODSQ+管理单元编号+'-'+申请日期+'-'+4位流水号
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
        /// 根据定单请购申请明细的获取定单请购申请单号
        /// </summary>
        /// <param name="detailid"></param>
        /// <returns></returns>
        public string GetSheetCodeByDetailID(int DetailID)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@DetailID", SqlDbType.Int, 4, DetailID),
				SQLDatabase.MakeOutParam("@SheetCode", SqlDbType.VarChar, 100)
			};
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_GetSheetCodeByDetailID", prams);

            return (string)prams[1].Value;
        }

        /// <summary>
        /// 获取定单请购申请单的总金额（含调整）
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

        /// <summary>
        /// 取消申请单并返还未发放的费用
        /// </summary>
        /// <param name="ID"></param>
        public void Finish(int ID, int staff)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, ID),
                SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4, staff)
			};
            #endregion

            SQLDatabase.RunProc("MCS_Logistics.dbo.sp_ORD_OrderApply_Finish", prams);
        }

        /// <summary>
        /// 获取指定发布单已提交的请购产品数量
        /// </summary>
        /// <param name="publishid"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        public int GetSubmitQuantity(int publishid, int product)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@PublishID", SqlDbType.Int, 4, publishid),
                SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, product),
				SQLDatabase.MakeOutParam("@SubmitQuantity", SqlDbType.Int, 4)
			};
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_GetSubmitQuantity", prams);

            return (int)prams[2].Value;
        }



        public DataTable GetSummaryTotal(int AccountMonth, int OrganizeCity, int Level, int ProductType, int Brand, int Classify, int Product, int State)
        {
            #region 设置参数集
            SqlParameter[] prams ={SQLDatabase.MakeInParam("@AccountMonth",SqlDbType.Int,4,AccountMonth),
                                     SQLDatabase.MakeInParam("@OrganizeCity",SqlDbType.Int,4,OrganizeCity),
                                     SQLDatabase.MakeInParam("@Level",SqlDbType.Int,4,Level),
                                     SQLDatabase.MakeInParam("@ProductType",SqlDbType.Int,4,ProductType),
                                     SQLDatabase.MakeInParam("@Brand",SqlDbType.Int,4,Brand),
                                     SQLDatabase.MakeInParam("@Classify",SqlDbType.Int,4,Classify),
                                     SQLDatabase.MakeInParam("@Product",SqlDbType.Int,4,Product),
                                     SQLDatabase.MakeInParam("@State",SqlDbType.Int,4,State)
                                 };
            SqlDataReader dr = null;
            SQLDatabase.RunProc(_ProcePrefix + "_GetSummaryTotal", prams, out dr);
            return Tools.ConvertDataReaderToDataTable(dr);
            #endregion
        }

        /// <summary>
        /// 赠品请购申请汇总
        /// </summary>
        /// <param name="AccountMonth"></param>
        /// <param name="OrganizeCity"></param>
        /// <param name="Level"></param>
        /// <param name="State"></param>
        /// <param name="GiftClassify"></param>
        /// <param name="Staff"></param>
        /// <returns></returns>
        public DataTable GetGiftSummary(int AccountMonth, int OrganizeCity, int Level, int State, int GiftClassify, int Staff,int Brand)
        {
            #region 设置参数集
            SqlParameter[] prams ={
                SQLDatabase.MakeInParam("@AccountMonth",SqlDbType.Int,4,AccountMonth),
                SQLDatabase.MakeInParam("@OrganizeCity",SqlDbType.Int,4,OrganizeCity),
                SQLDatabase.MakeInParam("@Level",SqlDbType.Int,4,Level),
                SQLDatabase.MakeInParam("@State",SqlDbType.Int,4,State),
                SQLDatabase.MakeInParam("@GiftClassify",SqlDbType.Int,4,GiftClassify),
                SQLDatabase.MakeInParam("@Staff",SqlDbType.Int,4,Staff),
                SQLDatabase.MakeInParam("@Brand",SqlDbType.Int,4,Brand)
            };
            SqlDataReader dr = null;
            SQLDatabase.RunProc(_ProcePrefix + "_GetGiftSummary", prams, out dr);
            return Tools.ConvertDataReaderToDataTable(dr);
            #endregion
        }

        /// <summary>
        /// 赠品申请申请汇总(以经销商为单位,相同赠品合并统计
        /// </summary>
        /// <param name="AccountMonth"></param>
        /// <param name="OrganizeCity"></param>
        /// <param name="State"></param>
        /// <param name="Staff"></param>
        /// <returns></returns>
        public DataTable GetGiftSummaryTotal(int AccountMonth, int OrganizeCity, int Client, int State, int Staff,int Brand)
        {
            #region 设置参数集
            SqlParameter[] prams ={
                SQLDatabase.MakeInParam("@AccountMonth",SqlDbType.Int,4,AccountMonth),
                SQLDatabase.MakeInParam("@OrganizeCity",SqlDbType.Int,4,OrganizeCity),
                SQLDatabase.MakeInParam("@Client",SqlDbType.Int,4,Client),
                SQLDatabase.MakeInParam("@State",SqlDbType.Int,4,State),
                SQLDatabase.MakeInParam("@Staff",SqlDbType.Int,4,Staff),
                SQLDatabase.MakeInParam("@Brand",SqlDbType.Int,4,Brand)            
            };
            SqlDataReader dr = null;
            SQLDatabase.RunProc(_ProcePrefix + "_GetGiftSummaryTotal", prams, out dr);
            return Tools.ConvertDataReaderToDataTable(dr);
            #endregion
        }

        /// <summary>
        /// 检查当月该客户是否可以申请赠品订单
        /// </summary>
        /// <param name="AccountMonth">会计月</param>
        /// <param name="Client">客户ID</param>
        /// <param name="PublishID">赠品订单目录</param>
        /// <returns>当前可以申请的次数</returns>
        public int CheckClientCanApply(int AccountMonth, int Client, int PublishID)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, AccountMonth),
				SQLDatabase.MakeInParam("@ClientID", SqlDbType.Int, 4,Client),
                SQLDatabase.MakeInParam("@PublishID", SqlDbType.Int, 4,PublishID)
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_CheckClientCanApply", prams);

            return ret;
        }

        /// <summary>
        /// 根据员工获取负责的产品系列
        /// </summary>
        /// <param name="Staff">员工ID</param>
        /// <returns></returns>
        public string GetBrandsByStaff(int Staff)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4, Staff),
                SQLDatabase.MakeOutParam("@Brands",SqlDbType.VarChar,500)
			};
            #endregion
            SQLDatabase.RunProc("MCS_Pub.dbo.sp_PDT_Brand_Manager_GetBrandsByStaff", prams);
            return prams[1].Value.ToString();
        }

        public DataTable GetGiftDetail(int AccountMonth, int OrganizeCity, int Client, int State, int Staff,int Brand,int GiftClassify)
        {
            #region 设置参数集
            SqlParameter[] prams ={
                SQLDatabase.MakeInParam("@AccountMonth",SqlDbType.Int,4,AccountMonth),
                SQLDatabase.MakeInParam("@OrganizeCity",SqlDbType.Int,4,OrganizeCity),
                SQLDatabase.MakeInParam("@Client",SqlDbType.Int,4,Client),
                SQLDatabase.MakeInParam("@State",SqlDbType.Int,4,State),
                SQLDatabase.MakeInParam("@Staff",SqlDbType.Int,4,Staff),
                SQLDatabase.MakeInParam("@Brand",SqlDbType.Int,4,Brand),
                SQLDatabase.MakeInParam("@GiftClassify",SqlDbType.Int,4,GiftClassify)
            };           
            #endregion
            SqlDataReader dr = null;
            SQLDatabase.RunProc(_ProcePrefix + "_GetGiftDetail", prams, out dr);
            return Tools.ConvertDataReaderToDataTable(dr);
        }

        public DataTable GetRPTOrderApplyPayTrack(int AccountMonth, int Client, int Classify, int OrganizeCity)
        {
            #region 设置参数集
            SqlParameter[] prams ={
                SQLDatabase.MakeInParam("@AccountMonth",SqlDbType.Int,4,AccountMonth),
                SQLDatabase.MakeInParam("@Client",SqlDbType.Int,4,Client),
                SQLDatabase.MakeInParam("@Classify",SqlDbType.Int,4,Classify),
                SQLDatabase.MakeInParam("@OrganizeCity",SqlDbType.Int,4,OrganizeCity)
                
            };
            #endregion
            SqlDataReader dr = null;
            SQLDatabase.RunProc("MCS_Logistics.dbo.sp_RPTOrderApplyPayTrack", prams, out dr);
            return Tools.ConvertDataReaderToDataTable(dr);
        }
        public DataTable GetRPTOrderCurProcess(int OrganizeCity, int Client, DateTime BeginDate, DateTime EndDate,string SheetCode)
        {
            #region 设置参数集
            SqlParameter[] prams ={
                SQLDatabase.MakeInParam("@OrganizeCity",SqlDbType.Int,4,OrganizeCity),
                SQLDatabase.MakeInParam("@Client",SqlDbType.Int,4,Client),
                SQLDatabase.MakeInParam("@BeginDate",SqlDbType.DateTime,8,BeginDate),
                SQLDatabase.MakeInParam("@EndDate",SqlDbType.DateTime,8,EndDate),
                SQLDatabase.MakeInParam("@SheetCode",SqlDbType.VarChar,50,SheetCode)
                
            };
            #endregion
            SqlDataReader dr = null;
            SQLDatabase.RunProc("MCS_Logistics.dbo.sp_RPTOrderCurProcess", prams, out dr);
            return Tools.ConvertDataReaderToDataTable(dr);
        }

        public DataTable GetRPTOrderList(int OrganizeCity, int Client, DateTime BeginDate, DateTime EndDate, string SheetCode)
        {
            #region 设置参数集
            SqlParameter[] prams ={
                SQLDatabase.MakeInParam("@OrganizeCity",SqlDbType.Int,4,OrganizeCity),
                SQLDatabase.MakeInParam("@Client",SqlDbType.Int,4,Client),
                SQLDatabase.MakeInParam("@BeginDate",SqlDbType.DateTime,8,BeginDate),
                SQLDatabase.MakeInParam("@EndDate",SqlDbType.DateTime,8,EndDate),
                SQLDatabase.MakeInParam("@SheetCode",SqlDbType.VarChar,50,SheetCode)
                
            };
            #endregion
            SqlDataReader dr = null;
            SQLDatabase.RunProc("MCS_Logistics.dbo.sp_RPTOrderList", prams, out dr);
            return Tools.ConvertDataReaderToDataTable(dr);
        }
        public DataTable GetRPTOrderDetail(string SheetCode)
        {
            #region 设置参数集
            SqlParameter[] prams ={
                
                SQLDatabase.MakeInParam("@SheetCode",SqlDbType.VarChar,50,SheetCode)
                
            };
            #endregion
            SqlDataReader dr = null;
            SQLDatabase.RunProc("MCS_Logistics.dbo.sp_RPTOrderDetail", prams, out dr);
            return Tools.ConvertDataReaderToDataTable(dr);
        }

    }
}