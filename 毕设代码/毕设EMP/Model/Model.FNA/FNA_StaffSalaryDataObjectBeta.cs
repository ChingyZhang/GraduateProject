// ===================================================================
// 文件： FNA_StaffSalaryDataObjectBeta.cs
// 项目名称：
// 创建时间：2014/7/14
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
    ///FNA_StaffSalaryDataObjectBeta数据实体类
    /// </summary>
    [Serializable]
    public class FNA_StaffSalaryDataObjectBeta : IModel
    {
        #region 私有变量定义
        private int _id = 0;
        private int _staff = 0;
        private int _position = 0;
        private int _positiontype = 0;
        private decimal _salestarget = 0;
        private decimal _salestargetadjust = 0;
        private decimal _data01 = 0;
        private decimal _data02 = 0;
        private decimal _data03 = 0;
        private decimal _data04 = 0;
        private decimal _data05 = 0;
        private decimal _data06 = 0;
        private decimal _data07 = 0;
        private decimal _data08 = 0;
        private int _flag = 0;
        private int _submitflag = 0;
        private int _state = 0;
        private int _approveflag = 0;
        private int _updatestaff = 0;
        private DateTime _updatetime = new DateTime(1900, 1, 1);
        private NameValueCollection _extpropertys;
        private int _accountmonth = 0;
        private int _organizecity = 0;
        private string _remark="";
        #endregion

        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public FNA_StaffSalaryDataObjectBeta()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public FNA_StaffSalaryDataObjectBeta(int id, int staff, int position, int positiontype, decimal salestarget, decimal salestargetadjust, decimal data01, decimal data02, decimal data03, decimal data04, decimal data05, decimal data06, decimal data07, decimal data08, int flag, int submitflag, int state, int approveflag, int updatestaff, DateTime updatetime, int accountmonth, int organizecity)
        {
            _id = id;
            _staff = staff;
            _position = position;
            _positiontype = positiontype;
            _salestarget = salestarget;
            _salestargetadjust = salestargetadjust;
            _data01 = data01;
            _data02 = data02;
            _data03 = data03;
            _data04 = data04;
            _data05 = data05;
            _data06 = data06;
            _data07 = data07;
            _data08 = data08;
            _flag = flag;
            _submitflag = submitflag;
            _state = state;
            _approveflag = approveflag;
            _updatestaff = updatestaff;
            _updatetime = updatetime;
            _accountmonth = accountmonth;
            _organizecity = organizecity;

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
        ///Staff
        ///</summary>
        public int Staff
        {
            get { return _staff; }
            set { _staff = value; }
        }

        ///<summary>
        ///Position
        ///</summary>
        public int Position
        {
            get { return _position; }
            set { _position = value; }
        }

        ///<summary>
        ///PositionType
        ///</summary>
        public int PositionType
        {
            get { return _positiontype; }
            set { _positiontype = value; }
        }

        ///<summary>
        ///SalesTarget
        ///</summary>
        public decimal SalesTarget
        {
            get { return _salestarget; }
            set { _salestarget = value; }
        }

        ///<summary>
        ///SalesTargetAdjust
        ///</summary>
        public decimal SalesTargetAdjust
        {
            get { return _salestargetadjust; }
            set { _salestargetadjust = value; }
        }

        ///<summary>
        ///Data01
        ///</summary>
        public decimal Data01
        {
            get { return _data01; }
            set { _data01 = value; }
        }

        ///<summary>
        ///Data02
        ///</summary>
        public decimal Data02
        {
            get { return _data02; }
            set { _data02 = value; }
        }

        ///<summary>
        ///Data03
        ///</summary>
        public decimal Data03
        {
            get { return _data03; }
            set { _data03 = value; }
        }

        ///<summary>
        ///Data04
        ///</summary>
        public decimal Data04
        {
            get { return _data04; }
            set { _data04 = value; }
        }

        ///<summary>
        ///Data05
        ///</summary>
        public decimal Data05
        {
            get { return _data05; }
            set { _data05 = value; }
        }

        ///<summary>
        ///Data06
        ///</summary>
        public decimal Data06
        {
            get { return _data06; }
            set { _data06 = value; }
        }

        ///<summary>
        ///Data07
        ///</summary>
        public decimal Data07
        {
            get { return _data07; }
            set { _data07 = value; }
        }

        ///<summary>
        ///Data08
        ///</summary>
        public decimal Data08
        {
            get { return _data08; }
            set { _data08 = value; }
        }

        ///<summary>
        ///Flag
        ///</summary>
        public int Flag
        {
            get { return _flag; }
            set { _flag = value; }
        }

        ///<summary>
        ///SubmitFlag
        ///</summary>
        public int SubmitFlag
        {
            get { return _submitflag; }
            set { _submitflag = value; }
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
        ///ApproveFlag
        ///</summary>
        public int ApproveFlag
        {
            get { return _approveflag; }
            set { _approveflag = value; }
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

        ///<summary>
        ///AccountMonth
        ///</summary>
        public int AccountMonth
        {
            get { return _accountmonth; }
            set { _accountmonth = value; }
        }

        ///<summary>
        ///OrganizeCity
        ///</summary>
        public int OrganizeCity
        {
            get { return _organizecity; }
            set { _organizecity = value; }
        }

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
            get { return "FNA_StaffSalaryDataObjectBeta"; }
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
                    case "Staff":
                        return _staff.ToString();
                    case "Position":
                        return _position.ToString();
                    case "PositionType":
                        return _positiontype.ToString();
                    case "SalesTarget":
                        return _salestarget.ToString();
                    case "SalesTargetAdjust":
                        return _salestargetadjust.ToString();
                    case "Data01":
                        return _data01.ToString();
                    case "Data02":
                        return _data02.ToString();
                    case "Data03":
                        return _data03.ToString();
                    case "Data04":
                        return _data04.ToString();
                    case "Data05":
                        return _data05.ToString();
                    case "Data06":
                        return _data06.ToString();
                    case "Data07":
                        return _data07.ToString();
                    case "Data08":
                        return _data08.ToString();
                    case "Flag":
                        return _flag.ToString();
                    case "SubmitFlag":
                        return _submitflag.ToString();
                    case "State":
                        return _state.ToString();
                    case "ApproveFlag":
                        return _approveflag.ToString();
                    case "UpdateStaff":
                        return _updatestaff.ToString();
                    case "UpdateTime":
                        return _updatetime.ToString();
                    case "AccountMonth":
                        return _accountmonth.ToString();
                    case "OrganizeCity":
                        return _organizecity.ToString();
                    case "Remark":
                        return _remark.ToString();
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
                    case "Staff":
                        int.TryParse(value, out _staff);
                        break;
                    case "Position":
                        int.TryParse(value, out _position);
                        break;
                    case "PositionType":
                        int.TryParse(value, out _positiontype);
                        break;
                    case "SalesTarget":
                        decimal.TryParse(value, out _salestarget);
                        break;
                    case "SalesTargetAdjust":
                        decimal.TryParse(value, out _salestargetadjust);
                        break;
                    case "Data01":
                        decimal.TryParse(value, out _data01);
                        break;
                    case "Data02":
                        decimal.TryParse(value, out _data02);
                        break;
                    case "Data03":
                        decimal.TryParse(value, out _data03);
                        break;
                    case "Data04":
                        decimal.TryParse(value, out _data04);
                        break;
                    case "Data05":
                        decimal.TryParse(value, out _data05);
                        break;
                    case "Data06":
                        decimal.TryParse(value, out _data06);
                        break;
                    case "Data07":
                        decimal.TryParse(value, out _data07);
                        break;
                    case "Data08":
                        decimal.TryParse(value, out _data08);
                        break;
                    case "Flag":
                        int.TryParse(value, out _flag);
                        break;
                    case "SubmitFlag":
                        int.TryParse(value, out _submitflag);
                        break;
                    case "State":
                        int.TryParse(value, out _state);
                        break;
                    case "ApproveFlag":
                        int.TryParse(value, out _approveflag);
                        break;
                    case "UpdateStaff":
                        int.TryParse(value, out _updatestaff);
                        break;
                    case "UpdateTime":
                        DateTime.TryParse(value, out _updatetime);
                        break;
                    case "AccountMonth":
                        int.TryParse(value, out _accountmonth);
                        break;
                    case "OrganizeCity":
                        int.TryParse(value, out _organizecity);
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
