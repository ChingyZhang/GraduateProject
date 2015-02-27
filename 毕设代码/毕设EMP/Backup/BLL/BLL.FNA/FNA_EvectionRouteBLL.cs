
// ===================================================================
// 文件： FNA_EvectionRouteDAL.cs
// 项目名称：
// 创建时间：2009/2/22
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model.FNA;
using MCSFramework.SQLDAL.FNA;

namespace MCSFramework.BLL.FNA
{
	/// <summary>
	///FNA_EvectionRouteBLL业务逻辑BLL类
	/// </summary>
	public class FNA_EvectionRouteBLL : BaseSimpleBLL<FNA_EvectionRoute>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.FNA.FNA_EvectionRouteDAL";
        private FNA_EvectionRouteDAL _dal;
		
		#region 构造函数
		///<summary>
		///FNA_EvectionRouteBLL
		///</summary>
		public FNA_EvectionRouteBLL()
			: base(DALClassName)
		{
			_dal = (FNA_EvectionRouteDAL)_DAL;
            _m = new FNA_EvectionRoute(); 
		}
		
		public FNA_EvectionRouteBLL(int id)
            : base(DALClassName)
        {
            _dal = (FNA_EvectionRouteDAL)_DAL;
            FillModel(id);
        }

        public FNA_EvectionRouteBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (FNA_EvectionRouteDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<FNA_EvectionRoute> GetModelList(string condition)
        {
            return new FNA_EvectionRouteBLL()._GetModelList(condition);
        }
		#endregion
	}
}