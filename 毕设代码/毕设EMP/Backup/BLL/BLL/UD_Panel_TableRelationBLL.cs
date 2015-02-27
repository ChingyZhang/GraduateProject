
// ===================================================================
// 文件： UD_Panel_TableRelationDAL.cs
// 项目名称：
// 创建时间：2008-12-9
// 作者:	   
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model;
using MCSFramework.SQLDAL;
using System.Collections.Generic;

namespace MCSFramework.BLL
{
	/// <summary>
	///UD_Panel_TableRelationBLL业务逻辑BLL类
	/// </summary>
	public class UD_Panel_TableRelationBLL : BaseSimpleBLL<UD_Panel_TableRelation>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.UD_Panel_TableRelationDAL";
        private UD_Panel_TableRelationDAL _dal;
		
		#region 构造函数
		///<summary>
		///UD_Panel_TableRelationBLL
		///</summary>
		public UD_Panel_TableRelationBLL()
			: base(DALClassName)
		{
			_dal = (UD_Panel_TableRelationDAL)_DAL;
            _m = new UD_Panel_TableRelation(); 
		}

        public UD_Panel_TableRelationBLL(Guid id)
            : base(DALClassName)
        {
            _dal = (UD_Panel_TableRelationDAL)_DAL;
            FillModel(id);
        }

        public UD_Panel_TableRelationBLL(Guid id, bool bycache)
            : base(DALClassName)
        {
            _dal = (UD_Panel_TableRelationDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion

        #region	静态GetModelList方法
        public static IList<UD_Panel_TableRelation> GetModelList(string condition)
        {
            return new UD_Panel_TableRelationBLL()._GetModelList(condition);
        }
        #endregion
	}
}