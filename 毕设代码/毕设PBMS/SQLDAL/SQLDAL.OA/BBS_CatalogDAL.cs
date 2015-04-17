
// ===================================================================
// 文件： BBS_CatalogDAL.cs
// 项目名称：
// 创建时间：2009-3-16
// 作者:	   cl
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.OA;
using MCSFramework.Common;


namespace MCSFramework.SQLDAL.OA
{
	/// <summary>
	///BBS_Catalog数据访问DAL类
	/// </summary>
	public class BBS_CatalogDAL : BaseSimpleDAL<BBS_Catalog>
	{
		#region 构造函数
		///<summary>
		///
		///</summary>
		public BBS_CatalogDAL()
		{
			_ProcePrefix = "MCS_OA.dbo.sp_BBS_Catalog";
		}
		#endregion
		
		
		/// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(BBS_Catalog m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Name", SqlDbType.VarChar, 200, m.Name),
				SQLDatabase.MakeInParam("@Description", SqlDbType.VarChar, 2000, m.Description),
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
        public override int Update(BBS_Catalog m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@Name", SqlDbType.VarChar, 200, m.Name),
				SQLDatabase.MakeInParam("@Description", SqlDbType.VarChar, 2000, m.Description),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 8000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
			#endregion
			
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
			
			return ret;
        }
		
        protected override BBS_Catalog FillModel(IDataReader dr)
		{
			BBS_Catalog m = new BBS_Catalog();
			if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (int)dr["ID"];
			if (!string.IsNullOrEmpty(dr["Name"].ToString()))	m.Name = (string)dr["Name"];
			if (!string.IsNullOrEmpty(dr["Description"].ToString()))	m.Description = (string)dr["Description"];
			if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString()))	m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);
						
			return m;
		}

        public DataTable GetAllCatalog(string condition)
        {
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Condition", SqlDbType.NVarChar, 2000,condition)
             };
            SqlDataReader dr = null;

            SQLDatabase.RunProc(_ProcePrefix + "_GetByCondition",prams , out dr);

            return Tools.ConvertDataReaderToDataTable(dr);
        }

        public int DeleteCatalog(int id)
        {
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4,id)
             };

           return  SQLDatabase.RunProc(_ProcePrefix + "_DeleteCatalog", prams);
        }

    }
}

