
// ===================================================================
// 文件： CM_DIUsersDAL.cs
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
	///CM_DIUsersBLL业务逻辑BLL类
	/// </summary>
	public class CM_DIUsersBLL : BaseSimpleBLL<CM_DIUsers>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.CM.CM_DIUsersDAL";
        private CM_DIUsersDAL _dal;
		
		#region 构造函数
		///<summary>
		///CM_DIUsersBLL
		///</summary>
		public CM_DIUsersBLL()
			: base(DALClassName)
		{
			_dal = (CM_DIUsersDAL)_DAL;
            _m = new CM_DIUsers(); 
		}
		
		public CM_DIUsersBLL(int id)
            : base(DALClassName)
        {
            _dal = (CM_DIUsersDAL)_DAL;
            FillModel(id);
        }

        public CM_DIUsersBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (CM_DIUsersDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<CM_DIUsers> GetModelList(string condition)
        {
            return new CM_DIUsersBLL()._GetModelList(condition);
        }
		#endregion
	}
}