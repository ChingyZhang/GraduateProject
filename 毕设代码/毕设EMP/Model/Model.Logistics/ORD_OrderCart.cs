using System;
using System.Collections.Generic;
using System.Text;

namespace MCSFramework.Model.Logistics
{
    [Serializable]
    public class ORD_OrderCart
    {
        private int _id = 0;
        private int _product = 0;
        private int _minquantity = 0;
        private int _maxquantity = 0;
        private int _availablequantity = 0;
        private int _submitquantity = 0;
        private decimal _price = 0;
        private int _bookquantity = 0;

        /// <summary>
        /// 请购申请单明细ID
        /// </summary>
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }
        ///<summary>
        ///品项
        ///</summary>
        public int Product
        {
            get { return _product; }
            set { _product = value; }
        }

        ///<summary>
        ///请购最小量
        ///</summary>
        public int MinQuantity
        {
            get { return _minquantity; }
            set { _minquantity = value; }
        }

        ///<summary>
        ///请购最大量
        ///</summary>
        public int MaxQuantity
        {
            get { return _maxquantity; }
            set { _maxquantity = value; }
        }

        ///<summary>
        ///可供采购量
        ///</summary>
        public int AvailableQuantity
        {
            get { return _availablequantity; }
            set { _availablequantity = value; }
        }

        /// <summary>
        /// 已提交采购量
        /// </summary>
        public int SubmitQuantity
        {
            get { return _submitquantity; }
            set { _submitquantity = value; }
        }
        ///<summary>
        ///请购单价
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

        public int AdjustQuantity
        {
            get { return 0; }
        }

        public string AdjustReason
        {
            get { return ""; }
        }

        public int DeliveryQuantity
        {
            get { return 0; }
        }
    }
}
