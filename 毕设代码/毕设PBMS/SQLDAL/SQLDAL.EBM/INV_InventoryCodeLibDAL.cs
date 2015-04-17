
// ===================================================================
// 文件： INV_InventoryCodeLibDAL.cs
// 项目名称：
// 创建时间：2012-7-21
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
	///INV_InventoryCodeLib数据访问DAL类
	/// </summary>
	public class INV_InventoryCodeLibDAL : BaseSimpleDAL<INV_InventoryCodeLib>
	{
		#region 构造函数
		///<summary>
		///
		///</summary>
		public INV_InventoryCodeLibDAL()
		{
			_ProcePrefix = "MCS_EBM.dbo.sp_INV_InventoryCodeLib";
		}
		#endregion

        #region 基本操作
        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(INV_InventoryCodeLib m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, m.Product),
				SQLDatabase.MakeInParam("@CaseCode", SqlDbType.VarChar, 50, m.CaseCode),
				SQLDatabase.MakeInParam("@PieceCode", SqlDbType.VarChar, 50, m.PieceCode),
				SQLDatabase.MakeInParam("@LotNumber", SqlDbType.VarChar, 50, m.LotNumber),
				SQLDatabase.MakeInParam("@WareHouse", SqlDbType.Int, 4, m.WareHouse),
				SQLDatabase.MakeInParam("@WareHouseCell", SqlDbType.Int, 4, m.WareHouseCell),
				SQLDatabase.MakeInParam("@Price", SqlDbType.Decimal, 9, m.Price),
				SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, m.State),
				SQLDatabase.MakeInParam("@PutInTime", SqlDbType.DateTime, 8, m.PutInTime),
				SQLDatabase.MakeInParam("@LastUpdateTime", SqlDbType.DateTime, 8, m.LastUpdateTime),
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
        public override int Update(INV_InventoryCodeLib m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.BigInt, 8, m.ID),
				SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, m.Product),
				SQLDatabase.MakeInParam("@CaseCode", SqlDbType.VarChar, 50, m.CaseCode),
				SQLDatabase.MakeInParam("@PieceCode", SqlDbType.VarChar, 50, m.PieceCode),
				SQLDatabase.MakeInParam("@LotNumber", SqlDbType.VarChar, 50, m.LotNumber),
				SQLDatabase.MakeInParam("@WareHouse", SqlDbType.Int, 4, m.WareHouse),
				SQLDatabase.MakeInParam("@WareHouseCell", SqlDbType.Int, 4, m.WareHouseCell),
				SQLDatabase.MakeInParam("@Price", SqlDbType.Decimal, 9, m.Price),
				SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, m.State),
				SQLDatabase.MakeInParam("@PutInTime", SqlDbType.DateTime, 8, m.PutInTime),
				SQLDatabase.MakeInParam("@LastUpdateTime", SqlDbType.DateTime, 8, m.LastUpdateTime),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
			#endregion
			
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
			
			return ret;
        }
		
        protected override INV_InventoryCodeLib FillModel(IDataReader dr)
		{
			INV_InventoryCodeLib m = new INV_InventoryCodeLib();
			if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (long)dr["ID"];
			if (!string.IsNullOrEmpty(dr["Product"].ToString()))	m.Product = (int)dr["Product"];
			if (!string.IsNullOrEmpty(dr["CaseCode"].ToString()))	m.CaseCode = (string)dr["CaseCode"];
			if (!string.IsNullOrEmpty(dr["PieceCode"].ToString()))	m.PieceCode = (string)dr["PieceCode"];
			if (!string.IsNullOrEmpty(dr["LotNumber"].ToString()))	m.LotNumber = (string)dr["LotNumber"];
			if (!string.IsNullOrEmpty(dr["WareHouse"].ToString()))	m.WareHouse = (int)dr["WareHouse"];
			if (!string.IsNullOrEmpty(dr["WareHouseCell"].ToString()))	m.WareHouseCell = (int)dr["WareHouseCell"];
			if (!string.IsNullOrEmpty(dr["Price"].ToString()))	m.Price = (decimal)dr["Price"];
			if (!string.IsNullOrEmpty(dr["State"].ToString()))	m.State = (int)dr["State"];
			if (!string.IsNullOrEmpty(dr["PutInTime"].ToString()))	m.PutInTime = (DateTime)dr["PutInTime"];
			if (!string.IsNullOrEmpty(dr["LastUpdateTime"].ToString()))	m.LastUpdateTime = (DateTime)dr["LastUpdateTime"];
			if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString()))	m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);
						
			return m;
		}
        #endregion

        /// <summary>
        /// 按物流码设置库存状态
        /// </summary>
        /// <param name="PieceCode"></param>
        /// <param name="State"></param>
        /// <returns></returns>
        public int SetState(string PieceCode, int State)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@PieceCode", SqlDbType.VarChar, 50, PieceCode),
				SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, State)
			};
			#endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_SetState", prams);
			
			return ret;
        }
    }
}

