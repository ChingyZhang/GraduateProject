
// ===================================================================
// 文件： JN_WorkingPlanDAL.cs
// 项目名称：
// 创建时间：2009/6/18
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model.OA;
using MCSFramework.SQLDAL.OA;

namespace MCSFramework.BLL.OA
{
    /// <summary>
    ///JN_WorkingPlanBLL业务逻辑BLL类
    /// </summary>
    public class JN_WorkingPlanBLL : BaseComplexBLL<JN_WorkingPlan, JN_WorkingPlanDetail>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.OA.JN_WorkingPlanDAL";
        private JN_WorkingPlanDAL _dal;

        #region 构造函数
        ///<summary>
        ///JN_WorkingPlanBLL
        ///</summary>
        public JN_WorkingPlanBLL()
            : base(DALClassName)
        {
            _dal = (JN_WorkingPlanDAL)_DAL;
            _m = new JN_WorkingPlan();
        }

        public JN_WorkingPlanBLL(int id)
            : base(DALClassName)
        {
            _dal = (JN_WorkingPlanDAL)_DAL;
            FillModel(id);
        }

        public JN_WorkingPlanBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (JN_WorkingPlanDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<JN_WorkingPlan> GetModelList(string condition)
        {
            return new JN_WorkingPlanBLL()._GetModelList(condition);
        }
        #endregion

        /// <summary>
        /// 提交申请
        /// </summary>
        /// <param name="staff"></param>
        /// <returns></returns>
        public int Submit(int staff, int taskid)
        {
            return _dal.Submit(_m.ID, staff, taskid);
        }

        public static DataTable GetSummary(DateTime BeginDate, DateTime EndDate, int OrganizeCity, int Position, string Staff, int IncludeChildPosition)
        {
            JN_WorkingPlanDAL dal = (JN_WorkingPlanDAL)DataAccess.CreateObject(DALClassName);
            return Tools.ConvertDataReaderToDataTable(dal.GetSummary(BeginDate, EndDate, OrganizeCity, Position, Staff,IncludeChildPosition));
        }
    }
}