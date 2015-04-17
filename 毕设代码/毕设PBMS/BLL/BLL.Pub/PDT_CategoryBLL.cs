
// ===================================================================
// 文件： PDT_CategoryDAL.cs
// 项目名称：
// 创建时间：2015/1/27
// 作者:	   lv
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model.Pub;
using MCSFramework.SQLDAL.Pub;

namespace MCSFramework.BLL.Pub
{
	/// <summary>
	///PDT_CategoryBLL业务逻辑BLL类
	/// </summary>
	public class PDT_CategoryBLL : BaseSimpleBLL<PDT_Category>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.Pub.PDT_CategoryDAL";
        private PDT_CategoryDAL _dal;
		
		#region 构造函数
		///<summary>
		///PDT_CategoryBLL
		///</summary>
		public PDT_CategoryBLL()
			: base(DALClassName)
		{
			_dal = (PDT_CategoryDAL)_DAL;
            _m = new PDT_Category(); 
		}
		
		public PDT_CategoryBLL(int id)
            : base(DALClassName)
        {
            _dal = (PDT_CategoryDAL)_DAL;
            FillModel(id);
        }

        public PDT_CategoryBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (PDT_CategoryDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<PDT_Category> GetModelList(string condition)
        {
            return new PDT_CategoryBLL()._GetModelList(condition);
        }
		#endregion

        public static string GetFullCategoryName(int ID)
        {
            return TreeTableBLL.GetFullPathName("MCS_PUB.dbo.PDT_Category", ID);
        }
        /// <summary>
        /// 获取指定客户可以查询的产品类别
        /// </summary>
        /// <param name="OwnerType"></param>
        /// <param name="OwnerClient"></param>
        /// <returns></returns>
        public static DataTable GetListByOwnerClient(int OwnerType, int OwnerClient, string EnabledFlag)
        {
            PDT_CategoryDAL dal = (PDT_CategoryDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetListByOwnerClient(OwnerType, OwnerClient,EnabledFlag);
        }
        public static DataTable GetListByOwnerClient(int OwnerType, int OwnerClient)
        {
            return GetListByOwnerClient(OwnerType, OwnerClient, "Y");
        }

	}
}