
// ===================================================================
// 文件： UD_ModelFieldsDAL.cs
// 项目名称：
// 创建时间：2008-11-25
// 作者:	   
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using MCSFramework.Common;
using MCSFramework.DBUtility;
using MCSFramework.Model;
using System.Collections.Generic;
using System.Web.Caching;


namespace MCSFramework.SQLDAL
{
    /// <summary>
    ///UD_ModelFields数据访问DAL类
    /// </summary>
    public class UD_ModelFieldsDAL : BaseSimpleDAL<UD_ModelFields>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public UD_ModelFieldsDAL()
        {
            _ProcePrefix = "MCS_SYS.dbo.sp_UD_ModelFields";
        }
        #endregion


        #region 成员方法
        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(UD_ModelFields m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
                SQLDatabase.MakeInParam("@ID", SqlDbType.UniqueIdentifier, 16, m.ID),
				SQLDatabase.MakeInParam("@TableID", SqlDbType.UniqueIdentifier, 16, m.TableID),
				SQLDatabase.MakeInParam("@FieldName", SqlDbType.VarChar, 50, m.FieldName),
				SQLDatabase.MakeInParam("@DisplayName", SqlDbType.VarChar, 50, m.DisplayName),
				SQLDatabase.MakeInParam("@Position", SqlDbType.Int, 4, m.Position),
				SQLDatabase.MakeInParam("@Flag", SqlDbType.Char, 1, m.Flag),
				SQLDatabase.MakeInParam("@DataType", SqlDbType.Int, 4, m.DataType),
				SQLDatabase.MakeInParam("@DataLength", SqlDbType.Int, 4, m.DataLength),
				SQLDatabase.MakeInParam("@Precision", SqlDbType.Int, 4, m.Precision),
				SQLDatabase.MakeInParam("@DefaultValue", SqlDbType.VarChar, 50, m.DefaultValue),
				SQLDatabase.MakeInParam("@Description", SqlDbType.VarChar, 2000, m.Description),
				SQLDatabase.MakeInParam("@LastUpdateTime", SqlDbType.DateTime, 8, m.LastUpdateTime),
				SQLDatabase.MakeInParam("@RelationType", SqlDbType.Int, 4, m.RelationType),
				SQLDatabase.MakeInParam("@RelationTableName", SqlDbType.VarChar, 50, m.RelationTableName),
				SQLDatabase.MakeInParam("@RelationValueField", SqlDbType.VarChar, 50, m.RelationValueField),
				SQLDatabase.MakeInParam("@RelationTextField", SqlDbType.VarChar, 50, m.RelationTextField),
                SQLDatabase.MakeInParam("@SearchPageURL", SqlDbType.VarChar, 200, m.SearchPageURL)
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
        public override int Update(UD_ModelFields m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.UniqueIdentifier, 16, m.ID),
				SQLDatabase.MakeInParam("@TableID", SqlDbType.UniqueIdentifier, 16, m.TableID),
				SQLDatabase.MakeInParam("@FieldName", SqlDbType.VarChar, 50, m.FieldName),
				SQLDatabase.MakeInParam("@DisplayName", SqlDbType.VarChar, 50, m.DisplayName),
				SQLDatabase.MakeInParam("@Position", SqlDbType.Int, 4, m.Position),
				SQLDatabase.MakeInParam("@Flag", SqlDbType.Char, 1, m.Flag),
				SQLDatabase.MakeInParam("@DataType", SqlDbType.Int, 4, m.DataType),
				SQLDatabase.MakeInParam("@DataLength", SqlDbType.Int, 4, m.DataLength),
				SQLDatabase.MakeInParam("@Precision", SqlDbType.Int, 4, m.Precision),
				SQLDatabase.MakeInParam("@DefaultValue", SqlDbType.VarChar, 50, m.DefaultValue),
				SQLDatabase.MakeInParam("@Description", SqlDbType.VarChar, 2000, m.Description),
				SQLDatabase.MakeInParam("@LastUpdateTime", SqlDbType.DateTime, 8, m.LastUpdateTime),
				SQLDatabase.MakeInParam("@RelationType", SqlDbType.Int, 4, m.RelationType),
				SQLDatabase.MakeInParam("@RelationTableName", SqlDbType.VarChar, 50, m.RelationTableName),
				SQLDatabase.MakeInParam("@RelationValueField", SqlDbType.VarChar, 50, m.RelationValueField),
				SQLDatabase.MakeInParam("@RelationTextField", SqlDbType.VarChar, 50, m.RelationTextField),
                SQLDatabase.MakeInParam("@SearchPageURL", SqlDbType.VarChar, 200, m.SearchPageURL)
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
            return ret;
        }

