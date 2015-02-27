
// ===================================================================
// 文件： FNA_FeeWriteOffDetail_AdjustInfoDAL.cs
// 项目名称：
// 创建时间：2013-01-26
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model.FNA;
using MCSFramework.SQLDAL.FNA;

namespace MCSFramework.BLL.FNA
{
	/// <summary>
	///FNA_FeeWriteOffDetail_AdjustInfoBLL业务逻辑BLL类
	/// </summary>
	public class FNA_FeeWriteOffDetail_AdjustInfoBLL : BaseSimpleBLL<FNA_FeeWriteOffDetail_AdjustInfo>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.FNA.FNA_FeeWriteOffDetail_AdjustInfoDAL";
        private FNA_FeeWriteOffDetail_AdjustInfoDAL _dal;
		
		#region 构造函数
		///<summary>
		///FNA_FeeWriteOffDetail_AdjustInfoBLL
		///</summary>
		public FNA_FeeWriteOffDetail_AdjustInfoBLL()
			: base(DALClassName)
		{
			_dal = (FNA_FeeWriteOffDetail_AdjustInfoDAL)_DAL;
            _m = new FNA_FeeWriteOffDetail_AdjustInfo(); 
		}
		
		public FNA_FeeWriteOffDetail_AdjustInfoBLL(int id)
            : base(DALClassName)
        {
            _dal = (FNA_FeeWriteOffDetail_AdjustInfoDAL)_DAL;
            FillModel(id);
        }

        public FNA_FeeWriteOffDetail_AdjustInfoBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (FNA_FeeWriteOffDetail_AdjustInfoDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<FNA_FeeWriteOffDetail_AdjustInfo> GetModelList(string condition)
        {
            return new FNA_FeeWriteOffDetail_AdjustInfoBLL()._GetModelList(condition);
        }
		#endregion
	}
}