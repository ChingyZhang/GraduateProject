
// ===================================================================
// 文件： ORD_ApplyPublishDAL.cs
// 项目名称：
// 创建时间：2009/9/5
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.Logistics;


namespace MCSFramework.SQLDAL.Logistics
{
    /// <summary>
    ///ORD_ApplyPublish数据访问DAL类
    /// </summary>
    public class ORD_ApplyPublishDAL : BaseComplexDAL<ORD_ApplyPublish, ORD_ApplyPublishDetail>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public ORD_ApplyPublishDAL()
        {
            _ProcePrefix = "MCS_Logistics.dbo.sp_ORD_ApplyPublish";
        }
        #endregion


        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(ORD_ApplyPublish m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Topic", SqlDbType.VarChar, 200, m.Topic),
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, m.AccountMonth),
				SQLDatabase.MakeInParam("@BeginTime", SqlDbType.DateTime, 8, m.BeginTime),
				SQLDatabase.MakeInParam("@EndTime", SqlDbType.DateTime, 8, m.EndTime),
				SQLDatabase.MakeInParam("@ToOrganizeCity", SqlDbType.Int, 4, m.ToOrganizeCity),
				SQLDatabase.MakeInParam("@Type", SqlDbType.Int, 4, m.Type),
				SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, m.State),
				SQLDatabase.MakeInParam("@Description", SqlDbType.VarChar, 2000, m.Description),
				SQLDatabase.MakeInParam("@FeeType", SqlDbType.Int, 4, m.FeeType),
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
        public override int Update(ORD_ApplyPublish m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@Topic", SqlDbType.VarChar, 200, m.Topic),
				SQLDatabase.MakeInParam("@AccountMonth", SqlDbType.Int, 4, m.AccountMonth),
				SQLDatabase.MakeInParam("@BeginTime", SqlDbType.DateTime, 8, m.BeginTime),
				SQLDatabase.MakeInParam("@EndTime", SqlDbType.DateTime, 8, m.EndTime),
				SQLDatabase.MakeInParam("@ToOrganizeCity", SqlDbType.Int, 4, m.ToOrganizeCity),
				SQLDatabase.MakeInParam("@Type", SqlDbType.Int, 4, m.Type),
				SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, m.State),
				SQLDatabase.MakeInParam("@Description", SqlDbType.VarChar, 2000, m.Description),
				SQLDatabase.MakeInParam("@FeeType", SqlDbType.Int, 4, m.FeeType),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@UpdateStaff", SqlDbType.Int, 4, m.UpdateStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override ORD_ApplyPublish FillModel(IDataReader dr)
        {
            ORD_ApplyPublish m = new ORD_ApplyPublish();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["Topic"].ToString())) m.Topic = (string)dr["Topic"];
            if (!string.IsNullOrEmpty(dr["AccountMonth"].ToString())) m.AccountMonth = (int)dr["AccountMonth"];
            if (!string.IsNullOrEmpty(dr["BeginTime"].ToString())) m.BeginTime = (DateTime)dr["BeginTime"];
            if (!string.IsNullOrEmpty(dr["EndTime"].ToString())) m.EndTime = (DateTime)dr["EndTime"];
            if (!string.IsNullOrEmpty(dr["ToOrganizeCity"].ToString())) m.ToOrganizeCity = (int)dr["ToOrganizeCity"];
            if (!string.IsNullOrEmpty(dr["Type"].ToString())) m.Type = (int)dr["Type"];
            if (!string.IsNullOrEmpty(dr["State"].ToString())) m.State = (int)dr["State"];
            if (!string.IsNullOrEmpty(dr["Description"].ToString())) m.Description = (string)dr["Description"];
            if (!string.IsNullOrEmpty(dr["FeeType"].ToString())) m.FeeType = (int)dr["FeeType"];
            if (!string.IsNullOrEmpty(dr["ApproveFlag"].ToString())) m.ApproveFlag = (int)dr["ApproveFlag"];
            if (!string.IsNullOrEmpty(dr["InsertTime"].ToString())) m.InsertTime = (DateTime)dr["InsertTime"];
            if (!string.IsNullOrEmpty(dr["InsertStaff"].ToString())) m.InsertStaff = (int)dr["InsertStaff"];
            if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString())) m.UpdateTime = (DateTime)dr["UpdateTime"];
            if (!string.IsNullOrEmpty(dr["UpdateStaff"].ToString())) m.UpdateStaff = (int)dr["UpdateStaff"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);

            return m;
        }

        public override int AddDetail(ORD_ApplyPublishDetail m)
        {
            m.PublishID = HeadID;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@PublishID", SqlDbType.Int, 4, m.PublishID),
				SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, m.Product),
				SQLDatabase.MakeInParam("@MinQuantity", SqlDbType.Int, 4, m.MinQuantity),
				SQLDatabase.MakeInParam("@MaxQuantity", SqlDbType.Int, 4, m.MaxQuantity),
				SQLDatabase.MakeInParam("@AvailableQuantity", SqlDbType.Int, 4, m.AvailableQuantity),
				SQLDatabase.MakeInParam("@Price", SqlDbType.Decimal, 9, m.Price),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 1000, m.Remark),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 8000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            m.ID = SQLDatabase.RunProc(_ProcePrefix + "_AddDetail", prams);

            return m.ID;
        }

        public override int UpdateDetail(ORD_ApplyPublishDetail m)
        {
            m.PublishID = HeadID;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@PublishID", SqlDbType.Int, 4, m.PublishID),
				SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, m.Product),
				SQLDatabase.MakeInParam("@MinQuantity", SqlDbType.Int, 4, m.MinQuantity),
				SQLDatabase.MakeInParam("@MaxQuantity", SqlDbType.Int, 4, m.MaxQuantity),
				SQLDatabase.MakeInParam("@AvailableQuantity", SqlDbType.Int, 4, m.AvailableQuantity),
				SQLDatabase.MakeInParam("@Price", SqlDbType.Decimal, 9, m.Price),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 1000, m.Remark),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 8000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_UpdateDetail", prams);

            return ret;
        }

        protected override ORD_ApplyPublishDetail FillDetailModel(IDataReader dr)
        {
            ORD_ApplyPublishDetail m = new ORD_ApplyPublishDetail();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["PublishID"].ToString())) m.PublishID = (int)dr["PublishID"];
            if (!string.IsNullOrEmpty(dr["Product"].ToString())) m.Product = (int)dr["Product"];
            if (!string.IsNullOrEmpty(dr["MinQuantity"].ToString())) m.MinQuantity = (int)dr["MinQuantity"];
            if (!string.IsNullOrEmpty(dr["MaxQuantity"].ToString())) m.MaxQuantity = (int)dr["MaxQuantity"];
            if (!string.IsNullOrEmpty(dr["AvailableQuantity"].ToString())) m.AvailableQuantity = (int)dr["AvailableQuantity"];
            if (!string.IsNullOrEmpty(dr["Price"].ToString())) m.Price = (decimal)dr["Price"];
            if (!string.IsNullOrEmpty(dr["Remark"].ToString())) m.Remark = (string)dr["Remark"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);


            return m;
        }


        /// <summary>
        /// 产品请购发布
        /// </summary>
        /// <param name="id"></param>
        /// <param name="state"></param>
        /// <param name="staff"></param>
        /// <returns></returns>
        public int Publish(int id, int state, int staff)
        {
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, id),
                SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, state),
                SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4, staff)
            };

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Publish", prams);

            return ret;
        }

        /// <summary>
        /// 复制请购发布目录
        /// </summary>
        /// <param name="id"></param>
        /// <param name="staff"></param>
        /// <returns></returns>
        public int Copy(int id, int staff)
        {
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, id),
                SQLDatabase.MakeInParam("@Staff", SqlDbType.Int, 4, staff)
            };

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Copy", prams);

            return ret;
        }

        public void GetMinApplyAmount(int id, out decimal MinApplyAmount, out decimal MaxApplyAmount)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@PublishID", SqlDbType.Int, 4, id),			 
				new SqlParameter("@MinApplyAmount", SqlDbType.Decimal, 9, ParameterDirection.Output,false,18,3, "MinApplyAmount", DataRowVersion.Current,0), 
                new SqlParameter("@MaxApplyAmount", SqlDbType.Decimal, 9, ParameterDirection.Output,false,18,3, "MaxApplyAmount", DataRowVersion.Current,0), 
			};
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_GetMinApplyAmount", prams);

            MinApplyAmount = (decimal)prams[1].Value;
            MaxApplyAmount = (decimal)prams[2].Value;

        }

        public SqlDataReader GetbyOrganizeCity(int OrganizeCity, int Type)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, OrganizeCity),
                SQLDatabase.MakeInParam("@Type", SqlDbType.Int, 4, Type)
                                   };
            #endregion
            SqlDataReader dr = null;
            SQLDatabase.RunProc(_ProcePrefix + "_GetbyOrganizeCity", prams, out dr);
            return dr;
        }
    }
}

