
// ===================================================================
// 文件： AC_AccountQuarterDAL.cs
// 项目名称：
// 创建时间：2013-08-02
// 作者:	   Jace
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.Pub;


namespace MCSFramework.SQLDAL.Pub
{
    /// <summary>
    ///AC_AccountQuarter数据访问DAL类
    /// </summary>
    public class AC_AccountQuarterDAL : BaseSimpleDAL<AC_AccountQuarter>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public AC_AccountQuarterDAL()
        {
            _ProcePrefix = "MCS_Pub.dbo.sp_AC_AccountQuarter";
        }
        #endregion


        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(AC_AccountQuarter m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Name", SqlDbType.VarChar, 50, m.Name),
				SQLDatabase.MakeInParam("@BeginMonth", SqlDbType.Int, 4, m.BeginMonth),
				SQLDatabase.MakeInParam("@EndMonth", SqlDbType.Int, 4, m.EndMonth),
				SQLDatabase.MakeInParam("@Year", SqlDbType.Int, 4, m.Year),
				SQLDatabase.MakeInParam("@Quarter", SqlDbType.Int, 4, m.Quarter),
				SQLDatabase.MakeInParam("@BeginDate", SqlDbType.DateTime, 8, m.BeginDate),
				SQLDatabase.MakeInParam("@EndDate", SqlDbType.DateTime, 8, m.EndDate)
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
        public override int Update(AC_AccountQuarter m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@Name", SqlDbType.VarChar, 50, m.Name),
				SQLDatabase.MakeInParam("@BeginMonth", SqlDbType.Int, 4, m.BeginMonth),
				SQLDatabase.MakeInParam("@EndMonth", SqlDbType.Int, 4, m.EndMonth),
				SQLDatabase.MakeInParam("@Year", SqlDbType.Int, 4, m.Year),
				SQLDatabase.MakeInParam("@Quarter", SqlDbType.Int, 4, m.Quarter),
				SQLDatabase.MakeInParam("@BeginDate", SqlDbType.DateTime, 8, m.BeginDate),
				SQLDatabase.MakeInParam("@EndDate", SqlDbType.DateTime, 8, m.EndDate)
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override AC_AccountQuarter FillModel(IDataReader dr)
        {
            AC_AccountQuarter m = new AC_AccountQuarter();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["Name"].ToString())) m.Name = (string)dr["Name"];
            if (!string.IsNullOrEmpty(dr["BeginMonth"].ToString())) m.BeginMonth = (int)dr["BeginMonth"];
            if (!string.IsNullOrEmpty(dr["EndMonth"].ToString())) m.EndMonth = (int)dr["EndMonth"];
            if (!string.IsNullOrEmpty(dr["Year"].ToString())) m.Year = (int)dr["Year"];
            if (!string.IsNullOrEmpty(dr["Quarter"].ToString())) m.Quarter = (int)dr["Quarter"];
            if (!string.IsNullOrEmpty(dr["BeginDate"].ToString())) m.BeginDate = (DateTime)dr["BeginDate"];
            if (!string.IsNullOrEmpty(dr["EndDate"].ToString())) m.EndDate = (DateTime)dr["EndDate"];

            return m;
        }
    }
}

