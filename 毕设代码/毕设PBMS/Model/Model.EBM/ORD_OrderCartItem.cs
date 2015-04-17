using System;
using System.Collections.Generic;
using System.Text;

namespace MCSFramework.Model.EBM
{
    [Serializable]
    public class ORD_OrderCartItem
    {
        private int _id = 0;
        private int _product = 0;
        private int _minquantity = 0;
        private int _maxquantity = 0;
        private int _availablequantity = 0;
        private int _bookquantity = 0;
        private int _submitquantity = 0;
        private int _confirmquantity = 0;
        private int _deliveredquantity=0;
        private int _quotaquantity = 0;
        private decimal _price = 0;
        private decimal _points = 0;

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

        /// <summary>
        /// 兑换所需积分
        /// </summary>
        public decimal Points
        {
            get { return _points; }
            set { _points = value; }
        }

        ///<summary>
        ///请购数量
        ///</summary>
        public int BookQuantity
        {
            get { return _bookquantity; }
            set { _bookquantity = value; }
        }

        /// <summary>
        /// 确认数量
        /// </summary>
        public int ConfirmQuantity
        {
            get { return _confirmquantity; }
            set { _confirmquantity = value; }
        }

        /// <summary>
        /// 已发放数量
        /// </summary>
        public int DeliveredQuantity
        {
            get { return _deliveredquantity; }
            set { _deliveredquantity = value; }
        }

        /// <summary>
        /// 配额数量
        /// </summary>
        public int QuotaQuantity
        {
            get { return _quotaquantity; }
            set { _quotaquantity = value; }
        }
    }
}
