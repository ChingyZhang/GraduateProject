// ===================================================================
// 文件： ORD_OrderApply_ClientTimes.cs
// 项目名称：
// 创建时间：2013-03-20
// 作者:	   Jace
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.Logistics
{
    /// <summary>
    ///ORD_OrderApply_ClientTimes数据实体类
    /// </summary>
    [Serializable]
    public class ORD_OrderApply_ClientTimes : IModel
    {
        #region 私有变量定义
        private int _id = 0;
        private int _client = 0;
        private int _ordertimes = 0;
        private NameValueCollection _extpropertys;
        #endregion

        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public ORD_OrderApply_ClientTimes()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public ORD_OrderApply_ClientTimes(int id, int client, int ordertimes)
        {
            _id = id;
            _client = client;
            _ordertimes = ordertimes;

        }
        #endregion

        #region 公共属性
        ///<summary>
        ///ID
        ///</summary>
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        ///<summary>
        ///Client
        ///</summary>
        public int Client
        {
            get { return _client; }
            set { _client = value; }
        }

        ///<summary>
        ///OrderTimes
        ///</summary>
        public int OrderTimes
        {
            get { return _ordertimes; }
            set { _ordertimes = value; }
        }

        /// <summary>
        /// 扩展属性集合
        /// </summary>
        public NameValueCollection ExtPropertys
        {
            get { return _extpropertys; }
            set { _extpropertys = value; }
        }
        #endregion

        public string ModelName
        {
            get { return "ORD_OrderApply_ClientTimes"; }
        }
        #region 索引器访问
        public string this[string FieldName]
        {
            get
            {
                switch (FieldName)
                {
                    case "ID":
                        return _id.ToString();
                    case "Client":
                        return _client.ToString();
                    case "OrderTimes":
                        return _ordertimes.ToString();
                    default:
                        if (_extpropertys == null)
                            return "";
                        else
                            return _extpropertys[FieldName];
                }
            }
            set
            {
                switch (FieldName)
                {
                    case "ID":
                        int.TryParse(value, out _id);
                        break;
                    case "Client":
                        int.TryParse(value, out _client);
                        break;
                    case "OrderTimes":
                        int.TryParse(value, out _ordertimes);
                        break;
                    default:
                        if (_extpropertys == null)
                            _extpropertys = new NameValueCollection();
                        if (_extpropertys[FieldName] == null)
                            _extpropertys.Add(FieldName, value);
                        else
                            _extpropertys[FieldName] = value;
                        break;
                }
            }
        }
        #endregion
    }
}
