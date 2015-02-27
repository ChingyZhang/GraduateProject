using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using MCSFramework.Model.Logistics;
using MCSFramework.SQLDAL.Logistics;
using MCSFramework.BLL.Pub;
using MCSFramework.Model.Pub;

namespace MCSFramework.BLL.Logistics
{
    [Serializable]
    public class ORD_OrderCartBLL
    {
        private int _publish = 0;
        private int _organizecity = 0;
        private int _client = 0;
        private int _accountmonth = 0;
        private int _type = 1;              //默认为成品
        private int _brand = 0;
        private int _isspecial = 0;
        private int _ordertype = 0;
        private int _giftclassify = 1;      //赠品费用类别
        private int _giftfeetype = 6;       //赠品费用类型
        private int _addressid;//地址ID
        private int _receiver;

        private IList<ORD_OrderCart> _items;

        public int Publish
        {
            get { return _publish; }
            set { _publish = value; }
        }

        public int OrganizeCity
        {
            get { return _organizecity; }
            set { _organizecity = value; }
        }

        public int Client
        {
            get { return _client; }
            set { _client = value; }
        }

        public int AccountMonth
        {
            get { return _accountmonth; }
            set { _accountmonth = value; }
        }

        public int AddressID
        {
            get { return _addressid; }
            set { _addressid = value; }
        }
        /// <summary>
        /// 发布单类型 1:成品 2:促销品，只读
        /// </summary>
        public int Type
        {
            get { return _type; }
        }
        public int Brand
        {
            get { return _brand; }
            set { _brand = value; }
        }
        public int IsSpecial
        {
            get { return _isspecial; }
            set { _isspecial = value; }
        }
        public int OrderType
        {
            get { return _ordertype; }
            set { _ordertype = value; }
        }

        /// <summary>
        /// 赠品费用类别 1:有导赠品 2:无导赠品
        /// </summary>
        public int GiftClassify
        {
            get { return _giftclassify; }
        }
        /// <summary>
        /// 赠品预算对应的费用类型
        /// </summary>
        public int GiftFeeType
        {
            get { return _giftfeetype; }
        }
        /// <summary>
        /// 收货客户
        /// </summary>
        public int Receiver
        {
            get { return _receiver; }
            set { _receiver = value; }
        }

        public IList<ORD_OrderCart> Items
        {
            get { return _items; }
            set { _items = value; }
        }

        public ORD_OrderCartBLL(int publish, int organizecity, int client, int month,int addressid)
        {
            _organizecity = organizecity;
            _client = client;
            _accountmonth = month;
            _addressid = addressid;
            if (publish != 0)
            {
                _publish = publish;

                ORD_ApplyPublishBLL p = new ORD_ApplyPublishBLL(publish);
                _type = p.Model.Type;
                _giftfeetype = p.Model.FeeType;
                int.TryParse(p.Model["ProductBrand"], out _brand);
                int.TryParse(p.Model["GiftClassify"], out _giftclassify);
            }

            _items = new List<ORD_OrderCart>();
        }

        public ORD_OrderCartBLL(int publish, int organizecity, int client, int month, int addressid,int receiver,string useless)
        {
            _organizecity = organizecity;
            _client = client;
            _accountmonth = month;
            _addressid = addressid;
            _receiver = receiver;
            if (publish != 0)
            {
                _publish = publish;

                ORD_ApplyPublishBLL p = new ORD_ApplyPublishBLL(publish);
                _type = p.Model.Type;
                _giftfeetype = p.Model.FeeType;
                int.TryParse(p.Model["ProductBrand"], out _brand);
                int.TryParse(p.Model["GiftClassify"], out _giftclassify);
            }

            _items = new List<ORD_OrderCart>();
        }

        public ORD_OrderCartBLL(int publish, int organizecity, int ordertype, int brand, int issepecial, int client)
        {
            ORD_ApplyPublishBLL p = new ORD_ApplyPublishBLL(publish);
            _type = p.Model.Type;
            _brand = brand;
            _isspecial = issepecial;
            _ordertype = ordertype;
            _publish = publish;
            _organizecity = organizecity;
            _client = client;            
            _items = new List<ORD_OrderCart>();
        }

