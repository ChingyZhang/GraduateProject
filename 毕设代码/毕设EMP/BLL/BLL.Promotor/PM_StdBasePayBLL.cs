
// ===================================================================
// 文件： PM_StdBasePayDAL.cs
// 项目名称：
// 创建时间：2011/10/21
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
	///PM_StdBasePayBLL业务逻辑BLL类
	/// </summary>
	public class PM_StdBasePayBLL : BaseSimpleBLL<PM_StdBasePay>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.Promotor.PM_StdBasePayDAL";
        private PM_StdBasePayDAL _dal;
		
		#region 构造函数
		///<summary>
		///PM_StdBasePayBLL
		///</summary>
		public PM_StdBasePayBLL()
			: base(DALClassName)
		{
			_dal = (PM_StdBasePayDAL)_DAL;
            _m = new PM_StdBasePay(); 
		}
		
		public PM_StdBasePayBLL(int id)
            : base(DALClassName)
        {
            _dal = (PM_StdBasePayDAL)_DAL;
            FillModel(id);
        }

        public PM_StdBasePayBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (PM_StdBasePayDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<PM_StdBasePay> GetModelList(string condition)
        {
            return new PM_StdBasePayBLL()._GetModelList(condition);
        }
		#endregion

        /// <summary>
        /// 获取指定促销员所在门店的底薪标准
        /// </summary>
        /// <param name="Promotor"></param>
        /// <returns></returns>
        public static decimal GetBasePayByPromotor(int Promotor)
        {
            PM_StdBasePayDAL dal = (PM_StdBasePayDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetBasePayByPromotor(Promotor);
        }

        /// <summary>
        /// 获取指定促销员所在门店的保底标准
        /// </summary>
        /// <param name="Promotor"></param>
        /// <returns></returns>
        public static decimal GetMinimumWageByPromotor(int Promotor)
        {
            PM_StdBasePayDAL dal = (PM_StdBasePayDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetMinimumWageByPromotor(Promotor);
        }
	}
}