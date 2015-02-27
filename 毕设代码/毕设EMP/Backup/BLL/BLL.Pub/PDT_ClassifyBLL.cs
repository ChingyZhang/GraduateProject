
// ===================================================================
// 文件： PDT_ClassifyDAL.cs
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
	///PDT_ClassifyBLL业务逻辑BLL类
	/// </summary>
	public class PDT_ClassifyBLL : BaseSimpleBLL<PDT_Classify>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.Pub.PDT_ClassifyDAL";
        private PDT_ClassifyDAL _dal;
		
		#region 构造函数
		///<summary>
		///PDT_ClassifyBLL
		///</summary>
		public PDT_ClassifyBLL()
			: base(DALClassName)
		{
			_dal = (PDT_ClassifyDAL)_DAL;
            _m = new PDT_Classify(); 
		}
		
		public PDT_ClassifyBLL(int id)
            : base(DALClassName)
        {
            _dal = (PDT_ClassifyDAL)_DAL;
            FillModel(id);
        }

        public PDT_ClassifyBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (PDT_ClassifyDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion

        public static IList<PDT_Classify> GetModelList(string condition)
        {
            PDT_ClassifyBLL b = new PDT_ClassifyBLL();
            return b._GetModelList(condition);
        }
	}
}