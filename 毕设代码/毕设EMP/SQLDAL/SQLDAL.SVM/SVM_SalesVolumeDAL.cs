
// ===================================================================
// 文件： SVM_SalesVolumeDAL.cs
// 项目名称：
// 创建时间：2009-2-19
// 作者:	   
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.SVM;


namespace MCSFramework.SQLDAL.SVM
{
    /// <summary>
    ///SVM_SalesVolume数据访问DAL类
    /// </summary>
    public class SVM_SalesVolumeDAL : BaseComplexDAL<SVM_SalesVolume, SVM_SalesVolume_Detail>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public SVM_SalesVolumeDAL()
        {
            _ProcePrefix = "MCS_SVM.dbo.sp_SVM_SalesVolume";
        }
        #endregion


        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(SVM_SalesVolume m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@SheetCode", SqlDbType.VarChar, 50, m.SheetCode),
				SQLDatabase.MakeInParam("@Type", SqlDbType.Int, 4, m.Type),
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, m.OrganizeCity),
				SQLDatabase.MakeInParam("@SalesDate", SqlDbType.DateTime, 8, m.SalesDate),
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, m.AccountMonth),
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, m.Client),
				SQLDatabase.MakeInParam("@Supplier", SqlDbType.Int, 4, m.Supplier),
                SQLDatabase.MakeInParam("@Flag", SqlDbType.Int, 4, m.Flag),
				SQLDatabase.MakeInParam("@SalesStaff", SqlDbType.Int, 4, m.SalesStaff),
				SQLDatabase.MakeInParam("@Promotor", SqlDbType.Int, 4, m.Promotor),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 500, m.Remark),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@InsertStaff", SqlDbType.Int, 4, m.InsertStaff),
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
        public override int Update(SVM_SalesVolume m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@SheetCode", SqlDbType.VarChar, 50, m.SheetCode),
				SQLDatabase.MakeInParam("@Type", SqlDbType.Int, 4, m.Type),
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, m.OrganizeCity),
				SQLDatabase.MakeInParam("@SalesDate", SqlDbType.DateTime, 8, m.SalesDate),
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, m.AccountMonth),
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, m.Client),
				SQLDatabase.MakeInParam("@Supplier", SqlDbType.Int, 4, m.Supplier),
                SQLDatabase.MakeInParam("@Flag", SqlDbType.Int, 4, m.Flag),
				SQLDatabase.MakeInParam("@SalesStaff", SqlDbType.Int, 4, m.SalesStaff),
				SQLDatabase.MakeInParam("@Promotor", SqlDbType.Int, 4, m.Promotor),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 500, m.Remark),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@UpdateStaff", SqlDbType.Int, 4, m.UpdateStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 8000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override SVM_SalesVolume FillModel(IDataReader dr)
        {
            SVM_SalesVolume m = new SVM_SalesVolume();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["SheetCode"].ToString())) m.SheetCode = (string)dr["SheetCode"];
            if (!string.IsNullOrEmpty(dr["Type"].ToString())) m.Type = (int)dr["Type"];
            if (!string.IsNullOrEmpty(dr["OrganizeCity"].ToString())) m.OrganizeCity = (int)dr["OrganizeCity"];
            if (!string.IsNullOrEmpty(dr["SalesDate"].ToString())) m.SalesDate = (DateTime)dr["SalesDate"];
            if (!string.IsNullOrEmpty(dr["AccountMonth"].ToString())) m.AccountMonth = (int)dr["AccountMonth"];
            if (!string.IsNullOrEmpty(dr["Client"].ToString())) m.Client = (int)dr["Client"];
            if (!string.IsNullOrEmpty(dr["Supplier"].ToString())) m.Supplier = (int)dr["Supplier"];
            if (!string.IsNullOrEmpty(dr["Flag"].ToString())) m.Flag = (int)dr["Flag"];
            if (!string.IsNullOrEmpty(dr["SalesStaff"].ToString())) m.SalesStaff = (int)dr["SalesStaff"];
            if (!string.IsNullOrEmpty(dr["Promotor"].ToString())) m.Promotor = (int)dr["Promotor"];
            if (!string.IsNullOrEmpty(dr["Remark"].ToString())) m.Remark = (string)dr["Remark"];
            if (!string.IsNullOrEmpty(dr["ApproveFlag"].ToString())) m.ApproveFlag = (int)dr["ApproveFlag"];
            if (!string.IsNullOrEmpty(dr["InsertTime"].ToString())) m.InsertTime = (DateTime)dr["InsertTime"];
            if (!string.IsNullOrEmpty(dr["InsertStaff"].ToString())) m.InsertStaff = (int)dr["InsertStaff"];
            if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString())) m.UpdateTime = (DateTime)dr["UpdateTime"];
            if (!string.IsNullOrEmpty(dr["UpdateStaff"].ToString())) m.UpdateStaff = (int)dr["UpdateStaff"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);

            return m;
        }

        public override int AddDetail(SVM_SalesVolume_Detail m)
        {
            m.SalesVolume = HeadID;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@SalesVolume", SqlDbType.Int, 4, m.SalesVolume),
				SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, m.Product),
				SQLDatabase.MakeInParam("@LotNumber", SqlDbType.VarChar, 50, m.LotNumber),
				SQLDatabase.MakeInParam("@SalesPrice", SqlDbType.Decimal, 9, m.SalesPrice),
				SQLDatabase.MakeInParam("@Quantity", SqlDbType.Int, 4, m.Quantity),
				SQLDatabase.MakeInParam("@FactoryPrice", SqlDbType.Decimal, 9, m.FactoryPrice)
			};
            #endregion

            m.ID = SQLDatabase.RunProc(_ProcePrefix + "_AddDetail", prams);

            return m.ID;
        }

        public override int UpdateDetail(SVM_SalesVolume_Detail m)
        {
            m.SalesVolume = HeadID;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@SalesVolume", SqlDbType.Int, 4, m.SalesVolume),
				SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, m.Product),
				SQLDatabase.MakeInParam("@LotNumber", SqlDbType.VarChar, 50, m.LotNumber),
				SQLDatabase.MakeInParam("@SalesPrice", SqlDbType.Decimal, 9, m.SalesPrice),
				SQLDatabase.MakeInParam("@Quantity", SqlDbType.Int, 4, m.Quantity),
				SQLDatabase.MakeInParam("@FactoryPrice", SqlDbType.Decimal, 9, m.FactoryPrice)
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_UpdateDetail", prams);

            return ret;
        }

        protected override SVM_SalesVolume_Detail FillDetailModel(IDataReader dr)
        {
            SVM_SalesVolume_Detail m = new SVM_SalesVolume_Detail();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["SalesVolume"].ToString())) m.SalesVolume = (int)dr["SalesVolume"];
            if (!string.IsNullOrEmpty(dr["Product"].ToString())) m.Product = (int)dr["Product"];
            if (!string.IsNullOrEmpty(dr["LotNumber"].ToString())) m.LotNumber = (string)dr["LotNumber"];
            if (!string.IsNullOrEmpty(dr["SalesPrice"].ToString())) m.SalesPrice = (decimal)dr["SalesPrice"];
            if (!string.IsNullOrEmpty(dr["Quantity"].ToString())) m.Quantity = (int)dr["Quantity"];
            if (!string.IsNullOrEmpty(dr["SyncQuantity"].ToString())) m.SyncQuantity = (int)dr["SyncQuantity"];
            if (!string.IsNullOrEmpty(dr["FactoryPrice"].ToString())) m.FactoryPrice = (decimal)dr["FactoryPrice"];


            return m;
        }

        /// <summary>
        /// 获取指定销量单ID的按销售价计算的总销售额，含DDF
        /// </summary>
        /// <returns></returns>
        public decimal GetTotalValue()
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, HeadID),
                 new SqlParameter("@TotalValue", SqlDbType.Decimal,9, ParameterDirection.Output,false,18,3,"TotalValue", DataRowVersion.Current,0)
			};
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_GetTotalValue", prams);

            return (decimal)prams[1].Value;
        }

        /// <summary>
        /// 获取订单的产品件数
        /// </summary>
        /// <returns></returns>
        public int GetTotalBox()
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, HeadID),
                 new SqlParameter("@TotalBox", SqlDbType.Int,9, ParameterDirection.Output,false,18,3,"TotalBox", DataRowVersion.Current,0)
			};
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_GetTotalBox", prams);

            return (int)prams[1].Value;
        }

        /// <summary>
        /// 获取按出厂价计算的总销售额，不含DDF
        /// </summary>
        /// <returns></returns>
        public decimal GetTotalFactoryPriceValue()
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, HeadID),
                 new SqlParameter("@TotalValue", SqlDbType.Decimal,9, ParameterDirection.Output,false,18,3,"TotalValue", DataRowVersion.Current,0)
			};
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_GetTotalFactoryPriceValue", prams);

            return (decimal)prams[1].Value;
        }

        public void Approve(int StaffID)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, HeadID),
				SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4, StaffID)
			};
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_Approve", prams);
        }

        public int BatApprove(string IDS, int StaffID)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.VarChar, IDS.Length, IDS),
				SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4, StaffID)
			};
            #endregion

            return SQLDatabase.RunProc(_ProcePrefix + "_BatApprove", prams);
        }
        /// <summary>
        /// 获取指定管理片区合计实际销量
        /// </summary>
        /// <param name="AccountMonth"></param>
        /// <param name="OrganizeCity"></param>
        /// <param name="IncludeChildOrganizeCity"></param>
        /// <returns></returns>
        public decimal GetTotalVolume(int OrganizeCity, int AccountMonth, bool IncludeChildOrganizeCity)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, OrganizeCity),
                SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, AccountMonth),
				SQLDatabase.MakeInParam("@IncludeChildCity", SqlDbType.Int, 4, IncludeChildOrganizeCity ? 1 : 0),
                new SqlParameter("@TotalVolume", SqlDbType.Decimal,18, ParameterDirection.Output,false,18,3,"TotalVolume", DataRowVersion.Current,0)
            };
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_GetTotalVolume", prams);

            return (decimal)prams[3].Value;
        }

        /// <summary>
        /// 获取指定管理片区历史平均实际销量(门店实销)
        /// </summary>
        /// <param name="OrganizeCity"></param>
        /// <param name="MonthCount"></param>
        /// <param name="IncludeChildOrganizeCity"></param>
        /// <returns></returns>
        public decimal GetAvgVolume(int OrganizeCity, int EndMonth, int MonthCount, bool IncludeChildOrganizeCity)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, OrganizeCity),
                SQLDatabase.MakeInParam("@EndMonth", SqlDbType.Int, 4, EndMonth),
                SQLDatabase.MakeInParam("@MonthCount", SqlDbType.Int, 4, MonthCount),
				SQLDatabase.MakeInParam("@IncludeChildCity", SqlDbType.Int, 4, IncludeChildOrganizeCity ? 1 : 0),
                new SqlParameter("@AvgVolume", SqlDbType.Decimal,18, ParameterDirection.Output,false,18,3,"AvgVolume", DataRowVersion.Current,0)
            };
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_GetAvgVolume", prams);

            return (decimal)prams[4].Value;
        }

        /// <summary>
        /// Get summary info by retailer,begindate,enddate
        /// </summary>
        /// <param name="retailer"></param>
        /// <param name="begintime"></param>
        /// <param name="endtime"></param>
        /// <returns></returns>
        public SqlDataReader GetSummary(int OrganizeCity, int Supplier, int ClientID, DateTime begintime, DateTime endtime, int Type, int Flag)
        {
            #region	设置参数集
            SqlParameter[] prams = {
                SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4,OrganizeCity),
                SQLDatabase.MakeInParam("@SupplierID", SqlDbType.Int, 4,Supplier),
				SQLDatabase.MakeInParam("@ClientID", SqlDbType.Int, 4,ClientID),
              	SQLDatabase.MakeInParam("@BeginTime", SqlDbType.DateTime, 8,begintime),
                SQLDatabase.MakeInParam("@EndTime", SqlDbType.DateTime, 8,endtime),
                SQLDatabase.MakeInParam("@Type", SqlDbType.Int, 4,Type),
                SQLDatabase.MakeInParam("@Flag", SqlDbType.Int, 4,Flag)
			};
            #endregion

            SqlDataReader dr = null;
            SQLDatabase.RunProc(_ProcePrefix + "_GetSummary", prams, out dr);
            return dr;
        }

        public SqlDataReader GetSummary_GroupByDate(int OrganizeCity, int Supplier, int ClientID, DateTime begintime, DateTime endtime, int Type, int Flag, int DataSource)
        {
            #region	设置参数集
            SqlParameter[] prams = {
                SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4,OrganizeCity),
                SQLDatabase.MakeInParam("@SupplierID", SqlDbType.Int, 4,Supplier),
				SQLDatabase.MakeInParam("@ClientID", SqlDbType.Int, 4,ClientID),
              	SQLDatabase.MakeInParam("@BeginTime", SqlDbType.DateTime, 8,begintime),
                SQLDatabase.MakeInParam("@EndTime", SqlDbType.DateTime, 8,endtime),
                SQLDatabase.MakeInParam("@Type", SqlDbType.Int, 4,Type),
                SQLDatabase.MakeInParam("@Flag", SqlDbType.Int, 4,Flag),
                SQLDatabase.MakeInParam("@DataSource", SqlDbType.Int, 4,DataSource)
			};
            #endregion

            SqlDataReader dr = null;
            SQLDatabase.RunProc(_ProcePrefix + "_GetSummary_GroupByDate", prams, out dr);
            return dr;
        }

        public SqlDataReader InitProductList(int VolumeID, int ClientID, int Type, int Month, bool IsCXP)
        {
            #region	设置参数集
            SqlParameter[] prams = {
                SQLDatabase.MakeInParam("@VolumeID", SqlDbType.Int, 4,VolumeID),
				SQLDatabase.MakeInParam("@ClientID", SqlDbType.Int, 4,ClientID),
                SQLDatabase.MakeInParam("@Type", SqlDbType.Int, 4,Type),
                SQLDatabase.MakeInParam("@Month", SqlDbType.Int, 4,Month),
                SQLDatabase.MakeInParam("@IsCXP", SqlDbType.Int, 4,IsCXP ? 1 : 0)
			};
            #endregion
            SqlDataReader dr = null;
            SQLDatabase.RunProc(_ProcePrefix + "_InitProductList", prams, out dr);
            return dr;
        }

        public int CheckSalesVolume(int ClientID, DateTime volumedate, int Type, int Flag)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ClientID", SqlDbType.Int, 4,ClientID),
              	SQLDatabase.MakeInParam("@volumedate", SqlDbType.DateTime, 8,volumedate),
                SQLDatabase.MakeInParam("@Type", SqlDbType.Int, 4,Type),
                SQLDatabase.MakeInParam("@Flag", SqlDbType.Int,4,Flag)
			};
            #endregion
            return SQLDatabase.RunProc(_ProcePrefix + "_CheckSalesVolume", prams);

        }

        #region
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SheetCode"></param>
        /// <param name="SalesDate"></param>
        /// <param name="Client"></param>
        /// <returns></returns>
        public int SVMinsert(string SheetCode, DateTime SalesDate, int Client)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@SheetCode", SqlDbType.VarChar, 50,SheetCode),
              	SQLDatabase.MakeInParam("@SalesDate", SqlDbType.DateTime, 8,SalesDate),
                SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4,Client)
			};
            #endregion
            return SQLDatabase.RunProc(_ProcePrefix + "_InsertSunCareJXCHeader", prams);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SalesVolume"></param>
        /// <param name="Product"></param>
        /// <param name="LotNumber"></param>
        /// <param name="SalesPrice"></param>
        /// <param name="Quantity"></param>
        /// <param name="FactoryPrice"></param>
        /// <returns></returns>
        public int SVMinsertdetail(int SalesVolume, string Code, string LotNumber, int Client, int Quantity, decimal FactoryPrice)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@SalesVolume", SqlDbType.Int, 4,SalesVolume),
              	SQLDatabase.MakeInParam("@Code ", SqlDbType.VarChar, 50,Code ),
                SQLDatabase.MakeInParam("@LotNumber", SqlDbType.VarChar, 50,LotNumber),
                SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4,Client),
              	SQLDatabase.MakeInParam("@Quantity", SqlDbType.Int, 4,Quantity),
                SQLDatabase.MakeInParam("@FactoryPrice", SqlDbType.Decimal, 9,FactoryPrice)
			};
            #endregion
            return SQLDatabase.RunProc(_ProcePrefix + "_InsertSunCareJXCDetail", prams);
        }
        #endregion


        public decimal GetTotalSalesPriceByOrganizeCity(int organizecity, int accountmonth, int ClientType, int Type)
        {
            #region	设置参数集
            SqlParameter[] prams = {
                SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4 ,organizecity),
                SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4,accountmonth),
                SQLDatabase.MakeInParam("@ClientType", SqlDbType.Int, 4,ClientType),
                SQLDatabase.MakeInParam("@Type", SqlDbType.Int, 4,Type),
                new SqlParameter("@TotalValue", SqlDbType.Decimal,9, ParameterDirection.Output,false,18,2,"TotalValue", DataRowVersion.Current,0)
			};
            #endregion
            SQLDatabase.RunProc(_ProcePrefix + "_GetTotalSalesPriceByOrganizeCity", prams);
            return (decimal)prams[4].Value;
        }

        /// <summary>
        /// 获取客户销量(进货、销量、库存)录入记录数量
        /// </summary>
        /// <param name="AccountMonth"></param>
        /// <param name="Client"></param>
        /// <returns></returns>
        public DataTable GetCountByClient(int AccountMonth, int Client)
        {
            SqlDataReader dr = null;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4,AccountMonth),
              	SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4,Client)
			};
            #endregion
            SQLDatabase.RunProc(_ProcePrefix + "_GetCountByClient", prams, out dr);

            return MCSFramework.Common.Tools.ConvertDataReaderToDataTable(dr);
        }

        /// <summary>
        /// 获取某个客户指定月指定品类的合计销量
        /// </summary>
        /// <param name="Classfiy"></param>
        /// <param name="AccountMonth"></param>
        /// <param name="Client"></param>
        /// <returns></returns>
        public decimal GetByClassify(int Classify, int AccountMonth, int Client)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Classify", SqlDbType.Int, 4,Classify),
              	SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4,AccountMonth),
                SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4,Client),
                new SqlParameter("@SumAmount", SqlDbType.Decimal,9, ParameterDirection.Output,false,18,2,"SumAmount", DataRowVersion.Current,0)
			};
            #endregion
            SQLDatabase.RunProc(_ProcePrefix + "_GetByClassify", prams);
            return (decimal)prams[3].Value;
        }

        /// <summary>
        /// 获取进货,销量的汇总
        /// </summary>
        /// <param name="organizecity"></param>
        /// <param name="accountmonth"></param>
        /// <param name="clienttype"></param>
        /// <param name="flag"></param>
        /// <param name="level"></param>
        /// <param name="state"></param>
        /// <param name="type"></param>
        /// <param name="staff"></param>
        /// <returns></returns>
        public SqlDataReader GetSummaryTotal(int organizecity, int accountmonth, int clienttype, int flag, int level, int state, int type, int staff)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4 ,organizecity),
                SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4,accountmonth),
                SQLDatabase.MakeInParam("@ClientType", SqlDbType.Int, 4,clienttype),
                SQLDatabase.MakeInParam("@Level", SqlDbType.Int, 4,level),
                SQLDatabase.MakeInParam("@Flag", SqlDbType.Int, 4,flag),
                SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4,state),
                SQLDatabase.MakeInParam("@Type", SqlDbType.Int, 4,type),
                SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4,staff)
			};
            #endregion
            SqlDataReader dr = null;
            SQLDatabase.RunProc(_ProcePrefix + "_GetSummaryTotal", prams, out dr, 600);
            return dr;
        }

        /// <summary>
        /// 获取进货,销量的汇总(按门店或经销商，按SKU)
        /// </summary>
        /// <param name="organizecity"></param>
        /// <param name="accountmonth"></param>
        /// <param name="clienttype"></param>
        /// <param name="flag"></param>
        /// <param name="state"></param>
        /// <param name="type"></param>
        /// <param name="staff"></param>
        /// <returns></returns>
        public SqlDataReader GetSummaryTotal2(int organizecity, int accountmonth, int clienttype, int flag, int state, int type, int staff)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4 ,organizecity),
                SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4,accountmonth),
                SQLDatabase.MakeInParam("@ClientType", SqlDbType.Int, 4,clienttype),
                SQLDatabase.MakeInParam("@Flag", SqlDbType.Int, 4,flag),
                SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4,state),
                SQLDatabase.MakeInParam("@Type", SqlDbType.Int, 4,type),
                SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4,staff)
			};
            #endregion
            SqlDataReader dr = null;
            SQLDatabase.RunProc(_ProcePrefix + "_GetSummaryTotal2", prams, out dr, 600);
            return dr;
        }

        public int ApproveByStaff(int organizecity, int staff, int accountmonth, int clienttype, int flag, int type)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4 ,organizecity),
                SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4 ,staff),
                SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4,accountmonth),
                SQLDatabase.MakeInParam("@ClientType", SqlDbType.Int, 4,clienttype),         
                SQLDatabase.MakeInParam("@Flag", SqlDbType.Int, 4,flag),               
                SQLDatabase.MakeInParam("@Type", SqlDbType.Int, 4,type)
			};
            #endregion
            return SQLDatabase.RunProc(_ProcePrefix + "_ApproveByStaff", prams, 600);
        }

        public int SubmitByStaff(int organizecity, int staff, int accountmonth, int clienttype, int flag, int type)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4 ,organizecity),
                SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4 ,staff),
                SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4,accountmonth),
                SQLDatabase.MakeInParam("@ClientType", SqlDbType.Int, 4,clienttype),         
                SQLDatabase.MakeInParam("@Flag", SqlDbType.Int, 4,flag),               
                SQLDatabase.MakeInParam("@Type", SqlDbType.Int, 4,type)
			};
            #endregion
            return SQLDatabase.RunProc(_ProcePrefix + "_SubmitByStaff", prams, 600);
        }
    }
}

