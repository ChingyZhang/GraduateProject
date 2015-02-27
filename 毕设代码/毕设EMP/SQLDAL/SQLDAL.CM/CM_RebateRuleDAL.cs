
// ===================================================================
// 文件： CM_RebateRuleDAL.cs
// 项目名称：
// 创建时间：2011/11/16
// 作者:	   Shen Gang
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
    ///CM_RebateRule数据访问DAL类
    /// </summary>
    public class CM_RebateRuleDAL : BaseSimpleDAL<CM_RebateRule>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public CM_RebateRuleDAL()
        {
            _ProcePrefix = "MCS_CM.dbo.sp_CM_RebateRule";
        }
        #endregion


        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(CM_RebateRule m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Name", SqlDbType.VarChar, 200, m.Name),
				SQLDatabase.MakeInParam("@RebateRate", SqlDbType.Decimal, 9, m.RebateRate),
				SQLDatabase.MakeInParam("@DIRebateRate", SqlDbType.Decimal, 9, m.DIRebateRate),
				SQLDatabase.MakeInParam("@StandardPrice", SqlDbType.Int, 4, m.StandardPrice),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 2000, m.Remark),
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
        public override int Update(CM_RebateRule m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@Name", SqlDbType.VarChar, 200, m.Name),
				SQLDatabase.MakeInParam("@RebateRate", SqlDbType.Decimal, 9, m.RebateRate),
				SQLDatabase.MakeInParam("@DIRebateRate", SqlDbType.Decimal, 9, m.DIRebateRate),
				SQLDatabase.MakeInParam("@StandardPrice", SqlDbType.Int, 4, m.StandardPrice),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 2000, m.Remark),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@UpdateStaff", SqlDbType.Int, 4, m.UpdateStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override CM_RebateRule FillModel(IDataReader dr)
        {
            CM_RebateRule m = new CM_RebateRule();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["Name"].ToString())) m.Name = (string)dr["Name"];
            if (!string.IsNullOrEmpty(dr["RebateRate"].ToString())) m.RebateRate = (decimal)dr["RebateRate"];
            if (!string.IsNullOrEmpty(dr["DIRebateRate"].ToString())) m.DIRebateRate = (decimal)dr["DIRebateRate"];
            if (!string.IsNullOrEmpty(dr["StandardPrice"].ToString())) m.StandardPrice = (int)dr["StandardPrice"]; 
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

