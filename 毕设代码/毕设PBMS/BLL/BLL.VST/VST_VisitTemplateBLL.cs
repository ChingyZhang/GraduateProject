
// ===================================================================
// 文件： VST_VisitTemplateDAL.cs
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
    ///VST_VisitTemplateBLL业务逻辑BLL类
    /// </summary>
    public class VST_VisitTemplateBLL : BaseComplexBLL<VST_VisitTemplate, VST_VisitTemplateDetail>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.VST.VST_VisitTemplateDAL";
        private VST_VisitTemplateDAL _dal;

        #region 构造函数
        ///<summary>
        ///VST_VisitTemplateBLL
        ///</summary>
        public VST_VisitTemplateBLL()
            : base(DALClassName)
        {
            _dal = (VST_VisitTemplateDAL)_DAL;
            _m = new VST_VisitTemplate();
        }

        public VST_VisitTemplateBLL(int id)
            : base(DALClassName)
        {
            _dal = (VST_VisitTemplateDAL)_DAL;
            FillModel(id);
        }

        public VST_VisitTemplateBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (VST_VisitTemplateDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<VST_VisitTemplate> GetModelList(string condition)
        {
            return new VST_VisitTemplateBLL()._GetModelList(condition);
        }
        #endregion

        /// <summary>
        /// 获取指定TDP可使用的拜访模板
        /// </summary>
        /// <param name="TDP"></param>
        /// <returns></returns>
        public static IList<VST_VisitTemplate> GetVisitTemplateByTDP(int TDP)
        {
            return GetModelList("ISNULL(EnableFlag,'Y')='Y' AND (OwnerType IN (1,2) OR OwnerClient=" + TDP.ToString() + ")");
        }

        /// <summary>
        /// 获取非TDP维护的拜访模板
        /// </summary>
        /// <returns></returns>
        public static IList<VST_VisitTemplate> GetVisitTemplateUnTDP()
        {
            return GetModelList(" OwnerType IN (1,2) ");
        }
    }
}