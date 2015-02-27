
// ===================================================================
// 文件： PDT_PackagingDAL.cs
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

namespace MCSFramework.BLL.Pub
{
	/// <summary>
	///PDT_PackagingBLL业务逻辑BLL类
	/// </summary>
	public class PDT_PackagingBLL : BaseSimpleBLL<PDT_Packaging>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.Pub.PDT_PackagingDAL";
        private PDT_PackagingDAL _dal;
		
		#region 构造函数
		///<summary>
		///PDT_PackagingBLL
		///</summary>
		public PDT_PackagingBLL()
			: base(DALClassName)
		{
			_dal = (PDT_PackagingDAL)_DAL;
            _m = new PDT_Packaging(); 
		}
		
		public PDT_PackagingBLL(int id)
            : base(DALClassName)
        {
            _dal = (PDT_PackagingDAL)_DAL;
            FillModel(id);
        }

        public PDT_PackagingBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (PDT_PackagingDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
	}
}