
// ===================================================================
// 文件： PBM_DeliveryPayInfoDAL.cs
// 项目名称：
// 创建时间：2015-03-15
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
	///PBM_DeliveryPayInfoBLL业务逻辑BLL类
	/// </summary>
	public class PBM_DeliveryPayInfoBLL : BaseSimpleBLL<PBM_DeliveryPayInfo>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.PBM.PBM_DeliveryPayInfoDAL";
        private PBM_DeliveryPayInfoDAL _dal;
		
		#region 构造函数
		///<summary>
		///PBM_DeliveryPayInfoBLL
		///</summary>
		public PBM_DeliveryPayInfoBLL()
			: base(DALClassName)
		{
			_dal = (PBM_DeliveryPayInfoDAL)_DAL;
            _m = new PBM_DeliveryPayInfo(); 
		}
		
		public PBM_DeliveryPayInfoBLL(int id)
            : base(DALClassName)
        {
            _dal = (PBM_DeliveryPayInfoDAL)_DAL;
            FillModel(id);
        }

        public PBM_DeliveryPayInfoBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (PBM_DeliveryPayInfoDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<PBM_DeliveryPayInfo> GetModelList(string condition)
        {
            return new PBM_DeliveryPayInfoBLL()._GetModelList(condition);
        }
		#endregion
	}
}