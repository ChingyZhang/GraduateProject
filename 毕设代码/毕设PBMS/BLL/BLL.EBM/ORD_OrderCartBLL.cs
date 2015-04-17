using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using MCSFramework.Model.EBM;
using MCSFramework.SQLDAL.EBM;
using MCSFramework.BLL.Pub;
using MCSFramework.Model.Pub;

namespace MCSFramework.BLL.EBM
{
    [Serializable]
    public class ORD_OrderCartBLL
    {
        private int _accountmonth = 0;
        private int _publish = 0;
        private int _supplier = 0;
        private int _client = 0;
        private int _paymode = 0;
        private int _type = 1;              //默认为成品
        private int _classify = 2;          //默认为非配额
        private int _reqwarehose = 0;
        private DateTime _reqarrivaldate = new DateTime(1900, 1, 1);

        private IList<ORD_OrderCartItem> _items;

        /// <summary>
        /// 订货月份
        /// </summary>
        public int AccountMonth
        {
            get { return _accountmonth; }
            set { _accountmonth = value; }
        }

        /// <summary>
        /// 发布目录
        /// </summary>
        public int Publish
        {
            get { return _publish; }
            set { _publish = value; }
        }

        /// <summary>
        /// 供货商
        /// </summary>
        public int Supplier
        {
            get { return _supplier; }
            set { _supplier = value; }
        }

        /// <summary>
        /// 请购客户
        /// </summary>
        public int Client
        {
            get { return _client; }
            set { _client = value; }
        }

        /// <summary>
        /// 支付方式 1:现金 2:转账 8:积分
        /// </summary>
        public int PayMode
        {
            get { return _paymode; }
            set { _paymode = value; }
        }

        /// <summary>
        /// 订单类型 1:成品 2:赠品
        /// </summary>
        public int Type
        {
            get { return _type; }
            set { _type = value; }
        }

        /// <summary>
        /// 订单类别 1:配额订单 2:非配额订单
        /// </summary>
        public int Classify
        {
            get { return _classify; }
            set { _classify = value; }
        }

        /// <summary>
        /// 要求到货仓库
        /// </summary>
        public int ReqWarehouse
        {
            get { return _reqwarehose; }
            set { _reqwarehose = value; }
        }
        /// <summary>
        /// 要求到货日期
        /// </summary>
        public DateTime ReqArrivalDate
        {
            get { return _reqarrivaldate; }
            set { _reqarrivaldate = value; }
        }

        public IList<ORD_OrderCartItem> Items
        {
            get { return _items; }
            set { _items = value; }
        }

        public ORD_OrderCartBLL(int publish, int client, int supplierid, int paymode, int reqwarehouse, DateTime reqarrivaldate)
        {
            new ORD_OrderCartBLL(0, publish, client, supplierid, 2, paymode, reqwarehouse, reqarrivaldate);
        }
        public ORD_OrderCartBLL(int accountmonth, int publish, int client, int supplierid, int classify, int paymode, int reqwarehouse, DateTime reqarrivaldate)
        {
            _accountmonth = accountmonth;
            _publish = publish;
            _client = client;
            _classify = classify;
            _paymode = paymode;
            _reqwarehose = reqwarehouse;
            _reqarrivaldate = reqarrivaldate;

            if (publish != 0)
            {
                ORD_PublishBLL p = new ORD_PublishBLL(publish);
                _supplier = supplierid;
                _type = p.Model.Type;
            }

            _items = new List<ORD_OrderCartItem>();
        }



        /// <summary>
        /// 向购物车中新增产品
        /// </summary>
        /// <param name="product"></param>
        /// <returns>0：成功 -1：发布目录中不包括此产品 -2：已超可请购数量 -3：该产品已在购物车中 -4:超订货配额</returns>
        public int AddProduct(int product, int bookquantity)
        {
            if (Items.FirstOrDefault(p => p.Product == product) != null) return -3;

            ORD_PublishDetail _m = new ORD_PublishBLL(_publish).Items.FirstOrDefault(m => m.Product == product);

            if (_m != null)
            {
                ORD_OrderCartItem cartitem = new ORD_OrderCartItem();
                cartitem.Product = _m.Product;
                cartitem.MinQuantity = _m.MinQuantity;
                cartitem.MaxQuantity = _m.MaxQuantity;
                cartitem.Price = _m.Price;
                cartitem.Points = _m.Points;
                cartitem.AvailableQuantity = _m.AvailableQuantity;
                cartitem.SubmitQuantity = ORD_OrderBLL.GetSubmitQuantity(_accountmonth, _client, product, _classify == 1 ? 1 : 0);

                if (bookquantity == 0)
                    cartitem.BookQuantity = cartitem.MinQuantity > 0 ? cartitem.MinQuantity : new PDT_ProductBLL(product).Model.ConvertFactor;
                else
                    cartitem.BookQuantity = bookquantity;
                cartitem.ConfirmQuantity = cartitem.BookQuantity;

                #region 判断是否超过发布目录设定的可订货数量
                if (cartitem.AvailableQuantity > 0)
                {
                    int totalquantity = ORD_PublishBLL.GetSubmitQuantity(_publish, product);
                    if (cartitem.BookQuantity > cartitem.AvailableQuantity - totalquantity) return -2;
                }
                #endregion

                #region 获取配额，判断是否超过配额
                if (_classify == 1)
                {
                    //常规订单，按配额订货
                    cartitem.QuotaQuantity = 0;
                    int quotaid = ORD_QuotaBLL.GetQuotaByClientAndMonth(_client, _accountmonth);
                    ORD_QuotaBLL quota = new ORD_QuotaBLL(quotaid);
                    if (quota != null)
                    {
                        ORD_QuotaDetail d = quota.Items.FirstOrDefault(p => p.Product == product);
                        if (d != null) cartitem.QuotaQuantity = d.StdQuota + d.AdjQuota;
                    }

                    if (cartitem.BookQuantity > cartitem.QuotaQuantity - cartitem.SubmitQuantity) return -4;
                }
                #endregion

                _items.Add(cartitem);

                return 0;
            }
            return -1;
        }

