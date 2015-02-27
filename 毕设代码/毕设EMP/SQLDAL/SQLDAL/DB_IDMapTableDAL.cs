
// ===================================================================
// 文件： DB_IDMapTableDAL.cs
// 项目名称：
// 创建时间：2010/1/19
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model;


namespace MCSFramework.SQLDAL
{
	/// <summary>
	///DB_IDMapTable数据访问DAL类
	/// </summary>
	public class DB_IDMapTableDAL : BaseSimpleDAL<DB_IDMapTable>
	{
		#region 构造函数
		///<summary>
		///
		///</summary>
		public DB_IDMapTableDAL()
		{
			_ProcePrefix = "MCS_SYS.dbo.sp_DB_IDMapTable";
		}
		#endregion
		
		
		/// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(DB_IDMapTable m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@TableName", SqlDbType.VarChar, 50, m.TableName),
				SQLDatabase.MakeInParam("@IDV3", SqlDbType.Int, 4, m.IDV3),
				SQLDatabase.MakeInParam("@IDV4", SqlDbType.Int, 4, m.IDV4),
				SQLDatabase.MakeInParam("@NameV3", SqlDbType.VarChar, 50, m.NameV3),
				SQLDatabase.MakeInParam("@NameV4", SqlDbType.VarChar, 50, m.NameV4)
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
        public override int Update(DB_IDMapTable m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@TableName", SqlDbType.VarChar, 50, m.TableName),
				SQLDatabase.MakeInParam("@IDV3", SqlDbType.Int, 4, m.IDV3),
				SQLDatabase.MakeInParam("@IDV4", SqlDbType.Int, 4, m.IDV4),
				SQLDatabase.MakeInParam("@NameV3", SqlDbType.VarChar, 50, m.NameV3),
				SQLDatabase.MakeInParam("@NameV4", SqlDbType.VarChar, 50, m.NameV4)
			};
			#endregion
			
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
			
			return ret;
        }
		
        protected override DB_IDMapTable FillModel(IDataReader dr)
		{
			DB_IDMapTable m = new DB_IDMapTable();
			if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (int)dr["ID"];
			if (!string.IsNullOrEmpty(dr["TableName"].ToString()))	m.TableName = (string)dr["TableName"];
			if (!string.IsNullOrEmpty(dr["IDV3"].ToString()))	m.IDV3 = (int)dr["IDV3"];
			if (!string.IsNullOrEmpty(dr["IDV4"].ToString()))	m.IDV4 = (int)dr["IDV4"];
			if (!string.IsNullOrEmpty(dr["NameV3"].ToString()))	m.NameV3 = (string)dr["NameV3"];
			if (!string.IsNullOrEmpty(dr["NameV4"].ToString()))	m.NameV4 = (string)dr["NameV4"];
						
			return m;
		}
    }
}

