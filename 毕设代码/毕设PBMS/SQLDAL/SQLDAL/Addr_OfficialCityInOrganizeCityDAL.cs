
// ===================================================================
// 文件： Addr_OfficialCityInOrganizeCityDAL.cs
// 项目名称：
// 创建时间：2010/2/26
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
	///Addr_OfficialCityInOrganizeCity数据访问DAL类
	/// </summary>
	public class Addr_OfficialCityInOrganizeCityDAL : BaseSimpleDAL<Addr_OfficialCityInOrganizeCity>
	{
		#region 构造函数
		///<summary>
		///
		///</summary>
		public Addr_OfficialCityInOrganizeCityDAL()
		{
			_ProcePrefix = "MCS_SYS.dbo.sp_Addr_OfficialCityInOrganizeCity";
		}
		#endregion
		
		
		/// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(Addr_OfficialCityInOrganizeCity m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, m.OrganizeCity),
				SQLDatabase.MakeInParam("@OfficialCity", SqlDbType.Int, 4, m.OfficialCity)
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
        public override int Update(Addr_OfficialCityInOrganizeCity m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, m.OrganizeCity),
				SQLDatabase.MakeInParam("@OfficialCity", SqlDbType.Int, 4, m.OfficialCity)
			};
			#endregion
			
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
			
			return ret;
        }
		
        protected override Addr_OfficialCityInOrganizeCity FillModel(IDataReader dr)
		{
			Addr_OfficialCityInOrganizeCity m = new Addr_OfficialCityInOrganizeCity();
			if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (int)dr["ID"];
			if (!string.IsNullOrEmpty(dr["OrganizeCity"].ToString()))	m.OrganizeCity = (int)dr["OrganizeCity"];
			if (!string.IsNullOrEmpty(dr["OfficialCity"].ToString()))	m.OfficialCity = (int)dr["OfficialCity"];
						
			return m;
		}
    }
}

