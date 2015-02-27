
// ===================================================================
// 文件： PDT_BrandDAL.cs
// 项目名称：
// 创建时间：2008-12-22
// 作者:	   
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model.Pub;
using MCSFramework.SQLDAL.Pub;
using System.Collections.Generic;

namespace MCSFramework.BLL.Pub
{
	/// <summary>
	///PDT_BrandBLL业务逻辑BLL类
	/// </summary>
	public class PDT_BrandBLL : BaseSimpleBLL<PDT_Brand>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.Pub.PDT_BrandDAL";
        private PDT_BrandDAL _dal;
		
		#region 构造函数
		///<summary>
		///PDT_BrandBLL
		///</summary>
		public PDT_BrandBLL()
			: base(DALClassName)
		{
			_dal = (PDT_BrandDAL)_DAL;
            _m = new PDT_Brand(); 
		}
		
		public PDT_BrandBLL(int id)
            : base(DALClassName)
        {
            _dal = (PDT_BrandDAL)_DAL;
            FillModel(id);
        }

        public PDT_BrandBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (PDT_BrandDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion

        public static IList<PDT_Brand> GetModelList(string condition)
        {
            PDT_BrandBLL b = new PDT_BrandBLL();
            return b._GetModelList(condition);
        }
	}
}