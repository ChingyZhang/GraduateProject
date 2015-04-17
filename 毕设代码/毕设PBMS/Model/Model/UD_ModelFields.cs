// ===================================================================
// 文件： UD_ModelFields.cs
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
    ///UD_ModelFields数据实体类
    /// </summary>
    [Serializable]
    public class UD_ModelFields :IModel
    {
        #region 变量定义
        private Guid _id = Guid.NewGuid();
        private Guid _tableid ;
        private string _fieldname = String.Empty;
        private string _displayname = String.Empty;
        private int _position = 0;
        private string _flag = String.Empty;
        private int _datatype = 0;
        private int _datalength = 0;
        private int _precision = 0;
        private string _defaultvalue = String.Empty;
        private string _description = String.Empty;
        private DateTime _lastupdatetime = new DateTime(1900, 1, 1);
        private int _relationtype = 0;
        private string _relationtablename = string.Empty;
        private string _relationvaluefield = String.Empty;
        private string _relationtextfield = String.Empty;
        private string _searchpageurl = String.Empty;

        #endregion

        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public UD_ModelFields()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public UD_ModelFields(Guid id, Guid tableid, string fieldname, string displayname, int position, string flag, int datatype, int datalength, int precision, string defaultvalue, string description, DateTime lastupdatetime, int relationtype, string relationtablename, string relationvaluefield, string relationtextfield)
        {
            _id = id;
            _tableid = tableid;
            _fieldname = fieldname;
            _displayname = displayname;
            _position = position;
            _flag = flag;
            _datatype = datatype;
            _datalength = datalength;
            _precision = precision;
            _defaultvalue = defaultvalue;
            _description = description;
            _lastupdatetime = lastupdatetime;
            _relationtype = relationtype;
            _relationtablename = relationtablename;
            _relationvaluefield = relationvaluefield;
            _relationtextfield = relationtextfield;


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
        ///TableID
        ///</summary>
        public Guid TableID
        {
            get
            { return _tableid; }
            set
            { _tableid = value; }
        }

        ///<summary>
        ///FieldName
        ///</summary>
        public string FieldName
        {
            get
            { return _fieldname; }
            set
            { _fieldname = value; }
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
        ///在扩展属性序列中的位置
        ///</summary>
        public int Position
        {
            get
            { return _position; }
            set
            { _position = value; }
        }

        ///<summary>
        ///在现有的实体表中是否已经存在该字段
        ///</summary>
        public string Flag
        {
            get
            { return _flag; }
            set
            { _flag = value; }
        }

        ///<summary>
        ///DataType
        ///</summary>
        public int DataType
        {
            get
            { return _datatype; }
            set
            { _datatype = value; }
        }

        ///<summary>
        ///DataLength
        ///</summary>
        public int DataLength
        {
            get
            { return _datalength; }
            set
            { _datalength = value; }
        }

        ///<summary>
        ///Precision
        ///</summary>
        public int Precision
        {
            get
            { return _precision; }
            set
            { _precision = value; }
        }

        ///<summary>
        ///DefaultValue
        ///</summary>
        public string DefaultValue
        {
            get
            { return _defaultvalue; }
            set
            { _defaultvalue = value; }
        }

        ///<summary>
        ///Description
        ///</summary>
        public string Description
        {
            get
            { return _description; }
            set
            { _description = value; }
        }

        ///<summary>
        ///LastUpdateTime
        ///</summary>
        public DateTime LastUpdateTime
        {
            get
            { return _lastupdatetime; }
            set
            { _lastupdatetime = value; }
        }

        ///<summary>
        ///关联类型，1 关联字典 2,关联实体表
        ///</summary>
        public int RelationType
        {
            get
            { return _relationtype; }
            set
            { _relationtype = value; }
        }

        ///<summary>
        ///关联表名
        ///</summary>
        public string RelationTableName
        {
            get
            { return _relationtablename; }
            set
            { _relationtablename = value; }
        }

        ///<summary>
        ///关联表值字段
        ///</summary>
        public string RelationValueField
        {
            get
            { return _relationvaluefield; }
            set
            { _relationvaluefield = value; }
        }

        ///<summary>
        ///关联表文本字段
        ///</summary>
        public string RelationTextField
        {
            get
            { return _relationtextfield; }
            set
            { _relationtextfield = value; }
        }

        ///<summary>
        ///查询页面url
        ///</summary>
        public string SearchPageURL
        {
            get
            { return _searchpageurl; }
            set
            { _searchpageurl = value; }
        }


        #endregion


        #region IModel 成员

        public string ModelName
        {
            get { return "UD_ModelFields"; }
        }

        public string this[string FieldName]
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion
    }
}
