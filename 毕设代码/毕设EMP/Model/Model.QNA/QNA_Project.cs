// ===================================================================
// 文件： QNA_Project.cs
// 项目名称：
// 创建时间：2011/9/7
// 作者:	   TT
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.QNA
{
    /// <summary>
    ///QNA_Project数据实体类
    /// </summary>
    [Serializable]
    public class QNA_Project : IModel
    {
        #region 私有变量定义
        private int _id = 0;
        private string _name = string.Empty;
        private int _classify = 0;
        private int _displaytype = 0;
        private string _enabled = string.Empty;
        private int _approveflag = 0;
        private DateTime _inserttime = new DateTime(1900, 1, 1);
        private int _insertstaff = 0;
        private DateTime _updatetime = new DateTime(1900, 1, 1);
        private int _updatestaff = 0;

        private int _faceto = 0;
        private string _toallstaff = string.Empty;
        private string _toallorganizecity = string.Empty;
        private DateTime _effectivetime = new DateTime(1900, 1, 1);
        private DateTime _closetime = new DateTime(1900, 1, 1);
        private NameValueCollection _extpropertys;
        #endregion

        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public QNA_Project()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public QNA_Project(int id, string name, int classify, int displaytype, string enabled, int approveflag, DateTime inserttime, int insertstaff, DateTime updatetime, int updatestaff, int faceto, string toallstaff, string toallorganizecity, DateTime effectivetime, DateTime closetime)
        {
            _id = id;
            _name = name;
            _classify = classify;
            _displaytype = displaytype;
            _enabled = enabled;
            _approveflag = approveflag;
            _inserttime = inserttime;
            _insertstaff = insertstaff;
            _updatetime = updatetime;
            _updatestaff = updatestaff;
            _faceto = faceto;
            _toallstaff = toallstaff;
            _toallorganizecity = toallorganizecity;
            _effectivetime = effectivetime;
            _closetime = closetime;

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
        ///问卷名称
        ///</summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        ///<summary>
        ///问卷分类
        ///</summary>
        public int Classify
        {
            get { return _classify; }
            set { _classify = value; }
        }

        ///<summary>
        ///显示方式
        ///</summary>
        public int DisplayType
        {
            get { return _displaytype; }
            set { _displaytype = value; }
        }

        ///<summary>
        ///启用标志
        ///</summary>
        public string Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
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

        ///<summary>
        ///FaceTo
        ///</summary>
        public int FaceTo
        {
            get { return _faceto; }
            set { _faceto = value; }
        }

        ///<summary>
        ///ToAllStaff
        ///</summary>
        public string ToAllStaff
        {
            get { return _toallstaff; }
            set { _toallstaff = value; }
        }

        ///<summary>
        ///ToAllOrganizeCity
        ///</summary>
        public string ToAllOrganizeCity
        {
            get { return _toallorganizecity; }
            set { _toallorganizecity = value; }
        }

        ///<summary>
        ///EffectiveTime
        ///</summary>
        public DateTime EffectiveTime
        {
            get { return _effectivetime; }
            set { _effectivetime = value; }
        }

        ///<summary>
        ///CloseTime
        ///</summary>
        public DateTime CloseTime
        {
            get { return _closetime; }
            set { _closetime = value; }
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
            get { return "QNA_Project"; }
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
                    case "Classify":
                        return _classify.ToString();
                    case "DisplayType":
                        return _displaytype.ToString();
                    case "Enabled":
                        return _enabled;
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
                    case "FaceTo":
                        return _faceto.ToString();
                    case "ToAllStaff":
                        return _toallstaff;
                    case "ToAllOrganizeCity":
                        return _toallorganizecity;
                    case "EffectiveTime":
                        return _effectivetime.ToString();
                    case "CloseTime":
                        return _closetime.ToString();
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
                    case "Classify":
                        int.TryParse(value, out _classify);
                        break;
                    case "DisplayType":
                        int.TryParse(value, out _displaytype);
                        break;
                    case "Enabled":
                        _enabled = value;
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
                    case "FaceTo":
                        int.TryParse(value, out _faceto);
                        break;
                    case "ToAllStaff":
                        _toallstaff = value;
                        break;
                    case "ToAllOrganizeCity":
                        _toallorganizecity = value;
                        break;
                    case "EffectiveTime":
                        DateTime.TryParse(value, out _effectivetime);
                        break;
                    case "CloseTime":
                        DateTime.TryParse(value, out _closetime);
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
