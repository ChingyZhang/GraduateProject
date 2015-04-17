
// ===================================================================
// 文件： Rpt_DataSetFieldsDAL.cs
// 项目名称：
// 创建时间：2010/9/26
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
	///Rpt_DataSetFields数据访问DAL类
	/// </summary>
	public class Rpt_DataSetFieldsDAL : BaseSimpleDAL<Rpt_DataSetFields>
	{
		#region 构造函数
		///<summary>
		///
		///</summary>
		public Rpt_DataSetFieldsDAL()
		{
			_ProcePrefix = "MCS_Reports.dbo.sp_Rpt_DataSetFields";
		}
		#endregion
		
		
		/// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(Rpt_DataSetFields m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.UniqueIdentifier, 16, m.ID),
				SQLDatabase.MakeInParam("@DataSet", SqlDbType.UniqueIdentifier, 16, m.DataSet),
				SQLDatabase.MakeInParam("@FieldID", SqlDbType.UniqueIdentifier, 16, m.FieldID),
				SQLDatabase.MakeInParam("@FieldName", SqlDbType.VarChar, 50, m.FieldName),
				SQLDatabase.MakeInParam("@DisplayName", SqlDbType.VarChar, 50, m.DisplayName),
				SQLDatabase.MakeInParam("@DisplayMode", SqlDbType.Int, 4, m.DisplayMode),
				SQLDatabase.MakeInParam("@TreeLevel", SqlDbType.Int, 4, m.TreeLevel),
				SQLDatabase.MakeInParam("@ColumnSortID", SqlDbType.Int, 4, m.ColumnSortID),
				SQLDatabase.MakeInParam("@DataType", SqlDbType.Int, 4, m.DataType),
				SQLDatabase.MakeInParam("@IsComputeField", SqlDbType.Char, 1, m.IsComputeField),
				SQLDatabase.MakeInParam("@Expression", SqlDbType.VarChar, 500, m.Expression),
				SQLDatabase.MakeInParam("@Description", SqlDbType.VarChar, 500, m.Description),
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
        public override int Update(Rpt_DataSetFields m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.UniqueIdentifier, 16, m.ID),
				SQLDatabase.MakeInParam("@DataSet", SqlDbType.UniqueIdentifier, 16, m.DataSet),
				SQLDatabase.MakeInParam("@FieldID", SqlDbType.UniqueIdentifier, 16, m.FieldID),
				SQLDatabase.MakeInParam("@FieldName", SqlDbType.VarChar, 50, m.FieldName),
				SQLDatabase.MakeInParam("@DisplayName", SqlDbType.VarChar, 50, m.DisplayName),
				SQLDatabase.MakeInParam("@DisplayMode", SqlDbType.Int, 4, m.DisplayMode),
				SQLDatabase.MakeInParam("@TreeLevel", SqlDbType.Int, 4, m.TreeLevel),
				SQLDatabase.MakeInParam("@ColumnSortID", SqlDbType.Int, 4, m.ColumnSortID),
				SQLDatabase.MakeInParam("@DataType", SqlDbType.Int, 4, m.DataType),
				SQLDatabase.MakeInParam("@IsComputeField", SqlDbType.Char, 1, m.IsComputeField),
				SQLDatabase.MakeInParam("@Expression", SqlDbType.VarChar, 500, m.Expression),
				SQLDatabase.MakeInParam("@Description", SqlDbType.VarChar, 500, m.Description),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion
			
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
			
			return ret;
        }
		
        protected override Rpt_DataSetFields FillModel(IDataReader dr)
		{
			Rpt_DataSetFields m = new Rpt_DataSetFields();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (Guid)dr["ID"];
            if (!string.IsNullOrEmpty(dr["DataSet"].ToString())) m.DataSet = (Guid)dr["DataSet"];
            if (!string.IsNullOrEmpty(dr["FieldID"].ToString())) m.FieldID = (Guid)dr["FieldID"];
            if (!string.IsNullOrEmpty(dr["FieldName"].ToString())) m.FieldName = (string)dr["FieldName"];
            if (!string.IsNullOrEmpty(dr["DisplayName"].ToString())) m.DisplayName = (string)dr["DisplayName"];
            if (!string.IsNullOrEmpty(dr["DisplayMode"].ToString())) m.DisplayMode = (int)dr["DisplayMode"];
            if (!string.IsNullOrEmpty(dr["TreeLevel"].ToString())) m.TreeLevel = (int)dr["TreeLevel"];
            if (!string.IsNullOrEmpty(dr["ColumnSortID"].ToString())) m.ColumnSortID = (int)dr["ColumnSortID"];
            if (!string.IsNullOrEmpty(dr["DataType"].ToString())) m.DataType = (int)dr["DataType"];
            if (!string.IsNullOrEmpty(dr["IsComputeField"].ToString())) m.IsComputeField = (string)dr["IsComputeField"];
            if (!string.IsNullOrEmpty(dr["Expression"].ToString())) m.Expression = (string)dr["Expression"];
            if (!string.IsNullOrEmpty(dr["Description"].ToString())) m.Description = (string)dr["Description"];
            if (!string.IsNullOrEmpty(dr["InsertTime"].ToString())) m.InsertTime = (DateTime)dr["InsertTime"];
            if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString())) m.UpdateTime = (DateTime)dr["UpdateTime"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);
						
			return m;
		}
    }
}

