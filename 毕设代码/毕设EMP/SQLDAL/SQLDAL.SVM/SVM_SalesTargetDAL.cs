
// ===================================================================
// 文件： SVM_SalesTargetDAL.cs
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
	///SVM_SalesTarget数据访问DAL类
	/// </summary>
	public class SVM_SalesTargetDAL : BaseComplexDAL<SVM_SalesTarget,SVM_SalesTarget_Detail>
	{
		#region 构造函数
		///<summary>
		///
		///</summary>
		public SVM_SalesTargetDAL()
		{
			_ProcePrefix = "MCS_SVM.dbo.sp_SVM_SalesTarget";
		}
		#endregion
		
		
		/// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(SVM_SalesTarget m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, m.AccountMonth),
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, m.OrganizeCity),
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, m.Client),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@InsertTime", SqlDbType.DateTime, 8, m.InsertTime),
				SQLDatabase.MakeInParam("@InsertStaff", SqlDbType.Int, 4, m.InsertStaff),
				SQLDatabase.MakeInParam("@UpdateTime", SqlDbType.DateTime, 8, m.UpdateTime),
				SQLDatabase.MakeInParam("@UpdateStaff", SqlDbType.Int, 4, m.UpdateStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 8000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
			#endregion
			
            m.ID =  SQLDatabase.RunProc(_ProcePrefix + "_Add", prams);
			
            return m.ID;
        }
		
		/// <summary>
        /// 更新项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Update(SVM_SalesTarget m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, m.AccountMonth),
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, m.OrganizeCity),
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, m.Client),
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
		
        protected override SVM_SalesTarget FillModel(IDataReader dr)
		{
			SVM_SalesTarget m = new SVM_SalesTarget();
			if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (int)dr["ID"];
			if (!string.IsNullOrEmpty(dr["AccountMonth"].ToString()))	m.AccountMonth = (int)dr["AccountMonth"];
			if (!string.IsNullOrEmpty(dr["OrganizeCity"].ToString()))	m.OrganizeCity = (int)dr["OrganizeCity"];
			if (!string.IsNullOrEmpty(dr["Client"].ToString()))	m.Client = (int)dr["Client"];
			if (!string.IsNullOrEmpty(dr["ApproveFlag"].ToString()))	m.ApproveFlag = (int)dr["ApproveFlag"];
			if (!string.IsNullOrEmpty(dr["InsertTime"].ToString()))	m.InsertTime = (DateTime)dr["InsertTime"];
			if (!string.IsNullOrEmpty(dr["InsertStaff"].ToString()))	m.InsertStaff = (int)dr["InsertStaff"];
			if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString()))	m.UpdateTime = (DateTime)dr["UpdateTime"];
			if (!string.IsNullOrEmpty(dr["UpdateStaff"].ToString()))	m.UpdateStaff = (int)dr["UpdateStaff"];
			if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString()))	m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);
						
			return m;
		}
		
		public override int AddDetail(SVM_SalesTarget_Detail m)
        {
			m.TargetID = HeadID;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@TargetID", SqlDbType.Int, 4, m.TargetID),
				SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, m.Product),
				SQLDatabase.MakeInParam("@Quantity", SqlDbType.Int, 4, m.Quantity),
				SQLDatabase.MakeInParam("@FactoryPrice", SqlDbType.Decimal, 9, m.FactoryPrice),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 2000, m.Remark)
			};
            #endregion

            m.ID = SQLDatabase.RunProc(_ProcePrefix + "_AddDetail", prams);

            return m.ID;
        }

        public override int UpdateDetail(SVM_SalesTarget_Detail m)
        {
            m.TargetID = HeadID;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@TargetID", SqlDbType.Int, 4, m.TargetID),
				SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, m.Product),
				SQLDatabase.MakeInParam("@Quantity", SqlDbType.Int, 4, m.Quantity),
				SQLDatabase.MakeInParam("@FactoryPrice", SqlDbType.Decimal, 9, m.FactoryPrice),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 2000, m.Remark)
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix+"_UpdateDetail", prams);

            return ret;
        }

        protected override SVM_SalesTarget_Detail FillDetailModel(IDataReader dr)
        {
            SVM_SalesTarget_Detail m = new SVM_SalesTarget_Detail();
            if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (int)dr["ID"];
			if (!string.IsNullOrEmpty(dr["TargetID"].ToString()))	m.TargetID = (int)dr["TargetID"];
			if (!string.IsNullOrEmpty(dr["Product"].ToString()))	m.Product = (int)dr["Product"];
			if (!string.IsNullOrEmpty(dr["Quantity"].ToString()))	m.Quantity = (int)dr["Quantity"];
			if (!string.IsNullOrEmpty(dr["FactoryPrice"].ToString()))	m.FactoryPrice = (decimal)dr["FactoryPrice"];
			if (!string.IsNullOrEmpty(dr["Remark"].ToString()))	m.Remark = (string)dr["Remark"];
					

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

        public SqlDataReader GetInputDetail(int TargetID)
        {
            SqlDataReader dr = null;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@TargetID", SqlDbType.Int, 4,TargetID)
			};
            #endregion
            SQLDatabase.RunProc(_ProcePrefix + "_GetInputDetail", prams, out dr);
            return dr;
        }

        public int Approve(int ID, int StaffID)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, ID),
				SQLDatabase.MakeInParam("@StaffID", SqlDbType.Int, 4, StaffID)
			};
            #endregion

            return SQLDatabase.RunProc(_ProcePrefix + "_Approve", prams);
        }

        public SqlDataReader GetListByOrganizeCity(int OrganizeCity, int BeginMonth, int EndMonth, int Client)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4,OrganizeCity),
                SQLDatabase.MakeInParam("@BeginMonth", SqlDbType.Int, 4,BeginMonth),
                SQLDatabase.MakeInParam("@EndMonth", SqlDbType.Int, 4,EndMonth),
                SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4,Client)
			};
            #endregion
            SqlDataReader dr = null;
            SQLDatabase.RunProc(_ProcePrefix + "_GetListByOrganizeCity", prams, out dr);
            return dr;
        }

        public decimal GetTargetSumPrice(int TargetID)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@TargetID", SqlDbType.Int, 4, TargetID)
			};
            #endregion

            return SQLDatabase.RunProc(_ProcePrefix + "_GetTargetSumPrice", prams);
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
    }
}

