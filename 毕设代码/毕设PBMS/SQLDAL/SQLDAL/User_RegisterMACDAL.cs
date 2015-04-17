
// ===================================================================
// 文件： User_RegisterMACDAL.cs
// 项目名称：
// 创建时间：2011/11/18
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
    ///User_RegisterMAC数据访问DAL类
    /// </summary>
    public class User_RegisterMACDAL : BaseSimpleDAL<User_RegisterMAC>
    {
        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public User_RegisterMACDAL()
        {
            _ProcePrefix = "MCS_SYS.dbo.sp_User_RegisterMAC";
        }
        #endregion


        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(User_RegisterMAC m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@MacAddr", SqlDbType.VarChar, 200, m.MacAddr),
				SQLDatabase.MakeInParam("@UserName", SqlDbType.VarChar, 50, m.UserName),
				SQLDatabase.MakeInParam("@Enabled", SqlDbType.Char, 1, m.Enabled),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
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
        public override int Update(User_RegisterMAC m)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@MacAddr", SqlDbType.VarChar, 200, m.MacAddr),
				SQLDatabase.MakeInParam("@UserName", SqlDbType.VarChar, 50, m.UserName),
				SQLDatabase.MakeInParam("@Enabled", SqlDbType.Char, 1, m.Enabled),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);

            return ret;
        }

        protected override User_RegisterMAC FillModel(IDataReader dr)
        {
            User_RegisterMAC m = new User_RegisterMAC();
            if (!string.IsNullOrEmpty(dr["ID"].ToString())) m.ID = (int)dr["ID"];
            if (!string.IsNullOrEmpty(dr["MacAddr"].ToString())) m.MacAddr = (string)dr["MacAddr"];
            if (!string.IsNullOrEmpty(dr["UserName"].ToString())) m.UserName = (string)dr["UserName"];
            if (!string.IsNullOrEmpty(dr["Enabled"].ToString())) m.Enabled = (string)dr["Enabled"];
            if (!string.IsNullOrEmpty(dr["ApproveFlag"].ToString())) m.ApproveFlag = (int)dr["ApproveFlag"];
            if (!string.IsNullOrEmpty(dr["InsertTime"].ToString())) m.InsertTime = (DateTime)dr["InsertTime"];
            if (!string.IsNullOrEmpty(dr["InsertStaff"].ToString())) m.InsertStaff = (int)dr["InsertStaff"];
            if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString())) m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);

            return m;
        }

        /// <summary>
        /// 根据MAC地址判断，是否可以可以登录
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="MACAddr"></param>
        /// <param name="Flag"></param>
        /// <returns></returns>
        public int CanLogin(string UserName, ref string MACAddr, int Flag)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@UserName", SqlDbType.VarChar, 50, UserName),
                new SqlParameter("@MACAddr", SqlDbType.VarChar,200, ParameterDirection.InputOutput,false,0,0,"MACAddr", DataRowVersion.Original,MACAddr),
				SQLDatabase.MakeInParam("@Flag", SqlDbType.Int, 4, Flag)
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_CanLogin", prams);
            MACAddr = prams[1].Value.ToString();
            return ret;
        }

        /// <summary>
        /// 移除指定串码已授权的指定会员用户账户
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="MACAddr"></param>
        /// <returns></returns>
        public int Remove(string UserName, string MACAddr)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@UserName", SqlDbType.VarChar, 50, UserName),
                SQLDatabase.MakeInParam("@MACAddr", SqlDbType.VarChar, 200, MACAddr)
                
			};
            #endregion

            return SQLDatabase.RunProc(_ProcePrefix + "_Remove", prams);
        }
    }
}

