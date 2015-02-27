
// ===================================================================
// 文件： UD_WebPageControlDAL.cs
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
    ///UD_WebPageControl数据访问DAL类
    /// </summary>
    public class UD_WebPageControlDAL : BaseSimpleDAL<UD_WebPageControl>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public UD_WebPageControlDAL()
        {
            _ProcePrefix = "MCS_SYS.dbo.sp_UD_WebPageControl";
        }
        #endregion


        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(UD_WebPageControl m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.UniqueIdentifier, 16, m.ID),
				SQLDatabase.MakeInParam("@WebPageID", SqlDbType.UniqueIdentifier, 16, m.WebPageID),
				SQLDatabase.MakeInParam("@ControlName", SqlDbType.VarChar, 100, m.ControlName),
				SQLDatabase.MakeInParam("@ControlIndex", SqlDbType.Int, 4, m.ControlIndex),
				SQLDatabase.MakeInParam("@Text", SqlDbType.VarChar, 200, m.Text),
				SQLDatabase.MakeInParam("@VisibleActionCode", SqlDbType.VarChar, 200, m.VisibleActionCode),
				SQLDatabase.MakeInParam("@EnableActionCode", SqlDbType.VarChar, 200, m.EnableActionCode),
				SQLDatabase.MakeInParam("@Description", SqlDbType.VarChar, 200, m.Description),
				SQLDatabase.MakeInParam("@ControlType", SqlDbType.VarChar, 200, m.ControlType),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 8000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            return SQLDatabase.RunProc(_ProcePrefix + "_Add", prams);
        }

        /// <summary>
        /// 更新项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Update(UD_WebPageControl m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.UniqueIdentifier, 16, m.ID),
				SQLDatabase.MakeInParam("@WebPageID", SqlDbType.UniqueIdentifier, 16, m.WebPageID),
				SQLDatabase.MakeInParam("@ControlName", SqlDbType.VarChar, 100, m.ControlName),
				SQLDatabase.MakeInParam("@ControlIndex", SqlDbType.Int, 4, m.ControlIndex),
				SQLDatabase.MakeInParam("@Text", SqlDbType.VarChar, 200, m.Text),
				SQLDatabase.MakeInParam("@VisibleActionCode", SqlDbType.VarChar, 200, m.VisibleActionCode),
				SQLDatabase.MakeInParam("@EnableActionCode", SqlDbType.VarChar, 200, m.EnableActionCode),
				SQLDatabase.MakeInParam("@Description", SqlDbType.VarChar, 200, m.Description),
				SQLDatabase.MakeInParam("@ControlType", SqlDbType.VarChar, 200, m.ControlType),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 8000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override UD_WebPageControl FillModel(IDataReader dr)
        {
            UD_WebPageControl m = new UD_WebPageControl();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (Guid)dr["ID"];
            if (!string.IsNullOrEmpty(dr["WebPageID"].ToString())) m.WebPageID = (Guid)dr["WebPageID"];
            if (!string.IsNullOrEmpty(dr["ControlName"].ToString())) m.ControlName = (string)dr["ControlName"];
            if (!string.IsNullOrEmpty(dr["ControlIndex"].ToString())) m.ControlIndex = (int)dr["ControlIndex"];
            if (!string.IsNullOrEmpty(dr["Text"].ToString())) m.Text = (string)dr["Text"];
            if (!string.IsNullOrEmpty(dr["VisibleActionCode"].ToString())) m.VisibleActionCode = (string)dr["VisibleActionCode"];
            if (!string.IsNullOrEmpty(dr["EnableActionCode"].ToString())) m.EnableActionCode = (string)dr["EnableActionCode"];
            if (!string.IsNullOrEmpty(dr["Description"].ToString())) m.Description = (string)dr["Description"];
            if (!string.IsNullOrEmpty(dr["ControlType"].ToString())) m.ControlType = (string)dr["ControlType"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);

            return m;
        }
    }
}

