
// ===================================================================
// 文件： FNA_FeeWriteOffDAL.cs
// 项目名称：
// 创建时间：2009/2/22
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.FNA;
using MCSFramework.Common;


namespace MCSFramework.SQLDAL.FNA
{
    /// <summary>
    ///FNA_FeeWriteOff数据访问DAL类
    /// </summary>
    public class FNA_FeeWriteOffDAL : BaseComplexDAL<FNA_FeeWriteOff, FNA_FeeWriteOffDetail>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public FNA_FeeWriteOffDAL()
        {
            _ProcePrefix = "MCS_FNA.dbo.sp_FNA_FeeWriteOff";
        }
        #endregion


        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(FNA_FeeWriteOff m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@SheetCode", SqlDbType.VarChar, 100, m.SheetCode),
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, m.OrganizeCity),
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, m.AccountMonth),
				SQLDatabase.MakeInParam("@FeeType", SqlDbType.Int, 4, m.FeeType),
				SQLDatabase.MakeInParam("@InsteadPayClient", SqlDbType.Int, 4, m.InsteadPayClient),
				SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, m.State),
                SQLDatabase.MakeInParam("@ApproveTask", SqlDbType.Int, 4, m.ApproveTask),
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
        public override int Update(FNA_FeeWriteOff m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@SheetCode", SqlDbType.VarChar, 100, m.SheetCode),
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, m.OrganizeCity),
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, m.AccountMonth),
				SQLDatabase.MakeInParam("@FeeType", SqlDbType.Int, 4, m.FeeType),
                SQLDatabase.MakeInParam("@InsteadPayClient", SqlDbType.Int, 4, m.InsteadPayClient),
				SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, m.State),
                SQLDatabase.MakeInParam("@ApproveTask", SqlDbType.Int, 4, m.ApproveTask),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@UpdateStaff", SqlDbType.Int, 4, m.UpdateStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 8000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override FNA_FeeWriteOff FillModel(IDataReader dr)
        {
            FNA_FeeWriteOff m = new FNA_FeeWriteOff();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["SheetCode"].ToString())) m.SheetCode = (string)dr["SheetCode"];
            if (!string.IsNullOrEmpty(dr["OrganizeCity"].ToString())) m.OrganizeCity = (int)dr["OrganizeCity"];
            if (!string.IsNullOrEmpty(dr["AccountMonth"].ToString())) m.AccountMonth = (int)dr["AccountMonth"];
            if (!string.IsNullOrEmpty(dr["FeeType"].ToString())) m.FeeType = (int)dr["FeeType"];
            if (!string.IsNullOrEmpty(dr["InsteadPayClient"].ToString())) m.InsteadPayClient = (int)dr["InsteadPayClient"];
            if (!string.IsNullOrEmpty(dr["State"].ToString())) m.State = (int)dr["State"];
            if (!string.IsNullOrEmpty(dr["ApproveTask"].ToString())) m.ApproveTask = (int)dr["ApproveTask"];
            if (!string.IsNullOrEmpty(dr["ApproveFlag"].ToString())) m.ApproveFlag = (int)dr["ApproveFlag"];
            if (!string.IsNullOrEmpty(dr["InsertTime"].ToString())) m.InsertTime = (DateTime)dr["InsertTime"];
            if (!string.IsNullOrEmpty(dr["InsertStaff"].ToString())) m.InsertStaff = (int)dr["InsertStaff"];
            if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString())) m.UpdateTime = (DateTime)dr["UpdateTime"];
            if (!string.IsNullOrEmpty(dr["UpdateStaff"].ToString())) m.UpdateStaff = (int)dr["UpdateStaff"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);

            return m;
        }

        public override int AddDetail(FNA_FeeWriteOffDetail m)
        {
            m.WriteOffID = HeadID;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@WriteOffID", SqlDbType.Int, 4, m.WriteOffID),
				SQLDatabase.MakeInParam("@ApplyDetailID", SqlDbType.Int, 4, m.ApplyDetailID),
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, m.Client),
				SQLDatabase.MakeInParam("@AccountTitle", SqlDbType.Int, 4, m.AccountTitle),
				SQLDatabase.MakeInParam("@ProductBrand", SqlDbType.Int, 4, m.ProductBrand),
				SQLDatabase.MakeInParam("@ApplyCost", SqlDbType.Decimal, 9, m.ApplyCost),
				SQLDatabase.MakeInParam("@WriteOffCost", SqlDbType.Decimal, 9, m.WriteOffCost),
				SQLDatabase.MakeInParam("@BalanceMode", SqlDbType.Int, 4, m.BalanceMode),
				SQLDatabase.MakeInParam("@AdjustMode", SqlDbType.Int, 4, m.AdjustMode),
				SQLDatabase.MakeInParam("@AdjustCost", SqlDbType.Decimal, 9, m.AdjustCost),
				SQLDatabase.MakeInParam("@AdjustReason", SqlDbType.VarChar, 500, m.AdjustReason),
				SQLDatabase.MakeInParam("@BeginMonth", SqlDbType.Int, 4, m.BeginMonth),
				SQLDatabase.MakeInParam("@EndMonth", SqlDbType.Int, 4, m.EndMonth),
				SQLDatabase.MakeInParam("@BeginDate", SqlDbType.DateTime, 8, m.BeginDate),
				SQLDatabase.MakeInParam("@EndDate", SqlDbType.DateTime, 8, m.EndDate),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 500, m.Remark),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            m.ID = SQLDatabase.RunProc(_ProcePrefix + "_AddDetail", prams);

            return m.ID;
        }

        public override int UpdateDetail(FNA_FeeWriteOffDetail m)
        {
            m.WriteOffID = HeadID;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@WriteOffID", SqlDbType.Int, 4, m.WriteOffID),
				SQLDatabase.MakeInParam("@ApplyDetailID", SqlDbType.Int, 4, m.ApplyDetailID),
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, m.Client),
				SQLDatabase.MakeInParam("@AccountTitle", SqlDbType.Int, 4, m.AccountTitle),
				SQLDatabase.MakeInParam("@ProductBrand", SqlDbType.Int, 4, m.ProductBrand),
				SQLDatabase.MakeInParam("@ApplyCost", SqlDbType.Decimal, 9, m.ApplyCost),
				SQLDatabase.MakeInParam("@WriteOffCost", SqlDbType.Decimal, 9, m.WriteOffCost),
				SQLDatabase.MakeInParam("@BalanceMode", SqlDbType.Int, 4, m.BalanceMode),
				SQLDatabase.MakeInParam("@AdjustMode", SqlDbType.Int, 4, m.AdjustMode),
				SQLDatabase.MakeInParam("@AdjustCost", SqlDbType.Decimal, 9, m.AdjustCost),
				SQLDatabase.MakeInParam("@AdjustReason", SqlDbType.VarChar, 500, m.AdjustReason),
				SQLDatabase.MakeInParam("@BeginMonth", SqlDbType.Int, 4, m.BeginMonth),
				SQLDatabase.MakeInParam("@EndMonth", SqlDbType.Int, 4, m.EndMonth),
				SQLDatabase.MakeInParam("@BeginDate", SqlDbType.DateTime, 8, m.BeginDate),
				SQLDatabase.MakeInParam("@EndDate", SqlDbType.DateTime, 8, m.EndDate),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 500, m.Remark),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_UpdateDetail", prams);

            return ret;
        }

        protected override FNA_FeeWriteOffDetail FillDetailModel(IDataReader dr)
        {
            FNA_FeeWriteOffDetail m = new FNA_FeeWriteOffDetail();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["WriteOffID"].ToString())) m.WriteOffID = (int)dr["WriteOffID"];
            if (!string.IsNullOrEmpty(dr["ApplyDetailID"].ToString())) m.ApplyDetailID = (int)dr["ApplyDetailID"];
            if (!string.IsNullOrEmpty(dr["Client"].ToString())) m.Client = (int)dr["Client"];
            if (!string.IsNullOrEmpty(dr["AccountTitle"].ToString())) m.AccountTitle = (int)dr["AccountTitle"];
            if (!string.IsNullOrEmpty(dr["ProductBrand"].ToString())) m.ProductBrand = (int)dr["ProductBrand"];
            if (!string.IsNullOrEmpty(dr["ApplyCost"].ToString())) m.ApplyCost = (decimal)dr["ApplyCost"];
            if (!string.IsNullOrEmpty(dr["WriteOffCost"].ToString())) m.WriteOffCost = (decimal)dr["WriteOffCost"];
            if (!string.IsNullOrEmpty(dr["BalanceMode"].ToString())) m.BalanceMode = (int)dr["BalanceMode"];
            if (!string.IsNullOrEmpty(dr["AdjustMode"].ToString())) m.AdjustMode = (int)dr["AdjustMode"];
            if (!string.IsNullOrEmpty(dr["AdjustCost"].ToString())) m.AdjustCost = (decimal)dr["AdjustCost"];
            if (!string.IsNullOrEmpty(dr["AdjustReason"].ToString())) m.AdjustReason = (string)dr["AdjustReason"];
            if (!string.IsNullOrEmpty(dr["BeginMonth"].ToString())) m.BeginMonth = (int)dr["BeginMonth"];
            if (!string.IsNullOrEmpty(dr["EndMonth"].ToString())) m.EndMonth = (int)dr["EndMonth"];
            if (!string.IsNullOrEmpty(dr["BeginDate"].ToString())) m.BeginDate = (DateTime)dr["BeginDate"];
            if (!string.IsNullOrEmpty(dr["EndDate"].ToString())) m.EndDate = (DateTime)dr["EndDate"];
            if (!string.IsNullOrEmpty(dr["Remark"].ToString())) m.Remark = (string)dr["Remark"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);

            return m;
        }

        /// <summary>
        /// 生成费用报销单号 格式：FYHX+管理单元编号+'-'+申请日期+'-'+4位流水号
        /// </summary>
        /// <param name="organizecity"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 提交费用核消
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
                SQLDatabase.MakeInParam("@ApproveTask", SqlDbType.Int, 4,taskid)
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Submit", prams);

            return ret;
        }

        public void UpdateAdjustRecord(int ID, int Staff, int Client, int AccountTitle, string OldAdjustCost, string AdjustCost, string AdjustReason)
        {
            #region	设置参数集
            SqlParameter[] prams = {
                SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, ID),
				SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4, Staff),
                SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, Client),
                SQLDatabase.MakeInParam("@AccountTitle", SqlDbType.Int, 4, AccountTitle),
                SQLDatabase.MakeInParam("@OldAdjustCost", SqlDbType.VarChar, 20,OldAdjustCost),
                SQLDatabase.MakeInParam("@AdjustCost", SqlDbType.VarChar,20, AdjustCost),
                SQLDatabase.MakeInParam("@AdjustReason", SqlDbType.VarChar,500, AdjustReason)
			};
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_UpdateAdjustRecord", prams);
        }

        /// <summary>
        /// 获取费用核消单的总金额（含调整）
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
        public DataTable GetSummaryTotal(int AccountMonth, int OrganizeCity, int Level, int FeeType, int State, int Staff, string ExtCondition)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, AccountMonth),
                SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, OrganizeCity),
                SQLDatabase.MakeInParam("@Level", SqlDbType.Int, 4, Level),
                SQLDatabase.MakeInParam("@FeeType", SqlDbType.Int, 4, FeeType),
                SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, State),
                SQLDatabase.MakeInParam("@Staff",SqlDbType.Int, 4, Staff),
                SQLDatabase.MakeInParam("@ExtCondition",SqlDbType.VarChar,ExtCondition.Length,ExtCondition)
			};
            #endregion

            SqlDataReader dr = null;
            SQLDatabase.RunProc(_ProcePrefix + "_GetSummaryTotal", prams, out dr);

            return Tools.ConvertDataReaderToDataTable(dr);
        }
        /// <summary>
        /// 验证核销增值税发票号及验收凭证号是否唯一,如果存在返回1,不存在返回-1
        /// </summary>
        /// <param name="NOType"></param>
        /// <param name="NOValue"></param>
        /// <returns></returns>
        public int VerifyNO(int ID, int NOType, string NOValue)
        {
            #region	设置参数集
            SqlParameter[] prams = {
                SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, ID),
				SQLDatabase.MakeInParam("@NOType", SqlDbType.Int, 4, NOType),
                SQLDatabase.MakeInParam("@NOValue", SqlDbType.VarChar, 500, NOValue)};
            #endregion

            return SQLDatabase.RunProc(_ProcePrefix + "_VerifyNO", prams);

        }

        /// <summary>
        /// 判断指定电话的电话费是否已经核销
        /// </summary>
        /// <param name="AccountTitle"></param>
        /// <param name="Month"></param>
        /// <param name="RelateTelephone"></param>
        /// <returns></returns>
        public bool CheckTeleFeeHasWriteOff(int AccountTitle, int Month, int RelateTelephone)
        {
            #region	设置参数集
            SqlParameter[] prams = {
                SQLDatabase.MakeInParam("@AccountTitle", SqlDbType.Int, 4, AccountTitle),
				SQLDatabase.MakeInParam("@Month", SqlDbType.Int, 4, Month),
                SQLDatabase.MakeInParam("@RelateTelephone", SqlDbType.Int, 4, RelateTelephone)
            };
            #endregion

            return SQLDatabase.RunProc(_ProcePrefix + "_CheckTeleFeeHasWriteOff", prams) == 1;
        }

        /// <summary>
        /// 判断指定员工的手机费是否已经核销
        /// </summary>
        /// <param name="AccountTitle"></param>
        /// <param name="Month"></param>
        /// <param name="MobileFeeRelateStaff"></param>
        /// <returns></returns>
        public bool CheckMobileFeeHasWriteOff(int AccountTitle, int Month, int MobileFeeRelateStaff)
        {
            #region	设置参数集
            SqlParameter[] prams = {
                SQLDatabase.MakeInParam("@AccountTitle", SqlDbType.Int, 4, AccountTitle),
				SQLDatabase.MakeInParam("@Month", SqlDbType.Int, 4, Month),
                SQLDatabase.MakeInParam("@MobileFeeRelateStaff", SqlDbType.Int, 4, MobileFeeRelateStaff)
            };
            #endregion

            return SQLDatabase.RunProc(_ProcePrefix + "_CheckMobileFeeHasWriteOff", prams) == 1;
        }
        /// <summary>
        /// 根据核销单号获取进货
        /// </summary>
        /// <param name="WriteOffID"></param>
        /// <returns></returns>
        public DataTable GetPurchaseVolume(int WriteOffID)
        {
            #region	设置参数集
            SqlParameter[] prams = {
                SQLDatabase.MakeInParam("@WriteOffID", SqlDbType.Int, 4, WriteOffID)
            };
            #endregion
            SqlDataReader dr= null;
            SQLDatabase.RunProc(_ProcePrefix + "_GetPurchaseVolume", prams,out dr);
            return Tools.ConvertDataReaderToDataTable(dr);
        }
    }
}

