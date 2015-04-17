
// ===================================================================
// 文件： CM_RTChannel_SYSDAL.cs
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
	///CM_RTChannel_SYSBLL业务逻辑BLL类
	/// </summary>
	public class CM_RTChannel_SYSBLL : BaseSimpleBLL<CM_RTChannel_SYS>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.CM.CM_RTChannel_SYSDAL";
        private CM_RTChannel_SYSDAL _dal;
		
		#region 构造函数
		///<summary>
		///CM_RTChannel_SYSBLL
		///</summary>
		public CM_RTChannel_SYSBLL()
			: base(DALClassName)
		{
			_dal = (CM_RTChannel_SYSDAL)_DAL;
            _m = new CM_RTChannel_SYS(); 
		}
		
		public CM_RTChannel_SYSBLL(int id)
            : base(DALClassName)
        {
            _dal = (CM_RTChannel_SYSDAL)_DAL;
            FillModel(id);
        }

        public CM_RTChannel_SYSBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (CM_RTChannel_SYSDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<CM_RTChannel_SYS> GetModelList(string condition)
        {
            return new CM_RTChannel_SYSBLL()._GetModelList(condition);
        }
		#endregion
	}
}