        /// <summary>
        /// 向购物车中新增产品
        /// </summary>
        /// <param name="product"></param>
        /// <param name="price"></param>
        /// <returns>0：成功 -1：发布目录中不包括此产品 -2：已超可请购数量 -3：该产品已在购物车中 -4：该产品不在指定客户的价表中</returns>
        public int AddProduct(int product, decimal price)
        {
            if (Items.FirstOrDefault(m => m.Product == product) != null) return -3;

            ORD_ApplyPublishDetail _m = new ORD_ApplyPublishBLL(_publish).Items.FirstOrDefault(m => m.Product == product);

            if (_m != null)
            {
                ORD_OrderCart cartitem = new ORD_OrderCart();
                cartitem.Product = _m.Product;
                cartitem.MinQuantity = _m.MinQuantity;
                cartitem.MaxQuantity = _m.MaxQuantity;
                cartitem.Price = price;
                cartitem.AvailableQuantity = _m.AvailableQuantity;
                cartitem.SubmitQuantity = ORD_OrderApplyBLL.GetSubmitQuantity(_publish, product);

                cartitem.BookQuantity = cartitem.MinQuantity > 0 ? cartitem.MinQuantity : new PDT_ProductBLL(product).Model.ConvertFactor;

                if (cartitem.AvailableQuantity > 0 && cartitem.BookQuantity > cartitem.AvailableQuantity - cartitem.SubmitQuantity) return -2;

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
            ORD_OrderCart cartitem = Items.FirstOrDefault(m => m.Product == product);
            if (cartitem != null) Items.Remove(cartitem);

            return 0;
        }

        /// <summary>
        /// 修改请购买数量
        /// </summary>
        /// <param name="product"></param>
        /// <param name="quantity"></param>
        /// <returns>0：成功 -1：购物车中不包括此产品 -2：已超可请购数量 -3：小于最小单次请购数量 -4：大于最大单次请购数量</returns>
        public int ModifyQuantity(int product, int quantity)
        {
            ORD_OrderCart cartitem = Items.FirstOrDefault(m => m.Product == product);
            if (cartitem == null) return -1;

            cartitem.AvailableQuantity = cartitem.AvailableQuantity;
            cartitem.SubmitQuantity = ORD_OrderApplyBLL.GetSubmitQuantity(_publish, product);

            if (cartitem.AvailableQuantity > 0 && quantity > cartitem.AvailableQuantity - cartitem.SubmitQuantity) return -2;
            if (cartitem.MinQuantity > 0 && cartitem.MinQuantity > quantity || quantity < 0) return -3;
            if (cartitem.MaxQuantity > 0 && cartitem.MaxQuantity < quantity) return -4;

            cartitem.BookQuantity = quantity;

            return 0;
        }

        /// <summary>
        /// 根据请购买申请单重新初始化购物车
        /// </summary>
        /// <param name="ApplyID"></param>
        /// <returns></returns>
        public static ORD_OrderCartBLL InitByOrderApply(int ApplyID)
        {
            ORD_OrderApplyBLL apply = new ORD_OrderApplyBLL(ApplyID);
            if (apply.Model == null) return null;

            ORD_ApplyPublishBLL publish = new ORD_ApplyPublishBLL(apply.Model.PublishID);
            if (publish.Model == null) return null;
            int addressid = 0;
            int.TryParse(apply.Model["AddressID"], out addressid);
            ORD_OrderCartBLL cart = new ORD_OrderCartBLL(apply.Model.PublishID, apply.Model.OrganizeCity, apply.Model.Client, apply.Model.AccountMonth, addressid);

            foreach (ORD_OrderApplyDetail item in apply.Items)
            {
                ORD_ApplyPublishDetail _m = publish.Items.FirstOrDefault(m => m.Product == item.Product);
                if (_m == null) continue;

                ORD_OrderCart cartitem = new ORD_OrderCart();
                cartitem.ID = _m.ID;
                cartitem.Product = _m.Product;
                cartitem.MinQuantity = _m.MinQuantity;
                cartitem.MaxQuantity = _m.MaxQuantity;
                cartitem.Price = item.Price;
                cartitem.AvailableQuantity = _m.AvailableQuantity;
                cartitem.SubmitQuantity = ORD_OrderApplyBLL.GetSubmitQuantity(cart.Publish, cartitem.Product);

                cartitem.BookQuantity = item.BookQuantity;

                cart.Items.Add(cartitem);
            }

            return cart;
        }
    }
}
