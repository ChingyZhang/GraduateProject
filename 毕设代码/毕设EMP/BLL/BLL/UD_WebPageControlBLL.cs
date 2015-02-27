
// ===================================================================
// 文件： UD_WebPageControlDAL.cs
// 项目名称：
// 创建时间：2009/3/7
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
	///UD_WebPageControlBLL业务逻辑BLL类
	/// </summary>
	public class UD_WebPageControlBLL : BaseSimpleBLL<UD_WebPageControl>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.UD_WebPageControlDAL";
        private UD_WebPageControlDAL _dal;
		
		#region 构造函数
		///<summary>
		///UD_WebPageControlBLL
		///</summary>
		public UD_WebPageControlBLL()
			: base(DALClassName)
		{
			_dal = (UD_WebPageControlDAL)_DAL;
            _m = new UD_WebPageControl(); 
		}

        public UD_WebPageControlBLL(Guid id)
            : base(DALClassName)
        {
            _dal = (UD_WebPageControlDAL)_DAL;
            FillModel(id);
        }

        public UD_WebPageControlBLL(Guid id, bool bycache)
            : base(DALClassName)
        {
            _dal = (UD_WebPageControlDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<UD_WebPageControl> GetModelList(string condition)
        {
            return new UD_WebPageControlBLL()._GetModelList(condition);
        }
		#endregion
	}
}