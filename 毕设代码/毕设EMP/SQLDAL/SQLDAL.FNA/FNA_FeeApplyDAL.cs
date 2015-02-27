
// ===================================================================
// 文件： FNA_FeeApplyDAL.cs
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
using System.Collections.Generic;

namespace MCSFramework.SQLDAL.FNA
{
    /// <summary>
    ///FNA_FeeApply数据访问DAL类
    /// </summary>
    public class FNA_FeeApplyDAL : BaseComplexDAL<FNA_FeeApply, FNA_FeeApplyDetail>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public FNA_FeeApplyDAL()
        {
            _ProcePrefix = "MCS_FNA.dbo.sp_FNA_FeeApply";
        }
        #endregion


        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(FNA_FeeApply m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@SheetCode", SqlDbType.VarChar, 100, m.SheetCode),
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, m.OrganizeCity),
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, m.AccountMonth),
				SQLDatabase.MakeInParam("@FeeType", SqlDbType.Int, 4, m.FeeType),
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, m.Client),
				SQLDatabase.MakeInParam("@ProductBrand", SqlDbType.Int, 4, m.ProductBrand),
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
        public override int Update(FNA_FeeApply m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@SheetCode", SqlDbType.VarChar, 100, m.SheetCode),
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, m.OrganizeCity),
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, m.AccountMonth),
				SQLDatabase.MakeInParam("@FeeType", SqlDbType.Int, 4, m.FeeType),
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, m.Client),
				SQLDatabase.MakeInParam("@ProductBrand", SqlDbType.Int, 4, m.ProductBrand),
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

        protected override FNA_FeeApply FillModel(IDataReader dr)
        {
            FNA_FeeApply m = new FNA_FeeApply();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["SheetCode"].ToString())) m.SheetCode = (string)dr["SheetCode"];
            if (!string.IsNullOrEmpty(dr["OrganizeCity"].ToString())) m.OrganizeCity = (int)dr["OrganizeCity"];
            if (!string.IsNullOrEmpty(dr["AccountMonth"].ToString())) m.AccountMonth = (int)dr["AccountMonth"];
            if (!string.IsNullOrEmpty(dr["FeeType"].ToString())) m.FeeType = (int)dr["FeeType"];
            if (!string.IsNullOrEmpty(dr["Client"].ToString())) m.Client = (int)dr["Client"];
            if (!string.IsNullOrEmpty(dr["ProductBrand"].ToString())) m.ProductBrand = (int)dr["ProductBrand"];
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

        public override int AddDetail(FNA_FeeApplyDetail m)
        {
            m.ApplyID = HeadID;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ApplyID", SqlDbType.Int, 4, m.ApplyID),
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, m.Client),
				SQLDatabase.MakeInParam("@AccountTitle", SqlDbType.Int, 4, m.AccountTitle),
				SQLDatabase.MakeInParam("@ApplyCost", SqlDbType.Decimal, 9, m.ApplyCost),
				SQLDatabase.MakeInParam("@AdjustCost", SqlDbType.Decimal, 9, m.AdjustCost),
				SQLDatabase.MakeInParam("@AdjustReason", SqlDbType.VarChar, 500, m.AdjustReason),
				SQLDatabase.MakeInParam("@Flag", SqlDbType.Int, 4, m.Flag),
				SQLDatabase.MakeInParam("@BeginMonth", SqlDbType.Int, 4, m.BeginMonth),
				SQLDatabase.MakeInParam("@EndMonth", SqlDbType.Int, 4, m.EndMonth),
				SQLDatabase.MakeInParam("@BeginDate", SqlDbType.DateTime, 8, m.BeginDate),
				SQLDatabase.MakeInParam("@EndDate", SqlDbType.DateTime, 8, m.EndDate),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 500, m.Remark),
				SQLDatabase.MakeInParam("@AvailCost", SqlDbType.Decimal, 9, m.AvailCost),
                SQLDatabase.MakeInParam("@CancelCost", SqlDbType.Decimal, 9, m.CancelCost),
                SQLDatabase.MakeInParam("@RelateContractDetail", SqlDbType.Int, 4, m.RelateContractDetail),
				SQLDatabase.MakeInParam("@DICost", SqlDbType.Decimal, 9, m.DICost),
                SQLDatabase.MakeInParam("@DIAdjustCost", SqlDbType.Decimal, 9, m.DIAdjustCost),
                SQLDatabase.MakeInParam("@SalesForcast", SqlDbType.Decimal, 9, m.SalesForcast),
                SQLDatabase.MakeInParam("@LastWriteOffMonth", SqlDbType.Decimal, 9, m.LastWriteOffMonth),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            m.ID = SQLDatabase.RunProc(_ProcePrefix + "_AddDetail", prams);
            AddBrandRate(m.ID, m.RelateBrands);
            return m.ID;
        }

        public override int UpdateDetail(FNA_FeeApplyDetail m)
        {
            m.ApplyID = HeadID;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@ApplyID", SqlDbType.Int, 4, m.ApplyID),
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, m.Client),
				SQLDatabase.MakeInParam("@AccountTitle", SqlDbType.Int, 4, m.AccountTitle),
				SQLDatabase.MakeInParam("@ApplyCost", SqlDbType.Decimal, 9, m.ApplyCost),
				SQLDatabase.MakeInParam("@AdjustCost", SqlDbType.Decimal, 9, m.AdjustCost),
				SQLDatabase.MakeInParam("@AdjustReason", SqlDbType.VarChar, 500, m.AdjustReason),
				SQLDatabase.MakeInParam("@Flag", SqlDbType.Int, 4, m.Flag),
				SQLDatabase.MakeInParam("@BeginMonth", SqlDbType.Int, 4, m.BeginMonth),
				SQLDatabase.MakeInParam("@EndMonth", SqlDbType.Int, 4, m.EndMonth),
				SQLDatabase.MakeInParam("@BeginDate", SqlDbType.DateTime, 8, m.BeginDate),
				SQLDatabase.MakeInParam("@EndDate", SqlDbType.DateTime, 8, m.EndDate),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 500, m.Remark),
				SQLDatabase.MakeInParam("@AvailCost", SqlDbType.Decimal, 9, m.AvailCost),
                SQLDatabase.MakeInParam("@CancelCost", SqlDbType.Decimal, 9, m.CancelCost),
                SQLDatabase.MakeInParam("@RelateContractDetail", SqlDbType.Int, 4, m.RelateContractDetail),
				SQLDatabase.MakeInParam("@DICost", SqlDbType.Decimal, 9, m.DICost),
                SQLDatabase.MakeInParam("@DIAdjustCost", SqlDbType.Decimal, 9, m.DIAdjustCost),
                SQLDatabase.MakeInParam("@SalesForcast", SqlDbType.Decimal, 9, m.SalesForcast),
                SQLDatabase.MakeInParam("@LastWriteOffMonth", SqlDbType.Decimal, 9, m.LastWriteOffMonth),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_UpdateDetail", prams);

            return ret;
        }

        protected override FNA_FeeApplyDetail FillDetailModel(IDataReader dr)
        {
            FNA_FeeApplyDetail m = new FNA_FeeApplyDetail();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["ApplyID"].ToString())) m.ApplyID = (int)dr["ApplyID"];
            if (!string.IsNullOrEmpty(dr["Client"].ToString())) m.Client = (int)dr["Client"];
            if (!string.IsNullOrEmpty(dr["AccountTitle"].ToString())) m.AccountTitle = (int)dr["AccountTitle"];
            if (!string.IsNullOrEmpty(dr["ApplyCost"].ToString())) m.ApplyCost = (decimal)dr["ApplyCost"];
            if (!string.IsNullOrEmpty(dr["AdjustCost"].ToString())) m.AdjustCost = (decimal)dr["AdjustCost"];
            if (!string.IsNullOrEmpty(dr["AdjustReason"].ToString())) m.AdjustReason = (string)dr["AdjustReason"];
            if (!string.IsNullOrEmpty(dr["Flag"].ToString())) m.Flag = (int)dr["Flag"];
            if (!string.IsNullOrEmpty(dr["BeginMonth"].ToString())) m.BeginMonth = (int)dr["BeginMonth"];
            if (!string.IsNullOrEmpty(dr["EndMonth"].ToString())) m.EndMonth = (int)dr["EndMonth"];
            if (!string.IsNullOrEmpty(dr["BeginDate"].ToString())) m.BeginDate = (DateTime)dr["BeginDate"];
            if (!string.IsNullOrEmpty(dr["EndDate"].ToString())) m.EndDate = (DateTime)dr["EndDate"];
            if (!string.IsNullOrEmpty(dr["Remark"].ToString())) m.Remark = (string)dr["Remark"];
            if (!string.IsNullOrEmpty(dr["AvailCost"].ToString())) m.AvailCost = (decimal)dr["AvailCost"];
            if (!string.IsNullOrEmpty(dr["CancelCost"].ToString())) m.CancelCost = (decimal)dr["CancelCost"];
            if (!string.IsNullOrEmpty(dr["RelateContractDetail"].ToString())) m.RelateContractDetail = (int)dr["RelateContractDetail"];
            if (!string.IsNullOrEmpty(dr["DICost"].ToString())) m.DICost = (decimal)dr["DICost"];
            if (!string.IsNullOrEmpty(dr["DIAdjustCost"].ToString())) m.DIAdjustCost = (decimal)dr["DIAdjustCost"];
            if (!string.IsNullOrEmpty(dr["SalesForcast"].ToString())) m.SalesForcast = (decimal)dr["SalesForcast"];
            if (!string.IsNullOrEmpty(dr["LastWriteOffMonth"].ToString())) m.LastWriteOffMonth = (int)dr["LastWriteOffMonth"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);

            foreach (FNA_FeeApplyDetail_BrandRate item in GetBrandRates(m.ID))
            {
                m.RelateBrands += item.Brand.ToString() + ",";
            }
            if (m.RelateBrands.EndsWith(",")) m.RelateBrands = m.RelateBrands.Substring(0, m.RelateBrands.Length - 1);

            return m;
        }

        #region 费用明细所细分的品牌
        public int AddBrandRate(int DetailID, string RelateBrands)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@DetailID", SqlDbType.Int, 4, DetailID),
				SQLDatabase.MakeInParam("@RelateBrands", SqlDbType.VarChar, 1000, RelateBrands)
			};
            #endregion

            return SQLDatabase.RunProc(_ProcePrefix + "Detail_BrandRate_AddBrands", prams);
        }
        public int AddBrandRate(FNA_FeeApplyDetail_BrandRate m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@DetailID", SqlDbType.Int, 4, m.DetailID),
				SQLDatabase.MakeInParam("@Brand", SqlDbType.Int, 4, m.Brand),
				SQLDatabase.MakeInParam("@Rate", SqlDbType.Decimal, 9, m.Rate),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            m.ID = SQLDatabase.RunProc(_ProcePrefix + "Detail_BrandRate_Add", prams);

            return m.ID;
        }
        public IList<FNA_FeeApplyDetail_BrandRate> GetBrandRates(int DetailID)
        {
            SqlDataReader dr = null;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@DetailID", SqlDbType.Int, 4, DetailID)
			};
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "Detail_BrandRate_GetByDetailID", prams, out dr);

            IList<FNA_FeeApplyDetail_BrandRate> list = new List<FNA_FeeApplyDetail_BrandRate>();
            while (dr.Read())
            {
                list.Add(FillBrandRateModel(dr));
            }
            dr.Close();

            return list;
        }
        protected FNA_FeeApplyDetail_BrandRate FillBrandRateModel(IDataReader dr)
        {
            FNA_FeeApplyDetail_BrandRate m = new FNA_FeeApplyDetail_BrandRate();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["DetailID"].ToString())) m.DetailID = (int)dr["DetailID"];
            if (!string.IsNullOrEmpty(dr["Brand"].ToString())) m.Brand = (int)dr["Brand"];
            if (!string.IsNullOrEmpty(dr["Rate"].ToString())) m.Rate = (decimal)dr["Rate"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);

            return m;
        }
        #endregion
        /// <summary>
        /// 提交费用申请
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

        /// <summary>
        /// 生成费用申请单号 格式：FYSQ+管理单元编号+'-'+申请日期+'-'+4位流水号
        /// </summary>
        /// <param name="organizecity"></param>
        /// <returns></returns>
        public string GenerateSheetCode(int organizecity, int month)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, organizecity),
                SQLDatabase.MakeInParam("@Month", SqlDbType.Int, 4, month),
				SQLDatabase.MakeOutParam("@SheetCode", SqlDbType.VarChar, 100)
			};
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_GenerateSheetCode", prams);

            return (string)prams[2].Value;
        }

        /// <summary>
        /// 根据费用申请明细的获取费用申请单号
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
        /// 获取费用申请单的总金额（含调整）
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
        /// 获取费用申请单的可核销金额
        /// </summary>
        /// <param name="detailid"></param>
        /// <returns></returns>
        public decimal GetAvailCost(int ID)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, ID),
                new SqlParameter("@AvailCost", SqlDbType.Decimal,9, ParameterDirection.Output,false,18,3,"AvailCost", DataRowVersion.Current,0)
			};
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_GetAvailCost", prams);

            return (decimal)prams[1].Value;
        }

        /// <summary>
        /// 获取费用申请单的总金额（含调整）
        /// </summary>
        /// <param name="detailid"></param>
        /// <returns></returns>
        public decimal GetApplyTotalCost(int AccountMonth, int OrganizeCity, int FeeType, bool IncludeChildOrganizeCity)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, AccountMonth),
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, OrganizeCity),
                SQLDatabase.MakeInParam("@FeeType", SqlDbType.Int, 4, FeeType),
                SQLDatabase.MakeInParam("@IncludeChildCity", SqlDbType.Int, 4, IncludeChildOrganizeCity ? 1 : 0),
                new SqlParameter("@TotalCost", SqlDbType.Decimal,18, ParameterDirection.Output,false,18,3,"TotalCost", DataRowVersion.Current,0)
            };
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_GetApplyTotalCost", prams);

            return (decimal)prams[4].Value;
        }

        public DataTable GetFeeApplyOrWriteoffByClient(int Client, int BeginMonth, int EndMonth)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, Client),
                SQLDatabase.MakeInParam("@BeginMonth", SqlDbType.Int, 4, BeginMonth),
                SQLDatabase.MakeInParam("@EndMonth", SqlDbType.Int, 4, EndMonth)
			};
            #endregion

            SqlDataReader dr = null;
            SQLDatabase.RunProc(_ProcePrefix + "_GetFeeApplyOrWriteoffByClient", prams, out dr);

            return Tools.ConvertDataReaderToDataTable(dr);
        }

        public void UpdateAdjustRecord(int ID, int Staff, int AccountTitle, string OldAdjustCost, string AdjustCost, string AdjustReason)
        {
            #region	设置参数集
            SqlParameter[] prams = {
                SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, ID),
				SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4, Staff),
                SQLDatabase.MakeInParam("@AccountTitle", SqlDbType.Int, 4, AccountTitle),
                SQLDatabase.MakeInParam("@OldAdjustCost", SqlDbType.VarChar, 20,OldAdjustCost),
                SQLDatabase.MakeInParam("@AdjustCost", SqlDbType.VarChar,20, AdjustCost),
                SQLDatabase.MakeInParam("@AdjustReason",SqlDbType.VarChar,500,AdjustReason)
			};
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_UpdateAdjustRecord", prams);
        }

        public bool CheckFNAByAccontTitle(int ClientID, int AccountMonth, int AccountTitle, decimal ApplyCost)
        {
            #region	设置参数集
            SqlParameter[] prams = {
                SQLDatabase.MakeInParam("@ClientID", SqlDbType.Int, 4, ClientID),
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, AccountMonth),
                SQLDatabase.MakeInParam("@AccountTitle", SqlDbType.Int, 4, AccountTitle),
                SQLDatabase.MakeInParam("@ApplyCost", SqlDbType.Decimal, 18, ApplyCost),
                SQLDatabase.MakeOutParam("@Flag", SqlDbType.VarChar, 1)
			};
            #endregion
            SQLDatabase.RunProc(_ProcePrefix + "_CheckFNAByAccontTitle", prams);

            if ((string)prams[4].Value == "1")
                return true;
            else
                return false;
        }

        /// <summary>
        /// 复制费用申请单，新单为未提交状态
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Staff"></param>
        /// <returns></returns>
        public int Copy(int ID, int Staff, int Month)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, ID),
                SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4, Staff),
                SQLDatabase.MakeInParam("@Month", SqlDbType.Int, 4, Month),
			};
            #endregion

            return SQLDatabase.RunProc(_ProcePrefix + "_Copy", prams);
        }

        /// <summary>
        /// 取消已审批通过待核销部分的费用申请
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Staff"></param>
        /// <returns></returns>
        public int Cancel(int ID, int Staff)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, ID),
                SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4, Staff)
			};
            #endregion

            return SQLDatabase.RunProc(_ProcePrefix + "_Cancel", prams);
        }

        /// <summary>
        /// 汇总显示指定区域(包括子区域)所有费用申请单汇总信息
        /// </summary>
        /// <param name="AccountMonth"></param>
        /// <param name="OrganizeCity"></param>
        /// <param name="FeeType"></param>
        /// <param name="State">0:所有已提交及已批复 1：已提交待我审批 2：已批复</param>
        /// <param name="TaskIDs"></param>
        /// <returns></returns>
        public DataTable GetSummaryTotal(int AccountMonth, int OrganizeCity, int Level, int FeeType, int State, int Flag, int Staff)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, AccountMonth),
                SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, OrganizeCity),
                SQLDatabase.MakeInParam("@Level", SqlDbType.Int, 4, Level),
                SQLDatabase.MakeInParam("@FeeType", SqlDbType.Int, 4, FeeType),
                SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, State),
                SQLDatabase.MakeInParam("@Flag", SqlDbType.Int, 4, Flag),
                SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4, Staff)
			};
            #endregion

            SqlDataReader dr = null;
            SQLDatabase.RunProc(_ProcePrefix + "_GetSummaryTotal", prams, out dr, 600);

            return Tools.ConvertDataReaderToDataTable(dr);
        }
        public DataTable GetSummaryTotal_Sub(int AccountMonth, int OrganizeCity, int Level, int FeeType, int State, int Flag, int Staff)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, AccountMonth),
                SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, OrganizeCity),
                SQLDatabase.MakeInParam("@Level", SqlDbType.Int, 4, Level),
                SQLDatabase.MakeInParam("@FeeType", SqlDbType.Int, 4, FeeType),
                SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, State),
                SQLDatabase.MakeInParam("@Flag", SqlDbType.Int, 4, Flag),
                SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4, Staff)
			};
            #endregion

            SqlDataReader dr = null;
            SQLDatabase.RunProc(_ProcePrefix + "_GetSummaryTotal_Sub", prams, out dr, 600);

            return Tools.ConvertDataReaderToDataTable(dr);
        }


        /// <summary>
        /// 按经销商查看下游零售商费用申请汇总信息
        /// </summary>
        /// <param name="Client">经销商ID</param>
        /// <param name="BeginMonth"></param>
        /// <param name="EndMonth"></param>
        /// <param name="FeeType"></param>
        /// <returns></returns>
        public DataTable GetSummaryTotalByDistributor(int Client, int BeginMonth, int EndMonth, int FeeType)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, Client),
                SQLDatabase.MakeInParam("@BeginMonth", SqlDbType.Int, 4, BeginMonth),
                SQLDatabase.MakeInParam("@EndMonth", SqlDbType.Int, 4, EndMonth),
                SQLDatabase.MakeInParam("@FeeType", SqlDbType.Int, 4, FeeType)
			};
            #endregion

            SqlDataReader dr = null;
            SQLDatabase.RunProc(_ProcePrefix + "_GetSummaryTotalByDistributor", prams, out dr, 600);

            return Tools.ConvertDataReaderToDataTable(dr);
        }
        /// <summary>
        /// 获取指定客户指定科目前一次费用申请信息
        /// </summary>
        /// <param name="OrganizeCity"></param>
        /// <param name="Client"></param>
        /// <param name="AccountTitle"></param>
        /// <returns></returns>
        public FNA_FeeApplyDetail GetPreApplyInfo(int DetailID)
        {
            #region	设置参数集
            SqlParameter[] prams = {
                SQLDatabase.MakeInParam("@DetailID", SqlDbType.Int, 4, DetailID),
			};
            #endregion

            SqlDataReader dr = null;
            SQLDatabase.RunProc(_ProcePrefix + "_GetPreApplyInfo", prams, out dr);

            FNA_FeeApplyDetail d = null;
            if (dr.HasRows && dr.Read())
            {
                d = FillDetailModel(dr);
            }
            dr.Close();

            return d;
        }

        /// <summary>
        /// 获取指定管理片区指定月的终结预算额度
        /// </summary>
        /// <param name="AccountMonth">会计月</param>
        /// <param name="OrganizeCity">管理片区</param>
        /// <returns>终结费用</returns>
        public decimal GetCancelCost(int AccountMonth, int OrganizeCity, int FeeType, bool IncludeChildOrganizeCity)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, AccountMonth),
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, OrganizeCity),
                SQLDatabase.MakeInParam("@FeeType", SqlDbType.Int, 4, FeeType),
                SQLDatabase.MakeInParam("@IncludeChildCity", SqlDbType.Int, 4, IncludeChildOrganizeCity ? 1 : 0),
                new SqlParameter("@CancelCost", SqlDbType.Decimal,18, ParameterDirection.Output,false,18,3,"CancelCost", DataRowVersion.Current,0)
            };
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_GetCancelCost", prams);

            return (decimal)prams[4].Value;
        }

        /// <summary>
        /// 获取陈列费用汇总表
        /// </summary>
        /// <param name="AccountMonth"></param>
        /// <param name="OrganizeCity"></param>
        /// <param name="Level"></param>
        /// <param name="State"></param>
        /// <param name="Staff"></param>
        /// <returns></returns>
        public DataTable GetDiaplayFeeSummary(int AccountMonth, int OrganizeCity, int Level, int State, int Staff)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, AccountMonth),
                SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, OrganizeCity),  
                SQLDatabase.MakeInParam("@Level", SqlDbType.Int, 4, Level),      
                SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, State),
                SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4, Staff)
			};
            #endregion

            SqlDataReader dr = null;
            SQLDatabase.RunProc(_ProcePrefix + "_GetDiaplayFeeSummary", prams, out dr, 600);

            return Tools.ConvertDataReaderToDataTable(dr);
        }

        /// <summary>
        /// 获取陈列费用明细表
        /// </summary>
        /// <param name="AccountMonth"></param>
        /// <param name="OrganizeCity"></param>
        /// <param name="State"></param>
        /// <param name="Staff"></param>
        /// <param name="RTChannel"></param>
        /// <param name="ApplyCostCondition"></param>
        /// <param name="RTType"></param>
        /// <param name="ATSuppierIDs"></param>
        /// <returns></returns>
        public DataTable GetRTChannelDiaplayFee(int AccountMonth, int OrganizeCity, int State, int Staff, int RTChannel, string ApplyCostCondition, int RTType, string ATSuppierIDs)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, AccountMonth),
                SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, OrganizeCity),      
                SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, State),
                SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4, Staff),
                SQLDatabase.MakeInParam("@RTChannel", SqlDbType.Int, 4, RTChannel),
                SQLDatabase.MakeInParam("@RTType", SqlDbType.Int, 4, RTType),
                SQLDatabase.MakeInParam("@ATSuppierIDs", SqlDbType.VarChar, 50, ATSuppierIDs),
                SQLDatabase.MakeInParam("@ApplyCostCondition", SqlDbType.VarChar, ApplyCostCondition.Length, ApplyCostCondition)
			};
            #endregion

            SqlDataReader dr = null;
            SQLDatabase.RunProc(_ProcePrefix + "_GetRTChannelDiaplayFee", prams, out dr, 600);

            return Tools.ConvertDataReaderToDataTable(dr);
        }

        /// <summary>
        /// 获取返利费用汇总表
        /// </summary>
        /// <param name="AccountMonth"></param>
        /// <param name="OrganizeCity"></param>
        /// <param name="Level"></param>
        /// <param name="State"></param>
        /// <param name="Staff"></param>
        /// <returns></returns>
        public DataTable GetFLFeeSummary(int AccountMonth, int OrganizeCity, int Level, int State, int Staff)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, AccountMonth),
                SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, OrganizeCity),  
                SQLDatabase.MakeInParam("@Level", SqlDbType.Int, 4, Level),      
                SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, State),
                SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4, Staff)
			};
            #endregion

            SqlDataReader dr = null;
            SQLDatabase.RunProc(_ProcePrefix + "_GetFLFeeSummary", prams, out dr, 600);

            return Tools.ConvertDataReaderToDataTable(dr);
        }
        public DataTable GetFLFeeSummary2(int AccountMonth, int OrganizeCity, int Level, int State, int Staff)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, AccountMonth),
                SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, OrganizeCity),  
                SQLDatabase.MakeInParam("@Level", SqlDbType.Int, 4, Level),      
                SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, State),
                SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4, Staff)
			};
            #endregion

            SqlDataReader dr = null;
            SQLDatabase.RunProc(_ProcePrefix + "_GetFLFeeSummary2", prams, out dr, 600);

            return Tools.ConvertDataReaderToDataTable(dr);
        }
        /// <summary>
        /// 获取返利明细表
        /// </summary>
        /// <param name="AccountMonth"></param>
        /// <param name="OrganizeCity"></param>
        /// <param name="State"></param>
        /// <param name="Staff"></param>
        /// <param name="RTChannel"></param>
        /// <param name="ApplyCostCondition"></param>
        /// <param name="RTType"></param>
        /// <param name="ATSuppierIDs"></param>
        /// <returns></returns>
        public DataTable GetRTChannelFLFee(int AccountMonth, int OrganizeCity, int State, int Staff, int RTChannel, string ApplyCostCondition, int RTType)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, AccountMonth),
                SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, OrganizeCity),      
                SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, State),
                SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4, Staff),
                SQLDatabase.MakeInParam("@RTChannel", SqlDbType.Int, 4, RTChannel),
                SQLDatabase.MakeInParam("@RTType", SqlDbType.Int, 4, RTType),
                SQLDatabase.MakeInParam("@ApplyCostCondition", SqlDbType.VarChar, ApplyCostCondition.Length, ApplyCostCondition)
			};
            #endregion

            SqlDataReader dr = null;
            SQLDatabase.RunProc(_ProcePrefix + "_GetRTChannelFLFee", prams, out dr, 600);

            return Tools.ConvertDataReaderToDataTable(dr);
        }

        public DataTable GetDiaplayFeeByDisplay(int AccountMonth, int OrganizeCity, int Level, int State, int Staff)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, AccountMonth),
                SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, OrganizeCity),  
                SQLDatabase.MakeInParam("@Level", SqlDbType.Int, 4, Level),      
                SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, State),
                SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4, Staff)
			};
            #endregion

            SqlDataReader dr = null;
            SQLDatabase.RunProc(_ProcePrefix + "_GetDiaplayFeeByDisplay", prams, out dr, 600);

            return Tools.ConvertDataReaderToDataTable(dr);
        }

        public DataTable GetByPayMode(int AccountMonth, int OrganizeCity, int Level, int State, int Staff)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, AccountMonth),
                SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, OrganizeCity),  
                SQLDatabase.MakeInParam("@Level", SqlDbType.Int, 4, Level),      
                SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, State),
                SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4, Staff)
			};
            #endregion

            SqlDataReader dr = null;
            SQLDatabase.RunProc(_ProcePrefix + "_GetByPayMode", prams, out dr, 600);

            return Tools.ConvertDataReaderToDataTable(dr);
        }

    }
}

