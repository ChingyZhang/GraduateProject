
// ===================================================================
// 文件： BBS_BoardUserMemberDAL.cs
// 项目名称：
// 创建时间：2009-3-16
// 作者:	   cl
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model.OA;
using MCSFramework.SQLDAL.OA;

namespace MCSFramework.BLL.OA
{
	/// <summary>
	///BBS_BoardUserMemberBLL业务逻辑BLL类
	/// </summary>
	public class BBS_BoardUserMemberBLL : BaseSimpleBLL<BBS_BoardUserMember>
	{
		private static string DALClassName = "MCSFramework.SQLDAL.OA.BBS_BoardUserMemberDAL";
        private BBS_BoardUserMemberDAL _dal;
		
		#region 构造函数
		///<summary>
		///BBS_BoardUserMemberBLL
		///</summary>
		public BBS_BoardUserMemberBLL()
			: base(DALClassName)
		{
			_dal = (BBS_BoardUserMemberDAL)_DAL;
            _m = new BBS_BoardUserMember(); 
		}
		
		public BBS_BoardUserMemberBLL(int id)
            : base(DALClassName)
        {
            _dal = (BBS_BoardUserMemberDAL)_DAL;
            FillModel(id);
        }

        public BBS_BoardUserMemberBLL(int id, bool bycache)
            : base(DALClassName)
        {
            _dal = (BBS_BoardUserMemberDAL)_DAL;
            FillModel(id, bycache);
        }
		#endregion
		
		#region	静态GetModelList方法
		public static IList<BBS_BoardUserMember> GetModelList(string condition)
        {
            return new BBS_BoardUserMemberBLL()._GetModelList(condition);
        }
		#endregion

        public DataTable GetAllBoardUserMember(string condition)
        {
            return _dal.GetAllBoardUserMember(condition);
        }

        public int UpdateRoleByUserNameAndBoard(string userName, int board, int role)
        {
            return _dal.UpdateRoleByUserNameAndBoard(userName, board, role);
        }
        public int DeleteByUserNameAndBoard(string userName, int board)
        {
            return _dal.DeleteByUserNameAndBoard(userName, board);
        }
        public int GetUserRoleByBoard(int board, string name)
        {
            return _dal.GetUserRoleByBoard(board, name);
        }
	}
}