
// ===================================================================
// 文件： ORD_OrderLimitFactorDAL.cs
// 项目名称：
// 创建时间：2010/12/8
// 作者:	   
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.Logistics;
using MCSFramework.Common;


namespace MCSFramework.SQLDAL.Logistics
{
	/// <summary>
	///ORD_OrderLimitFactor数据访问DAL类
	/// </summary>
	public class ORD_OrderLimitFactorDAL : BaseSimpleDAL<ORD_OrderLimitFactor>
	{
		#region 构造函数
		///<summary>
		///
		///</summary>
		public ORD_OrderLimitFactorDAL()
		{
			_ProcePrefix = "MCS_Logistics.dbo.sp_ORD_OrderLimitFactor";
		}
		#endregion
		
		
		/// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(ORD_OrderLimitFactor m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, m.Client),
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, m.AccountMonth),
				SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, m.Product),
				SQLDatabase.MakeInParam("@UpperLimit", SqlDbType.Int, 4, m.UpperLimit),
				SQLDatabase.MakeInParam("@LowerLimit", SqlDbType.Int, 4, m.LowerLimit),
                SQLDatabase.MakeInParam("@TheoryQuantity", SqlDbType.Int, 4, m.TheoryQuantity),
				SQLDatabase.MakeInParam("@Factor", SqlDbType.Decimal, 9, m.Factor),
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
        public override int Update(ORD_OrderLimitFactor m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, m.Client),
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, m.AccountMonth),
				SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, m.Product),
				SQLDatabase.MakeInParam("@UpperLimit", SqlDbType.Int, 4, m.UpperLimit),
				SQLDatabase.MakeInParam("@LowerLimit", SqlDbType.Int, 4, m.LowerLimit),
                SQLDatabase.MakeInParam("@TheoryQuantity", SqlDbType.Int, 4, m.TheoryQuantity),
				SQLDatabase.MakeInParam("@Factor", SqlDbType.Decimal, 9, m.Factor),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@UpdateStaff", SqlDbType.Int, 4, m.UpdateStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
			#endregion
			
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
			
			return ret;
        }
		
        protected override ORD_OrderLimitFactor FillModel(IDataReader dr)
		{
			ORD_OrderLimitFactor m = new ORD_OrderLimitFactor();
			if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (int)dr["ID"];
			if (!string.IsNullOrEmpty(dr["Client"].ToString()))	m.Client = (int)dr["Client"];
			if (!string.IsNullOrEmpty(dr["AccountMonth"].ToString()))	m.AccountMonth = (int)dr["AccountMonth"];
			if (!string.IsNullOrEmpty(dr["Product"].ToString()))	m.Product = (int)dr["Product"];
			if (!string.IsNullOrEmpty(dr["UpperLimit"].ToString()))	m.UpperLimit = (int)dr["UpperLimit"];
			if (!string.IsNullOrEmpty(dr["LowerLimit"].ToString()))	m.LowerLimit = (int)dr["LowerLimit"];
            if (!string.IsNullOrEmpty(dr["TheoryQuantity"].ToString())) m.LowerLimit = (int)dr["TheoryQuantity"];
			if (!string.IsNullOrEmpty(dr["Factor"].ToString()))	m.Factor = (decimal)dr["Factor"];
			if (!string.IsNullOrEmpty(dr["ApproveFlag"].ToString()))	m.ApproveFlag = (int)dr["ApproveFlag"];
			if (!string.IsNullOrEmpty(dr["InsertTime"].ToString()))	m.InsertTime = (DateTime)dr["InsertTime"];
			if (!string.IsNullOrEmpty(dr["InsertStaff"].ToString()))	m.InsertStaff = (int)dr["InsertStaff"];
			if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString()))	m.UpdateTime = (DateTime)dr["UpdateTime"];
			if (!string.IsNullOrEmpty(dr["UpdateStaff"].ToString()))	m.UpdateStaff = (int)dr["UpdateStaff"];
			if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString()))	m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);
						
			return m;
		}

        public  DataTable GetLimitInfo(int month, int Client)
        {
            SqlDataReader dr = null;
            #region	设置参数集
            SqlParameter[] prams = {
                SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4 ,Client),
                SQLDatabase.MakeInParam("@Month", SqlDbType.Int, 4,month)               
			};
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_GetLimitInfo", prams, out dr);

            return Tools.ConvertDataReaderToDataTable(dr);
        }
    }
}

