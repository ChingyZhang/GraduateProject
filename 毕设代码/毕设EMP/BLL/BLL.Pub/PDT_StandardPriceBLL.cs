
// ===================================================================
// 文件： PDT_StandardPriceDAL.cs
// 项目名称：
// 创建时间：2011/8/23
// 作者:	   chf
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
    ///PDT_StandardPriceBLL业务逻辑BLL类
    /// </summary>
    public class PDT_StandardPriceBLL : BaseComplexBLL<PDT_StandardPrice, PDT_StandardPrice_Detail>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.Pub.PDT_StandardPriceDAL";
        private PDT_StandardPriceDAL _dal;
        private IList<PDT_StandardPrice_ApplyCity> _ApplyCityitems;

        public IList<PDT_StandardPrice_ApplyCity> ApplyCityItems
        {
            get
            {
                if (_ApplyCityitems == null) _ApplyCityitems = _dal.GetApplyCityDetail();
                return _ApplyCityitems;
            }
            set { _ApplyCityitems = value; }
        }

        #region 构造函数
        ///<summary>
        ///PDT_StandardPriceBLL
        ///</summary>
        public PDT_StandardPriceBLL()
            : base(DALClassName)
        {
            _dal = (PDT_StandardPriceDAL)_DAL;
            _m = new PDT_StandardPrice();
        }

        public PDT_StandardPriceBLL(int id)
            : base(DALClassName)
        {
            _dal = (PDT_StandardPriceDAL)_DAL;
            FillModel(id);
        }

        public PDT_StandardPriceBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (PDT_StandardPriceDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<PDT_StandardPrice> GetModelList(string condition)
        {
            return new PDT_StandardPriceBLL()._GetModelList(condition);
        }
        #endregion

        #region 适用管理片区明细操作
        public int AddApplyCity(PDT_StandardPrice_ApplyCity m)
        {
            return _dal.AddApplyCity(m);
        }

        public int UpdateApplyCity(PDT_StandardPrice_ApplyCity m)
        {
            return _dal.UpdateApplyCity(m);
        }

        public int DeleteApplyCity(int detailid)
        {
            return _dal.DeleteApplyCity(detailid);
        }

        public int DeleteApplyCity()
        {
            return _dal.DeleteApplyCity();
        }

        public PDT_StandardPrice_ApplyCity GetApplyCityDetailModel(int detailid)
        {
            return _dal.GetApplyCityDetailModel(detailid);
        }

        public IList<PDT_StandardPrice_ApplyCity> GetApplyCityDetail()
        {
            return _dal.GetApplyCityDetail();
        }
        #endregion

        public int Approve(int StaffID)
        {
            return _dal.Approve(_m.ID, StaffID);
        }

        public int UnActive(int StaffID)
        {
            return _dal.UnActive(_m.ID, StaffID);
        }

        /// <summary>
        /// 将产品推送发布到所有关联此标准价表的客户价表目录中
        /// </summary>
        /// <param name="Product"></param>
        /// <returns></returns>
        public int PublishProduct(int Product)
        {
            return _dal.PublishProduct(_m.ID, Product);
        }
    }
}