
// ===================================================================
// 文件： Const_IPLocationDAL.cs
// 项目名称：
// 创建时间：2009/6/21
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model;
using MCSFramework.SQLDAL;

namespace MCSFramework.BLL
{
	/// <summary>
	///Const_IPLocationBLL业务逻辑BLL类
	/// </summary>
	public class Const_IPLocationBLL : BaseSimpleBLL<Const_IPLocation>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.Const_IPLocationDAL";
        private Const_IPLocationDAL _dal;
		
		#region 构造函数
		///<summary>
		///Const_IPLocationBLL
		///</summary>
		public Const_IPLocationBLL()
			: base(DALClassName)
		{
			_dal = (Const_IPLocationDAL)_DAL;
            _m = new Const_IPLocation(); 
		}
		
		public Const_IPLocationBLL(int id)
            : base(DALClassName)
        {
            _dal = (Const_IPLocationDAL)_DAL;
            FillModel(id);
        }

        public Const_IPLocationBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (Const_IPLocationDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<Const_IPLocation> GetModelList(string condition)
        {
            return new Const_IPLocationBLL()._GetModelList(condition);
        }
		#endregion

        /// <summary>
        /// 根据IP寻找所属地
        /// </summary>
        /// <param name="ipaddress"></param>
        /// <returns></returns>
        public static Const_IPLocation FindByIP(string ipaddr)
        {
            Const_IPLocationDAL dal = (Const_IPLocationDAL)DataAccess.CreateObject(DALClassName);
            return dal.FindByIP(ipaddr);
        }
	}
}