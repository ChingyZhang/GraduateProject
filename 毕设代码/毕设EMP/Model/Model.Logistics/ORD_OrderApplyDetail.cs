// ===================================================================
// 文件： ORD_OrderApplyDetail.cs
// 项目名称：
// 创建时间：2010/6/8
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.Logistics
{
    /// <summary>
    ///ORD_OrderApplyDetail数据实体类
    /// </summary>
    [Serializable]
    public class ORD_OrderApplyDetail : IModel
    {
        #region 私有变量定义
        private int _id = 0;
        private int _applyid = 0;
        private int _product = 0;
        private decimal _price = 0;
        private int _bookquantity = 0;
        private int _adjustquantity = 0;
        private string _adjustreason = string.Empty;
        private int _deliveryquantity = 0;
        private string _remark = string.Empty;

        private NameValueCollection _extpropertys;
        #endregion

        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public ORD_OrderApplyDetail()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public ORD_OrderApplyDetail(int id, int applyid, int product, decimal price, int bookquantity, int adjustquantity, string adjustreason, int deliveryquantity, string remark)
        {
            _id = id;
            _applyid = applyid;
            _product = product;
            _price = price;
            _bookquantity = bookquantity;
            _adjustquantity = adjustquantity;
            _adjustreason = adjustreason;
            _deliveryquantity = deliveryquantity;
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
        ///ApplyID
        ///</summary>
        public int ApplyID
        {
            get { return _applyid; }
            set { _applyid = value; }
        }

        ///<summary>
        ///产品
        ///</summary>
        public int Product
        {
            get { return _product; }
            set { _product = value; }
        }

        ///<summary>
        ///价格
        ///</summary>
        public decimal Price
        {
            get { return _price; }
            set { _price = value; }
        }

        ///<summary>
        ///请购数量
        ///</summary>
        public int BookQuantity
        {
            get { return _bookquantity; }
            set { _bookquantity = value; }
        }

        ///<summary>
        ///调整数量
        ///</summary>
        public int AdjustQuantity
        {
            get { return _adjustquantity; }
            set { _adjustquantity = value; }
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
        ///已发放数量
        ///</summary>
        public int DeliveryQuantity
        {
            get { return _deliveryquantity; }
            set { _deliveryquantity = value; }
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
            get { return "ORD_OrderApplyDetail"; }
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
                    case "ApplyID":
                        return _applyid.ToString();
                    case "Product":
                        return _product.ToString();
                    case "Price":
                        return _price.ToString();
                    case "BookQuantity":
                        return _bookquantity.ToString();
                    case "AdjustQuantity":
                        return _adjustquantity.ToString();
                    case "AdjustReason":
                        return _adjustreason;
                    case "DeliveryQuantity":
                        return _deliveryquantity.ToString();
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
                    case "ApplyID":
                        int.TryParse(value, out _applyid);
                        break;
                    case "Product":
                        int.TryParse(value, out _product);
                        break;
                    case "Price":
                        decimal.TryParse(value, out _price);
                        break;
                    case "BookQuantity":
                        int.TryParse(value, out _bookquantity);
                        break;
                    case "AdjustQuantity":
                        int.TryParse(value, out _adjustquantity);
                        break;
                    case "AdjustReason":
                        _adjustreason = value;
                        break;
                    case "DeliveryQuantity":
                        int.TryParse(value, out _deliveryquantity);
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
