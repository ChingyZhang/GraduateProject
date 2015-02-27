
// ===================================================================
// 文件： SVM_OrganizeTargetDAL.cs
// 项目名称：
// 创建时间：2013/4/7
// 作者:	   chf
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model.SVM;
using MCSFramework.SQLDAL.SVM;

namespace MCSFramework.BLL.SVM
{
	/// <summary>
	///SVM_OrganizeTargetBLL业务逻辑BLL类
	/// </summary>
	public class SVM_OrganizeTargetBLL : BaseComplexBLL<SVM_OrganizeTarget,SVM_KeyProductTarget_Detail>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.SVM.SVM_OrganizeTargetDAL";
        private SVM_OrganizeTargetDAL _dal;
		
		#region 构造函数
		///<summary>
		///SVM_OrganizeTargetBLL
		///</summary>
		public SVM_OrganizeTargetBLL()
			: base(DALClassName)
		{
			_dal = (SVM_OrganizeTargetDAL)_DAL;
            _m = new SVM_OrganizeTarget(); 
		}
		
		public SVM_OrganizeTargetBLL(int id)
            : base(DALClassName)
        {
            _dal = (SVM_OrganizeTargetDAL)_DAL;
            FillModel(id);
        }

        public SVM_OrganizeTargetBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (SVM_OrganizeTargetDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<SVM_OrganizeTarget> GetModelList(string condition)
        {
            return new SVM_OrganizeTargetBLL()._GetModelList(condition);
        }
		#endregion

        public decimal GetSumByID(int ID)
        {
            SVM_OrganizeTargetDAL dal = (SVM_OrganizeTargetDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetSumByID(ID);
        }

        public static int UnApprove(int AccountMonth, int OrganizeCity)
        {
            SVM_OrganizeTargetDAL dal = (SVM_OrganizeTargetDAL)DataAccess.CreateObject(DALClassName);
            return dal.UnApprove(AccountMonth, OrganizeCity);
        }

        public static int Approve(int AccountMonth, int OrganizeCity)
        {
            SVM_OrganizeTargetDAL dal = (SVM_OrganizeTargetDAL)DataAccess.CreateObject(DALClassName);
            return dal.Approve(AccountMonth, OrganizeCity);
        }
	}
}