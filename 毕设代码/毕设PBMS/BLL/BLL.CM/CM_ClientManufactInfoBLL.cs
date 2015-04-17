
// ===================================================================
// 文件： CM_ClientManufactInfoDAL.cs
// 项目名称：
// 创建时间：2015/1/27
// 作者:	   
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
	///CM_ClientManufactInfoBLL业务逻辑BLL类
	/// </summary>
	public class CM_ClientManufactInfoBLL : BaseSimpleBLL<CM_ClientManufactInfo>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.CM.CM_ClientManufactInfoDAL";
        private CM_ClientManufactInfoDAL _dal;
		
		#region 构造函数
		///<summary>
		///CM_ClientManufactInfoBLL
		///</summary>
		public CM_ClientManufactInfoBLL()
			: base(DALClassName)
		{
			_dal = (CM_ClientManufactInfoDAL)_DAL;
            _m = new CM_ClientManufactInfo(); 
		}
		
		public CM_ClientManufactInfoBLL(int id)
            : base(DALClassName)
        {
            _dal = (CM_ClientManufactInfoDAL)_DAL;
            FillModel(id);
        }

        public CM_ClientManufactInfoBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (CM_ClientManufactInfoDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<CM_ClientManufactInfo> GetModelList(string condition)
        {
            return new CM_ClientManufactInfoBLL()._GetModelList(condition);
        }
		#endregion

        /// <summary>
        /// 审核客户的厂商管理信息
        /// </summary>
        /// <param name="Client"></param>
        /// <param name="Manufacturer"></param>
        /// <param name="OpStaff"></param>
        /// <param name="State"></param>
        /// <returns></returns>
        public static int Approve(int Client, int Manufacturer, int OpStaff, int State)
        {
            CM_ClientManufactInfoDAL dal = (CM_ClientManufactInfoDAL)DataAccess.CreateObject(DALClassName);

            return dal.Approve(Client, Manufacturer, OpStaff, State);
        }
	}
}