using System;
using System.Collections.Generic;
using System.Text;
using MCSFramework.Model;
using System.Data.SqlClient;
using System.Data;
using MCSFramework.DBUtility;
using MCSFramework.Common;
//using MCSFramework.Common;

namespace MCSFramework.SQLDAL
{
    public class Right_Module_DAL : BaseSimpleDAL<Right_Module>
    {
        #region 系统模块的增加、删除、修改
        public Right_Module_DAL()
        {
            _ProcePrefix = "MCS_SYS.dbo.sp_Right_Module";
        }

        public override int Add(Right_Module m)
        {
            SqlParameterDictionary p = GetParamsCollection(m);

            SqlParameter[] parms ={
                p["ID"],
                p["Name"],
                p["Remark"],
                p["SuperID"],
                p["SortValue"],
                p["EnableFlag"],
                p["Ico"]
                                 };
            return SQLDatabase.RunProc(_ProcePrefix + "_Add", parms);
        }

        public override int Update(Right_Module m)
        {
            SqlParameterDictionary p = GetParamsCollection(m);

            SqlParameter[] parms ={
                p["ID"],
                p["Name"],
                p["Remark"],
                p["SuperID"],
                p["SortValue"],
                p["EnableFlag"],
                p["Ico"]
                                 };
            return SQLDatabase.RunProc(_ProcePrefix + "_Update", parms);
        }

        protected override SqlParameterDictionary GetParamsCollection(Right_Module m)
        {
            SqlParameterDictionary p = new SqlParameterDictionary();

            p.Add("ID", SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID ));
            p.Add("Name", SQLDatabase.MakeInParam("@Name", SqlDbType.VarChar, 50,  m.Name));
            p.Add("Remark", SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 200,  m.Remark));
            p.Add("SuperID", SQLDatabase.MakeInParam("@SuperID", SqlDbType.Int, 4,  m.SuperID));
            p.Add("SortValue", SQLDatabase.MakeInParam("@SortValue", SqlDbType.Int, 4,  m.SortValue));
            p.Add("EnableFlag", SQLDatabase.MakeInParam("@EnableFlag", SqlDbType.Char, 1,  m.EnableFlag));
            p.Add("Ico", SQLDatabase.MakeInParam("@Ico", SqlDbType.VarChar, 20,  m.Ico));

            return p;
        }

        protected override Right_Module FillModel(System.Data.IDataReader dr)
        {
            Right_Module m = new Right_Module();

            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["Name"].ToString())) m.Name = (string)dr["Name"];
            if (!string.IsNullOrEmpty(dr["Remark"].ToString())) m.Remark = (string)dr["Remark"];
            if (!string.IsNullOrEmpty(dr["SuperID"].ToString())) m.SuperID = (int)dr["SuperID"];
            if (!string.IsNullOrEmpty(dr["SortValue"].ToString())) m.SortValue = (int)dr["SortValue"];
            if (!string.IsNullOrEmpty(dr["EnableFlag"].ToString())) m.EnableFlag = (string)dr["EnableFlag"];
            if (!string.IsNullOrEmpty(dr["Ico"].ToString())) m.Ico = (string)dr["Ico"];

            return m;
        }
        #endregion

        #region 获取用户可浏览的模块对象集
        /// <summary>
        /// 返回用户可浏览的模块对象集
        /// </summary>
        /// <param name="applicationname"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public DataTable GetBroweModuleByUser(string applicationname, string username)
        {
            SqlDataReader dr = null;
            SqlParameter[] prams = {
                SQLDatabase.MakeInParam("@ApplicationName",SqlDbType.NVarChar , 256, applicationname),
				SQLDatabase.MakeInParam("@UserName",SqlDbType.NVarChar , 256, username)
					};
            SQLDatabase.RunProc(_ProcePrefix + "_GetBrowseModuleByUser", prams, out dr);

            return Tools.ConvertDataReaderToDataTable(dr);
        }
        #endregion
    }
}
