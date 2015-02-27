// ===================================================================
// 文件： CAT_FeeApplyDetail.cs
// 项目名称：
// 创建时间：2012/8/13
// 作者:	   chf
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.CAT
{
    /// <summary>
    ///CAT_FeeApplyDetail数据实体类
    /// </summary>
    [Serializable]
    public class CAT_FeeApplyDetail : IModel
    {
        #region 私有变量定义
        private int _id = 0;
        private int _activity = 0;
        private int _client = 0;
        private int _accounttitle = 0;
        private decimal _applycost = 0;
        private decimal _adjustcost = 0;
        private string _adjustreason = string.Empty;
        private int _flag = 0;
        private int _beginmonth = 0;
        private int _endmonth = 0;
        private DateTime _begindate = new DateTime(1900, 1, 1);
        private DateTime _enddate = new DateTime(1900, 1, 1);
        private string _remark = string.Empty;
        private decimal _availcost = 0;
        private decimal _cancelcost = 0;

        private decimal _dicost = 0;
        private decimal _diadjustcost = 0;
        private int _relatecontractdetail = 0;
        private decimal _salesforcast = 0;
        private int _lastwriteoffmonth = 0;
        private NameValueCollection _extpropertys;
        #endregion

        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public CAT_FeeApplyDetail()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public CAT_FeeApplyDetail(int id, int activity, int client, int accounttitle, decimal applycost, decimal adjustcost, string adjustreason, int flag, int beginmonth, int endmonth, DateTime begindate, DateTime enddate, string remark, decimal availcost, decimal cancelcost, decimal dicost, decimal diadjustcost, int relatecontractdetail, decimal salesforcast, int lastwriteoffmonth)
        {
            _id = id;
            _activity = activity;
            _client = client;
            _accounttitle = accounttitle;
            _applycost = applycost;
            _adjustcost = adjustcost;
            _adjustreason = adjustreason;
            _flag = flag;
            _beginmonth = beginmonth;
            _endmonth = endmonth;
            _begindate = begindate;
            _enddate = enddate;
            _remark = remark;
            _availcost = availcost;
            _cancelcost = cancelcost;
            _dicost = dicost;
            _diadjustcost = diadjustcost;
            _relatecontractdetail = relatecontractdetail;
            _salesforcast = salesforcast;
            _lastwriteoffmonth = lastwriteoffmonth;

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
        ///费用申请单ID
        ///</summary>
        public int Activity
        {
            get { return _activity; }
            set { _activity = value; }
        }

        ///<summary>
        ///Client
        ///</summary>
        public int Client
        {
            get { return _client; }
            set { _client = value; }
        }

        ///<summary>
        ///会计科目
        ///</summary>
        public int AccountTitle
        {
            get { return _accounttitle; }
            set { _accounttitle = value; }
        }

        ///<summary>
        ///申请金额
        ///</summary>
        public decimal ApplyCost
        {
            get { return _applycost; }
            set { _applycost = value; }
        }

        ///<summary>
        ///调整金额
        ///</summary>
        public decimal AdjustCost
        {
            get { return _adjustcost; }
            set { _adjustcost = value; }
        }

        ///<summary>
        ///调整原因
        ///</summary>
        public string AdjustReason
        {
            get { return _adjustreason; }
            set { _adjustreason = value; }
        }

        ///<summary>
        ///标志
        ///</summary>
        public int Flag
        {
            get { return _flag; }
            set { _flag = value; }
        }

        ///<summary>
        ///费用开始月
        ///</summary>
        public int BeginMonth
        {
            get { return _beginmonth; }
            set { _beginmonth = value; }
        }

        ///<summary>
        ///费用截止月
        ///</summary>
        public int EndMonth
        {
            get { return _endmonth; }
            set { _endmonth = value; }
        }

        ///<summary>
        ///开始日期
        ///</summary>
        public DateTime BeginDate
        {
            get { return _begindate; }
            set { _begindate = value; }
        }

        ///<summary>
        ///截止日期
        ///</summary>
        public DateTime EndDate
        {
            get { return _enddate; }
            set { _enddate = value; }
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
        ///可用金额
        ///</summary>
        public decimal AvailCost
        {
            get { return _availcost; }
            set { _availcost = value; }
        }

        ///<summary>
        ///取消金额
        ///</summary>
        public decimal CancelCost
        {
            get { return _cancelcost; }
            set { _cancelcost = value; }
        }

        ///<summary>
        ///经销商承担金额
        ///</summary>
        public decimal DICost
        {
            get { return _dicost; }
            set { _dicost = value; }
        }

        ///<summary>
        ///经销商调整金额
        ///</summary>
        public decimal DIAdjustCost
        {
            get { return _diadjustcost; }
            set { _diadjustcost = value; }
        }

        ///<summary>
        ///关联合同明细ID
        ///</summary>
        public int RelateContractDetail
        {
            get { return _relatecontractdetail; }
            set { _relatecontractdetail = value; }
        }

        ///<summary>
        ///预计销量
        ///</summary>
        public decimal SalesForcast
        {
            get { return _salesforcast; }
            set { _salesforcast = value; }
        }

        ///<summary>
        ///最迟核销月
        ///</summary>
        public int LastWriteOffMonth
        {
            get { return _lastwriteoffmonth; }
            set { _lastwriteoffmonth = value; }
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
            get { return "CAT_FeeApplyDetail"; }
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
                    case "Activity":
                        return _activity.ToString();
                    case "Client":
                        return _client.ToString();
                    case "AccountTitle":
                        return _accounttitle.ToString();
                    case "ApplyCost":
                        return _applycost.ToString();
                    case "AdjustCost":
                        return _adjustcost.ToString();
                    case "AdjustReason":
                        return _adjustreason;
                    case "Flag":
                        return _flag.ToString();
                    case "BeginMonth":
                        return _beginmonth.ToString();
                    case "EndMonth":
                        return _endmonth.ToString();
                    case "BeginDate":
                        return _begindate.ToString();
                    case "EndDate":
                        return _enddate.ToString();
                    case "Remark":
                        return _remark;
                    case "AvailCost":
                        return _availcost.ToString();
                    case "CancelCost":
                        return _cancelcost.ToString();
                    case "DICost":
                        return _dicost.ToString();
                    case "DIAdjustCost":
                        return _diadjustcost.ToString();
                    case "RelateContractDetail":
                        return _relatecontractdetail.ToString();
                    case "SalesForcast":
                        return _salesforcast.ToString();
                    case "LastWriteOffMonth":
                        return _lastwriteoffmonth.ToString();
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
                    case "Activity":
                        int.TryParse(value, out _activity);
                        break;
                    case "Client":
                        int.TryParse(value, out _client);
                        break;
                    case "AccountTitle":
                        int.TryParse(value, out _accounttitle);
                        break;
                    case "ApplyCost":
                        decimal.TryParse(value, out _applycost);
                        break;
                    case "AdjustCost":
                        decimal.TryParse(value, out _adjustcost);
                        break;
                    case "AdjustReason":
                        _adjustreason = value;
                        break;
                    case "Flag":
                        int.TryParse(value, out _flag);
                        break;
                    case "BeginMonth":
                        int.TryParse(value, out _beginmonth);
                        break;
                    case "EndMonth":
                        int.TryParse(value, out _endmonth);
                        break;
                    case "BeginDate":
                        DateTime.TryParse(value, out _begindate);
                        break;
                    case "EndDate":
                        DateTime.TryParse(value, out _enddate);
                        break;
                    case "Remark":
                        _remark = value;
                        break;
                    case "AvailCost":
                        decimal.TryParse(value, out _availcost);
                        break;
                    case "CancelCost":
                        decimal.TryParse(value, out _cancelcost);
                        break;
                    case "DICost":
                        decimal.TryParse(value, out _dicost);
                        break;
                    case "DIAdjustCost":
                        decimal.TryParse(value, out _diadjustcost);
                        break;
                    case "RelateContractDetail":
                        int.TryParse(value, out _relatecontractdetail);
                        break;
                    case "SalesForcast":
                        decimal.TryParse(value, out _salesforcast);
                        break;
                    case "LastWriteOffMonth":
                        int.TryParse(value, out _lastwriteoffmonth);
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
