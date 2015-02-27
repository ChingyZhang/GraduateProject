
// ===================================================================
// 文件： Addr_OfficialCityDAL.cs
// 项目名称：
// 创建时间：2008-12-17
// 作者:	   
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.Model;
using MCSFramework.Common;


namespace MCSFramework.SQLDAL
{
    /// <summary>
    ///Addr_OfficialCity数据访问DAL类
    /// </summary>
    public class Addr_OfficialCityDAL : BaseSimpleDAL<Addr_OfficialCity>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public Addr_OfficialCityDAL()
        {
            _ProcePrefix = "MCS_SYS.dbo.sp_Addr_OfficialCity";
        }
        #endregion


        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(Addr_OfficialCity m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Name", SqlDbType.VarChar, 200, m.Name),
				SQLDatabase.MakeInParam("@SuperID", SqlDbType.Int, 4, m.SuperID),
				SQLDatabase.MakeInParam("@Level", SqlDbType.Int, 4, m.Level),
				SQLDatabase.MakeInParam("@Code", SqlDbType.VarChar, 20, m.Code),
				SQLDatabase.MakeInParam("@CallAreaCode", SqlDbType.VarChar, 50, m.CallAreaCode),
				SQLDatabase.MakeInParam("@PostCode", SqlDbType.VarChar, 50, m.PostCode),
                SQLDatabase.MakeInParam("@Births", SqlDbType.Int, 4, m.Births),
                SQLDatabase.MakeInParam("@Attribute", SqlDbType.Int, 4, m.Attribute)
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
        public override int Update(Addr_OfficialCity m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@Name", SqlDbType.VarChar, 200, m.Name),
				SQLDatabase.MakeInParam("@SuperID", SqlDbType.Int, 4, m.SuperID),
				SQLDatabase.MakeInParam("@Level", SqlDbType.Int, 4, m.Level),
				SQLDatabase.MakeInParam("@Code", SqlDbType.VarChar, 20, m.Code),
				SQLDatabase.MakeInParam("@CallAreaCode", SqlDbType.VarChar, 50, m.CallAreaCode),
				SQLDatabase.MakeInParam("@PostCode", SqlDbType.VarChar, 50, m.PostCode),
                SQLDatabase.MakeInParam("@Births", SqlDbType.Int , 4, m.Births),
                SQLDatabase.MakeInParam("@Attribute", SqlDbType.Int, 4, m.Attribute)
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override Addr_OfficialCity FillModel(IDataReader dr)
        {
            Addr_OfficialCity m = new Addr_OfficialCity();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["Name"].ToString())) m.Name = (string)dr["Name"];
            if (!string.IsNullOrEmpty(dr["SuperID"].ToString())) m.SuperID = (int)dr["SuperID"];
            if (!string.IsNullOrEmpty(dr["Level"].ToString())) m.Level = (int)dr["Level"];
            if (!string.IsNullOrEmpty(dr["Code"].ToString())) m.Code = (string)dr["Code"];
            if (!string.IsNullOrEmpty(dr["CallAreaCode"].ToString())) m.CallAreaCode = (string)dr["CallAreaCode"];
            if (!string.IsNullOrEmpty(dr["PostCode"].ToString())) m.PostCode = (string)dr["PostCode"];
            if (!string.IsNullOrEmpty(dr["Births"].ToString())) m.Births = (int)dr["Births"];
            if (!string.IsNullOrEmpty(dr["Attribute"].ToString())) m.Attribute = (int)dr["Attribute"];
            return m;
        }

        public DataTable GetAllOfficialCity()
        {
            SqlDataReader dr = null;
            SqlParameter[] prams = {
            SQLDatabase.MakeInParam("@Condition", SqlDbType.VarChar, 2000,""),
                                   };
            SQLDatabase.RunProc(_ProcePrefix + "_GetByCondition", prams, out dr);

            return Tools.ConvertDataReaderToDataTable(dr);
        }

        /// <summary>
        /// 根据电话号码、手机号码、邮编获取所属行政城市
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public Addr_OfficialCity GetCityByTeleNumOrPostCode(string number)
        {
            SqlDataReader dr = null;
            SqlParameter[] prams = { SQLDatabase.MakeInParam("@Number", SqlDbType.VarChar, 50, number) };

            SQLDatabase.RunProc(_ProcePrefix + "_GetModelByTeleNumOrPostCode", prams, out dr);

            if (dr.Read())
                return FillModel(dr);
            else
                return null;
        }
    }
}

