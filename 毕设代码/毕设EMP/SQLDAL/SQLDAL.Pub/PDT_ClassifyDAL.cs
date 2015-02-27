
// ===================================================================
// 文件： PDT_ClassifyDAL.cs
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


namespace MCSFramework.SQLDAL.Pub
{
	/// <summary>
	///PDT_Classify数据访问DAL类
	/// </summary>
	public class PDT_ClassifyDAL : BaseSimpleDAL<PDT_Classify>
	{
		#region 构造函数
		///<summary>
		///
		///</summary>
		public PDT_ClassifyDAL()
		{
			_ProcePrefix = "MCS_Pub.dbo.sp_PDT_Classify";
		}
		#endregion
		
		
		/// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(PDT_Classify m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Name", SqlDbType.VarChar, 50, m.Name),
				SQLDatabase.MakeInParam("@Brand", SqlDbType.Int, 4, m.Brand),
                SQLDatabase.MakeInParam("@SortID", SqlDbType.Int, 4, m.SortID)
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
        public override int Update(PDT_Classify m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@Name", SqlDbType.VarChar, 50, m.Name),
				SQLDatabase.MakeInParam("@Brand", SqlDbType.Int, 4, m.Brand),
                SQLDatabase.MakeInParam("@SortID", SqlDbType.Int, 4, m.SortID)
			};
			#endregion
			
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
			
			return ret;
        }
		
        protected override PDT_Classify FillModel(IDataReader dr)
		{
			PDT_Classify m = new PDT_Classify();
			if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (int)dr["ID"];
			if (!string.IsNullOrEmpty(dr["Name"].ToString()))	m.Name = (string)dr["Name"];
			if (!string.IsNullOrEmpty(dr["Brand"].ToString()))	m.Brand = (int)dr["Brand"];
            if (!string.IsNullOrEmpty(dr["SortID"].ToString())) m.SortID = (int)dr["SortID"];			
			return m;
		}
    }
}

