// ===================================================================
// 文件： Addr_OrganizeCity.cs
// 项目名称：
// 创建时间：2008-12-17
// 作者:	   
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
//using MCSFramework.Common;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model
{
    /// <summary>
    ///Addr_OrganizeCity数据实体类
    /// </summary>
    [Serializable]
    public class Addr_OrganizeCity : IModel
    {
        #region 私有变量定义
        private int _id = 0;
        private string _name = string.Empty;
        private int _superid = 0;
        private int _level = 0;
        private string _code = string.Empty;
        private int _manager = 0;

        private NameValueCollection _extpropertys;
        #endregion

        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public Addr_OrganizeCity()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public Addr_OrganizeCity(int id, string name, int superid, int level, string code, int manager)
        {
            _id = id;
            _name = name;
            _superid = superid;
            _level = level;
            _code = code;
            _manager = manager;

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
        ///Name
        ///</summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        ///<summary>
        ///SuperID
        ///</summary>
        public int SuperID
        {
            get { return _superid; }
            set { _superid = value; }
        }

        ///<summary>
        ///Level
        ///</summary>
        public int Level
        {
            get { return _level; }
            set { _level = value; }
        }

        ///<summary>
        ///Code
        ///</summary>
        public string Code
        {
            get { return _code; }
            set { _code = value; }
        }

        ///<summary>
        ///Manager
        ///</summary>
        public int Manager
        {
            get { return _manager; }
            set { _manager = value; }
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
            get { return "Addr_OrganizeCity"; }
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
                    case "Name":
                        return _name;
                    case "SuperID":
                        return _superid.ToString();
                    case "Level":
                        return _level.ToString();
                    case "Code":
                        return _code;
                    case "Manager":
                        return _manager.ToString();
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
                    case "Name":
                        _name = value;
                        break;
                    case "SuperID":
                        int.TryParse(value, out _superid);
                        break;
                    case "Level":
                        int.TryParse(value, out _level);
                        break;
                    case "Code":
                        _code = value;
                        break;
                    case "Manager":
                        int.TryParse(value, out _manager);
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
