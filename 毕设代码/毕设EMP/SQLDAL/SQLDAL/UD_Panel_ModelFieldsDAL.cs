
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
//using MCSFramework.Common;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model;


namespace MCSFramework.SQLDAL
{
	/// <summary>
	///UD_Panel_ModelFields数据访问DAL类
	/// </summary>
	public class UD_Panel_ModelFieldsDAL : BaseSimpleDAL<UD_Panel_ModelFields>
	{
		#region 构造函数
		///<summary>
		///
		///</summary>
		public UD_Panel_ModelFieldsDAL()
		{
			_ProcePrefix = "MCS_SYS.dbo.sp_UD_Panel_ModelFields";
		}
		#endregion
		
		
		#region 成员方法
		/// <summary>
		/// 重写获取获取实例化对像
		/// </summary>
        /// <param name="id"></param>
		/// <returns></returns>
		public override UD_Panel_ModelFields GetModel(int id)
        {
            UD_Panel_ModelFields m = base.GetModel(id);


            return m;
        }
		/// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(UD_Panel_ModelFields m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.UniqueIdentifier, 16, m.ID),
				SQLDatabase.MakeInParam("@PanelID", SqlDbType.UniqueIdentifier, 16, m.PanelID),
				SQLDatabase.MakeInParam("@FieldID", SqlDbType.UniqueIdentifier, 16, m.FieldID),
                SQLDatabase.MakeInParam("@LabelText", SqlDbType.VarChar, 100, m.LabelText),
				SQLDatabase.MakeInParam("@ReadOnly", SqlDbType.Char, 1, m.ReadOnly),
				SQLDatabase.MakeInParam("@ControlType", SqlDbType.Int, 4, m.ControlType),
				SQLDatabase.MakeInParam("@ControlWidth", SqlDbType.Int, 4, m.ControlWidth),
				SQLDatabase.MakeInParam("@ControlHeight", SqlDbType.Int, 4, m.ControlHeight),
				SQLDatabase.MakeInParam("@ControlStyle", SqlDbType.VarChar, 500, m.ControlStyle),
				SQLDatabase.MakeInParam("@Description", SqlDbType.VarChar, 2000, m.Description),
				SQLDatabase.MakeInParam("@Enable", SqlDbType.Char, 1, m.Enable),
				SQLDatabase.MakeInParam("@Visible", SqlDbType.Char, 1, m.Visible),
				SQLDatabase.MakeInParam("@IsRequireField", SqlDbType.Char, 1, m.IsRequireField),
				SQLDatabase.MakeInParam("@SortID", SqlDbType.Int, 4, m.SortID),
                SQLDatabase.MakeInParam("@VisibleActionCode", SqlDbType.VarChar, 50, m.EnableActionCode),
				SQLDatabase.MakeInParam("@EnableActionCode", SqlDbType.VarChar, 50, m.EnableActionCode),
                SQLDatabase.MakeInParam("@DisplayMode", SqlDbType.Int, 4, m.DisplayMode),
                SQLDatabase.MakeInParam("@ColSpan", SqlDbType.Int, 4, m.ColSpan),
                SQLDatabase.MakeInParam("@RegularExpression", SqlDbType.VarChar, 500, m.RegularExpression),
                SQLDatabase.MakeInParam("@FormatString", SqlDbType.VarChar, 500, m.FormatString),
				SQLDatabase.MakeInParam("@SearchPageURL", SqlDbType.VarChar, 500, m.SearchPageURL),
                SQLDatabase.MakeInParam("@TreeLevel", SqlDbType.Int, 4, m.TreeLevel)
			};
			#endregion
			
