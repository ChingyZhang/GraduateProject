
// ===================================================================
// 文件： PDT_ManufacturerDAL.cs
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
	///PDT_Manufacturer数据访问DAL类
	/// </summary>
	public class PDT_ManufacturerDAL : BaseSimpleDAL<PDT_Manufacturer>
	{
		#region 构造函数
		///<summary>
		///
		///</summary>
		public PDT_ManufacturerDAL()
		{
			_ProcePrefix = "MCS_Pub.dbo.sp_PDT_Manufacturer";
		}
		#endregion
		
		
		/// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(PDT_Manufacturer m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Name", SqlDbType.VarChar, 100, m.Name),
				SQLDatabase.MakeInParam("@TeleNum", SqlDbType.VarChar, 50, m.TeleNum),
				SQLDatabase.MakeInParam("@Address", SqlDbType.VarChar, 200, m.Address),
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
        public override int Update(PDT_Manufacturer m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@Name", SqlDbType.VarChar, 100, m.Name),
				SQLDatabase.MakeInParam("@TeleNum", SqlDbType.VarChar, 50, m.TeleNum),
				SQLDatabase.MakeInParam("@Address", SqlDbType.VarChar, 200, m.Address),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 200, m.Remark)
			};
			#endregion
			
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
			
			return ret;
        }
		
        protected override PDT_Manufacturer FillModel(IDataReader dr)
		{
			PDT_Manufacturer m = new PDT_Manufacturer();
			if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (int)dr["ID"];
			if (!string.IsNullOrEmpty(dr["Name"].ToString()))	m.Name = (string)dr["Name"];
			if (!string.IsNullOrEmpty(dr["TeleNum"].ToString()))	m.TeleNum = (string)dr["TeleNum"];
			if (!string.IsNullOrEmpty(dr["Address"].ToString()))	m.Address = (string)dr["Address"];
			if (!string.IsNullOrEmpty(dr["Remark"].ToString()))	m.Remark = (string)dr["Remark"];
						
			return m;
		}
    }
}

