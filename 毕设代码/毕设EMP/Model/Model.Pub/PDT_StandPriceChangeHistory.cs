// ===================================================================
// 文件： PDT_StandPriceChangeHistory.cs
// 项目名称：
// 创建时间：2013-10-08
// 作者:	   Jace
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.Pub
{
    /// <summary>
    ///PDT_StandPriceChangeHistory数据实体类
    /// </summary>
    [Serializable]
    public class PDT_StandPriceChangeHistory : IModel
    {
        #region 私有变量定义
        private int _id = 0;
        private int _standardprice = 0;
        private int _product = 0;
        private int _changetype = 0;
        private decimal _prefactoryprice = 0;
        private decimal _aftfactoryprice = 0;
        private decimal _pretradeoutprice = 0;
        private decimal _afttradeoutprice = 0;
        private decimal _pretradeinprice = 0;
        private decimal _afttradeinprice = 0;
        private decimal _prestdprice = 0;
        private decimal _aftstdprice = 0;
        private decimal _prerebateprice = 0;
        private decimal _aftrebateprice = 0;
        private decimal _predirebateprice = 0;
        private decimal _aftdirebateprice = 0;
        private DateTime _chagetime = new DateTime(1900, 1, 1);
        private int _changestaff = 0;
        private int _remark = 0;
        #endregion

        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public PDT_StandPriceChangeHistory()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public PDT_StandPriceChangeHistory(int id, int standardprice, int product, int changetype, decimal prefactoryprice, decimal aftfactoryprice, decimal pretradeoutprice, decimal afttradeoutprice, decimal pretradeinprice, decimal afttradeinprice, decimal prestdprice, decimal aftstdprice, decimal prerebateprice, decimal aftrebateprice, decimal predirebateprice, decimal aftdirebateprice, DateTime chagetime, int changestaff, int remark)
        {
            _id = id;
            _standardprice = standardprice;
            _product = product;
            _changetype = changetype;
            _prefactoryprice = prefactoryprice;
            _aftfactoryprice = aftfactoryprice;
            _pretradeoutprice = pretradeoutprice;
            _afttradeoutprice = afttradeoutprice;
            _pretradeinprice = pretradeinprice;
            _afttradeinprice = afttradeinprice;
            _prestdprice = prestdprice;
            _aftstdprice = aftstdprice;
            _prerebateprice = prerebateprice;
            _aftrebateprice = aftrebateprice;
            _predirebateprice = predirebateprice;
            _aftdirebateprice = aftdirebateprice;
            _chagetime = chagetime;
            _changestaff = changestaff;
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
        ///StandardPrice
        ///</summary>
        public int StandardPrice
        {
            get { return _standardprice; }
            set { _standardprice = value; }
        }

        ///<summary>
        ///Product
        ///</summary>
        public int Product
        {
            get { return _product; }
            set { _product = value; }
        }

        ///<summary>
        ///ChangeType
        ///</summary>
        public int ChangeType
        {
            get { return _changetype; }
            set { _changetype = value; }
        }

        ///<summary>
        ///PreFactoryPrice
        ///</summary>
        public decimal PreFactoryPrice
        {
            get { return _prefactoryprice; }
            set { _prefactoryprice = value; }
        }

        ///<summary>
        ///AftFactoryPrice
        ///</summary>
        public decimal AftFactoryPrice
        {
            get { return _aftfactoryprice; }
            set { _aftfactoryprice = value; }
        }

        ///<summary>
        ///PreTradeOutPrice
        ///</summary>
        public decimal PreTradeOutPrice
        {
            get { return _pretradeoutprice; }
            set { _pretradeoutprice = value; }
        }

        ///<summary>
        ///AftTradeOutPrice
        ///</summary>
        public decimal AftTradeOutPrice
        {
            get { return _afttradeoutprice; }
            set { _afttradeoutprice = value; }
        }

        ///<summary>
        ///PreTradeInPrice
        ///</summary>
        public decimal PreTradeInPrice
        {
            get { return _pretradeinprice; }
            set { _pretradeinprice = value; }
        }

        ///<summary>
        ///AftTradeInPrice
        ///</summary>
        public decimal AftTradeInPrice
        {
            get { return _afttradeinprice; }
            set { _afttradeinprice = value; }
        }

        ///<summary>
        ///PreStdPrice
        ///</summary>
        public decimal PreStdPrice
        {
            get { return _prestdprice; }
            set { _prestdprice = value; }
        }

        ///<summary>
        ///AftStdPrice
        ///</summary>
        public decimal AftStdPrice
        {
            get { return _aftstdprice; }
            set { _aftstdprice = value; }
        }

        ///<summary>
        ///PreRebatePrice
        ///</summary>
        public decimal PreRebatePrice
        {
            get { return _prerebateprice; }
            set { _prerebateprice = value; }
        }

        ///<summary>
        ///AftRebatePrice
        ///</summary>
        public decimal AftRebatePrice
        {
            get { return _aftrebateprice; }
            set { _aftrebateprice = value; }
        }

        ///<summary>
        ///PreDIRebatePrice
        ///</summary>
        public decimal PreDIRebatePrice
        {
            get { return _predirebateprice; }
            set { _predirebateprice = value; }
        }

        ///<summary>
        ///AftDIRebatePrice
        ///</summary>
        public decimal AftDIRebatePrice
        {
            get { return _aftdirebateprice; }
            set { _aftdirebateprice = value; }
        }

        ///<summary>
        ///ChageTime
        ///</summary>
        public DateTime ChageTime
        {
            get { return _chagetime; }
            set { _chagetime = value; }
        }

        ///<summary>
        ///ChangeStaff
        ///</summary>
        public int ChangeStaff
        {
            get { return _changestaff; }
            set { _changestaff = value; }
        }

        ///<summary>
        ///Remark
        ///</summary>
        public int Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }

        #endregion

        public string ModelName
        {
            get { return "PDT_StandPriceChangeHistory"; }
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
                    case "StandardPrice":
                        return _standardprice.ToString();
                    case "Product":
                        return _product.ToString();
                    case "ChangeType":
                        return _changetype.ToString();
                    case "PreFactoryPrice":
                        return _prefactoryprice.ToString();
                    case "AftFactoryPrice":
                        return _aftfactoryprice.ToString();
                    case "PreTradeOutPrice":
                        return _pretradeoutprice.ToString();
                    case "AftTradeOutPrice":
                        return _afttradeoutprice.ToString();
                    case "PreTradeInPrice":
                        return _pretradeinprice.ToString();
                    case "AftTradeInPrice":
                        return _afttradeinprice.ToString();
                    case "PreStdPrice":
                        return _prestdprice.ToString();
                    case "AftStdPrice":
                        return _aftstdprice.ToString();
                    case "PreRebatePrice":
                        return _prerebateprice.ToString();
                    case "AftRebatePrice":
                        return _aftrebateprice.ToString();
                    case "PreDIRebatePrice":
                        return _predirebateprice.ToString();
                    case "AftDIRebatePrice":
                        return _aftdirebateprice.ToString();
                    case "ChageTime":
                        return _chagetime.ToString();
                    case "ChangeStaff":
                        return _changestaff.ToString();
                    case "Remark":
                        return _remark.ToString();
                    default:
                        return "";
                }
            }
            set
            {
                switch (FieldName)
                {
                    case "ID":
                        int.TryParse(value, out _id);
                        break;
                    case "StandardPrice":
                        int.TryParse(value, out _standardprice);
                        break;
                    case "Product":
                        int.TryParse(value, out _product);
                        break;
                    case "ChangeType":
                        int.TryParse(value, out _changetype);
                        break;
                    case "PreFactoryPrice":
                        decimal.TryParse(value, out _prefactoryprice);
                        break;
                    case "AftFactoryPrice":
                        decimal.TryParse(value, out _aftfactoryprice);
                        break;
                    case "PreTradeOutPrice":
                        decimal.TryParse(value, out _pretradeoutprice);
                        break;
                    case "AftTradeOutPrice":
                        decimal.TryParse(value, out _afttradeoutprice);
                        break;
                    case "PreTradeInPrice":
                        decimal.TryParse(value, out _pretradeinprice);
                        break;
                    case "AftTradeInPrice":
                        decimal.TryParse(value, out _afttradeinprice);
                        break;
                    case "PreStdPrice":
                        decimal.TryParse(value, out _prestdprice);
                        break;
                    case "AftStdPrice":
                        decimal.TryParse(value, out _aftstdprice);
                        break;
                    case "PreRebatePrice":
                        decimal.TryParse(value, out _prerebateprice);
                        break;
                    case "AftRebatePrice":
                        decimal.TryParse(value, out _aftrebateprice);
                        break;
                    case "PreDIRebatePrice":
                        decimal.TryParse(value, out _predirebateprice);
                        break;
                    case "AftDIRebatePrice":
                        decimal.TryParse(value, out _aftdirebateprice);
                        break;
                    case "ChageTime":
                        DateTime.TryParse(value, out _chagetime);
                        break;
                    case "ChangeStaff":
                        int.TryParse(value, out _changestaff);
                        break;
                    case "Remark":
                        int.TryParse(value, out _remark);
                        break;

                }
            }
        }
        #endregion
    }
}
