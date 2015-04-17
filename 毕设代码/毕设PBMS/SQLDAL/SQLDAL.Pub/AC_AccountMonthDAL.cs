
// ===================================================================
// 文件： AC_AccountMonthDAL.cs
// 项目名称：
// 创建时间：2008-12-22
// 作者:	   
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
	///AC_AccountMonth数据访问DAL类
	/// </summary>
	public class AC_AccountMonthDAL : BaseSimpleDAL<AC_AccountMonth>
	{
		#region 构造函数
		///<summary>
		///
		///</summary>
		public AC_AccountMonthDAL()
		{
			_ProcePrefix = "MCS_Pub.dbo.sp_AC_AccountMonth";
		}
		#endregion
		
		
		/// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(AC_AccountMonth m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Name", SqlDbType.VarChar, 50, m.Name),
				SQLDatabase.MakeInParam("@BeginDate", SqlDbType.DateTime, 8, m.BeginDate),
				SQLDatabase.MakeInParam("@EndDate", SqlDbType.DateTime, 8, m.EndDate),
				SQLDatabase.MakeInParam("@Year", SqlDbType.Int, 4, m.Year),
				SQLDatabase.MakeInParam("@Month", SqlDbType.Int, 4, m.Month)
			};
			#endregion
			
            m.ID =  SQLDatabase.RunProc(_ProcePrefix + "_Add", prams);
			
            return m.ID;
        }
		
		/// <summary>
        /// 更新项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Update(AC_AccountMonth m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@Name", SqlDbType.VarChar, 50, m.Name),
				SQLDatabase.MakeInParam("@BeginDate", SqlDbType.DateTime, 8, m.BeginDate),
				SQLDatabase.MakeInParam("@EndDate", SqlDbType.DateTime, 8, m.EndDate),
				SQLDatabase.MakeInParam("@Year", SqlDbType.Int, 4, m.Year),
				SQLDatabase.MakeInParam("@Month", SqlDbType.Int, 4, m.Month)
			};
			#endregion
			
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
			
			return ret;
        }
		
        protected override AC_AccountMonth FillModel(IDataReader dr)
		{
			AC_AccountMonth m = new AC_AccountMonth();
			if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (int)dr["ID"];
			if (!string.IsNullOrEmpty(dr["Name"].ToString()))	m.Name = (string)dr["Name"];
			if (!string.IsNullOrEmpty(dr["BeginDate"].ToString()))	m.BeginDate = (DateTime)dr["BeginDate"];
			if (!string.IsNullOrEmpty(dr["EndDate"].ToString()))	m.EndDate = (DateTime)dr["EndDate"];
			if (!string.IsNullOrEmpty(dr["Year"].ToString()))	m.Year = (int)dr["Year"];
			if (!string.IsNullOrEmpty(dr["Month"].ToString()))	m.Month = (int)dr["Month"];
						
			return m;
		}

        #region 静态方法

        /// <summary>
        /// 根据某个日期获取其所属的会计月
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public int GetMonthByDate(DateTime date)
        {
            SqlParameter[] parms ={
                SQLDatabase.MakeInParam("@Date", SqlDbType.DateTime,8,date)
            };

            return SQLDatabase.RunProc(_ProcePrefix + "_GetMonthByDate", parms);

            //if (dt != null && dt.Rows.Count > 0)
            //{
            //    return (int)dt.Rows[0]["ID"];
            //}
            //return 0;
        }

        /// <summary>
        /// 获取目前会计月表中所有的年份
        /// </summary>
        /// <returns></returns>
        public SqlDataReader GetAllYear()
        {
            SqlDataReader dr = null;
            SQLDatabase.RunProc(_ProcePrefix + "_GetAllYear", out dr);
            return (dr);
        }

        /// <summary>
        /// 获取某年份包含的会计月
        /// </summary>
        /// <param name="AccountYear"></param>
        /// <returns></returns>
        public SqlDataReader GetAccountMonthByYear(int AccountYear)
        {
            SqlDataReader dr = null;
            SqlParameter[] parms ={
                SQLDatabase.MakeInParam("@AccountYear", SqlDbType.Int,4,AccountYear)
            };
            SQLDatabase.RunProc(_ProcePrefix + "_GetAccountMonthByYear", parms, out dr);
            return (dr);
        }

        /// <summary>
        /// 获取某时间段跨越的会计月
        /// </summary>
        /// <param name="Begindate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public SqlDataReader GetByDateRegion(DateTime Begindate, DateTime EndDate)
        {
            SqlDataReader dr = null;
            SqlParameter[] parms ={
                SQLDatabase.MakeInParam("@Begindate", SqlDbType.DateTime,8,Begindate),
                SQLDatabase.MakeInParam("@EndDate", SqlDbType.DateTime,8,EndDate)
            };
            SQLDatabase.RunProc(_ProcePrefix + "_GetByDateRegion", parms, out dr);
            return (dr);
        }
        #endregion
    }
}

