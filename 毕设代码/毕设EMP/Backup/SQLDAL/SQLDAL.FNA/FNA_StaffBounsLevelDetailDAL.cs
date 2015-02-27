
// ===================================================================
// 文件： FNA_StaffBounsLevelDetailDAL.cs
// 项目名称：
// 创建时间：2013-08-02
// 作者:	   Jace
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
    ///FNA_StaffBounsLevelDetail数据访问DAL类
    /// </summary>
    public class FNA_StaffBounsLevelDetailDAL : BaseSimpleDAL<FNA_StaffBounsLevelDetail>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public FNA_StaffBounsLevelDetailDAL()
        {
            _ProcePrefix = "MCS_FNA.dbo.sp_FNA_StaffBounsLevelDetail";
        }
        #endregion


        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(FNA_StaffBounsLevelDetail m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, m.OrganizeCity),
				SQLDatabase.MakeInParam("@SalesVolume", SqlDbType.Decimal, 9, m.SalesVolume),
                SQLDatabase.MakeInParam("@AccountQuarter",SqlDbType.Int,4,m.AccountQuarter),
				SQLDatabase.MakeInParam("@SalesAdjust", SqlDbType.Decimal, 9, m.SalesAdjust),
				SQLDatabase.MakeInParam("@Level", SqlDbType.Int, 4, m.Level),
				SQLDatabase.MakeInParam("@Bouns", SqlDbType.Decimal, 9, m.Bouns),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 2000, m.Remark),
                SQLDatabase.MakeInParam("@BudgetFeeRate", SqlDbType.Decimal, 9, m.BudgetFeeRate),
                SQLDatabase.MakeInParam("@ActFeeRate", SqlDbType.Decimal, 9, m.ActFeeRate),
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
        public override int Update(FNA_StaffBounsLevelDetail m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, m.OrganizeCity),
                SQLDatabase.MakeInParam("@AccountQuarter",SqlDbType.Int,4,m.AccountQuarter),
				SQLDatabase.MakeInParam("@SalesVolume", SqlDbType.Decimal, 9, m.SalesVolume),
				SQLDatabase.MakeInParam("@SalesAdjust", SqlDbType.Decimal, 9, m.SalesAdjust),
				SQLDatabase.MakeInParam("@Level", SqlDbType.Int, 4, m.Level),
				SQLDatabase.MakeInParam("@Bouns", SqlDbType.Decimal, 9, m.Bouns),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 2000, m.Remark),
                SQLDatabase.MakeInParam("@BudgetFeeRate", SqlDbType.Decimal, 9, m.BudgetFeeRate),
                SQLDatabase.MakeInParam("@ActFeeRate", SqlDbType.Decimal, 9, m.ActFeeRate),
				SQLDatabase.MakeInParam("@UpdateStaff", SqlDbType.Int, 4, m.UpdateStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override FNA_StaffBounsLevelDetail FillModel(IDataReader dr)
        {
            FNA_StaffBounsLevelDetail m = new FNA_StaffBounsLevelDetail();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["OrganizeCity"].ToString())) m.OrganizeCity = (int)dr["OrganizeCity"];
            if (!string.IsNullOrEmpty(dr["AccountQuarter"].ToString())) m.AccountQuarter = (int)dr["AccountQuarter"];
            if (!string.IsNullOrEmpty(dr["SalesVolume"].ToString())) m.SalesVolume = (decimal)dr["SalesVolume"];
            if (!string.IsNullOrEmpty(dr["SalesAdjust"].ToString())) m.SalesAdjust = (decimal)dr["SalesAdjust"];
            if (!string.IsNullOrEmpty(dr["Level"].ToString())) m.Level = (int)dr["Level"];
            if (!string.IsNullOrEmpty(dr["Bouns"].ToString())) m.Bouns = (decimal)dr["Bouns"];
            if (!string.IsNullOrEmpty(dr["Remark"].ToString())) m.Remark = (string)dr["Remark"];
            if (!string.IsNullOrEmpty(dr["BudgetFeeRate"].ToString())) m.BudgetFeeRate = (decimal)dr["BudgetFeeRate"];
            if (!string.IsNullOrEmpty(dr["ActFeeRate"].ToString())) m.ActFeeRate = (decimal)dr["ActFeeRate"];
            if (!string.IsNullOrEmpty(dr["InsertTime"].ToString())) m.InsertTime = (DateTime)dr["InsertTime"];
            if (!string.IsNullOrEmpty(dr["InsertStaff"].ToString())) m.InsertStaff = (int)dr["InsertStaff"];
            if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString())) m.UpdateTime = (DateTime)dr["UpdateTime"];
            if (!string.IsNullOrEmpty(dr["UpdateStaff"].ToString())) m.UpdateStaff = (int)dr["UpdateStaff"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);

            return m;
        }

        public int Init(int AccountQuarter, int Staff)
        {
            #region	设置参数集
            SqlParameter[] prams = {
                SQLDatabase.MakeInParam("@AccountQuarter",SqlDbType.Int,4,AccountQuarter),
				SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4, Staff)
			};
            #endregion
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Init", prams);

            return ret;
        }
    }
}

