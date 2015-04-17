
// ===================================================================
// 文件： User_UserInfoDAL.cs
// 项目名称：
// 创建时间：2013-08-31
// 作者:	   ShenGang
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using MCSFramework.Common;
using MCSFramework.BLL;
using MCSFramework.Model;
using MCSFramework.SQLDAL;
using System.Web.Security;

namespace MCSFramework.BLL
{
    /// <summary>
    ///User_UserInfoBLL业务逻辑BLL类
    /// </summary>
    public class User_UserInfoBLL : BaseSimpleBLL<User_UserInfo>
    {
        private static string DALClassName = "MCSFramework.SQLDAL.User_UserInfoDAL";
        private User_UserInfoDAL _dal;

        #region 构造函数
        ///<summary>
        ///User_UserInfoBLL
        ///</summary>
        public User_UserInfoBLL()
            : base(DALClassName)
        {
            _dal = (User_UserInfoDAL)_DAL;
            _m = new User_UserInfo();
        }

        public User_UserInfoBLL(Guid userid)
            : base(DALClassName)
        {
            _dal = (User_UserInfoDAL)_DAL;
            FillModel(userid);
        }

        public User_UserInfoBLL(Guid userid, bool bycache)
            : base(DALClassName)
        {
            _dal = (User_UserInfoDAL)_DAL;
            FillModel(userid, bycache);
        }
        #endregion

        #region	静态GetModelList方法
        public static IList<User_UserInfo> GetModelList(string condition)
        {
            return new User_UserInfoBLL()._GetModelList(condition);
        }
        #endregion

        /*
        public static int CreateUserWithStaff(string Username, string RealName, string Password, string Email, int StaffID)
        {
            if (Membership.GetUser(Username) != null)
            {
                //用户名重复
                return -10;
            }

            MembershipUser user;
            try
            {
                user = Membership.CreateUser(Username, Password, Email);

                if (!Roles.RoleExists("全体员工")) Roles.CreateRole("全体员工");
                Roles.AddUserToRole(user.UserName, "全体员工");
            }
            catch (System.Exception err)
            {
                //创建用户失败
                LogWriter.WriteLog("User_UserInfoBLL.CreateUserWithStaff", err);
                return -11;
            }
            if (user == null) return -11;

            try
            {
                User_UserInfoBLL bll = new User_UserInfoBLL();
                bll.Model.UserId = (Guid)user.ProviderUserKey;
                bll.Model.AccountType = 1;                      //账户类型 1:员工,2:商业客户,3:导购,4:顾客
                bll.Model.RealName = RealName;
                bll.Model.RelateStaff = StaffID;
                bll.Model.Email = Email;
                if (bll.Add() < 0) return -12;
            }
            catch (System.Exception err)
            {
                //创建用户失败
                Membership.DeleteUser(user.UserName);
                LogWriter.WriteLog("User_UserInfoBLL.CreateUserWithStaff", err);
                return -12;
            }

            return 0;
        }

        public static int CreateUserWithPromotor(string Username, string RealName, string Password, string Email, int Promotor)
        {
            if (Membership.GetUser(Username) != null)
            {
                //用户名重复
                return -10;
            }

            MembershipUser user;
            try
            {
                user = Membership.CreateUser(Username, Password, Email);

                if (!Roles.RoleExists("营养顾问")) Roles.CreateRole("营养顾问");
                Roles.AddUserToRole(user.UserName, "营养顾问");
            }
            catch (System.Exception err)
            {
                //创建用户失败
                LogWriter.WriteLog("User_UserInfoBLL.CreateUserWithPromotor", err);
                return -11;
            }
            if (user == null) return -11;

            try
            {
                User_UserInfoBLL bll = new User_UserInfoBLL();
                bll.Model.UserId = (Guid)user.ProviderUserKey;
                bll.Model.AccountType = 3;                      //账户类型 1:员工,2:商业客户,3:导购,4:顾客
                bll.Model.RealName = RealName;
                bll.Model.RelatePromotor = Promotor;
                bll.Model.Email = Email;
                if (bll.Add() < 0) return -12;
            }
            catch (System.Exception err)
            {
                //创建用户失败
                Membership.DeleteUser(user.UserName);
                LogWriter.WriteLog("User_UserInfoBLL.CreateUserWithPromotor", err);
                return -12;
            }

            return 0;
        }
        */
    }
}