// ===================================================================
// 文件： UD_DetailView.cs
// 项目名称：
// 创建时间：2009/3/5
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model
{
    /// <summary>
    ///UD_DetailView数据实体类
    /// </summary>
    [Serializable]
    public class UD_DetailView : IModel
    {
        #region 私有变量定义
        private Guid _id = Guid.NewGuid();
        private string _code = string.Empty;
        private string _name = string.Empty;
        #endregion

        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public UD_DetailView()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public UD_DetailView(Guid id, string code, string name)
        {
            _id = id;
            _code = code;
            _name = name;

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

        #endregion

        public string ModelName
        {
            get { return "UD_DetailView"; }
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
                    default:
                        return "";
                }
            }
            set
            {
                switch (FieldName)
                {
                    case "ID":
                        _id = new Guid(value);
                        break;
                    case "Code":
                        _code = value;
                        break;
                    case "Name":
                        _name = value;
                        break;

                }
            }
        }
        #endregion
    }
}
