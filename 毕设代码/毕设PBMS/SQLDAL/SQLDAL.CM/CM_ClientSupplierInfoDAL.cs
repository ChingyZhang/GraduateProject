
// ===================================================================
// 文件： CM_ClientSupplierInfoDAL.cs
// 项目名称：
// 创建时间：2015-03-25
// 作者:	   Shen Gang
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
	///CM_ClientSupplierInfo数据访问DAL类
	/// </summary>
	public class CM_ClientSupplierInfoDAL : BaseSimpleDAL<CM_ClientSupplierInfo>
	{
		#region 构造函数
		///<summary>
		///
		///</summary>
		public CM_ClientSupplierInfoDAL()
		{
			_ProcePrefix = "MCS_CM.dbo.sp_CM_ClientSupplierInfo";
		}
		#endregion
		
		
		/// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(CM_ClientSupplierInfo m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, m.Client),
				SQLDatabase.MakeInParam("@Supplier", SqlDbType.Int, 4, m.Supplier),
				SQLDatabase.MakeInParam("@Salesman", SqlDbType.Int, 4, m.Salesman),
				SQLDatabase.MakeInParam("@Code", SqlDbType.VarChar, 100, m.Code),
				SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, m.State),
				SQLDatabase.MakeInParam("@BeginDate", SqlDbType.DateTime, 8, m.BeginDate),
				SQLDatabase.MakeInParam("@EndDate", SqlDbType.DateTime, 8, m.EndDate),
				SQLDatabase.MakeInParam("@StandardPrice", SqlDbType.Int, 4, m.StandardPrice),
				SQLDatabase.MakeInParam("@TDPChannel", SqlDbType.Int, 4, m.TDPChannel),
				SQLDatabase.MakeInParam("@TDPSalesArea", SqlDbType.Int, 4, m.TDPSalesArea),
				SQLDatabase.MakeInParam("@VisitRoute", SqlDbType.Int, 4, m.VisitRoute),
				SQLDatabase.MakeInParam("@VisitSequence", SqlDbType.Int, 4, m.VisitSequence),
				SQLDatabase.MakeInParam("@VisitTemplate", SqlDbType.Int, 4, m.VisitTemplate),
				SQLDatabase.MakeInParam("@VisitCycle", SqlDbType.Int, 4, m.VisitCycle),
				SQLDatabase.MakeInParam("@VisitDay", SqlDbType.Int, 4, m.VisitDay),
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
        public override int Update(CM_ClientSupplierInfo m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, m.Client),
				SQLDatabase.MakeInParam("@Supplier", SqlDbType.Int, 4, m.Supplier),
				SQLDatabase.MakeInParam("@Salesman", SqlDbType.Int, 4, m.Salesman),
				SQLDatabase.MakeInParam("@Code", SqlDbType.VarChar, 100, m.Code),
				SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, m.State),
				SQLDatabase.MakeInParam("@BeginDate", SqlDbType.DateTime, 8, m.BeginDate),
				SQLDatabase.MakeInParam("@EndDate", SqlDbType.DateTime, 8, m.EndDate),
				SQLDatabase.MakeInParam("@StandardPrice", SqlDbType.Int, 4, m.StandardPrice),
				SQLDatabase.MakeInParam("@TDPChannel", SqlDbType.Int, 4, m.TDPChannel),
				SQLDatabase.MakeInParam("@TDPSalesArea", SqlDbType.Int, 4, m.TDPSalesArea),
				SQLDatabase.MakeInParam("@VisitRoute", SqlDbType.Int, 4, m.VisitRoute),
				SQLDatabase.MakeInParam("@VisitSequence", SqlDbType.Int, 4, m.VisitSequence),
				SQLDatabase.MakeInParam("@VisitTemplate", SqlDbType.Int, 4, m.VisitTemplate),
				SQLDatabase.MakeInParam("@VisitCycle", SqlDbType.Int, 4, m.VisitCycle),
				SQLDatabase.MakeInParam("@VisitDay", SqlDbType.Int, 4, m.VisitDay),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 1000, m.Remark),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@UpdateStaff", SqlDbType.Int, 4, m.UpdateStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
			#endregion
			
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
			
			return ret;
        }
		
        protected override CM_ClientSupplierInfo FillModel(IDataReader dr)
		{
			CM_ClientSupplierInfo m = new CM_ClientSupplierInfo();
			if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (int)dr["ID"];
			if (!string.IsNullOrEmpty(dr["Client"].ToString()))	m.Client = (int)dr["Client"];
			if (!string.IsNullOrEmpty(dr["Supplier"].ToString()))	m.Supplier = (int)dr["Supplier"];
			if (!string.IsNullOrEmpty(dr["Salesman"].ToString()))	m.Salesman = (int)dr["Salesman"];
			if (!string.IsNullOrEmpty(dr["Code"].ToString()))	m.Code = (string)dr["Code"];
			if (!string.IsNullOrEmpty(dr["State"].ToString()))	m.State = (int)dr["State"];
			if (!string.IsNullOrEmpty(dr["BeginDate"].ToString()))	m.BeginDate = (DateTime)dr["BeginDate"];
			if (!string.IsNullOrEmpty(dr["EndDate"].ToString()))	m.EndDate = (DateTime)dr["EndDate"];
			if (!string.IsNullOrEmpty(dr["StandardPrice"].ToString()))	m.StandardPrice = (int)dr["StandardPrice"];
			if (!string.IsNullOrEmpty(dr["TDPChannel"].ToString()))	m.TDPChannel = (int)dr["TDPChannel"];
			if (!string.IsNullOrEmpty(dr["TDPSalesArea"].ToString()))	m.TDPSalesArea = (int)dr["TDPSalesArea"];
			if (!string.IsNullOrEmpty(dr["VisitRoute"].ToString()))	m.VisitRoute = (int)dr["VisitRoute"];
			if (!string.IsNullOrEmpty(dr["VisitSequence"].ToString()))	m.VisitSequence = (int)dr["VisitSequence"];
			if (!string.IsNullOrEmpty(dr["VisitTemplate"].ToString()))	m.VisitTemplate = (int)dr["VisitTemplate"];
			if (!string.IsNullOrEmpty(dr["VisitCycle"].ToString()))	m.VisitCycle = (int)dr["VisitCycle"];
			if (!string.IsNullOrEmpty(dr["VisitDay"].ToString()))	m.VisitDay = (int)dr["VisitDay"];
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

