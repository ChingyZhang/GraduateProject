
// ===================================================================
// 文件： AC_AccountTitleInFeeTypeDAL.cs
// 项目名称：
// 创建时间：2010/7/20
// 作者:	   Shen Gang
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
	///AC_AccountTitleInFeeTypeBLL业务逻辑BLL类
	/// </summary>
	public class AC_AccountTitleInFeeTypeBLL : BaseSimpleBLL<AC_AccountTitleInFeeType>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.Pub.AC_AccountTitleInFeeTypeDAL";
        private AC_AccountTitleInFeeTypeDAL _dal;
		
		#region 构造函数
		///<summary>
		///AC_AccountTitleInFeeTypeBLL
		///</summary>
		public AC_AccountTitleInFeeTypeBLL()
			: base(DALClassName)
		{
			_dal = (AC_AccountTitleInFeeTypeDAL)_DAL;
            _m = new AC_AccountTitleInFeeType(); 
		}
		
		public AC_AccountTitleInFeeTypeBLL(int id)
            : base(DALClassName)
        {
            _dal = (AC_AccountTitleInFeeTypeDAL)_DAL;
            FillModel(id);
        }

        public AC_AccountTitleInFeeTypeBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (AC_AccountTitleInFeeTypeDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<AC_AccountTitleInFeeType> GetModelList(string condition)
        {
            return new AC_AccountTitleInFeeTypeBLL()._GetModelList(condition);
        }
		#endregion
	}
}