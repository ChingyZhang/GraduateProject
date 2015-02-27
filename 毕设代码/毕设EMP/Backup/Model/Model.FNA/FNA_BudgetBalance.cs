// ===================================================================
// 文件： FNA_BudgetBalance.cs
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
    ///FNA_BudgetBalance数据实体类
    /// </summary>
    [Serializable]
    public class FNA_BudgetBalance : IModel
    {
        #region 私有变量定义
        private int _id = 0;
        private int _accountmonth = 0;
        private int _organizecity = 0;
        private int _feetype = 0;
        private decimal _costbalance = 0;
        private decimal _ddfinitialbalance = 0;
        private DateTime _updatetime = new DateTime(1900, 1, 1);
        private NameValueCollection _extpropertys;
        #endregion

        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public FNA_BudgetBalance()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public FNA_BudgetBalance(int id, int accountmonth, int organizecity, int feetype, decimal costbalance, decimal ddfinitialbalance, DateTime updatetime)
        {
            _id = id;
            _accountmonth = accountmonth;
            _organizecity = organizecity;
            _feetype = feetype;
            _costbalance = costbalance;
            _ddfinitialbalance = ddfinitialbalance;
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
        ///余额
        ///</summary>
        public decimal CostBalance
        {
            get { return _costbalance; }
            set { _costbalance = value; }
        }

        ///<summary>
        ///DDF期初余额
        ///</summary>
        public decimal DDFInitialBalance
        {
            get { return _ddfinitialbalance; }
            set { _ddfinitialbalance = value; }
        }

        ///<summary>
        ///最后更新时间
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
            get { return "FNA_BudgetBalance"; }
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
                    case "CostBalance":
                        return _costbalance.ToString();
                    case "DDFInitialBalance":
                        return _ddfinitialbalance.ToString();
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
                    case "AccountMonth":
                        int.TryParse(value, out _accountmonth);
                        break;
                    case "OrganizeCity":
                        int.TryParse(value, out _organizecity);
                        break;
                    case "FeeType":
                        int.TryParse(value, out _feetype);
                        break;
                    case "CostBalance":
                        decimal.TryParse(value, out _costbalance);
                        break;
                    case "DDFInitialBalance":
                        decimal.TryParse(value, out _ddfinitialbalance);
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
