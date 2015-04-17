
// ===================================================================
// 文件： Right_ActionDAL.cs
// 项目名称：
// 创建时间：2009/3/5
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
	///Right_Action数据访问DAL类
	/// </summary>
	public class Right_ActionDAL : BaseSimpleDAL<Right_Action>
	{
		#region 构造函数
		///<summary>
		///
		///</summary>
		public Right_ActionDAL()
		{
			_ProcePrefix = "MCS_SYS.dbo.sp_Right_Action";
		}
		#endregion
		
		
		/// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(Right_Action m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Code", SqlDbType.VarChar, 50, m.Code),
				SQLDatabase.MakeInParam("@Name", SqlDbType.VarChar, 200, m.Name),
				SQLDatabase.MakeInParam("@Module", SqlDbType.Int, 4, m.Module),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 200, m.Remark)
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
        public override int Update(Right_Action m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@Code", SqlDbType.VarChar, 50, m.Code),
				SQLDatabase.MakeInParam("@Name", SqlDbType.VarChar, 200, m.Name),
				SQLDatabase.MakeInParam("@Module", SqlDbType.Int, 4, m.Module),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 200, m.Remark)
			};
			#endregion
			
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
			
			return ret;
        }
		
        protected override Right_Action FillModel(IDataReader dr)
		{
			Right_Action m = new Right_Action();
			if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (int)dr["ID"];
			if (!string.IsNullOrEmpty(dr["Code"].ToString()))	m.Code = (string)dr["Code"];
			if (!string.IsNullOrEmpty(dr["Name"].ToString()))	m.Name = (string)dr["Name"];
			if (!string.IsNullOrEmpty(dr["Module"].ToString()))	m.Module = (int)dr["Module"];
			if (!string.IsNullOrEmpty(dr["Remark"].ToString()))	m.Remark = (string)dr["Remark"];
						
			return m;
		}
    }
}

