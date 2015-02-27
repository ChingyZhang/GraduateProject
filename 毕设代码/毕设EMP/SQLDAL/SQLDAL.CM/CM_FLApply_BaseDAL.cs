
// ===================================================================
// 文件： CM_FLApply_BaseDAL.cs
// 项目名称：
// 创建时间：2013-06-20
// 作者:	   Jace
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.CM;


namespace MCSFramework.SQLDAL.CM
{
    /// <summary>
    ///CM_FLApply_Base数据访问DAL类
    /// </summary>
    public class CM_FLApply_BaseDAL : BaseSimpleDAL<CM_FLApply_Base>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public CM_FLApply_BaseDAL()
        {
            _ProcePrefix = "MCS_CM.dbo.sp_CM_FLApply_Base";
        }
        #endregion


        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(CM_FLApply_Base m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, m.Client),
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, m.AccountMonth),
				SQLDatabase.MakeInParam("@FLBase", SqlDbType.Decimal, 9, m.FLBase),
				SQLDatabase.MakeInParam("@FLType", SqlDbType.Int, 4, m.FLType),
				SQLDatabase.MakeInParam("@ISMYD", SqlDbType.Int, 4, m.ISMYD),
				SQLDatabase.MakeInParam("@RTCount", SqlDbType.Int, 4, m.RTCount),
				SQLDatabase.MakeInParam("@FLContractID", SqlDbType.Int, 4, m.FLContractID),
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
        public override int Update(CM_FLApply_Base m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, m.Client),
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, m.AccountMonth),
				SQLDatabase.MakeInParam("@FLBase", SqlDbType.Decimal, 9, m.FLBase),
				SQLDatabase.MakeInParam("@FLType", SqlDbType.Int, 4, m.FLType),
				SQLDatabase.MakeInParam("@ISMYD", SqlDbType.Int, 4, m.ISMYD),
				SQLDatabase.MakeInParam("@RTCount", SqlDbType.Int, 4, m.RTCount),
				SQLDatabase.MakeInParam("@FLContractID", SqlDbType.Int, 4, m.FLContractID),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override CM_FLApply_Base FillModel(IDataReader dr)
        {
            CM_FLApply_Base m = new CM_FLApply_Base();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["Client"].ToString())) m.Client = (int)dr["Client"];
            if (!string.IsNullOrEmpty(dr["AccountMonth"].ToString())) m.AccountMonth = (int)dr["AccountMonth"];
            if (!string.IsNullOrEmpty(dr["FLBase"].ToString())) m.FLBase = (decimal)dr["FLBase"];
            if (!string.IsNullOrEmpty(dr["FLType"].ToString())) m.FLType = (int)dr["FLType"];
            if (!string.IsNullOrEmpty(dr["ISMYD"].ToString())) m.ISMYD = (int)dr["ISMYD"];
            if (!string.IsNullOrEmpty(dr["RTCount"].ToString())) m.RTCount = (int)dr["RTCount"];
            if (!string.IsNullOrEmpty(dr["FLContractID"].ToString())) m.FLContractID = (int)dr["FLContractID"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);

            return m;
        }

        public SqlDataReader GetByDIClient(int OrganizeCIty, int AccountMonth, int DIClient)
        {
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@OrganizeCIty", SqlDbType.Int, 4,OrganizeCIty),
                SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4,AccountMonth),
                SQLDatabase.MakeInParam("@DIClient", SqlDbType.Int, 4,DIClient)};
            SqlDataReader dr = null;

            SQLDatabase.RunProc(_ProcePrefix + "_GetByDIClient", prams, out dr);
            return dr;
        }

        public SqlDataReader GetByOrganizeCity(int OrganizeCIty, int AccountMonth, int Client, int ISMYD, int FLType)
        {
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@OrganizeCIty", SqlDbType.Int, 4,OrganizeCIty),
                SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4,AccountMonth),
                SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4,Client),
                SQLDatabase.MakeInParam("@ISMYD", SqlDbType.Int, 4,ISMYD),
                SQLDatabase.MakeInParam("@FLType", SqlDbType.Int, 4,FLType)};
            SqlDataReader dr = null;

            SQLDatabase.RunProc(_ProcePrefix + "_GetByOrganizeCity", prams, out dr);
            return dr;
        }
    }
}

