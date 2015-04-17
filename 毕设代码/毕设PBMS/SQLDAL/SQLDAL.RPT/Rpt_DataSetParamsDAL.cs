
// ===================================================================
// 文件： Rpt_DataSetParamsDAL.cs
// 项目名称：
// 创建时间：2010/9/25
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.RPT;


namespace MCSFramework.SQLDAL.RPT
{
	/// <summary>
	///Rpt_DataSetParams数据访问DAL类
	/// </summary>
	public class Rpt_DataSetParamsDAL : BaseSimpleDAL<Rpt_DataSetParams>
	{
		#region 构造函数
		///<summary>
		///
		///</summary>
		public Rpt_DataSetParamsDAL()
		{
			_ProcePrefix = "MCS_Reports.dbo.sp_Rpt_DataSetParams";
		}
		#endregion
		
		
		/// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(Rpt_DataSetParams m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.UniqueIdentifier, 16, m.ID),
				SQLDatabase.MakeInParam("@DataSet", SqlDbType.UniqueIdentifier, 16, m.DataSet),
				SQLDatabase.MakeInParam("@ParamName", SqlDbType.VarChar, 50, m.ParamName),
				SQLDatabase.MakeInParam("@DisplayName", SqlDbType.VarChar, 50, m.DisplayName),
				SQLDatabase.MakeInParam("@DataType", SqlDbType.Int, 4, m.DataType),
				SQLDatabase.MakeInParam("@ParamSortID", SqlDbType.Int, 4, m.ParamSortID),
				SQLDatabase.MakeInParam("@Description", SqlDbType.VarChar, 2000, m.Description),
				SQLDatabase.MakeInParam("@DefaultValue", SqlDbType.VarChar, 200, m.DefaultValue),
				SQLDatabase.MakeInParam("@Visible", SqlDbType.Char, 1, m.Visible),
				SQLDatabase.MakeInParam("@ControlType", SqlDbType.Int, 4, m.ControlType),
				SQLDatabase.MakeInParam("@DataSetSource", SqlDbType.UniqueIdentifier, 16, m.DataSetSource),
				SQLDatabase.MakeInParam("@RegularExpression", SqlDbType.VarChar, 500, m.RegularExpression),
				SQLDatabase.MakeInParam("@RelationType", SqlDbType.Int, 4, m.RelationType),
				SQLDatabase.MakeInParam("@RelationTableName", SqlDbType.VarChar, 50, m.RelationTableName),
				SQLDatabase.MakeInParam("@RelationValueField", SqlDbType.VarChar, 50, m.RelationValueField),
				SQLDatabase.MakeInParam("@RelationTextField", SqlDbType.VarChar, 50, m.RelationTextField),
				SQLDatabase.MakeInParam("@SearchPageURL", SqlDbType.VarChar, 200, m.SearchPageURL),
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
        public override int Update(Rpt_DataSetParams m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.UniqueIdentifier, 16, m.ID),
				SQLDatabase.MakeInParam("@DataSet", SqlDbType.UniqueIdentifier, 16, m.DataSet),
				SQLDatabase.MakeInParam("@ParamName", SqlDbType.VarChar, 50, m.ParamName),
				SQLDatabase.MakeInParam("@DisplayName", SqlDbType.VarChar, 50, m.DisplayName),
				SQLDatabase.MakeInParam("@DataType", SqlDbType.Int, 4, m.DataType),
				SQLDatabase.MakeInParam("@ParamSortID", SqlDbType.Int, 4, m.ParamSortID),
				SQLDatabase.MakeInParam("@Description", SqlDbType.VarChar, 2000, m.Description),
				SQLDatabase.MakeInParam("@DefaultValue", SqlDbType.VarChar, 200, m.DefaultValue),
				SQLDatabase.MakeInParam("@Visible", SqlDbType.Char, 1, m.Visible),
				SQLDatabase.MakeInParam("@ControlType", SqlDbType.Int, 4, m.ControlType),
				SQLDatabase.MakeInParam("@DataSetSource", SqlDbType.UniqueIdentifier, 16, m.DataSetSource),
				SQLDatabase.MakeInParam("@RegularExpression", SqlDbType.VarChar, 500, m.RegularExpression),
				SQLDatabase.MakeInParam("@RelationType", SqlDbType.Int, 4, m.RelationType),
				SQLDatabase.MakeInParam("@RelationTableName", SqlDbType.VarChar, 50, m.RelationTableName),
				SQLDatabase.MakeInParam("@RelationValueField", SqlDbType.VarChar, 50, m.RelationValueField),
				SQLDatabase.MakeInParam("@RelationTextField", SqlDbType.VarChar, 50, m.RelationTextField),
				SQLDatabase.MakeInParam("@SearchPageURL", SqlDbType.VarChar, 200, m.SearchPageURL),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion
			
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
			
			return ret;
        }
		
        protected override Rpt_DataSetParams FillModel(IDataReader dr)
		{
			Rpt_DataSetParams m = new Rpt_DataSetParams();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (Guid)dr["ID"];
            if (!string.IsNullOrEmpty(dr["DataSet"].ToString())) m.DataSet = (Guid)dr["DataSet"];
            if (!string.IsNullOrEmpty(dr["ParamName"].ToString())) m.ParamName = (string)dr["ParamName"];
            if (!string.IsNullOrEmpty(dr["DisplayName"].ToString())) m.DisplayName = (string)dr["DisplayName"];
            if (!string.IsNullOrEmpty(dr["DataType"].ToString())) m.DataType = (int)dr["DataType"];
            if (!string.IsNullOrEmpty(dr["ParamSortID"].ToString())) m.ParamSortID = (int)dr["ParamSortID"];
            if (!string.IsNullOrEmpty(dr["Description"].ToString())) m.Description = (string)dr["Description"];
            if (!string.IsNullOrEmpty(dr["DefaultValue"].ToString())) m.DefaultValue = (string)dr["DefaultValue"];
            if (!string.IsNullOrEmpty(dr["Visible"].ToString())) m.Visible = (string)dr["Visible"];
            if (!string.IsNullOrEmpty(dr["ControlType"].ToString())) m.ControlType = (int)dr["ControlType"];
            if (!string.IsNullOrEmpty(dr["DataSetSource"].ToString())) m.DataSetSource = (Guid)dr["DataSetSource"];
            if (!string.IsNullOrEmpty(dr["RegularExpression"].ToString())) m.RegularExpression = (string)dr["RegularExpression"];
            if (!string.IsNullOrEmpty(dr["RelationType"].ToString())) m.RelationType = (int)dr["RelationType"];
            if (!string.IsNullOrEmpty(dr["RelationTableName"].ToString())) m.RelationTableName = (string)dr["RelationTableName"];
            if (!string.IsNullOrEmpty(dr["RelationValueField"].ToString())) m.RelationValueField = (string)dr["RelationValueField"];
            if (!string.IsNullOrEmpty(dr["RelationTextField"].ToString())) m.RelationTextField = (string)dr["RelationTextField"];
            if (!string.IsNullOrEmpty(dr["SearchPageURL"].ToString())) m.SearchPageURL = (string)dr["SearchPageURL"];
            if (!string.IsNullOrEmpty(dr["InsertTime"].ToString())) m.InsertTime = (DateTime)dr["InsertTime"];
            if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString())) m.UpdateTime = (DateTime)dr["UpdateTime"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);
						
			return m;
		}
    }
}

