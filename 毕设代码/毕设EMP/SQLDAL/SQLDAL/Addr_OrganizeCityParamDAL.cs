
// ===================================================================
// 文件： Addr_OrganizeCityParamDAL.cs
// 项目名称：
// 创建时间：2012/11/27 星期二
// 作者:	   Jace
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model;
using MCSFramework.Common;
//using MCSFramework.Common;


namespace MCSFramework.SQLDAL
{
    /// <summary>
    ///Addr_OrganizeCityParam数据访问DAL类
    /// </summary>
    public class Addr_OrganizeCityParamDAL : BaseSimpleDAL<Addr_OrganizeCityParam>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public Addr_OrganizeCityParamDAL()
        {
            _ProcePrefix = "MCS_SYS.dbo.sp_Addr_OrganizeCityParam";
        }
        #endregion


        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(Addr_OrganizeCityParam m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ParamType", SqlDbType.Int, 4, m.ParamType),
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, m.OrganizeCity),
				SQLDatabase.MakeInParam("@ParamValue", SqlDbType.VarChar, 50, m.ParamValue),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 2000, m.Remark),
				SQLDatabase.MakeInParam("@InsertStaff", SqlDbType.Int, 4, m.InsertStaff),
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
        public override int Update(Addr_OrganizeCityParam m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@ParamType", SqlDbType.Int, 4, m.ParamType),
				SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, m.OrganizeCity),
				SQLDatabase.MakeInParam("@ParamValue", SqlDbType.VarChar, 50, m.ParamValue),
				SQLDatabase.MakeInParam("@Remark", SqlDbType.VarChar, 2000, m.Remark),
				SQLDatabase.MakeInParam("@UpdateStaff", SqlDbType.Int, 4, m.UpdateStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override Addr_OrganizeCityParam FillModel(IDataReader dr)
        {
            Addr_OrganizeCityParam m = new Addr_OrganizeCityParam();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["ParamType"].ToString())) m.ParamType = (int)dr["ParamType"];
            if (!string.IsNullOrEmpty(dr["OrganizeCity"].ToString())) m.OrganizeCity = (int)dr["OrganizeCity"];
            if (!string.IsNullOrEmpty(dr["ParamValue"].ToString())) m.ParamValue = (string)dr["ParamValue"];
            if (!string.IsNullOrEmpty(dr["Remark"].ToString())) m.Remark = (string)dr["Remark"];
            if (!string.IsNullOrEmpty(dr["InsertTime"].ToString())) m.InsertTime = (DateTime)dr["InsertTime"];
            if (!string.IsNullOrEmpty(dr["InsertStaff"].ToString())) m.InsertStaff = (int)dr["InsertStaff"];
            if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString())) m.UpdateTime = (DateTime)dr["UpdateTime"];
            if (!string.IsNullOrEmpty(dr["UpdateStaff"].ToString())) m.UpdateStaff = (int)dr["UpdateStaff"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);

            return m;
        }

        public DataTable GetByOrganizeCity(int OrganizeCity, int ParamType, int Include)
        {
            SqlDataReader sdr = null;
            #region	设置参数集
            SqlParameter[] prams = {
                SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, OrganizeCity),
				SQLDatabase.MakeInParam("@ParamType", SqlDbType.Int, 4, ParamType),
                SQLDatabase.MakeInParam("@Include", SqlDbType.Int, 4, Include)
                                   };
            #endregion
            SQLDatabase.RunProc(_ProcePrefix + "_GetByOrganizeCity",prams, out sdr);
            return Tools.ConvertDataReaderToDataTable(sdr);
        }
        public string GetValueByType(int OrganizeCity, int ParamType)
        {
            SqlParameter[] prams ={
                SQLDatabase.MakeInParam("@OrganizeCity", SqlDbType.Int, 4, OrganizeCity),
				SQLDatabase.MakeInParam("@ParamType", SqlDbType.Int, 4, ParamType),
                SQLDatabase.MakeOutParam("@ParamValue",SqlDbType.VarChar,50)
                                };
            SQLDatabase.RunProc(_ProcePrefix+"_GetValueByType",prams);

            return prams[2].Value.ToString();
        }
    }
}

