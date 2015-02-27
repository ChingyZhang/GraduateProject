
// ===================================================================
// 文件： ADFind_FindConditionDAL.cs
// 项目名称：
// 创建时间：2008-12-23
// 作者:	   
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model.Pub;
using MCSFramework.SQLDAL.Pub;
using System.Collections.Generic;
using MCSFramework.Model;

namespace MCSFramework.BLL.Pub
{
    /// <summary>
    ///ADFind_FindConditionBLL业务逻辑BLL类
    /// </summary>
    public class ADFind_FindConditionBLL : BaseSimpleBLL<ADFind_FindCondition>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.Pub.ADFind_FindConditionDAL";
        private ADFind_FindConditionDAL _dal;

        #region 构造函数
        ///<summary>
        ///ADFind_FindConditionBLL
        ///</summary>
        public ADFind_FindConditionBLL()
            : base(DALClassName)
        {
            _dal = (ADFind_FindConditionDAL)_DAL;
            _m = new ADFind_FindCondition();
        }

        public ADFind_FindConditionBLL(int id)
            : base(DALClassName)
        {
            _dal = (ADFind_FindConditionDAL)_DAL;
            FillModel(id);
        }

        public ADFind_FindConditionBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (ADFind_FindConditionDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<ADFind_FindCondition> GetModelList(string condition)
        {
            return new ADFind_FindConditionBLL()._GetModelList(condition);
        }
        #endregion

        public static IList<ADFind_FindCondition> GetMyADFind(string panelcode, int staff)
        {
            string condition = "";
            UD_Panel panel = new UD_PanelBLL(panelcode, true).Model;
            if (panel != null)
                condition = "Panel='" + panel.ID.ToString() + "' AND (OpStaff=" + staff + " OR IsPublic='Y')";
            else
                condition = "1=2";

            return GetModelList(condition);
        }
    }
}