
// ===================================================================
// 文件： SVM_DownloadTemplateDAL.cs
// 项目名称：
// 创建时间：2012/6/19
// 作者:	   chf
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
    ///SVM_DownloadTemplateBLL业务逻辑BLL类
    /// </summary>
    public class SVM_DownloadTemplateBLL : BaseSimpleBLL<SVM_DownloadTemplate>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.SVM.SVM_DownloadTemplateDAL";
        private SVM_DownloadTemplateDAL _dal;

        #region 构造函数
        ///<summary>
        ///SVM_DownloadTemplateBLL
        ///</summary>
        public SVM_DownloadTemplateBLL()
            : base(DALClassName)
        {
            _dal = (SVM_DownloadTemplateDAL)_DAL;
            _m = new SVM_DownloadTemplate();
        }

        public SVM_DownloadTemplateBLL(int id)
            : base(DALClassName)
        {
            _dal = (SVM_DownloadTemplateDAL)_DAL;
            FillModel(id);
        }

        public SVM_DownloadTemplateBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (SVM_DownloadTemplateDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<SVM_DownloadTemplate> GetModelList(string condition)
        {
            return new SVM_DownloadTemplateBLL()._GetModelList(condition);
        }
        #endregion
    }
}