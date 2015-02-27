
// ===================================================================
// 文件： QNA_ProjectDAL.cs
// 项目名称：
// 创建时间：2009/11/29
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.QNA;
using MCSFramework.Common;

namespace MCSFramework.SQLDAL.QNA
{
    /// <summary>
    ///QNA_Project数据访问DAL类
    /// </summary>
    public class QNA_ProjectDAL : BaseSimpleDAL<QNA_Project>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public QNA_ProjectDAL()
        {
            _ProcePrefix = "MCS_QNA.dbo.sp_QNA_Project";
        }
        #endregion


        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(QNA_Project m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Name", SqlDbType.VarChar, 200, m.Name),
				SQLDatabase.MakeInParam("@Classify", SqlDbType.Int, 4, m.Classify),
				SQLDatabase.MakeInParam("@DisplayType", SqlDbType.Int, 4, m.DisplayType),
				SQLDatabase.MakeInParam("@Enabled", SqlDbType.Char, 1, m.Enabled),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@InsertStaff", SqlDbType.Int, 4, m.InsertStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName)),
				SQLDatabase.MakeInParam("@FaceTo", SqlDbType.Int, 4, m.FaceTo),
				SQLDatabase.MakeInParam("@ToAllStaff", SqlDbType.Char, 1, m.ToAllStaff),
				SQLDatabase.MakeInParam("@ToAllOrganizeCity", SqlDbType.Char, 1, m.ToAllOrganizeCity),
				SQLDatabase.MakeInParam("@EffectiveTime", SqlDbType.DateTime, 8, m.EffectiveTime),
				SQLDatabase.MakeInParam("@CloseTime", SqlDbType.DateTime, 8, m.CloseTime)
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
        public override int Update(QNA_Project m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@Name", SqlDbType.VarChar, 200, m.Name),
				SQLDatabase.MakeInParam("@Classify", SqlDbType.Int, 4, m.Classify),
				SQLDatabase.MakeInParam("@DisplayType", SqlDbType.Int, 4, m.DisplayType),
				SQLDatabase.MakeInParam("@Enabled", SqlDbType.Char, 1, m.Enabled),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@UpdateStaff", SqlDbType.Int, 4, m.UpdateStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName)),
				SQLDatabase.MakeInParam("@FaceTo", SqlDbType.Int, 4, m.FaceTo),
				SQLDatabase.MakeInParam("@ToAllStaff", SqlDbType.Char, 1, m.ToAllStaff),
				SQLDatabase.MakeInParam("@ToAllOrganizeCity", SqlDbType.Char, 1, m.ToAllOrganizeCity),
				SQLDatabase.MakeInParam("@EffectiveTime", SqlDbType.DateTime, 8, m.EffectiveTime),
				SQLDatabase.MakeInParam("@CloseTime", SqlDbType.DateTime, 8, m.CloseTime)
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override QNA_Project FillModel(IDataReader dr)
        {
            QNA_Project m = new QNA_Project();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["Name"].ToString())) m.Name = (string)dr["Name"];
            if (!string.IsNullOrEmpty(dr["Classify"].ToString())) m.Classify = (int)dr["Classify"];
            if (!string.IsNullOrEmpty(dr["DisplayType"].ToString())) m.DisplayType = (int)dr["DisplayType"];
            if (!string.IsNullOrEmpty(dr["Enabled"].ToString())) m.Enabled = (string)dr["Enabled"];
            if (!string.IsNullOrEmpty(dr["ApproveFlag"].ToString())) m.ApproveFlag = (int)dr["ApproveFlag"];
            if (!string.IsNullOrEmpty(dr["InsertTime"].ToString())) m.InsertTime = (DateTime)dr["InsertTime"];
            if (!string.IsNullOrEmpty(dr["InsertStaff"].ToString())) m.InsertStaff = (int)dr["InsertStaff"];
            if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString())) m.UpdateTime = (DateTime)dr["UpdateTime"];
            if (!string.IsNullOrEmpty(dr["UpdateStaff"].ToString())) m.UpdateStaff = (int)dr["UpdateStaff"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);
            if (!string.IsNullOrEmpty(dr["FaceTo"].ToString())) m.FaceTo = (int)dr["FaceTo"];
            if (!string.IsNullOrEmpty(dr["ToAllStaff"].ToString())) m.ToAllStaff = (string)dr["ToAllStaff"];
            if (!string.IsNullOrEmpty(dr["ToAllOrganizeCity"].ToString())) m.ToAllOrganizeCity = (string)dr["ToAllOrganizeCity"];
            if (!string.IsNullOrEmpty(dr["EffectiveTime"].ToString())) m.EffectiveTime = (DateTime)dr["EffectiveTime"];
            if (!string.IsNullOrEmpty(dr["CloseTime"].ToString())) m.CloseTime = (DateTime)dr["CloseTime"];

            return m;
        }
        /// <summary>
        /// 获取问卷统计结果
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public DataTable GetResultStatistics(int project)
        {
            SqlDataReader dr = null;
            #region	设置参数集
            SqlParameter[] prams = { SQLDatabase.MakeInParam("@Project", SqlDbType.Int, 4, project) };
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_GetResultStatistics", prams, out dr);

            return Tools.ConvertDataReaderToDataTable(dr);
        }

        /// <summary>
        /// 获取问卷调研份数
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public int GetResultCount(int project)
        {
            #region	设置参数集
            SqlParameter[] prams = { SQLDatabase.MakeInParam("@Project", SqlDbType.Int, 4, project) };
            #endregion

            return SQLDatabase.RunProc(_ProcePrefix + "_GetResultCount", prams);
        }
    }
}

