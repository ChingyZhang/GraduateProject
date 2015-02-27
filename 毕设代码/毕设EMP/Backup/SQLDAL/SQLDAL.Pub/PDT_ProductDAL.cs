
// ===================================================================
// 文件： PDT_ProductDAL.cs
// 项目名称：
// 创建时间：2008-12-22
// 作者:	   
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.Pub;
using MCSFramework.Common;

namespace MCSFramework.SQLDAL.Pub
{
	/// <summary>
	///PDT_Product数据访问DAL类
	/// </summary>
	public class PDT_ProductDAL : BaseSimpleDAL<PDT_Product>
	{
		#region 构造函数
		///<summary>
		///
		///</summary>
		public PDT_ProductDAL()
		{
			_ProcePrefix = "MCS_Pub.dbo.sp_PDT_Product";
		}
		#endregion
		
		
		/// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(PDT_Product m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@FullName", SqlDbType.VarChar, 200, m.FullName),
				SQLDatabase.MakeInParam("@ShortName", SqlDbType.VarChar, 50, m.ShortName),
				SQLDatabase.MakeInParam("@Code", SqlDbType.VarChar, 50, m.Code),
				SQLDatabase.MakeInParam("@Brand", SqlDbType.Int, 4, m.Brand),
				SQLDatabase.MakeInParam("@Classify", SqlDbType.Int, 4, m.Classify),
				SQLDatabase.MakeInParam("@Grade", SqlDbType.Int, 4, m.Grade),
				SQLDatabase.MakeInParam("@TrafficPackaging", SqlDbType.Int, 4, m.TrafficPackaging),
				SQLDatabase.MakeInParam("@Packaging", SqlDbType.Int, 4, m.Packaging),
				SQLDatabase.MakeInParam("@ConvertFactor", SqlDbType.Int, 4, m.ConvertFactor),
				SQLDatabase.MakeInParam("@SubUnit", SqlDbType.Int, 4, m.SubUnit),
				SQLDatabase.MakeInParam("@Weight", SqlDbType.Decimal, 9, m.Weight),
				SQLDatabase.MakeInParam("@FactoryPrice", SqlDbType.Decimal, 9, m.FactoryPrice),
				SQLDatabase.MakeInParam("@TradePrice", SqlDbType.Decimal, 9, m.TradePrice),
				SQLDatabase.MakeInParam("@StdPrice", SqlDbType.Decimal, 9, m.StdPrice),
				SQLDatabase.MakeInParam("@NetPrice", SqlDbType.Decimal, 9, m.NetPrice),
				SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, m.State),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@InsertTime", SqlDbType.DateTime, 8, m.InsertTime),
				SQLDatabase.MakeInParam("@InsertStaff", SqlDbType.Int, 4, m.InsertStaff),
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
        public override int Update(PDT_Product m)
        {
            SaveUpdateHistory(m);
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@FullName", SqlDbType.VarChar, 200, m.FullName),
				SQLDatabase.MakeInParam("@ShortName", SqlDbType.VarChar, 50, m.ShortName),
				SQLDatabase.MakeInParam("@Code", SqlDbType.VarChar, 50, m.Code),
				SQLDatabase.MakeInParam("@Brand", SqlDbType.Int, 4, m.Brand),
				SQLDatabase.MakeInParam("@Classify", SqlDbType.Int, 4, m.Classify),
				SQLDatabase.MakeInParam("@Grade", SqlDbType.Int, 4, m.Grade),
				SQLDatabase.MakeInParam("@TrafficPackaging", SqlDbType.Int, 4, m.TrafficPackaging),
				SQLDatabase.MakeInParam("@Packaging", SqlDbType.Int, 4, m.Packaging),
				SQLDatabase.MakeInParam("@ConvertFactor", SqlDbType.Int, 4, m.ConvertFactor),
				SQLDatabase.MakeInParam("@SubUnit", SqlDbType.Int, 4, m.SubUnit),
				SQLDatabase.MakeInParam("@Weight", SqlDbType.Decimal, 9, m.Weight),
				SQLDatabase.MakeInParam("@FactoryPrice", SqlDbType.Decimal, 9, m.FactoryPrice),
				SQLDatabase.MakeInParam("@TradePrice", SqlDbType.Decimal, 9, m.TradePrice),
				SQLDatabase.MakeInParam("@StdPrice", SqlDbType.Decimal, 9, m.StdPrice),
				SQLDatabase.MakeInParam("@NetPrice", SqlDbType.Decimal, 9, m.NetPrice),
				SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, m.State),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@InsertTime", SqlDbType.DateTime, 8, m.InsertTime),
				SQLDatabase.MakeInParam("@InsertStaff", SqlDbType.Int, 4, m.InsertStaff),
				SQLDatabase.MakeInParam("@UpdateTime", SqlDbType.DateTime, 8, m.UpdateTime),
				SQLDatabase.MakeInParam("@UpdateStaff", SqlDbType.Int, 4, m.UpdateStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 8000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
			#endregion
			
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
			
			return ret;
        }
		
        protected override PDT_Product FillModel(IDataReader dr)
		{
			PDT_Product m = new PDT_Product();
			if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (int)dr["ID"];
			if (!string.IsNullOrEmpty(dr["FullName"].ToString()))	m.FullName = (string)dr["FullName"];
			if (!string.IsNullOrEmpty(dr["ShortName"].ToString()))	m.ShortName = (string)dr["ShortName"];
			if (!string.IsNullOrEmpty(dr["Code"].ToString()))	m.Code = (string)dr["Code"];
			if (!string.IsNullOrEmpty(dr["Brand"].ToString()))	m.Brand = (int)dr["Brand"];
			if (!string.IsNullOrEmpty(dr["Classify"].ToString()))	m.Classify = (int)dr["Classify"];
			if (!string.IsNullOrEmpty(dr["Grade"].ToString()))	m.Grade = (int)dr["Grade"];
			if (!string.IsNullOrEmpty(dr["TrafficPackaging"].ToString()))	m.TrafficPackaging = (int)dr["TrafficPackaging"];
			if (!string.IsNullOrEmpty(dr["Packaging"].ToString()))	m.Packaging = (int)dr["Packaging"];
			if (!string.IsNullOrEmpty(dr["ConvertFactor"].ToString()))	m.ConvertFactor = (int)dr["ConvertFactor"];
			if (!string.IsNullOrEmpty(dr["SubUnit"].ToString()))	m.SubUnit = (int)dr["SubUnit"];
			if (!string.IsNullOrEmpty(dr["Weight"].ToString()))	m.Weight = (decimal)dr["Weight"];
			if (!string.IsNullOrEmpty(dr["FactoryPrice"].ToString()))	m.FactoryPrice = (decimal)dr["FactoryPrice"];
			if (!string.IsNullOrEmpty(dr["TradePrice"].ToString()))	m.TradePrice = (decimal)dr["TradePrice"];
			if (!string.IsNullOrEmpty(dr["StdPrice"].ToString()))	m.StdPrice = (decimal)dr["StdPrice"];
			if (!string.IsNullOrEmpty(dr["NetPrice"].ToString()))	m.NetPrice = (decimal)dr["NetPrice"];
			if (!string.IsNullOrEmpty(dr["State"].ToString()))	m.State = (int)dr["State"];
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

