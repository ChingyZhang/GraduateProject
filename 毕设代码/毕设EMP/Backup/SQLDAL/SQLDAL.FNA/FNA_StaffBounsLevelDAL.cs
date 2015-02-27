
// ===================================================================
// 文件： FNA_StaffBounsLevelDAL.cs
// 项目名称：
// 创建时间：2013-08-02
// 作者:	   Jace
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.FNA;
using MCSFramework.Common;


namespace MCSFramework.SQLDAL.FNA
{
    /// <summary>
    ///FNA_StaffBounsLevel数据访问DAL类
    /// </summary>
    public class FNA_StaffBounsLevelDAL : BaseSimpleDAL<FNA_StaffBounsLevel>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public FNA_StaffBounsLevelDAL()
        {
            _ProcePrefix = "MCS_FNA.dbo.sp_FNA_StaffBounsLevel";
        }
        #endregion


        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(FNA_StaffBounsLevel m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, m.OrganizeCity),
				SQLDatabase.MakeInParam("@Quarter", SqlDbType.Int, 4, m.Quarter),
				SQLDatabase.MakeInParam("@BegainMonth", SqlDbType.Int, 4, m.BegainMonth),
				SQLDatabase.MakeInParam("@EndMonth", SqlDbType.Int, 4, m.EndMonth),
				SQLDatabase.MakeInParam("@Level", SqlDbType.Int, 4, m.Level),
				SQLDatabase.MakeInParam("@SalesVolume1", SqlDbType.Decimal, 9, m.SalesVolume1),
				SQLDatabase.MakeInParam("@SalesVolume2", SqlDbType.Decimal, 9, m.SalesVolume2),
				SQLDatabase.MakeInParam("@Bouns", SqlDbType.Decimal, 9, m.Bouns),
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
        public override int Update(FNA_StaffBounsLevel m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, m.OrganizeCity),
				SQLDatabase.MakeInParam("@Quarter", SqlDbType.Int, 4, m.Quarter),
				SQLDatabase.MakeInParam("@BegainMonth", SqlDbType.Int, 4, m.BegainMonth),
				SQLDatabase.MakeInParam("@EndMonth", SqlDbType.Int, 4, m.EndMonth),
				SQLDatabase.MakeInParam("@Level", SqlDbType.Int, 4, m.Level),
				SQLDatabase.MakeInParam("@SalesVolume1", SqlDbType.Decimal, 9, m.SalesVolume1),
				SQLDatabase.MakeInParam("@SalesVolume2", SqlDbType.Decimal, 9, m.SalesVolume2),
				SQLDatabase.MakeInParam("@Bouns", SqlDbType.Decimal, 9, m.Bouns),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@UpdateStaff", SqlDbType.Int, 4, m.UpdateStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override FNA_StaffBounsLevel FillModel(IDataReader dr)
        {
            FNA_StaffBounsLevel m = new FNA_StaffBounsLevel();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["OrganizeCity"].ToString())) m.OrganizeCity = (int)dr["OrganizeCity"];
            if (!string.IsNullOrEmpty(dr["Quarter"].ToString())) m.Quarter = (int)dr["Quarter"];
            if (!string.IsNullOrEmpty(dr["BegainMonth"].ToString())) m.BegainMonth = (int)dr["BegainMonth"];
            if (!string.IsNullOrEmpty(dr["EndMonth"].ToString())) m.EndMonth = (int)dr["EndMonth"];
            if (!string.IsNullOrEmpty(dr["Level"].ToString())) m.Level = (int)dr["Level"];
            if (!string.IsNullOrEmpty(dr["SalesVolume1"].ToString())) m.SalesVolume1 = (decimal)dr["SalesVolume1"];
            if (!string.IsNullOrEmpty(dr["SalesVolume2"].ToString())) m.SalesVolume2 = (decimal)dr["SalesVolume2"];
            if (!string.IsNullOrEmpty(dr["Bouns"].ToString())) m.Bouns = (decimal)dr["Bouns"];
            if (!string.IsNullOrEmpty(dr["ApproveFlag"].ToString())) m.ApproveFlag = (int)dr["ApproveFlag"];
            if (!string.IsNullOrEmpty(dr["InsertTime"].ToString())) m.InsertTime = (DateTime)dr["InsertTime"];
            if (!string.IsNullOrEmpty(dr["InsertStaff"].ToString())) m.InsertStaff = (int)dr["InsertStaff"];
            if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString())) m.UpdateTime = (DateTime)dr["UpdateTime"];
            if (!string.IsNullOrEmpty(dr["UpdateStaff"].ToString())) m.UpdateStaff = (int)dr["UpdateStaff"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);

            return m;
        }

        public DataTable GetData(int quarter)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Quarter", SqlDbType.Int, 4, quarter)
                                   };
            #endregion
            SqlDataReader dr = null;
            SQLDatabase.RunProc(_ProcePrefix + "_GetData", prams, out dr);
            return Tools.ConvertDataReaderToDataTable(dr);
        }

        public int ChageApproveState(int quarter,int approveflag)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Quarter", SqlDbType.Int, 4, quarter),
                SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4,approveflag)
                                   };
            #endregion
            return SQLDatabase.RunProc(_ProcePrefix + "_ChageApproveState", prams);
        }
        public int GetApproveState(int quarter)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Quarter", SqlDbType.Int, 4, quarter)
                                   };
            #endregion
            return SQLDatabase.RunProc(_ProcePrefix + "_GetApproveState", prams);
        }
    }
}

