
// ===================================================================
// 文件： Const_IPLocationDAL.cs
// 项目名称：
// 创建时间：2009/6/21
// 作者:	   Shen Gang
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
    ///Const_IPLocation数据访问DAL类
    /// </summary>
    public class Const_IPLocationDAL : BaseSimpleDAL<Const_IPLocation>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public Const_IPLocationDAL()
        {
            _ProcePrefix = "MCS_Pub.dbo.sp_Const_IPLocation";
        }
        #endregion


        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(Const_IPLocation m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@IP", SqlDbType.VarChar, 50, m.IP),
				SQLDatabase.MakeInParam("@Location", SqlDbType.VarChar, 200, m.Location),
				SQLDatabase.MakeInParam("@OfficialCity", SqlDbType.Int, 4, m.OfficialCity),
				SQLDatabase.MakeInParam("@ISP", SqlDbType.Int, 4, m.ISP)
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
        public override int Update(Const_IPLocation m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@IP", SqlDbType.VarChar, 50, m.IP),
				SQLDatabase.MakeInParam("@Location", SqlDbType.VarChar, 200, m.Location),
				SQLDatabase.MakeInParam("@OfficialCity", SqlDbType.Int, 4, m.OfficialCity),
				SQLDatabase.MakeInParam("@ISP", SqlDbType.Int, 4, m.ISP)
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override Const_IPLocation FillModel(IDataReader dr)
        {
            Const_IPLocation m = new Const_IPLocation();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["IP"].ToString())) m.IP = (string)dr["IP"];
            if (!string.IsNullOrEmpty(dr["Location"].ToString())) m.Location = (string)dr["Location"];
            if (!string.IsNullOrEmpty(dr["OfficialCity"].ToString())) m.OfficialCity = (int)dr["OfficialCity"];
            if (!string.IsNullOrEmpty(dr["ISP"].ToString())) m.ISP = (int)dr["ISP"];

            return m;
        }

        /// <summary>
        /// 根据IP寻找所属地
        /// </summary>
        /// <param name="ipaddress"></param>
        /// <returns></returns>
        public Const_IPLocation FindByIP(string ipaddress)
        {
            Const_IPLocation m = null;
            SqlDataReader dr = null;

            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@IP", SqlDbType.VarChar, 50, ipaddress),
			};
            #endregion

            SQLDatabase.RunProc(_ProcePrefix + "_FindByIP", prams, out dr);

            if (dr.HasRows)
            {
                if (dr.Read()) m = FillModel(dr);
            }
            dr.Close();

            return m;
        }
    }
}

