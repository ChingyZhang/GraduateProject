
// ===================================================================
// 文件： Info_ChangeHistoryDAL.cs
// 项目名称：
// 创建时间：2012/2/23
// 作者:	   chf
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model;


namespace MCSFramework.SQLDAL
{
    /// <summary>
    ///Info_ChangeHistory数据访问DAL类
    /// </summary>
    public class Info_ChangeHistoryDAL : BaseSimpleDAL<Info_ChangeHistory>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public Info_ChangeHistoryDAL()
        {
            _ProcePrefix = "MCS_SYS.dbo.sp_Info_ChangeHistory";
        }
        #endregion


        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(Info_ChangeHistory m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@TableID", SqlDbType.UniqueIdentifier, 16, m.TableID),
				SQLDatabase.MakeInParam("@FieldID", SqlDbType.UniqueIdentifier, 16, m.FieldID),
				SQLDatabase.MakeInParam("@InfoID", SqlDbType.Int, 4, m.InfoID),
				SQLDatabase.MakeInParam("@OldValue", SqlDbType.VarChar, 500, m.OldValue),
				SQLDatabase.MakeInParam("@NewValue", SqlDbType.VarChar, 500, m.NewValue),
				SQLDatabase.MakeInParam("@InfoType", SqlDbType.Int, 4, m.InfoType),
				SQLDatabase.MakeInParam("@ChangeStaff", SqlDbType.Int, 4, m.ChangeStaff),
				SQLDatabase.MakeInParam("@ChangeTime", SqlDbType.DateTime, 8, m.ChangeTime),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 2000, m.Remark),
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
        public override int Update(Info_ChangeHistory m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@TableID", SqlDbType.UniqueIdentifier, 16, m.TableID),
				SQLDatabase.MakeInParam("@FieldID", SqlDbType.UniqueIdentifier, 16, m.FieldID),
				SQLDatabase.MakeInParam("@InfoID", SqlDbType.Int, 4, m.InfoID),
				SQLDatabase.MakeInParam("@OldValue", SqlDbType.VarChar, 500, m.OldValue),
				SQLDatabase.MakeInParam("@NewValue", SqlDbType.VarChar, 500, m.NewValue),
				SQLDatabase.MakeInParam("@InfoType", SqlDbType.Int, 4, m.InfoType),
				SQLDatabase.MakeInParam("@ChangeStaff", SqlDbType.Int, 4, m.ChangeStaff),
				SQLDatabase.MakeInParam("@ChangeTime", SqlDbType.DateTime, 8, m.ChangeTime),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 2000, m.Remark),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override Info_ChangeHistory FillModel(IDataReader dr)
        {
            Info_ChangeHistory m = new Info_ChangeHistory();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["TableID"].ToString())) m.TableID = (Guid)dr["TableID"];
            if (!string.IsNullOrEmpty(dr["FieldID"].ToString())) m.FieldID = (Guid)dr["FieldID"];
            if (!string.IsNullOrEmpty(dr["InfoID"].ToString())) m.InfoID = (int)dr["InfoID"];
            if (!string.IsNullOrEmpty(dr["OldValue"].ToString())) m.OldValue = (string)dr["OldValue"];
            if (!string.IsNullOrEmpty(dr["NewValue"].ToString())) m.NewValue = (string)dr["NewValue"];
            if (!string.IsNullOrEmpty(dr["InfoType"].ToString())) m.InfoType = (int)dr["InfoType"];
            if (!string.IsNullOrEmpty(dr["ChangeStaff"].ToString())) m.ChangeStaff = (int)dr["ChangeStaff"];
            if (!string.IsNullOrEmpty(dr["ChangeTime"].ToString())) m.ChangeTime = (DateTime)dr["ChangeTime"];
            if (!string.IsNullOrEmpty(dr["Remark"].ToString())) m.Remark = (string)dr["Remark"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);

            return m;
        }
    }
}

