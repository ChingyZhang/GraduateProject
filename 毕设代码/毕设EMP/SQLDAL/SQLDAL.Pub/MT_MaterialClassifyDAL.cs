
// ===================================================================
// 文件： MT_MaterialClassifyDAL.cs
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
	///MT_MaterialClassify数据访问DAL类
	/// </summary>
    public class MT_MaterialClassifyDAL : BaseSimpleDAL<MT_MaterialClassify>
	{
		#region 构造函数
		///<summary>
		///
		///</summary>
		public MT_MaterialClassifyDAL()
		{
			_ProcePrefix = "MCS_Pub.dbo.sp_MT_MaterialClassify";
		}
		#endregion
		
		
		/// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(MT_MaterialClassify m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@Name", SqlDbType.VarChar, 50, m.Name),
				SQLDatabase.MakeInParam("@SuperID", SqlDbType.Int, 4, m.SuperID)
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
        public override int Update(MT_MaterialClassify m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@Name", SqlDbType.VarChar, 50, m.Name),
				SQLDatabase.MakeInParam("@SuperID", SqlDbType.Int, 4, m.SuperID)
			};
			#endregion
			
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
			
			return ret;
        }
		
        protected override MT_MaterialClassify FillModel(IDataReader dr)
		{
			MT_MaterialClassify m = new MT_MaterialClassify();
			if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (int)dr["ID"];
			if (!string.IsNullOrEmpty(dr["Name"].ToString()))	m.Name = (string)dr["Name"];
			if (!string.IsNullOrEmpty(dr["SuperID"].ToString()))	m.SuperID = (int)dr["SuperID"];
						
			return m;
		}
    }
}

