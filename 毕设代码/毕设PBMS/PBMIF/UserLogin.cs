using MCSFramework.BLL;
using MCSFramework.BLL.CM;
using MCSFramework.Common;
using MCSFramework.Common.Encrypter;
using MCSFramework.Model;
using MCSFramework.Model.CM;
using MCSFramework.WSI.Model;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace MCSFramework.WSI
{
    public class UserLogin
    {
        public UserLogin()
        {
            LogWriter.FILE_PATH = "C:\\MCSLog_PBMIF";
        }

        #region 检查用户授权码
        /// <summary>
        /// 检查用户授权码
        /// </summary>
        /// <param name="AuthKey"></param>
        /// <param name="NewMsgCount"></param>
        /// <returns></returns>
        public static UserInfo CheckAuthKey(string AuthKey, out int NewMsgCount)
        {
            string UserName = "";
            NewMsgCount = 0;
            if (string.IsNullOrEmpty(AuthKey)) return null;

            string cachekey = "EBMIF_OnlineUser-" + AuthKey;
            UserInfo User = (UserInfo)DataCache.GetCache(cachekey);
            if (User == null)
            {
                #region 如果缓存中丢失，从数据库获取用户登录信息，并再次放入缓存中
                string strUserInfo = "", strCryptKey = "", ExtPropertys = "";
                int ret = UserBLL.CheckAuthKey(AuthKey, 0, out UserName, out NewMsgCount, out strUserInfo, out strCryptKey, out ExtPropertys);
                if (ret < 0 || string.IsNullOrEmpty(strUserInfo)) return null;

                try
                {
                    User = JsonConvert.DeserializeObject<UserInfo>(strUserInfo);
                    if (User == null) return null;

                    DataCache.SetCache(cachekey, User, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 20, 0));

                    return User;
                }
                catch { return null; }
                #endregion
            }
            else
            {
                int ret = UserBLL.CheckAuthKey(AuthKey, 0, out UserName, out NewMsgCount);
                if (ret < 0)
                {
                    DataCache.RemoveCache(cachekey);
                    return null;
                }

                //放入1分钟活跃名单中
                string cachekey2 = "EBMIF_OnlineUser-ActiveOneMinute-" + AuthKey;
                DataCache.SetCache(cachekey2, User, DateTime.Now.AddMinutes(1), System.Web.Caching.Cache.NoSlidingExpiration);

                return User;
            }
        }

        /// <summary>
        /// 检查用户授权码
        /// </summary>
        /// <param name="AuthKey"></param>
        /// <returns></returns>
        public static UserInfo CheckAuthKey(string AuthKey)
        {
            //1分钟之内Check过的AuthKey，不再Check
            string cachekey = "EBMIF_OnlineUser-ActiveOneMinute-" + AuthKey;
            UserInfo User = (UserInfo)DataCache.GetCache(cachekey);
            if (User != null) return User;

            int NewMsgCount = 0;
            return CheckAuthKey(AuthKey, out NewMsgCount);
        }

        /// <summary>
        /// 检查授权码(含匿名用户)
        /// </summary>
        /// <param name="AuthKey"></param>
        /// <returns></returns>
        public static UserInfo CheckAuthKeyIncludeAnonymous(string AuthKey)
        {
            int NewMsgCount = 0;
            UserInfo User = CheckAuthKey(AuthKey, out NewMsgCount);

            return User;
        }
        #endregion

        #region 用户登录与注销
        /// <summary>
        /// 用户账户登录
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <param name="DeviceCode"></param>
        /// <param name="AuthKey"></param>
        /// <returns></returns>
        private static int userlogin(string UserName, string Password, string DeviceCode, out string AuthKey)
        {
            return userlogin(UserName, Password, DeviceCode, "", out AuthKey);
        }
        /// <summary>
        /// 帐号登录
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        private static int userlogin(string UserName, string Password, string DeviceCode, string ExtPropertys, out string AuthKey)
        {
            if (ConfigHelper.GetConfigBool("DebugMode")) LogWriter.WriteLog("UserLogin.Login:UserName=" + UserName + ",Password=" + Password);
            AuthKey = "";
            if (Membership.ValidateUser(UserName, Password))
            {
                UserInfo User = new UserInfo(UserName);

                #region 判断门店状态是否可以登录
                //if (user.ClientID > 0 && user.ClientType == 3)
                //{
                //    CM_Client _c = new CM_ClientBLL(user.ClientID).Model;

                //    //会员店已中止合作
                //    if (_c == null || _c.ActiveFlag == 2) return -11;

                //    //会员店状态不为已启动、准备启动状态
                //    if (_c["IsRMSClient"] == "2" || _c["IsRMSClient"] == "8") return -12;

                //    //会员店为流通店
                //    //if (_c["RTClassify"] == "1") return -13;
                //}
                #endregion

                AuthKey = Guid.NewGuid().ToString();
                User.DeviceCode = DeviceCode;

                string cachekey = "EBMIF_OnlineUser-" + AuthKey;
                DataCache.SetCache(cachekey, User, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 20, 0));

                cachekey = "EBMIF_DeviceCryptKey-" + DeviceCode;
                DeviceCryptKey cryptkey = (DeviceCryptKey)DataCache.GetCache(cachekey);
                if (cryptkey != null) cryptkey.AuthKey = AuthKey;

                HttpRequest Requst = HttpContext.Current.Request;
                string IpAddr = Requst.UserHostAddress;

                UserBLL.LoginSuccess(UserName, AuthKey, IpAddr, DeviceCode, JsonConvert.SerializeObject(User), JsonConvert.SerializeObject(cryptkey), ExtPropertys);
                return 1;
            }
            else if (UserName.StartsWith("1") && UserName.Length == 11 && Membership.GetUser(UserName) == null)
            {
                IList<Org_Staff> staffs = Org_StaffBLL.GetStaffList("Mobile='" + UserName + "' AND Dimission=1 AND ApproveFlag=1");
                if (staffs.Count == 1)
                {
                    DataTable dt = new Org_StaffBLL(staffs[0].ID).GetUserList();
                    if (dt.Rows.Count > 0)
                        return userlogin(dt.Rows[0]["UserName"].ToString(), Password, DeviceCode, ExtPropertys, out AuthKey);
                }
            }
            return -2;
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <param name="EncryptPassword">加密后的登录密码</param>
        /// <param name="DeviceCode">设备识别号</param>
        /// <param name="AuthKey">输出:授权码</param>
        /// <param name="ExtParams">扩展登录参数Json格式，包括AppCode、AppVersion、DeviceModel、DeviceOS、OSVersion、NetworkType
        /// 如:{"AppCode":"YSLRMAPP","AppVersion":43,"DeviceModel":"SM-G7108V","DeviceOS":"Android","OSVersion":"4.3","NetworkType":"ChinaMobile TD-SCDMA"}
        /// </param>
        /// <returns>0:登录成功 
        /// -1001:用户名或密码错误，登录失败 
        /// -1002:未能获取到对称加密密钥 
        /// -1003:设备号未在可登录的列表中登记
        /// -1004:当前用户不允许从该设备号登录
        /// -1005:登录失败
        /// -1009:APP版本过低必须更新
        /// </returns>        
        public static int Login(string UserName, string EncryptPassword, string DeviceCode, string ExtParams, out string AuthKey)
        {
            LogWriter.WriteLog("UserLogin.LoginEx2:UserName=" + UserName + ",EncryptPassword=" + EncryptPassword
                + ",DeviceCode=" + DeviceCode + ",ExtParams=" + ExtParams);
            AuthKey = "";

            Hashtable hs = string.IsNullOrEmpty(ExtParams) ? new Hashtable() : JsonConvert.DeserializeObject<Hashtable>(ExtParams);

            #region 判断是否符合最新版本要求
            if (hs["AppCode"] != null)
            {
                int MinAppVersion = 0;
                if (hs["AppCode"].ToString() == "PBMSAPP")
                    MinAppVersion = ConfigHelper.GetConfigInt("MinAppVersion");
                else if (hs["AppCode"].ToString() == "PBMSAPP-iOS")
                    MinAppVersion = ConfigHelper.GetConfigInt("MinAppVersion-iOS");

                if (MinAppVersion > 0 && hs["AppVersion"] != null)
                {
                    int AppVersion = 0;
                    if (int.TryParse(hs["AppVersion"].ToString(), out AppVersion) && AppVersion < MinAppVersion)
                    {
                        LogWriter.WriteLog("UserLogin.LoginEx2: AppVersion too lower! UserName=" + UserName + ",DeviceCode=" + DeviceCode + ",AppVersion=" + AppVersion.ToString());
                        return -1009;       //APP版本过低必须更新
                    }
                }
            }

            #endregion

            #region 组织登录扩展属性
            string ExtPropertys = "";
            try
            {
                IList<UD_TableList> tables = UD_TableListBLL.GetModelList("Name='MCS_SYS.dbo.User_Online'");
                if (tables.Count > 0)
                {
                    IList<UD_ModelFields> models = UD_ModelFieldsBLL.GetModelList("Tableid='" + tables[0].ID.ToString() + "' AND Flag='N'");
                    foreach (UD_ModelFields item in models.OrderBy(p => p.Position))
                    {
                        if (hs.ContainsKey(item.FieldName))
                        {
                            ExtPropertys += hs[item.FieldName].ToString();
                        }
                        ExtPropertys += "|";
                    }
                }
            }
            catch { }
            #endregion

            string cachekey = "EBMIF_DeviceCryptKey-" + DeviceCode;
            DeviceCryptKey key = null;

            #region 从数据库中加载保存的密钥
            if (key == null)
            {
                string _keystr = "";
                if (UserBLL.AppCryptKey_LoadKey(DeviceCode, out _keystr) == 0 && !string.IsNullOrEmpty(_keystr))
                {
                    try
                    {
                        key = JsonConvert.DeserializeObject<DeviceCryptKey>(_keystr);
                        if (key != null)
                        {
                            DataCache.SetCache(cachekey, key, DateTime.Now.AddMinutes(5), System.Web.Caching.Cache.NoSlidingExpiration);
                        }
                    }
                    catch { }
                }
            }
            #endregion

            int ret = 0;
            string Password = EncryptPassword;
            if (key == null)
            {
                LogWriter.WriteLog("UserLogin.LoginEx: Get DeviceCrytKey Null! UserName=" + UserName + ",DeviceCode=" + DeviceCode);
                return -1002;  //未能获取到对称加密密钥
            }
            else
            {
                ret = AESProvider.DecryptText(EncryptPassword, key.AESKey, key.AESIV, out Password);
                if (ret < 0)
                {
                    LogWriter.WriteLog("UserLogin.LoginEx! AESProvider.DecryptText Ret=" + ret.ToString() + ",DeviceCode=" + DeviceCode +
                        ",EncryptPassword=" + EncryptPassword + ",AESKey=" + key.AESKey + ",AESIV=" + key.AESIV);
                    return -1002;
                }
            }

            ret = userlogin(UserName, Password, DeviceCode, ExtPropertys, out AuthKey);

            if (ConfigHelper.GetConfigBool("DebugMode") && key != null)
            {
                LogWriter.WriteLog("UserLogin.LoginEx:Login Return ret=" + ret.ToString() + ",DeviceCode=" + DeviceCode
                    + ",AESKey=" + key.AESKey + ",AESIV=" + key.AESIV + ",AuthKey=" + AuthKey);
            }

            switch (ret)
            {
                case -1003:
                    //设备号未在可登录的列表中登记
                    return -1003;
                case -3:
                case -5:
                case -10:
                    //当前用户不允许从该设备号登录
                    return -1004;
                case -2:
                case -11:
                case -12:
                case -13:
                    //用户名或密码错误，登录失败
                    return -1001;
                case 1:
                    //登录成功
                    return 0;
                default:
                    //登录失败
                    return -1005;
            }
        }

        public static string Login_T(string UserName, string Password, string DeviceCode)
        {
            if (!ConfigHelper.GetConfigBool("DebugMode")) return "";
            LogWriter.WriteLog("UserLogin.Login1:UserName=" + UserName + ",Password=" + Password + ",DeviceCode=" + DeviceCode);

            string AuthKey = "";
            int ret = userlogin(UserName, Password, DeviceCode, out AuthKey);
            if (ret < 0)
                return ret.ToString();
            else
                return AuthKey;
        }

        /// <summary>
        /// 用户注销
        /// </summary>
        /// <param name="AuthKey"></param>
        /// <returns></returns>
        public static int Logout(UserInfo User, string AuthKey)
        {
            LogWriter.WriteLog("UserLogin.Logout:AuthKey=" + AuthKey);

            if (User != null)
            {
                UserBLL.Logout(User.UserName, AuthKey);

                string cachekey = "EBMIF_OnlineUser-" + AuthKey;
                DataCache.RemoveCache(cachekey);
            }

            return 0;
        }

        /// <summary>
        /// 根据设备号获取该设备关联的登录账户
        /// </summary>
        /// <param name="DeviceCode"></param>
        /// <returns></returns>        
        public List<string> GetUserNameListByDeviceCode(string DeviceCode)
        {
            IList<User_RegisterMAC> lists = User_RegisterMACBLL.GetModelList("MacAddr='" + DeviceCode +
                @"' AND Enabled='Y' AND ApproveFlag=1 AND UserName IN (SELECT LoweredUserName FROM aspnet_Users INNER JOIN aspnet_Membership 
                        ON aspnet_Users.UserId = aspnet_Membership.UserId AND aspnet_Membership.ClientID IS NOT NULL)");

            List<string> ret = new List<string>(lists.Count);
            foreach (User_RegisterMAC item in lists)
            {
                ret.Add(item.UserName);
            }
            return ret;
        }

        /// <summary>
        /// 根据门店ID，获取可登录的设备号列表
        /// </summary>
        /// <param name="ClientID"></param>
        /// <returns></returns>
        public List<string> GetDeviceCodeListByClientID(int ClientID)
        {
            IList<User_RegisterMAC> lists = User_RegisterMACBLL.GetModelList(@" UserName IN (SELECT LoweredUserName FROM aspnet_Users INNER JOIN aspnet_Membership 
                        ON aspnet_Users.UserId = aspnet_Membership.UserId AND aspnet_Membership.ClientID = " + ClientID.ToString() + ")  AND Enabled='Y' AND ApproveFlag=1");

            List<string> ret = new List<string>(lists.Count);
            foreach (User_RegisterMAC item in lists)
            {
                ret.Add(item.MacAddr.ToUpper());
            }
            return ret;
        }

        /// <summary>
        /// 根据设备号获取该设备的注册门店
        /// </summary>
        /// <param name="DeviceCode"></param>
        /// <returns></returns>

        public int GetClientInfoByDeviceCode(string DeviceCode)
        {
            List<string> lists = GetUserNameListByDeviceCode(DeviceCode);
            if (lists == null || lists.Count == 0) return -1;

            int client = UserBLL.GetClientIDByUsername(lists[0]);

            return client;
        }
        #endregion

        #region 修改密码
        /// <summary>
        /// 
        /// </summary>
        /// <param name="AuthKey"></param>
        /// <param name="OldPassword"></param>
        /// <param name="NewPassword"></param>
        /// <returns>0:修改成功 -1:修改失败 -100:用户需重新登录</returns>        
        public static int ChangePassword(UserInfo User, string OldPassword, string NewPassword)
        {
            LogWriter.WriteLog("UserLogin.ChangePassword:UserName=" + User.UserName + "OldPassword=" + OldPassword + ",NewPassword=" + NewPassword);

            MembershipUser memberuser = Membership.GetUser(User.UserName);
            if (memberuser == null) return -100;

            if (memberuser.ChangePassword(OldPassword, NewPassword))
                return 0;
            else
                return -1;
        }
        #endregion

        #region 客户端申请AES密码
        /// <summary>
        /// 客户端申请AES密码
        /// </summary>
        /// <param name="DeviceCode">设备号</param>
        /// <param name="Modulus">RSA公钥模</param>
        /// <param name="Exponent">RSA公钥指数</param>
        /// <param name="CryptAESKey">ASE密钥(密文)</param>
        /// <param name="CryptAESIV">ASE向量(密文)</param>
        /// <returns>0:成功 -100:设备号未在可登录的列表中登记</returns>
        public static int ApplyAESEncryptKey(string DeviceCode, string Modulus, string Exponent, out string CryptAESKey, out string CryptAESIV)
        {
            LogWriter.WriteLog("UserLogin.ApplyAESEncryptKey:DeviceCode=" + DeviceCode + ",Modulus=" + Modulus + ",Exponent=" + Exponent);
            CryptAESKey = ""; CryptAESIV = "";
            if (ConfigHelper.GetConfigBool("CheckDeviceCode"))
            {
                //if (!DeviceCode.StartsWith("iOS"))
                {
                    if (User_RegisterMACBLL.GetModelList("MacAddr='" + DeviceCode + "'").Count() == 0)
                    {
                        LogWriter.WriteLog("UserLogin.ApplyAESEncryptKey Error! DeviceCode not in allow lists! DeviceCode=" + DeviceCode);
                        return -1003;
                    }
                }
            }

            string cachekey = "EBMIF_DeviceCryptKey-" + DeviceCode;
            DeviceCryptKey key = null;

            #region 从数据库中加载保存的密钥
            if (key == null)
            {
                string _keystr = "";

                if (UserBLL.AppCryptKey_LoadKey(DeviceCode, out _keystr) == 0 && !string.IsNullOrEmpty(_keystr))
                {
                    try
                    { key = JsonConvert.DeserializeObject<DeviceCryptKey>(_keystr); }
                    catch { }
                }
            }
            #endregion

            if (key == null)
            {
                //生成AES加密密钥
                key = new DeviceCryptKey(DeviceCode, Modulus, Exponent);
                key.GenerateAESKey();
                DataCache.SetCache(cachekey, key, DateTime.Now.AddMinutes(5), System.Web.Caching.Cache.NoSlidingExpiration);

                //密钥保存至数据库
                UserBLL.AppCryptKey_SaveKey(DeviceCode, JsonConvert.SerializeObject(key));
            }

            //将密钥RSA加密
            RSAProvider.EncryptText(key.AESKey, Modulus, Exponent, out CryptAESKey);
            RSAProvider.EncryptText(key.AESIV, Modulus, Exponent, out CryptAESIV);

            if (ConfigHelper.GetConfigBool("DebugMode"))
                LogWriter.WriteLog("UserLogin.ApplyAESEncryptKeyA:DeviceCode=" + DeviceCode + ",AESKey=" + key.AESKey + ",AESIV=" + key.AESIV);
            LogWriter.WriteLog("UserLogin.ApplyAESEncryptKeyB:DeviceCode=" + DeviceCode + ",CryptAESKey=" + CryptAESKey + ",CryptAESIV=" + CryptAESIV);
            return 0;
        }
        #endregion

    }
}