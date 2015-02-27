
// ===================================================================
// 文件： SVM_ClassifyForcastDAL.cs
// 项目名称：
// 创建时间：2011/10/13
// 作者:	   chf
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
    ///SVM_ClassifyForcast数据访问DAL类
    /// </summary>
    public class SVM_ClassifyForcastDAL : BaseComplexDAL<SVM_ClassifyForcast, SVM_ClassifyForcast_Detail>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public SVM_ClassifyForcastDAL()
        {
            _ProcePrefix = "MCS_SVM.dbo.sp_SVM_ClassifyForcast";
        }
        #endregion


        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(SVM_ClassifyForcast m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, m.AccountMonth),
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, m.OrganizeCity),
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, m.Client),
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
        public override int Update(SVM_ClassifyForcast m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, m.AccountMonth),
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, m.OrganizeCity),
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, m.Client),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
                SQLDatabase.MakeInParam("@TaskID", SqlDbType.Int, 4, m.TaskID),
				SQLDatabase.MakeInParam("@UpdateStaff", SqlDbType.Int, 4, m.UpdateStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override SVM_ClassifyForcast FillModel(IDataReader dr)
        {
            SVM_ClassifyForcast m = new SVM_ClassifyForcast();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["AccountMonth"].ToString())) m.AccountMonth = (int)dr["AccountMonth"];
            if (!string.IsNullOrEmpty(dr["OrganizeCity"].ToString())) m.OrganizeCity = (int)dr["OrganizeCity"];
            if (!string.IsNullOrEmpty(dr["Client"].ToString())) m.Client = (int)dr["Client"];
            if (!string.IsNullOrEmpty(dr["ApproveFlag"].ToString())) m.ApproveFlag = (int)dr["ApproveFlag"];
            if (!string.IsNullOrEmpty(dr["TaskID"].ToString())) m.TaskID = (int)dr["TaskID"];
            if (!string.IsNullOrEmpty(dr["InsertTime"].ToString())) m.InsertTime = (DateTime)dr["InsertTime"];
            if (!string.IsNullOrEmpty(dr["InsertStaff"].ToString())) m.InsertStaff = (int)dr["InsertStaff"];
            if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString())) m.UpdateTime = (DateTime)dr["UpdateTime"];
            if (!string.IsNullOrEmpty(dr["UpdateStaff"].ToString())) m.UpdateStaff = (int)dr["UpdateStaff"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);

            return m;
        }

        public override int AddDetail(SVM_ClassifyForcast_Detail m)
        {
            m.ForcastID = HeadID;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ForcastID", SqlDbType.Int, 4, m.ForcastID),
				SQLDatabase.MakeInParam("@Classify", SqlDbType.Int, 4, m.Classify),
                SQLDatabase.MakeInParam("@Rate", SqlDbType.Decimal, 9, m.Rate),             
				SQLDatabase.MakeInParam("@Amount", SqlDbType.Decimal, 9, m.Amount),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 2000, m.Remark)
			};
            #endregion

            m.ID = SQLDatabase.RunProc(_ProcePrefix + "_AddDetail", prams);

            return m.ID;
        }

        public override int UpdateDetail(SVM_ClassifyForcast_Detail m)
        {
            m.ForcastID = HeadID;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@ForcastID", SqlDbType.Int, 4, m.ForcastID),
				SQLDatabase.MakeInParam("@Classify", SqlDbType.Int, 4, m.Classify),
                SQLDatabase.MakeInParam("@Rate", SqlDbType.Decimal, 9, m.Rate),              
				SQLDatabase.MakeInParam("@Amount", SqlDbType.Decimal, 9, m.Amount),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 2000, m.Remark)
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_UpdateDetail", prams);

            return ret;
        }

        protected override SVM_ClassifyForcast_Detail FillDetailModel(IDataReader dr)
        {
            SVM_ClassifyForcast_Detail m = new SVM_ClassifyForcast_Detail();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["ForcastID"].ToString())) m.ForcastID = (int)dr["ForcastID"];
            if (!string.IsNullOrEmpty(dr["Classify"].ToString())) m.Classify = (int)dr["Classify"];
            if (!string.IsNullOrEmpty(dr["Rate"].ToString())) m.Rate = (decimal)dr["Rate"];
            if (!string.IsNullOrEmpty(dr["AvgSales"].ToString())) m.AvgSales = (decimal)dr["AvgSales"];
            if (!string.IsNullOrEmpty(dr["Amount"].ToString())) m.Amount = (decimal)dr["Amount"];
            if (!string.IsNullOrEmpty(dr["Remark"].ToString())) m.Remark = (string)dr["Remark"];


            return m;
        }

        public int Init(int OrganizeCity, int AccountMonth, int ClientID, int StaffID)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4,OrganizeCity),
                SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4,AccountMonth),
                SQLDatabase.MakeInParam("@ClientID", SqlDbType.Int, 4,ClientID),
                SQLDatabase.MakeInParam("@StaffID", SqlDbType.Int, 4,StaffID)
			};
            #endregion
            return SQLDatabase.RunProc(_ProcePrefix + "_Init", prams);
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

        public int Submit(int ForcastID, int TaskID)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, ForcastID),
				SQLDatabase.MakeInParam("@TaskID", SqlDbType.Int,4,TaskID)               
            };
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Submit", prams);
            return ret;
            #endregion
        }

        public void RefreshAvgSales(int ForcastID)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ForcastID", SqlDbType.Int, 4, ForcastID)				              
            };
           SQLDatabase.RunProc(_ProcePrefix + "_RefreshAvgSales", prams);
          
            #endregion
        }
    }
}

