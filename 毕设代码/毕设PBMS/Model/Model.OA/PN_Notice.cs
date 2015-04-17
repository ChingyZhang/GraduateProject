// ===================================================================
// 文件： PN_Notice.cs
// 项目名称：
// 创建时间：2010/5/14
// 作者:	   
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.OA
{
    /// <summary>
    ///PN_Notice数据实体类
    /// </summary>
    [Serializable]
    public class PN_Notice : IModel
    {
        #region 私有变量定义
        private int _id = 0;
        private string _topic = string.Empty;
        private string _keyword = string.Empty;
        private string _content = string.Empty;
        private string _toallstaff = string.Empty;
        private string _toallorganizecity = string.Empty;
        private string _cancomment = string.Empty;
        private string _isdelete = string.Empty;
        private int _approveflag = 0;
        private DateTime _inserttime = new DateTime(1900, 1, 1);
        private int _insertstaff = 0;
        private DateTime _updatetime = new DateTime(1900, 1, 1);
        private int _updatestaff = 0;

        private NameValueCollection _extpropertys;
        #endregion

        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public PN_Notice()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public PN_Notice(int id, string topic, string keyword, string content, string toallstaff, string cancomment, string isdelete, int approveflag, DateTime inserttime, int insertstaff, DateTime updatetime, int updatestaff)
        {
            _id = id;
            _topic = topic;
            _keyword = keyword;
            _content = content;
            _toallstaff = toallstaff;
            _cancomment = cancomment;
            _isdelete = isdelete;
            _approveflag = approveflag;
            _inserttime = inserttime;
            _insertstaff = insertstaff;
            _updatetime = updatetime;
            _updatestaff = updatestaff;

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
        ///主题
        ///</summary>
        public string Topic
        {
            get { return _topic; }
            set { _topic = value; }
        }

        ///<summary>
        ///关键字
        ///</summary>
        public string KeyWord
        {
            get { return _keyword; }
            set { _keyword = value; }
        }

        ///<summary>
        ///内容
        ///</summary>
        public string Content
        {
            get { return _content; }
            set { _content = value; }
        }

        ///<summary>
        ///是否面向全体职位
        ///</summary>
        public string ToAllStaff
        {
            get { return _toallstaff; }
            set { _toallstaff = value; }
        }

        ///<summary>
        ///是否面向所有管理片区
        ///</summary>
        public string ToAllOrganizeCity
        {
            get { return _toallorganizecity; }
            set { _toallorganizecity = value; }
        }

        ///<summary>
        ///是否可以评论
        ///</summary>
        public string CanComment
        {
            get { return _cancomment; }
            set { _cancomment = value; }
        }

        ///<summary>
        ///是否删除
        ///</summary>
        public string IsDelete
        {
            get { return _isdelete; }
            set { _isdelete = value; }
        }

        ///<summary>
        ///审核标志
        ///</summary>
        public int ApproveFlag
        {
            get { return _approveflag; }
            set { _approveflag = value; }
        }

        ///<summary>
        ///录入时间
        ///</summary>
        public DateTime InsertTime
        {
            get { return _inserttime; }
            set { _inserttime = value; }
        }

        ///<summary>
        ///录入人
        ///</summary>
        public int InsertStaff
        {
            get { return _insertstaff; }
            set { _insertstaff = value; }
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
        ///更新人
        ///</summary>
        public int UpdateStaff
        {
            get { return _updatestaff; }
            set { _updatestaff = value; }
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
            get { return "PN_Notice"; }
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
                    case "Topic":
                        return _topic;
                    case "KeyWord":
                        return _keyword;
                    case "Content":
                        return _content;
                    case "ToAllStaff":
                        return _toallstaff;
                    case "ToAllOrganizeCity":
                        return _toallorganizecity;
                    case "CanComment":
                        return _cancomment;
                    case "IsDelete":
                        return _isdelete;
                    case "ApproveFlag":
                        return _approveflag.ToString();
                    case "InsertTime":
                        return _inserttime.ToString();
                    case "InsertStaff":
                        return _insertstaff.ToString();
                    case "UpdateTime":
                        return _updatetime.ToString();
                    case "UpdateStaff":
                        return _updatestaff.ToString();
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
                    case "Topic":
                        _topic = value;
                        break;
                    case "KeyWord":
                        _keyword = value;
                        break;
                    case "Content":
                        _content = value;
                        break;
                    case "ToAllStaff":
                        _toallstaff = value;
                        break;
                    case "ToAllOrganizeCity":
                        _toallorganizecity = value;
                        break;
                    case "CanComment":
                        _cancomment = value;
                        break;
                    case "IsDelete":
                        _isdelete = value;
                        break;
                    case "ApproveFlag":
                        int.TryParse(value, out _approveflag);
                        break;
                    case "InsertTime":
                        DateTime.TryParse(value, out _inserttime);
                        break;
                    case "InsertStaff":
                        int.TryParse(value, out _insertstaff);
                        break;
                    case "UpdateTime":
                        DateTime.TryParse(value, out _updatetime);
                        break;
                    case "UpdateStaff":
                        int.TryParse(value, out _updatestaff);
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