        /// <summary>
        /// 从购物车中移除产品
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public int RemoveProduct(int product)
        {
            ORD_OrderCartItem cartitem = Items.FirstOrDefault(m => m.Product == product);
            if (cartitem != null) Items.Remove(cartitem);

            return 0;
        }

        /// <summary>
        /// 修改请购买数量
        /// </summary>
        /// <param name="product"></param>
        /// <param name="quantity"></param>
        /// <returns>0：成功 -1：购物车中不包括此产品 -2：已超可请购数量 -3：小于最小单次请购数量 -4：大于最大单次请购数量 -5:超订货配额</returns>
        public int ModifyQuantity(int product, int quantity)
        {
            ORD_OrderCartItem cartitem = Items.FirstOrDefault(m => m.Product == product);
            if (cartitem == null) return -1;

            cartitem.AvailableQuantity = cartitem.AvailableQuantity;
            cartitem.SubmitQuantity = ORD_OrderBLL.GetSubmitQuantity(_accountmonth, _client, product, _classify == 1 ? 1 : 0);

            if (cartitem.MinQuantity > 0 && cartitem.MinQuantity > quantity || quantity < 0) return -3;
            if (cartitem.MaxQuantity > 0 && cartitem.MaxQuantity < quantity) return -4;

            #region 判断是否超过发布目录设定的可订货数量
            if (cartitem.AvailableQuantity > 0)
            {
                int totalquantity = ORD_PublishBLL.GetSubmitQuantity(_publish, product);
                if (cartitem.BookQuantity > cartitem.AvailableQuantity - totalquantity) return -2;
            }
            #endregion

            if (_classify == 1 && quantity > cartitem.QuotaQuantity - cartitem.SubmitQuantity) return -5;

            cartitem.BookQuantity = quantity;
            cartitem.ConfirmQuantity = quantity;
            return 0;
        }

        /// <summary>
        /// 根据请购买申请单重新初始化购物车
        /// </summary>
        /// <param name="ApplyID"></param>
        /// <returns></returns>
        public static ORD_OrderCartBLL InitByOrderApply(int OrderID)
        {
            ORD_OrderBLL Order = new ORD_OrderBLL(OrderID);
            if (Order.Model == null) return null;

            ORD_PublishBLL publish = new ORD_PublishBLL(Order.Model.PublishID);
            if (publish.Model == null) return null;

            ORD_OrderCartBLL cart = new ORD_OrderCartBLL(Order.Model.AccountMonth, Order.Model.PublishID, Order.Model.Client, Order.Model.Supplier,
                Order.Model.Classify, Order.Model.PayMode, Order.Model.ReqWarehouse, Order.Model.ReqArrivalDate);

            int quotaid = ORD_QuotaBLL.GetQuotaByClientAndMonth(cart.Client, cart.AccountMonth);
            ORD_QuotaBLL quota = new ORD_QuotaBLL(quotaid);

            foreach (ORD_OrderDetail item in Order.Items)
            {
                ORD_PublishDetail _m = publish.Items.FirstOrDefault(m => m.Product == item.Product);
                if (_m == null) continue;

                ORD_OrderCartItem cartitem = new ORD_OrderCartItem();
                cartitem.ID = _m.ID;
                cartitem.Product = _m.Product;
                cartitem.MinQuantity = _m.MinQuantity;
                cartitem.MaxQuantity = _m.MaxQuantity;
                cartitem.Price = item.Price;
                cartitem.AvailableQuantity = _m.AvailableQuantity;
                cartitem.SubmitQuantity = ORD_OrderBLL.GetSubmitQuantity(Order.Model.AccountMonth, Order.Model.Client, _m.Product, Order.Model.Classify == 1 ? 1 : 0);
                cartitem.BookQuantity = item.BookQuantity;
                cartitem.Points = item.Points;

                #region 获取配额数量
                if (quota.Model != null)
                {
                    ORD_QuotaDetail d = quota.Items.FirstOrDefault(p => p.Product == _m.Product);
                    if (d != null) cartitem.QuotaQuantity = d.StdQuota + d.AdjQuota;
                }
                #endregion

                cart.Items.Add(cartitem);
            }

            return cart;
        }
    }
}
