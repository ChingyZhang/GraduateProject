// ===================================================================
// 文件： FNA_BudgetChangeHistory.cs
// 项目名称：
// 创建时间：2010/5/15
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.FNA
{
    /// <summary>
    ///FNA_BudgetChangeHistory数据实体类
    /// </summary>
    [Serializable]
    public class FNA_BudgetChangeHistory : IModel
    {
        #region 私有变量定义
        private int _id = 0;
        private int _accountmonth = 0;
        private int _organizecity = 0;
        private int _feetype = 0;
        private decimal _changeamount = 0;
        private decimal _balance = 0;
        private int _changetype = 0;
        private int _changestaff = 0;
        private DateTime _changetime = new DateTime(1900, 1, 1);
        private string _relatedinfo = string.Empty;
        private NameValueCollection _extpropertys;
        #endregion

        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public FNA_BudgetChangeHistory()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public FNA_BudgetChangeHistory(int id, int accountmonth, int organizecity, int feetype, decimal changeamount, decimal balance, int changetype, int changestaff, DateTime changetime, string relatedinfo)
        {
            _id = id;
            _accountmonth = accountmonth;
            _organizecity = organizecity;
            _feetype = feetype;
            _changeamount = changeamount;
            _balance = balance;
            _changetype = changetype;
            _changestaff = changestaff;
            _changetime = changetime;
            _relatedinfo = relatedinfo;

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
        ///会计月
        ///</summary>
        public int AccountMonth
        {
            get { return _accountmonth; }
            set { _accountmonth = value; }
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
        ///费用类型
        ///</summary>
        public int FeeType
        {
            get { return _feetype; }
            set { _feetype = value; }
        }

        ///<summary>
        ///变动金额
        ///</summary>
        public decimal ChangeAmount
        {
            get { return _changeamount; }
            set { _changeamount = value; }
        }

        ///<summary>
        ///变动后余额
        ///</summary>
        public decimal Balance
        {
            get { return _balance; }
            set { _balance = value; }
        }

        ///<summary>
        ///变动类型
        ///</summary>
        public int ChangeType
        {
            get { return _changetype; }
            set { _changetype = value; }
        }

        ///<summary>
        ///变动操作人
        ///</summary>
        public int ChangeStaff
        {
            get { return _changestaff; }
            set { _changestaff = value; }
        }

        ///<summary>
        ///变动时间
        ///</summary>
        public DateTime ChangeTime
        {
            get { return _changetime; }
            set { _changetime = value; }
        }

        ///<summary>
        ///相关信息
        ///</summary>
        public string RelatedInfo
        {
            get { return _relatedinfo; }
            set { _relatedinfo = value; }
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
            get { return "FNA_BudgetChangeHistory"; }
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
                    case "AccountMonth":
                        return _accountmonth.ToString();
                    case "OrganizeCity":
                        return _organizecity.ToString();
                    case "FeeType":
                        return _feetype.ToString();
                    case "ChangeAmount":
                        return _changeamount.ToString();
                    case "Balance":
                        return _balance.ToString();
                    case "ChangeType":
                        return _changetype.ToString();
                    case "ChangeStaff":
                        return _changestaff.ToString();
                    case "ChangeTime":
                        return _changetime.ToString();
                    case "RelatedInfo":
                        return _relatedinfo;
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
                    case "AccountMonth":
                        int.TryParse(value, out _accountmonth);
                        break;
                    case "OrganizeCity":
                        int.TryParse(value, out _organizecity);
                        break;
                    case "FeeType":
                        int.TryParse(value, out _feetype);
                        break;
                    case "ChangeAmount":
                        decimal.TryParse(value, out _changeamount);
                        break;
                    case "Balance":
                        decimal.TryParse(value, out _balance);
                        break;
                    case "ChangeType":
                        int.TryParse(value, out _changetype);
                        break;
                    case "ChangeStaff":
                        int.TryParse(value, out _changestaff);
                        break;
                    case "ChangeTime":
                        DateTime.TryParse(value, out _changetime);
                        break;
                    case "RelatedInfo":
                        _relatedinfo = value;
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
