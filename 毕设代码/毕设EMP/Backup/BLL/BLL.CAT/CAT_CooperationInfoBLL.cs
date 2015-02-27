
// ===================================================================
// 文件： CAT_CooperationInfoDAL.cs
// 项目名称：
// 创建时间：2011/1/20
// 作者:	   
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
	///CAT_CooperationInfoBLL业务逻辑BLL类
	/// </summary>
	public class CAT_CooperationInfoBLL : BaseSimpleBLL<CAT_CooperationInfo>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.CAT.CAT_CooperationInfoDAL";
        private CAT_CooperationInfoDAL _dal;
		
		#region 构造函数
		///<summary>
		///CAT_CooperationInfoBLL
		///</summary>
		public CAT_CooperationInfoBLL()
			: base(DALClassName)
		{
			_dal = (CAT_CooperationInfoDAL)_DAL;
            _m = new CAT_CooperationInfo(); 
		}
		
		public CAT_CooperationInfoBLL(int id)
            : base(DALClassName)
        {
            _dal = (CAT_CooperationInfoDAL)_DAL;
            FillModel(id);
        }

        public CAT_CooperationInfoBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (CAT_CooperationInfoDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<CAT_CooperationInfo> GetModelList(string condition)
        {
            return new CAT_CooperationInfoBLL()._GetModelList(condition);
        }
        public void DeleteByCooperationIDS(string IDS)
        {
              _dal.DeleteByCooperationIDS(IDS);
        }
		#endregion
	}
}