
// ===================================================================
// 文件： User_UserInfoDAL.cs
// 项目名称：
// 创建时间：2013-08-31
// 作者:	   ShenGang
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
	///User_UserInfo数据访问DAL类
	/// </summary>
	public class User_UserInfoDAL : BaseSimpleDAL<User_UserInfo>
	{
		#region 构造函数
		///<summary>
		///
		///</summary>
		public User_UserInfoDAL()
		{
			_ProcePrefix = "MCS_SYS.dbo.sp_User_UserInfo";
		}
		#endregion
		
		
		/// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(User_UserInfo m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@UserId", SqlDbType.UniqueIdentifier, 16, m.UserId),
				SQLDatabase.MakeInParam("@AccountType", SqlDbType.Int, 4, m.AccountType),
				SQLDatabase.MakeInParam("@RelateStaff", SqlDbType.Int, 4, m.RelateStaff),
				SQLDatabase.MakeInParam("@RelateClient", SqlDbType.Int, 4, m.RelateClient),
				SQLDatabase.MakeInParam("@RelateClientLinkMan", SqlDbType.Int, 4, m.RelateClientLinkMan),
				SQLDatabase.MakeInParam("@RelatePromotor", SqlDbType.Int, 4, m.RelatePromotor),
				SQLDatabase.MakeInParam("@RelateCustomer", SqlDbType.Int, 4, m.RelateCustomer),
				SQLDatabase.MakeInParam("@RealName", SqlDbType.VarChar, 200, m.RealName),
				SQLDatabase.MakeInParam("@Mobile", SqlDbType.VarChar, 50, m.Mobile),
				SQLDatabase.MakeInParam("@Email", SqlDbType.VarChar, 200, m.Email),
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
        public override int Update(User_UserInfo m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@UserId", SqlDbType.UniqueIdentifier, 16, m.UserId),
				SQLDatabase.MakeInParam("@AccountType", SqlDbType.Int, 4, m.AccountType),
				SQLDatabase.MakeInParam("@RelateStaff", SqlDbType.Int, 4, m.RelateStaff),
				SQLDatabase.MakeInParam("@RelateClient", SqlDbType.Int, 4, m.RelateClient),
				SQLDatabase.MakeInParam("@RelateClientLinkMan", SqlDbType.Int, 4, m.RelateClientLinkMan),
				SQLDatabase.MakeInParam("@RelatePromotor", SqlDbType.Int, 4, m.RelatePromotor),
				SQLDatabase.MakeInParam("@RelateCustomer", SqlDbType.Int, 4, m.RelateCustomer),
				SQLDatabase.MakeInParam("@RealName", SqlDbType.VarChar, 200, m.RealName),
				SQLDatabase.MakeInParam("@Mobile", SqlDbType.VarChar, 50, m.Mobile),
				SQLDatabase.MakeInParam("@Email", SqlDbType.VarChar, 200, m.Email),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 4000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
			#endregion
			
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
			
			return ret;
        }
		
        protected override User_UserInfo FillModel(IDataReader dr)
		{
			User_UserInfo m = new User_UserInfo();
			if (!string.IsNullOrEmpty(dr["UserId"].ToString()))	m.UserId = (Guid)dr["UserId"];
			if (!string.IsNullOrEmpty(dr["AccountType"].ToString()))	m.AccountType = (int)dr["AccountType"];
			if (!string.IsNullOrEmpty(dr["RelateStaff"].ToString()))	m.RelateStaff = (int)dr["RelateStaff"];
			if (!string.IsNullOrEmpty(dr["RelateClient"].ToString()))	m.RelateClient = (int)dr["RelateClient"];
			if (!string.IsNullOrEmpty(dr["RelateClientLinkMan"].ToString()))	m.RelateClientLinkMan = (int)dr["RelateClientLinkMan"];
			if (!string.IsNullOrEmpty(dr["RelatePromotor"].ToString()))	m.RelatePromotor = (int)dr["RelatePromotor"];
			if (!string.IsNullOrEmpty(dr["RelateCustomer"].ToString()))	m.RelateCustomer = (int)dr["RelateCustomer"];
			if (!string.IsNullOrEmpty(dr["RealName"].ToString()))	m.RealName = (string)dr["RealName"];
            if (!string.IsNullOrEmpty(dr["Mobile"].ToString())) m.Mobile = (string)dr["Mobile"];
            if (!string.IsNullOrEmpty(dr["Email"].ToString())) m.Email = (string)dr["Email"];
			if (!string.IsNullOrEmpty(dr["CreateTime"].ToString()))	m.CreateTime = (DateTime)dr["CreateTime"];
			if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString()))	m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);
						
			return m;
		}
    }
}

