
// ===================================================================
// 文件： CM_DIMembershipDAL.cs
// 项目名称：
// 创建时间：2013/9/24
// 作者:	   chf
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
	///CM_DIMembershipBLL业务逻辑BLL类
	/// </summary>
	public class CM_DIMembershipBLL : BaseSimpleBLL<CM_DIMembership>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.CM.CM_DIMembershipDAL";
        private CM_DIMembershipDAL _dal;
		
		#region 构造函数
		///<summary>
		///CM_DIMembershipBLL
		///</summary>
		public CM_DIMembershipBLL()
			: base(DALClassName)
		{
			_dal = (CM_DIMembershipDAL)_DAL;
            _m = new CM_DIMembership(); 
		}
		
		public CM_DIMembershipBLL(int id)
            : base(DALClassName)
        {
            _dal = (CM_DIMembershipDAL)_DAL;
            FillModel(id);
        }

        public CM_DIMembershipBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (CM_DIMembershipDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<CM_DIMembership> GetModelList(string condition)
        {
            return new CM_DIMembershipBLL()._GetModelList(condition);
        }
		#endregion
        public static DataTable GetByUserName(string userName)
        {
            CM_DIMembershipDAL dal = (CM_DIMembershipDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetByUserName(userName);
        }
	}

   
}