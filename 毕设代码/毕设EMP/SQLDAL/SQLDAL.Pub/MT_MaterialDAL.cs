
// ===================================================================
// 文件： MT_MaterialDAL.cs
// 项目名称：
// 创建时间：2008-12-22
// 作者:	   yangwei
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model;
using MCSFramework.Model.Pub;

namespace MCSFramework.SQLDAL.Pub
{
	/// <summary>
	///MT_Material数据访问DAL类
	/// </summary>
    public class MT_MaterialDAL : BaseSimpleDAL<MT_Material>
	{
		#region 构造函数
		///<summary>
		///
		///</summary>
		public MT_MaterialDAL()
		{
			_ProcePrefix = "MCS_Pub.dbo.sp_MT_Material";
		}
		#endregion
		
		
		/// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(MT_Material m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Name", SqlDbType.VarChar, 200, m.Name),
				SQLDatabase.MakeInParam("@Classify", SqlDbType.Int, 4, m.Classify),
				SQLDatabase.MakeInParam("@Code", SqlDbType.VarChar, 50, m.Code),
				SQLDatabase.MakeInParam("@TrafficPackaging", SqlDbType.Int, 4, m.TrafficPackaging),
				SQLDatabase.MakeInParam("@Packaging", SqlDbType.Int, 4, m.Packaging),
				SQLDatabase.MakeInParam("@ConvertFactor", SqlDbType.Int, 4, m.ConvertFactor),
				SQLDatabase.MakeInParam("@Weight", SqlDbType.Decimal, 9, m.Weight),
				SQLDatabase.MakeInParam("@Price", SqlDbType.Decimal, 9, m.Price),
				SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, m.State),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@InputTime", SqlDbType.DateTime, 8, m.InputTime),
				SQLDatabase.MakeInParam("@InputStaff", SqlDbType.Int, 4, m.InputStaff),
				SQLDatabase.MakeInParam("@UpdateTime", SqlDbType.DateTime, 8, m.UpdateTime),
				SQLDatabase.MakeInParam("@UpdateStaff", SqlDbType.Int, 4, m.UpdateStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 8000, CombineExtProperty(m.ExtPropertys,m.ModelName))
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
        public override int Update(MT_Material m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@Name", SqlDbType.VarChar, 200, m.Name),
				SQLDatabase.MakeInParam("@Classify", SqlDbType.Int, 4, m.Classify),
				SQLDatabase.MakeInParam("@Code", SqlDbType.VarChar, 50, m.Code),
				SQLDatabase.MakeInParam("@TrafficPackaging", SqlDbType.Int, 4, m.TrafficPackaging),
				SQLDatabase.MakeInParam("@Packaging", SqlDbType.Int, 4, m.Packaging),
				SQLDatabase.MakeInParam("@ConvertFactor", SqlDbType.Int, 4, m.ConvertFactor),
				SQLDatabase.MakeInParam("@Weight", SqlDbType.Decimal, 9, m.Weight),
				SQLDatabase.MakeInParam("@Price", SqlDbType.Decimal, 9, m.Price),
				SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, m.State),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Char, 1, m.ApproveFlag),
				SQLDatabase.MakeInParam("@InputTime", SqlDbType.DateTime, 8, m.InputTime),
				SQLDatabase.MakeInParam("@InputStaff", SqlDbType.Int, 4, m.InputStaff),
				SQLDatabase.MakeInParam("@UpdateTime", SqlDbType.DateTime, 8, m.UpdateTime),
				SQLDatabase.MakeInParam("@UpdateStaff", SqlDbType.Int, 4, m.UpdateStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 8000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
			#endregion
			
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
			
			return ret;
        }
		
        protected override MT_Material FillModel(IDataReader dr)
		{
			MT_Material m = new MT_Material();
			if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (int)dr["ID"];
			if (!string.IsNullOrEmpty(dr["Name"].ToString()))	m.Name = (string)dr["Name"];
			if (!string.IsNullOrEmpty(dr["Classify"].ToString()))	m.Classify = (int)dr["Classify"];
			if (!string.IsNullOrEmpty(dr["Code"].ToString()))	m.Code = (string)dr["Code"];
			if (!string.IsNullOrEmpty(dr["TrafficPackaging"].ToString()))	m.TrafficPackaging = (int)dr["TrafficPackaging"];
			if (!string.IsNullOrEmpty(dr["Packaging"].ToString()))	m.Packaging = (int)dr["Packaging"];
			if (!string.IsNullOrEmpty(dr["ConvertFactor"].ToString()))	m.ConvertFactor = (int)dr["ConvertFactor"];
			if (!string.IsNullOrEmpty(dr["Weight"].ToString()))	m.Weight = (decimal)dr["Weight"];
			if (!string.IsNullOrEmpty(dr["Price"].ToString()))	m.Price = (decimal)dr["Price"];
			if (!string.IsNullOrEmpty(dr["State"].ToString()))	m.State = (int)dr["State"];
			if (!string.IsNullOrEmpty(dr["ApproveFlag"].ToString()))	m.ApproveFlag = (int)dr["ApproveFlag"];
			if (!string.IsNullOrEmpty(dr["InputTime"].ToString()))	m.InputTime = (DateTime)dr["InputTime"];
			if (!string.IsNullOrEmpty(dr["InputStaff"].ToString()))	m.InputStaff = (int)dr["InputStaff"];
			if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString()))	m.UpdateTime = (DateTime)dr["UpdateTime"];
			if (!string.IsNullOrEmpty(dr["UpdateStaff"].ToString()))	m.UpdateStaff = (int)dr["UpdateStaff"];
			if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString()))	m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);
						
			return m;
		}
    }
}

