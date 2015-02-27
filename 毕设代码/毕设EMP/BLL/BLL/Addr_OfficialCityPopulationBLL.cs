
// ===================================================================
// 文件： Addr_OfficialCityPopulationDAL.cs
// 项目名称：
// 创建时间：2010/12/17
// 作者:	   
// ===================================================================
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
    ///Addr_OfficialCityPopulationBLL业务逻辑BLL类
    /// </summary>
    public class Addr_OfficialCityPopulationBLL : BaseSimpleBLL<Addr_OfficialCityPopulation>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.Addr_OfficialCityPopulationDAL";
        private Addr_OfficialCityPopulationDAL _dal;

        #region 构造函数
        ///<summary>
        ///Addr_OfficialCityPopulationBLL
        ///</summary>
        public Addr_OfficialCityPopulationBLL()
            : base(DALClassName)
        {
            _dal = (Addr_OfficialCityPopulationDAL)_DAL;
            _m = new Addr_OfficialCityPopulation();
        }

        public Addr_OfficialCityPopulationBLL(int id)
            : base(DALClassName)
        {
            _dal = (Addr_OfficialCityPopulationDAL)_DAL;
            FillModel(id);
        }

        public Addr_OfficialCityPopulationBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (Addr_OfficialCityPopulationDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<Addr_OfficialCityPopulation> GetModelList(string condition)
        {
            return new Addr_OfficialCityPopulationBLL()._GetModelList(condition);
        }
        #endregion
    }
}