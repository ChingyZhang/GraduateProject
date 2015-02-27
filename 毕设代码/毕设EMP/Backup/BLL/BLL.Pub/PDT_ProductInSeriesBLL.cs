
// ===================================================================
// 文件： PDT_ProductInSeriesDAL.cs
// 项目名称：
// 创建时间：2009-4-27
// 作者:	   chenli
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model.Pub;
using MCSFramework.SQLDAL.Pub;

namespace MCSFramework.BLL.Pub
{
	/// <summary>
	///PDT_ProductInSeriesBLL业务逻辑BLL类
	/// </summary>
	public class PDT_ProductInSeriesBLL : BaseSimpleBLL<PDT_ProductInSeries>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.Pub.PDT_ProductInSeriesDAL";
        private PDT_ProductInSeriesDAL _dal;
		
		#region 构造函数
		///<summary>
		///PDT_ProductInSeriesBLL
		///</summary>
		public PDT_ProductInSeriesBLL()
			: base(DALClassName)
		{
			_dal = (PDT_ProductInSeriesDAL)_DAL;
            _m = new PDT_ProductInSeries(); 
		}
		
		public PDT_ProductInSeriesBLL(int id)
            : base(DALClassName)
        {
            _dal = (PDT_ProductInSeriesDAL)_DAL;
            FillModel(id);
        }

        public PDT_ProductInSeriesBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (PDT_ProductInSeriesDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<PDT_ProductInSeries> GetModelList(string condition)
        {
            return new PDT_ProductInSeriesBLL()._GetModelList(condition);
        }
		#endregion
	}
}