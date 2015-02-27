
// ===================================================================
// 文件： PDT_ClassifyGiftCostRateDAL.cs
// 项目名称：
// 创建时间：2013/8/26
// 作者:	   chf
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.Pub;


namespace MCSFramework.SQLDAL.Pub
{
    /// <summary>
    ///PDT_ClassifyGiftCostRate数据访问DAL类
    /// </summary>
    public class PDT_ClassifyGiftCostRateDAL : BaseSimpleDAL<PDT_ClassifyGiftCostRate>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public PDT_ClassifyGiftCostRateDAL()
        {
            _ProcePrefix = "MCS_Pub.dbo.sp_PDT_ClassifyGiftCostRate";
        }
        #endregion


        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(PDT_ClassifyGiftCostRate m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@BeginMonth", SqlDbType.Int, 4, m.BeginMonth),
				SQLDatabase.MakeInParam("@Cycle", SqlDbType.Int, 4, m.Cycle),
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, m.OrganizeCity),
				SQLDatabase.MakeInParam("@PDTBrand", SqlDbType.Int, 4, m.PDTBrand),
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, m.Client),
				SQLDatabase.MakeInParam("@GiftCostClassify", SqlDbType.Int, 4, m.GiftCostClassify),
				SQLDatabase.MakeInParam("@GiftCostRate", SqlDbType.Decimal, 9, m.GiftCostRate),
				SQLDatabase.MakeInParam("@Enabled", SqlDbType.Char, 1, m.Enabled),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 500, m.Remark),
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
        public override int Update(PDT_ClassifyGiftCostRate m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@BeginMonth", SqlDbType.Int, 4, m.BeginMonth),
				SQLDatabase.MakeInParam("@Cycle", SqlDbType.Int, 4, m.Cycle),
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, m.OrganizeCity),
				SQLDatabase.MakeInParam("@PDTBrand", SqlDbType.Int, 4, m.PDTBrand),
				SQLDatabase.MakeInParam("@Client", SqlDbType.Int, 4, m.Client),
				SQLDatabase.MakeInParam("@GiftCostClassify", SqlDbType.Int, 4, m.GiftCostClassify),
				SQLDatabase.MakeInParam("@GiftCostRate", SqlDbType.Decimal, 9, m.GiftCostRate),
				SQLDatabase.MakeInParam("@Enabled", SqlDbType.Char, 1, m.Enabled),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 500, m.Remark),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@UpdateStaff", SqlDbType.Int, 4, m.UpdateStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override PDT_ClassifyGiftCostRate FillModel(IDataReader dr)
        {
            PDT_ClassifyGiftCostRate m = new PDT_ClassifyGiftCostRate();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["BeginMonth"].ToString())) m.BeginMonth = (int)dr["BeginMonth"];
            if (!string.IsNullOrEmpty(dr["Cycle"].ToString())) m.Cycle = (int)dr["Cycle"];
            if (!string.IsNullOrEmpty(dr["OrganizeCity"].ToString())) m.OrganizeCity = (int)dr["OrganizeCity"];
            if (!string.IsNullOrEmpty(dr["PDTBrand"].ToString())) m.PDTBrand = (int)dr["PDTBrand"];
            if (!string.IsNullOrEmpty(dr["Client"].ToString())) m.Client = (int)dr["Client"];
            if (!string.IsNullOrEmpty(dr["GiftCostClassify"].ToString())) m.GiftCostClassify = (int)dr["GiftCostClassify"];
            if (!string.IsNullOrEmpty(dr["GiftCostRate"].ToString())) m.GiftCostRate = (decimal)dr["GiftCostRate"];
            if (!string.IsNullOrEmpty(dr["Enabled"].ToString())) m.Enabled = (string)dr["Enabled"];
            if (!string.IsNullOrEmpty(dr["Remark"].ToString())) m.Remark = (string)dr["Remark"];
            if (!string.IsNullOrEmpty(dr["ApproveFlag"].ToString())) m.ApproveFlag = (int)dr["ApproveFlag"];
            if (!string.IsNullOrEmpty(dr["InsertTime"].ToString())) m.InsertTime = (DateTime)dr["InsertTime"];
            if (!string.IsNullOrEmpty(dr["InsertStaff"].ToString())) m.InsertStaff = (int)dr["InsertStaff"];
            if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString())) m.UpdateTime = (DateTime)dr["UpdateTime"];
            if (!string.IsNullOrEmpty(dr["UpdateStaff"].ToString())) m.UpdateStaff = (int)dr["UpdateStaff"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);

            return m;
        }
    }
}

