// ===================================================================
// 文件： Rpt_DataSet_DataCache.cs
// 项目名称：
// 创建时间：2010/9/28
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.RPT
{
    /// <summary>
    ///Rpt_DataSet_DataCache数据实体类
    /// </summary>
    [Serializable]
    public class Rpt_DataSet_DataCache : IModel
    {
        #region 私有变量定义
        private Guid _id = Guid.NewGuid();
        private Guid _dataset = Guid.Empty;
        private string _paramvalues = string.Empty;
        private DateTime _updatetime = new DateTime(1900, 1, 1);
        private int _datalen = 0;
        private byte[] _data;
        private int _loadcount = 0;
        private DateTime _lastloadtime = new DateTime(1900, 1, 1);

        private NameValueCollection _extpropertys;
        #endregion

        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public Rpt_DataSet_DataCache()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public Rpt_DataSet_DataCache(Guid id, Guid dataset, string paramvalues, DateTime updatetime, int datalen, byte[] data, int loadcount, DateTime lastloadtime)
        {
            _id = id;
            _dataset = dataset;
            _paramvalues = paramvalues;
            _updatetime = updatetime;
            _datalen = datalen;
            _data = data;
            _loadcount = loadcount;
            _lastloadtime = lastloadtime;

        }
        #endregion

        #region 公共属性
        ///<summary>
        ///ID
        ///</summary>
        public Guid ID
        {
            get { return _id; }
            set { _id = value; }
        }

        ///<summary>
        ///所属数据集
        ///</summary>
        public Guid DataSet
        {
            get { return _dataset; }
            set { _dataset = value; }
        }

        ///<summary>
        ///参数值
        ///</summary>
        public string ParamValues
        {
            get { return _paramvalues; }
            set { _paramvalues = value; }
        }

        ///<summary>
        ///更新时间
        ///</summary>
        public DateTime UpdateTime
        {
            get { return _updatetime; }
            set { _updatetime = value; }
        }

        ///<summary>
        ///缓存长度
        ///</summary>
        public int DataLen
        {
            get { return _datalen; }
            set { _datalen = value; }
        }

        ///<summary>
        ///缓存
        ///</summary>
        public byte[] Data
        {
            get { return _data; }
            set { _data = value; }
        }

        ///<summary>
        ///加载次数
        ///</summary>
        public int LoadCount
        {
            get { return _loadcount; }
            set { _loadcount = value; }
        }

        ///<summary>
        ///最后加载时间
        ///</summary>
        public DateTime LastLoadTime
        {
            get { return _lastloadtime; }
            set { _lastloadtime = value; }
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
            get { return "Rpt_DataSet_DataCache"; }
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
                    case "DataSet":
                        return _dataset.ToString();
                    case "ParamValues":
                        return _paramvalues;
                    case "UpdateTime":
                        return _updatetime.ToString();
                    case "DataLen":
                        return _datalen.ToString();
                    case "Data":
                        return _data.ToString();
                    case "LoadCount":
                        return _loadcount.ToString();
                    case "LastLoadTime":
                        return _lastloadtime.ToString();
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
                        _id = new Guid(value);
                        break;
                    case "DataSet":
                        _dataset = new Guid(value);
                        break;
                    case "ParamValues":
                        _paramvalues = value;
                        break;
                    case "UpdateTime":
                        DateTime.TryParse(value, out _updatetime);
                        break;
                    case "DataLen":
                        int.TryParse(value, out _datalen);
                        break;
                    case "Data":
                        break;
                    case "LoadCount":
                        int.TryParse(value, out _loadcount);
                        break;
                    case "LastLoadTime":
                        DateTime.TryParse(value, out _lastloadtime);
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
