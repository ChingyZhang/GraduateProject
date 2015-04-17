
// ===================================================================
// 文件： PDT_ProductBLL.cs
// 项目名称：
// 创建时间：2015/1/30
// 作者:	   Jace
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
	///PDT_ProductBLL业务逻辑BLL类
	/// </summary>
	public class PDT_ProductBLL : BaseSimpleBLL<PDT_Product>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.Pub.PDT_ProductDAL";
        private PDT_ProductDAL _dal;
		
		#region 构造函数
		///<summary>
		///PDT_ProductBLL
		///</summary>
		public PDT_ProductBLL()
			: base(DALClassName)
		{
			_dal = (PDT_ProductDAL)_DAL;
            _m = new PDT_Product(); 
		}
		
		public PDT_ProductBLL(int id)
            : base(DALClassName)
        {
            _dal = (PDT_ProductDAL)_DAL;
            FillModel(id);
        }

        public PDT_ProductBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (PDT_ProductDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<PDT_Product> GetModelList(string condition)
        {
            return new PDT_ProductBLL()._GetModelList(condition);
        }
		#endregion

        #region TDP商品扩展管理信息维护
        /// <summary>
        /// 获取指定商品的TDP管理信息
        /// </summary>
        /// <param name="Supplier"></param>
        /// <returns></returns>
        public PDT_ProductExtInfo GetProductExtInfo(int Supplier)
        {
            IList<PDT_ProductExtInfo> extinfos = PDT_ProductExtInfoBLL.GetModelList("Supplier = " + Supplier.ToString()+" AND Product="+_m.ID.ToString());
            if (extinfos != null && extinfos.Count > 0)
                return extinfos[0];
            else
                return null;
        }

        /// <summary>
        /// 设置指定商品的TDP管理信息
        /// </summary>
        /// <param name="ProductExtInfo"></param>
        /// <returns></returns>
        public int SetProductExtInfo(PDT_ProductExtInfo ProductExtInfo)
        {
            if (ProductExtInfo == null) return -1;
            if (ProductExtInfo.Supplier == 0) return -2;

            PDT_ProductExtInfoBLL bll = new PDT_ProductExtInfoBLL();
            if (ProductExtInfo.ID == 0)
            {
                //判断是否已存在指定供货商的供货信息
                PDT_ProductExtInfo org = GetProductExtInfo(ProductExtInfo.Supplier);
                if (org == null)
                {
                    //不存在，新增一条供货信息
                    bll.Model = ProductExtInfo;
                    bll.Model.Product = _m.ID;
                    if (bll.Model.Category == 0) bll.Model.Category = _m.Category;
                    if (bll.Model.SalesState == 0) bll.Model.SalesState = 1;
                    if (bll.Model.ApproveFlag == 0) bll.Model.ApproveFlag = 1;
                   
                    return bll.Add();
                }
                else
                {
                    //存在，则将原ID赋给本次更新的ID，执行后续的更新操作
                    ProductExtInfo.ID = org.ID;
                }
            }

            //更新现在供货信息
            if (ProductExtInfo.ID > 0)
            {
                bll.Model = ProductExtInfo;
                return bll.Update();
            }

            return 0;
        }
        #endregion
        
	}
}