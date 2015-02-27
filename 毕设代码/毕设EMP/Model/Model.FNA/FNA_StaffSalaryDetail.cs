// ===================================================================
// 文件： FNA_StaffSalaryDetail.cs
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
    ///FNA_StaffSalaryDetail数据实体类
    /// </summary>
    [Serializable]
    public class FNA_StaffSalaryDetail : IModel
    {
        #region 私有变量定义
        private int _id = 0;
        private int _salaryid = 0;
        private int _staff = 0;
        private int _organizecity = 0;
        private decimal _actworkdays = 0;
        private decimal _bounsbase = 0;
        private decimal _salestarget = 0;
        private decimal _actsales = 0;
        private decimal _salesyieldrate = 0;
        private decimal _saleswtdyieldrate = 0;
        private decimal _keytarget = 0;
        private decimal _actkeysales = 0;
        private decimal _keyyieldrate = 0;
        private decimal _keywtdyieldrate = 0;
        private decimal _feeratetarget = 0;
        private decimal _actfeerate = 0;
        private decimal _feeyieldrate = 0;
        private decimal _feewtdyieldrate = 0;
        private decimal _kpiyieldrate = 0;
        private decimal _kpibonus = 0;
        private decimal _deductedbonus = 0;
        private decimal _totalyieldrate = 0;
        private decimal _bonusadd = 0;
        private decimal _bounsdeduction = 0;
        private decimal _bonus = 0;
        private decimal _totalsalary = 0;

        private NameValueCollection _extpropertys;
        #endregion

        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public FNA_StaffSalaryDetail()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public FNA_StaffSalaryDetail(int id, int salaryid, int staff, int organizecity, decimal actworkdays, decimal bounsbase, decimal salestarget, decimal actsales, decimal salesyieldrate, decimal saleswtdyieldrate, decimal keytarget, decimal actkeysales, decimal keyyieldrate, decimal keywtdyieldrate, decimal feeratetarget, decimal actfeerate, decimal feeyieldrate, decimal feewtdyieldrate, decimal kpiyieldrate, decimal kpibonus, decimal deductedbonus, decimal totalyieldrate, decimal bonusadd, decimal bounsdeduction, decimal bonus, decimal totalsalary)
        {
            _id = id;
            _salaryid = salaryid;
            _staff = staff;
            _organizecity = organizecity;
            _actworkdays = actworkdays;
            _bounsbase = bounsbase;
            _salestarget = salestarget;
            _actsales = actsales;
            _salesyieldrate = salesyieldrate;
            _saleswtdyieldrate = saleswtdyieldrate;
            _keytarget = keytarget;
            _actkeysales = actkeysales;
            _keyyieldrate = keyyieldrate;
            _keywtdyieldrate = keywtdyieldrate;
            _feeratetarget = feeratetarget;
            _actfeerate = actfeerate;
            _feeyieldrate = feeyieldrate;
            _feewtdyieldrate = feewtdyieldrate;
            _kpiyieldrate = kpiyieldrate;
            _kpibonus = kpibonus;
            _deductedbonus = deductedbonus;
            _totalyieldrate = totalyieldrate;
            _bonusadd = bonusadd;
            _bounsdeduction = bounsdeduction;
            _bonus = bonus;
            _totalsalary = totalsalary;

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
        ///SalaryID
        ///</summary>
        public int SalaryID
        {
            get { return _salaryid; }
            set { _salaryid = value; }
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
        ///管理片区
        ///</summary>
        public int OrganizeCity
        {
            get { return _organizecity; }
            set { _organizecity = value; }
        }

        ///<summary>
        ///实际工作天数
        ///</summary>
        public decimal ActWorkDays
        {
            get { return _actworkdays; }
            set { _actworkdays = value; }
        }

        ///<summary>
        ///奖金基数
        ///</summary>
        public decimal BounsBase
        {
            get { return _bounsbase; }
            set { _bounsbase = value; }
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
        ///业绩实际销售
        ///</summary>
        public decimal ActSales
        {
            get { return _actsales; }
            set { _actsales = value; }
        }

        ///<summary>
        ///业绩达成率
        ///</summary>
        public decimal SalesYieldRate
        {
            get { return _salesyieldrate; }
            set { _salesyieldrate = value; }
        }

        ///<summary>
        ///业线加权达成率
        ///</summary>
        public decimal SalesWtdYieldRate
        {
            get { return _saleswtdyieldrate; }
            set { _saleswtdyieldrate = value; }
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
        ///重点品项销售
        ///</summary>
        public decimal ActKeySales
        {
            get { return _actkeysales; }
            set { _actkeysales = value; }
        }

        ///<summary>
        ///重点品项达成率
        ///</summary>
        public decimal KeyYieldRate
        {
            get { return _keyyieldrate; }
            set { _keyyieldrate = value; }
        }

        ///<summary>
        ///重点品项加权达成率
        ///</summary>
        public decimal KeyWtdYieldRate
        {
            get { return _keywtdyieldrate; }
            set { _keywtdyieldrate = value; }
        }

        ///<summary>
        ///费率目标
        ///</summary>
        public decimal FeeRateTarget
        {
            get { return _feeratetarget; }
            set { _feeratetarget = value; }
        }

        ///<summary>
        ///费率实际
        ///</summary>
        public decimal ActFeeRate
        {
            get { return _actfeerate; }
            set { _actfeerate = value; }
        }

        ///<summary>
        ///费率达成率
        ///</summary>
        public decimal FeeYieldRate
        {
            get { return _feeyieldrate; }
            set { _feeyieldrate = value; }
        }

        ///<summary>
        ///费率加权达成率
        ///</summary>
        public decimal FeeWtdYieldRate
        {
            get { return _feewtdyieldrate; }
            set { _feewtdyieldrate = value; }
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
        ///KPI奖金
        ///</summary>
        public decimal KPIBonus
        {
            get { return _kpibonus; }
            set { _kpibonus = value; }
        }

        ///<summary>
        ///KPI暂扣额
        ///</summary>
        public decimal DeductedBonus
        {
            get { return _deductedbonus; }
            set { _deductedbonus = value; }
        }

        ///<summary>
        ///总达成率
        ///</summary>
        public decimal TotalYieldRate
        {
            get { return _totalyieldrate; }
            set { _totalyieldrate = value; }
        }

        ///<summary>
        ///绩效奖金加项
        ///</summary>
        public decimal BonusAdd
        {
            get { return _bonusadd; }
            set { _bonusadd = value; }
        }

        ///<summary>
        ///绩效奖金减项
        ///</summary>
        public decimal Bounsdeduction
        {
            get { return _bounsdeduction; }
            set { _bounsdeduction = value; }
        }

        ///<summary>
        ///实发奖金
        ///</summary>
        public decimal Bonus
        {
            get { return _bonus; }
            set { _bonus = value; }
        }

        ///<summary>
        ///实发工资
        ///</summary>
        public decimal TotalSalary
        {
            get { return _totalsalary; }
            set { _totalsalary = value; }
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
            get { return "FNA_StaffSalaryDetail"; }
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
                    case "SalaryID":
                        return _salaryid.ToString();
                    case "Staff":
                        return _staff.ToString();
                    case "OrganizeCity":
                        return _organizecity.ToString();
                    case "ActWorkDays":
                        return _actworkdays.ToString();
                    case "BounsBase":
                        return _bounsbase.ToString();
                    case "SalesTarget":
                        return _salestarget.ToString();
                    case "ActSales":
                        return _actsales.ToString();
                    case "SalesYieldRate":
                        return _salesyieldrate.ToString();
                    case "SalesWtdYieldRate":
                        return _saleswtdyieldrate.ToString();
                    case "KeyTarget":
                        return _keytarget.ToString();
                    case "ActKeySales":
                        return _actkeysales.ToString();
                    case "KeyYieldRate":
                        return _keyyieldrate.ToString();
                    case "KeyWtdYieldRate":
                        return _keywtdyieldrate.ToString();
                    case "FeeRateTarget":
                        return _feeratetarget.ToString();
                    case "ActFeeRate":
                        return _actfeerate.ToString();
                    case "FeeYieldRate":
                        return _feeyieldrate.ToString();
                    case "FeeWtdYieldRate":
                        return _feewtdyieldrate.ToString();
                    case "KPIYieldRate":
                        return _kpiyieldrate.ToString();
                    case "KPIBonus":
                        return _kpibonus.ToString();
                    case "DeductedBonus":
                        return _deductedbonus.ToString();
                    case "TotalYieldRate":
                        return _totalyieldrate.ToString();
                    case "BonusAdd":
                        return _bonusadd.ToString();
                    case "Bounsdeduction":
                        return _bounsdeduction.ToString();
                    case "Bonus":
                        return _bonus.ToString();
                    case "TotalSalary":
                        return _totalsalary.ToString();
                    default:
                        if (_extpropertys == null)
                            return "";
                        else
                            return _extpropertys[FieldName]; return "";
                }
            }
            set
            {
                switch (FieldName)
                {
                    case "ID":
                        int.TryParse(value, out _id);
                        break;
                    case "SalaryID":
                        int.TryParse(value, out _salaryid);
                        break;
                    case "Staff":
                        int.TryParse(value, out _staff);
                        break;
                    case "OrganizeCity":
                        int.TryParse(value, out _organizecity);
                        break;
                    case "ActWorkDays":
                        decimal.TryParse(value, out _actworkdays);
                        break;
                    case "BounsBase":
                        decimal.TryParse(value, out _bounsbase);
                        break;
                    case "SalesTarget":
                        decimal.TryParse(value, out _salestarget);
                        break;
                    case "ActSales":
                        decimal.TryParse(value, out _actsales);
                        break;
                    case "SalesYieldRate":
                        decimal.TryParse(value, out _salesyieldrate);
                        break;
                    case "SalesWtdYieldRate":
                        decimal.TryParse(value, out _saleswtdyieldrate);
                        break;
                    case "KeyTarget":
                        decimal.TryParse(value, out _keytarget);
                        break;
                    case "ActKeySales":
                        decimal.TryParse(value, out _actkeysales);
                        break;
                    case "KeyYieldRate":
                        decimal.TryParse(value, out _keyyieldrate);
                        break;
                    case "KeyWtdYieldRate":
                        decimal.TryParse(value, out _keywtdyieldrate);
                        break;
                    case "FeeRateTarget":
                        decimal.TryParse(value, out _feeratetarget);
                        break;
                    case "ActFeeRate":
                        decimal.TryParse(value, out _actfeerate);
                        break;
                    case "FeeYieldRate":
                        decimal.TryParse(value, out _feeyieldrate);
                        break;
                    case "FeeWtdYieldRate":
                        decimal.TryParse(value, out _feewtdyieldrate);
                        break;
                    case "KPIYieldRate":
                        decimal.TryParse(value, out _kpiyieldrate);
                        break;
                    case "KPIBonus":
                        decimal.TryParse(value, out _kpibonus);
                        break;
                    case "DeductedBonus":
                        decimal.TryParse(value, out _deductedbonus);
                        break;
                    case "TotalYieldRate":
                        decimal.TryParse(value, out _totalyieldrate);
                        break;
                    case "BonusAdd":
                        decimal.TryParse(value, out _bonusadd);
                        break;
                    case "Bounsdeduction":
                        decimal.TryParse(value, out _bounsdeduction);
                        break;
                    case "Bonus":
                        decimal.TryParse(value, out _bonus);
                        break;
                    case "TotalSalary":
                        decimal.TryParse(value, out _totalsalary);
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
