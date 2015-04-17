
// ===================================================================
// 文件： UD_Panel_TableRelationDAL.cs
// 项目名称：
// 创建时间：2008-12-9
// 作者:	   
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using MCSFramework.Common;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model;


namespace MCSFramework.SQLDAL
{
    /// <summary>
    ///UD_Panel_TableRelation数据访问DAL类
    /// </summary>
    public class UD_Panel_TableRelationDAL : BaseSimpleDAL<UD_Panel_TableRelation>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public UD_Panel_TableRelationDAL()
        {
            _ProcePrefix = "MCS_SYS.dbo.sp_UD_Panel_TableRelation";
        }
        #endregion


        #region 成员方法
        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(UD_Panel_TableRelation m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.UniqueIdentifier, 16, m.ID),
				SQLDatabase.MakeInParam("@PanelID", SqlDbType.UniqueIdentifier, 16, m.PanelID),
				SQLDatabase.MakeInParam("@ParentTableID", SqlDbType.UniqueIdentifier, 16, m.ParentTableID),
				SQLDatabase.MakeInParam("@ParentFieldID", SqlDbType.UniqueIdentifier, 16, m.ParentFieldID),
				SQLDatabase.MakeInParam("@ChildTableID", SqlDbType.UniqueIdentifier, 16, m.ChildTableID),
				SQLDatabase.MakeInParam("@ChildFieldID", SqlDbType.UniqueIdentifier, 16, m.ChildFieldID),
                SQLDatabase.MakeInParam("@JoinMode", SqlDbType.VarChar, 20, m.JoinMode),
                SQLDatabase.MakeInParam("@RelationCondition", SqlDbType.VarChar, 500, m.RelationCondition),
                SQLDatabase.MakeInParam("@SortID", SqlDbType.Int, 4, m.SortID)
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Add", prams);

            return ret;
        }

        /// <summary>
        /// 更新项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Update(UD_Panel_TableRelation m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.UniqueIdentifier, 16, m.ID),
				SQLDatabase.MakeInParam("@PanelID", SqlDbType.UniqueIdentifier, 16, m.PanelID),
				SQLDatabase.MakeInParam("@ParentTableID", SqlDbType.UniqueIdentifier, 16, m.ParentTableID),
				SQLDatabase.MakeInParam("@ParentFieldID", SqlDbType.UniqueIdentifier, 16, m.ParentFieldID),
				SQLDatabase.MakeInParam("@ChildTableID", SqlDbType.UniqueIdentifier, 16, m.ChildTableID),
				SQLDatabase.MakeInParam("@ChildFieldID", SqlDbType.UniqueIdentifier, 16, m.ChildFieldID),
                SQLDatabase.MakeInParam("@JoinMode", SqlDbType.VarChar, 20, m.JoinMode),
                SQLDatabase.MakeInParam("@RelationCondition", SqlDbType.VarChar, 500, m.RelationCondition),
                SQLDatabase.MakeInParam("@SortID", SqlDbType.Int, 4, m.SortID)
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
            return ret;
        }

        protected override UD_Panel_TableRelation FillModel(IDataReader dr)
        {
            UD_Panel_TableRelation m = new UD_Panel_TableRelation();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (Guid)dr["ID"];
            if (!string.IsNullOrEmpty(dr["PanelID"].ToString())) m.PanelID = (Guid)dr["PanelID"];
            if (!string.IsNullOrEmpty(dr["ParentTableID"].ToString())) m.ParentTableID = (Guid)dr["ParentTableID"];
            if (!string.IsNullOrEmpty(dr["ParentFieldID"].ToString())) m.ParentFieldID = (Guid)dr["ParentFieldID"];
            if (!string.IsNullOrEmpty(dr["ChildTableID"].ToString())) m.ChildTableID = (Guid)dr["ChildTableID"];
            if (!string.IsNullOrEmpty(dr["ChildFieldID"].ToString())) m.ChildFieldID = (Guid)dr["ChildFieldID"];
            if (!string.IsNullOrEmpty(dr["JoinMode"].ToString())) m.JoinMode = (string)dr["JoinMode"];
            if (!string.IsNullOrEmpty(dr["RelationCondition"].ToString())) m.RelationCondition = (string)dr["RelationCondition"];
            if (!string.IsNullOrEmpty(dr["SortID"].ToString())) m.SortID = (int)dr["SortID"];

            return m;
        }
        #endregion
    }
}

