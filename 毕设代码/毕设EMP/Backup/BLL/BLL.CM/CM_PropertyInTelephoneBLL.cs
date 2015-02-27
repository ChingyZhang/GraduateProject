
// ===================================================================
// 文件： CM_PropertyInTelephoneDAL.cs
// 项目名称：
// 创建时间：2012/3/6
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model.CM;
using MCSFramework.SQLDAL.CM;

namespace MCSFramework.BLL.CM
{
	/// <summary>
	///CM_PropertyInTelephoneBLL业务逻辑BLL类
	/// </summary>
	public class CM_PropertyInTelephoneBLL : BaseSimpleBLL<CM_PropertyInTelephone>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.CM.CM_PropertyInTelephoneDAL";
        private CM_PropertyInTelephoneDAL _dal;
		
		#region 构造函数
		///<summary>
		///CM_PropertyInTelephoneBLL
		///</summary>
		public CM_PropertyInTelephoneBLL()
			: base(DALClassName)
		{
			_dal = (CM_PropertyInTelephoneDAL)_DAL;
            _m = new CM_PropertyInTelephone(); 
		}
		
		public CM_PropertyInTelephoneBLL(int id)
            : base(DALClassName)
        {
            _dal = (CM_PropertyInTelephoneDAL)_DAL;
            FillModel(id);
        }

        public CM_PropertyInTelephoneBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (CM_PropertyInTelephoneDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<CM_PropertyInTelephone> GetModelList(string condition)
        {
            return new CM_PropertyInTelephoneBLL()._GetModelList(condition);
        }
		#endregion

        /// <summary>
        /// 获取指定管理片区范围内的所有已安装的电话目录
        /// </summary>
        /// <param name="OrganizeCity"></param>
        /// <returns></returns>
        public static IList<CM_PropertyInTelephone> GetListByOrganizeCity(int OrganizeCity)
        {
            CM_PropertyInTelephoneDAL dal = (CM_PropertyInTelephoneDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetListByOrganizeCity(OrganizeCity);
        }
	}
}