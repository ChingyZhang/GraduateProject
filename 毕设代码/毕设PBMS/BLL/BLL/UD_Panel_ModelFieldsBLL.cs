
// ===================================================================
// 文件： UD_Panel_ModelFieldsDAL.cs
// 项目名称：
// 创建时间：2008-11-27
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
	///UD_Panel_ModelFieldsBLL业务逻辑BLL类
	/// </summary>
	public class UD_Panel_ModelFieldsBLL : BaseSimpleBLL<UD_Panel_ModelFields>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.UD_Panel_ModelFieldsDAL";
        private UD_Panel_ModelFieldsDAL _dal;
		
		#region 构造函数
		///<summary>
		///UD_Panel_ModelFieldsBLL
		///</summary>
		public UD_Panel_ModelFieldsBLL()
			: base(DALClassName)
		{
			_dal = (UD_Panel_ModelFieldsDAL)_DAL;
            _m = new UD_Panel_ModelFields(); 
		}

        public UD_Panel_ModelFieldsBLL(Guid id)
            : base(DALClassName)
        {
            _dal = (UD_Panel_ModelFieldsDAL)_DAL;
            FillModel(id);
        }

        public UD_Panel_ModelFieldsBLL(Guid id, bool bycache)
            : base(DALClassName)
        {
            _dal = (UD_Panel_ModelFieldsDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion

        #region	静态GetModelList方法
        public static IList<UD_Panel_ModelFields> GetModelList(string condition)
        {
            return new UD_Panel_ModelFieldsBLL()._GetModelList(condition);
        }
        #endregion
	}
}