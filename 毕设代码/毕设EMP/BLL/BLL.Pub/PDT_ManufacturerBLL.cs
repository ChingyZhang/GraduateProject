
// ===================================================================
// 文件： PDT_ManufacturerDAL.cs
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
	///PDT_ManufacturerBLL业务逻辑BLL类
	/// </summary>
	public class PDT_ManufacturerBLL : BaseSimpleBLL<PDT_Manufacturer>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.Pub.PDT_ManufacturerDAL";
        private PDT_ManufacturerDAL _dal;
		
		#region 构造函数
		///<summary>
		///PDT_ManufacturerBLL
		///</summary>
		public PDT_ManufacturerBLL()
			: base(DALClassName)
		{
			_dal = (PDT_ManufacturerDAL)_DAL;
            _m = new PDT_Manufacturer(); 
		}
		
		public PDT_ManufacturerBLL(int id)
            : base(DALClassName)
        {
            _dal = (PDT_ManufacturerDAL)_DAL;
            FillModel(id);
        }

        public PDT_ManufacturerBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (PDT_ManufacturerDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
	}
}