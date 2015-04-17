using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model;

namespace MCSFramework.SQLDAL
{
    /// <summary>
    ///Right_ModuleWithApp数据访问DAL类
    /// </summary>
    public class Right_ModuleWithAppDAL : BaseSimpleDAL<Right_ModuleWithApp>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public Right_ModuleWithAppDAL()
        {
            _ProcePrefix = "MCS_SYS.dbo.sp_Right_ModuleWithApp";
        }
        #endregion


        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(Right_ModuleWithApp m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@Name", SqlDbType.VarChar, 50, m.Name),
				SQLDatabase.MakeInParam("@SuperID", SqlDbType.Int, 4, m.SuperID),
				SQLDatabase.MakeInParam("@SortValue", SqlDbType.Int, 4, m.SortValue),
				SQLDatabase.MakeInParam("@EnableFlag", SqlDbType.Char, 1, m.EnableFlag),
				SQLDatabase.MakeInParam("@DefaultIco", SqlDbType.Int, 4, m.DefaultIco),
				SQLDatabase.MakeInParam("@IsMenu", SqlDbType.Char, 1, m.IsMenu),
				SQLDatabase.MakeInParam("@IsHttp", SqlDbType.Char, 1, m.IsHttp),
				SQLDatabase.MakeInParam("@PageName", SqlDbType.VarChar, 200, m.PageName),
				SQLDatabase.MakeInParam("@IsAnonymous", SqlDbType.Char, 1, m.IsAnonymous),
				SQLDatabase.MakeInParam("@InsertStaff", SqlDbType.Int, 4, m.InsertStaff),
				SQLDatabase.MakeInParam("@InsertDate", SqlDbType.DateTime, 8, m.InsertDate),
				SQLDatabase.MakeInParam("@UpdateDate", SqlDbType.DateTime, 8, m.UpdateDate),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 200, m.Remark)
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
        public override int Update(Right_ModuleWithApp m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@Name", SqlDbType.VarChar, 50, m.Name),
				SQLDatabase.MakeInParam("@SuperID", SqlDbType.Int, 4, m.SuperID),
				SQLDatabase.MakeInParam("@SortValue", SqlDbType.Int, 4, m.SortValue),
				SQLDatabase.MakeInParam("@EnableFlag", SqlDbType.Char, 1, m.EnableFlag),
				SQLDatabase.MakeInParam("@DefaultIco", SqlDbType.Int, 4, m.DefaultIco),
				SQLDatabase.MakeInParam("@IsMenu", SqlDbType.Char, 1, m.IsMenu),
				SQLDatabase.MakeInParam("@IsHttp", SqlDbType.Char, 1, m.IsHttp),
				SQLDatabase.MakeInParam("@PageName", SqlDbType.VarChar, 200, m.PageName),
				SQLDatabase.MakeInParam("@IsAnonymous", SqlDbType.Char, 1, m.IsAnonymous),
				SQLDatabase.MakeInParam("@InsertDate", SqlDbType.DateTime, 8, m.InsertDate),
				SQLDatabase.MakeInParam("@UpdateStaff", SqlDbType.Int, 4, m.UpdateStaff),
				SQLDatabase.MakeInParam("@UpdateDate", SqlDbType.DateTime, 8, m.UpdateDate),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 200, m.Remark)
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override Right_ModuleWithApp FillModel(IDataReader dr)
        {
            Right_ModuleWithApp m = new Right_ModuleWithApp();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["Name"].ToString())) m.Name = (string)dr["Name"];
            if (!string.IsNullOrEmpty(dr["SuperID"].ToString())) m.SuperID = (int)dr["SuperID"];
            if (!string.IsNullOrEmpty(dr["SortValue"].ToString())) m.SortValue = (int)dr["SortValue"];
            if (!string.IsNullOrEmpty(dr["EnableFlag"].ToString())) m.EnableFlag = (string)dr["EnableFlag"];
            if (!string.IsNullOrEmpty(dr["DefaultIco"].ToString())) m.DefaultIco = (int)dr["DefaultIco"];
            if (!string.IsNullOrEmpty(dr["IsMenu"].ToString())) m.IsMenu = (string)dr["IsMenu"];
            if (!string.IsNullOrEmpty(dr["IsHttp"].ToString())) m.IsHttp = (string)dr["IsHttp"];
            if (!string.IsNullOrEmpty(dr["PageName"].ToString())) m.PageName = (string)dr["PageName"];
            if (!string.IsNullOrEmpty(dr["IsAnonymous"].ToString())) m.IsAnonymous = (string)dr["IsAnonymous"];
            if (!string.IsNullOrEmpty(dr["InsertStaff"].ToString())) m.InsertStaff = (int)dr["InsertStaff"];
            if (!string.IsNullOrEmpty(dr["InsertDate"].ToString())) m.InsertDate = (DateTime)dr["InsertDate"];
            if (!string.IsNullOrEmpty(dr["UpdateStaff"].ToString())) m.UpdateStaff = (int)dr["UpdateStaff"];
            if (!string.IsNullOrEmpty(dr["UpdateDate"].ToString())) m.UpdateDate = (DateTime)dr["UpdateDate"];
            if (!string.IsNullOrEmpty(dr["Remark"].ToString())) m.Remark = (string)dr["Remark"];

            return m;
        }
    }
}
