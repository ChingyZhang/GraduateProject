
// ===================================================================
// 文件： INV_CheckInventoryCodeLibDAL.cs
// 项目名称：
// 创建时间：2014-07-27
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
	///INV_CheckInventoryCodeLib数据访问DAL类
	/// </summary>
	public class INV_CheckInventoryCodeLibDAL : BaseSimpleDAL<INV_CheckInventoryCodeLib>
	{
		#region 构造函数
		///<summary>
		///
		///</summary>
		public INV_CheckInventoryCodeLibDAL()
		{
			_ProcePrefix = "MCS_EBM.dbo.sp_INV_CheckInventoryCodeLib";
		}
		#endregion
		
		
		/// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(INV_CheckInventoryCodeLib m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@CheckID", SqlDbType.Int, 4, m.CheckID),
				SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, m.Product),
				SQLDatabase.MakeInParam("@CaseCode", SqlDbType.VarChar, 50, m.CaseCode),
				SQLDatabase.MakeInParam("@PieceCode", SqlDbType.VarChar, 50, m.PieceCode),
				SQLDatabase.MakeInParam("@LotNumber", SqlDbType.VarChar, 50, m.LotNumber),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
			#endregion
			
            m.ID =  SQLDatabase.RunProc(_ProcePrefix + "_Add", prams);
			
            return (int)m.ID;
        }
		
		/// <summary>
        /// 更新项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Update(INV_CheckInventoryCodeLib m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.BigInt, 8, m.ID),
				SQLDatabase.MakeInParam("@CheckID", SqlDbType.Int, 4, m.CheckID),
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
		
        protected override INV_CheckInventoryCodeLib FillModel(IDataReader dr)
		{
			INV_CheckInventoryCodeLib m = new INV_CheckInventoryCodeLib();
			if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (long)dr["ID"];
			if (!string.IsNullOrEmpty(dr["CheckID"].ToString()))	m.CheckID = (int)dr["CheckID"];
			if (!string.IsNullOrEmpty(dr["Product"].ToString()))	m.Product = (int)dr["Product"];
			if (!string.IsNullOrEmpty(dr["CaseCode"].ToString()))	m.CaseCode = (string)dr["CaseCode"];
			if (!string.IsNullOrEmpty(dr["PieceCode"].ToString()))	m.PieceCode = (string)dr["PieceCode"];
			if (!string.IsNullOrEmpty(dr["LotNumber"].ToString()))	m.LotNumber = (string)dr["LotNumber"];
			if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString()))	m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);
						
			return m;
		}
    }
}

