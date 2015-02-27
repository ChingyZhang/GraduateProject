
// ===================================================================
// 文件： MT_MaterialBLL.cs
// 项目名称：
// 创建时间：2008-12-22
// 作者:	   yangwei
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model;
using MCSFramework.Model.Pub;
using MCSFramework.SQLDAL.Pub;

namespace MCSFramework.BLL.Pub
{
	/// <summary>
	///MaterialBLL业务逻辑BLL类
	/// </summary>
	public class MT_MaterialBLL : BaseSimpleBLL<MT_Material>
	{
        private static string DALClassName = "MCSFramework.SQLDAL.Pub.MT_MaterialDAL";
        private MT_MaterialDAL _dal;
		
		#region 构造函数
		///<summary>
		///MaterialBLL
		///</summary>
		public MT_MaterialBLL()
			: base(DALClassName)
		{
			_dal = (MT_MaterialDAL)_DAL;
            _m = new MT_Material(); 
		}
		
		public MT_MaterialBLL(int id)
            : base(DALClassName)
        {
            _dal = (MT_MaterialDAL)_DAL;
            FillModel(id);
        }

        public MT_MaterialBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (MT_MaterialDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
	}
}