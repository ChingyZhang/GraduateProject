
// ===================================================================
// 文件： ORD_GiftApplyAmountDAL.cs
// 项目名称：
// 创建时间：2011/12/12
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.Logistics;


namespace MCSFramework.SQLDAL.Logistics
{
    /// <summary>
    ///ORD_GiftApplyAmount数据访问DAL类
    /// </summary>
    public class ORD_GiftApplyAmountDAL : BaseSimpleDAL<ORD_GiftApplyAmount>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public ORD_GiftApplyAmountDAL()
        {
            _ProcePrefix = "MCS_Logistics.dbo.sp_ORD_GiftApplyAmount";
        }
        #endregion


        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(ORD_GiftApplyAmount m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, m.AccountMonth),
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, m.Client),
				SQLDatabase.MakeInParam("@Brand", SqlDbType.Int, 4, m.Brand),
				SQLDatabase.MakeInParam("@Classify", SqlDbType.Int, 4, m.Classify),
				SQLDatabase.MakeInParam("@FeeRate", SqlDbType.Decimal, 9, m.FeeRate),
				SQLDatabase.MakeInParam("@SalesVolume", SqlDbType.Decimal, 9, m.SalesVolume),
				SQLDatabase.MakeInParam("@AvailableAmount", SqlDbType.Decimal, 9, m.AvailableAmount),
				SQLDatabase.MakeInParam("@AppliedAmount", SqlDbType.Decimal, 9, m.AppliedAmount),
				SQLDatabase.MakeInParam("@BalanceAmount", SqlDbType.Decimal, 9, m.BalanceAmount),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 500, m.Remark),
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
        public override int Update(ORD_GiftApplyAmount m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, m.AccountMonth),
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, m.Client),
				SQLDatabase.MakeInParam("@Brand", SqlDbType.Int, 4, m.Brand),
				SQLDatabase.MakeInParam("@Classify", SqlDbType.Int, 4, m.Classify),
				SQLDatabase.MakeInParam("@FeeRate", SqlDbType.Decimal, 9, m.FeeRate),
				SQLDatabase.MakeInParam("@SalesVolume", SqlDbType.Decimal, 9, m.SalesVolume),
				SQLDatabase.MakeInParam("@AvailableAmount", SqlDbType.Decimal, 9, m.AvailableAmount),
				SQLDatabase.MakeInParam("@AppliedAmount", SqlDbType.Decimal, 9, m.AppliedAmount),
				SQLDatabase.MakeInParam("@BalanceAmount", SqlDbType.Decimal, 9, m.BalanceAmount),
                SQLDatabase.MakeInParam("@PreBalance", SqlDbType.Decimal, 9, m.PreBalance),
				SQLDatabase.MakeInParam("@DeductibleAmount", SqlDbType.Decimal, 9, m.DeductibleAmount),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 500, m.Remark),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@UpdateStaff", SqlDbType.Int, 4, m.UpdateStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName)) 
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override ORD_GiftApplyAmount FillModel(IDataReader dr)
        {
            ORD_GiftApplyAmount m = new ORD_GiftApplyAmount();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["AccountMonth"].ToString())) m.AccountMonth = (int)dr["AccountMonth"];
            if (!string.IsNullOrEmpty(dr["Client"].ToString())) m.Client = (int)dr["Client"];
            if (!string.IsNullOrEmpty(dr["Brand"].ToString())) m.Brand = (int)dr["Brand"];
            if (!string.IsNullOrEmpty(dr["Classify"].ToString())) m.Classify = (int)dr["Classify"];
            if (!string.IsNullOrEmpty(dr["FeeRate"].ToString())) m.FeeRate = (decimal)dr["FeeRate"];
            if (!string.IsNullOrEmpty(dr["SalesVolume"].ToString())) m.SalesVolume = (decimal)dr["SalesVolume"];
            if (!string.IsNullOrEmpty(dr["AvailableAmount"].ToString())) m.AvailableAmount = (decimal)dr["AvailableAmount"];
            if (!string.IsNullOrEmpty(dr["AppliedAmount"].ToString())) m.AppliedAmount = (decimal)dr["AppliedAmount"];
            if (!string.IsNullOrEmpty(dr["BalanceAmount"].ToString())) m.BalanceAmount = (decimal)dr["BalanceAmount"];
            if (!string.IsNullOrEmpty(dr["PreBalance"].ToString())) m.PreBalance = (decimal)dr["PreBalance"];
            if (!string.IsNullOrEmpty(dr["DeductibleAmount"].ToString())) m.DeductibleAmount = (decimal)dr["DeductibleAmount"];
            if (!string.IsNullOrEmpty(dr["Remark"].ToString())) m.Remark = (string)dr["Remark"];
            if (!string.IsNullOrEmpty(dr["ApproveFlag"].ToString())) m.ApproveFlag = (int)dr["ApproveFlag"];
            if (!string.IsNullOrEmpty(dr["InsertTime"].ToString())) m.InsertTime = (DateTime)dr["InsertTime"];
            if (!string.IsNullOrEmpty(dr["InsertStaff"].ToString())) m.InsertStaff = (int)dr["InsertStaff"];
            if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString())) m.UpdateTime = (DateTime)dr["UpdateTime"];
            if (!string.IsNullOrEmpty(dr["UpdateStaff"].ToString())) m.UpdateStaff = (int)dr["UpdateStaff"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);   

            return m;
        }

        /// <summary>
        /// 更新赠品可请购余额
        /// </summary>
        /// <param name="AccountMonth"></param>
        /// <param name="Client"></param>
        /// <param name="Brand"></param>
        /// <param name="Classify"></param>
        /// <param name="ChangeAmount"></param>
        /// <param name="Staff"></param>
        /// <returns></returns>
        public int ChangeBalanceAmount(int AccountMonth, int Client, int Brand, int Classify, decimal ChangeAmount, int Staff)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, AccountMonth),
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, Client),
				SQLDatabase.MakeInParam("@Brand", SqlDbType.Int, 4, Brand),
				SQLDatabase.MakeInParam("@Classify", SqlDbType.Int, 4, Classify),
				SQLDatabase.MakeInParam("@ChangeAmount", SqlDbType.Decimal, 9, ChangeAmount),
				SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4, Staff),
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_ChangeBalanceAmount", prams);

            return ret;
        }

        /// <summary>
        /// 获取指定客户赠品可请购余额
        /// </summary>
        /// <param name="AccountMonth"></param>
        /// <param name="Client"></param>
        /// <param name="Brand"></param>
        /// <param name="Classify"></param>
        /// <returns></returns>
        public decimal GetBalanceAmount(int AccountMonth, int Client, int Brand, int Classify)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, AccountMonth),
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, Client),
				SQLDatabase.MakeInParam("@Brand", SqlDbType.Int, 4, Brand),
				SQLDatabase.MakeInParam("@Classify", SqlDbType.Int, 4, Classify),
				new SqlParameter("@BalanceAmount", SqlDbType.Decimal, 9, ParameterDirection.Output,false,18,3, "BalanceAmount", DataRowVersion.Current,0)
			};
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_GetBalanceAmount", prams);

            return (decimal)prams[4].Value;
        }

        /// <summary>
        /// 计算各经销商下月可请购赠品金额
        /// </summary>
        /// <param name="AccountMonth"></param>
        /// <param name="Client"></param>
        /// <returns></returns>
        public int ComputAvailableAmount(int AccountMonth, int Client)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, AccountMonth),
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, Client)
			};
            #endregion

            return SQLDatabase.RunProc(_ProcePrefix + "_ComputAvailableAmount", prams, 600);
        }

        public SqlDataReader GetUsedInfo(int AccountMonth, int OrganizeCity)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, AccountMonth),
                SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, OrganizeCity) 
			};
            #endregion

            SqlDataReader dr = null;
            SQLDatabase.RunProc(_ProcePrefix + "_GetUsedInfo", prams, out dr, 600);
            return dr;
        }
        public SqlDataReader GetChangeHistory(int AccountMonth, int Client, int Brand, int Classify)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, AccountMonth),
                SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, Client) ,
                SQLDatabase.MakeInParam("@Brand", SqlDbType.Int, 4, Brand),
                SQLDatabase.MakeInParam("@Classify", SqlDbType.Int, 4, Classify) 
			};
            string sql = @"SELECT 
 AvailableAmount 本月生成请购额度,
 AppliedAmount 已请购赠品额度,
 BalanceAmount 还可请购赠品额度,
 Remark 备注,
 Org_Staff.RealName 调整人,
 ChangeTime 调整时间
 FROM MCS_Logistics.dbo.ORD_GiftApplyAmountChangeHistroy
 LEFT JOIN MCS_SYS.dbo.Org_Staff
 ON ORD_GiftApplyAmountChangeHistroy.ChangeStaff = Org_Staff.ID
WHERE AccountMonth=@AccountMonth AND Client=@Client
AND Brand=@Brand AND Classify=@Classify 
ORDER BY ChangeTime";
            #endregion
            SqlDataReader dr = null;
            SQLDatabase.RunSQL(sql, prams, out dr);
            
            return dr;
        }
    } 
}

