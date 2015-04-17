using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MCSFramework.DBUtility;
using MCSFramework.Common;

namespace MCSFramework.SQLDAL
{
    public class UserDAL
    {
        /// <summary>
        /// 设置用户帐号对应的员工StaffID
        /// </summary>
        /// <param name="application"></param>
        /// <param name="user"></param>
        /// <param name="staff"></param>
        /// <returns></returns>
        public int Membership_SetStaffID(string application, string user, int staff)
        {
            SqlParameter[] parms =
            {
                SQLDatabase.MakeInParam("@ApplicationName", SqlDbType.NVarChar,256,application),
                SQLDatabase.MakeInParam("@UserName", SqlDbType.NVarChar,256,user),
                SQLDatabase.MakeInParam("@StaffID", SqlDbType.Int,4,staff)
            };

            return SQLDatabase.RunProc("MCS_SYS.dbo.sp_Membership_SetStaffID", parms);
        }


        /// <summary>
        /// 设置用户帐号对应的客户ClientID
        /// </summary>
        /// <param name="application"></param>
        /// <param name="user"></param>
        /// <param name="staff"></param>
        /// <returns></returns>
        public int Membership_SetClientID(string application, string user, int client)
        {
            SqlParameter[] parms =
            {
                SQLDatabase.MakeInParam("@ApplicationName", SqlDbType.NVarChar,256,application),
                SQLDatabase.MakeInParam("@UserName", SqlDbType.NVarChar,256,user),
                SQLDatabase.MakeInParam("@ClientID", SqlDbType.Int,4,client)
            };

            return SQLDatabase.RunProc("MCS_SYS.dbo.sp_Membership_SetClientID", parms);
        }