            int ret =  SQLDatabase.RunProc(_ProcePrefix + "_Add", prams);
			
			
            return ret;
        }
		
		/// <summary>
        /// 更新项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Update(UD_Panel_ModelFields m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.UniqueIdentifier, 16, m.ID),
				SQLDatabase.MakeInParam("@PanelID", SqlDbType.UniqueIdentifier, 16, m.PanelID),
				SQLDatabase.MakeInParam("@FieldID", SqlDbType.UniqueIdentifier, 16, m.FieldID),
                SQLDatabase.MakeInParam("@LabelText", SqlDbType.VarChar, 100, m.LabelText),
				SQLDatabase.MakeInParam("@ReadOnly", SqlDbType.Char, 1, m.ReadOnly),
				SQLDatabase.MakeInParam("@ControlType", SqlDbType.Int, 4, m.ControlType),
				SQLDatabase.MakeInParam("@ControlWidth", SqlDbType.Int, 4, m.ControlWidth),
				SQLDatabase.MakeInParam("@ControlHeight", SqlDbType.Int, 4, m.ControlHeight),
				SQLDatabase.MakeInParam("@ControlStyle", SqlDbType.VarChar, 500, m.ControlStyle),
				SQLDatabase.MakeInParam("@Description", SqlDbType.VarChar, 2000, m.Description),
				SQLDatabase.MakeInParam("@Enable", SqlDbType.Char, 1, m.Enable),
				SQLDatabase.MakeInParam("@Visible", SqlDbType.Char, 1, m.Visible),
				SQLDatabase.MakeInParam("@IsRequireField", SqlDbType.Char, 1, m.IsRequireField),
				SQLDatabase.MakeInParam("@SortID", SqlDbType.Int, 4, m.SortID),
				SQLDatabase.MakeInParam("@VisibleActionCode", SqlDbType.VarChar, 50, m.VisibleActionCode),
                SQLDatabase.MakeInParam("@EnableActionCode", SqlDbType.VarChar, 50, m.EnableActionCode),
                SQLDatabase.MakeInParam("@DisplayMode", SqlDbType.Int, 4, m.DisplayMode),
                SQLDatabase.MakeInParam("@ColSpan", SqlDbType.Int, 4, m.ColSpan),
                SQLDatabase.MakeInParam("@RegularExpression", SqlDbType.VarChar, 500, m.RegularExpression),
                SQLDatabase.MakeInParam("@FormatString", SqlDbType.VarChar, 500, m.FormatString),
				SQLDatabase.MakeInParam("@SearchPageURL", SqlDbType.VarChar, 500, m.SearchPageURL),
                SQLDatabase.MakeInParam("@TreeLevel", SqlDbType.Int, 4, m.TreeLevel)
			};
			#endregion
			
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
			return ret;
        }
		
        protected override UD_Panel_ModelFields FillModel(IDataReader dr)
		{
			UD_Panel_ModelFields m = new UD_Panel_ModelFields();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (Guid)dr["ID"];
            if (!string.IsNullOrEmpty(dr["PanelID"].ToString())) m.PanelID = (Guid)dr["PanelID"];
            if (!string.IsNullOrEmpty(dr["FieldID"].ToString())) m.FieldID = (Guid)dr["FieldID"];
            if (!string.IsNullOrEmpty(dr["LabelText"].ToString())) m.LabelText = (string)dr["LabelText"];
			if (!string.IsNullOrEmpty(dr["ReadOnly"].ToString()))	m.ReadOnly = (string)dr["ReadOnly"];
			if (!string.IsNullOrEmpty(dr["ControlType"].ToString()))	m.ControlType = (int)dr["ControlType"];
			if (!string.IsNullOrEmpty(dr["ControlWidth"].ToString()))	m.ControlWidth = (int)dr["ControlWidth"];
			if (!string.IsNullOrEmpty(dr["ControlHeight"].ToString()))	m.ControlHeight = (int)dr["ControlHeight"];
			if (!string.IsNullOrEmpty(dr["ControlStyle"].ToString()))	m.ControlStyle = (string)dr["ControlStyle"];
			if (!string.IsNullOrEmpty(dr["Description"].ToString()))	m.Description = (string)dr["Description"];
			if (!string.IsNullOrEmpty(dr["Enable"].ToString()))	m.Enable = (string)dr["Enable"];
			if (!string.IsNullOrEmpty(dr["Visible"].ToString()))	m.Visible = (string)dr["Visible"];
			if (!string.IsNullOrEmpty(dr["IsRequireField"].ToString()))	m.IsRequireField = (string)dr["IsRequireField"];
			if (!string.IsNullOrEmpty(dr["SortID"].ToString()))	m.SortID = (int)dr["SortID"];
            if (!string.IsNullOrEmpty(dr["VisibleActionCode"].ToString())) m.VisibleActionCode = (string)dr["VisibleActionCode"];
            if (!string.IsNullOrEmpty(dr["EnableActionCode"].ToString())) m.EnableActionCode = (string)dr["EnableActionCode"];
            if (!string.IsNullOrEmpty(dr["DisplayMode"].ToString())) m.DisplayMode = (int)dr["DisplayMode"];
            if (!string.IsNullOrEmpty(dr["ColSpan"].ToString())) m.ColSpan = (int)dr["ColSpan"];
            if (!string.IsNullOrEmpty(dr["RegularExpression"].ToString())) m.RegularExpression = (string)dr["RegularExpression"];
            if (!string.IsNullOrEmpty(dr["FormatString"].ToString())) m.FormatString = (string)dr["FormatString"];
            if (!string.IsNullOrEmpty(dr["SearchPageURL"].ToString())) m.SearchPageURL = (string)dr["SearchPageURL"];
            if (!string.IsNullOrEmpty(dr["TreeLevel"].ToString())) m.TreeLevel = (int)dr["TreeLevel"];
			return m;
		}
        #endregion
	}
}

