
// ===================================================================
// 文件： BBS_BoardUserMemberDAL.cs
// 项目名称：
// 创建时间：2009-3-16
// 作者:	   cl
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.SQLDAL;
using MCSFramework.Model.OA;
using MCSFramework.Common;


namespace MCSFramework.SQLDAL.OA
{
	/// <summary>
	///BBS_BoardUserMember数据访问DAL类
	/// </summary>
	public class BBS_BoardUserMemberDAL : BaseSimpleDAL<BBS_BoardUserMember>
	{
		#region 构造函数
		///<summary>
		///
		///</summary>
		public BBS_BoardUserMemberDAL()
		{
			_ProcePrefix = "MCS_OA.dbo.sp_BBS_BoardUserMember";
		}
		#endregion
		
		
		/// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public override int Add(BBS_BoardUserMember m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Board", SqlDbType.Int, 4, m.Board),
				SQLDatabase.MakeInParam("@UserName", SqlDbType.NVarChar, 256, m.UserName),
				SQLDatabase.MakeInParam("@Role", SqlDbType.Int, 4, m.Role)
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
        public override int Update(BBS_BoardUserMember m)
        {
			#region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@ID", SqlDbType.Int, 4, m.ID),
				SQLDatabase.MakeInParam("@Board", SqlDbType.Int, 4, m.Board),
				SQLDatabase.MakeInParam("@UserName", SqlDbType.NVarChar, 256, m.UserName),
				SQLDatabase.MakeInParam("@Role", SqlDbType.Int, 4, m.Role)
			};
			#endregion
			
            int ret = SQLDatabase.RunProc(_ProcePrefix + "_Update", prams);
			
			return ret;
        }
		
        protected override BBS_BoardUserMember FillModel(IDataReader dr)
		{
			BBS_BoardUserMember m = new BBS_BoardUserMember();
			if (!string.IsNullOrEmpty(dr["ID"].ToString()))	m.ID = (int)dr["ID"];
			if (!string.IsNullOrEmpty(dr["Board"].ToString()))	m.Board = (int)dr["Board"];
			if (!string.IsNullOrEmpty(dr["UserName"].ToString()))	m.UserName = (string)dr["UserName"];
			if (!string.IsNullOrEmpty(dr["Role"].ToString()))	m.Role = (int)dr["Role"];
						
			return m;
		}

        public DataTable GetAllBoardUserMember(string condition)
        {
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@Condition", SqlDbType.NVarChar, 2000,condition)
             };
            SqlDataReader dr = null;

            SQLDatabase.RunProc(_ProcePrefix + "_GetByCondition",prams , out dr);

            return Tools.ConvertDataReaderToDataTable(dr);
        }


        public int UpdateRoleByUserNameAndBoard(string userName, int board, int role)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@UserName", SqlDbType.NVarChar, 256, userName),
				SQLDatabase.MakeInParam("@Board", SqlDbType.Int, 4, board),
				SQLDatabase.MakeInParam("@Role", SqlDbType.Int, 4, role)
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_UpdateRoleByUserNameAndBoard", prams);

            return ret;
        }

        public int DeleteByUserNameAndBoard(string userName, int board)
        {
            #region	设置参数集
            SqlParameter[] prams = {
				SQLDatabase.MakeInParam("@UserName", SqlDbType.NVarChar, 256, userName),
				SQLDatabase.MakeInParam("@Board", SqlDbType.Int, 4, board)
			};
            #endregion

            int ret = SQLDatabase.RunProc(_ProcePrefix + "_DeleteByUserNameAndBoard", prams);

            return ret;
        }
        public int GetUserRoleByBoard(int board,string name)
        {
            string condition = " Board="+board.ToString()+" and UserName= '"+name+"'";
            DataTable dt = GetAllBoardUserMember(condition);
            if (dt.Rows.Count > 0)
                return (int)dt.Rows[0]["Role"];
            else
                return 0;
        }
        
    }
}

