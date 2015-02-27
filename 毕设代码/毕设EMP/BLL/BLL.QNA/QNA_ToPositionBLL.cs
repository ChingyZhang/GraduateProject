
// ===================================================================
// 文件： QNA_ToPositionDAL.cs
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
    ///QNA_ToPositionBLL业务逻辑BLL类
    /// </summary>
    public class QNA_ToPositionBLL : BaseSimpleBLL<QNA_ToPosition>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.QNA.QNA_ToPositionDAL";
        private QNA_ToPositionDAL _dal;

        #region 构造函数
        ///<summary>
        ///QNA_ToPositionBLL
        ///</summary>
        public QNA_ToPositionBLL()
            : base(DALClassName)
        {
            _dal = (QNA_ToPositionDAL)_DAL;
            _m = new QNA_ToPosition();
        }

        public QNA_ToPositionBLL(int id)
            : base(DALClassName)
        {
            _dal = (QNA_ToPositionDAL)_DAL;
            FillModel(id);
        }

        public QNA_ToPositionBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (QNA_ToPositionDAL)_DAL;
            FillModel(id, bycache);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<QNA_ToPosition> GetModelList(string condition)
        {
            return new QNA_ToPositionBLL()._GetModelList(condition);
        }
        #endregion


        #region 根据问卷ID获取职位
        public List<int> GetPositionByProjectID(int projectID)
        {
            return _dal.GetPositionByProjectID(projectID);
        }
        #endregion

        #region 根据职位获取问卷ID
        public List<int> GetProjectIDByPosition(int position)
        {
            return _dal.GetProjectIDByPosition(position);
        }
        #endregion

        #region 根据问卷ID删除所有职位
        public int DeleteByProjectID(int projectID)
        {
            return _dal.DeleteByProjectID(projectID);
        }
        #endregion

        #region 根据问卷ID删除一个职位
        public int DeletePosition(int projectID, int position)
        {
            return _dal.DeletePosition(projectID, position);
        }
        #endregion
    }
}