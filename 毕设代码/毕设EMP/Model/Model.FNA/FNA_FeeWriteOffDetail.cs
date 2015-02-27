// ===================================================================
// 文件： FNA_FeeWriteOffDetail.cs
// 项目名称：
// 创建时间：2010/5/16
// 作者:	   
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.FNA
{
    /// <summary>
    ///FNA_FeeWriteOffDetail数据实体类
    /// </summary>
    [Serializable]
    public class FNA_FeeWriteOffDetail : IModel
    {
        #region 私有变量定义
        private int _id = 0;
        private int _writeoffid = 0;
        private int _applydetailid = 0;
        private int _client = 0;
        private int _accounttitle = 0;
        private int _productbrand = 0;
        private decimal _applycost = 0;
        private decimal _writeoffcost = 0;
        private int _balancemode = 0;
        private int _adjustmode = 0;
        private decimal _adjustcost = 0;
        private string _adjustreason = string.Empty;
        private int _beginmonth = 0;
        private int _endmonth = 0;
        private DateTime _begindate = new DateTime(1900, 1, 1);
        private DateTime _enddate = new DateTime(1900, 1, 1);
        private string _remark = string.Empty;

        private NameValueCollection _extpropertys;
        #endregion

        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public FNA_FeeWriteOffDetail()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public FNA_FeeWriteOffDetail(int id, int writeoffid, int applydetailid, int client, int accounttitle, int productbrand, decimal applycost, decimal writeoffcost, int balancemode, int adjustmode, decimal adjustcost, string adjustreason, int beginmonth, int endmonth, DateTime begindate, DateTime enddate, string remark)
        {
            _id = id;
            _writeoffid = writeoffid;
            _applydetailid = applydetailid;
            _client = client;
            _accounttitle = accounttitle;
            _productbrand = productbrand;
            _applycost = applycost;
            _writeoffcost = writeoffcost;
            _balancemode = balancemode;
            _adjustmode = adjustmode;
            _adjustcost = adjustcost;
            _adjustreason = adjustreason;
            _beginmonth = beginmonth;
            _endmonth = endmonth;
            _begindate = begindate;
            _enddate = enddate;
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
        ///费用申请单ID
        ///</summary>
        public int WriteOffID
        {
            get { return _writeoffid; }
            set { _writeoffid = value; }
        }

        ///<summary>
        ///费用申请明细ID
        ///</summary>
        public int ApplyDetailID
        {
            get { return _applydetailid; }
            set { _applydetailid = value; }
        }

        ///<summary>
        ///发生客户
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
        ///品牌
        ///</summary>
        public int ProductBrand
        {
            get { return _productbrand; }
            set { _productbrand = value; }
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
        ///核消金额
        ///</summary>
        public decimal WriteOffCost
        {
            get { return _writeoffcost; }
            set { _writeoffcost = value; }
        }

        ///<summary>
        ///结余方式
        ///</summary>
        public int BalanceMode
        {
            get { return _balancemode; }
            set { _balancemode = value; }
        }

        ///<summary>
        ///调整方式
        ///</summary>
        public int AdjustMode
        {
            get { return _adjustmode; }
            set { _adjustmode = value; }
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
        ///核销开始月
        ///</summary>
        public int BeginMonth
        {
            get { return _beginmonth; }
            set { _beginmonth = value; }
        }

        ///<summary>
        ///核销截止月
        ///</summary>
        public int EndMonth
        {
            get { return _endmonth; }
            set { _endmonth = value; }
        }

        ///<summary>
        ///核销开始日期
        ///</summary>
        public DateTime BeginDate
        {
            get { return _begindate; }
            set { _begindate = value; }
        }

        ///<summary>
        ///核销截止日期
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
            get { return "FNA_FeeWriteOffDetail"; }
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
                    case "WriteOffID":
                        return _writeoffid.ToString();
                    case "ApplyDetailID":
                        return _applydetailid.ToString();
                    case "Client":
                        return _client.ToString();
                    case "AccountTitle":
                        return _accounttitle.ToString();
                    case "ProductBrand":
                        return _productbrand.ToString();
                    case "ApplyCost":
                        return _applycost.ToString();
                    case "WriteOffCost":
                        return _writeoffcost.ToString();
                    case "BalanceMode":
                        return _balancemode.ToString();
                    case "AdjustMode":
                        return _adjustmode.ToString();
                    case "AdjustCost":
                        return _adjustcost.ToString();
                    case "AdjustReason":
                        return _adjustreason;
                    case "BeginMonth":
                        return _beginmonth.ToString();
                    case "EndMonth":
                        return _endmonth.ToString();
                    case "BeginDate":
                        return _begindate.ToShortDateString();
                    case "EndDate":
                        return _enddate.ToShortDateString();
                    case "Remark":
                        return _remark;
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
                    case "WriteOffID":
                        int.TryParse(value, out _writeoffid);
                        break;
                    case "ApplyDetailID":
                        int.TryParse(value, out _applydetailid);
                        break;
                    case "Client":
                        int.TryParse(value, out _client);
                        break;
                    case "AccountTitle":
                        int.TryParse(value, out _accounttitle);
                        break;
                    case "ProductBrand":
                        int.TryParse(value, out _productbrand);
                        break;
                    case "ApplyCost":
                        decimal.TryParse(value, out _applycost);
                        break;
                    case "WriteOffCost":
                        decimal.TryParse(value, out _writeoffcost);
                        break;
                    case "BalanceMode":
                        int.TryParse(value, out _balancemode);
                        break;
                    case "AdjustMode":
                        int.TryParse(value, out _adjustmode);
                        break;
                    case "AdjustCost":
                        decimal.TryParse(value, out _adjustcost);
                        break;
                    case "AdjustReason":
                        _adjustreason = value;
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
