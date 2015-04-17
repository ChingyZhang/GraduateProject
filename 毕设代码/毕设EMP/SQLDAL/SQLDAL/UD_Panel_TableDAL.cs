
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
//using MCSFramework.Common;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model;
using MCSFramework.Common;


namespace MCSFramework.SQLDAL
{
	/// <summary>
	///UD_Panel_Table数据访问DAL类
	/// </summary>
	public class UD_Panel_TableDAL : BaseSimpleDAL<UD_Panel_Table>
	{
		#region 构造函数
		///<summary>
		///
		///</summary>
		public UD_Panel_TableDAL()
		{
			_ProcePrefix = "MCS_SYS.dbo.sp_UD_Panel_Table";
		}
		#endregion
		
		
		#region 成员方法
		/// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(UD_Panel_Table m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.UniqueIdentifier, 16, m.ID),
				SQLDatabase.MakeInParam("@PanelID", SqlDbType.UniqueIdentifier, 16, m.PanelID),
				SQLDatabase.MakeInParam("@TableID", SqlDbType.UniqueIdentifier, 16, m.TableID)
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
        public override int Update(UD_Panel_Table m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.UniqueIdentifier, 16, m.ID),
				SQLDatabase.MakeInParam("@PanelID", SqlDbType.UniqueIdentifier, 16, m.PanelID),
				SQLDatabase.MakeInParam("@TableID", SqlDbType.UniqueIdentifier, 16, m.TableID)
			};
			#endregion
			
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
			return ret;
        }
		
        protected override UD_Panel_Table FillModel(IDataReader dr)
		{
			UD_Panel_Table m = new UD_Panel_Table();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (Guid)dr["ID"];
            if (!string.IsNullOrEmpty(dr["PanelID"].ToString())) m.PanelID = (Guid)dr["PanelID"];
            if (!string.IsNullOrEmpty(dr["TableID"].ToString())) m.TableID = (Guid)dr["TableID"];
						
			return m;
		}
        #endregion

        /// <summary>
        /// 获取指定模块包含的表
        /// </summary>
        /// <param name="PanelID"></param>
        /// <returns></returns>
        public DataTable GetTableListByPanelID(Guid PanelID)
        {
            SqlDataReader dr = null;
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Condition", SqlDbType.NVarChar, 2000,  "UD_Panel_Table.PanelID='"+PanelID.ToString()+"'")
			};
            SQLDatabase.RunProc("sp_UD_Panel_Table_GetByCondition", prams, out dr);
            return Tools.ConvertDataReaderToDataTable(dr);
        }
	}
}

