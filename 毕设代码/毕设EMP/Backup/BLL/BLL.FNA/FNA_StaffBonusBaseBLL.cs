
// ===================================================================
// 文件： FNA_StaffBonusBaseDAL.cs
// 项目名称：
// 创建时间：2013/7/19
// 作者:	   chf
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
	///FNA_StaffBonusBaseBLL业务逻辑BLL类
	/// </summary>
	public class FNA_StaffBonusBaseBLL : BaseSimpleBLL<FNA_StaffBonusBase>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.FNA.FNA_StaffBonusBaseDAL";
        private FNA_StaffBonusBaseDAL _dal;
		
		#region 构造函数
		///<summary>
		///FNA_StaffBonusBaseBLL
		///</summary>
		public FNA_StaffBonusBaseBLL()
			: base(DALClassName)
		{
			_dal = (FNA_StaffBonusBaseDAL)_DAL;
            _m = new FNA_StaffBonusBase(); 
		}
		
		public FNA_StaffBonusBaseBLL(int id)
            : base(DALClassName)
        {
            _dal = (FNA_StaffBonusBaseDAL)_DAL;
            FillModel(id);
        }

        public FNA_StaffBonusBaseBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (FNA_StaffBonusBaseDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<FNA_StaffBonusBase> GetModelList(string condition)
        {
            return new FNA_StaffBonusBaseBLL()._GetModelList(condition);
        }
		#endregion
	}
}