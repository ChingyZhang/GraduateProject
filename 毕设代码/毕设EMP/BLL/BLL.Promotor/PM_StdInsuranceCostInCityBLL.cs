
// ===================================================================
// 文件： PM_StdInsuranceCostInCityBLL.cs
// 项目名称：
// 创建时间：2014/2/24
// 作者:	   Jace
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model.Promotor;
using MCSFramework.SQLDAL.Promotor;

namespace MCSFramework.BLL.Promotor
{
    /// <summary>
    ///PM_StdBasePayInCityBLL业务逻辑BLL类
    /// </summary>
    public class PM_StdInsuranceCostInCityBLL : BaseSimpleBLL<PM_StdInsuranceCostInCity>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.Promotor.PM_StdInsuranceCostInCityDAL";
        private PM_StdInsuranceCostInCityDAL _dal;

        #region 构造函数
        ///<summary>
        ///PM_StdBasePayInCityBLL
        ///</summary>
        public PM_StdInsuranceCostInCityBLL()
            : base(DALClassName)
        {
            _dal = (PM_StdInsuranceCostInCityDAL)_DAL;
            _m = new PM_StdInsuranceCostInCity();
        }

        public PM_StdInsuranceCostInCityBLL(int id)
            : base(DALClassName)
        {
            _dal = (PM_StdInsuranceCostInCityDAL)_DAL;
            FillModel(id);
        }

        public PM_StdInsuranceCostInCityBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (PM_StdInsuranceCostInCityDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<PM_StdInsuranceCostInCity> GetModelList(string condition)
        {
            return new PM_StdInsuranceCostInCityBLL()._GetModelList(condition);
        }
        #endregion
    }
}