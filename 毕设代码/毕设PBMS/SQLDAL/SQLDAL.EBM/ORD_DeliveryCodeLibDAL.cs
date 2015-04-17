
// ===================================================================
// 文件： ORD_DeliveryCodeLibDAL.cs
// 项目名称：
// 创建时间：2012-7-22
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
	///ORD_DeliveryCodeLib数据访问DAL类
	/// </summary>
	public class ORD_DeliveryCodeLibDAL : BaseSimpleDAL<ORD_DeliveryCodeLib>
	{
		#region 构造函数
		///<summary>
		///
		///</summary>
		public ORD_DeliveryCodeLibDAL()
		{
			_ProcePrefix = "MCS_EBM.dbo.sp_ORD_DeliveryCodeLib";
		}
		#endregion
		
		
		/// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(ORD_DeliveryCodeLib m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@DeliveryID", SqlDbType.Int, 4, m.DeliveryID),
				SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, m.Product),
				SQLDatabase.MakeInParam("@CaseCode", SqlDbType.VarChar, 50, m.CaseCode),
				SQLDatabase.MakeInParam("@PieceCode", SqlDbType.VarChar, 50, m.PieceCode),
				SQLDatabase.MakeInParam("@LotNumber", SqlDbType.VarChar, 50, m.LotNumber),
				SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, m.State),
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
        public override int Update(ORD_DeliveryCodeLib m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.BigInt, 8, m.ID),
				SQLDatabase.MakeInParam("@DeliveryID", SqlDbType.Int, 4, m.DeliveryID),
				SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, m.Product),
				SQLDatabase.MakeInParam("@CaseCode", SqlDbType.VarChar, 50, m.CaseCode),
				SQLDatabase.MakeInParam("@PieceCode", SqlDbType.VarChar, 50, m.PieceCode),
				SQLDatabase.MakeInParam("@LotNumber", SqlDbType.VarChar, 50, m.LotNumber),
				SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, m.State),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
			#endregion
			
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
			
			return ret;
        }
		
        protected override ORD_DeliveryCodeLib FillModel(IDataReader dr)
		{
			ORD_DeliveryCodeLib m = new ORD_DeliveryCodeLib();
			if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (long)dr["ID"];
			if (!string.IsNullOrEmpty(dr["DeliveryID"].ToString()))	m.DeliveryID = (int)dr["DeliveryID"];
			if (!string.IsNullOrEmpty(dr["Product"].ToString()))	m.Product = (int)dr["Product"];
			if (!string.IsNullOrEmpty(dr["CaseCode"].ToString()))	m.CaseCode = (string)dr["CaseCode"];
			if (!string.IsNullOrEmpty(dr["PieceCode"].ToString()))	m.PieceCode = (string)dr["PieceCode"];
			if (!string.IsNullOrEmpty(dr["LotNumber"].ToString()))	m.LotNumber = (string)dr["LotNumber"];
			if (!string.IsNullOrEmpty(dr["State"].ToString()))	m.State = (int)dr["State"];
			if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString()))	m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);
						
			return m;
		}
    }
}

