
// ===================================================================
// 文件： ORD_QuotaDAL.cs
// 项目名称：
// 创建时间：2014-01-24
// 作者:	   ShenGang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.EBM;


namespace MCSFramework.SQLDAL.EBM
{
    /// <summary>
    ///ORD_Quota数据访问DAL类
    /// </summary>
    public class ORD_QuotaDAL : BaseComplexDAL<ORD_Quota, ORD_QuotaDetail>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public ORD_QuotaDAL()
        {
            _ProcePrefix = "MCS_EBM.dbo.sp_ORD_Quota";
        }
        #endregion


        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(ORD_Quota m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, m.Client),
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, m.AccountMonth),
				SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, m.State),
				SQLDatabase.MakeInParam("@Description", SqlDbType.VarChar, 2000, m.Description),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 2000, m.Remark),
				SQLDatabase.MakeInParam("@ApproveTask", SqlDbType.Int, 4, m.ApproveTask),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@InsertUser", SqlDbType.UniqueIdentifier, 16, m.InsertUser),
				SQLDatabase.MakeInParam("@UpdateUser", SqlDbType.UniqueIdentifier, 16, m.UpdateUser),
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
        public override int Update(ORD_Quota m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, m.Client),
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, m.AccountMonth),
				SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, m.State),
				SQLDatabase.MakeInParam("@Description", SqlDbType.VarChar, 2000, m.Description),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 2000, m.Remark),
				SQLDatabase.MakeInParam("@ApproveTask", SqlDbType.Int, 4, m.ApproveTask),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@InsertUser", SqlDbType.UniqueIdentifier, 16, m.InsertUser),
				SQLDatabase.MakeInParam("@UpdateUser", SqlDbType.UniqueIdentifier, 16, m.UpdateUser),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override ORD_Quota FillModel(IDataReader dr)
        {
            ORD_Quota m = new ORD_Quota();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["Client"].ToString())) m.Client = (int)dr["Client"];
            if (!string.IsNullOrEmpty(dr["AccountMonth"].ToString())) m.AccountMonth = (int)dr["AccountMonth"];
            if (!string.IsNullOrEmpty(dr["State"].ToString())) m.State = (int)dr["State"];
            if (!string.IsNullOrEmpty(dr["Description"].ToString())) m.Description = (string)dr["Description"];
            if (!string.IsNullOrEmpty(dr["Remark"].ToString())) m.Remark = (string)dr["Remark"];
            if (!string.IsNullOrEmpty(dr["ApproveTask"].ToString())) m.ApproveTask = (int)dr["ApproveTask"];
            if (!string.IsNullOrEmpty(dr["ApproveFlag"].ToString())) m.ApproveFlag = (int)dr["ApproveFlag"];
            if (!string.IsNullOrEmpty(dr["InsertTime"].ToString())) m.InsertTime = (DateTime)dr["InsertTime"];
            if (!string.IsNullOrEmpty(dr["InsertUser"].ToString())) m.InsertUser = (Guid)dr["InsertUser"];
            if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString())) m.UpdateTime = (DateTime)dr["UpdateTime"];
            if (!string.IsNullOrEmpty(dr["UpdateUser"].ToString())) m.UpdateUser = (Guid)dr["UpdateUser"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);

            return m;
        }

        public override int AddDetail(ORD_QuotaDetail m)
        {
            m.QuotaID = HeadID;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@QuotaID", SqlDbType.Int, 4, m.QuotaID),
				SQLDatabase.MakeInParam("@Brand", SqlDbType.Int, 4, m.Brand),
				SQLDatabase.MakeInParam("@Classify", SqlDbType.Int, 4, m.Classify),
				SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, m.Product),
				SQLDatabase.MakeInParam("@StdQuota", SqlDbType.Int, 4, m.StdQuota),
				SQLDatabase.MakeInParam("@AdjQuota", SqlDbType.Int, 4, m.AdjQuota),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 1000, m.Remark),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            m.ID = SQLDatabase.RunProc(_ProcePrefix + "_AddDetail", prams);

            return m.ID;
        }

        public override int UpdateDetail(ORD_QuotaDetail m)
        {
            m.QuotaID = HeadID;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@QuotaID", SqlDbType.Int, 4, m.QuotaID),
				SQLDatabase.MakeInParam("@Brand", SqlDbType.Int, 4, m.Brand),
				SQLDatabase.MakeInParam("@Classify", SqlDbType.Int, 4, m.Classify),
				SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, m.Product),
				SQLDatabase.MakeInParam("@StdQuota", SqlDbType.Int, 4, m.StdQuota),
				SQLDatabase.MakeInParam("@AdjQuota", SqlDbType.Int, 4, m.AdjQuota),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 1000, m.Remark),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_UpdateDetail", prams);

            return ret;
        }

        protected override ORD_QuotaDetail FillDetailModel(IDataReader dr)
        {
            ORD_QuotaDetail m = new ORD_QuotaDetail();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["QuotaID"].ToString())) m.QuotaID = (int)dr["QuotaID"];
            if (!string.IsNullOrEmpty(dr["Brand"].ToString())) m.Brand = (int)dr["Brand"];
            if (!string.IsNullOrEmpty(dr["Classify"].ToString())) m.Classify = (int)dr["Classify"];
            if (!string.IsNullOrEmpty(dr["Product"].ToString())) m.Product = (int)dr["Product"];
            if (!string.IsNullOrEmpty(dr["StdQuota"].ToString())) m.StdQuota = (int)dr["StdQuota"];
            if (!string.IsNullOrEmpty(dr["AdjQuota"].ToString())) m.AdjQuota = (int)dr["AdjQuota"];
            if (!string.IsNullOrEmpty(dr["Remark"].ToString())) m.Remark = (string)dr["Remark"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);


            return m;
        }

        /// <summary>
        /// 配额审核通过
        /// </summary>
        /// <param name="QuotaID"></param>
        /// <param name="State"></param>
        /// <param name="OpUser"></param>
        /// <returns></returns>
        public int SetState(int QuotaID, int State, Guid OpUser)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@QuotaID", SqlDbType.Int, 4, QuotaID),
				SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, State),
				SQLDatabase.MakeInParam("@OpUser", SqlDbType.UniqueIdentifier, 16, OpUser)
            };
            #endregion

            return SQLDatabase.RunProc(_ProcePrefix + "_SetState", prams);
        }

        /// <summary>
        /// 将下游客户的配额汇总至上级客户
        /// </summary>
        /// <param name="Supplier"></param>
        /// <param name="AccountMonth"></param>
        /// <param name="OpUser"></param>
        /// <returns></returns>
        public int SummaryToSupplier(int Supplier, int AccountMonth, Guid OpUser)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Supplier", SqlDbType.Int, 4, Supplier),
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, AccountMonth),
				SQLDatabase.MakeInParam("@OpUser", SqlDbType.UniqueIdentifier, 16, OpUser)
            };
            #endregion

            return SQLDatabase.RunProc(_ProcePrefix + "_SummaryToSupplier", prams);
        }

        
    }
}

