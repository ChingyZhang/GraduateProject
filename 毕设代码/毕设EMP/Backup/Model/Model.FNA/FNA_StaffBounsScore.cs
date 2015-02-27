// ===================================================================
// 文件： FNA_StaffBounsScore.cs
// 项目名称：
// 创建时间：2013-11-06
// 作者:	   Jace
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.FNA
{
    /// <summary>
    ///FNA_StaffBounsScore数据实体类
    /// </summary>
    [Serializable]
    public class FNA_StaffBounsScore : IModel
    {
        #region 私有变量定义
        private int _id = 0;
        private int _accountquarter = 0;
        private int _accountmonth = 0;
        private int _approveflag = 0;
        private int _approvetask = 0;
        private int _state = 0;
        private string _remark = string.Empty;
        private int _insertstaff = 0;
        private DateTime _inserttime = new DateTime(1900, 1, 1);
        private int _updatestaff = 0;
        private DateTime _updatetime = new DateTime(1900, 1, 1);

        private NameValueCollection _extpropertys;
        #endregion

        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public FNA_StaffBounsScore()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public FNA_StaffBounsScore(int id, int accountquarter, int accountmonth, int approveflag, int approvetask, int state, string remark, int insertstaff, DateTime inserttime, int updatestaff, DateTime updatetime)
        {
            _id = id;
            _accountquarter = accountquarter;
            _accountmonth = accountmonth;
            _approveflag = approveflag;
            _approvetask = approvetask;
            _state = state;
            _remark = remark;
            _insertstaff = insertstaff;
            _inserttime = inserttime;
            _updatestaff = updatestaff;
            _updatetime = updatetime;

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
        ///AccountQuarter
        ///</summary>
        public int AccountQuarter
        {
            get { return _accountquarter; }
            set { _accountquarter = value; }
        }

        ///<summary>
        ///AccountMonth
        ///</summary>
        public int AccountMonth
        {
            get { return _accountmonth; }
            set { _accountmonth = value; }
        }

        ///<summary>
        ///ApproveFlag
        ///</summary>
        public int ApproveFlag
        {
            get { return _approveflag; }
            set { _approveflag = value; }
        }

        ///<summary>
        ///ApproveTask
        ///</summary>
        public int ApproveTask
        {
            get { return _approvetask; }
            set { _approvetask = value; }
        }

        ///<summary>
        ///State
        ///</summary>
        public int State
        {
            get { return _state; }
            set { _state = value; }
        }

        ///<summary>
        ///Remark
        ///</summary>
        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }

        ///<summary>
        ///InsertStaff
        ///</summary>
        public int InsertStaff
        {
            get { return _insertstaff; }
            set { _insertstaff = value; }
        }

        ///<summary>
        ///InsertTime
        ///</summary>
        public DateTime InsertTime
        {
            get { return _inserttime; }
            set { _inserttime = value; }
        }

        ///<summary>
        ///UpdateStaff
        ///</summary>
        public int UpdateStaff
        {
            get { return _updatestaff; }
            set { _updatestaff = value; }
        }

        ///<summary>
        ///UpdateTime
        ///</summary>
        public DateTime UpdateTime
        {
            get { return _updatetime; }
            set { _updatetime = value; }
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
            get { return "FNA_StaffBounsScore"; }
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
                    case "AccountQuarter":
                        return _accountquarter.ToString();
                    case "AccountMonth":
                        return _accountmonth.ToString();
                    case "ApproveFlag":
                        return _approveflag.ToString();
                    case "ApproveTask":
                        return _approvetask.ToString();
                    case "State":
                        return _state.ToString();
                    case "Remark":
                        return _remark;
                    case "InsertStaff":
                        return _insertstaff.ToString();
                    case "InsertTime":
                        return _inserttime.ToString();
                    case "UpdateStaff":
                        return _updatestaff.ToString();
                    case "UpdateTime":
                        return _updatetime.ToString();
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
                    case "AccountQuarter":
                        int.TryParse(value, out _accountquarter);
                        break;
                    case "AccountMonth":
                        int.TryParse(value, out _accountmonth);
                        break;
                    case "ApproveFlag":
                        int.TryParse(value, out _approveflag);
                        break;
                    case "ApproveTask":
                        int.TryParse(value, out _approvetask);
                        break;
                    case "State":
                        int.TryParse(value, out _state);
                        break;
                    case "Remark":
                        _remark = value;
                        break;
                    case "InsertStaff":
                        int.TryParse(value, out _insertstaff);
                        break;
                    case "InsertTime":
                        DateTime.TryParse(value, out _inserttime);
                        break;
                    case "UpdateStaff":
                        int.TryParse(value, out _updatestaff);
                        break;
                    case "UpdateTime":
                        DateTime.TryParse(value, out _updatetime);
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
