
// ===================================================================
// 文件： FNA_EvectionRouteDAL.cs
// 项目名称：
// 创建时间：2010/8/3
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.FNA;


namespace MCSFramework.SQLDAL.FNA
{
    /// <summary>
    ///FNA_EvectionRoute数据访问DAL类
    /// </summary>
    public class FNA_EvectionRouteDAL : BaseSimpleDAL<FNA_EvectionRoute>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public FNA_EvectionRouteDAL()
        {
            _ProcePrefix = "MCS_FNA.dbo.sp_FNA_EvectionRoute";
        }
        #endregion


        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(FNA_EvectionRoute m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@WriteOffID", SqlDbType.Int, 4, m.WriteOffID),
				SQLDatabase.MakeInParam("@RelateStaff", SqlDbType.Int, 4, m.RelateStaff),
				SQLDatabase.MakeInParam("@BeginDate", SqlDbType.DateTime, 8, m.BeginDate),
				SQLDatabase.MakeInParam("@EndDate", SqlDbType.DateTime, 8, m.EndDate),
				SQLDatabase.MakeInParam("@EvectionLine", SqlDbType.VarChar, 50, m.EvectionLine),
				SQLDatabase.MakeInParam("@Transport", SqlDbType.VarChar, 20, m.Transport),
				SQLDatabase.MakeInParam("@Cost1", SqlDbType.Decimal, 9, m.Cost1),
				SQLDatabase.MakeInParam("@Cost2", SqlDbType.Decimal, 9, m.Cost2),
				SQLDatabase.MakeInParam("@Cost3", SqlDbType.Decimal, 9, m.Cost3),
				SQLDatabase.MakeInParam("@Cost4", SqlDbType.Decimal, 9, m.Cost4),
				SQLDatabase.MakeInParam("@Cost5", SqlDbType.Decimal, 9, m.Cost5),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 2000, m.Remark),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
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
        public override int Update(FNA_EvectionRoute m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@WriteOffID", SqlDbType.Int, 4, m.WriteOffID),
				SQLDatabase.MakeInParam("@RelateStaff", SqlDbType.Int, 4, m.RelateStaff),
				SQLDatabase.MakeInParam("@BeginDate", SqlDbType.DateTime, 8, m.BeginDate),
				SQLDatabase.MakeInParam("@EndDate", SqlDbType.DateTime, 8, m.EndDate),
				SQLDatabase.MakeInParam("@EvectionLine", SqlDbType.VarChar, 50, m.EvectionLine),
				SQLDatabase.MakeInParam("@Transport", SqlDbType.VarChar, 20, m.Transport),
				SQLDatabase.MakeInParam("@Cost1", SqlDbType.Decimal, 9, m.Cost1),
				SQLDatabase.MakeInParam("@Cost2", SqlDbType.Decimal, 9, m.Cost2),
				SQLDatabase.MakeInParam("@Cost3", SqlDbType.Decimal, 9, m.Cost3),
				SQLDatabase.MakeInParam("@Cost4", SqlDbType.Decimal, 9, m.Cost4),
				SQLDatabase.MakeInParam("@Cost5", SqlDbType.Decimal, 9, m.Cost5),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 2000, m.Remark),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@UpdateStaff", SqlDbType.Int, 4, m.UpdateStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override FNA_EvectionRoute FillModel(IDataReader dr)
        {
            FNA_EvectionRoute m = new FNA_EvectionRoute();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["WriteOffID"].ToString())) m.WriteOffID = (int)dr["WriteOffID"];
            if (!string.IsNullOrEmpty(dr["RelateStaff"].ToString())) m.RelateStaff = (int)dr["RelateStaff"];
            if (!string.IsNullOrEmpty(dr["BeginDate"].ToString())) m.BeginDate = (DateTime)dr["BeginDate"];
            if (!string.IsNullOrEmpty(dr["EndDate"].ToString())) m.EndDate = (DateTime)dr["EndDate"];
            if (!string.IsNullOrEmpty(dr["EvectionLine"].ToString())) m.EvectionLine = (string)dr["EvectionLine"];
            if (!string.IsNullOrEmpty(dr["Transport"].ToString())) m.Transport = (string)dr["Transport"];
            if (!string.IsNullOrEmpty(dr["Cost1"].ToString())) m.Cost1 = (decimal)dr["Cost1"];
            if (!string.IsNullOrEmpty(dr["Cost2"].ToString())) m.Cost2 = (decimal)dr["Cost2"];
            if (!string.IsNullOrEmpty(dr["Cost3"].ToString())) m.Cost3 = (decimal)dr["Cost3"];
            if (!string.IsNullOrEmpty(dr["Cost4"].ToString())) m.Cost4 = (decimal)dr["Cost4"];
            if (!string.IsNullOrEmpty(dr["Cost5"].ToString())) m.Cost5 = (decimal)dr["Cost5"];
            if (!string.IsNullOrEmpty(dr["Remark"].ToString())) m.Remark = (string)dr["Remark"];
            if (!string.IsNullOrEmpty(dr["ApproveFlag"].ToString())) m.ApproveFlag = (int)dr["ApproveFlag"];
            if (!string.IsNullOrEmpty(dr["InsertTime"].ToString())) m.InsertTime = (DateTime)dr["InsertTime"];
            if (!string.IsNullOrEmpty(dr["InsertStaff"].ToString())) m.InsertStaff = (int)dr["InsertStaff"];
            if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString())) m.UpdateTime = (DateTime)dr["UpdateTime"];
            if (!string.IsNullOrEmpty(dr["UpdateStaff"].ToString())) m.UpdateStaff = (int)dr["UpdateStaff"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);

            return m;
        }
    }
}

