
// ===================================================================
// 文件： PBM_OrderPayInfoDAL.cs
// 项目名称：
// 创建时间：2015-03-17
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model.PBM;
using MCSFramework.SQLDAL.PBM;

namespace MCSFramework.BLL.PBM
{
	/// <summary>
	///PBM_OrderPayInfoBLL业务逻辑BLL类
	/// </summary>
	public class PBM_OrderPayInfoBLL : BaseSimpleBLL<PBM_OrderPayInfo>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.PBM.PBM_OrderPayInfoDAL";
        private PBM_OrderPayInfoDAL _dal;
		
		#region 构造函数
		///<summary>
		///PBM_OrderPayInfoBLL
		///</summary>
		public PBM_OrderPayInfoBLL()
			: base(DALClassName)
		{
			_dal = (PBM_OrderPayInfoDAL)_DAL;
            _m = new PBM_OrderPayInfo(); 
		}
		
		public PBM_OrderPayInfoBLL(int id)
            : base(DALClassName)
        {
            _dal = (PBM_OrderPayInfoDAL)_DAL;
            FillModel(id);
        }

        public PBM_OrderPayInfoBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (PBM_OrderPayInfoDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<PBM_OrderPayInfo> GetModelList(string condition)
        {
            return new PBM_OrderPayInfoBLL()._GetModelList(condition);
        }
		#endregion
	}
}