// ===================================================================
// 文件： UD_TableList.cs
// 项目名称：
// 创建时间：2008-11-25
// 作者:	   
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using MCSFramework.Common;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model
{
    /// <summary>
    ///UD_TableList数据实体类
    /// </summary>
    [Serializable]
    public class UD_TableList : IModel
    {
        #region 变量定义
        private Guid _id = Guid.NewGuid();
        private string _name = String.Empty;
        private string _displayname = String.Empty;
        private string _extflag = "N";
        private string _modelname = String.Empty;
        private string _treeflag = "N";


        #endregion

        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public UD_TableList()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public UD_TableList(Guid id, string name, string displayname, string extflag, string modelname, string treeflag)
        {
            _id = id;
            _name = name;
            _displayname = displayname;
            _extflag = extflag;
            _modelname = modelname;
            _treeflag = treeflag;

        }
        #endregion

        #region 公共属性

        ///<summary>
        ///ID
        ///</summary>
        public Guid ID
        {
            get
            { return _id; }
            set
            { _id = value; }
        }

        ///<summary>
        ///Name
        ///</summary>
        public string Name
        {
            get
            { return _name; }
            set
            { _name = value; }
        }

        ///<summary>
        ///DisplayName
        ///</summary>
        public string DisplayName
        {
            get
            { return _displayname; }
            set
            { _displayname = value; }
        }

        ///<summary>
        ///ExtFlag
        ///</summary>
        public string ExtFlag
        {
            get
            { return _extflag; }
            set
            { _extflag = value; }
        }

        /// <summary>
        /// Model实体类名
        /// </summary>
        public string ModelClassName
        {
            get
            { return _modelname; }
            set
            { _modelname = value; }
        }

        /// <summary>
        /// 是否树形结构
        /// </summary>
        public string TreeFlag
        {
            get { return _treeflag; }
            set { _treeflag = value; }
        }
        #endregion
        public string ModelName
        {
            get { return "UD_TableList"; }
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
                    case "DisplayName":
                        return _displayname;
                    case "ExtFlag":
                        return _extflag;
                    case "ModelName":
                        return _modelname;
                    case "TreeFlag":
                        return _treeflag;
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
                    case "Name":
                        _name = value;
                        break;
                    case "DisplayName":
                        _displayname = value;
                        break;
                    case "ExtFlag":
                        _extflag = value;
                        break;
                    case "ModelName":
                        _modelname = value;
                        break;
                    case "TreeFlag":
                        _treeflag = value;
                        break;

                }
            }
        }
        #endregion

    }
}
