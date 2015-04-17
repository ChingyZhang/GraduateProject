
// ===================================================================
// 文件： UD_PanelDAL.cs
// 项目名称：
// 创建时间：2009/3/7
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model;


namespace MCSFramework.SQLDAL
{
	/// <summary>
	///UD_Panel数据访问DAL类
	/// </summary>
	public class UD_PanelDAL : BaseSimpleDAL<UD_Panel>
	{
		#region 构造函数
		///<summary>
		///
		///</summary>
		public UD_PanelDAL()
		{
			_ProcePrefix = "MCS_SYS.dbo.sp_UD_Panel";
		}
		#endregion

		/// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(UD_Panel m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.UniqueIdentifier, 16, m.ID),
				SQLDatabase.MakeInParam("@Code", SqlDbType.VarChar, 50, m.Code),
				SQLDatabase.MakeInParam("@Name", SqlDbType.VarChar, 50, m.Name),
				SQLDatabase.MakeInParam("@SortID", SqlDbType.Int, 4, m.SortID),
				SQLDatabase.MakeInParam("@Enable", SqlDbType.Char, 1, m.Enable),
				SQLDatabase.MakeInParam("@Description", SqlDbType.VarChar, 2000, m.Description),
				SQLDatabase.MakeInParam("@FieldCount", SqlDbType.Int, 4, m.FieldCount),
				SQLDatabase.MakeInParam("@DisplayType", SqlDbType.Int, 4, m.DisplayType),
				SQLDatabase.MakeInParam("@DetailViewID", SqlDbType.UniqueIdentifier, 16, m.DetailViewID),
				SQLDatabase.MakeInParam("@AdvanceFind", SqlDbType.Char, 1, m.AdvanceFind),
                SQLDatabase.MakeInParam("@DefaultSortFields", SqlDbType.VarChar, 200, m.DefaultSortFields),
                SQLDatabase.MakeInParam("@TableStyle", SqlDbType.VarChar, 200, m.TableStyle)
			};
			#endregion
			
            return SQLDatabase.RunProc(_ProcePrefix + "_Add", prams);
        }
		
		/// <summary>
        /// 更新项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Update(UD_Panel m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.UniqueIdentifier, 16, m.ID),
				SQLDatabase.MakeInParam("@Code", SqlDbType.VarChar, 50, m.Code),
				SQLDatabase.MakeInParam("@Name", SqlDbType.VarChar, 50, m.Name),
				SQLDatabase.MakeInParam("@SortID", SqlDbType.Int, 4, m.SortID),
				SQLDatabase.MakeInParam("@Enable", SqlDbType.Char, 1, m.Enable),
				SQLDatabase.MakeInParam("@Description", SqlDbType.VarChar, 2000, m.Description),
				SQLDatabase.MakeInParam("@FieldCount", SqlDbType.Int, 4, m.FieldCount),
				SQLDatabase.MakeInParam("@DisplayType", SqlDbType.Int, 4, m.DisplayType),
				SQLDatabase.MakeInParam("@DetailViewID", SqlDbType.UniqueIdentifier, 16, m.DetailViewID),
				SQLDatabase.MakeInParam("@AdvanceFind", SqlDbType.Char, 1, m.AdvanceFind),
                SQLDatabase.MakeInParam("@DefaultSortFields", SqlDbType.VarChar, 200, m.DefaultSortFields),
                SQLDatabase.MakeInParam("@TableStyle", SqlDbType.VarChar, 200, m.TableStyle)
			};
			#endregion
			
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
			
			return ret;
        }
		
        protected override UD_Panel FillModel(IDataReader dr)
		{
			UD_Panel m = new UD_Panel();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (Guid)dr["ID"];
			if (!string.IsNullOrEmpty(dr["Code"].ToString()))	m.Code = (string)dr["Code"];
			if (!string.IsNullOrEmpty(dr["Name"].ToString()))	m.Name = (string)dr["Name"];
			if (!string.IsNullOrEmpty(dr["SortID"].ToString()))	m.SortID = (int)dr["SortID"];
			if (!string.IsNullOrEmpty(dr["Enable"].ToString()))	m.Enable = (string)dr["Enable"];
			if (!string.IsNullOrEmpty(dr["Description"].ToString()))	m.Description = (string)dr["Description"];
			if (!string.IsNullOrEmpty(dr["FieldCount"].ToString()))	m.FieldCount = (int)dr["FieldCount"];
			if (!string.IsNullOrEmpty(dr["DisplayType"].ToString()))	m.DisplayType = (int)dr["DisplayType"];
            if (!string.IsNullOrEmpty(dr["DetailViewID"].ToString())) m.DetailViewID = (Guid)dr["DetailViewID"];
			if (!string.IsNullOrEmpty(dr["AdvanceFind"].ToString()))	m.AdvanceFind = (string)dr["AdvanceFind"];
            if (!string.IsNullOrEmpty(dr["DefaultSortFields"].ToString())) m.DefaultSortFields = (string)dr["DefaultSortFields"];
            if (!string.IsNullOrEmpty(dr["TableStyle"].ToString())) m.TableStyle = (string)dr["TableStyle"];
						
			return m;
		}

        /// <summary>
        /// 获取某板块已分配字段的最大排序号
        /// </summary>
        /// <param name="PanelID"></param>
        /// <returns></returns>
        public int GetFieldMaxSort(Guid PanelID)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@PanelID", SqlDbType.UniqueIdentifier, 16, PanelID)
			};
            #endregion
            return SQLDatabase.RunProc(_ProcePrefix + "_GetMaxSort", prams);
        }
    }
}

