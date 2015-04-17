
// ===================================================================
// 文件： LOT_LotNumberDAL.cs
// 项目名称：
// 创建时间：2012-11-11
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.EBM;
using System.Collections.Generic;


namespace MCSFramework.SQLDAL.EBM
{
    /// <summary>
    ///LOT_LotNumber数据访问DAL类
    /// </summary>
    public class LOT_LotNumberDAL : BaseComplexDAL<LOT_LotNumber, LOT_MaterialDetail>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public LOT_LotNumberDAL()
        {
            _ProcePrefix = "MCS_EBM.dbo.sp_LOT_LotNumber";
        }
        #endregion


        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(LOT_LotNumber m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@LotNumber", SqlDbType.VarChar, 50, m.LotNumber),
				SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, m.Product),
				SQLDatabase.MakeInParam("@ProductionDate", SqlDbType.DateTime, 8, m.ProductionDate),
				SQLDatabase.MakeInParam("@Manufacturer", SqlDbType.Int, 4, m.Manufacturer),
				SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, m.State),
				SQLDatabase.MakeInParam("@QualityReportCode", SqlDbType.VarChar, 50, m.QualityReportCode),
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
        public override int Update(LOT_LotNumber m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@LotNumber", SqlDbType.VarChar, 50, m.LotNumber),
				SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, m.Product),
				SQLDatabase.MakeInParam("@ProductionDate", SqlDbType.DateTime, 8, m.ProductionDate),
				SQLDatabase.MakeInParam("@Manufacturer", SqlDbType.Int, 4, m.Manufacturer),
				SQLDatabase.MakeInParam("@State", SqlDbType.Int, 4, m.State),
				SQLDatabase.MakeInParam("@QualityReportCode", SqlDbType.VarChar, 50, m.QualityReportCode),
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

        protected override LOT_LotNumber FillModel(IDataReader dr)
        {
            LOT_LotNumber m = new LOT_LotNumber();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["LotNumber"].ToString())) m.LotNumber = (string)dr["LotNumber"];
            if (!string.IsNullOrEmpty(dr["Product"].ToString())) m.Product = (int)dr["Product"];
            if (!string.IsNullOrEmpty(dr["ProductionDate"].ToString())) m.ProductionDate = (DateTime)dr["ProductionDate"];
            if (!string.IsNullOrEmpty(dr["Manufacturer"].ToString())) m.Manufacturer = (int)dr["Manufacturer"];
            if (!string.IsNullOrEmpty(dr["State"].ToString())) m.State = (int)dr["State"];
            if (!string.IsNullOrEmpty(dr["QualityReportCode"].ToString())) m.QualityReportCode = (string)dr["QualityReportCode"];
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

        public override int AddDetail(LOT_MaterialDetail m)
        {
            m.LotID = HeadID;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@LotID", SqlDbType.Int, 4, m.LotID),
				SQLDatabase.MakeInParam("@Material", SqlDbType.Int, 4, m.Material),
				SQLDatabase.MakeInParam("@OriginCountry", SqlDbType.Int, 4, m.OriginCountry),
				SQLDatabase.MakeInParam("@DepartureDate", SqlDbType.DateTime, 8, m.DepartureDate),
				SQLDatabase.MakeInParam("@ArrivalDate", SqlDbType.DateTime, 8, m.ArrivalDate),
				SQLDatabase.MakeInParam("@EntryCustomsCode", SqlDbType.VarChar, 50, m.EntryCustomsCode),
				SQLDatabase.MakeInParam("@ExitCustomsDate", SqlDbType.DateTime, 8, m.ExitCustomsDate),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 2000, m.Remark),
				SQLDatabase.MakeInParam("@InsertUser", SqlDbType.UniqueIdentifier, 16, m.InsertUser),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            m.ID = SQLDatabase.RunProc(_ProcePrefix + "_AddDetail", prams);

            return m.ID;
        }

        public override int UpdateDetail(LOT_MaterialDetail m)
        {
            m.LotID = HeadID;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@LotID", SqlDbType.Int, 4, m.LotID),
				SQLDatabase.MakeInParam("@Material", SqlDbType.Int, 4, m.Material),
				SQLDatabase.MakeInParam("@OriginCountry", SqlDbType.Int, 4, m.OriginCountry),
				SQLDatabase.MakeInParam("@DepartureDate", SqlDbType.DateTime, 8, m.DepartureDate),
				SQLDatabase.MakeInParam("@ArrivalDate", SqlDbType.DateTime, 8, m.ArrivalDate),
				SQLDatabase.MakeInParam("@EntryCustomsCode", SqlDbType.VarChar, 50, m.EntryCustomsCode),
				SQLDatabase.MakeInParam("@ExitCustomsDate", SqlDbType.DateTime, 8, m.ExitCustomsDate),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 2000, m.Remark),
				SQLDatabase.MakeInParam("@InsertUser", SqlDbType.UniqueIdentifier, 16, m.InsertUser),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_UpdateDetail", prams);

            return ret;
        }

        protected override LOT_MaterialDetail FillDetailModel(IDataReader dr)
        {
            LOT_MaterialDetail m = new LOT_MaterialDetail();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["LotID"].ToString())) m.LotID = (int)dr["LotID"];
            if (!string.IsNullOrEmpty(dr["Material"].ToString())) m.Material = (int)dr["Material"];
            if (!string.IsNullOrEmpty(dr["OriginCountry"].ToString())) m.OriginCountry = (int)dr["OriginCountry"];
            if (!string.IsNullOrEmpty(dr["DepartureDate"].ToString())) m.DepartureDate = (DateTime)dr["DepartureDate"];
            if (!string.IsNullOrEmpty(dr["ArrivalDate"].ToString())) m.ArrivalDate = (DateTime)dr["ArrivalDate"];
            if (!string.IsNullOrEmpty(dr["EntryCustomsCode"].ToString())) m.EntryCustomsCode = (string)dr["EntryCustomsCode"];
            if (!string.IsNullOrEmpty(dr["ExitCustomsDate"].ToString())) m.ExitCustomsDate = (DateTime)dr["ExitCustomsDate"];
            if (!string.IsNullOrEmpty(dr["Remark"].ToString())) m.Remark = (string)dr["Remark"];
            if (!string.IsNullOrEmpty(dr["InsertTime"].ToString())) m.InsertTime = (DateTime)dr["InsertTime"];
            if (!string.IsNullOrEmpty(dr["InsertUser"].ToString())) m.InsertUser = (Guid)dr["InsertUser"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);


            return m;
        }

        #region 获取某产品的批号信息
        /// <summary>
        /// 获取某产品的批号信息
        /// </summary>
        /// <param name="Product"></param>
        /// <returns></returns>
        public IList<LOT_LotNumber> GetListByProduct(int Product)
        {
            SqlDataReader dr = null;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, Product)
			};
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_GetListByProduct", prams, out dr);

            return FillModelList(dr);
        }

        /// <summary>
        /// 获取指定生产日期范围内某产品的批号信息
        /// </summary>
        /// <param name="Product"></param>
        /// <param name="BeginProductionDate"></param>
        /// <param name="EndProductionDate"></param>
        /// <returns></returns>
        public IList<LOT_LotNumber> GetListByProduct(int Product, DateTime BeginProductionDate, DateTime EndProductionDate)
        {
            SqlDataReader dr = null;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Product", SqlDbType.Int, 4, Product),
                SQLDatabase.MakeInParam("@BeginProductionDate", SqlDbType.DateTime, 8, BeginProductionDate),
                SQLDatabase.MakeInParam("@EndProductionDate", SqlDbType.DateTime, 8, EndProductionDate)
			};
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_GetListByProduct", prams, out dr);

            return FillModelList(dr);
        }
        #endregion
    }
}

