
// ===================================================================
// 文件： Addr_OfficialCityPopulationDAL.cs
// 项目名称：
// 创建时间：2010/12/17
// 作者:	   
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
    ///Addr_OfficialCityPopulation数据访问DAL类
    /// </summary>
    public class Addr_OfficialCityPopulationDAL : BaseSimpleDAL<Addr_OfficialCityPopulation>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public Addr_OfficialCityPopulationDAL()
        {
            _ProcePrefix = "MCS_SYS.dbo.sp_Addr_OfficialCityPopulation";
        }
        #endregion


        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(Addr_OfficialCityPopulation m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@TotalPopulation", SqlDbType.Int, 4, m.TotalPopulation),
				SQLDatabase.MakeInParam("@ManPopulation", SqlDbType.Int, 4, m.ManPopulation),
				SQLDatabase.MakeInParam("@FemalePopulation", SqlDbType.Int, 4, m.FemalePopulation),
				SQLDatabase.MakeInParam("@F1", SqlDbType.Int, 4, m.F1),
				SQLDatabase.MakeInParam("@F2", SqlDbType.Int, 4, m.F2),
				SQLDatabase.MakeInParam("@F3", SqlDbType.Int, 4, m.F3),
				SQLDatabase.MakeInParam("@F4", SqlDbType.Int, 4, m.F4),
				SQLDatabase.MakeInParam("@F5", SqlDbType.Int, 4, m.F5),
				SQLDatabase.MakeInParam("@F6", SqlDbType.Int, 4, m.F6),
				SQLDatabase.MakeInParam("@F7", SqlDbType.Int, 4, m.F7),
				SQLDatabase.MakeInParam("@F8", SqlDbType.Int, 4, m.F8),
				SQLDatabase.MakeInParam("@F9", SqlDbType.Int, 4, m.F9),
				SQLDatabase.MakeInParam("@F10", SqlDbType.Int, 4, m.F10),
				SQLDatabase.MakeInParam("@F11", SqlDbType.Int, 4, m.F11),
				SQLDatabase.MakeInParam("@F12", SqlDbType.Int, 4, m.F12),
				SQLDatabase.MakeInParam("@F13", SqlDbType.Int, 4, m.F13),
				SQLDatabase.MakeInParam("@F14", SqlDbType.Int, 4, m.F14),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
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
        public override int Update(Addr_OfficialCityPopulation m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@TotalPopulation", SqlDbType.Int, 4, m.TotalPopulation),
				SQLDatabase.MakeInParam("@ManPopulation", SqlDbType.Int, 4, m.ManPopulation),
				SQLDatabase.MakeInParam("@FemalePopulation", SqlDbType.Int, 4, m.FemalePopulation),
				SQLDatabase.MakeInParam("@F1", SqlDbType.Int, 4, m.F1),
				SQLDatabase.MakeInParam("@F2", SqlDbType.Int, 4, m.F2),
				SQLDatabase.MakeInParam("@F3", SqlDbType.Int, 4, m.F3),
				SQLDatabase.MakeInParam("@F4", SqlDbType.Int, 4, m.F4),
				SQLDatabase.MakeInParam("@F5", SqlDbType.Int, 4, m.F5),
				SQLDatabase.MakeInParam("@F6", SqlDbType.Int, 4, m.F6),
				SQLDatabase.MakeInParam("@F7", SqlDbType.Int, 4, m.F7),
				SQLDatabase.MakeInParam("@F8", SqlDbType.Int, 4, m.F8),
				SQLDatabase.MakeInParam("@F9", SqlDbType.Int, 4, m.F9),
				SQLDatabase.MakeInParam("@F10", SqlDbType.Int, 4, m.F10),
				SQLDatabase.MakeInParam("@F11", SqlDbType.Int, 4, m.F11),
				SQLDatabase.MakeInParam("@F12", SqlDbType.Int, 4, m.F12),
				SQLDatabase.MakeInParam("@F13", SqlDbType.Int, 4, m.F13),
				SQLDatabase.MakeInParam("@F14", SqlDbType.Int, 4, m.F14),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override Addr_OfficialCityPopulation FillModel(IDataReader dr)
        {
            Addr_OfficialCityPopulation m = new Addr_OfficialCityPopulation();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["TotalPopulation"].ToString())) m.TotalPopulation = (int)dr["TotalPopulation"];
            if (!string.IsNullOrEmpty(dr["ManPopulation"].ToString())) m.ManPopulation = (int)dr["ManPopulation"];
            if (!string.IsNullOrEmpty(dr["FemalePopulation"].ToString())) m.FemalePopulation = (int)dr["FemalePopulation"];
            if (!string.IsNullOrEmpty(dr["F1"].ToString())) m.F1 = (int)dr["F1"];
            if (!string.IsNullOrEmpty(dr["F2"].ToString())) m.F2 = (int)dr["F2"];
            if (!string.IsNullOrEmpty(dr["F3"].ToString())) m.F3 = (int)dr["F3"];
            if (!string.IsNullOrEmpty(dr["F4"].ToString())) m.F4 = (int)dr["F4"];
            if (!string.IsNullOrEmpty(dr["F5"].ToString())) m.F5 = (int)dr["F5"];
            if (!string.IsNullOrEmpty(dr["F6"].ToString())) m.F6 = (int)dr["F6"];
            if (!string.IsNullOrEmpty(dr["F7"].ToString())) m.F7 = (int)dr["F7"];
            if (!string.IsNullOrEmpty(dr["F8"].ToString())) m.F8 = (int)dr["F8"];
            if (!string.IsNullOrEmpty(dr["F9"].ToString())) m.F9 = (int)dr["F9"];
            if (!string.IsNullOrEmpty(dr["F10"].ToString())) m.F10 = (int)dr["F10"];
            if (!string.IsNullOrEmpty(dr["F11"].ToString())) m.F11 = (int)dr["F11"];
            if (!string.IsNullOrEmpty(dr["F12"].ToString())) m.F12 = (int)dr["F12"];
            if (!string.IsNullOrEmpty(dr["F13"].ToString())) m.F13 = (int)dr["F13"];
            if (!string.IsNullOrEmpty(dr["F14"].ToString())) m.F14 = (int)dr["F14"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);

            return m;
        }
    }
}

