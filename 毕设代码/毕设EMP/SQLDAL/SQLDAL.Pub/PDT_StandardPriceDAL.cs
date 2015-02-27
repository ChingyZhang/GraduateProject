
// ===================================================================
// 文件： PDT_StandardPriceDAL.cs
// 项目名称：
// 创建时间：2011/8/23
// 作者:	   chf
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.Pub;
using System.Collections.Generic;


namespace MCSFramework.SQLDAL.Pub
{
    /// <summary>
    ///PDT_StandardPrice数据访问DAL类
    /// </summary>
    public class PDT_StandardPriceDAL : BaseComplexDAL<PDT_StandardPrice, PDT_StandardPrice_Detail>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public PDT_StandardPriceDAL()
        {
            _ProcePrefix = "MCS_PUB.dbo.sp_PDT_StandardPrice";
        }
        #endregion


        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(PDT_StandardPrice m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@FullName", SqlDbType.VarChar, 100, m.FullName),
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, m.OrganizeCity),
				SQLDatabase.MakeInParam("@ActiveFlag", SqlDbType.Int, 4, m.ActiveFlag),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@TaskID", SqlDbType.Int, 4, m.TaskID),
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
        public override int Update(PDT_StandardPrice m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@FullName", SqlDbType.VarChar, 100, m.FullName),
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, m.OrganizeCity),
				SQLDatabase.MakeInParam("@ActiveFlag", SqlDbType.Int, 4, m.ActiveFlag),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@TaskID", SqlDbType.Int, 4, m.TaskID),
				SQLDatabase.MakeInParam("@UpdateStaff", SqlDbType.Int, 4, m.UpdateStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override PDT_StandardPrice FillModel(IDataReader dr)
        {
            PDT_StandardPrice m = new PDT_StandardPrice();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["FullName"].ToString())) m.FullName = (string)dr["FullName"];
            if (!string.IsNullOrEmpty(dr["OrganizeCity"].ToString())) m.OrganizeCity = (int)dr["OrganizeCity"];
            if (!string.IsNullOrEmpty(dr["ActiveFlag"].ToString())) m.ActiveFlag = (int)dr["ActiveFlag"];
            if (!string.IsNullOrEmpty(dr["ApproveFlag"].ToString())) m.ApproveFlag = (int)dr["ApproveFlag"];
            if (!string.IsNullOrEmpty(dr["TaskID"].ToString())) m.TaskID = (int)dr["TaskID"];
            if (!string.IsNullOrEmpty(dr["InsertTime"].ToString())) m.InsertTime = (DateTime)dr["InsertTime"];
            if (!string.IsNullOrEmpty(dr["InsertStaff"].ToString())) m.InsertStaff = (int)dr["InsertStaff"];
            if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString())) m.UpdateTime = (DateTime)dr["UpdateTime"];
            if (!string.IsNullOrEmpty(dr["UpdateStaff"].ToString())) m.UpdateStaff = (int)dr["UpdateStaff"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);

            return m;
        }

        public override int AddDetail(PDT_StandardPrice_Detail m)
        {
            m.StandardPrice = HeadID;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@StandardPrice", SqlDbType.Int, 4, m.StandardPrice),
				SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, m.Product),
				SQLDatabase.MakeInParam("@FactoryPrice", SqlDbType.Decimal, 9, m.FactoryPrice),
				SQLDatabase.MakeInParam("@TradeOutPrice", SqlDbType.Decimal, 9, m.TradeOutPrice),
				SQLDatabase.MakeInParam("@TradeInPrice", SqlDbType.Decimal, 9, m.TradeInPrice),
				SQLDatabase.MakeInParam("@StdPrice", SqlDbType.Decimal, 9, m.StdPrice),
                SQLDatabase.MakeInParam("@RebatePrice", SqlDbType.Decimal, 9, m.RebatePrice),
                SQLDatabase.MakeInParam("@DIRebatePrice", SqlDbType.Decimal, 9, m.DIRebatePrice),
                SQLDatabase.MakeInParam("@ISFL", SqlDbType.Int, 4, m.ISFL),
                SQLDatabase.MakeInParam("@ISJH", SqlDbType.Int, 4, m.ISJH),
                SQLDatabase.MakeInParam("@ISCheckJF", SqlDbType.Int, 4, m.ISCheckJF)
			};
            #endregion

            m.ID = SQLDatabase.RunProc(_ProcePrefix + "_AddDetail", prams);

            return m.ID;
        }

        public override int UpdateDetail(PDT_StandardPrice_Detail m)
        {
            m.StandardPrice = HeadID;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@StandardPrice", SqlDbType.Int, 4, m.StandardPrice),
				SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, m.Product),
				SQLDatabase.MakeInParam("@FactoryPrice", SqlDbType.Decimal, 9, m.FactoryPrice),
				SQLDatabase.MakeInParam("@TradeOutPrice", SqlDbType.Decimal, 9, m.TradeOutPrice),
				SQLDatabase.MakeInParam("@TradeInPrice", SqlDbType.Decimal, 9, m.TradeInPrice),
				SQLDatabase.MakeInParam("@StdPrice", SqlDbType.Decimal, 9, m.StdPrice),
                SQLDatabase.MakeInParam("@RebatePrice", SqlDbType.Decimal, 9, m.RebatePrice),
                SQLDatabase.MakeInParam("@DIRebatePrice", SqlDbType.Decimal, 9, m.DIRebatePrice),
                SQLDatabase.MakeInParam("@ISFL", SqlDbType.Int, 4, m.ISFL),
                SQLDatabase.MakeInParam("@ISJH", SqlDbType.Int, 4, m.ISJH),
                SQLDatabase.MakeInParam("@ISCheckJF", SqlDbType.Int, 4, m.ISCheckJF)
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_UpdateDetail", prams);

            return ret;
        }

        protected override PDT_StandardPrice_Detail FillDetailModel(IDataReader dr)
        {
            PDT_StandardPrice_Detail m = new PDT_StandardPrice_Detail();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["StandardPrice"].ToString())) m.StandardPrice = (int)dr["StandardPrice"];
            if (!string.IsNullOrEmpty(dr["Product"].ToString())) m.Product = (int)dr["Product"];
            if (!string.IsNullOrEmpty(dr["FactoryPrice"].ToString())) m.FactoryPrice = (decimal)dr["FactoryPrice"];
            if (!string.IsNullOrEmpty(dr["TradeOutPrice"].ToString())) m.TradeOutPrice = (decimal)dr["TradeOutPrice"];
            if (!string.IsNullOrEmpty(dr["TradeInPrice"].ToString())) m.TradeInPrice = (decimal)dr["TradeInPrice"];
            if (!string.IsNullOrEmpty(dr["StdPrice"].ToString())) m.StdPrice = (decimal)dr["StdPrice"];
            if (!string.IsNullOrEmpty(dr["RebatePrice"].ToString())) m.RebatePrice = (decimal)dr["RebatePrice"];
            if (!string.IsNullOrEmpty(dr["DIRebatePrice"].ToString())) m.DIRebatePrice = (decimal)dr["DIRebatePrice"];
            if (!string.IsNullOrEmpty(dr["ISFL"].ToString())) m.ISFL = (int)dr["ISFL"];
            if (!string.IsNullOrEmpty(dr["ISJH"].ToString())) m.ISJH = (int)dr["ISJH"];
            if (!string.IsNullOrEmpty(dr["ISCheckJF"].ToString())) m.ISCheckJF = (int)dr["ISCheckJF"];
            return m;
        }

        #region 适用管理片区明细操作
        public int AddApplyCity(PDT_StandardPrice_ApplyCity m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@StandardPrice", SqlDbType.Int, 4, HeadID),
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, m.OrganizeCity),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            m.ID = SQLDatabase.RunProc(_ProcePrefix + "_AddApplyCity", prams);

            return m.ID;
        }

        public int UpdateApplyCity(PDT_StandardPrice_ApplyCity m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@StandardPrice", SqlDbType.Int, 4, HeadID),
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, m.OrganizeCity),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_UpdateApplyCity", prams);

            return ret;
        }

        public int DeleteApplyCity(int ID)
        {
            SqlParameter[] parameters = {
					SQLDatabase.MakeInParam("@ID", SqlDbType.Int,4,ID)				};
            return SQLDatabase.RunProc(_ProcePrefix + "_DeleteApplyCity", parameters);
        }

        public int DeleteApplyCity()
        {
            SqlParameter[] parameters = {
					SQLDatabase.MakeInParam("@StandardPrice", SqlDbType.Int,4,HeadID)				};
            return SQLDatabase.RunProc(_ProcePrefix + "_ClearApplyCity", parameters);
        }

        public PDT_StandardPrice_ApplyCity GetApplyCityDetailModel(int detailid)
        {
            SqlDataReader dr = null;
            SqlParameter[] parameters = {
					SQLDatabase.MakeInParam("@DetailID", SqlDbType.Int,4,detailid)				};
            SQLDatabase.RunProc(_ProcePrefix + "_GetApplyCityDetailModel", parameters, out dr);

            PDT_StandardPrice_ApplyCity m = default(PDT_StandardPrice_ApplyCity);

            if (dr.Read())
            {
                m = FillApplyCityModel(dr);
            }
            dr.Close();

            return m;
        }

        public IList<PDT_StandardPrice_ApplyCity> GetApplyCityDetail()
        {
            SqlParameter[] parameters = {
					SQLDatabase.MakeInParam("@StandardPrice", SqlDbType.Int,4,HeadID)};
            SqlDataReader dr = null;

            SQLDatabase.RunProc(_ProcePrefix + "_GetApplyCityDetail", parameters, out dr);

            return FillApplyCityDetailModelList(dr);
        }

        private PDT_StandardPrice_ApplyCity FillApplyCityModel(IDataReader dr)
        {
            PDT_StandardPrice_ApplyCity m = new PDT_StandardPrice_ApplyCity();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["StandardPrice"].ToString())) m.StandardPrice = (int)dr["StandardPrice"];
            if (!string.IsNullOrEmpty(dr["OrganizeCity"].ToString())) m.OrganizeCity = (int)dr["OrganizeCity"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);

            return m;
        }

        private IList<PDT_StandardPrice_ApplyCity> FillApplyCityDetailModelList(IDataReader dr)
        {
            IList<PDT_StandardPrice_ApplyCity> list = new List<PDT_StandardPrice_ApplyCity>();
            while (dr.Read())
            {
                list.Add(FillApplyCityModel(dr));
            }
            dr.Close();

            return list;
        }
        #endregion

        public int Approve(int ID, int StaffID)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, ID),
				SQLDatabase.MakeInParam("@StaffID", SqlDbType.Int, 4, StaffID)
			};
            #endregion

            return SQLDatabase.RunProc(_ProcePrefix + "_Approve", prams);
        }

        /// <summary>
        /// 停用标准价表
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public int UnActive(int ID, int StaffID)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, ID),
                SQLDatabase.MakeInParam("@StaffID", SqlDbType.Int, 4, StaffID)
			};
            #endregion

            return SQLDatabase.RunProc(_ProcePrefix + "_UnActive", prams);
        }

        /// <summary>
        /// 将产品推送发布到所有关联此标准价表的客户价表目录中
        /// </summary>
        /// <param name="PriceID"></param>
        /// <param name="Product"></param>
        /// <returns></returns>
        public int PublishProduct(int PriceID, int Product)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@PriceID", SqlDbType.Int, 4, PriceID),
                SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, Product)
			};
            #endregion

            return SQLDatabase.RunProc(_ProcePrefix + "_PublishProduct", prams);
        }
    }
}

