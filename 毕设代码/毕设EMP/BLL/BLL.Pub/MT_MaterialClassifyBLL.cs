
// ===================================================================
// 文件： MT_MaterialClassifyBLL.cs
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
using MCSFramework.BLL.Pub;
using MCSFramework.Model;
using MCSFramework.Model.Pub;
using MCSFramework.SQLDAL.Pub;

namespace MCSFramework.BLL.Pub
{
	/// <summary>
    ///MT_MaterialClassifyDAL业务逻辑BLL类
	/// </summary>
	public class MT_MaterialClassifyBLL : BaseSimpleBLL<MT_MaterialClassify>
	{
        private static string DALClassName = "MCSFramework.SQLDAL.Pub.MT_MaterialClassifyDAL";
        private MT_MaterialClassifyDAL _dal;
		
		#region 构造函数
		///<summary>
		///MT_MaterialClassifyBLL
		///</summary>
		public MT_MaterialClassifyBLL()
			: base(DALClassName)
		{
			_dal = (MT_MaterialClassifyDAL)_DAL;
            _m = new MT_MaterialClassify(); 
		}
		
		public MT_MaterialClassifyBLL(int id)
            : base(DALClassName)
        {
            _dal = (MT_MaterialClassifyDAL)_DAL;
            FillModel(id);
        }

        public MT_MaterialClassifyBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (MT_MaterialClassifyDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
	}
}