
// ===================================================================
// 文件： Addr_OfficialCityDAL.cs
// 项目名称：
// 创建时间：2008-12-17
// 作者:	   
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model;
using MCSFramework.SQLDAL;
using System.Collections.Generic;

namespace MCSFramework.BLL
{
	/// <summary>
	///Addr_OfficialCityBLL业务逻辑BLL类
	/// </summary>
	public class Addr_OfficialCityBLL : BaseSimpleBLL<Addr_OfficialCity>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.Addr_OfficialCityDAL";
        private Addr_OfficialCityDAL _dal;
		
		#region 构造函数
		///<summary>
		///Addr_OfficialCityBLL
		///</summary>
		public Addr_OfficialCityBLL()
			: base(DALClassName)
		{
			_dal = (Addr_OfficialCityDAL)_DAL;
            _m = new Addr_OfficialCity(); 
		}
		
		public Addr_OfficialCityBLL(int id)
            : base(DALClassName)
        {
            _dal = (Addr_OfficialCityDAL)_DAL;
            FillModel(id);
        }

        public Addr_OfficialCityBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (Addr_OfficialCityDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion

        #region	静态GetModelList方法
        public static IList<Addr_OfficialCity> GetModelList(string condition)
        {
            return new Addr_OfficialCityBLL()._GetModelList(condition);
        }
        #endregion

        /// <summary>
        /// 获取所有省份，城市
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllOfficialCity()
        {
            ///可以引入缓存
            Addr_OfficialCityDAL dal = (Addr_OfficialCityDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetAllOfficialCity();
        }
        
        //获取所有下级行政城市ids
        public string GetAllChildNodeIDs()
        {
            DataTable dt = TreeTableBLL.GetAllChildByNodes("MCS_SYS.dbo.Addr_OfficialCity", "ID", "SuperID", _m.ID.ToString());
            string ids = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (ids != "") ids += ",";
                ids += dt.Rows[i]["ID"];
            }

            return ids;
        }

        public string GetAllSuperNodeIDs()
        {
            DataTable dt = TreeTableBLL.GetAllSuperNodeIDs("MCS_SYS.dbo.Addr_OfficialCity", "SuperID", "ID", _m.ID.ToString());
            string ids = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (ids != "") ids += ",";
                ids += dt.Rows[i]["SuperID"];
            }

            return ids;
        }

        //获取所有下级行政城市
        public DataTable GetAllChildNode()
        {
            DataTable dt = TreeTableBLL.GetAllChildByNodes("MCS_SYS.dbo.Addr_OfficialCity", "ID", "SuperID", _m.ID.ToString());
            return dt;
        }

        /// <summary>
        /// 根据电话号码、手机号码、邮编获取所属行政城市
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static Addr_OfficialCity GetCityByTeleNumOrPostCode(string number)
        {
            Addr_OfficialCityDAL dal = (Addr_OfficialCityDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetCityByTeleNumOrPostCode(number);
        }

        /// <summary>
        /// 根据行政城市获取对应的v3的ID
        /// </summary>
        /// <param name="officialCity"></param>
        /// <returns></returns>
        public static int GetOfficialCityRelate_V3(int officialCity)
        {
            return Addr_OfficialCityDAL.GetOfficialCityRelate_V3(officialCity);
        }

          /// <summary>
        /// 根据v3的ID获取对应的行政城市的ID
        /// </summary>
        /// <param name="officialCity"></param>
        /// <returns></returns>
        public static int GetOfficialCityBy_V3ID(int Level, int V3_ID)
        {
            return Addr_OfficialCityDAL.GetOfficialCityBy_V3ID(Level, V3_ID);
        }

        
	} 

}