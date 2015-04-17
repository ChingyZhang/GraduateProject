
// ===================================================================
// 文件： Rpt_DataSet_DataCacheDAL.cs
// 项目名称：
// 创建时间：2010/9/28
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.RPT;


namespace MCSFramework.SQLDAL.RPT
{
    /// <summary>
    ///Rpt_DataSet_DataCache数据访问DAL类
    /// </summary>
    public class Rpt_DataSet_DataCacheDAL : BaseSimpleDAL<Rpt_DataSet_DataCache>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public Rpt_DataSet_DataCacheDAL()
        {
            _ProcePrefix = "MCS_Reports.dbo.sp_Rpt_DataSet_DataCache";
        }
        #endregion


        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(Rpt_DataSet_DataCache m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.UniqueIdentifier, 16, m.ID),
				SQLDatabase.MakeInParam("@DataSet", SqlDbType.UniqueIdentifier, 16, m.DataSet),
				SQLDatabase.MakeInParam("@ParamValues", SqlDbType.VarChar, 2000, m.ParamValues),
				SQLDatabase.MakeInParam("@DataLen", SqlDbType.Int, 4, m.DataLen),
				SQLDatabase.MakeInParam("@Data", SqlDbType.Image, m.Data.Length, m.Data),
                SQLDatabase.MakeInParam("@LoadCount", SqlDbType.Int, 4, m.LoadCount),
				SQLDatabase.MakeInParam("@LastLoadTime", SqlDbType.DateTime, 8, m.LastLoadTime),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            return SQLDatabase.RunProc(_ProcePrefix + "_Add", prams);
        }

        /// <summary>
        /// 更新项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Update(Rpt_DataSet_DataCache m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.UniqueIdentifier, 16, m.ID),
				SQLDatabase.MakeInParam("@DataSet", SqlDbType.UniqueIdentifier, 16, m.DataSet),
				SQLDatabase.MakeInParam("@ParamValues", SqlDbType.VarChar, 2000, m.ParamValues),
				SQLDatabase.MakeInParam("@DataLen", SqlDbType.Int, 4, m.DataLen),
				SQLDatabase.MakeInParam("@Data", SqlDbType.Image, m.Data.Length, m.Data),
                SQLDatabase.MakeInParam("@LoadCount", SqlDbType.Int, 4, m.LoadCount),
				SQLDatabase.MakeInParam("@LastLoadTime", SqlDbType.DateTime, 8, m.LastLoadTime),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override Rpt_DataSet_DataCache FillModel(IDataReader dr)
        {
            Rpt_DataSet_DataCache m = new Rpt_DataSet_DataCache();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (Guid)dr["ID"];
            if (!string.IsNullOrEmpty(dr["DataSet"].ToString())) m.DataSet = (Guid)dr["DataSet"];
            if (!string.IsNullOrEmpty(dr["ParamValues"].ToString())) m.ParamValues = (string)dr["ParamValues"];
            if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString())) m.UpdateTime = (DateTime)dr["UpdateTime"];
            if (!string.IsNullOrEmpty(dr["DataLen"].ToString())) m.DataLen = (int)dr["DataLen"];
            if (!string.IsNullOrEmpty(dr["Data"].ToString())) m.Data = (byte[])dr["Data"];
            if (!string.IsNullOrEmpty(dr["LoadCount"].ToString())) m.LoadCount = (int)dr["LoadCount"];
            if (!string.IsNullOrEmpty(dr["LastLoadTime"].ToString())) m.LastLoadTime = (DateTime)dr["LastLoadTime"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);

            return m;
        }

        public int Clear(Guid DataSet)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@DataSet", SqlDbType.UniqueIdentifier, 16, DataSet)
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Clear", prams);

            return ret;
        }

        public Rpt_DataSet_DataCache Load(Guid DataSet, string ParamValues)
        {
            Rpt_DataSet_DataCache m = null;
            SqlDataReader dr = null;
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@DataSet", SqlDbType.UniqueIdentifier, 16, DataSet),
				SQLDatabase.MakeInParam("@ParamValues", SqlDbType.VarChar, 2000, ParamValues)
			};
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_Load", prams, out dr);

            if (dr.Read())
            {
                m = FillModel(dr);
            }
            dr.Close();

            return m;
        }

        public int SaveForever(Guid DataSet, string ParamValues)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@DataSet", SqlDbType.UniqueIdentifier, 16, DataSet),
				SQLDatabase.MakeInParam("@ParamValues", SqlDbType.VarChar, 2000, ParamValues)
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_SaveForever", prams);
            return ret;
        }
    }
}

