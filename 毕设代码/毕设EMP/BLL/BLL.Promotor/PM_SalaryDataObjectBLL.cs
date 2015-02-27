
// ===================================================================
// 文件： PM_SalaryDataObjectDAL.cs
// 项目名称：
// 创建时间：2011/1/11
// 作者:	   
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model.Promotor;
using MCSFramework.SQLDAL.Promotor;

namespace MCSFramework.BLL.Promotor
{
	/// <summary>
	///PM_SalaryDataObjectBLL业务逻辑BLL类
	/// </summary>
	public class PM_SalaryDataObjectBLL : BaseSimpleBLL<PM_SalaryDataObject>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.Promotor.PM_SalaryDataObjectDAL";
        private PM_SalaryDataObjectDAL _dal;
		
		#region 构造函数
		///<summary>
		///PM_SalaryDataObjectBLL
		///</summary>
		public PM_SalaryDataObjectBLL()
			: base(DALClassName)
		{
			_dal = (PM_SalaryDataObjectDAL)_DAL;
            _m = new PM_SalaryDataObject(); 
		}
		
		public PM_SalaryDataObjectBLL(int id)
            : base(DALClassName)
        {
            _dal = (PM_SalaryDataObjectDAL)_DAL;
            FillModel(id);
        }

        public PM_SalaryDataObjectBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (PM_SalaryDataObjectDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<PM_SalaryDataObject> GetModelList(string condition)
        {
            return new PM_SalaryDataObjectBLL()._GetModelList(condition);
        }
		#endregion

        public static DataTable GetByOrganizeCity(int OrganizeCity, int AccountMonth, int Promotor, int ApproveFlag)
        {
            PM_SalaryDataObjectDAL dal = (PM_SalaryDataObjectDAL)DataAccess.CreateObject(DALClassName);
            return Tools.ConvertDataReaderToDataTable(dal.GetByOrganizeCity(OrganizeCity, AccountMonth, Promotor, ApproveFlag));
        }
        public static void Init(int OrganizeCity, int AccountMonth)
        {
            PM_SalaryDataObjectDAL dal = (PM_SalaryDataObjectDAL)DataAccess.CreateObject(DALClassName);
            dal.Init(OrganizeCity, AccountMonth);
        }
        public static int Refresh(int ID)
        {
            PM_SalaryDataObjectDAL dal = (PM_SalaryDataObjectDAL)DataAccess.CreateObject(DALClassName);
            return dal.Refresh(ID);

        }
	}
}