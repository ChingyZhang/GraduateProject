// ===================================================================
// 文件： Addr_OfficialCity.cs
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
    ///Addr_OfficialCity数据实体类
    /// </summary>
    [Serializable]
    public class Addr_OfficialCity : IModel
    {
        #region 私有变量定义
        private int _id = 0;
        private string _name = string.Empty;
        private int _superid = 0;
        private int _level = 0;
        private string _code = string.Empty;
        private string _callareacode = string.Empty;
        private string _postcode = string.Empty;
        private int _births = 0;
        private int _attribute = 0;
        #endregion

        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public Addr_OfficialCity()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public Addr_OfficialCity(int id, string name, int superid, int level, string code, string callareacode, string postcode, int births, int attribute)
        {
            _id = id;
            _name = name;
            _superid = superid;
            _level = level;
            _code = code;
            _callareacode = callareacode;
            _postcode = postcode;
            _births = births;
            _attribute = attribute;

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
        ///CallAreaCode
        ///</summary>
        public string CallAreaCode
        {
            get { return _callareacode; }
            set { _callareacode = value; }
        }

        ///<summary>
        ///PostCode
        ///</summary>
        public string PostCode
        {
            get { return _postcode; }
            set { _postcode = value; }
        }


        /// <summary>
        /// Births
        /// </summary>

        public int Births
        {
            get { return _births; }

            set
            {
                _births = value;
            }
        }

        public int Attribute
        {
            get { return _attribute; }

            set
            {
                _attribute = value;
            }
        }
        #endregion

        public string ModelName
        {
            get { return "Addr_OfficialCity"; }
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
                    case "CallAreaCode":
                        return _callareacode;
                    case "PostCode":
                        return _postcode;

                    case "Births":
                        return _births.ToString();
                    case "Attribute":
                        return _attribute.ToString(); 

                    default:
                        return "";
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
                    case "CallAreaCode":
                        _callareacode = value;
                        break;
                    case "PostCode":
                        _postcode = value;
                        break;
                    case "Births":
                        int.TryParse(value, out _births);
                        break;
                    case "Attribute":
                        int.TryParse(value, out _attribute);
                        break;

                }
            }
        }
        #endregion
    }
}