        protected override UD_ModelFields FillModel(IDataReader dr)
        {
            UD_ModelFields m = new UD_ModelFields();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (Guid)dr["ID"];
            if (!string.IsNullOrEmpty(dr["TableID"].ToString())) m.TableID = (Guid)dr["TableID"];
            if (!string.IsNullOrEmpty(dr["FieldName"].ToString())) m.FieldName = (string)dr["FieldName"];
            if (!string.IsNullOrEmpty(dr["DisplayName"].ToString())) m.DisplayName = (string)dr["DisplayName"];
            if (!string.IsNullOrEmpty(dr["Position"].ToString())) m.Position = (int)dr["Position"];
            if (!string.IsNullOrEmpty(dr["Flag"].ToString())) m.Flag = (string)dr["Flag"];
            if (!string.IsNullOrEmpty(dr["DataType"].ToString())) m.DataType = (int)dr["DataType"];
            if (!string.IsNullOrEmpty(dr["DataLength"].ToString())) m.DataLength = (int)dr["DataLength"];
            if (!string.IsNullOrEmpty(dr["Precision"].ToString())) m.Precision = (int)dr["Precision"];
            if (!string.IsNullOrEmpty(dr["DefaultValue"].ToString())) m.DefaultValue = (string)dr["DefaultValue"];
            if (!string.IsNullOrEmpty(dr["Description"].ToString())) m.Description = (string)dr["Description"];
            if (!string.IsNullOrEmpty(dr["LastUpdateTime"].ToString())) m.LastUpdateTime = (DateTime)dr["LastUpdateTime"];

            if (!string.IsNullOrEmpty(dr["RelationType"].ToString())) m.RelationType = (int)dr["RelationType"];
            if (!string.IsNullOrEmpty(dr["RelationTableName"].ToString())) m.RelationTableName = (string)dr["RelationTableName"];
            if (!string.IsNullOrEmpty(dr["RelationValueField"].ToString())) m.RelationValueField = (string)dr["RelationValueField"];
            if (!string.IsNullOrEmpty(dr["RelationTextField"].ToString())) m.RelationTextField = (string)dr["RelationTextField"];
            if (!string.IsNullOrEmpty(dr["SearchPageURL"].ToString())) m.SearchPageURL = (string)dr["SearchPageURL"];


            return m;
        }

        public void Init(Guid TableID)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@TableID", SqlDbType.UniqueIdentifier, 16, TableID)
			};
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_Init", prams);
        }

        /// <summary>
        /// 获取指定Model的扩展字段
        /// </summary>
        /// <param name="ModelName"></param>
        /// <returns></returns>
        public IList<UD_ModelFields> GetExtFieldsList(string ModelName)
        {
            string CacheKey = "UD_TableList-ExtFieldsList-" + ModelName;
            IList<UD_ModelFields> list = (IList<UD_ModelFields>)DataCache.GetCache(CacheKey);

            if (list == null)
            {
                #region	设置参数集
                SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ModelName", SqlDbType.VarChar,50, ModelName)
			        };
                #endregion

                SqlDataReader dr = null;
                SQLDatabase.RunProc(_ProcePrefix + "_GetExtFieldsList", prams, out dr);
                list = FillModelList(dr);

                AggregateCacheDependency cachedependency = new AggregateCacheDependency();
                cachedependency.Add(new SqlCacheDependency("MCS_SYS", "UD_ModelFields"));

                DataCache.SetCache(CacheKey, list, cachedependency);
            }
            return list;
        }
        #endregion


    }
}

