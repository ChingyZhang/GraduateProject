using System;
using System.Collections.Generic;
using System.Text;
using MCSFramework.Model;
using System.Data.SqlClient;
using System.Data;
using MCSFramework.DBUtility;
using MCSFramework.Common;

namespace MCSFramework.SQLDAL
{
    public class Org_Position_DAL : BaseSimpleDAL<Org_Position>
    {

        public Org_Position_DAL()
        {
            _ProcePrefix = "MCS_SYS.dbo.sp_Org_Position";
        }

        public override int Add(Org_Position m)
        {
            SqlParameterDictionary p = GetParamsCollection(m);

            SqlParameter[] parameters = {
                p["Name"],
                p["SuperID"],
                p["IsHeadOffice"],
				p["Remark"],
                p["Department"],
                p["Enabled"] };

            return SQLDatabase.RunProc(_ProcePrefix+"_Add", parameters);
        }

        public override int Update(Org_Position m)
        {
            SqlParameterDictionary p = GetParamsCollection(m);

            SqlParameter[] parameters = {
                p["ID"],
                p["Name"],
                p["SuperID"],
                p["IsHeadOffice"],
				p["Remark"],
                p["Department"],
                p["Enabled"] };

            return SQLDatabase.RunProc(_ProcePrefix + "_Update", parameters);
        }

        protected override SqlParameterDictionary GetParamsCollection(Org_Position m)
        {
            SqlParameterDictionary p = new SqlParameterDictionary();

            #region 定义数据表的列集合
            p.Add("ID", SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4,m.ID));
            p.Add("Name", SQLDatabase.MakeInParam("@Name", SqlDbType.VarChar, 50,m.Name));
            p.Add("SuperID", SQLDatabase.MakeInParam("@SuperID", SqlDbType.Int, 4, m.SuperID));
            p.Add("IsHeadOffice", SQLDatabase.MakeInParam("@IsHeadOffice", SqlDbType.Char, 1,m.IsHeadOffice));
            p.Add("Remark", SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 200,m.Remark));
            p.Add("Department", SQLDatabase.MakeInParam("@Department", SqlDbType.Int, 4,m.Department));
            p.Add("Enabled", SQLDatabase.MakeInParam("@Enabled", SqlDbType.Char, 1,m.Enabled));
            #endregion

            return p;
        }

        protected override Org_Position FillModel(IDataReader dr)
        {
            Org_Position m = new Org_Position();

            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["Name"].ToString())) m.Name = (string)dr["Name"];
            if (!string.IsNullOrEmpty(dr["SuperID"].ToString())) m.SuperID = (int)dr["SuperID"];
            if (!string.IsNullOrEmpty(dr["IsHeadOffice"].ToString())) m.IsHeadOffice = (string)dr["IsHeadOffice"];
            if (!string.IsNullOrEmpty(dr["Remark"].ToString())) m.Remark = (string)dr["Remark"];
            if (!string.IsNullOrEmpty(dr["Department"].ToString())) m.Department = (int)dr["Department"];
            if (!string.IsNullOrEmpty(dr["Enabled"].ToString())) m.Enabled = (string)dr["Enabled"];

            return m;
        }

        public DataTable GetAllPosition()
        {
            SqlDataReader dr = null;

            SQLDatabase.RunProc("sp_Org_Position_GetByCondition", out dr);

            return Tools.ConvertDataReaderToDataTable(dr);
        }
    }
}
