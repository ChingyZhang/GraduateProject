
// ===================================================================
// 文件： INV_Inventory_CarryDownDAL.cs
// 项目名称：
// 创建时间：2015/1/27
// 作者:	   lv
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.PBM;


namespace MCSFramework.SQLDAL.PBM
{
	/// <summary>
	///INV_Inventory_CarryDown数据访问DAL类
	/// </summary>
	public class INV_Inventory_CarryDownDAL : BaseSimpleDAL<INV_Inventory_CarryDown>
	{
		#region 构造函数
		///<summary>
		///
		///</summary>
		public INV_Inventory_CarryDownDAL()
		{
			_ProcePrefix = "MCS_PBM.dbo.sp_INV_Inventory_CarryDown";
		}
		#endregion
		
		
		/// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(INV_Inventory_CarryDown m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@WareHouse", SqlDbType.Int, 4, m.WareHouse),
				SQLDatabase.MakeInParam("@BeginDate", SqlDbType.DateTime, 8, m.BeginDate),
				SQLDatabase.MakeInParam("@EndDate", SqlDbType.DateTime, 8, m.EndDate),
				SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, m.Product),
				SQLDatabase.MakeInParam("@LotNumber", SqlDbType.VarChar, 100, m.LotNumber),
				SQLDatabase.MakeInParam("@ProductDate", SqlDbType.DateTime, 8, m.ProductDate),
				SQLDatabase.MakeInParam("@Price", SqlDbType.Decimal, 9, m.Price),
				SQLDatabase.MakeInParam("@BeginQuanitity", SqlDbType.Int, 4, m.BeginQuanitity),
				SQLDatabase.MakeInParam("@InWareHouse", SqlDbType.Int, 4, m.InWareHouse),
				SQLDatabase.MakeInParam("@OutWarehouse", SqlDbType.Int, 4, m.OutWarehouse),
				SQLDatabase.MakeInParam("@LossWarehouse", SqlDbType.Int, 4, m.LossWarehouse),
				SQLDatabase.MakeInParam("@EndQuantitty", SqlDbType.Int, 4, m.EndQuantitty),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 1000, m.Remark),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@InsertStaff", SqlDbType.Int, 4, m.InsertStaff),
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
        public override int Update(INV_Inventory_CarryDown m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@WareHouse", SqlDbType.Int, 4, m.WareHouse),
				SQLDatabase.MakeInParam("@BeginDate", SqlDbType.DateTime, 8, m.BeginDate),
				SQLDatabase.MakeInParam("@EndDate", SqlDbType.DateTime, 8, m.EndDate),
				SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, m.Product),
				SQLDatabase.MakeInParam("@LotNumber", SqlDbType.VarChar, 100, m.LotNumber),
				SQLDatabase.MakeInParam("@ProductDate", SqlDbType.DateTime, 8, m.ProductDate),
				SQLDatabase.MakeInParam("@Price", SqlDbType.Decimal, 9, m.Price),
				SQLDatabase.MakeInParam("@BeginQuanitity", SqlDbType.Int, 4, m.BeginQuanitity),
				SQLDatabase.MakeInParam("@InWareHouse", SqlDbType.Int, 4, m.InWareHouse),
				SQLDatabase.MakeInParam("@OutWarehouse", SqlDbType.Int, 4, m.OutWarehouse),
				SQLDatabase.MakeInParam("@LossWarehouse", SqlDbType.Int, 4, m.LossWarehouse),
				SQLDatabase.MakeInParam("@EndQuantitty", SqlDbType.Int, 4, m.EndQuantitty),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 1000, m.Remark),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
			#endregion
			
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
			
			return ret;
        }
		
        protected override INV_Inventory_CarryDown FillModel(IDataReader dr)
		{
			INV_Inventory_CarryDown m = new INV_Inventory_CarryDown();
			if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (int)dr["ID"];
			if (!string.IsNullOrEmpty(dr["WareHouse"].ToString()))	m.WareHouse = (int)dr["WareHouse"];
			if (!string.IsNullOrEmpty(dr["BeginDate"].ToString()))	m.BeginDate = (DateTime)dr["BeginDate"];
			if (!string.IsNullOrEmpty(dr["EndDate"].ToString()))	m.EndDate = (DateTime)dr["EndDate"];
			if (!string.IsNullOrEmpty(dr["Product"].ToString()))	m.Product = (int)dr["Product"];
			if (!string.IsNullOrEmpty(dr["LotNumber"].ToString()))	m.LotNumber = (string)dr["LotNumber"];
			if (!string.IsNullOrEmpty(dr["ProductDate"].ToString()))	m.ProductDate = (DateTime)dr["ProductDate"];
			if (!string.IsNullOrEmpty(dr["Price"].ToString()))	m.Price = (decimal)dr["Price"];
			if (!string.IsNullOrEmpty(dr["BeginQuanitity"].ToString()))	m.BeginQuanitity = (int)dr["BeginQuanitity"];
			if (!string.IsNullOrEmpty(dr["InWareHouse"].ToString()))	m.InWareHouse = (int)dr["InWareHouse"];
			if (!string.IsNullOrEmpty(dr["OutWarehouse"].ToString()))	m.OutWarehouse = (int)dr["OutWarehouse"];
			if (!string.IsNullOrEmpty(dr["LossWarehouse"].ToString()))	m.LossWarehouse = (int)dr["LossWarehouse"];
			if (!string.IsNullOrEmpty(dr["EndQuantitty"].ToString()))	m.EndQuantitty = (int)dr["EndQuantitty"];
			if (!string.IsNullOrEmpty(dr["Remark"].ToString()))	m.Remark = (string)dr["Remark"];
			if (!string.IsNullOrEmpty(dr["ApproveFlag"].ToString()))	m.ApproveFlag = (int)dr["ApproveFlag"];
			if (!string.IsNullOrEmpty(dr["InsertTime"].ToString()))	m.InsertTime = (DateTime)dr["InsertTime"];
			if (!string.IsNullOrEmpty(dr["InsertStaff"].ToString()))	m.InsertStaff = (int)dr["InsertStaff"];
			if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString()))	m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);
						
			return m;
		}
    }
}

