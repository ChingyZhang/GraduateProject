
// ===================================================================
// 文件： CM_LinkManDAL.cs
// 项目名称：
// 创建时间：2009/2/19
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
	///CM_LinkManBLL业务逻辑BLL类
	/// </summary>
	public class CM_LinkManBLL : BaseSimpleBLL<CM_LinkMan>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.CM.CM_LinkManDAL";
        private CM_LinkManDAL _dal;
		
		#region 构造函数
		///<summary>
		///CM_LinkManBLL
		///</summary>
		public CM_LinkManBLL()
			: base(DALClassName)
		{
			_dal = (CM_LinkManDAL)_DAL;
            _m = new CM_LinkMan(); 
		}
		
		public CM_LinkManBLL(int id)
            : base(DALClassName)
        {
            _dal = (CM_LinkManDAL)_DAL;
            FillModel(id);
        }

        public CM_LinkManBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (CM_LinkManDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<CM_LinkMan> GetModelList(string condition)
        {
            return new CM_LinkManBLL()._GetModelList(condition);
        }
		#endregion
	}
}