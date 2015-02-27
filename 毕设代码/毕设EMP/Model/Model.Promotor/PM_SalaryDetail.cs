// ===================================================================
// 文件： PM_SalaryDetail.cs
// 项目名称：
// 创建时间：2009/2/27
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.Promotor
{
    /// <summary>
    ///PM_SalaryDetail数据实体类
    /// </summary>
    [Serializable]
    public class PM_SalaryDetail : IModel
    {
        #region 私有变量定义
        private int _id = 0;
        private int _salaryid = 0;
        private int _promotor = 0;
        private decimal _actworkdays = 0;
        private decimal _targetsalesvolume = 0;
        private decimal _actsalesvolume = 0;
        private decimal _kpiscore = 0;
        private decimal _bonus = 0;
        private decimal _totalsalary = 0;

        private decimal _pay1 = 0;
        private decimal _pay2 = 0;
        private decimal _pay3 = 0;
        private decimal _pay4 = 0;
        private decimal _pay5 = 0;
        private decimal _pay6 = 0;
        private decimal _pay7 = 0;
        private decimal _pay8 = 0;
        private decimal _pay9 = 0;
        private decimal _pay10 = 0;
        private decimal _pay11 = 0;
        private decimal _pay12 = 0;
        private decimal _pay13 = 0;
        private decimal _pay14 = 0;
        private decimal _pay15 = 0;
        private decimal _pay16 = 0;
        private decimal _pay17 = 0;
        private decimal _pay18 = 0;
        private decimal _pay19 = 0;
        private decimal _pay20 = 0;
        private decimal _sum1 = 0;
        private decimal _sum2 = 0;
        private decimal _sum3 = 0;
        private decimal _sum4 = 0;
        private decimal _tax = 0;
        private decimal _copmfee = 0;
        private decimal _dipmfee = 0;
        private decimal _pmfeetotal = 0;
        private NameValueCollection _extpropertys;
        #endregion

        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public PM_SalaryDetail()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public PM_SalaryDetail(int id, int salaryid, int promotor, decimal actworkdays, decimal targetsalesvolume, decimal actsalesvolume, decimal kpiscore, decimal bonus, decimal totalsalary, decimal pay1, decimal pay2, decimal pay3, decimal pay4, decimal pay5, decimal pay6, decimal pay7, decimal pay8, decimal pay9, decimal pay10, decimal pay11, decimal pay12, decimal pay13, decimal pay14, decimal pay15, decimal pay16, decimal pay17, decimal pay18, decimal pay19, decimal pay20, decimal sum1, decimal sum2, decimal sum3, decimal sum4, decimal tax, decimal copmfee, decimal dipmfee, decimal pmfeetotal)
        {
            _id = id;
            _salaryid = salaryid;
            _promotor = promotor;
            _actworkdays = actworkdays;
            _targetsalesvolume = targetsalesvolume;
            _actsalesvolume = actsalesvolume;
            _kpiscore = kpiscore;
            _bonus = bonus;
            _totalsalary = totalsalary;
            _pay1 = pay1;
            _pay2 = pay2;
            _pay3 = pay3;
            _pay4 = pay4;
            _pay5 = pay5;
            _pay6 = pay6;
            _pay7 = pay7;
            _pay8 = pay8;
            _pay9 = pay9;
            _pay10 = pay10;
            _pay11 = pay11;
            _pay12 = pay12;
            _pay13 = pay13;
            _pay14 = pay14;
            _pay15 = pay15;
            _pay16 = pay16;
            _pay17 = pay17;
            _pay18 = pay18;
            _pay19 = pay19;
            _pay20 = pay20;
            _sum1 = sum1;
            _sum2 = sum2;
            _sum3 = sum3;
            _sum4 = sum4;
            _tax = tax;
            _copmfee = copmfee;
            _dipmfee = dipmfee;
            _pmfeetotal = pmfeetotal;

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
        ///促销员
        ///</summary>
        public int Promotor
        {
            get { return _promotor; }
            set { _promotor = value; }
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
        ///目标销量
        ///</summary>
        public decimal TargetSalesVolume
        {
            get { return _targetsalesvolume; }
            set { _targetsalesvolume = value; }
        }

        ///<summary>
        ///实际销量
        ///</summary>
        public decimal ActSalesVolume
        {
            get { return _actsalesvolume; }
            set { _actsalesvolume = value; }
        }

        ///<summary>
        ///KPI评分
        ///</summary>
        public decimal KPIScore
        {
            get { return _kpiscore; }
            set { _kpiscore = value; }
        }

        ///<summary>
        ///奖金
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

        ///<summary>
        ///底薪
        ///</summary>
        public decimal Pay1
        {
            get { return _pay1; }
            set { _pay1 = value; }
        }

        ///<summary>
        ///我司底薪补贴
        ///</summary>
        public decimal Pay2
        {
            get { return _pay2; }
            set { _pay2 = value; }
        }

        ///<summary>
        ///工龄工资
        ///</summary>
        public decimal Pay3
        {
            get { return _pay3; }
            set { _pay3 = value; }
        }

        ///<summary>
        ///保底补贴
        ///</summary>
        public decimal Pay4
        {
            get { return _pay4; }
            set { _pay4 = value; }
        }

        ///<summary>
        ///社保补贴
        ///</summary>
        public decimal Pay5
        {
            get { return _pay5; }
            set { _pay5 = value; }
        }

        ///<summary>
        ///我司承担提成
        ///</summary>
        public decimal Pay6
        {
            get { return _pay6; }
            set { _pay6 = value; }
        }

        ///<summary>
        ///奖惩项
        ///</summary>
        public decimal Pay7
        {
            get { return _pay7; }
            set { _pay7 = value; }
        }

        ///<summary>
        ///社保代扣额
        ///</summary>
        public decimal Pay8
        {
            get { return _pay8; }
            set { _pay8 = value; }
        }

        ///<summary>
        ///浮动底薪补贴
        ///</summary>
        public decimal Pay9
        {
            get { return _pay9; }
            set { _pay9 = value; }
        }

        ///<summary>
        ///薪酬预留2
        ///</summary>
        public decimal Pay10
        {
            get { return _pay10; }
            set { _pay10 = value; }
        }

        ///<summary>
        ///含税工资额
        ///</summary>
        public decimal Pay11
        {
            get { return _pay11; }
            set { _pay11 = value; }
        }

        ///<summary>
        ///社保费用额
        ///</summary>
        public decimal Pay12
        {
            get { return _pay12; }
            set { _pay12 = value; }
        }

        ///<summary>
        ///社保报销额
        ///</summary>
        public decimal Pay13
        {
            get { return _pay13; }
            set { _pay13 = value; }
        }

        ///<summary>
        ///派遣服务费
        ///</summary>
        public decimal Pay14
        {
            get { return _pay14; }
            set { _pay14 = value; }
        }

        ///<summary>
        ///预留
        ///</summary>
        public decimal Pay15
        {
            get { return _pay15; }
            set { _pay15 = value; }
        }

        ///<summary>
        ///经销商底薪补贴
        ///</summary>
        public decimal Pay16
        {
            get { return _pay16; }
            set { _pay16 = value; }
        }

        ///<summary>
        ///费用补贴
        ///</summary>
        public decimal Pay17
        {
            get { return _pay17; }
            set { _pay17 = value; }
        }

        ///<summary>
        ///经销商承担提成
        ///</summary>
        public decimal Pay18
        {
            get { return _pay18; }
            set { _pay18 = value; }
        }

        ///<summary>
        ///经销商调整项
        ///</summary>
        public decimal Pay19
        {
            get { return _pay19; }
            set { _pay19 = value; }
        }

        ///<summary>
        ///预留2
        ///</summary>
        public decimal Pay20
        {
            get { return _pay20; }
            set { _pay20 = value; }
        }

        ///<summary>
        ///我司实发额
        ///</summary>
        public decimal Sum1
        {
            get { return _sum1; }
            set { _sum1 = value; }
        }

        ///<summary>
        ///导购费用小计
        ///</summary>
        public decimal Sum2
        {
            get { return _sum2; }
            set { _sum2 = value; }
        }

        ///<summary>
        ///经销薪资合计
        ///</summary>
        public decimal Sum3
        {
            get { return _sum3; }
            set { _sum3 = value; }
        }

        ///<summary>
        ///导购实得薪资小计
        ///</summary>
        public decimal Sum4
        {
            get { return _sum4; }
            set { _sum4 = value; }
        }

        ///<summary>
        ///个税
        ///</summary>
        public decimal Tax
        {
            get { return _tax; }
            set { _tax = value; }
        }

        ///<summary>
        ///我司卖场导购费用合计
        ///</summary>
        public decimal CoPMFee
        {
            get { return _copmfee; }
            set { _copmfee = value; }
        }

        ///<summary>
        ///经销商导购管理费用
        ///</summary>
        public decimal DIPMFee
        {
            get { return _dipmfee; }
            set { _dipmfee = value; }
        }

        ///<summary>
        ///导购费用合计
        ///</summary>
        public decimal PMFeeTotal
        {
            get { return _pmfeetotal; }
            set { _pmfeetotal = value; }
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
            get { return "PM_SalaryDetail"; }
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
                    case "Promotor":
                        return _promotor.ToString();
                    case "ActWorkDays":
                        return _actworkdays.ToString();
                    case "TargetSalesVolume":
                        return _targetsalesvolume.ToString();
                    case "ActSalesVolume":
                        return _actsalesvolume.ToString();
                    case "KPIScore":
                        return _kpiscore.ToString();
                    case "Bonus":
                        return _bonus.ToString();
                    case "TotalSalary":
                        return _totalsalary.ToString();
                    case "Pay1":
                        return _pay1.ToString();
                    case "Pay2":
                        return _pay2.ToString();
                    case "Pay3":
                        return _pay3.ToString();
                    case "Pay4":
                        return _pay4.ToString();
                    case "Pay5":
                        return _pay5.ToString();
                    case "Pay6":
                        return _pay6.ToString();
                    case "Pay7":
                        return _pay7.ToString();
                    case "Pay8":
                        return _pay8.ToString();
                    case "Pay9":
                        return _pay9.ToString();
                    case "Pay10":
                        return _pay10.ToString();
                    case "Pay11":
                        return _pay11.ToString();
                    case "Pay12":
                        return _pay12.ToString();
                    case "Pay13":
                        return _pay13.ToString();
                    case "Pay14":
                        return _pay14.ToString();
                    case "Pay15":
                        return _pay15.ToString();
                    case "Pay16":
                        return _pay16.ToString();
                    case "Pay17":
                        return _pay17.ToString();
                    case "Pay18":
                        return _pay18.ToString();
                    case "Pay19":
                        return _pay19.ToString();
                    case "Pay20":
                        return _pay20.ToString();
                    case "Sum1":
                        return _sum1.ToString();
                    case "Sum2":
                        return _sum2.ToString();
                    case "Sum3":
                        return _sum3.ToString();
                    case "Sum4":
                        return _sum4.ToString();
                    case "Tax":
                        return _tax.ToString();
                    case "CoPMFee":
                        return _copmfee.ToString();
                    case "DIPMFee":
                        return _dipmfee.ToString();
                    case "PMFeeTotal":
                        return _pmfeetotal.ToString();
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
                    case "SalaryID":
                        int.TryParse(value, out _salaryid);
                        break;
                    case "Promotor":
                        int.TryParse(value, out _promotor);
                        break;
                    case "ActWorkDays":
                        decimal.TryParse(value, out _actworkdays);
                        break;
                    case "TargetSalesVolume":
                        decimal.TryParse(value, out _targetsalesvolume);
                        break;
                    case "ActSalesVolume":
                        decimal.TryParse(value, out _actsalesvolume);
                        break;
                    case "KPIScore":
                        decimal.TryParse(value, out _kpiscore);
                        break;
                    case "Bonus":
                        decimal.TryParse(value, out _bonus);
                        break;
                    case "TotalSalary":
                        decimal.TryParse(value, out _totalsalary);
                        break;
                    case "Pay1":
                        decimal.TryParse(value, out _pay1);
                        break;
                    case "Pay2":
                        decimal.TryParse(value, out _pay2);
                        break;
                    case "Pay3":
                        decimal.TryParse(value, out _pay3);
                        break;
                    case "Pay4":
                        decimal.TryParse(value, out _pay4);
                        break;
                    case "Pay5":
                        decimal.TryParse(value, out _pay5);
                        break;
                    case "Pay6":
                        decimal.TryParse(value, out _pay6);
                        break;
                    case "Pay7":
                        decimal.TryParse(value, out _pay7);
                        break;
                    case "Pay8":
                        decimal.TryParse(value, out _pay8);
                        break;
                    case "Pay9":
                        decimal.TryParse(value, out _pay9);
                        break;
                    case "Pay10":
                        decimal.TryParse(value, out _pay10);
                        break;
                    case "Pay11":
                        decimal.TryParse(value, out _pay11);
                        break;
                    case "Pay12":
                        decimal.TryParse(value, out _pay12);
                        break;
                    case "Pay13":
                        decimal.TryParse(value, out _pay13);
                        break;
                    case "Pay14":
                        decimal.TryParse(value, out _pay14);
                        break;
                    case "Pay15":
                        decimal.TryParse(value, out _pay15);
                        break;
                    case "Pay16":
                        decimal.TryParse(value, out _pay16);
                        break;
                    case "Pay17":
                        decimal.TryParse(value, out _pay17);
                        break;
                    case "Pay18":
                        decimal.TryParse(value, out _pay18);
                        break;
                    case "Pay19":
                        decimal.TryParse(value, out _pay19);
                        break;
                    case "Pay20":
                        decimal.TryParse(value, out _pay20);
                        break;
                    case "Sum1":
                        decimal.TryParse(value, out _sum1);
                        break;
                    case "Sum2":
                        decimal.TryParse(value, out _sum2);
                        break;
                    case "Sum3":
                        decimal.TryParse(value, out _sum3);
                        break;
                    case "Sum4":
                        decimal.TryParse(value, out _sum4);
                        break;
                    case "Tax":
                        decimal.TryParse(value, out _tax);
                        break;
                    case "CoPMFee":
                        decimal.TryParse(value, out _copmfee);
                        break;
                    case "DIPMFee":
                        decimal.TryParse(value, out _dipmfee);
                        break;
                    case "PMFeeTotal":
                        decimal.TryParse(value, out _pmfeetotal);
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
