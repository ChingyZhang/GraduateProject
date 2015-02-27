// ===================================================================
// 文件： SVM_SalesVolume_Detail.cs
// 项目名称：
// 创建时间：2009-2-19
// 作者:	   
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.SVM
{
    /// <summary>
    ///SVM_SalesVolume_Detail数据实体类
    /// </summary>
    [Serializable]
    public class SVM_SalesVolume_Detail : IModel
    {
        #region 私有变量定义
        private int _id = 0;
        private int _salesvolume = 0;
        private int _product = 0;
        private string _lotnumber = string.Empty;
        private decimal _salesprice = 0;
        private int _quantity = 0;
        private decimal _factoryprice = 0;
        private int _syncquantity = 0;
        #endregion

        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public SVM_SalesVolume_Detail()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public SVM_SalesVolume_Detail(int id, int salesvolume, int product, string lotnumber, decimal salesprice, int quantity, decimal factoryprice, int syncquantity)
        {
            _id = id;
            _salesvolume = salesvolume;
            _product = product;
            _lotnumber = lotnumber;
            _salesprice = salesprice;
            _quantity = quantity;
            _factoryprice = factoryprice;
            _syncquantity = syncquantity;
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
        ///销售单ID
        ///</summary>
        public int SalesVolume
        {
            get { return _salesvolume; }
            set { _salesvolume = value; }
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
        ///批号
        ///</summary>
        public string LotNumber
        {
            get { return _lotnumber; }
            set { _lotnumber = value; }
        }

        ///<summary>
        ///销售价格
        ///</summary>
        public decimal SalesPrice
        {
            get { return _salesprice; }
            set { _salesprice = value; }
        }

        ///<summary>
        ///销售数量
        ///</summary>
        public int Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }

        ///<summary>
        ///厂价
        ///</summary>
        public decimal FactoryPrice
        {
            get { return _factoryprice; }
            set { _factoryprice = value; }
        }
        /// <summary>
        /// 积分同步数量
        /// </summary>
        public int SyncQuantity
        {
            get { return _syncquantity; }
            set { _syncquantity = value; }
        }

        #endregion

        public string ModelName
        {
            get { return "SVM_SalesVolume_Detail"; }
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
                    case "SalesVolume":
                        return _salesvolume.ToString();
                    case "Product":
                        return _product.ToString();
                    case "LotNumber":
                        return _lotnumber;
                    case "SalesPrice":
                        return _salesprice.ToString();
                    case "Quantity":
                        return _quantity.ToString();
                    case "FactoryPrice":
                        return _factoryprice.ToString();
                    case "SyncQuantity":
                        return _syncquantity.ToString();
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
                    case "SalesVolume":
                        int.TryParse(value, out _salesvolume);
                        break;
                    case "Product":
                        int.TryParse(value, out _product);
                        break;
                    case "LotNumber":
                        _lotnumber = value;
                        break;
                    case "SalesPrice":
                        decimal.TryParse(value, out _salesprice);
                        break;
                    case "Quantity":
                        int.TryParse(value, out _quantity);
                        break;
                    case "FactoryPrice":
                        decimal.TryParse(value, out _factoryprice);
                        break;
                    case "SyncQuantity":
                        int.TryParse(value, out _syncquantity);
                        break;

                }
            }
        }
        #endregion
    }
}
