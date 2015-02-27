
// ===================================================================
// 文件： Right_ActionDAL.cs
// 项目名称：
// 创建时间：2009/3/5
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
	///Right_ActionBLL业务逻辑BLL类
	/// </summary>
	public class Right_ActionBLL : BaseSimpleBLL<Right_Action>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.Right_ActionDAL";
        private Right_ActionDAL _dal;
		
		#region 构造函数
		///<summary>
		///Right_ActionBLL
		///</summary>
		public Right_ActionBLL()
			: base(DALClassName)
		{
			_dal = (Right_ActionDAL)_DAL;
            _m = new Right_Action(); 
		}
		
		public Right_ActionBLL(int id)
            : base(DALClassName)
        {
            _dal = (Right_ActionDAL)_DAL;
            FillModel(id);
        }

        public Right_ActionBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (Right_ActionDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<Right_Action> GetModelList(string condition)
        {
            return new Right_ActionBLL()._GetModelList(condition);
        }
		#endregion
	}
}