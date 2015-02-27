// ===================================================================
// 文件： CM_FLApply_Base.cs
// 项目名称：
// 创建时间：2013-06-20
// 作者:	   Jace
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.CM
{
    /// <summary>
    ///CM_FLApply_Base数据实体类
    /// </summary>
    [Serializable]
    public class CM_FLApply_Base : IModel
    {
        #region 私有变量定义
        private int _id = 0;
        private int _client = 0;
        private int _accountmonth = 0;
        private decimal _flbase = 0;
        private int _fltype = 0;
        private int _ismyd = 0;
        private int _rtcount = 0;
        private int _flcontractid = 0;

        private NameValueCollection _extpropertys;
        #endregion

        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public CM_FLApply_Base()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public CM_FLApply_Base(int id, int client, int accountmonth, decimal flbase, int fltype, int ismyd, int rtcount, int flcontractid)
        {
            _id = id;
            _client = client;
            _accountmonth = accountmonth;
            _flbase = flbase;
            _fltype = fltype;
            _ismyd = ismyd;
            _rtcount = rtcount;
            _flcontractid = flcontractid;

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
        ///AccountMonth
        ///</summary>
        public int AccountMonth
        {
            get { return _accountmonth; }
            set { _accountmonth = value; }
        }

        ///<summary>
        ///FLBase
        ///</summary>
        public decimal FLBase
        {
            get { return _flbase; }
            set { _flbase = value; }
        }

        ///<summary>
        ///FLType
        ///</summary>
        public int FLType
        {
            get { return _fltype; }
            set { _fltype = value; }
        }

        ///<summary>
        ///ISMYD
        ///</summary>
        public int ISMYD
        {
            get { return _ismyd; }
            set { _ismyd = value; }
        }

        ///<summary>
        ///RTCount
        ///</summary>
        public int RTCount
        {
            get { return _rtcount; }
            set { _rtcount = value; }
        }

        ///<summary>
        ///FLContractID
        ///</summary>
        public int FLContractID
        {
            get { return _flcontractid; }
            set { _flcontractid = value; }
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
            get { return "CM_FLApply_Base"; }
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
                    case "AccountMonth":
                        return _accountmonth.ToString();
                    case "FLBase":
                        return _flbase.ToString();
                    case "FLType":
                        return _fltype.ToString();
                    case "ISMYD":
                        return _ismyd.ToString();
                    case "RTCount":
                        return _rtcount.ToString();
                    case "FLContractID":
                        return _flcontractid.ToString();
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
                    case "AccountMonth":
                        int.TryParse(value, out _accountmonth);
                        break;
                    case "FLBase":
                        decimal.TryParse(value, out _flbase);
                        break;
                    case "FLType":
                        int.TryParse(value, out _fltype);
                        break;
                    case "ISMYD":
                        int.TryParse(value, out _ismyd);
                        break;
                    case "RTCount":
                        int.TryParse(value, out _rtcount);
                        break;
                    case "FLContractID":
                        int.TryParse(value, out _flcontractid);
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
