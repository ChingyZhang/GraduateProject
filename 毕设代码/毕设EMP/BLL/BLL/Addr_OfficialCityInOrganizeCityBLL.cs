
// ===================================================================
// 文件： Addr_OfficialCityInOrganizeCityDAL.cs
// 项目名称：
// 创建时间：2010/2/26
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model;
using MCSFramework.SQLDAL;

namespace MCSFramework.BLL
{
	/// <summary>
	///Addr_OfficialCityInOrganizeCityBLL业务逻辑BLL类
	/// </summary>
	public class Addr_OfficialCityInOrganizeCityBLL : BaseSimpleBLL<Addr_OfficialCityInOrganizeCity>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.Addr_OfficialCityInOrganizeCityDAL";
        private Addr_OfficialCityInOrganizeCityDAL _dal;
		
		#region 构造函数
		///<summary>
		///Addr_OfficialCityInOrganizeCityBLL
		///</summary>
		public Addr_OfficialCityInOrganizeCityBLL()
			: base(DALClassName)
		{
			_dal = (Addr_OfficialCityInOrganizeCityDAL)_DAL;
            _m = new Addr_OfficialCityInOrganizeCity(); 
		}
		
		public Addr_OfficialCityInOrganizeCityBLL(int id)
            : base(DALClassName)
        {
            _dal = (Addr_OfficialCityInOrganizeCityDAL)_DAL;
            FillModel(id);
        }

        public Addr_OfficialCityInOrganizeCityBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (Addr_OfficialCityInOrganizeCityDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<Addr_OfficialCityInOrganizeCity> GetModelList(string condition)
        {
            return new Addr_OfficialCityInOrganizeCityBLL()._GetModelList(condition);
        }
		#endregion
	}
}