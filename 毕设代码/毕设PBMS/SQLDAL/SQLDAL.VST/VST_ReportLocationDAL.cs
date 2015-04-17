
// ===================================================================
// 文件： VST_ReportLocationDAL.cs
// 项目名称：
// 创建时间：2015-04-12
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.VST;


namespace MCSFramework.SQLDAL.VST
{
    /// <summary>
    ///VST_ReportLocation数据访问DAL类
    /// </summary>
    public class VST_ReportLocationDAL : BaseSimpleDAL<VST_ReportLocation>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public VST_ReportLocationDAL()
        {
            _ProcePrefix = "MCS_VST.dbo.sp_VST_ReportLocation";
        }
        #endregion


        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(VST_ReportLocation m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@RelateStaff", SqlDbType.Int, 4, m.RelateStaff),
				SQLDatabase.MakeInParam("@LocateType", SqlDbType.Int, 4, m.LocateType),
				SQLDatabase.MakeInParam("@Longitude", SqlDbType.Float, 8, m.Longitude),
				SQLDatabase.MakeInParam("@Latitude", SqlDbType.Float, 8, m.Latitude),
				SQLDatabase.MakeInParam("@DeviceCode", SqlDbType.VarChar, 50, m.DeviceCode),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 1000, m.Remark),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            m.ID = SQLDatabase.RunProc(_ProcePrefix + "_Add", prams);

            return m.ID > 0 ? 1 : (int)m.ID;
        }

        /// <summary>
        /// 更新项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Update(VST_ReportLocation m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.BigInt, 8, m.ID),
				SQLDatabase.MakeInParam("@RelateStaff", SqlDbType.Int, 4, m.RelateStaff),
				SQLDatabase.MakeInParam("@LocateType", SqlDbType.Int, 4, m.LocateType),
				SQLDatabase.MakeInParam("@Longitude", SqlDbType.Float, 8, m.Longitude),
				SQLDatabase.MakeInParam("@Latitude", SqlDbType.Float, 8, m.Latitude),
				SQLDatabase.MakeInParam("@DeviceCode", SqlDbType.VarChar, 50, m.DeviceCode),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 1000, m.Remark),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override VST_ReportLocation FillModel(IDataReader dr)
        {
            VST_ReportLocation m = new VST_ReportLocation();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (long)dr["ID"];
            if (!string.IsNullOrEmpty(dr["RelateStaff"].ToString())) m.RelateStaff = (int)dr["RelateStaff"];
            if (!string.IsNullOrEmpty(dr["LocateType"].ToString())) m.LocateType = (int)dr["LocateType"];
            if (!string.IsNullOrEmpty(dr["Longitude"].ToString())) m.Longitude = (double)dr["Longitude"];
            if (!string.IsNullOrEmpty(dr["Latitude"].ToString())) m.Latitude = (double)dr["Latitude"];
            if (!string.IsNullOrEmpty(dr["DeviceCode"].ToString())) m.DeviceCode = (string)dr["DeviceCode"];
            if (!string.IsNullOrEmpty(dr["Remark"].ToString())) m.Remark = (string)dr["Remark"];
            if (!string.IsNullOrEmpty(dr["InsertTime"].ToString())) m.InsertTime = (DateTime)dr["InsertTime"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);

            return m;
        }
    }
}

