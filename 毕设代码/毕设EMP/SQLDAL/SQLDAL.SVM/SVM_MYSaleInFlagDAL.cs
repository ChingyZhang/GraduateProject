
// ===================================================================
// 文件： SVM_MYSaleInFlagDAL.cs
// 项目名称：
// 创建时间：2014/11/12
// 作者:	   Jace
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.SVM;


namespace MCSFramework.SQLDAL.SVM
{
    /// <summary>
    ///SVM_MYSaleInFlag数据访问DAL类
    /// </summary>
    public class SVM_MYSaleInFlagDAL : BaseSimpleDAL<SVM_MYSaleInFlag>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public SVM_MYSaleInFlagDAL()
        {
            _ProcePrefix = "MCS_SVM.dbo.sp_SVM_MYSaleInFlag";
        }
        #endregion


        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(SVM_MYSaleInFlag m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, m.AccountMonth),
                SQLDatabase.MakeInParam("@OrganizeCity",SqlDbType.Int,4,m.OrganizeCity),
				SQLDatabase.MakeInParam("@ClientID", SqlDbType.Int, 4, m.ClientID),
				SQLDatabase.MakeInParam("@ClientCode", SqlDbType.VarChar, 80, m.ClientCode),
				SQLDatabase.MakeInParam("@ClientName", SqlDbType.VarChar, 150, m.ClientName),
				SQLDatabase.MakeInParam("@DIID", SqlDbType.Int, 4, m.DIID),
				SQLDatabase.MakeInParam("@DIName", SqlDbType.VarChar, 150, m.DIName),
				SQLDatabase.MakeInParam("@DICode", SqlDbType.VarChar, 80, m.DICode),
				SQLDatabase.MakeInParam("@ClientManager", SqlDbType.Int, 4, m.ClientManager),
				SQLDatabase.MakeInParam("@ManagerCode", SqlDbType.VarChar, 80, m.ManagerCode),
				SQLDatabase.MakeInParam("@ManagerName", SqlDbType.VarChar, 50, m.ManagerName),
				SQLDatabase.MakeInParam("@Flag", SqlDbType.Int, 4, m.Flag),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@InsertStaff", SqlDbType.Int, 4, m.InsertStaff)
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
        public override int Update(SVM_MYSaleInFlag m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
                SQLDatabase.MakeInParam("@OrganizeCity",SqlDbType.Int,4,m.OrganizeCity),
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, m.AccountMonth),
				SQLDatabase.MakeInParam("@ClientID", SqlDbType.Int, 4, m.ClientID),
				SQLDatabase.MakeInParam("@ClientCode", SqlDbType.VarChar, 80, m.ClientCode),
				SQLDatabase.MakeInParam("@ClientName", SqlDbType.VarChar, 150, m.ClientName),
				SQLDatabase.MakeInParam("@DIID", SqlDbType.Int, 4, m.DIID),
				SQLDatabase.MakeInParam("@DIName", SqlDbType.VarChar, 150, m.DIName),
				SQLDatabase.MakeInParam("@DICode", SqlDbType.VarChar, 80, m.DICode),
				SQLDatabase.MakeInParam("@ClientManager", SqlDbType.Int, 4, m.ClientManager),
				SQLDatabase.MakeInParam("@ManagerCode", SqlDbType.VarChar, 80, m.ManagerCode),
				SQLDatabase.MakeInParam("@ManagerName", SqlDbType.VarChar, 50, m.ManagerName),
				SQLDatabase.MakeInParam("@Flag", SqlDbType.Int, 4, m.Flag),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@UpdateStaff", SqlDbType.Int, 4, m.UpdateStaff)
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override SVM_MYSaleInFlag FillModel(IDataReader dr)
        {
            SVM_MYSaleInFlag m = new SVM_MYSaleInFlag();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["OrganizeCity"].ToString())) m.OrganizeCity = (int)dr["OrganizeCity"];
            if (!string.IsNullOrEmpty(dr["AccountMonth"].ToString())) m.AccountMonth = (int)dr["AccountMonth"];
            if (!string.IsNullOrEmpty(dr["ClientID"].ToString())) m.ClientID = (int)dr["ClientID"];
            if (!string.IsNullOrEmpty(dr["ClientCode"].ToString())) m.ClientCode = (string)dr["ClientCode"];
            if (!string.IsNullOrEmpty(dr["ClientName"].ToString())) m.ClientName = (string)dr["ClientName"];
            if (!string.IsNullOrEmpty(dr["DIID"].ToString())) m.DIID = (int)dr["DIID"];
            if (!string.IsNullOrEmpty(dr["DIName"].ToString())) m.DIName = (string)dr["DIName"];
            if (!string.IsNullOrEmpty(dr["DICode"].ToString())) m.DICode = (string)dr["DICode"];
            if (!string.IsNullOrEmpty(dr["ClientManager"].ToString())) m.ClientManager = (int)dr["ClientManager"];
            if (!string.IsNullOrEmpty(dr["ManagerCode"].ToString())) m.ManagerCode = (string)dr["ManagerCode"];
            if (!string.IsNullOrEmpty(dr["ManagerName"].ToString())) m.ManagerName = (string)dr["ManagerName"];
            if (!string.IsNullOrEmpty(dr["Flag"].ToString())) m.Flag = (int)dr["Flag"];
            if (!string.IsNullOrEmpty(dr["ApproveFlag"].ToString())) m.ApproveFlag = (int)dr["ApproveFlag"];
            if (!string.IsNullOrEmpty(dr["InsertTime"].ToString())) m.InsertTime = (DateTime)dr["InsertTime"];
            if (!string.IsNullOrEmpty(dr["InsertStaff"].ToString())) m.InsertStaff = (int)dr["InsertStaff"];
            if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString())) m.UpdateTime = (DateTime)dr["UpdateTime"];
            if (!string.IsNullOrEmpty(dr["UpdateStaff"].ToString())) m.UpdateStaff = (int)dr["UpdateStaff"];

            return m;
        }

        /// <summary>
        /// 月度母婴进货门店明细查询
        /// </summary>
        public SqlDataReader GetRPTSummary(int BeginMonth, int EndMonth, int OrganizeCity, int ClientManager, string Condition)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@BeginMonth", SqlDbType.Int, 4, BeginMonth),
				SQLDatabase.MakeInParam("@EndMonth", SqlDbType.Int, 4, EndMonth),
                SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, OrganizeCity),
                SQLDatabase.MakeInParam("@ClientManager", SqlDbType.Int, 4, ClientManager),
                SQLDatabase.MakeInParam("@ExtCondition", SqlDbType.VarChar,8000, Condition)

			};
            #endregion

            SqlDataReader sdr = null;

            SQLDatabase.RunProc("MCS_SVM.dbo.RPT_SVM_MYSaleInFlag_Summary", prams,out sdr);

            return sdr;

        }

        /// <summary>
        /// 获取列表
        /// </summary>
        public SqlDataReader GetList(int AccountMonth, int OrganizeCity, int ClientManager, string DICondition, string RTCondition)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, AccountMonth),
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, OrganizeCity),
                SQLDatabase.MakeInParam("@ClientManager", SqlDbType.Int, 4, ClientManager),
                SQLDatabase.MakeInParam("@DICondition", SqlDbType.VarChar,8000, DICondition),
                SQLDatabase.MakeInParam("@RTCondition", SqlDbType.VarChar,8000, RTCondition)

			};
            #endregion
            SqlDataReader sdr = null;

            SQLDatabase.RunProc(_ProcePrefix + "_GetList", prams, out sdr);

            return sdr;

        }
    }
}

