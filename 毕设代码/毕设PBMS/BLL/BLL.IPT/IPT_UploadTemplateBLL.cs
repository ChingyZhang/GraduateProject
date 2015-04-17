
// ===================================================================
// 文件： IPT_UploadTemplateDAL.cs
// 项目名称：
// 创建时间：2015/3/17
// 作者:	   Jace
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model.IPT;
using MCSFramework.SQLDAL.IPT;

namespace MCSFramework.BLL.IPT
{
    /// <summary>
    ///IPT_UploadTemplateBLL业务逻辑BLL类
    /// </summary>
    public class IPT_UploadTemplateBLL : BaseSimpleBLL<IPT_UploadTemplate>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.IPT.IPT_UploadTemplateDAL";
        private IPT_UploadTemplateDAL _dal;

        #region 构造函数
        ///<summary>
        ///IPT_UploadTemplateBLL
        ///</summary>
        public IPT_UploadTemplateBLL()
            : base(DALClassName)
        {
            _dal = (IPT_UploadTemplateDAL)_DAL;
            _m = new IPT_UploadTemplate();
        }

        public IPT_UploadTemplateBLL(int id)
            : base(DALClassName)
        {
            _dal = (IPT_UploadTemplateDAL)_DAL;
            FillModel(id);
        }

        public IPT_UploadTemplateBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (IPT_UploadTemplateDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<IPT_UploadTemplate> GetModelList(string condition)
        {
            return new IPT_UploadTemplateBLL()._GetModelList(condition);
        }
        #endregion

        public int WriteMessage(string message, int type)
        {
            IPT_UploadTemplateMessageBLL _mess = new IPT_UploadTemplateMessageBLL();
            _mess.Model.Content = message;
            _mess.Model.TemplateID = this.Model.ID;
            _mess.Model.MessageType = type;
            return _mess.Add();
        }

        public int WriteMessage(string message)
        {
            return WriteMessage(message, 1);
        }

    }
}