
// ===================================================================
// 文件： ORD_PublishDAL.cs
// 项目名称：
// 创建时间：2012-7-21
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.EBM;
using System.Collections.Generic;
using MCSFramework.SQLDAL.Pub;
using MCSFramework.Model.Pub;


namespace MCSFramework.SQLDAL.EBM
{
    /// <summary>
    ///ORD_Publish数据访问DAL类
    /// </summary>
    public class ORD_PublishDAL : BaseComplexDAL<ORD_Publish, ORD_PublishDetail>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public ORD_PublishDAL()
        {
            _ProcePrefix = "MCS_EBM.dbo.sp_ORD_Publish";
            _ConnectionStirng = "MCS_EBM_ConnectionString";
        }
        #endregion

        #region 基本操作
        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(ORD_Publish m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Supplier", SqlDbType.Int, 4, m.Supplier),
				SQLDatabase.MakeInParam("@Topic", SqlDbType.VarChar, 200, m.Topic),
				SQLDatabase.MakeInParam("@BeginTime", SqlDbType.DateTime, 8, m.BeginTime),
				SQLDatabase.MakeInParam("@EndTime", SqlDbType.DateTime, 8, m.EndTime),
				SQLDatabase.MakeInParam("@Type", SqlDbType.Int, 4, m.Type),
				SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, m.State),
				SQLDatabase.MakeInParam("@FaceMode", SqlDbType.Int, 4, m.FaceMode),
				SQLDatabase.MakeInParam("@Description", SqlDbType.VarChar, 2000, m.Description),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 2000, m.Remark),
				SQLDatabase.MakeInParam("@ApproveTask", SqlDbType.Int, 4, m.ApproveTask),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@InsertUser", SqlDbType.UniqueIdentifier, 16, m.InsertUser),
				SQLDatabase.MakeInParam("@UpdateUser", SqlDbType.UniqueIdentifier, 16, m.UpdateUser),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            m.ID = SQLDatabase.RunProc(_ConnectionStirng, _ProcePrefix + "_Add", prams);

            return m.ID;
        }

        /// <summary>
        /// 更新项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Update(ORD_Publish m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@Supplier", SqlDbType.Int, 4, m.Supplier),
				SQLDatabase.MakeInParam("@Topic", SqlDbType.VarChar, 200, m.Topic),
				SQLDatabase.MakeInParam("@BeginTime", SqlDbType.DateTime, 8, m.BeginTime),
				SQLDatabase.MakeInParam("@EndTime", SqlDbType.DateTime, 8, m.EndTime),
				SQLDatabase.MakeInParam("@Type", SqlDbType.Int, 4, m.Type),
				SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, m.State),
				SQLDatabase.MakeInParam("@FaceMode", SqlDbType.Int, 4, m.FaceMode),
				SQLDatabase.MakeInParam("@Description", SqlDbType.VarChar, 2000, m.Description),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 2000, m.Remark),
				SQLDatabase.MakeInParam("@ApproveTask", SqlDbType.Int, 4, m.ApproveTask),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@InsertUser", SqlDbType.UniqueIdentifier, 16, m.InsertUser),
				SQLDatabase.MakeInParam("@UpdateUser", SqlDbType.UniqueIdentifier, 16, m.UpdateUser),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ConnectionStirng, _ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override ORD_Publish FillModel(IDataReader dr)
        {
            ORD_Publish m = new ORD_Publish();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["Supplier"].ToString())) m.Supplier = (int)dr["Supplier"];
            if (!string.IsNullOrEmpty(dr["Topic"].ToString())) m.Topic = (string)dr["Topic"];
            if (!string.IsNullOrEmpty(dr["BeginTime"].ToString())) m.BeginTime = (DateTime)dr["BeginTime"];
            if (!string.IsNullOrEmpty(dr["EndTime"].ToString())) m.EndTime = (DateTime)dr["EndTime"];
            if (!string.IsNullOrEmpty(dr["Type"].ToString())) m.Type = (int)dr["Type"];
            if (!string.IsNullOrEmpty(dr["State"].ToString())) m.State = (int)dr["State"];
            if (!string.IsNullOrEmpty(dr["FaceMode"].ToString())) m.FaceMode = (int)dr["FaceMode"];
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

        public override int AddDetail(ORD_PublishDetail m)
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
				SQLDatabase.MakeInParam("@Points", SqlDbType.Decimal, 9, m.Points),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 1000, m.Remark),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            m.ID = SQLDatabase.RunProc(_ConnectionStirng, _ProcePrefix + "_AddDetail", prams);

            return m.ID;
        }

        public override int UpdateDetail(ORD_PublishDetail m)
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
				SQLDatabase.MakeInParam("@Points", SqlDbType.Decimal, 9, m.Points),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 1000, m.Remark),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ConnectionStirng, _ProcePrefix + "_UpdateDetail", prams);

            return ret;
        }

        protected override ORD_PublishDetail FillDetailModel(IDataReader dr)
        {
            ORD_PublishDetail m = new ORD_PublishDetail();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["PublishID"].ToString())) m.PublishID = (int)dr["PublishID"];
            if (!string.IsNullOrEmpty(dr["Product"].ToString())) m.Product = (int)dr["Product"];
            if (!string.IsNullOrEmpty(dr["MinQuantity"].ToString())) m.MinQuantity = (int)dr["MinQuantity"];
            if (!string.IsNullOrEmpty(dr["MaxQuantity"].ToString())) m.MaxQuantity = (int)dr["MaxQuantity"];
            if (!string.IsNullOrEmpty(dr["AvailableQuantity"].ToString())) m.AvailableQuantity = (int)dr["AvailableQuantity"];
            if (!string.IsNullOrEmpty(dr["Price"].ToString())) m.Price = (decimal)dr["Price"];
            if (!string.IsNullOrEmpty(dr["Points"].ToString())) m.Points = (decimal)dr["Points"];
            if (!string.IsNullOrEmpty(dr["Remark"].ToString())) m.Remark = (string)dr["Remark"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);

            //填充产品编号
            try
            {
                PDT_ProductDAL productdal = new PDT_ProductDAL();
                m.ProductCode = productdal.GetModel(m.Product).Code;
            }
            catch { }
            return m;
        }
        #endregion

        /// <summary>
        /// 设置上架发布状态
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="State"></param>
        /// <param name="User"></param>
        /// <returns></returns>
        public int SetState(int ID, int State, Guid User)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, ID),
                SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, State),
				SQLDatabase.MakeInParam("@User", SqlDbType.UniqueIdentifier, 16,User)                
			};
            #endregion

            return SQLDatabase.RunProc(_ConnectionStirng, _ProcePrefix + "_SetState", prams);
        }

        public IList<ORD_Publish> GetPublishListByClient(int Client, int Supplier, int Type)
        {
            SqlDataReader dr = null;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, Client),
                SQLDatabase.MakeInParam("@Supplier", SqlDbType.Int, 4, Supplier),
				SQLDatabase.MakeInParam("@Type", SqlDbType.Int, 4,Type)                
			};
            #endregion

            SQLDatabase.RunProc(_ConnectionStirng, _ProcePrefix + "_GetPublishListByClient", prams, out dr);

            return FillModelList(dr);
        }

        public int GetPublishIDByClient(int Client, int Supplier, int Type)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, Client),
                SQLDatabase.MakeInParam("@Supplier", SqlDbType.Int, 4, Supplier),
				SQLDatabase.MakeInParam("@Type", SqlDbType.Int, 4,Type)                
			};
            #endregion

            return SQLDatabase.RunProc(_ConnectionStirng, _ProcePrefix + "_GetPublishIDByClient", prams);
        }
        /// <summary>
        /// 获取某客户的指定产品的请购价格
        /// </summary>
        /// <param name="Client"></param>
        /// <param name="Product"></param>
        /// <returns></returns>
        public decimal GetPublishPriceByClientAndProduct(int Client, int Product)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, Client),
                SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, Product),
		        new SqlParameter("@Price",SqlDbType.Decimal,9, ParameterDirection.Output,false,10,3,"Price", DataRowVersion.Default,0)
			};
            #endregion

            SQLDatabase.RunProc(_ConnectionStirng, _ProcePrefix + "_GetPublishPriceByClientAndProduct", prams);

            decimal price = 0;
            try { price = (decimal)prams[2].Value; }
            catch { }
            return price;
        }

        #region 设置可使用的账户类型
        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public int AddAccountType(int PublishID, int AccountType)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@PublishID", SqlDbType.Int, 4, PublishID),
				SQLDatabase.MakeInParam("@AccountType", SqlDbType.Int, 4, AccountType),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, "")
			};
            #endregion

            return SQLDatabase.RunProc(_ConnectionStirng, _ProcePrefix + "_AddAccountType", prams);
        }

        /// <summary>
        /// 更新项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public int RemoveAccountType(int PublishID, int AccountType)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@PublishID", SqlDbType.Int, 4, PublishID),
				SQLDatabase.MakeInParam("@AccountType", SqlDbType.Int, 4, AccountType)
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ConnectionStirng, _ProcePrefix + "_RemoveAccountType", prams);

            return ret;
        }

        /// <summary>
        /// 查询发布目录的可用账户类型
        /// </summary>
        /// <param name="PublishID"></param>
        /// <returns></returns>
        public IList<ORD_PublishWithAccountType> GetAccountType(int PublishID)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@PublishID", SqlDbType.Int, 4, PublishID)
			};
            #endregion

            SqlDataReader dr = null;
            SQLDatabase.RunProc(_ConnectionStirng, _ProcePrefix + "_GetAccountType", prams, out dr);

            IList<ORD_PublishWithAccountType> list = new List<ORD_PublishWithAccountType>();
            if (dr != null)
            {
                while (dr.Read())
                {
                    list.Add(FillModel_WithAccountType(dr));
                }
                dr.Close();
            }
            return list;
        }

        private ORD_PublishWithAccountType FillModel_WithAccountType(IDataReader dr)
        {
            ORD_PublishWithAccountType m = new ORD_PublishWithAccountType();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["PublishID"].ToString())) m.PublishID = (int)dr["PublishID"];
            if (!string.IsNullOrEmpty(dr["AccountType"].ToString())) m.AccountType = (int)dr["AccountType"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);

            return m;
        }
        #endregion

        #region 获取指定发布目录、指定产品已订货数量
        public int GetSubmitQuantity(int PublishID, int Product)
        {
            #region	设置参数集
            SqlParameter[] prams = {
                SQLDatabase.MakeInParam("@PublishID", SqlDbType.Int, 4, PublishID),
                SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, Product)
			};
            #endregion

            return SQLDatabase.RunProc(_ConnectionStirng, _ProcePrefix + "_GetSubmitQuantity", prams);
        }
        #endregion
    }
}

