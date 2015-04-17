using MCSFramework.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MCSFramework.WSI
{
    [Serializable]
    public class DeviceCryptKey
    {
        private string _devicecode = "";
        private string _authkey = "";
        private string _rsamodulus = "";
        private string _rsaexponent = "";
        private string _rsaprivatekey = "";
        private string _aeskey = "";
        private string _aesiv = "";

        /// <summary>
        /// 设备识别号
        /// </summary>
        public string DeviceCode
        {
            get { return _devicecode; }
            set { _devicecode = value; }
        }

        /// <summary>
        /// 登录授权码
        /// </summary>
        public string AuthKey
        {
            get { return _authkey; }
            set { _authkey = value; }
        }

        /// <summary>
        /// RSA公钥Key
        /// </summary>
        public string RSAModulus
        {
            get { return _rsamodulus; }
            set { _rsamodulus = value; }
        }

        /// <summary>
        /// RSA公钥指数
        /// </summary>
        public string RSAExponent
        {
            get { return _rsaexponent; }
            set { _rsaexponent = value; }
        }

        /// <summary>
        /// RSA私钥
        /// </summary>
        public string RSAPrivateKey
        {
            get { return _rsaprivatekey; }
            set { _rsaprivatekey = value; }
        }
        /// <summary>
        /// AES密钥
        /// </summary>
        public string AESKey
        {
            get { return _aeskey; }
            set { _aeskey = value; }
        }

        /// <summary>
        /// AES向量
        /// </summary>
        public string AESIV
        {
            get { return _aesiv; }
            set { _aesiv = value; }
        }

        public DeviceCryptKey() { }

        public DeviceCryptKey(string _DeviceCode)
        {
            _devicecode = _DeviceCode;
        }

        public DeviceCryptKey(string _DeviceCode, string _RSAModulus, string _RSAExponent)
        {
            _devicecode = _DeviceCode;
            _rsamodulus = _RSAModulus;
            _rsaexponent = _RSAExponent;
        }

        public void GenerateRSAKey()
        {
            MCSFramework.Common.Encrypter.RSAProvider.GenerateKey(out _rsamodulus, out _rsaexponent, out _rsaprivatekey);
        }
        public void GenerateAESKey()
        {
            MCSFramework.Common.Encrypter.AESProvider.GenerateKey(out _aeskey, out _aesiv);
        }
    }
}