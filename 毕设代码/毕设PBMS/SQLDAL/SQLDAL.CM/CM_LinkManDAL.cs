
// ===================================================================
// 文件： CM_LinkManDAL.cs
// 项目名称：
// 创建时间：2009/2/19
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.CM;


namespace MCSFramework.SQLDAL.CM
{
	/// <summary>
	///CM_LinkMan数据访问DAL类
	/// </summary>
	public class CM_LinkManDAL : BaseSimpleDAL<CM_LinkMan>
	{
		#region 构造函数
		///<summary>
		///
		///</summary>
		public CM_LinkManDAL()
		{
			_ProcePrefix = "MCS_CM.dbo.sp_CM_LinkMan";
		}
		#endregion
		
		
		/// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(CM_LinkMan m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ClientID", SqlDbType.Int, 4, m.ClientID),
				SQLDatabase.MakeInParam("@Name", SqlDbType.VarChar, 50, m.Name),
				SQLDatabase.MakeInParam("@Sex", SqlDbType.Int, 4, m.Sex),
				SQLDatabase.MakeInParam("@Mobile", SqlDbType.VarChar, 50, m.Mobile),
				SQLDatabase.MakeInParam("@OfficeTeleNum", SqlDbType.VarChar, 50, m.OfficeTeleNum),
				SQLDatabase.MakeInParam("@HomeTeleNum", SqlDbType.VarChar, 50, m.HomeTeleNum),
				SQLDatabase.MakeInParam("@Email", SqlDbType.VarChar, 50, m.Email),
				SQLDatabase.MakeInParam("@Address", SqlDbType.VarChar, 200, m.Address),
				SQLDatabase.MakeInParam("@Birthday", SqlDbType.DateTime, 8, m.Birthday),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@InsertStaff", SqlDbType.Int, 4, m.InsertStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 8000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
			#endregion
			
            m.ID =  SQLDatabase.RunProc(_ProcePrefix + "_Add", prams);
			
            return m.ID;
        }
		
		/// <summary>
        /// 更新项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Update(CM_LinkMan m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@ClientID", SqlDbType.Int, 4, m.ClientID),
				SQLDatabase.MakeInParam("@Name", SqlDbType.VarChar, 50, m.Name),
				SQLDatabase.MakeInParam("@Sex", SqlDbType.Int, 4, m.Sex),
				SQLDatabase.MakeInParam("@Mobile", SqlDbType.VarChar, 50, m.Mobile),
				SQLDatabase.MakeInParam("@OfficeTeleNum", SqlDbType.VarChar, 50, m.OfficeTeleNum),
				SQLDatabase.MakeInParam("@HomeTeleNum", SqlDbType.VarChar, 50, m.HomeTeleNum),
				SQLDatabase.MakeInParam("@Email", SqlDbType.VarChar, 50, m.Email),
				SQLDatabase.MakeInParam("@Address", SqlDbType.VarChar, 200, m.Address),
				SQLDatabase.MakeInParam("@Birthday", SqlDbType.DateTime, 8, m.Birthday),
				SQLDatabase.MakeInParam("@ApproveFlag", SqlDbType.Int, 4, m.ApproveFlag),
				SQLDatabase.MakeInParam("@UpdateStaff", SqlDbType.Int, 4, m.UpdateStaff),
				SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar, 8000, CombineExtProperty(m.ExtPropertys,m.ModelName))
			};
			#endregion
			
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
			
			return ret;
        }
		
        protected override CM_LinkMan FillModel(IDataReader dr)
		{
			CM_LinkMan m = new CM_LinkMan();
			if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (int)dr["ID"];
			if (!string.IsNullOrEmpty(dr["ClientID"].ToString()))	m.ClientID = (int)dr["ClientID"];
			if (!string.IsNullOrEmpty(dr["Name"].ToString()))	m.Name = (string)dr["Name"];
			if (!string.IsNullOrEmpty(dr["Sex"].ToString()))	m.Sex = (int)dr["Sex"];
			if (!string.IsNullOrEmpty(dr["Mobile"].ToString()))	m.Mobile = (string)dr["Mobile"];
			if (!string.IsNullOrEmpty(dr["OfficeTeleNum"].ToString()))	m.OfficeTeleNum = (string)dr["OfficeTeleNum"];
			if (!string.IsNullOrEmpty(dr["HomeTeleNum"].ToString()))	m.HomeTeleNum = (string)dr["HomeTeleNum"];
			if (!string.IsNullOrEmpty(dr["Email"].ToString()))	m.Email = (string)dr["Email"];
			if (!string.IsNullOrEmpty(dr["Address"].ToString()))	m.Address = (string)dr["Address"];
			if (!string.IsNullOrEmpty(dr["Birthday"].ToString()))	m.Birthday = (DateTime)dr["Birthday"];
			if (!string.IsNullOrEmpty(dr["ApproveFlag"].ToString()))	m.ApproveFlag = (int)dr["ApproveFlag"];
			if (!string.IsNullOrEmpty(dr["InsertTime"].ToString()))	m.InsertTime = (DateTime)dr["InsertTime"];
			if (!string.IsNullOrEmpty(dr["InsertStaff"].ToString()))	m.InsertStaff = (int)dr["InsertStaff"];
			if (!string.IsNullOrEmpty(dr["UpdateTime"].ToString()))	m.UpdateTime = (DateTime)dr["UpdateTime"];
			if (!string.IsNullOrEmpty(dr["UpdateStaff"].ToString()))	m.UpdateStaff = (int)dr["UpdateStaff"];
			if (!string.IsNullOrEmpty(dr["ExtPropertys"].ToString()))	m.ExtPropertys = SpiltExtProperty(m.ModelName, (string)dr["ExtPropertys"]);
						
			return m;
		}
    }
}

