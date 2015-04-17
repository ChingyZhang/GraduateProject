
// ===================================================================
// 文件： Rpt_FolderDAL.cs
// 项目名称：
// 创建时间：2010/9/25
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.RPT;


namespace MCSFramework.SQLDAL.RPT
{
    /// <summary>
    ///Rpt_Folder数据访问DAL类
    /// </summary>
    public class Rpt_FolderDAL : BaseSimpleDAL<Rpt_Folder>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public Rpt_FolderDAL()
        {
            _ProcePrefix = "MCS_Reports.dbo.sp_Rpt_Folder";
        }
        #endregion


        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(Rpt_Folder m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Name", SqlDbType.VarChar, 50, m.Name),
				SQLDatabase.MakeInParam("@SuperID", SqlDbType.Int, 4, m.SuperID),
				SQLDatabase.MakeInParam("@Level", SqlDbType.Int, 4, m.Level)
			};
            #endregion

            m.ID = SQLDatabase.RunProc(_ProcePrefix + "_Add", prams);

            return m.ID;
        }

        /// <summary>
        /// 更新项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Update(Rpt_Folder m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@Name", SqlDbType.VarChar, 50, m.Name),
				SQLDatabase.MakeInParam("@SuperID", SqlDbType.Int, 4, m.SuperID),
				SQLDatabase.MakeInParam("@Level", SqlDbType.Int, 4, m.Level)
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override Rpt_Folder FillModel(IDataReader dr)
        {
            Rpt_Folder m = new Rpt_Folder();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["Name"].ToString())) m.Name = (string)dr["Name"];
            if (!string.IsNullOrEmpty(dr["SuperID"].ToString())) m.SuperID = (int)dr["SuperID"];
            if (!string.IsNullOrEmpty(dr["Level"].ToString())) m.Level = (int)dr["Level"];

            return m;
        }
    }
}

