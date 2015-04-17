using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MCSFramework.WSI
{
    /// <summary>
    /// 请求包头
    /// </summary>
    public class WSIRequestPack
    {
        /// <summary>
        /// 方法调用序列号
        /// </summary>
        public int Sequence = 0;

        /// <summary>
        /// 调用方法名
        /// </summary>
        public string Method = "";

        /// <summary>
        /// 授权码
        /// </summary>
        public string AuthKey = "";

        /// <summary>
        /// 参数列表(Json格式)
        /// </summary>
        public string Params = "";
    }

    /// <summary>
    /// 回复包头
    /// </summary>
    public class WSIResultPack
    {
        /// <summary>
        /// 方法调用序列号
        /// </summary>
        public int Sequence = 0;

        /// <summary>
        /// 返回值
        /// </summary>
        public int Return = 0;

        /// <summary>
        /// 返回信息(错误提示文字)
        /// </summary>
        public string ReturnInfo = "";

        /// <summary>
        /// 返回结果集(Json格式)
        /// </summary>
        public string Result = "";

        public WSIResultPack() { }

        public WSIResultPack(int _Sequence, int _Return)
        {
            Sequence = _Sequence;
            Return = _Return;
        }

        public WSIResultPack(int _Sequence, int _Return, string _ReturnInfo)
        {
            Sequence = _Sequence;
            Return = _Return;
            ReturnInfo = _ReturnInfo;
        }

        public WSIResultPack(int _Sequence, int _Return, string _ReturnInfo, string _Result)
        {
            Sequence = _Sequence;
            Return = _Return;
            ReturnInfo = _ReturnInfo;
            Result = _Result;
        }

        public string ToJsonString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public string ToJsonString(string AuthKey)
        {
            string EncryptText = "";
            CryptHelper.AESEncryptText(AuthKey, Result, out EncryptText);
            Result = EncryptText;

            return ToJsonString();
        }
    }
}