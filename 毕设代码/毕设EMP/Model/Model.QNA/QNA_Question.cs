// ===================================================================
// 文件： QNA_Question.cs
// 项目名称：
// 创建时间：2009/12/13
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.QNA
{
    /// <summary>
    ///QNA_Question数据实体类
    /// </summary>
    [Serializable]
    public class QNA_Question : IModel
    {
        #region 私有变量定义
        private int _id = 0;
        private int _project = 0;
        private string _sortcode = string.Empty;
        private string _title = string.Empty;
        private string _description = string.Empty;
        private int _optionmode = 0;
        private int _defaultnextq = 0;
        private string _isfirstq = string.Empty;
        private string _islastq = string.Empty;
        private string _textregularexp = string.Empty;
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
        public QNA_Question()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public QNA_Question(int id, int project, string sortcode, string title, string description, int optionmode, int defaultnextq, string isfirstq, string islastq, string textregularexp, int approveflag, DateTime inserttime, int insertstaff, DateTime updatetime, int updatestaff)
        {
            _id = id;
            _project = project;
            _sortcode = sortcode;
            _title = title;
            _description = description;
            _optionmode = optionmode;
            _defaultnextq = defaultnextq;
            _isfirstq = isfirstq;
            _islastq = islastq;
            _textregularexp = textregularexp;
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
        ///所属调研项目
        ///</summary>
        public int Project
        {
            get { return _project; }
            set { _project = value; }
        }

        ///<summary>
        ///题号
        ///</summary>
        public string SortCode
        {
            get { return _sortcode; }
            set { _sortcode = value; }
        }

        ///<summary>
        ///标题
        ///</summary>
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        ///<summary>
        ///描述
        ///</summary>
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        ///<summary>
        ///答案模式
        ///</summary>
        public int OptionMode
        {
            get { return _optionmode; }
            set { _optionmode = value; }
        }

        ///<summary>
        ///默认下一问题
        ///</summary>
        public int DefaultNextQ
        {
            get { return _defaultnextq; }
            set { _defaultnextq = value; }
        }

        ///<summary>
        ///是否是问卷入口
        ///</summary>
        public string IsFirstQ
        {
            get { return _isfirstq; }
            set { _isfirstq = value; }
        }

        ///<summary>
        ///是否问卷结束
        ///</summary>
        public string IsLastQ
        {
            get { return _islastq; }
            set { _islastq = value; }
        }

        ///<summary>
        ///文本正则式
        ///</summary>
        public string TextRegularExp
        {
            get { return _textregularexp; }
            set { _textregularexp = value; }
        }

        ///<summary>
        ///审批标志
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
            get { return "QNA_Question"; }
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
                    case "Project":
                        return _project.ToString();
                    case "SortCode":
                        return _sortcode;
                    case "Title":
                        return _title;
                    case "Description":
                        return _description;
                    case "OptionMode":
                        return _optionmode.ToString();
                    case "DefaultNextQ":
                        return _defaultnextq.ToString();
                    case "IsFirstQ":
                        return _isfirstq;
                    case "IsLastQ":
                        return _islastq;
                    case "TextRegularExp":
                        return _textregularexp;
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
                    case "Project":
                        int.TryParse(value, out _project);
                        break;
                    case "SortCode":
                        _sortcode = value;
                        break;
                    case "Title":
                        _title = value;
                        break;
                    case "Description":
                        _description = value;
                        break;
                    case "OptionMode":
                        int.TryParse(value, out _optionmode);
                        break;
                    case "DefaultNextQ":
                        int.TryParse(value, out _defaultnextq);
                        break;
                    case "IsFirstQ":
                        _isfirstq = value;
                        break;
                    case "IsLastQ":
                        _islastq = value;
                        break;
                    case "TextRegularExp":
                        _textregularexp = value;
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
