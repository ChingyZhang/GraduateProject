// ===================================================================
// 文件： FNA_StaffSalaryDataObject.cs
// 项目名称：
// 创建时间：2013/11/19
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
    ///FNA_StaffSalaryDataObject数据实体类
    /// </summary>
    [Serializable]
    public class FNA_StaffSalaryDataObject : IModel
    {
        #region 私有变量定义
        private int _id = 0;
        private int _staff = 0;
        private int _directorop = 0;
        private int _position = 0;
        private int _accountmonth = 0;
        private decimal _salestarget = 0;
        private decimal _salestargetadujst = 0;
        private decimal _actsales = 0;
        private decimal _keytarget = 0;
        private decimal _keytargetadjust = 0;
        private decimal _actkeysales = 0;
        private decimal _kpiyieldrate = 0;
        private decimal _bounsbase = 0;
        private decimal _data01 = 0;
        private decimal _data02 = 0;
        private decimal _data03 = 0;
        private decimal _data04 = 0;
        private decimal _data05 = 0;
        private decimal _data06 = 0;
        private decimal _data07 = 0;
        private decimal _data08 = 0;
        private decimal _data09 = 0;
        private decimal _data10 = 0;
        private int _flag = 0;
        private int _submitflag = 0;
        private int _state = 0;
        private int _approveflag = 0;
        private int _updatestaff = 0;
        private DateTime _updatetime = new DateTime(1900, 1, 1);

        private NameValueCollection _extpropertys;
        #endregion

        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public FNA_StaffSalaryDataObject()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public FNA_StaffSalaryDataObject(int id, int staff, int directorop, int position, int accountmonth, decimal salestarget, decimal salestargetadujst, decimal actsales, decimal keytarget, decimal keytargetadjust, decimal actkeysales, decimal kpiyieldrate, decimal bounsbase, decimal data01, decimal data02, decimal data03, decimal data04, decimal data05, decimal data06, decimal data07, decimal data08, decimal data09, decimal data10, int flag, int submitflag, int state, int approveflag, int updatestaff, DateTime updatetime)
        {
            _id = id;
            _staff = staff;
            _directorop = directorop;
            _position = position;
            _accountmonth = accountmonth;
            _salestarget = salestarget;
            _salestargetadujst = salestargetadujst;
            _actsales = actsales;
            _keytarget = keytarget;
            _keytargetadjust = keytargetadjust;
            _actkeysales = actkeysales;
            _kpiyieldrate = kpiyieldrate;
            _bounsbase = bounsbase;
            _data01 = data01;
            _data02 = data02;
            _data03 = data03;
            _data04 = data04;
            _data05 = data05;
            _data06 = data06;
            _data07 = data07;
            _data08 = data08;
            _data09 = data09;
            _data10 = data10;
            _flag = flag;
            _submitflag = submitflag;
            _state = state;
            _approveflag = approveflag;
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
        ///员工
        ///</summary>
        public int Staff
        {
            get { return _staff; }
            set { _staff = value; }
        }

        ///<summary>
        ///业务直接领导
        ///</summary>
        public int DirectorOp
        {
            get { return _directorop; }
            set { _directorop = value; }
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
        ///会计月
        ///</summary>
        public int AccountMonth
        {
            get { return _accountmonth; }
            set { _accountmonth = value; }
        }

        ///<summary>
        ///业绩目标
        ///</summary>
        public decimal SalesTarget
        {
            get { return _salestarget; }
            set { _salestarget = value; }
        }

        ///<summary>
        ///SalesTargetAdujst
        ///</summary>
        public decimal SalesTargetAdujst
        {
            get { return _salestargetadujst; }
            set { _salestargetadujst = value; }
        }

        ///<summary>
        ///业绩销量
        ///</summary>
        public decimal ActSales
        {
            get { return _actsales; }
            set { _actsales = value; }
        }

        ///<summary>
        ///重点品项目标
        ///</summary>
        public decimal KeyTarget
        {
            get { return _keytarget; }
            set { _keytarget = value; }
        }

        ///<summary>
        ///KeyTargetAdjust
        ///</summary>
        public decimal KeyTargetAdjust
        {
            get { return _keytargetadjust; }
            set { _keytargetadjust = value; }
        }

        ///<summary>
        ///重点品项销售
        ///</summary>
        public decimal ActKeySales
        {
            get { return _actkeysales; }
            set { _actkeysales = value; }
        }

        ///<summary>
        ///KPI达成率
        ///</summary>
        public decimal KPIYieldRate
        {
            get { return _kpiyieldrate; }
            set { _kpiyieldrate = value; }
        }

        ///<summary>
        ///BounsBase
        ///</summary>
        public decimal BounsBase
        {
            get { return _bounsbase; }
            set { _bounsbase = value; }
        }

        ///<summary>
        ///业代对应经销商进货
        ///</summary>
        public decimal Data01
        {
            get { return _data01; }
            set { _data01 = value; }
        }

        ///<summary>
        ///业代门店实销
        ///</summary>
        public decimal Data02
        {
            get { return _data02; }
            set { _data02 = value; }
        }

        ///<summary>
        ///经销商门店实销
        ///</summary>
        public decimal Data03
        {
            get { return _data03; }
            set { _data03 = value; }
        }

        ///<summary>
        ///业代对应经销商重点品项进货
        ///</summary>
        public decimal Data04
        {
            get { return _data04; }
            set { _data04 = value; }
        }

        ///<summary>
        ///业代门店重点品项实销
        ///</summary>
        public decimal Data05
        {
            get { return _data05; }
            set { _data05 = value; }
        }

        ///<summary>
        ///经销商门店重点品项实销
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
        ///Data09
        ///</summary>
        public decimal Data09
        {
            get { return _data09; }
            set { _data09 = value; }
        }

        ///<summary>
        ///Data10
        ///</summary>
        public decimal Data10
        {
            get { return _data10; }
            set { _data10 = value; }
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
        ///审核标志
        ///</summary>
        public int ApproveFlag
        {
            get { return _approveflag; }
            set { _approveflag = value; }
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
        ///更新时间
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
            get { return "FNA_StaffSalaryDataObject"; }
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
                    case "DirectorOp":
                        return _directorop.ToString();
                    case "Position":
                        return _position.ToString();
                    case "AccountMonth":
                        return _accountmonth.ToString();
                    case "SalesTarget":
                        return _salestarget.ToString();
                    case "SalesTargetAdujst":
                        return _salestargetadujst.ToString();
                    case "ActSales":
                        return _actsales.ToString();
                    case "KeyTarget":
                        return _keytarget.ToString();
                    case "KeyTargetAdjust":
                        return _keytargetadjust.ToString();
                    case "ActKeySales":
                        return _actkeysales.ToString();
                    case "KPIYieldRate":
                        return _kpiyieldrate.ToString();
                    case "BounsBase":
                        return _bounsbase.ToString();
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
                    case "Data09":
                        return _data09.ToString();
                    case "Data10":
                        return _data10.ToString();
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
                    case "DirectorOp":
                        int.TryParse(value, out _directorop);
                        break;
                    case "Position":
                        int.TryParse(value, out _position);
                        break;
                    case "AccountMonth":
                        int.TryParse(value, out _accountmonth);
                        break;
                    case "SalesTarget":
                        decimal.TryParse(value, out _salestarget);
                        break;
                    case "SalesTargetAdujst":
                        decimal.TryParse(value, out _salestargetadujst);
                        break;
                    case "ActSales":
                        decimal.TryParse(value, out _actsales);
                        break;
                    case "KeyTarget":
                        decimal.TryParse(value, out _keytarget);
                        break;
                    case "KeyTargetAdjust":
                        decimal.TryParse(value, out _keytargetadjust);
                        break;
                    case "ActKeySales":
                        decimal.TryParse(value, out _actkeysales);
                        break;
                    case "KPIYieldRate":
                        decimal.TryParse(value, out _kpiyieldrate);
                        break;
                    case "BounsBase":
                        decimal.TryParse(value, out _bounsbase);
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
                    case "Data09":
                        decimal.TryParse(value, out _data09);
                        break;
                    case "Data10":
                        decimal.TryParse(value, out _data10);
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
