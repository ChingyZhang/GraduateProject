// ===================================================================
// 文件： FNA_StaffSalary.cs
// 项目名称：
// 创建时间：2013/4/25
// 作者:	   chf
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.FNA
{
    /// <summary>
    ///FNA_StaffSalary数据实体类
    /// </summary>
    [Serializable]
    public class FNA_StaffSalary : IModel
    {
        #region 私有变量定义
        private int _id = 0;
        private int _organizecity = 0;
        private int _accountmonth = 0;
        private int _state = 0;
        private int _classify = 0;
        private int _approvetask = 0;
        private int _approveflag = 0;
        private DateTime _inputtime = new DateTime(1900, 1, 1);
        private int _inputstaff = 0;
        private DateTime _updatetime = new DateTime(1900, 1, 1);
        private int _updatestaff = 0;
        private string _remark = string.Empty;

        private NameValueCollection _extpropertys;
        #endregion

        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public FNA_StaffSalary()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public FNA_StaffSalary(int id, int organizecity, int accountmonth, int state, int classify, int approvetask, int approveflag, DateTime inputtime, int inputstaff, DateTime updatetime, int updatestaff, string remark)
        {
            _id = id;
            _organizecity = organizecity;
            _accountmonth = accountmonth;
            _state = state;
            _classify = classify;
            _approvetask = approvetask;
            _approveflag = approveflag;
            _inputtime = inputtime;
            _inputstaff = inputstaff;
            _updatetime = updatetime;
            _updatestaff = updatestaff;
            _remark = remark;

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
        ///OrganizeCity
        ///</summary>
        public int OrganizeCity
        {
            get { return _organizecity; }
            set { _organizecity = value; }
        }

        ///<summary>
        ///会计月
        ///</summary>
        public int AccountMonth
        {
            get { return _accountmonth; }
            set { _accountmonth = value; }
        }

        ///<summary>
        ///工资单状态
        ///</summary>
        public int State
        {
            get { return _state; }
            set { _state = value; }
        }

        ///<summary>
        ///工资类别
        ///</summary>
        public int Classify
        {
            get { return _classify; }
            set { _classify = value; }
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
        ///审批标志
        ///</summary>
        public int ApproveFlag
        {
            get { return _approveflag; }
            set { _approveflag = value; }
        }

        ///<summary>
        ///录入日期
        ///</summary>
        public DateTime InputTime
        {
            get { return _inputtime; }
            set { _inputtime = value; }
        }

        ///<summary>
        ///录入人
        ///</summary>
        public int InputStaff
        {
            get { return _inputstaff; }
            set { _inputstaff = value; }
        }

        ///<summary>
        ///更新日期
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
        ///备注
        ///</summary>
        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
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
            get { return "FNA_StaffSalary"; }
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
                    case "OrganizeCity":
                        return _organizecity.ToString();
                    case "AccountMonth":
                        return _accountmonth.ToString();
                    case "State":
                        return _state.ToString();
                    case "Classify":
                        return _classify.ToString();
                    case "ApproveTask":
                        return _approvetask.ToString();
                    case "ApproveFlag":
                        return _approveflag.ToString();
                    case "InputTime":
                        return _inputtime.ToString();
                    case "InputStaff":
                        return _inputstaff.ToString();
                    case "UpdateTime":
                        return _updatetime.ToString();
                    case "UpdateStaff":
                        return _updatestaff.ToString();
                    case "Remark":
                        return _remark;
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
                    case "OrganizeCity":
                        int.TryParse(value, out _organizecity);
                        break;
                    case "AccountMonth":
                        int.TryParse(value, out _accountmonth);
                        break;
                    case "State":
                        int.TryParse(value, out _state);
                        break;
                    case "Classify":
                        int.TryParse(value, out _classify);
                        break;
                    case "ApproveTask":
                        int.TryParse(value, out _approvetask);
                        break;
                    case "ApproveFlag":
                        int.TryParse(value, out _approveflag);
                        break;
                    case "InputTime":
                        DateTime.TryParse(value, out _inputtime);
                        break;
                    case "InputStaff":
                        int.TryParse(value, out _inputstaff);
                        break;
                    case "UpdateTime":
                        DateTime.TryParse(value, out _updatetime);
                        break;
                    case "UpdateStaff":
                        int.TryParse(value, out _updatestaff);
                        break;
                    case "Remark":
                        _remark = value;
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
