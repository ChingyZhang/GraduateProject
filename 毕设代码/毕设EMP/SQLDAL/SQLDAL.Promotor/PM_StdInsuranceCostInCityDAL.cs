
// ===================================================================
// 文件： PM_StdInsuranceCostInCityDAL.cs
// 项目名称：
// 创建时间：2014/2/24
// 作者:	   Jace
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.Promotor;


namespace MCSFramework.SQLDAL.Promotor
{
    /// <summary>
    ///PM_StdInsuranceCostInCity数据访问DAL类
    /// </summary>
    public class PM_StdInsuranceCostInCityDAL : BaseSimpleDAL<PM_StdInsuranceCostInCity>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public PM_StdInsuranceCostInCityDAL()
        {
            _ProcePrefix = "MCS_Promotor.dbo.sp_PM_StdInsuranceCostInCity";
        }
        #endregion


        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(PM_StdInsuranceCostInCity m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Insurance", SqlDbType.Int, 4, m.Insurance),
				SQLDatabase.MakeInParam("@City", SqlDbType.Int, 4, m.City),
				SQLDatabase.MakeInParam("@InsertStaff", SqlDbType.Int, 4, m.InsertStaff),
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
        public override int Update(PM_StdInsuranceCostInCity m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@Insurance", SqlDbType.Int, 4, m.Insurance),
				SQLDatabase.MakeInParam("@City", SqlDbType.Int, 4, m.City),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override PM_StdInsuranceCostInCity FillModel(IDataReader dr)
        {
            PM_StdInsuranceCostInCity m = new PM_StdInsuranceCostInCity();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["Insurance"].ToString())) m.Insurance = (int)dr["Insurance"];
            if (!string.IsNullOrEmpty(dr["City"].ToString())) m.City = (int)dr["City"];
            if (!string.IsNullOrEmpty(dr["InsertTime"].ToString())) m.InsertTime = (DateTime)dr["InsertTime"];
            if (!string.IsNullOrEmpty(dr["InsertStaff"].ToString())) m.InsertStaff = (int)dr["InsertStaff"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);

            return m;
        }
    }
}

