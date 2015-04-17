
// ===================================================================
// 文件： Rpt_DataSetTableRelationsDAL.cs
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
	///Rpt_DataSetTableRelations数据访问DAL类
	/// </summary>
	public class Rpt_DataSetTableRelationsDAL : BaseSimpleDAL<Rpt_DataSetTableRelations>
	{
		#region 构造函数
		///<summary>
		///
		///</summary>
		public Rpt_DataSetTableRelationsDAL()
		{
			_ProcePrefix = "MCS_Reports.dbo.sp_Rpt_DataSetTableRelations";
		}
		#endregion
		
		
		/// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(Rpt_DataSetTableRelations m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.UniqueIdentifier, 16, m.ID),
				SQLDatabase.MakeInParam("@DataSet", SqlDbType.UniqueIdentifier, 16, m.DataSet),
				SQLDatabase.MakeInParam("@ParentTableID", SqlDbType.UniqueIdentifier, 16, m.ParentTableID),
				SQLDatabase.MakeInParam("@ParentFieldID", SqlDbType.UniqueIdentifier, 16, m.ParentFieldID),
				SQLDatabase.MakeInParam("@ChildTableID", SqlDbType.UniqueIdentifier, 16, m.ChildTableID),
				SQLDatabase.MakeInParam("@ChildFieldID", SqlDbType.UniqueIdentifier, 16, m.ChildFieldID),
				SQLDatabase.MakeInParam("@JoinMode", SqlDbType.VarChar, 20, m.JoinMode),
				SQLDatabase.MakeInParam("@RelationCondition", SqlDbType.VarChar, 500, m.RelationCondition),
				SQLDatabase.MakeInParam("@SortID", SqlDbType.Int, 4, m.SortID),
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
        public override int Update(Rpt_DataSetTableRelations m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.UniqueIdentifier, 16, m.ID),
				SQLDatabase.MakeInParam("@DataSet", SqlDbType.UniqueIdentifier, 16, m.DataSet),
				SQLDatabase.MakeInParam("@ParentTableID", SqlDbType.UniqueIdentifier, 16, m.ParentTableID),
				SQLDatabase.MakeInParam("@ParentFieldID", SqlDbType.UniqueIdentifier, 16, m.ParentFieldID),
				SQLDatabase.MakeInParam("@ChildTableID", SqlDbType.UniqueIdentifier, 16, m.ChildTableID),
				SQLDatabase.MakeInParam("@ChildFieldID", SqlDbType.UniqueIdentifier, 16, m.ChildFieldID),
				SQLDatabase.MakeInParam("@JoinMode", SqlDbType.VarChar, 20, m.JoinMode),
				SQLDatabase.MakeInParam("@RelationCondition", SqlDbType.VarChar, 500, m.RelationCondition),
				SQLDatabase.MakeInParam("@SortID", SqlDbType.Int, 4, m.SortID),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
			#endregion
			
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
			
			return ret;
        }
		
        protected override Rpt_DataSetTableRelations FillModel(IDataReader dr)
		{
			Rpt_DataSetTableRelations m = new Rpt_DataSetTableRelations();
			if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (Guid)dr["ID"];
			if (!string.IsNullOrEmpty(dr["DataSet"].ToString()))	m.DataSet = (Guid)dr["DataSet"];
			if (!string.IsNullOrEmpty(dr["ParentTableID"].ToString()))	m.ParentTableID = (Guid)dr["ParentTableID"];
			if (!string.IsNullOrEmpty(dr["ParentFieldID"].ToString()))	m.ParentFieldID = (Guid)dr["ParentFieldID"];
			if (!string.IsNullOrEmpty(dr["ChildTableID"].ToString()))	m.ChildTableID = (Guid)dr["ChildTableID"];
			if (!string.IsNullOrEmpty(dr["ChildFieldID"].ToString()))	m.ChildFieldID = (Guid)dr["ChildFieldID"];
			if (!string.IsNullOrEmpty(dr["JoinMode"].ToString()))	m.JoinMode = (string)dr["JoinMode"];
			if (!string.IsNullOrEmpty(dr["RelationCondition"].ToString()))	m.RelationCondition = (string)dr["RelationCondition"];
			if (!string.IsNullOrEmpty(dr["SortID"].ToString()))	m.SortID = (int)dr["SortID"];
			if (!string.IsNullOrEmpty(dr["InsertTime"].ToString()))	m.InsertTime = (DateTime)dr["InsertTime"];
			if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString()))	m.UpdateTime = (DateTime)dr["UpdateTime"];
			if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString()))	m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);
						
			return m;
		}
    }
}

