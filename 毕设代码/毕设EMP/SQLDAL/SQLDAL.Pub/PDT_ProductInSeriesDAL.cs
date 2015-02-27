
// ===================================================================
// 文件： PDT_ProductInSeriesDAL.cs
// 项目名称：
// 创建时间：2009-4-27
// 作者:	   chenli
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.Pub;


namespace MCSFramework.SQLDAL.Pub
{
	/// <summary>
	///PDT_ProductInSeries数据访问DAL类
	/// </summary>
	public class PDT_ProductInSeriesDAL : BaseSimpleDAL<PDT_ProductInSeries>
	{
		#region 构造函数
		///<summary>
		///
		///</summary>
		public PDT_ProductInSeriesDAL()
		{
			_ProcePrefix = "MCS_Pub.dbo.sp_PDT_ProductInSeries";
		}
		#endregion
		
		
		/// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(PDT_ProductInSeries m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, m.Product),
				SQLDatabase.MakeInParam("@Series", SqlDbType.Int, 4, m.Series)
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
        public override int Update(PDT_ProductInSeries m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, m.Product),
				SQLDatabase.MakeInParam("@Series", SqlDbType.Int, 4, m.Series)
			};
			#endregion
			
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
			
			return ret;
        }
		
        protected override PDT_ProductInSeries FillModel(IDataReader dr)
		{
			PDT_ProductInSeries m = new PDT_ProductInSeries();
			if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (int)dr["ID"];
			if (!string.IsNullOrEmpty(dr["Product"].ToString()))	m.Product = (int)dr["Product"];
			if (!string.IsNullOrEmpty(dr["Series"].ToString()))	m.Series = (int)dr["Series"];
						
			return m;
		}
    }
}

