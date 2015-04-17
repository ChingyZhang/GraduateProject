
// ===================================================================
// 文件： Addr_OrganizeCityDAL.cs
// 项目名称：
// 创建时间：2008-12-17
// 作者:	   
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.Model;
using MCSFramework.Common;
//using MCSFramework.Common;


namespace MCSFramework.SQLDAL
{
    /// <summary>
    ///Addr_OrganizeCity数据访问DAL类
    /// </summary>
    public class Addr_OrganizeCityDAL : BaseSimpleDAL<Addr_OrganizeCity>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public Addr_OrganizeCityDAL()
        {
            _ProcePrefix = "MCS_SYS.dbo.sp_Addr_OrganizeCity";
        }
        #endregion


        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(Addr_OrganizeCity m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Name", SqlDbType.VarChar, 200, m.Name),
				SQLDatabase.MakeInParam("@SuperID", SqlDbType.Int, 4, m.SuperID),
				SQLDatabase.MakeInParam("@Level", SqlDbType.Int, 4, m.Level),
				SQLDatabase.MakeInParam("@Code", SqlDbType.VarChar, 20, m.Code),
				SQLDatabase.MakeInParam("@Manager", SqlDbType.Int, 4, m.Manager),
                SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 8000, CombineExtProperty(m.ExtPropertys,m.ModelName))
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
        public override int Update(Addr_OrganizeCity m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@Name", SqlDbType.VarChar, 200, m.Name),
				SQLDatabase.MakeInParam("@SuperID", SqlDbType.Int, 4, m.SuperID),
				SQLDatabase.MakeInParam("@Level", SqlDbType.Int, 4, m.Level),
				SQLDatabase.MakeInParam("@Code", SqlDbType.VarChar, 20, m.Code),
				SQLDatabase.MakeInParam("@Manager", SqlDbType.Int, 4, m.Manager),
                SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 8000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override Addr_OrganizeCity FillModel(IDataReader dr)
        {
            Addr_OrganizeCity m = new Addr_OrganizeCity();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["Name"].ToString())) m.Name = (string)dr["Name"];
            if (!string.IsNullOrEmpty(dr["SuperID"].ToString())) m.SuperID = (int)dr["SuperID"];
            if (!string.IsNullOrEmpty(dr["Level"].ToString())) m.Level = (int)dr["Level"];
            if (!string.IsNullOrEmpty(dr["Code"].ToString())) m.Code = (string)dr["Code"];
            if (!string.IsNullOrEmpty(dr["Manager"].ToString())) m.Manager = (int)dr["Manager"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);
            return m;
        }

        public DataTable GetAllOrganizeCity()
        {
            SqlDataReader dr = null;
            SqlParameter[] prams = {
            SQLDatabase.MakeInParam("@Condition", SqlDbType.VarChar, 2000,""),
                                   };
            SQLDatabase.RunProc(_ProcePrefix + "_GetByCondition", prams, out dr);

            return Tools.ConvertDataReaderToDataTable(dr);
        }

        /// <summary>
        /// 与员工表同步办事区、区域、城市的负责经理
        /// </summary>
        /// <returns></returns>
        public int SyncManager()
        {
            return SQLDatabase.RunProc(_ProcePrefix + "_SyncManager");
        }
    }
}

