using System;
using System.Collections.Generic;
using System.Text;
using MCSFramework.SQLDAL;
using System.Data;
using MCSFramework.Model;
using MCSFramework.Common;

namespace MCSFramework.BLL
{
    public class UserBLL
    {
        private static string DALClassName = "MCSFramework.SQLDAL.UserDAL";


        /// <summary>
        /// 设置用户帐号对应的员工StaffID
        /// </summary>
        /// <param name="staffid"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public static int Membership_SetStaffID(string username, int staffid)
        {
            UserDAL dal = (UserDAL)DataAccess.CreateObject(DALClassName);
            return dal.Membership_SetStaffID(ConfigHelper.GetConfigString("ApplicationName"), username, staffid);
        }

        /// <summary>
        /// 设置用户帐号对应的客户ClientID
        /// </summary>
        /// <param name="username"></param>
        /// <param name="client"></param>
        /// <returns></returns>
        public static int Membership_SetClientID(string username, int client)
        {
            UserDAL dal = (UserDAL)DataAccess.CreateObject(DALClassName);
            return dal.Membership_SetClientID(ConfigHelper.GetConfigString("ApplicationName"), username, client);
        }
        
        /// <summary>
        /// 设置用户帐号对应的导购ID
        /// </summary>
        /// <param name="username"></param>
        /// <param name="promotor"></param>
        /// <returns></returns>
        public static int Membership_SetPromotorID(string username, int promotor)
        {
            UserDAL dal = (UserDAL)DataAccess.CreateObject(DALClassName);
            return dal.Membership_SetPromotorID(ConfigHelper.GetConfigString("ApplicationName"), username, promotor);
        }

        /// <summary>
        /// 用户登录成功
        /// </summary>
        /// <param name="username"></param>
        /// <param name="authkey"></param>
        /// <param name="ipaddr"></param>
        /// <returns>用户帐户的员工ID</returns>
        public static int LoginSuccess(string username, string authkey, string ipaddr)
        {
            UserDAL dal = (UserDAL)DataAccess.CreateObject(DALClassName);

            return dal.LoginSuccess(username, authkey, ipaddr, "");
        }
        public static int LoginSuccess(string username, string authkey, string ipaddr, string MACAddr)
        {
            UserDAL dal = (UserDAL)DataAccess.CreateObject(DALClassName);

            return dal.LoginSuccess(username, authkey, ipaddr, MACAddr);
        }

        /// <summary>
        /// 用户注销
        /// </summary>
        /// <param name="username"></param>
        /// <param name="authkey"></param>
        /// <returns></returns>
        public static int Logout(string username, string authkey)
        {
            UserDAL dal = (UserDAL)DataAccess.CreateObject(DALClassName);

            return dal.Logout(username, authkey);
        }

        /// <summary>
        /// 更新用户状态
        /// </summary>
        /// <param name="username"></param>
        /// <param name="authkey"></param>
        /// <param name="activemodule"></param>
        /// <param name="newmsgcount"></param>
        /// <returns></returns>
        public static int CheckUpdate(string username, string authkey, int activemodule, out int newmsgcount)
        {
            UserDAL dal = (UserDAL)DataAccess.CreateObject(DALClassName);

            return dal.CheckUpdate(username, authkey, activemodule, out newmsgcount);
        }
        /// <summary>
        /// 通过AuthKey验证用户，并更新用户状态
        /// </summary>
        /// <param name="authkey"></param>
        /// <param name="activemodule"></param>
        /// <param name="username"></param>
        /// <param name="newmsgcount"></param>
        /// <returns></returns>
        public static int CheckAuthKey(string authkey, int activemodule, out string username, out int newmsgcount)
        {
            UserDAL dal = (UserDAL)DataAccess.CreateObject(DALClassName);

            return dal.CheckAuthKey(authkey, activemodule, out username, out newmsgcount);
        }
        /// <summary>
        /// 查询在线用户名单
        /// </summary>
        /// <returns></returns>
        public static DataTable GetOnlineUserList()
        {
            UserDAL dal = (UserDAL)DataAccess.CreateObject(DALClassName);

            return dal.GetOnlineUserList();
        }

        /// <summary>
        /// 获取指定帐户的员工ID
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static Org_Staff GetStaffByUsername(string username)
        {
            UserDAL dal = (UserDAL)DataAccess.CreateObject(DALClassName);

            int staffid = dal.GetStaffIDByUsername(username);

            return new Org_StaffBLL(staffid).Model;
        }

        public static int GetClientIDByUsername(string username)
        {
            UserDAL dal = (UserDAL)DataAccess.CreateObject(DALClassName);

            return dal.GetClientIDByUsername(username);
        }

        public static int GetPromotorIDByUsername(string username)
        {
            UserDAL dal = (UserDAL)DataAccess.CreateObject(DALClassName);

            return dal.GetPromotorIDByUsername(username);
        }
        public static int GetNewMsgCount(string Username)
        {
            UserDAL dal = (UserDAL)DataAccess.CreateObject(DALClassName);

            return dal.GetNewMsgCount(Username);
        }
    }
}
