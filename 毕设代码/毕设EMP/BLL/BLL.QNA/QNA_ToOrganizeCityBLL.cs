
// ===================================================================
// 文件： QNA_ToOrganizeCityDAL.cs
// 项目名称：
// 创建时间：2011/9/7
// 作者:	   TT
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model.QNA;
using MCSFramework.SQLDAL.QNA;

namespace MCSFramework.BLL.QNA
{
    /// <summary>
    ///QNA_ToOrganizeCityBLL业务逻辑BLL类
    /// </summary>
    public class QNA_ToOrganizeCityBLL : BaseSimpleBLL<QNA_ToOrganizeCity>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.QNA.QNA_ToOrganizeCityDAL";
        private QNA_ToOrganizeCityDAL _dal;

        #region 构造函数
        ///<summary>
        ///QNA_ToOrganizeCityBLL
        ///</summary>
        public QNA_ToOrganizeCityBLL()
            : base(DALClassName)
        {
            _dal = (QNA_ToOrganizeCityDAL)_DAL;
            _m = new QNA_ToOrganizeCity();
        }

        public QNA_ToOrganizeCityBLL(int id)
            : base(DALClassName)
        {
            _dal = (QNA_ToOrganizeCityDAL)_DAL;
            FillModel(id);
        }

        public QNA_ToOrganizeCityBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (QNA_ToOrganizeCityDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<QNA_ToOrganizeCity> GetModelList(string condition)
        {
            return new QNA_ToOrganizeCityBLL()._GetModelList(condition);
        }
        #endregion


        #region 根据问卷ID获取片区
        public  List<int> GetOrganizeCityByProjectID(int projectID)
        {
            return _dal.GetOrganizeCityByProjectID(projectID);
        }
        #endregion

        #region 根据片区获取问卷ID
        public  List<int> GetProjectIDByOrganizeCity(int organizeCity)
        {
            return _dal.GetProjectIDByOrganizeCity(organizeCity);
        }
        #endregion

        #region 根据问卷ID删除所有片区
        public int DeleteByProjectID(int projectID)
        {
            return _dal.DeleteByProjectID(projectID);
        }
        #endregion

        #region 根据问卷ID删除一个片区
        public int DeleteOrganizeCity(int projectID, int organizeCity)
        {
            return _dal.DeleteOrganizeCity(projectID, organizeCity);
        }
        #endregion
    }
}