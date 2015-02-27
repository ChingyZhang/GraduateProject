
// ===================================================================
// 文件： SVM_MYSaleInFlagBLL.cs
// 项目名称：
// 创建时间：2014/11/12
// 作者:	   JACE
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
    ///SVM_MYSaleInFlagBLL业务逻辑BLL类
    /// </summary>
    public class SVM_MYSaleInFlagBLL : BaseSimpleBLL<SVM_MYSaleInFlag>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.SVM.SVM_MYSaleInFlagDAL";
        private SVM_MYSaleInFlagDAL _dal;

        #region 构造函数
        ///<summary>
        ///SVM_MYSaleInFlagBLL
        ///</summary>
        public SVM_MYSaleInFlagBLL()
            : base(DALClassName)
        {
            _dal = (SVM_MYSaleInFlagDAL)_DAL;
            _m = new SVM_MYSaleInFlag();
        }

        public SVM_MYSaleInFlagBLL(int id)
            : base(DALClassName)
        {
            _dal = (SVM_MYSaleInFlagDAL)_DAL;
            FillModel(id);
        }

        public SVM_MYSaleInFlagBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (SVM_MYSaleInFlagDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<SVM_MYSaleInFlag> GetModelList(string condition)
        {
            return new SVM_MYSaleInFlagBLL()._GetModelList(condition);
        }
        #endregion


         /// <summary>
        /// 月度母婴进货门店明细查询
        /// </summary>
        public static DataTable GetRPTSummary(int BeginMonth, int EndMonth, int OrganizeCity,int ClientManager,string Condition)
        {
            SVM_MYSaleInFlagDAL dal = (SVM_MYSaleInFlagDAL)DataAccess.CreateObject(DALClassName);
            return Tools.ConvertDataReaderToDataTable(dal.GetRPTSummary(BeginMonth, EndMonth, OrganizeCity, ClientManager, Condition));
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        public static DataTable GetList(int AccountMonth, int OrganizeCity, int ClientManger, string DICondition, string RTCondition)
        {
            SVM_MYSaleInFlagDAL dal = (SVM_MYSaleInFlagDAL)DataAccess.CreateObject(DALClassName);
            return Tools.ConvertDataReaderToDataTable(dal.GetList(AccountMonth, OrganizeCity, ClientManger, DICondition, RTCondition));
        }
    }
}