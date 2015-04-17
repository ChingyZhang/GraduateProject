
// ===================================================================
// 文件： AC_AccountMonthDAL.cs
// 项目名称：
// 创建时间：2008-12-22
// 作者:	   
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model.Pub;
using MCSFramework.SQLDAL.Pub;
using System.Collections.Generic;

namespace MCSFramework.BLL.Pub
{
	/// <summary>
	///AC_AccountMonthBLL业务逻辑BLL类
	/// </summary>
	public class AC_AccountMonthBLL : BaseSimpleBLL<AC_AccountMonth>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.Pub.AC_AccountMonthDAL";
        private AC_AccountMonthDAL _dal;
		
		#region 构造函数
		///<summary>
		///AC_AccountMonthBLL
		///</summary>
		public AC_AccountMonthBLL()
			: base(DALClassName)
		{
			_dal = (AC_AccountMonthDAL)_DAL;
            _m = new AC_AccountMonth(); 
		}
		
		public AC_AccountMonthBLL(int id)
            : base(DALClassName)
        {
            _dal = (AC_AccountMonthDAL)_DAL;
            FillModel(id);
        }

        public AC_AccountMonthBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (AC_AccountMonthDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion

        #region	静态GetModelList方法
        public static IList<AC_AccountMonth> GetModelList(string condition)
        {
            return new AC_AccountMonthBLL()._GetModelList(condition);
        }
        #endregion

        /// <summary>
        /// 获取指定日期所在的会计月
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static int GetMonthByDate(DateTime date)
        {
            AC_AccountMonthDAL dal = (AC_AccountMonthDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetMonthByDate(date);
        }

        /// <summary>
        /// 返回当前的会计月的ID
        /// </summary>
        /// <returns></returns>
        public static int GetCurrentMonth()
        {
            return GetMonthByDate(DateTime.Now);
        }

        public static DataTable GetAllYear()
        {
            AC_AccountMonthDAL dal = (AC_AccountMonthDAL)DataAccess.CreateObject(DALClassName);
            return Tools.ConvertDataReaderToDataTable(dal.GetAllYear());
        }

        public static DataTable GetAccountMonthByYear(int AccountYear)
        {
            AC_AccountMonthDAL dal = (AC_AccountMonthDAL)DataAccess.CreateObject(DALClassName);
            return Tools.ConvertDataReaderToDataTable(dal.GetAccountMonthByYear(AccountYear));
        }

        public static DataTable GetByDateRegion(DateTime Begindate, DateTime EndDate)
        {
            AC_AccountMonthDAL dal = (AC_AccountMonthDAL)DataAccess.CreateObject(DALClassName);
            return Tools.ConvertDataReaderToDataTable(dal.GetByDateRegion(Begindate, EndDate));
        }
	}
}