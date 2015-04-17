
// ===================================================================
// 文件： CM_VehicleDAL.cs
// 项目名称：
// 创建时间：2015/1/27
// 作者:	   lv
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.CM;


namespace MCSFramework.SQLDAL.CM
{
	/// <summary>
	///CM_Vehicle数据访问DAL类
	/// </summary>
	public class CM_VehicleDAL : BaseSimpleDAL<CM_Vehicle>
	{
		#region 构造函数
		///<summary>
		///
		///</summary>
		public CM_VehicleDAL()
		{
			_ProcePrefix = "MCS_CM.dbo.sp_CM_Vehicle";
		}
		#endregion
		
		
		/// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(CM_Vehicle m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@VehicleNo", SqlDbType.VarChar, 50, m.VehicleNo),
				SQLDatabase.MakeInParam("@VIN", SqlDbType.VarChar, 50, m.VIN),
				SQLDatabase.MakeInParam("@VehicleClassify", SqlDbType.Int, 4, m.VehicleClassify),
				SQLDatabase.MakeInParam("@Nameplate", SqlDbType.VarChar, 50, m.Nameplate),
				SQLDatabase.MakeInParam("@Tonnage", SqlDbType.Decimal, 9, m.Tonnage),
				SQLDatabase.MakeInParam("@SeatNum", SqlDbType.Int, 4, m.SeatNum),
				SQLDatabase.MakeInParam("@PurchaseDate", SqlDbType.DateTime, 8, m.PurchaseDate),
				SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, m.State),
				SQLDatabase.MakeInParam("@Kilometres", SqlDbType.Int, 4, m.Kilometres),
				SQLDatabase.MakeInParam("@AvgOilWear", SqlDbType.Int, 4, m.AvgOilWear),
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, m.Client),
				SQLDatabase.MakeInParam("@RelateStaff", SqlDbType.Int, 4, m.RelateStaff),
				SQLDatabase.MakeInParam("@RelateWareHouse", SqlDbType.Int, 4, m.RelateWareHouse),
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
        public override int Update(CM_Vehicle m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@VehicleNo", SqlDbType.VarChar, 50, m.VehicleNo),
				SQLDatabase.MakeInParam("@VIN", SqlDbType.VarChar, 50, m.VIN),
				SQLDatabase.MakeInParam("@VehicleClassify", SqlDbType.Int, 4, m.VehicleClassify),
				SQLDatabase.MakeInParam("@Nameplate", SqlDbType.VarChar, 50, m.Nameplate),
				SQLDatabase.MakeInParam("@Tonnage", SqlDbType.Decimal, 9, m.Tonnage),
				SQLDatabase.MakeInParam("@SeatNum", SqlDbType.Int, 4, m.SeatNum),
				SQLDatabase.MakeInParam("@PurchaseDate", SqlDbType.DateTime, 8, m.PurchaseDate),
				SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, m.State),
				SQLDatabase.MakeInParam("@Kilometres", SqlDbType.Int, 4, m.Kilometres),
				SQLDatabase.MakeInParam("@AvgOilWear", SqlDbType.Int, 4, m.AvgOilWear),
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, m.Client),
				SQLDatabase.MakeInParam("@RelateStaff", SqlDbType.Int, 4, m.RelateStaff),
				SQLDatabase.MakeInParam("@RelateWareHouse", SqlDbType.Int, 4, m.RelateWareHouse),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 1000, m.Remark),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@UpdateStaff", SqlDbType.Int, 4, m.UpdateStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
			#endregion
			
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
			
			return ret;
        }
		
        protected override CM_Vehicle FillModel(IDataReader dr)
		{
			CM_Vehicle m = new CM_Vehicle();
			if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (int)dr["ID"];
			if (!string.IsNullOrEmpty(dr["VehicleNo"].ToString()))	m.VehicleNo = (string)dr["VehicleNo"];
			if (!string.IsNullOrEmpty(dr["VIN"].ToString()))	m.VIN = (string)dr["VIN"];
			if (!string.IsNullOrEmpty(dr["VehicleClassify"].ToString()))	m.VehicleClassify = (int)dr["VehicleClassify"];
			if (!string.IsNullOrEmpty(dr["Nameplate"].ToString()))	m.Nameplate = (string)dr["Nameplate"];
			if (!string.IsNullOrEmpty(dr["Tonnage"].ToString()))	m.Tonnage = (decimal)dr["Tonnage"];
			if (!string.IsNullOrEmpty(dr["SeatNum"].ToString()))	m.SeatNum = (int)dr["SeatNum"];
			if (!string.IsNullOrEmpty(dr["PurchaseDate"].ToString()))	m.PurchaseDate = (DateTime)dr["PurchaseDate"];
			if (!string.IsNullOrEmpty(dr["State"].ToString()))	m.State = (int)dr["State"];
			if (!string.IsNullOrEmpty(dr["Kilometres"].ToString()))	m.Kilometres = (int)dr["Kilometres"];
			if (!string.IsNullOrEmpty(dr["AvgOilWear"].ToString()))	m.AvgOilWear = (int)dr["AvgOilWear"];
			if (!string.IsNullOrEmpty(dr["Client"].ToString()))	m.Client = (int)dr["Client"];
			if (!string.IsNullOrEmpty(dr["RelateStaff"].ToString()))	m.RelateStaff = (int)dr["RelateStaff"];
			if (!string.IsNullOrEmpty(dr["RelateWareHouse"].ToString()))	m.RelateWareHouse = (int)dr["RelateWareHouse"];
			if (!string.IsNullOrEmpty(dr["Remark"].ToString()))	m.Remark = (string)dr["Remark"];
			if (!string.IsNullOrEmpty(dr["ApproveFlag"].ToString()))	m.ApproveFlag = (int)dr["ApproveFlag"];
			if (!string.IsNullOrEmpty(dr["InsertTime"].ToString()))	m.InsertTime = (DateTime)dr["InsertTime"];
			if (!string.IsNullOrEmpty(dr["InsertStaff"].ToString()))	m.InsertStaff = (int)dr["InsertStaff"];
			if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString()))	m.UpdateTime = (DateTime)dr["UpdateTime"];
			if (!string.IsNullOrEmpty(dr["UpdateStaff"].ToString()))	m.UpdateStaff = (int)dr["UpdateStaff"];
			if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString()))	m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);
						
			return m;
		}
    }
}

