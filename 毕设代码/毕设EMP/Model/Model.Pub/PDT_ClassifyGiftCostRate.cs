// ===================================================================
// 文件： PDT_ClassifyGiftCostRate.cs
// 项目名称：
// 创建时间：2013/8/26
// 作者:	   chf
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.Pub
{
    /// <summary>
    ///PDT_ClassifyGiftCostRate数据实体类
    /// </summary>
    [Serializable]
    public class PDT_ClassifyGiftCostRate : IModel
    {
        #region 私有变量定义
        private int _id = 0;
        private int _beginmonth = 0;
        private int _cycle = 0;
        private int _organizecity = 0;
        private int _pdtbrand = 0;
        private int _client = 0;
        private int _giftcostclassify = 0;
        private decimal _giftcostrate = 0;
        private string _enabled = string.Empty;
        private string _remark = string.Empty;
        private int _approveflag = 0;
        private DateTime _inserttime = new DateTime(1900, 1, 1);
        private int _insertstaff = 0;
        private DateTime _updatetime = new DateTime(1900, 1, 1);
        private int _updatestaff = 0;

        private NameValueCollection _extpropertys;
        #endregion

        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public PDT_ClassifyGiftCostRate()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public PDT_ClassifyGiftCostRate(int id, int beginmonth, int cycle, int organizecity, int pdtbrand, int client, int giftcostclassify, decimal giftcostrate, string enabled, string remark, int approveflag, DateTime inserttime, int insertstaff, DateTime updatetime, int updatestaff)
        {
            _id = id;
            _beginmonth = beginmonth;
            _cycle = cycle;
            _organizecity = organizecity;
            _pdtbrand = pdtbrand;
            _client = client;
            _giftcostclassify = giftcostclassify;
            _giftcostrate = giftcostrate;
            _enabled = enabled;
            _remark = remark;
            _approveflag = approveflag;
            _inserttime = inserttime;
            _insertstaff = insertstaff;
            _updatetime = updatetime;
            _updatestaff = updatestaff;

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
        ///BeginMonth
        ///</summary>
        public int BeginMonth
        {
            get { return _beginmonth; }
            set { _beginmonth = value; }
        }

        ///<summary>
        ///Cycle
        ///</summary>
        public int Cycle
        {
            get { return _cycle; }
            set { _cycle = value; }
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
        ///产品品牌
        ///</summary>
        public int PDTBrand
        {
            get { return _pdtbrand; }
            set { _pdtbrand = value; }
        }

        ///<summary>
        ///经销商
        ///</summary>
        public int Client
        {
            get { return _client; }
            set { _client = value; }
        }

        ///<summary>
        ///赠品费用类别
        ///</summary>
        public int GiftCostClassify
        {
            get { return _giftcostclassify; }
            set { _giftcostclassify = value; }
        }

        ///<summary>
        ///赠品费用
        ///</summary>
        public decimal GiftCostRate
        {
            get { return _giftcostrate; }
            set { _giftcostrate = value; }
        }

        ///<summary>
        ///启用标志
        ///</summary>
        public string Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
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
            get { return "PDT_ClassifyGiftCostRate"; }
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
                    case "BeginMonth":
                        return _beginmonth.ToString();
                    case "Cycle":
                        return _cycle.ToString();
                    case "OrganizeCity":
                        return _organizecity.ToString();
                    case "PDTBrand":
                        return _pdtbrand.ToString();
                    case "Client":
                        return _client.ToString();
                    case "GiftCostClassify":
                        return _giftcostclassify.ToString();
                    case "GiftCostRate":
                        return _giftcostrate.ToString();
                    case "Enabled":
                        return _enabled;
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
                    case "BeginMonth":
                        int.TryParse(value, out _beginmonth);
                        break;
                    case "Cycle":
                        int.TryParse(value, out _cycle);
                        break;
                    case "OrganizeCity":
                        int.TryParse(value, out _organizecity);
                        break;
                    case "PDTBrand":
                        int.TryParse(value, out _pdtbrand);
                        break;
                    case "Client":
                        int.TryParse(value, out _client);
                        break;
                    case "GiftCostClassify":
                        int.TryParse(value, out _giftcostclassify);
                        break;
                    case "GiftCostRate":
                        decimal.TryParse(value, out _giftcostrate);
                        break;
                    case "Enabled":
                        _enabled = value;
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
