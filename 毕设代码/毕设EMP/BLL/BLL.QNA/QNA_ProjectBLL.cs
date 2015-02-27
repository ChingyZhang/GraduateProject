
// ===================================================================
// 文件： QNA_ProjectDAL.cs
// 项目名称：
// 创建时间：2009/11/29
// 作者:	   Shen Gang
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
	///QNA_ProjectBLL业务逻辑BLL类
	/// </summary>
	public class QNA_ProjectBLL : BaseSimpleBLL<QNA_Project>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.QNA.QNA_ProjectDAL";
        private QNA_ProjectDAL _dal;
		
		#region 构造函数
		///<summary>
		///QNA_ProjectBLL
		///</summary>
		public QNA_ProjectBLL()
			: base(DALClassName)
		{
			_dal = (QNA_ProjectDAL)_DAL;
            _m = new QNA_Project(); 
		}
		
		public QNA_ProjectBLL(int id)
            : base(DALClassName)
        {
            _dal = (QNA_ProjectDAL)_DAL;
            FillModel(id);
        }

        public QNA_ProjectBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (QNA_ProjectDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<QNA_Project> GetModelList(string condition)
        {
            return new QNA_ProjectBLL()._GetModelList(condition);
        }
		#endregion

        /// <summary>
        /// 获取问卷统计结果
        /// </summary>
        /// <returns></returns>
        public DataTable GetResultStatistics()
        {
            return _dal.GetResultStatistics(_m.ID);
        }

        /// <summary>
        /// 获取问卷调研份数
        /// </summary>
        /// <returns></returns>
        public int GetResultCount()
        {
            return _dal.GetResultCount(_m.ID);
        }
	}
}