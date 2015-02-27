
// ===================================================================
// 文件： Org_StaffNumberLimitDAL.cs
// 项目名称：
// 创建时间：2010/4/30
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
    ///Org_StaffNumberLimitBLL业务逻辑BLL类
    /// </summary>
    public class Org_StaffNumberLimitBLL : BaseSimpleBLL<Org_StaffNumberLimit>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.Org_StaffNumberLimitDAL";
        private Org_StaffNumberLimitDAL _dal;

        #region 构造函数
        ///<summary>
        ///Org_StaffNumberLimitBLL
        ///</summary>
        public Org_StaffNumberLimitBLL()
            : base(DALClassName)
        {
            _dal = (Org_StaffNumberLimitDAL)_DAL;
            _m = new Org_StaffNumberLimit();
        }

        public Org_StaffNumberLimitBLL(int id)
            : base(DALClassName)
        {
            _dal = (Org_StaffNumberLimitDAL)_DAL;
            FillModel(id);
        }

        public Org_StaffNumberLimitBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (Org_StaffNumberLimitDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<Org_StaffNumberLimit> GetModelList(string condition)
        {
            return new Org_StaffNumberLimitBLL()._GetModelList(condition);
        }
        #endregion

        public static int Init(int OrganizeCity, int Level, int Position, bool IncludeChild, int InsertStaff)
        {
            Org_StaffNumberLimitDAL dal = (Org_StaffNumberLimitDAL)DataAccess.CreateObject(DALClassName);
            return dal.Init(OrganizeCity, Level, Position, IncludeChild, InsertStaff);
        }

        public static int GetActualNumber(int OrganizeCity, int Position)
        {
            Org_StaffNumberLimitDAL dal = (Org_StaffNumberLimitDAL)DataAccess.CreateObject(DALClassName);
            return dal.GetActualNumber(OrganizeCity, Position);
        }


        /// <summary>
        /// 判断两个城市及类别是否同属于一条限制记录
        /// </summary>
        /// <param name="OrganizeCity1"></param>
        /// <param name="OrganizeCity2"></param>
        /// <returns>true:同属于一个限制记录</returns>
        public static bool IsSameLimit(int OrganizeCity1, int OrganizeCity2, int Position1, int Position2)
        {
            Org_StaffNumberLimitDAL dal = (Org_StaffNumberLimitDAL)DataAccess.CreateObject(DALClassName);
            return dal.IsSameLimit(OrganizeCity1, OrganizeCity2, Position1, Position2);
        }

        /// <summary>
        /// 判断该城市限制了录入促销员的数量
        /// </summary>
        /// <param name="OrganizeCity"></param>
        /// <returns>未限制返回100,否则返回还可增加人数</returns>
        public static int CheckAllowAdd(int OrganizeCity, int Position)
        {
            Org_StaffNumberLimitDAL dal = (Org_StaffNumberLimitDAL)DataAccess.CreateObject(DALClassName);
            return dal.CheckAllowAdd(OrganizeCity, Position);
        }

        /// <summary>
        /// 判断该城市是否设定了促销员的预算人数
        /// </summary>
        /// <param name="OrganizeCity"></param>
        /// <param name="Position"></param>
        /// <returns>未限制返回100,否则返回预算内还可增加人数</returns>
        public static int CheckOverBudget(int OrganizeCity, int Position)
        {
            Org_StaffNumberLimitDAL dal = (Org_StaffNumberLimitDAL)DataAccess.CreateObject(DALClassName);
            return dal.CheckOverBudget(OrganizeCity, Position);
        }
    }
}