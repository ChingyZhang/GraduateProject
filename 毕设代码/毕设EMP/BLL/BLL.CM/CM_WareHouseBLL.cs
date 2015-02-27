
// ===================================================================
// 文件： CM_WareHouseDAL.cs
// 项目名称：
// 创建时间：2012-7-21
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model.CM;
using MCSFramework.SQLDAL.CM;

namespace MCSFramework.BLL.CM
{
	/// <summary>
	///CM_WareHouseBLL业务逻辑BLL类
	/// </summary>
	public class CM_WareHouseBLL : BaseSimpleBLL<CM_WareHouse>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.CM.CM_WareHouseDAL";
        private CM_WareHouseDAL _dal;
		
		#region 构造函数
		///<summary>
		///CM_WareHouseBLL
		///</summary>
		public CM_WareHouseBLL()
			: base(DALClassName)
		{
			_dal = (CM_WareHouseDAL)_DAL;
            _m = new CM_WareHouse(); 
		}
		
		public CM_WareHouseBLL(int id)
            : base(DALClassName)
        {
            _dal = (CM_WareHouseDAL)_DAL;
            FillModel(id);
        }

        public CM_WareHouseBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (CM_WareHouseDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<CM_WareHouse> GetModelList(string condition)
        {
            return new CM_WareHouseBLL()._GetModelList(condition);
        }
		#endregion

        /// <summary>
        /// 获取指定客户的仓库列表
        /// </summary>
        /// <param name="Client"></param>
        /// <returns></returns>
        public static IList<CM_WareHouse> GetByClient(int Client)
        {
            return GetModelList("Client = " + Client.ToString());
        }

        public static IList<CM_WareHouse> GetEnbaledByClient(int Client)
        {
            return GetModelList("Client = " + Client.ToString() + " AND ActiveState = 1");
        }
	}
}