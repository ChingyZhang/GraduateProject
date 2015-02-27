
// ===================================================================
// 文件： FNA_BudgetExtraApplyDAL.cs
// 项目名称：
// 创建时间：2010/8/19
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.FNA;


namespace MCSFramework.SQLDAL.FNA
{
    /// <summary>
    ///FNA_BudgetExtraApply数据访问DAL类
    /// </summary>
    public class FNA_BudgetExtraApplyDAL : BaseSimpleDAL<FNA_BudgetExtraApply>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public FNA_BudgetExtraApplyDAL()
        {
            _ProcePrefix = "MCS_FNA.dbo.sp_FNA_BudgetExtraApply";
        }
        #endregion


        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(FNA_BudgetExtraApply m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, m.OrganizeCity),
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, m.AccountMonth),
				SQLDatabase.MakeInParam("@FeeType", SqlDbType.Int, 4, m.FeeType),
				SQLDatabase.MakeInParam("@ExtraAmount", SqlDbType.Decimal, 9, m.ExtraAmount),
				SQLDatabase.MakeInParam("@AdjustAmount", SqlDbType.Decimal, 9, m.AdjustAmount),
				SQLDatabase.MakeInParam("@Description", SqlDbType.VarChar, 500, m.Description),
				SQLDatabase.MakeInParam("@ApproveTask", SqlDbType.Int, 4, m.ApproveTask),
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
        public override int Update(FNA_BudgetExtraApply m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, m.OrganizeCity),
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, m.AccountMonth),
				SQLDatabase.MakeInParam("@FeeType", SqlDbType.Int, 4, m.FeeType),
				SQLDatabase.MakeInParam("@ExtraAmount", SqlDbType.Decimal, 9, m.ExtraAmount),
				SQLDatabase.MakeInParam("@AdjustAmount", SqlDbType.Decimal, 9, m.AdjustAmount),
				SQLDatabase.MakeInParam("@Description", SqlDbType.VarChar, 500, m.Description),
				SQLDatabase.MakeInParam("@ApproveTask", SqlDbType.Int, 4, m.ApproveTask),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@UpdateStaff", SqlDbType.Int, 4, m.UpdateStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override FNA_BudgetExtraApply FillModel(IDataReader dr)
        {
            FNA_BudgetExtraApply m = new FNA_BudgetExtraApply();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["OrganizeCity"].ToString())) m.OrganizeCity = (int)dr["OrganizeCity"];
            if (!string.IsNullOrEmpty(dr["AccountMonth"].ToString())) m.AccountMonth = (int)dr["AccountMonth"];
            if (!string.IsNullOrEmpty(dr["FeeType"].ToString())) m.FeeType = (int)dr["FeeType"];
            if (!string.IsNullOrEmpty(dr["ExtraAmount"].ToString())) m.ExtraAmount = (decimal)dr["ExtraAmount"];
            if (!string.IsNullOrEmpty(dr["AdjustAmount"].ToString())) m.AdjustAmount = (decimal)dr["AdjustAmount"];
            if (!string.IsNullOrEmpty(dr["Description"].ToString())) m.Description = (string)dr["Description"];
            if (!string.IsNullOrEmpty(dr["ApproveTask"].ToString())) m.ApproveTask = (int)dr["ApproveTask"];
            if (!string.IsNullOrEmpty(dr["ApproveFlag"].ToString())) m.ApproveFlag = (int)dr["ApproveFlag"];
            if (!string.IsNullOrEmpty(dr["InsertTime"].ToString())) m.InsertTime = (DateTime)dr["InsertTime"];
            if (!string.IsNullOrEmpty(dr["InsertStaff"].ToString())) m.InsertStaff = (int)dr["InsertStaff"];
            if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString())) m.UpdateTime = (DateTime)dr["UpdateTime"];
            if (!string.IsNullOrEmpty(dr["UpdateStaff"].ToString())) m.UpdateStaff = (int)dr["UpdateStaff"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);

            return m;
        }

        public int Submit(int ID, int Staff, int ApproveTask)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, ID),
				SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4, Staff),
				SQLDatabase.MakeInParam("@ApproveTask", SqlDbType.Int, 4, ApproveTask)				
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Submit", prams);

            return ret;
        }

        /// <summary>
        /// 获取指定管理片区指定月的已批复扩充的总预算额度
        /// </summary>
        /// <param name="AccountMonth"></param>
        /// <param name="OrganizeCity"></param>
        /// <param name="FeeType"></param>
        /// <param name="IncludeChildOrganizeCity">是否包括子管理片区 0:不包括 1:包括</param>
        /// <returns></returns>
        public decimal GetExtraAmount(int AccountMonth, int OrganizeCity, int FeeType, bool IncludeChildOrganizeCity)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, AccountMonth),
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, OrganizeCity),
                SQLDatabase.MakeInParam("@FeeType", SqlDbType.Int, 4, FeeType),
                SQLDatabase.MakeInParam("@IncludeChildCity", SqlDbType.Int, 4, IncludeChildOrganizeCity ? 1 : 0),
                new SqlParameter("@ExtraAmount", SqlDbType.Decimal,18, ParameterDirection.Output,false,18,3,"ExtraAmount", DataRowVersion.Current,0)
            };
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_GetExtraAmount", prams);

            return (decimal)prams[4].Value;
        }

        public string GenerateSheetCode(int organizecity)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, organizecity),
				SQLDatabase.MakeOutParam("@SheetCode", SqlDbType.VarChar, 100)
			};
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_GenerateSheetCode", prams);

            return (string)prams[1].Value;
        }

        public void UpdateAdjustRecord(int ID, int Staff, int FeeType, string OldAdjustCost, string AdjustCost, string AdjustReason)
        {
            #region	设置参数集
            SqlParameter[] prams = {
                SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, ID),
				SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4, Staff),
                SQLDatabase.MakeInParam("@@FeeType", SqlDbType.Int, 4, FeeType),
                SQLDatabase.MakeInParam("@OldAdjustCost", SqlDbType.VarChar, 20,OldAdjustCost),
                SQLDatabase.MakeInParam("@AdjustCost", SqlDbType.VarChar,20, AdjustCost),
                SQLDatabase.MakeInParam("@AdjustReason",SqlDbType.VarChar,500,AdjustReason)
			};
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_UpdateAdjustRecord", prams);
        }
    }
}

