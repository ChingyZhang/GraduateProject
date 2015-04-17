
// ===================================================================
// 文件： VST_WorkItem_JDBLL.cs
// 项目名称：
// 创建时间：2015/1/27
// 作者:	   ChingyZhang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model.VST;
using MCSFramework.SQLDAL.VST;

namespace MCSFramework.BLL.VST
{
    /// <summary>
    ///VST_WorkItem_JDDAL业务逻辑BLL类
    /// </summary>
    public class VST_WorkItem_JDBLL : BaseSimpleBLL<VST_WorkItem_JD>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.VST.VST_WorkItem_JDDAL";
        private VST_WorkItem_JDDAL _dal;

        #region 构造函数
        ///<summary>
        ///VST_WorkItem_JDDAL
        ///</summary>
        public VST_WorkItem_JDBLL()
            : base(DALClassName)
        {
            _dal = (VST_WorkItem_JDDAL)_DAL;
            _m = new VST_WorkItem_JD();
        }

        public VST_WorkItem_JDBLL(int id)
            : base(DALClassName)
        {
            _dal = (VST_WorkItem_JDDAL)_DAL;
            FillModel(id);
        }

        public VST_WorkItem_JDBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (VST_WorkItem_JDDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<VST_WorkItem_JD> GetModelList(string condition)
        {
            return new VST_WorkItem_JDBLL()._GetModelList(condition);
        }
        #endregion

        /// <summary>
        /// 获取工作明细Job获取对应的进出店信息
        /// </summary>
        /// <param name="JobID"></param>
        /// <returns></returns>
        public static VST_WorkItem_JD GetDetailByJobID(int JobID)
        {
            IList<VST_WorkItem_JD> lists = GetModelList("Job=" + JobID.ToString());
            if (lists.Count > 0)
                return lists[0];
            else
                return null;
        }
    }
}