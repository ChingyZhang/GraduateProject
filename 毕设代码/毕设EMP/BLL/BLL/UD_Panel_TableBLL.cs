
// ===================================================================
// 文件： UD_Panel_TableDAL.cs
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
	///UD_Panel_TableBLL业务逻辑BLL类
	/// </summary>
	public class UD_Panel_TableBLL : BaseSimpleBLL<UD_Panel_Table>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.UD_Panel_TableDAL";
        private UD_Panel_TableDAL _dal;
		
		#region 构造函数
		///<summary>
		///UD_Panel_TableBLL
		///</summary>
		public UD_Panel_TableBLL()
			: base(DALClassName)
		{
			_dal = (UD_Panel_TableDAL)_DAL;
            _m = new UD_Panel_Table(); 
		}
		
		public UD_Panel_TableBLL(Guid id)
            : base(DALClassName)
        {
            _dal = (UD_Panel_TableDAL)_DAL;
            FillModel(id);
        }

        public UD_Panel_TableBLL(Guid id, bool bycache)
            : base(DALClassName)
        {
            _dal = (UD_Panel_TableDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion

        #region	静态GetModelList方法
        public static IList<UD_Panel_Table> GetModelList(string condition)
        {
            return new UD_Panel_TableBLL()._GetModelList(condition);
        }
        #endregion

        public static DataTable GetTableListByPanelID(Guid PanelID)
        {
            UD_Panel_TableDAL dal = (UD_Panel_TableDAL)DataAccess.CreateObject(DALClassName);

            return dal.GetTableListByPanelID(PanelID); 
        }
	}
}