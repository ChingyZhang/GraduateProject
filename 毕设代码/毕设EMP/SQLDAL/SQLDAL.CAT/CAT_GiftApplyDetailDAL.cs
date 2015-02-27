
// ===================================================================
// 文件： CAT_GiftApplyDetailDAL.cs
// 项目名称：
// 创建时间：2012/8/13
// 作者:	   chf
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.CAT;


namespace MCSFramework.SQLDAL.CAT
{
	/// <summary>
	///CAT_GiftApplyDetail数据访问DAL类
	/// </summary>
	public class CAT_GiftApplyDetailDAL : BaseSimpleDAL<CAT_GiftApplyDetail>
	{
		#region 构造函数
		///<summary>
		///
		///</summary>
		public CAT_GiftApplyDetailDAL()
		{
			_ProcePrefix = "MCS_CAT.dbo.sp_CAT_GiftApplyDetail";
		}
		#endregion
		
		
		/// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(CAT_GiftApplyDetail m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Activity", SqlDbType.Int, 4, m.Activity),
				SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, m.Product),
				SQLDatabase.MakeInParam("@ApplyQuantity", SqlDbType.Int, 4, m.ApplyQuantity),
				SQLDatabase.MakeInParam("@AdjustQuantity", SqlDbType.Int, 4, m.AdjustQuantity),
				SQLDatabase.MakeInParam("@UsedQuantity", SqlDbType.Int, 4, m.UsedQuantity),
				SQLDatabase.MakeInParam("@BalanceQuantity", SqlDbType.Int, 4, m.BalanceQuantity),
				SQLDatabase.MakeInParam("@Price", SqlDbType.Decimal, 9, m.Price),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 2000, m.Remark),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
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
        public override int Update(CAT_GiftApplyDetail m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@Activity", SqlDbType.Int, 4, m.Activity),
				SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, m.Product),
				SQLDatabase.MakeInParam("@ApplyQuantity", SqlDbType.Int, 4, m.ApplyQuantity),
				SQLDatabase.MakeInParam("@AdjustQuantity", SqlDbType.Int, 4, m.AdjustQuantity),
				SQLDatabase.MakeInParam("@UsedQuantity", SqlDbType.Int, 4, m.UsedQuantity),
				SQLDatabase.MakeInParam("@BalanceQuantity", SqlDbType.Int, 4, m.BalanceQuantity),
				SQLDatabase.MakeInParam("@Price", SqlDbType.Decimal, 9, m.Price),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 2000, m.Remark),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
			#endregion
			
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
			
			return ret;
        }
		
        protected override CAT_GiftApplyDetail FillModel(IDataReader dr)
		{
			CAT_GiftApplyDetail m = new CAT_GiftApplyDetail();
			if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (int)dr["ID"];
			if (!string.IsNullOrEmpty(dr["Activity"].ToString()))	m.Activity = (int)dr["Activity"];
			if (!string.IsNullOrEmpty(dr["Product"].ToString()))	m.Product = (int)dr["Product"];
			if (!string.IsNullOrEmpty(dr["ApplyQuantity"].ToString()))	m.ApplyQuantity = (int)dr["ApplyQuantity"];
			if (!string.IsNullOrEmpty(dr["AdjustQuantity"].ToString()))	m.AdjustQuantity = (int)dr["AdjustQuantity"];
			if (!string.IsNullOrEmpty(dr["UsedQuantity"].ToString()))	m.UsedQuantity = (int)dr["UsedQuantity"];
			if (!string.IsNullOrEmpty(dr["BalanceQuantity"].ToString()))	m.BalanceQuantity = (int)dr["BalanceQuantity"];
			if (!string.IsNullOrEmpty(dr["Price"].ToString()))	m.Price = (decimal)dr["Price"];
			if (!string.IsNullOrEmpty(dr["Remark"].ToString()))	m.Remark = (string)dr["Remark"];
			if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString()))	m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);
						
			return m;
		}
    }
}

