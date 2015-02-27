// ===================================================================
// 文件： FNA_Budget.cs
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
    ///FNA_Budget数据实体类
    /// </summary>
    [Serializable]
    public class FNA_Budget : IModel
    {
        #region 私有变量定义
        private int _id = 0;
        private int _accountmonth = 0;
        private int _organizecity = 0;
        private int _budgettype = 0;
        private int _feetype = 0;
        private decimal _budgetamount = 0;
        private string _remark = string.Empty;
        private int _approveflag = 0;
        private DateTime _inserttime = new DateTime(1900, 1, 1);
        private int _insertstaff = 0;
        private DateTime _updatetime = new DateTime(1900, 1, 1);
        private int _updatestaff = 0;
        private string _extproperty = string.Empty;
        private NameValueCollection _extpropertys;
        #endregion

        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public FNA_Budget()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public FNA_Budget(int id, int accountmonth, int organizecity, int budgettype, int feetype, decimal budgetamount, string remark, int approveflag, DateTime inserttime, int insertstaff, DateTime updatetime, int updatestaff, string extproperty)
        {
            _id = id;
            _accountmonth = accountmonth;
            _organizecity = organizecity;
            _budgettype = budgettype;
            _feetype = feetype;
            _budgetamount = budgetamount;
            _remark = remark;
            _approveflag = approveflag;
            _inserttime = inserttime;
            _insertstaff = insertstaff;
            _updatetime = updatetime;
            _updatestaff = updatestaff;
            _extproperty = extproperty;

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
        ///预算类型

        ///</summary>
        public int BudgetType
        {
            get { return _budgettype; }
            set { _budgettype = value; }
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
        ///预算额度
        ///</summary>
        public decimal BudgetAmount
        {
            get { return _budgetamount; }
            set { _budgetamount = value; }
        }

        ///<summary>
        ///备注
        ///</summary>
        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
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
        ///扩展属性
        ///</summary>
        public string ExtProperty
        {
            get { return _extproperty; }
            set { _extproperty = value; }
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
            get { return "FNA_Budget"; }
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
                    case "BudgetType":
                        return _budgettype.ToString();
                    case "FeeType":
                        return _feetype.ToString();
                    case "BudgetAmount":
                        return _budgetamount.ToString();
                    case "Remark":
                        return _remark;
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
                    case "ExtProperty":
                        return _extproperty;
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
                    case "BudgetType":
                        int.TryParse(value, out _budgettype);
                        break;
                    case "FeeType":
                        int.TryParse(value, out _feetype);
                        break;
                    case "BudgetAmount":
                        decimal.TryParse(value, out _budgetamount);
                        break;
                    case "Remark":
                        _remark = value;
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
                    case "ExtProperty":
                        _extproperty = value;
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
