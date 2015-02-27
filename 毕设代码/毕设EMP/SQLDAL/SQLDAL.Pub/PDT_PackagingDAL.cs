
// ===================================================================
// 文件： PDT_PackagingDAL.cs
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
	///PDT_Packaging数据访问DAL类
	/// </summary>
	public class PDT_PackagingDAL : BaseSimpleDAL<PDT_Packaging>
	{
		#region 构造函数
		///<summary>
		///
		///</summary>
		public PDT_PackagingDAL()
		{
			_ProcePrefix = "MCS_Pub.dbo.sp_PDT_Packaging";
		}
		#endregion
		
		
		/// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(PDT_Packaging m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
                //SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@Name", SqlDbType.VarChar, 50, m.Name),
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
        public override int Update(PDT_Packaging m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@Name", SqlDbType.VarChar, 50, m.Name),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 200, m.Remark)
			};
			#endregion
			
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
			
			return ret;
        }
		
        protected override PDT_Packaging FillModel(IDataReader dr)
		{
			PDT_Packaging m = new PDT_Packaging();
			if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (int)dr["ID"];
			if (!string.IsNullOrEmpty(dr["Name"].ToString()))	m.Name = (string)dr["Name"];
			if (!string.IsNullOrEmpty(dr["Remark"].ToString()))	m.Remark = (string)dr["Remark"];
						
			return m;
		}
    }
}

