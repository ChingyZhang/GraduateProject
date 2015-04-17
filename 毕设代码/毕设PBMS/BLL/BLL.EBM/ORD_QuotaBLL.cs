
// ===================================================================
// 文件： ORD_QuotaDAL.cs
// 项目名称：
// 创建时间：2014-01-23
// 作者:	   ShenGang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model.EBM;
using MCSFramework.SQLDAL.EBM;

namespace MCSFramework.BLL.EBM
{
    /// <summary>
    ///ORD_QuotaBLL业务逻辑BLL类
    /// </summary>
    public class ORD_QuotaBLL : BaseComplexBLL<ORD_Quota, ORD_QuotaDetail>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.EBM.ORD_QuotaDAL";
        private ORD_QuotaDAL _dal;

        #region 构造函数
        ///<summary>
        ///ORD_QuotaBLL
        ///</summary>
        public ORD_QuotaBLL()
            : base(DALClassName)
        {
            _dal = (ORD_QuotaDAL)_DAL;
            _m = new ORD_Quota();
        }

        public ORD_QuotaBLL(int id)
            : base(DALClassName)
        {
            _dal = (ORD_QuotaDAL)_DAL;
            FillModel(id);
        }

        public ORD_QuotaBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (ORD_QuotaDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<ORD_Quota> GetModelList(string condition)
        {
            return new ORD_QuotaBLL()._GetModelList(condition);
        }
        #endregion

        /// <summary>
        /// 返回指定客户当月的可用配置信息
        /// </summary>
        /// <param name="Client"></param>
        /// <param name="Month"></param>
        /// <returns></returns>
        public static int GetQuotaByClientAndMonth(int Client, int Month)
        {
            IList<ORD_Quota> lists = GetModelList(string.Format("Client={0} AND AccountMonth={1} AND State=2", Client, Month));
            if (lists.Count == 0)
            {
                //取长期配额（Month=0)
                lists = GetModelList(string.Format("Client={0} AND ISNULL(AccountMonth,0)=0 AND State=2", Client));
                if (lists.Count == 0) return 0;
            }

            return lists[0].ID;
        }

        /// <summary>
        /// 设置配额状态
        /// </summary>
        /// <param name="State">1:拟定配额 2:配额已核准 3:配额作废</param>
        /// <param name="OpUser"></param>
        /// <returns></returns>
        public int SetState(int State, Guid OpUser)
        {
            return _dal.SetState(_m.ID, State, OpUser);
        }

        /// <summary>
        /// 将下游客户的配额汇总至上级客户
        /// </summary>
        /// <param name="Supplier"></param>
        /// <param name="AccountMonth"></param>
        /// <param name="OpUser"></param>
        /// <returns></returns>
        public static int SummaryToSupplier(int Supplier, int AccountMonth, Guid OpUser)
        {
            ORD_QuotaDAL dal = (ORD_QuotaDAL)DataAccess.CreateObject(DALClassName);
            return dal.SummaryToSupplier(Supplier, AccountMonth, OpUser);
        }

    }
}