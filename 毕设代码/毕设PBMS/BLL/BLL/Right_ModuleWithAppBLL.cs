using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model;
using MCSFramework.SQLDAL;
namespace MCSFramework.BLL
{

    /// <summary>
    ///Right_ModuleWithAppBLL业务逻辑BLL类
    /// </summary>
    public class Right_ModuleWithAppBLL : BaseSimpleBLL<Right_ModuleWithApp>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.Right_ModuleWithAppDAL";
        private Right_ModuleWithAppDAL _dal;

        #region 构造函数
        ///<summary>
        ///Right_ModuleWithAppBLL
        ///</summary>
        public Right_ModuleWithAppBLL()
            : base(DALClassName)
        {
            _dal = (Right_ModuleWithAppDAL)_DAL;
            _m = new Right_ModuleWithApp();
        }

        public Right_ModuleWithAppBLL(int id)
            : base(DALClassName)
        {
            _dal = (Right_ModuleWithAppDAL)_DAL;
            FillModel(id);
        }

        public Right_ModuleWithAppBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (Right_ModuleWithAppDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<Right_ModuleWithApp> GetModelList(string condition)
        {
            return new Right_ModuleWithAppBLL()._GetModelList(condition);
        }
        #endregion
    }
}
