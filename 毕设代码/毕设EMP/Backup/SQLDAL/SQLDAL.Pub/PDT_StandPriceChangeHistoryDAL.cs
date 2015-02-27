
// ===================================================================
// 文件： PDT_StandPriceChangeHistoryDAL.cs
// 项目名称：
// 创建时间：2013-10-08
// 作者:	   Jace
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.Pub;


namespace MCSFramework.SQLDAL.Pub
{
    /// <summary>
    ///PDT_StandPriceChangeHistory数据访问DAL类
    /// </summary>
    public class PDT_StandPriceChangeHistoryDAL : BaseSimpleDAL<PDT_StandPriceChangeHistory>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public PDT_StandPriceChangeHistoryDAL()
        {
            _ProcePrefix = "MCS_Pub.dbo.sp_PDT_StandPriceChangeHistory";
        }
        #endregion


        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(PDT_StandPriceChangeHistory m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@StandardPrice", SqlDbType.Int, 4, m.StandardPrice),
				SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, m.Product),
				SQLDatabase.MakeInParam("@ChangeType", SqlDbType.Int, 4, m.ChangeType),
				SQLDatabase.MakeInParam("@PreFactoryPrice", SqlDbType.Decimal, 9, m.PreFactoryPrice),
				SQLDatabase.MakeInParam("@AftFactoryPrice", SqlDbType.Decimal, 9, m.AftFactoryPrice),
				SQLDatabase.MakeInParam("@PreTradeOutPrice", SqlDbType.Decimal, 9, m.PreTradeOutPrice),
				SQLDatabase.MakeInParam("@AftTradeOutPrice", SqlDbType.Decimal, 9, m.AftTradeOutPrice),
				SQLDatabase.MakeInParam("@PreTradeInPrice", SqlDbType.Decimal, 9, m.PreTradeInPrice),
				SQLDatabase.MakeInParam("@AftTradeInPrice", SqlDbType.Decimal, 9, m.AftTradeInPrice),
				SQLDatabase.MakeInParam("@PreStdPrice", SqlDbType.Decimal, 9, m.PreStdPrice),
				SQLDatabase.MakeInParam("@AftStdPrice", SqlDbType.Decimal, 9, m.AftStdPrice),
				SQLDatabase.MakeInParam("@PreRebatePrice", SqlDbType.Decimal, 9, m.PreRebatePrice),
				SQLDatabase.MakeInParam("@AftRebatePrice", SqlDbType.Decimal, 9, m.AftRebatePrice),
				SQLDatabase.MakeInParam("@PreDIRebatePrice", SqlDbType.Decimal, 9, m.PreDIRebatePrice),
				SQLDatabase.MakeInParam("@AftDIRebatePrice", SqlDbType.Decimal, 9, m.AftDIRebatePrice),
				SQLDatabase.MakeInParam("@ChageTime", SqlDbType.DateTime, 8, m.ChageTime),
				SQLDatabase.MakeInParam("@ChangeStaff", SqlDbType.Int, 4, m.ChangeStaff),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.Int, 4, m.Remark)
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
        public override int Update(PDT_StandPriceChangeHistory m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@StandardPrice", SqlDbType.Int, 4, m.StandardPrice),
				SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, m.Product),
				SQLDatabase.MakeInParam("@ChangeType", SqlDbType.Int, 4, m.ChangeType),
				SQLDatabase.MakeInParam("@PreFactoryPrice", SqlDbType.Decimal, 9, m.PreFactoryPrice),
				SQLDatabase.MakeInParam("@AftFactoryPrice", SqlDbType.Decimal, 9, m.AftFactoryPrice),
				SQLDatabase.MakeInParam("@PreTradeOutPrice", SqlDbType.Decimal, 9, m.PreTradeOutPrice),
				SQLDatabase.MakeInParam("@AftTradeOutPrice", SqlDbType.Decimal, 9, m.AftTradeOutPrice),
				SQLDatabase.MakeInParam("@PreTradeInPrice", SqlDbType.Decimal, 9, m.PreTradeInPrice),
				SQLDatabase.MakeInParam("@AftTradeInPrice", SqlDbType.Decimal, 9, m.AftTradeInPrice),
				SQLDatabase.MakeInParam("@PreStdPrice", SqlDbType.Decimal, 9, m.PreStdPrice),
				SQLDatabase.MakeInParam("@AftStdPrice", SqlDbType.Decimal, 9, m.AftStdPrice),
				SQLDatabase.MakeInParam("@PreRebatePrice", SqlDbType.Decimal, 9, m.PreRebatePrice),
				SQLDatabase.MakeInParam("@AftRebatePrice", SqlDbType.Decimal, 9, m.AftRebatePrice),
				SQLDatabase.MakeInParam("@PreDIRebatePrice", SqlDbType.Decimal, 9, m.PreDIRebatePrice),
				SQLDatabase.MakeInParam("@AftDIRebatePrice", SqlDbType.Decimal, 9, m.AftDIRebatePrice),
				SQLDatabase.MakeInParam("@ChageTime", SqlDbType.DateTime, 8, m.ChageTime),
				SQLDatabase.MakeInParam("@ChangeStaff", SqlDbType.Int, 4, m.ChangeStaff),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.Int, 4, m.Remark)
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override PDT_StandPriceChangeHistory FillModel(IDataReader dr)
        {
            PDT_StandPriceChangeHistory m = new PDT_StandPriceChangeHistory();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["StandardPrice"].ToString())) m.StandardPrice = (int)dr["StandardPrice"];
            if (!string.IsNullOrEmpty(dr["Product"].ToString())) m.Product = (int)dr["Product"];
            if (!string.IsNullOrEmpty(dr["ChangeType"].ToString())) m.ChangeType = (int)dr["ChangeType"];
            if (!string.IsNullOrEmpty(dr["PreFactoryPrice"].ToString())) m.PreFactoryPrice = (decimal)dr["PreFactoryPrice"];
            if (!string.IsNullOrEmpty(dr["AftFactoryPrice"].ToString())) m.AftFactoryPrice = (decimal)dr["AftFactoryPrice"];
            if (!string.IsNullOrEmpty(dr["PreTradeOutPrice"].ToString())) m.PreTradeOutPrice = (decimal)dr["PreTradeOutPrice"];
            if (!string.IsNullOrEmpty(dr["AftTradeOutPrice"].ToString())) m.AftTradeOutPrice = (decimal)dr["AftTradeOutPrice"];
            if (!string.IsNullOrEmpty(dr["PreTradeInPrice"].ToString())) m.PreTradeInPrice = (decimal)dr["PreTradeInPrice"];
            if (!string.IsNullOrEmpty(dr["AftTradeInPrice"].ToString())) m.AftTradeInPrice = (decimal)dr["AftTradeInPrice"];
            if (!string.IsNullOrEmpty(dr["PreStdPrice"].ToString())) m.PreStdPrice = (decimal)dr["PreStdPrice"];
            if (!string.IsNullOrEmpty(dr["AftStdPrice"].ToString())) m.AftStdPrice = (decimal)dr["AftStdPrice"];
            if (!string.IsNullOrEmpty(dr["PreRebatePrice"].ToString())) m.PreRebatePrice = (decimal)dr["PreRebatePrice"];
            if (!string.IsNullOrEmpty(dr["AftRebatePrice"].ToString())) m.AftRebatePrice = (decimal)dr["AftRebatePrice"];
            if (!string.IsNullOrEmpty(dr["PreDIRebatePrice"].ToString())) m.PreDIRebatePrice = (decimal)dr["PreDIRebatePrice"];
            if (!string.IsNullOrEmpty(dr["AftDIRebatePrice"].ToString())) m.AftDIRebatePrice = (decimal)dr["AftDIRebatePrice"];
            if (!string.IsNullOrEmpty(dr["ChageTime"].ToString())) m.ChageTime = (DateTime)dr["ChageTime"];
            if (!string.IsNullOrEmpty(dr["ChangeStaff"].ToString())) m.ChangeStaff = (int)dr["ChangeStaff"];
            if (!string.IsNullOrEmpty(dr["Remark"].ToString())) m.Remark = (int)dr["Remark"];

            return m;
        }
    }
}

