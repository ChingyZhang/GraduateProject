
// ===================================================================
// 文件： FNA_ClientPaymentInfoDAL.cs
// 项目名称：
// 创建时间：2009/2/22
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
    ///FNA_ClientPaymentInfo数据访问DAL类
    /// </summary>
    public class FNA_ClientPaymentInfoDAL : BaseSimpleDAL<FNA_ClientPaymentInfo>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public FNA_ClientPaymentInfoDAL()
        {
            _ProcePrefix = "MCS_FNA.dbo.sp_FNA_ClientPaymentInfo";
        }
        #endregion


        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(FNA_ClientPaymentInfo m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, m.Client),
				SQLDatabase.MakeInParam("@PayDate", SqlDbType.DateTime, 8, m.PayDate),
				SQLDatabase.MakeInParam("@PayAmount", SqlDbType.Decimal, 9, m.PayAmount),
				SQLDatabase.MakeInParam("@ReceiveAccount", SqlDbType.VarChar, 100, m.ReceiveAccount),
				SQLDatabase.MakeInParam("@ConfirmDate", SqlDbType.DateTime, 8, m.ConfirmDate),
				SQLDatabase.MakeInParam("@ConfirmStaff", SqlDbType.Int, 4, m.ConfirmStaff),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@InsertStaff", SqlDbType.Int, 4, m.InsertStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 8000, CombineExtProperty(m.ExtPropertys,m.ModelName))
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
        public override int Update(FNA_ClientPaymentInfo m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, m.Client),
				SQLDatabase.MakeInParam("@PayDate", SqlDbType.DateTime, 8, m.PayDate),
				SQLDatabase.MakeInParam("@PayAmount", SqlDbType.Decimal, 9, m.PayAmount),
				SQLDatabase.MakeInParam("@ReceiveAccount", SqlDbType.VarChar, 100, m.ReceiveAccount),
				SQLDatabase.MakeInParam("@ConfirmDate", SqlDbType.DateTime, 8, m.ConfirmDate),
				SQLDatabase.MakeInParam("@ConfirmStaff", SqlDbType.Int, 4, m.ConfirmStaff),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@UpdateStaff", SqlDbType.Int, 4, m.UpdateStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 8000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override FNA_ClientPaymentInfo FillModel(IDataReader dr)
        {
            FNA_ClientPaymentInfo m = new FNA_ClientPaymentInfo();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["Client"].ToString())) m.Client = (int)dr["Client"];
            if (!string.IsNullOrEmpty(dr["PayDate"].ToString())) m.PayDate = (DateTime)dr["PayDate"];
            if (!string.IsNullOrEmpty(dr["PayAmount"].ToString())) m.PayAmount = (decimal)dr["PayAmount"];
            if (!string.IsNullOrEmpty(dr["ReceiveAccount"].ToString())) m.ReceiveAccount = (string)dr["ReceiveAccount"];
            if (!string.IsNullOrEmpty(dr["ConfirmDate"].ToString())) m.ConfirmDate = (DateTime)dr["ConfirmDate"];
            if (!string.IsNullOrEmpty(dr["ConfirmStaff"].ToString())) m.ConfirmStaff = (int)dr["ConfirmStaff"];
            if (!string.IsNullOrEmpty(dr["ApproveFlag"].ToString())) m.ApproveFlag = (int)dr["ApproveFlag"];
            if (!string.IsNullOrEmpty(dr["InsertTime"].ToString())) m.InsertTime = (DateTime)dr["InsertTime"];
            if (!string.IsNullOrEmpty(dr["InsertStaff"].ToString())) m.InsertStaff = (int)dr["InsertStaff"];
            if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString())) m.UpdateTime = (DateTime)dr["UpdateTime"];
            if (!string.IsNullOrEmpty(dr["UpdateStaff"].ToString())) m.UpdateStaff = (int)dr["UpdateStaff"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);

            return m;
        }

        /// <summary>
        /// 确认客户回款到账
        /// </summary>
        /// <param name="id"></param>
        /// <param name="staff"></param>
        /// <param name="confirmdate"></param>
        /// <returns></returns>
        public int Confirm(int id, int staff, DateTime confirmdate)
        {
            SqlParameter[] prams ={
                SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, id),
                SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4, staff),
                SQLDatabase.MakeInParam("@ConfirmDate", SqlDbType.DateTime, 8, confirmdate)
            };

            return SQLDatabase.RunProc(_ProcePrefix + "_Confirm", prams);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        /// <param name="accountmonth"></param>
        /// <returns></returns>
        public decimal GetByClientDate(int client, int accountmonth)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, client),
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, accountmonth),
                new SqlParameter("@Pay", SqlDbType.Decimal,18, ParameterDirection.Output,false,18,2,"Pay", DataRowVersion.Current,0)
            };
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_GetPayByClientDate", prams);
            return (decimal)prams[2].Value;
        }


        public void Init(int OrganizeCity, int Client)
        {
            SqlParameter[] parameters = {
					SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int,4,OrganizeCity),
                	SQLDatabase.MakeInParam("@Client", SqlDbType.Int,4,Client)                      
                                        };
            SQLDatabase.RunProc(_ProcePrefix + "_Init", parameters);
        }

      /// <summary>
      /// 取消回款
      /// </summary>
      /// <param name="id"></param>
      /// <param name="staff"></param>
      /// <param name="confirmdate"></param>
      /// <returns></returns>
        public int CancleConfirm(int id, int staff, DateTime confirmdate)
        {
            SqlParameter[] prams ={
                SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, id),
                SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4, staff),
                SQLDatabase.MakeInParam("@ConfirmDate", SqlDbType.DateTime, 8, confirmdate)
            };

            return SQLDatabase.RunProc(_ProcePrefix + "_CancleConfirm", prams);
        }
    }
}

