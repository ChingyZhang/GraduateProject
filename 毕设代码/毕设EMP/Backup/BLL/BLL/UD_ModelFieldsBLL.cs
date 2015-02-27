
// ===================================================================
// 文件： UD_ModelFieldsDAL.cs
// 项目名称：
// 创建时间：2008-11-25
// 作者:	   
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model;
using MCSFramework.SQLDAL;
using System.Collections.Generic;

namespace MCSFramework.BLL
{
    /// <summary>
    ///UD_ModelFieldsBLL业务逻辑BLL类
    /// </summary>
    public class UD_ModelFieldsBLL : BaseSimpleBLL<UD_ModelFields>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.UD_ModelFieldsDAL";
        private UD_ModelFieldsDAL _dal;

        #region 构造函数
        ///<summary>
        ///UD_ModelFieldsBLL
        ///</summary>
        public UD_ModelFieldsBLL()
            : base(DALClassName)
        {
            _dal = (UD_ModelFieldsDAL)_DAL;
            _m = new UD_ModelFields();
        }

        public UD_ModelFieldsBLL(Guid id)
            : base(DALClassName)
        {
            _dal = (UD_ModelFieldsDAL)_DAL;
            FillModel(id);
        }

        public UD_ModelFieldsBLL(Guid id, bool bycache)
            : base(DALClassName)
        {
            _dal = (UD_ModelFieldsDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        public static void Init(Guid TableID)
        {
            UD_ModelFieldsDAL dal = (UD_ModelFieldsDAL)DataAccess.CreateObject(DALClassName);
            dal.Init(TableID);
        }

        public static IList<UD_ModelFields> GetModelList(string condition)
        {
            return new UD_ModelFieldsBLL()._GetModelList(condition);
        }
    }
}