        /// <summary>
        /// 设置用户帐号对应的导购ID
        /// </summary>
        /// <param name="application"></param>
        /// <param name="user"></param>
        /// <param name="client"></param>
        /// <returns></returns>
        public int Membership_SetPromotorID(string application, string user, int promotor)
        {
            SqlParameter[] parms =
            {
                SQLDatabase.MakeInParam("@ApplicationName", SqlDbType.NVarChar,256,application),
                SQLDatabase.MakeInParam("@UserName", SqlDbType.NVarChar,256,user),
                SQLDatabase.MakeInParam("@PromotorID", SqlDbType.Int,4,promotor)
            };

            return SQLDatabase.RunProc("MCS_SYS.dbo.sp_Membership_SetPromotorID", parms);
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="username"></param>
        /// <param name="authkey"></param>
        /// <param name="ipaddr"></param>
        /// <returns>用户帐户的员工ID</returns>
        public int LoginSuccess(string username, string authkey, string ipaddr, string MACAddr)
        {
            SqlParameter[] parms =
            {
                SQLDatabase.MakeInParam("@Username", SqlDbType.VarChar,50,username),
                SQLDatabase.MakeInParam("@AuthKey", SqlDbType.VarChar,50,authkey),
                SQLDatabase.MakeInParam("@IpAddr", SqlDbType.VarChar,50,ipaddr),
                SQLDatabase.MakeInParam("@MACAddr", SqlDbType.VarChar,50,MACAddr)
            };
            return SQLDatabase.RunProc("MCS_SYS.dbo.sp_User_LoginSuccess", parms);
        }

        /// <summary>
        /// 用户登录(含用户加密信息)
        /// </summary>
        /// <param name="username"></param>
        /// <param name="authkey"></param>
        /// <param name="ipaddr"></param>
        /// <param name="MACAddr"></param>
        /// <param name="UserInfo"></param>
        /// <param name="CryptKey"></param>
        /// <param name="ExtPropertys"></param>
        /// <returns></returns>
        public int LoginSuccess(string username, string authkey, string ipaddr, string MACAddr, string UserInfo, string CryptKey, string ExtPropertys)
        {
            SqlParameter[] parms =
            {
                SQLDatabase.MakeInParam("@Username", SqlDbType.VarChar,50,username),
                SQLDatabase.MakeInParam("@AuthKey", SqlDbType.VarChar,50,authkey),
                SQLDatabase.MakeInParam("@IpAddr", SqlDbType.VarChar,50,ipaddr),
                SQLDatabase.MakeInParam("@MACAddr", SqlDbType.VarChar,50,MACAddr),
                SQLDatabase.MakeInParam("@UserInfo", SqlDbType.VarChar,4000,UserInfo),
                SQLDatabase.MakeInParam("@CryptKey", SqlDbType.VarChar,4000,CryptKey),
                SQLDatabase.MakeInParam("@ExtPropertys", SqlDbType.VarChar,4000,ExtPropertys)
            };
            return SQLDatabase.RunProc("MCS_SYS.dbo.sp_User_LoginSuccess", parms);
        }

        public int Logout(string username, string authkey)
        {
            SqlParameter[] parms =
            {
                SQLDatabase.MakeInParam("@Username", SqlDbType.VarChar,50,username),
                SQLDatabase.MakeInParam("@AuthKey", SqlDbType.VarChar,50,authkey)
            };
            return SQLDatabase.RunProc("MCS_SYS.dbo.sp_User_Logout", parms);
        }

        public int CheckUpdate(string username, string authkey, int activemodule, out int newmsgcount)
        {
            SqlParameter[] parms =
            {
                SQLDatabase.MakeInParam("@Username", SqlDbType.VarChar,255,username),
                SQLDatabase.MakeInParam("@AuthKey", SqlDbType.VarChar,100,authkey),
                SQLDatabase.MakeInParam("@ActiveModule", SqlDbType.Int,4,activemodule),
                SQLDatabase.MakeOutParam("@NewMsgCount", SqlDbType.Int,4)
            };

            int ret = SQLDatabase.RunProc("MCS_SYS.dbo.sp_User_CheckUpdate", parms);
            newmsgcount = (int)parms[3].Value;

            return ret;
        }

        public int CheckAuthKey(string authkey, int activemodule, out string username, out int newmsgcount)
        {
            SqlParameter[] parms =
            {
                SQLDatabase.MakeInParam("@AuthKey", SqlDbType.VarChar,100,authkey),
                SQLDatabase.MakeInParam("@ActiveModule", SqlDbType.Int,4,activemodule),
                SQLDatabase.MakeOutParam("@Username", SqlDbType.VarChar,255),
                SQLDatabase.MakeOutParam("@NewMsgCount", SqlDbType.Int,4)
            };

            int ret = SQLDatabase.RunProc("MCS_SYS.dbo.sp_User_CheckAuthKey", parms);

            username = (string)parms[2].Value;
            newmsgcount = (int)parms[3].Value;

            return ret;
        }

        public int CheckAuthKey(string authkey, int activemodule, out string username, out int newmsgcount, out string UserInfo, out string CryptKey, out string ExtPropertys)
        {
            SqlParameter[] parms =
            {
                SQLDatabase.MakeInParam("@AuthKey", SqlDbType.VarChar,100,authkey),
                SQLDatabase.MakeInParam("@ActiveModule", SqlDbType.Int,4,activemodule),
                SQLDatabase.MakeOutParam("@Username", SqlDbType.VarChar,255),
                SQLDatabase.MakeOutParam("@NewMsgCount", SqlDbType.Int,4),
                SQLDatabase.MakeOutParam("@UserInfo", SqlDbType.VarChar,4000),
                SQLDatabase.MakeOutParam("@CryptKey", SqlDbType.VarChar,4000),
                SQLDatabase.MakeOutParam("@ExtPropertys", SqlDbType.VarChar,4000)
            };

            int ret = SQLDatabase.RunProc("MCS_SYS.dbo.sp_User_CheckAuthKey", parms);

            username = (string)parms[2].Value;
            newmsgcount = (int)parms[3].Value;
            UserInfo = (string)parms[4].Value;
            CryptKey = (string)parms[5].Value;
            ExtPropertys = (string)parms[6].Value;

            return ret;
        }

        public DataTable GetOnlineUserList()
        {
            SqlDataReader dr = null;
            SQLDatabase.RunProc("MCS_SYS.dbo.sp_User_GetOnlineUserList", out dr);

            return Tools.ConvertDataReaderToDataTable(dr);
        }

        public int GetStaffIDByUsername(string username)
        {
            SqlParameter[] parms =
            {
                SQLDatabase.MakeInParam("@Username", SqlDbType.VarChar,50,username)
            };

            return SQLDatabase.RunProc("MCS_SYS.dbo.sp_User_GetStaffByUsername", parms);
        }

        public int GetClientIDByUsername(string username)
        {
            SqlParameter[] parms =
            {
                SQLDatabase.MakeInParam("@Username", SqlDbType.VarChar,50,username)
            };

            return SQLDatabase.RunProc("MCS_SYS.dbo.sp_User_GetClientByUsername", parms);
        }

        public int GetPromotorIDByUsername(string username)
        {
            SqlParameter[] parms =
            {
                SQLDatabase.MakeInParam("@Username", SqlDbType.VarChar,50,username)
            };

            return SQLDatabase.RunProc("MCS_SYS.dbo.sp_User_GetPromotorByUsername", parms);
        }

        public int GetNewMsgCount(string Username)
        {
            SqlParameter[] parms =
            {
                SQLDatabase.MakeInParam("@Username", SqlDbType.VarChar,50,Username)
            };

            return SQLDatabase.RunProc("MCS_SYS.dbo.sp_User_GetNewMsgCount", parms);
        }

        #region APP通讯密钥管理
        /// <summary>
        /// 加载指定设备号的密钥
        /// </summary>
        /// <param name="MacAddr"></param>
        /// <param name="CryptKey"></param>
        /// <returns></returns>
        public int AppCryptKey_LoadKey(string MacAddr, out string CryptKey)
        {
            CryptKey = "";
            SqlParameter[] parms =
            {
                SQLDatabase.MakeInParam("@MacAddr", SqlDbType.VarChar, 200, MacAddr),
                SQLDatabase.MakeOutParam("@CryptKey", SqlDbType.VarChar, 4000)
            };

            int ret = SQLDatabase.RunProc("MCS_SYS.dbo.sp_User_AppCryptKey_LoadKey", parms);

            if (parms[1].Value != DBNull.Value)
            {
                CryptKey = (string)parms[1].Value;                
            }
            return ret;
        }

        /// <summary>
        /// 保存指定设备号的密钥信息
        /// </summary>
        /// <param name="MacAddr"></param>
        /// <param name="CryptKey"></param>
        /// <returns></returns>
        public int AppCryptKey_SaveKey(string MacAddr, string CryptKey)
        {
            SqlParameter[] parms =
            {
                SQLDatabase.MakeInParam("@MacAddr", SqlDbType.VarChar, 200, MacAddr),
                SQLDatabase.MakeInParam("@CryptKey", SqlDbType.VarChar, 4000, CryptKey)
            };

            return SQLDatabase.RunProc("MCS_SYS.dbo.sp_User_AppCryptKey_SaveKey", parms);
        }
        #endregion
        
    }
}
