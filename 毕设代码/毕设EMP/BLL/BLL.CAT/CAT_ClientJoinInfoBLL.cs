
// ===================================================================
// 文件： CAT_ClientJoinInfoDAL.cs
// 项目名称：
// 创建时间：2009/11/29
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model.CAT;
using MCSFramework.SQLDAL.CAT;

namespace MCSFramework.BLL.CAT
{
	/// <summary>
	///CAT_ClientJoinInfoBLL业务逻辑BLL类
	/// </summary>
	public class CAT_ClientJoinInfoBLL : BaseSimpleBLL<CAT_ClientJoinInfo>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.CAT.CAT_ClientJoinInfoDAL";
        private CAT_ClientJoinInfoDAL _dal;
		
		#region 构造函数
		///<summary>
		///CAT_ClientJoinInfoBLL
		///</summary>
		public CAT_ClientJoinInfoBLL()
			: base(DALClassName)
		{
			_dal = (CAT_ClientJoinInfoDAL)_DAL;
            _m = new CAT_ClientJoinInfo(); 
		}
		
		public CAT_ClientJoinInfoBLL(int id)
            : base(DALClassName)
        {
            _dal = (CAT_ClientJoinInfoDAL)_DAL;
            FillModel(id);
        }

        public CAT_ClientJoinInfoBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (CAT_ClientJoinInfoDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<CAT_ClientJoinInfo> GetModelList(string condition)
        {
            return new CAT_ClientJoinInfoBLL()._GetModelList(condition);
        }
		#endregion        
	}
}