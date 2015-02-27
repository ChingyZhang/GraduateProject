
// ===================================================================
// 文件： AC_AccountQuarterBLL.cs
// 项目名称：
// 创建时间：2013-08-02
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
    ///AC_AccountQuarterBLL业务逻辑BLL类
    /// </summary>
    public class AC_AccountQuarterBLL : BaseSimpleBLL<AC_AccountQuarter>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.Pub.AC_AccountQuarterDAL";
        private AC_AccountQuarterDAL _dal;

        #region 构造函数
        ///<summary>
        ///AC_AccountQuarterBLL
        ///</summary>
        public AC_AccountQuarterBLL()
            : base(DALClassName)
        {
            _dal = (AC_AccountQuarterDAL)_DAL;
            _m = new AC_AccountQuarter();
        }

        public AC_AccountQuarterBLL(int id)
            : base(DALClassName)
        {
            _dal = (AC_AccountQuarterDAL)_DAL;
            FillModel(id);
        }

        public AC_AccountQuarterBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (AC_AccountQuarterDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<AC_AccountQuarter> GetModelList(string condition)
        {
            return new AC_AccountQuarterBLL()._GetModelList(condition);
        }
        #endregion
    }
}