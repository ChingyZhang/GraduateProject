
// ===================================================================
// 文件： UD_WebPageDAL.cs
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
    ///UD_WebPage数据访问DAL类
    /// </summary>
    public class UD_WebPageDAL : BaseSimpleDAL<UD_WebPage>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public UD_WebPageDAL()
        {
            _ProcePrefix = "MCS_SYS.dbo.sp_UD_WebPage";
        }
        #endregion


        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(UD_WebPage m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.UniqueIdentifier, 16, m.ID),
				SQLDatabase.MakeInParam("@Path", SqlDbType.VarChar, 200, m.Path),
				SQLDatabase.MakeInParam("@Title", SqlDbType.VarChar, 200, m.Title),
                SQLDatabase.MakeInParam("@SubCode", SqlDbType.VarChar, 50, m.SubCode),
				SQLDatabase.MakeInParam("@Module", SqlDbType.Int, 4, m.Module),
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
        public override int Update(UD_WebPage m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.UniqueIdentifier, 16, m.ID),
				SQLDatabase.MakeInParam("@Path", SqlDbType.VarChar, 200, m.Path),
                SQLDatabase.MakeInParam("@SubCode", SqlDbType.VarChar, 50, m.SubCode),
				SQLDatabase.MakeInParam("@Title", SqlDbType.VarChar, 200, m.Title),
				SQLDatabase.MakeInParam("@Module", SqlDbType.Int, 4, m.Module),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 8000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override UD_WebPage FillModel(IDataReader dr)
        {
            UD_WebPage m = new UD_WebPage();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (Guid)dr["ID"];
            if (!string.IsNullOrEmpty(dr["Path"].ToString())) m.Path = (string)dr["Path"];
            if (!string.IsNullOrEmpty(dr["SubCode"].ToString())) m.SubCode = (string)dr["SubCode"];
            if (!string.IsNullOrEmpty(dr["Title"].ToString())) m.Title = (string)dr["Title"];
            if (!string.IsNullOrEmpty(dr["Module"].ToString())) m.Module = (int)dr["Module"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);

            return m;
        }
    }
}

