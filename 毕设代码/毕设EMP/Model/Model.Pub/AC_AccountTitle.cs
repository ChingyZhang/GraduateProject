// ===================================================================
// 文件： AC_AccountTitle.cs
// 项目名称：
// 创建时间：2008-12-22
// 作者:	   
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.Pub
{
    /// <summary>
    ///AC_AccountTitle数据实体类
    /// </summary>
    [Serializable]
    public class AC_AccountTitle : IModel
    {
        #region 私有变量定义
        private int _id = 0;
        private string _code = string.Empty;
        private string _name = string.Empty;
        private int _superid = 0;
        private int _level = 0;
        private int _department = 0;
        private string _description = string.Empty;
        private int _feetype = 0;
        private NameValueCollection _extpropertys;
        #endregion

        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public AC_AccountTitle()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public AC_AccountTitle(int id, string code, string name, int superid, int level, int department, string description, int feetype)
        {
            _id = id;
            _code = code;
            _name = name;
            _superid = superid;
            _level = level;
            _department = department;
            _description = description;
            _feetype = feetype;

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
        ///Code
        ///</summary>
        public string Code
        {
            get { return _code; }
            set { _code = value; }
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
        ///Department
        ///</summary>
        public int Department
        {
            get { return _department; }
            set { _department = value; }
        }

        ///<summary>
        ///Description
        ///</summary>
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        ///<summary>
        ///FeeType
        ///</summary>
        public int FeeType
        {
            get { return _feetype; }
            set { _feetype = value; }
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
            get { return "AC_AccountTitle"; }
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
                    case "Code":
                        return _code;
                    case "Name":
                        return _name;
                    case "SuperID":
                        return _superid.ToString();
                    case "Level":
                        return _level.ToString();
                    case "Department":
                        return _department.ToString();
                    case "Description":
                        return _description;
                    case "FeeType":
                        return _feetype.ToString();
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
                    case "Code":
                        _code = value;
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
                    case "Department":
                        int.TryParse(value, out _department);
                        break;
                    case "Description":
                        _description = value;
                        break;
                    case "FeeType":
                        int.TryParse(value, out _feetype);
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
