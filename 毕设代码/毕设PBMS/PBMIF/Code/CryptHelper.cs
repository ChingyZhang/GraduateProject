using MCSFramework.BLL;
using MCSFramework.Common;
using MCSFramework.Common.Encrypter;
using MCSFramework.WSI.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MCSFramework.WSI
{
    public class CryptHelper
    {
        /// <summary>
        /// 根据授权码获取AES加密密钥
        /// </summary>
        /// <param name="AuthKey">登录授权码</param>
        /// <param name="AESKey">AES密钥</param>
        /// <param name="AESIV">AES向量</param>
        /// <returns>0:成功 -1:未找到缓存的密钥 -100:用户未登录</returns>
        public static int GetAESEncryptKey(string AuthKey, out string AESKey, out string AESIV)
        {
            AESKey = ""; AESIV = "";
            UserInfo User = UserLogin.CheckAuthKey(AuthKey);
            if (User == null) return -100;

            string cachekey = "EBMIF_DeviceCryptKey-" + User.DeviceCode;
            DeviceCryptKey key = (DeviceCryptKey)DataCache.GetCache(cachekey);
            if (key == null)
            {
                #region 如果缓存中丢失，则从数据库中获取加密信息，并再次放入缓存中
                string strUserName = "", strUserInfo = "", strCryptKey = "", ExtPropertys = "";
                int NewMsgCount = 0;
                int ret = UserBLL.CheckAuthKey(AuthKey, 0, out strUserName, out NewMsgCount, out strUserInfo, out strCryptKey, out ExtPropertys);
                if (ret < 0 || string.IsNullOrEmpty(strCryptKey))
                {
                    LogWriter.WriteLog("CryptHelper.GetAESEncryptKey,未能找到缓存的密钥1!AuthKey=" + AuthKey + ",DeviceCode=" + User.DeviceCode + ",strCryptKey=" + strCryptKey);
                    return -1;
                }

                try
                {
                    key = JsonConvert.DeserializeObject<DeviceCryptKey>(strCryptKey);
                    if (key == null)
                    {
                        LogWriter.WriteLog("CryptHelper.GetAESEncryptKey,未能找到缓存的密钥2!AuthKey=" + AuthKey + ",DeviceCode=" + User.DeviceCode + ",strCryptKey=" + strCryptKey);
                        return -1;
                    }

                    DataCache.SetCache(cachekey, key, DateTime.Now.AddMinutes(5), System.Web.Caching.Cache.NoSlidingExpiration);
                }
                catch (System.Exception err)
                {
                    LogWriter.WriteLog("CryptHelper.GetAESEncryptKey,未能找到缓存的密钥3!AuthKey=" + AuthKey + ",DeviceCode=" + User.DeviceCode + ",strCryptKey=" + strCryptKey, err);
                    return -1;
                }
                #endregion
            }

            AESKey = key.AESKey;
            AESIV = key.AESIV;
            return 0;
        }

        /// <summary>
        /// 解密文本
        /// </summary>
        /// <param name="AuthKey">登录授权码</param>
        /// <param name="EncryptText">密文</param>
        /// <param name="DecryptText">解密后的明文</param>
        /// <returns>0:成功 -1:未找到缓存的密钥 -2:解密失败 -100:用户未登录 </returns>
        public static int AESDecryptText(string AuthKey, string EncryptText, out string DecryptText)
        {
            DecryptText = "";

            if (string.IsNullOrEmpty(EncryptText)) return 0;

            //null字符不解密
            if (EncryptText.ToLower() == "null") { DecryptText = "null"; return 0; }

            string AESKey = "", AESIV = "";
            int ret = GetAESEncryptKey(AuthKey, out  AESKey, out AESIV);
            if (ret < 0)
            {
                if (ConfigHelper.GetConfigBool("DebugMode")) DecryptText = EncryptText;
                LogWriter.WriteLog("CryptHelper.AESDecryptText Error1! Ret=" + ret.ToString() + ",AuthKey=" + AuthKey + ",EncryptText=" + EncryptText);
                return ret;
            }

            ret = AESProvider.DecryptText(EncryptText, AESKey, AESIV, out DecryptText);
            if (ret < 0)
            {
                LogWriter.WriteLog("CryptHelper.AESDecryptText Error2! Ret=" + ret.ToString() + ",AuthKey=" + AuthKey + ",EncryptText=" + EncryptText);
                return -2;
            }

            return 0;
        }

        /// <summary>
        /// 加密文本
        /// </summary>
        /// <param name="AuthKey">登录授权码</param>
        /// <param name="DecryptText">明文</param>
        /// <param name="EncryptText">加密后的密文</param>
        /// <returns>0:成功 -1:未找到缓存的密钥 -2:加密失败 -100:用户未登录</returns>
        public static int AESEncryptText(string AuthKey, string DecryptText, out string EncryptText)
        {
            EncryptText = "";

            if (string.IsNullOrEmpty(DecryptText)) return 0;
            
            //null字符不加密
            if (DecryptText.ToLower() == "null") { EncryptText = "null"; return 0; }

            string AESKey = "", AESIV = "";
            int ret = GetAESEncryptKey(AuthKey, out  AESKey, out AESIV);
            if (ret < 0)
            {
                if (ConfigHelper.GetConfigBool("DebugMode")) EncryptText = DecryptText;
                LogWriter.WriteLog("CryptHelper.AESEncryptText Error1! Ret=" + ret.ToString() + ",AuthKey=" + AuthKey + ",DecryptText=" + DecryptText);
                return ret;
            }

            ret = AESProvider.EncryptText(DecryptText, AESKey, AESIV, out EncryptText);
            if (ret < 0)
            {
                LogWriter.WriteLog("CryptHelper.AESEncryptText Error2! Ret=" + ret.ToString() + ",AuthKey=" + AuthKey + ",DecryptText=" + DecryptText);
                return -2;
            }

            return 0;
        }
    }
}