using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using MCSFramework.Model;
using MCSFramework.DBUtility;

namespace MCSFramework.SQLDAL
{
    public class Dictionary_DAL : BaseSimpleDAL<Dictionary_Data>
    {
        public Dictionary_DAL()
        {
            _ProcePrefix = "MCS_SYS.dbo.sp_Dictionary_Data";
        }
        /// <summary>
        /// 新增字典项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(Dictionary_Data m)
        {
            SqlParameterDictionary p = GetParamsCollection(m);

            SqlParameter[] prams = {
                p["ID"],
                p["Code"],
                p["Name"],
                p["Type"],
                p["Enabled"],
                p["Description"],
                p["InsertStaff"]
			};
            return SQLDatabase.RunProc(_ProcePrefix + "_ADD", prams);
        }

        /// <summary>
        /// 更新字典项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Update(Dictionary_Data m)
        {
            SqlParameterDictionary p = GetParamsCollection(m);

            SqlParameter[] prams = {
                p["ID"],
                p["Code"],
                p["Name"],
                p["Type"],
                p["Enabled"],
                p["Description"],
                p["UpdateStaff"]
			};
            return SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
        }

        protected override SqlParameterDictionary GetParamsCollection(Dictionary_Data m)
        {
            SqlParameterDictionary p = new SqlParameterDictionary();

            p.Add("ID", SQLDatabase.MakeInParam("@ID", SqlDbType.UniqueIdentifier, 4, m.ID));
            p.Add("Code", SQLDatabase.MakeInParam("@Code", SqlDbType.VarChar, 20, m.Code));
            p.Add("Name", SQLDatabase.MakeInParam("@Name", SqlDbType.NVarChar, 50, m.Name));
            p.Add("Type", SQLDatabase.MakeInParam("@Type", SqlDbType.Int, 4, m.Type));
            p.Add("Enabled", SQLDatabase.MakeInParam("@Enabled", SqlDbType.Char, 1, m.Enabled));
            p.Add("Description", SQLDatabase.MakeInParam("@Description", SqlDbType.NVarChar, 50, m.Description));
            p.Add("InsertTime", SQLDatabase.MakeInParam("@InsertTime", SqlDbType.DateTime, 8, m.InsertTime));
            p.Add("InsertStaff", SQLDatabase.MakeInParam("@InsertStaff", SqlDbType.NVarChar, 256, m.InsertStaff));
            p.Add("UpdateTime", SQLDatabase.MakeInParam("@UpdateTime", SqlDbType.DateTime, 8, m.UpdateTime));
            p.Add("UpdateStaff", SQLDatabase.MakeInParam("@UpdateStaff", SqlDbType.NVarChar, 256, m.UpdateStaff));

            return p;
        }

        protected override Dictionary_Data FillModel(IDataReader dr)
        {
            Dictionary_Data m = new Dictionary_Data();

            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (Guid)dr["ID"];
            if (!string.IsNullOrEmpty(dr["Code"].ToString())) m.Code = (string)dr["Code"];
            if (!string.IsNullOrEmpty(dr["Name"].ToString())) m.Name = (string)dr["Name"];
            if (!string.IsNullOrEmpty(dr["Type"].ToString())) m.Type = (int)dr["Type"];
            if (!string.IsNullOrEmpty(dr["Enabled"].ToString())) m.Enabled = (string)dr["Enabled"];
            if (!string.IsNullOrEmpty(dr["Description"].ToString())) m.Description = (string)dr["Description"];
            if (!string.IsNullOrEmpty(dr["InsertTime"].ToString())) m.InsertTime = (DateTime)dr["InsertTime"];
            if (!string.IsNullOrEmpty(dr["InsertStaff"].ToString())) m.InsertStaff = (string)dr["InsertStaff"];
            if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString())) m.UpdateTime = (DateTime)dr["UpdateTime"];
            if (!string.IsNullOrEmpty(dr["UpdateStaff"].ToString())) m.UpdateStaff = (string)dr["UpdateStaff"];
            if (!string.IsNullOrEmpty(dr["TypeName"].ToString())) m.TypeName = (string)dr["TypeName"];
            if (!string.IsNullOrEmpty(dr["TableName"].ToString())) m.TableName = (string)dr["TableName"];
            return m;
        }

        /// <summary>
        /// 获取所有字典表类别明细
        /// </summary>
        /// <returns></returns>
        public IList<Dictionary_Type> Dictionary_Type_GetAll()
        {

            SqlDataReader dr = null;

            SQLDatabase.RunProc("MCS_SYS.dbo.sp_Dictionary_Type_GetAll", out dr);

            IList<Dictionary_Type> dictype = new List<Dictionary_Type>();
            while (dr.Read())
            {
                dictype.Add(FillModel_Dictionary_Type(dr));
            }
            dr.Close();

            return dictype;
        }

        public Dictionary_Type Dictionary_Type_GetModel(int id)
        {
            SqlDataReader dr = null;
            SqlParameter[] param =
            {
                SQLDatabase.MakeInParam("@ID", SqlDbType.Int,4,id)
            };
            SQLDatabase.RunProc("MCS_SYS.dbo.sp_Dictionary_Type_GetModel", param, out dr);

            Dictionary_Type model = null;
            if (dr.HasRows)
            {
                dr.Read();
                model = FillModel_Dictionary_Type(dr);
            }
            dr.Close();

            return model;
        }

        private Dictionary_Type FillModel_Dictionary_Type(IDataReader dr)
        {
            Dictionary_Type m = new Dictionary_Type();

            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["Name"].ToString())) m.Name = (string)dr["Name"];
            if (!string.IsNullOrEmpty(dr["TableName"].ToString())) m.TableName = (string)dr["TableName"];

            return m;
        }
    }
}
