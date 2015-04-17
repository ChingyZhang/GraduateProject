
// ===================================================================
// 文件： INV_ReportedLossCodeLibDAL.cs
// 项目名称：
// 创建时间：2012-7-23
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.EBM;


namespace MCSFramework.SQLDAL.EBM
{
	/// <summary>
	///INV_ReportedLossCodeLib数据访问DAL类
	/// </summary>
	public class INV_ReportedLossCodeLibDAL : BaseSimpleDAL<INV_ReportedLossCodeLib>
	{
		#region 构造函数
		///<summary>
		///
		///</summary>
		public INV_ReportedLossCodeLibDAL()
		{
			_ProcePrefix = "MCS_EBM.dbo.sp_INV_ReportedLossCodeLib";
		}
		#endregion
		
		
		/// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(INV_ReportedLossCodeLib m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@LossID", SqlDbType.Int, 4, m.LossID),
				SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, m.Product),
				SQLDatabase.MakeInParam("@CaseCode", SqlDbType.VarChar, 50, m.CaseCode),
				SQLDatabase.MakeInParam("@PieceCode", SqlDbType.VarChar, 50, m.PieceCode),
				SQLDatabase.MakeInParam("@LotNumber", SqlDbType.VarChar, 50, m.LotNumber),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
			#endregion
			
            return  SQLDatabase.RunProc(_ProcePrefix + "_Add", prams);
        }
		
		/// <summary>
        /// 更新项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Update(INV_ReportedLossCodeLib m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.BigInt, 8, m.ID),
				SQLDatabase.MakeInParam("@LossID", SqlDbType.Int, 4, m.LossID),
				SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, m.Product),
				SQLDatabase.MakeInParam("@CaseCode", SqlDbType.VarChar, 50, m.CaseCode),
				SQLDatabase.MakeInParam("@PieceCode", SqlDbType.VarChar, 50, m.PieceCode),
				SQLDatabase.MakeInParam("@LotNumber", SqlDbType.VarChar, 50, m.LotNumber),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
			#endregion
			
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
			
			return ret;
        }
		
        protected override INV_ReportedLossCodeLib FillModel(IDataReader dr)
		{
			INV_ReportedLossCodeLib m = new INV_ReportedLossCodeLib();
			if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (long)dr["ID"];
			if (!string.IsNullOrEmpty(dr["LossID"].ToString()))	m.LossID = (int)dr["LossID"];
			if (!string.IsNullOrEmpty(dr["Product"].ToString()))	m.Product = (int)dr["Product"];
			if (!string.IsNullOrEmpty(dr["CaseCode"].ToString()))	m.CaseCode = (string)dr["CaseCode"];
			if (!string.IsNullOrEmpty(dr["PieceCode"].ToString()))	m.PieceCode = (string)dr["PieceCode"];
			if (!string.IsNullOrEmpty(dr["LotNumber"].ToString()))	m.LotNumber = (string)dr["LotNumber"];
			if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString()))	m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);
						
			return m;
		}
    }
}

