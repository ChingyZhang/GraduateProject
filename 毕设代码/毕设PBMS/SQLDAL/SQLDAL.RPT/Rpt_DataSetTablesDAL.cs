
// ===================================================================
// 文件： Rpt_DataSetTablesDAL.cs
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
	///Rpt_DataSetTables数据访问DAL类
	/// </summary>
	public class Rpt_DataSetTablesDAL : BaseSimpleDAL<Rpt_DataSetTables>
	{
		#region 构造函数
		///<summary>
		///数据集包含的数据表
		///</summary>
		public Rpt_DataSetTablesDAL()
		{
			_ProcePrefix = "MCS_Reports.dbo.sp_Rpt_DataSetTables";
		}
		#endregion
		
		
		/// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(Rpt_DataSetTables m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.UniqueIdentifier, 16, m.ID),
				SQLDatabase.MakeInParam("@DataSet", SqlDbType.UniqueIdentifier, 16, m.DataSet),
				SQLDatabase.MakeInParam("@TableID", SqlDbType.UniqueIdentifier, 16, m.TableID),
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
        public override int Update(Rpt_DataSetTables m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.UniqueIdentifier, 16, m.ID),
				SQLDatabase.MakeInParam("@DataSet", SqlDbType.UniqueIdentifier, 16, m.DataSet),
				SQLDatabase.MakeInParam("@TableID", SqlDbType.UniqueIdentifier, 16, m.TableID),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
			#endregion
			
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
			
			return ret;
        }
		
        protected override Rpt_DataSetTables FillModel(IDataReader dr)
		{
			Rpt_DataSetTables m = new Rpt_DataSetTables();
			if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (Guid)dr["ID"];
			if (!string.IsNullOrEmpty(dr["DataSet"].ToString()))	m.DataSet = (Guid)dr["DataSet"];
			if (!string.IsNullOrEmpty(dr["TableID"].ToString()))	m.TableID = (Guid)dr["TableID"];
			if (!string.IsNullOrEmpty(dr["InsertTime"].ToString()))	m.InsertTime = (DateTime)dr["InsertTime"];
			if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString()))	m.UpdateTime = (DateTime)dr["UpdateTime"];
			if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString()))	m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);
						
			return m;
		}
    }
}

