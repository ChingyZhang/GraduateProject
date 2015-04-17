
// ===================================================================
// 文件： Rpt_DataSetDAL.cs
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
using MCSFramework.Common;
using System.Collections.Generic;


namespace MCSFramework.SQLDAL.RPT
{
    /// <summary>
    ///Rpt_DataSet数据访问DAL类
    /// </summary>
    public class Rpt_DataSetDAL : BaseSimpleDAL<Rpt_DataSet>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public Rpt_DataSetDAL()
        {
            _ProcePrefix = "MCS_Reports.dbo.sp_Rpt_DataSet";
        }
        #endregion


        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(Rpt_DataSet m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.UniqueIdentifier, 16, m.ID),
				SQLDatabase.MakeInParam("@Name", SqlDbType.VarChar, 200, m.Name),
				SQLDatabase.MakeInParam("@DataSource", SqlDbType.UniqueIdentifier, 16, m.DataSource),
                SQLDatabase.MakeInParam("@Folder", SqlDbType.Int, 4, m.Folder),
				SQLDatabase.MakeInParam("@Enabled", SqlDbType.Char, 1, m.Enabled),
				SQLDatabase.MakeInParam("@CommandType", SqlDbType.Int, 4, m.CommandType),
				SQLDatabase.MakeInParam("@CommandText", SqlDbType.VarChar, 5000, m.CommandText),
				SQLDatabase.MakeInParam("@ConditionText", SqlDbType.VarChar, 2500, m.ConditionText),
				SQLDatabase.MakeInParam("@ConditionValue", SqlDbType.VarChar, 2500, m.ConditionValue),
				SQLDatabase.MakeInParam("@ConditionSQL", SqlDbType.VarChar, 2500, m.ConditionSQL),
				SQLDatabase.MakeInParam("@OrderString", SqlDbType.VarChar, 1000, m.OrderString),
				SQLDatabase.MakeInParam("@IsParamDataSet", SqlDbType.Char, 1, m.IsParamDataSet),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@InsertStaff", SqlDbType.Int, 4, m.InsertStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            return SQLDatabase.RunProc(_ProcePrefix + "_Add", prams);
        }

        /// <summary>
        /// 更新项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Update(Rpt_DataSet m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.UniqueIdentifier, 16, m.ID),
				SQLDatabase.MakeInParam("@Name", SqlDbType.VarChar, 200, m.Name),
				SQLDatabase.MakeInParam("@DataSource", SqlDbType.UniqueIdentifier, 16, m.DataSource),
                SQLDatabase.MakeInParam("@Folder", SqlDbType.Int, 4, m.Folder),
				SQLDatabase.MakeInParam("@Enabled", SqlDbType.Char, 1, m.Enabled),
				SQLDatabase.MakeInParam("@CommandType", SqlDbType.Int, 4, m.CommandType),
				SQLDatabase.MakeInParam("@CommandText", SqlDbType.VarChar, 5000, m.CommandText),
				SQLDatabase.MakeInParam("@ConditionText", SqlDbType.VarChar, 2500, m.ConditionText),
				SQLDatabase.MakeInParam("@ConditionValue", SqlDbType.VarChar, 2500, m.ConditionValue),
				SQLDatabase.MakeInParam("@ConditionSQL", SqlDbType.VarChar, 2500, m.ConditionSQL),
				SQLDatabase.MakeInParam("@OrderString", SqlDbType.VarChar, 1000, m.OrderString),
				SQLDatabase.MakeInParam("@IsParamDataSet", SqlDbType.Char, 1, m.IsParamDataSet),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@UpdateStaff", SqlDbType.Int, 4, m.UpdateStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override Rpt_DataSet FillModel(IDataReader dr)
        {
            Rpt_DataSet m = new Rpt_DataSet();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (Guid)dr["ID"];
            if (!string.IsNullOrEmpty(dr["Name"].ToString())) m.Name = (string)dr["Name"];
            if (!string.IsNullOrEmpty(dr["DataSource"].ToString())) m.DataSource = (Guid)dr["DataSource"];
            if (!string.IsNullOrEmpty(dr["Folder"].ToString())) m.Folder = (int)dr["Folder"];
            if (!string.IsNullOrEmpty(dr["Enabled"].ToString())) m.Enabled = (string)dr["Enabled"];
            if (!string.IsNullOrEmpty(dr["CommandType"].ToString())) m.CommandType = (int)dr["CommandType"];
            if (!string.IsNullOrEmpty(dr["CommandText"].ToString())) m.CommandText = (string)dr["CommandText"];
            if (!string.IsNullOrEmpty(dr["ConditionText"].ToString())) m.ConditionText = (string)dr["ConditionText"];
            if (!string.IsNullOrEmpty(dr["ConditionValue"].ToString())) m.ConditionValue = (string)dr["ConditionValue"];
            if (!string.IsNullOrEmpty(dr["ConditionSQL"].ToString())) m.ConditionSQL = (string)dr["ConditionSQL"];
            if (!string.IsNullOrEmpty(dr["OrderString"].ToString())) m.OrderString = (string)dr["OrderString"];
            if (!string.IsNullOrEmpty(dr["IsParamDataSet"].ToString())) m.IsParamDataSet = (string)dr["IsParamDataSet"];
            if (!string.IsNullOrEmpty(dr["ApproveFlag"].ToString())) m.ApproveFlag = (int)dr["ApproveFlag"];
            if (!string.IsNullOrEmpty(dr["InsertTime"].ToString())) m.InsertTime = (DateTime)dr["InsertTime"];
            if (!string.IsNullOrEmpty(dr["InsertStaff"].ToString())) m.InsertStaff = (int)dr["InsertStaff"];
            if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString())) m.UpdateTime = (DateTime)dr["UpdateTime"];
            if (!string.IsNullOrEmpty(dr["UpdateStaff"].ToString())) m.UpdateStaff = (int)dr["UpdateStaff"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);

            return m;
        }

        public DataTable GetDataFromSQL(string ConnectionString, string CommandText, SqlParameter[] param)
        {
            SqlDataReader dr = null;
            SQLDatabase.RunSQL(ConnectionString, CommandText, param, out dr, 1800);
            return Tools.ConvertDataReaderToDataTable(dr);
        }
        public DataTable GetDataFromStoreProcedure(string ConnectionString, string StoreProcedureName, SqlParameter[] param)
        {
            SqlDataReader dr = null;
            SQLDatabase.RunProc(ConnectionString, StoreProcedureName, param, out dr, 1800);
            return Tools.ConvertDataReaderToDataTable(dr);
        }

        public IList<Rpt_DataSet> GetDataByFolder(int Folder, bool Enabled)
        {
            SqlDataReader dr = null;
            #region	设置参数集
            SqlParameter[] prams = {
                SQLDatabase.MakeInParam("@Folder", SqlDbType.Int, 4, Folder),
				SQLDatabase.MakeInParam("@Enabled", SqlDbType.Char, 1, Enabled?"Y":"N")
            };
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_GetByFolder", prams, out dr);

            return FillModelList(dr);
        }
    }
}

