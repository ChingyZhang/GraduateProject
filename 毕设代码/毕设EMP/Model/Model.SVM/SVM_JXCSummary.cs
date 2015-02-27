// ===================================================================
// 文件： SVM_JXCSummary.cs
// 项目名称：
// 创建时间：2010/7/8
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.SVM
{
    /// <summary>
    ///SVM_JXCSummary数据实体类
    /// </summary>
    [Serializable]
    public class SVM_JXCSummary : IModel
    {
        #region 私有变量定义
        private int _id = 0;
        private int _accountmonth = 0;
        private int _client = 0;
        private int _organizecity = 0;
        private int _product = 0;
        
        private string _productname = "";
        private string _productcode = "";
        private int _subunit = 0;
        private int _convertfactor = 0;

        private decimal _factoryprice = 0;
        private decimal _salesprice = 0;
        private decimal _retailprice = 0;
        private int _beginninginventory = 0;
        private int _purchasevolume = 0;
        private int _signinvolume = 0;
        private int _salesvolume = 0;
        private int _recallvolume = 0;
        private int _returnedvolume = 0;
        private int _giftvolume = 0;
        private int _endinginventory = 0;
        private int _computinventory = 0;
        private int _transitinventory = 0;
        private int _staleinventory = 0;
        private int _expiredinventory = 0;
        private int _transferinvolume = 0;
        private int _transferoutvolume = 0;
        private int _approveflag = 0;
        private int _approvestaff = 0;
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
        public SVM_JXCSummary()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public SVM_JXCSummary(int id, int accountmonth, int client, int organizecity, int product, decimal factoryprice, decimal salesprice, decimal retailprice, int beginninginventory, int purchasevolume,int signinvolume, int salesvolume, int recallvolume, int returnedvolume, int giftvolume, int endinginventory, int computinventory, int transitinventory, int staleinventory, int expiredinventory, int approveflag, int approvestaff, DateTime inserttime, int insertstaff, DateTime updatetime, int updatestaff)
        {
            _id = id;
            _accountmonth = accountmonth;
            _client = client;
            _organizecity = organizecity;
            _product = product;
            _factoryprice = factoryprice;
            _salesprice = salesprice;
            _retailprice = retailprice;
            _beginninginventory = beginninginventory;
            _purchasevolume = purchasevolume;
            _signinvolume = signinvolume;
            _salesvolume = salesvolume;
            _recallvolume = recallvolume;
            _returnedvolume = returnedvolume;
            _giftvolume = giftvolume;
            _endinginventory = endinginventory;
            _computinventory = computinventory;
            _transitinventory = transitinventory;
            _staleinventory = staleinventory;
            _expiredinventory = expiredinventory;
            _approveflag = approveflag;
            _approvestaff = approvestaff;
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
        ///会计月
        ///</summary>
        public int AccountMonth
        {
            get { return _accountmonth; }
            set { _accountmonth = value; }
        }

        ///<summary>
        ///客户
        ///</summary>
        public int Client
        {
            get { return _client; }
            set { _client = value; }
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
        ///产品
        ///</summary>
        public int Product
        {
            get { return _product; }
            set { _product = value; }
        }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName
        {
            get { return _productname; }
            set { _productname = value; }
        }
        /// <summary>
        /// 产品编码
        /// </summary>
        public string ProductCode
        {
            get { return _productcode; }
            set { _productcode = value; }
        }


        public int SubUnit
        {
            get { return _subunit; }
            set { _subunit = value; }
        }

        public int ConvertFactor
        {
            get { return _convertfactor; }
            set { _convertfactor = value; }
        }
        ///<summary>
        ///出厂价
        ///</summary>
        public decimal FactoryPrice
        {
            get { return _factoryprice; }
            set { _factoryprice = value; }
        }

        ///<summary>
        ///批发价
        ///</summary>
        public decimal SalesPrice
        {
            get { return _salesprice; }
            set { _salesprice = value; }
        }

        ///<summary>
        ///零售价
        ///</summary>
        public decimal RetailPrice
        {
            get { return _retailprice; }
            set { _retailprice = value; }
        }

        ///<summary>
        ///期初库存
        ///</summary>
        public int BeginningInventory
        {
            get { return _beginninginventory; }
            set { _beginninginventory = value; }
        }

        ///<summary>
        ///本期进货
        ///</summary>
        public int PurchaseVolume
        {
            get { return _purchasevolume; }
            set { _purchasevolume = value; }
        }

        /// <summary>
        /// 本期签收
        /// </summary>
        public int SignInVolume
        {
            get { return _signinvolume; }
            set { _signinvolume = value; }
        }
        ///<summary>
        ///本期销售
        ///</summary>
        public int SalesVolume
        {
            get { return _salesvolume; }
            set { _salesvolume = value; }
        }

        ///<summary>
        ///接收退货
        ///</summary>
        public int RecallVolume
        {
            get { return _recallvolume; }
            set { _recallvolume = value; }
        }

        ///<summary>
        ///退货
        ///</summary>
        public int ReturnedVolume
        {
            get { return _returnedvolume; }
            set { _returnedvolume = value; }
        }

        ///<summary>
        ///本品买赠
        ///</summary>
        public int GiftVolume
        {
            get { return _giftvolume; }
            set { _giftvolume = value; }
        }

        ///<summary>
        ///期末库存
        ///</summary>
        public int EndingInventory
        {
            get { return _endinginventory; }
            set { _endinginventory = value; }
        }

        ///<summary>
        ///计算库存
        ///</summary>
        public int ComputInventory
        {
            get { return _computinventory; }
            set { _computinventory = value; }
        }

        ///<summary>
        ///在途库存
        ///</summary>
        public int TransitInventory
        {
            get { return _transitinventory; }
            set { _transitinventory = value; }
        }

        ///<summary>
        ///界期品库存
        ///</summary>
        public int StaleInventory
        {
            get { return _staleinventory; }
            set { _staleinventory = value; }
        }

        ///<summary>
        ///过期品库存
        ///</summary>
        public int ExpiredInventory
        {
            get { return _expiredinventory; }
            set { _expiredinventory = value; }
        }

        ///<summary>
        ///调拨入数量
        ///</summary>
        public int TransferInVolume
        {
            get { return _transferinvolume; }
            set { _transferinvolume = value; }
        }

        ///<summary>
        ///调拨出数量
        ///</summary>
        public int TransferOutVolume
        {
            get { return _transferoutvolume; }
            set { _transferoutvolume = value; }
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
        ///审核人
        ///</summary>
        public int ApproveStaff
        {
            get { return _approvestaff; }
            set { _approvestaff = value; }
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
            get { return "SVM_JXCSummary"; }
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
                    case "AccountMonth":
                        return _accountmonth.ToString();
                    case "Client":
                        return _client.ToString();
                    case "OrganizeCity":
                        return _organizecity.ToString();
                    case "Product":
                        return _product.ToString();
                    case "FactoryPrice":
                        return _factoryprice.ToString();
                    case "SalesPrice":
                        return _salesprice.ToString();
                    case "RetailPrice":
                        return _retailprice.ToString();
                    case "BeginningInventory":
                        return _beginninginventory.ToString();
                    case "PurchaseVolume":
                        return _purchasevolume.ToString();
                    case "SignInVolume":
                        return _signinvolume.ToString();
                    case "SalesVolume":
                        return _salesvolume.ToString();
                    case "RecallVolume":
                        return _recallvolume.ToString();
                    case "ReturnedVolume":
                        return _returnedvolume.ToString();
                    case "GiftVolume":
                        return _giftvolume.ToString();
                    case "EndingInventory":
                        return _endinginventory.ToString();
                    case "ComputInventory":
                        return _computinventory.ToString();
                    case "TransitInventory":
                        return _transitinventory.ToString();
                    case "StaleInventory":
                        return _staleinventory.ToString();
                    case "ExpiredInventory":
                        return _expiredinventory.ToString();
                    case "TransferInVolume":
                        return _transferinvolume.ToString();
                    case "TransferOutVolume":
                        return _transferoutvolume.ToString();
                    case "ApproveFlag":
                        return _approveflag.ToString();
                    case "ApproveStaff":
                        return _approvestaff.ToString();
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
                    case "AccountMonth":
                        int.TryParse(value, out _accountmonth);
                        break;
                    case "Client":
                        int.TryParse(value, out _client);
                        break;
                    case "OrganizeCity":
                        int.TryParse(value, out _organizecity);
                        break;
                    case "Product":
                        int.TryParse(value, out _product);
                        break;
                    case "FactoryPrice":
                        decimal.TryParse(value, out _factoryprice);
                        break;
                    case "SalesPrice":
                        decimal.TryParse(value, out _salesprice);
                        break;
                    case "RetailPrice":
                        decimal.TryParse(value, out _retailprice);
                        break;
                    case "BeginningInventory":
                        int.TryParse(value, out _beginninginventory);
                        break;
                    case "PurchaseVolume":
                        int.TryParse(value, out _purchasevolume);
                        break;
                    case "SignInVolume":
                        int.TryParse(value, out _signinvolume);
                        break;
                    case "SalesVolume":
                        int.TryParse(value, out _salesvolume);
                        break;
                    case "RecallVolume":
                        int.TryParse(value, out _recallvolume);
                        break;
                    case "ReturnedVolume":
                        int.TryParse(value, out _returnedvolume);
                        break;
                    case "GiftVolume":
                        int.TryParse(value, out _giftvolume);
                        break;
                    case "EndingInventory":
                        int.TryParse(value, out _endinginventory);
                        break;
                    case "ComputInventory":
                        int.TryParse(value, out _computinventory);
                        break;
                    case "TransitInventory":
                        int.TryParse(value, out _transitinventory);
                        break;
                    case "StaleInventory":
                        int.TryParse(value, out _staleinventory);
                        break;
                    case "ExpiredInventory":
                        int.TryParse(value, out _expiredinventory);
                        break;
                    case "TransferInVolume":
                        int.TryParse(value, out _transferinvolume);
                        break;
                    case "TransferOutVolume":
                        int.TryParse(value, out _transferoutvolume);
                        break;
                    case "ApproveFlag":
                        int.TryParse(value, out _approveflag);
                        break;
                    case "ApproveStaff":
                        int.TryParse(value, out _approvestaff);
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
