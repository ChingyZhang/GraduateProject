
// ===================================================================
// 文件： SVM_SalesForcastDAL.cs
// 项目名称：
// 创建时间：2009/3/8
// 作者:	   Shen Gang
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
    ///SVM_SalesForcast数据访问DAL类
    /// </summary>
    public class SVM_SalesForcastDAL : BaseComplexDAL<SVM_SalesForcast, SVM_SalesForcast_Detail>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public SVM_SalesForcastDAL()
        {
            _ProcePrefix = "MCS_SVM.dbo.sp_SVM_SalesForcast";
        }
        #endregion


        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(SVM_SalesForcast m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, m.AccountMonth),
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, m.OrganizeCity),
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, m.Client),
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
        public override int Update(SVM_SalesForcast m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, m.AccountMonth),
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, m.OrganizeCity),
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, m.Client),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@UpdateStaff", SqlDbType.Int, 4, m.UpdateStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 8000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override SVM_SalesForcast FillModel(IDataReader dr)
        {
            SVM_SalesForcast m = new SVM_SalesForcast();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["AccountMonth"].ToString())) m.AccountMonth = (int)dr["AccountMonth"];
            if (!string.IsNullOrEmpty(dr["OrganizeCity"].ToString())) m.OrganizeCity = (int)dr["OrganizeCity"];
            if (!string.IsNullOrEmpty(dr["Client"].ToString())) m.Client = (int)dr["Client"];
            if (!string.IsNullOrEmpty(dr["ApproveFlag"].ToString())) m.ApproveFlag = (int)dr["ApproveFlag"];
            if (!string.IsNullOrEmpty(dr["InsertTime"].ToString())) m.InsertTime = (DateTime)dr["InsertTime"];
            if (!string.IsNullOrEmpty(dr["InsertStaff"].ToString())) m.InsertStaff = (int)dr["InsertStaff"];
            if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString())) m.UpdateTime = (DateTime)dr["UpdateTime"];
            if (!string.IsNullOrEmpty(dr["UpdateStaff"].ToString())) m.UpdateStaff = (int)dr["UpdateStaff"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);

            return m;
        }

        public override int AddDetail(SVM_SalesForcast_Detail m)
        {
            m.ForcastID = HeadID;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ForcastID", SqlDbType.Int, 4, m.ForcastID),
				SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, m.Product),
				SQLDatabase.MakeInParam("@Quantity", SqlDbType.Int, 4, m.Quantity),
				SQLDatabase.MakeInParam("@FactoryPrice", SqlDbType.Decimal, 9, m.FactoryPrice),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 2000, m.Remark)
			};
            #endregion

            m.ID = SQLDatabase.RunProc(_ProcePrefix + "_AddDetail", prams);

            return m.ID;
        }

        public override int UpdateDetail(SVM_SalesForcast_Detail m)
        {
            m.ForcastID = HeadID;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@ForcastID", SqlDbType.Int, 4, m.ForcastID),
				SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, m.Product),
				SQLDatabase.MakeInParam("@Quantity", SqlDbType.Int, 4, m.Quantity),
				SQLDatabase.MakeInParam("@FactoryPrice", SqlDbType.Decimal, 9, m.FactoryPrice),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 2000, m.Remark)
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_UpdateDetail", prams);

            return ret;
        }

        protected override SVM_SalesForcast_Detail FillDetailModel(IDataReader dr)
        {
            SVM_SalesForcast_Detail m = new SVM_SalesForcast_Detail();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["ForcastID"].ToString())) m.ForcastID = (int)dr["ForcastID"];
            if (!string.IsNullOrEmpty(dr["Product"].ToString())) m.Product = (int)dr["Product"];
            if (!string.IsNullOrEmpty(dr["Quantity"].ToString())) m.Quantity = (int)dr["Quantity"];
            if (!string.IsNullOrEmpty(dr["FactoryPrice"].ToString())) m.FactoryPrice = (decimal)dr["FactoryPrice"];
            if (!string.IsNullOrEmpty(dr["Remark"].ToString())) m.Remark = (string)dr["Remark"];


            return m;
        }

        public int InitProductList(int OrganizeCity, int AccountMonth, int ClientID, int StaffID)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4,OrganizeCity),
                SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4,AccountMonth),
                SQLDatabase.MakeInParam("@ClientID", SqlDbType.Int, 4,ClientID),
                SQLDatabase.MakeInParam("@StaffID", SqlDbType.Int, 4,StaffID)
			};
            #endregion
            return SQLDatabase.RunProc(_ProcePrefix + "_InitProductList", prams);
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

        public decimal GetForcastSumPrice(int ForcastID)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ForcastID", SqlDbType.Int, 4, ForcastID)
			};
            #endregion

            return SQLDatabase.RunProc(_ProcePrefix + "_GetForcastSumPrice", prams);
        }

        /// <summary>
        /// 获取指定管理片区合计预计销量
        /// </summary>
        /// <param name="AccountMonth"></param>
        /// <param name="OrganizeCity"></param>
        /// <param name="IncludeChildOrganizeCity"></param>
        /// <returns></returns>
        public decimal GetTotalVolume(int AccountMonth, int OrganizeCity, bool IncludeChildOrganizeCity)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, AccountMonth),
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, OrganizeCity),
                SQLDatabase.MakeInParam("@IncludeChildCity", SqlDbType.Int, 4, IncludeChildOrganizeCity ? 1 : 0),
                new SqlParameter("@TotalVolume", SqlDbType.Decimal,18, ParameterDirection.Output,false,18,3,"TotalVolume", DataRowVersion.Current,0)
            };
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_GetTotalVolume", prams);

            return (decimal)prams[3].Value;
        }

        /// <summary>
        /// 获取指定客户的合计预计销量
        /// </summary>
        /// <param name="AccountMonth"></param>
        /// <param name="Client"></param>
        /// <returns></returns>
        public decimal GetTotalVolumeByClient(int AccountMonth, int Client)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, AccountMonth),
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, Client),
                new SqlParameter("@TotalVolume", SqlDbType.Decimal,18, ParameterDirection.Output,false,18,3,"TotalVolume", DataRowVersion.Current,0)
            };
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_GetTotalVolumeByClient", prams);

            return (decimal)prams[2].Value;
        }

        public int GetSalesVolume(int Product, int AccountMonth, int Client)
        {
            #region	设置参数集
            SqlParameter[] prams = {
                SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, Product),
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, AccountMonth),
                SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, Client)
			};
            #endregion

            return SQLDatabase.RunProc(_ProcePrefix + "_GetSalesVolume", prams);
        }

        public SqlDataReader GetSummary(int OrganizeCity, int ClientID, int beginMonth, int endMonth, int ClientType)
        {
            SqlDataReader dr = null;
            #region	设置参数集
            SqlParameter[] prams = {
                SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4,OrganizeCity),
				SQLDatabase.MakeInParam("@ClientID", SqlDbType.Int, 4,ClientID),
              	SQLDatabase.MakeInParam("@beginMonth", SqlDbType.Int, 4,beginMonth),
                SQLDatabase.MakeInParam("@endMonth", SqlDbType.Int, 4,endMonth),
                SQLDatabase.MakeInParam("@ClientType", SqlDbType.Int, 4,ClientType)
			};
            #endregion
            SQLDatabase.RunProc(_ProcePrefix + "_GetSummary", prams, out dr);
            return dr;
        }

        public SqlDataReader RPT_001(string OrganizeCitys, int BeginMonth, int EndMonth)
        {
            SqlDataReader dr = null;
            #region	设置参数集
            SqlParameter[] prams = {
                SQLDatabase.MakeInParam("@OrganizeCitys", SqlDbType.VarChar, 4000,OrganizeCitys),
              	SQLDatabase.MakeInParam("@BeginMonth", SqlDbType.Int, 4,BeginMonth),
                SQLDatabase.MakeInParam("@EndMonth", SqlDbType.Int, 4,EndMonth)
			};
            #endregion
            SQLDatabase.RunProc(_ProcePrefix + "_RPT_001", prams, out dr);
            return dr;
        }
        public int Submit(int ForcastID, string TaskID)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, ForcastID),
				SQLDatabase.MakeInParam("@TaskID", SqlDbType.VarChar,10,TaskID)               
            };
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Submit",prams);
            return ret;
            #endregion
        }
    }
}

