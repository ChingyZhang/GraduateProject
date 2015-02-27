// ===================================================================
// 文件： SVM_ClientSalesTarget.cs
// 项目名称：
// 创建时间：2013/10/23
// 作者:	   chf
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.SVM
{
    /// <summary>
    ///SVM_ClientSalesTarget数据实体类
    /// </summary>
    [Serializable]
    public class SVM_ClientSalesTarget : IModel
    {
        #region 私有变量定义
        private int _id = 0;
        private int _accountmonth = 0;     
        private int _organizecity = 0;
        private int _client = 0;
        private int _disupplier = 0;
        private int _clientmanager = 0;
        private decimal _salestarget = 0;
        private decimal _keysalestarget = 0;
        private decimal _actsales = 0;
        private decimal _actkeysales = 0;
        private decimal _salestargetadjust = 0;
        private decimal _data01 = 0;
        private decimal _data02 = 0;
        private decimal _data03 = 0;
        private decimal _data04 = 0;
        private decimal _data05 = 0;
        private decimal _data06 = 0;
        private decimal _data07 = 0;
        private decimal _data08 = 0;
        private decimal _data09 = 0;
        private decimal _data10 = 0;
        private int _approveflag = 0;
        private NameValueCollection _extpropertys;
        #endregion

        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public SVM_ClientSalesTarget()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public SVM_ClientSalesTarget(int id, int accountmonth, int organizecity, int client, int disupplier, int clientmanager, decimal salestarget, decimal keysalestarget, decimal actsales, decimal actkeysales, decimal salestargetadjust, decimal data01, decimal data02, decimal data03, decimal data04, decimal data05, decimal data06, decimal data07, decimal data08, decimal data09, decimal data10, int approveflag)
        {
            _id = id;
            _accountmonth = accountmonth;
            _organizecity = organizecity;
            _client = client;
            _disupplier = disupplier;
            _clientmanager = clientmanager;
            _salestarget = salestarget;
            _keysalestarget = keysalestarget;
            _actsales = actsales;
            _actkeysales = actkeysales;
            _salestargetadjust = salestargetadjust;
            _data01 = data01;
            _data02 = data02;
            _data03 = data03;
            _data04 = data04;
            _data05 = data05;
            _data06 = data06;
            _data07 = data07;
            _data08 = data08;
            _data09 = data09;
            _data10 = data10;
            _approveflag = approveflag;

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
        ///AccountMonth
        ///</summary>
        public int AccountMonth
        {
            get { return _accountmonth; }
            set { _accountmonth = value; }
        }

        ///<summary>
        ///OrganizeCity
        ///</summary>
        public int OrganizeCity
        {
            get { return _organizecity; }
            set { _organizecity = value; }
        }

        ///<summary>
        ///Client
        ///</summary>
        public int Client
        {
            get { return _client; }
            set { _client = value; }
        }
        /// <summary>
        /// 经销商
        /// </summary>
        public int DISupplier
        {
            get { return _disupplier; }
            set { _disupplier = value; }
        }
        ///<summary>
        ///ClientManager
        ///</summary>
        public int ClientManager
        {
            get { return _clientmanager; }
            set { _clientmanager = value; }
        }

        ///<summary>
        ///SalesTarget
        ///</summary>
        public decimal SalesTarget
        {
            get { return _salestarget; }
            set { _salestarget = value; }
        }

        ///<summary>
        ///KeySalesTarget
        ///</summary>
        public decimal KeySalesTarget
        {
            get { return _keysalestarget; }
            set { _keysalestarget = value; }
        }

        ///<summary>
        ///ActSales
        ///</summary>
        public decimal ActSales
        {
            get { return _actsales; }
            set { _actsales = value; }
        }

        ///<summary>
        ///ActKeySales
        ///</summary>
        public decimal ActKeySales
        {
            get { return _actkeysales; }
            set { _actkeysales = value; }
        }

        ///<summary>
        ///SalesTargetAdjust
        ///</summary>
        public decimal SalesTargetAdjust
        {
            get { return _salestargetadjust; }
            set { _salestargetadjust = value; }
        }

        ///<summary>
        ///Data01
        ///</summary>
        public decimal Data01
        {
            get { return _data01; }
            set { _data01 = value; }
        }

        ///<summary>
        ///Data02
        ///</summary>
        public decimal Data02
        {
            get { return _data02; }
            set { _data02 = value; }
        }

        ///<summary>
        ///Data03
        ///</summary>
        public decimal Data03
        {
            get { return _data03; }
            set { _data03 = value; }
        }

        ///<summary>
        ///Data04
        ///</summary>
        public decimal Data04
        {
            get { return _data04; }
            set { _data04 = value; }
        }

        ///<summary>
        ///Data05
        ///</summary>
        public decimal Data05
        {
            get { return _data05; }
            set { _data05 = value; }
        }

        ///<summary>
        ///Data06
        ///</summary>
        public decimal Data06
        {
            get { return _data06; }
            set { _data06 = value; }
        }

        ///<summary>
        ///Data07
        ///</summary>
        public decimal Data07
        {
            get { return _data07; }
            set { _data07 = value; }
        }

        ///<summary>
        ///Data08
        ///</summary>
        public decimal Data08
        {
            get { return _data08; }
            set { _data08 = value; }
        }

        ///<summary>
        ///Data09
        ///</summary>
        public decimal Data09
        {
            get { return _data09; }
            set { _data09 = value; }
        }

        ///<summary>
        ///Data10
        ///</summary>
        public decimal Data10
        {
            get { return _data10; }
            set { _data10 = value; }
        }

        ///<summary>
        ///ApproveFlag
        ///</summary>
        public int ApproveFlag
        {
            get { return _approveflag; }
            set { _approveflag = value; }
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
            get { return "SVM_ClientSalesTarget"; }
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
                    case "OrganizeCity":
                        return _organizecity.ToString();
                    case "Client":
                        return _client.ToString();
                    case "DISupplier":
                        return _disupplier.ToString();
                    case "ClientManager":
                        return _clientmanager.ToString();
                    case "SalesTarget":
                        return _salestarget.ToString();
                    case "KeySalesTarget":
                        return _keysalestarget.ToString();
                    case "ActSales":
                        return _actsales.ToString();
                    case "ActKeySales":
                        return _actkeysales.ToString();
                    case "SalesTargetAdjust":
                        return _salestargetadjust.ToString();
                    case "Data01":
                        return _data01.ToString();
                    case "Data02":
                        return _data02.ToString();
                    case "Data03":
                        return _data03.ToString();
                    case "Data04":
                        return _data04.ToString();
                    case "Data05":
                        return _data05.ToString();
                    case "Data06":
                        return _data06.ToString();
                    case "Data07":
                        return _data07.ToString();
                    case "Data08":
                        return _data08.ToString();
                    case "Data09":
                        return _data09.ToString();
                    case "Data10":
                        return _data10.ToString();
                    case "ApproveFlag":
                        return _approveflag.ToString();
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
                    case "OrganizeCity":
                        int.TryParse(value, out _organizecity);
                        break;
                    case "Client":
                        int.TryParse(value, out _client);
                        break;
                    case "DISupplier":
                        int.TryParse(value,out _disupplier);
                        break;
                    case "ClientManager":
                        int.TryParse(value, out _clientmanager);
                        break;
                    case "SalesTarget":
                        decimal.TryParse(value, out _salestarget);
                        break;
                    case "KeySalesTarget":
                        decimal.TryParse(value, out _keysalestarget);
                        break;
                    case "ActSales":
                        decimal.TryParse(value, out _actsales);
                        break;
                    case "ActKeySales":
                        decimal.TryParse(value, out _actkeysales);
                        break;
                    case "SalesTargetAdjust":
                        decimal.TryParse(value, out _salestargetadjust);
                        break;
                    case "Data01":
                        decimal.TryParse(value, out _data01);
                        break;
                    case "Data02":
                        decimal.TryParse(value, out _data02);
                        break;
                    case "Data03":
                        decimal.TryParse(value, out _data03);
                        break;
                    case "Data04":
                        decimal.TryParse(value, out _data04);
                        break;
                    case "Data05":
                        decimal.TryParse(value, out _data05);
                        break;
                    case "Data06":
                        decimal.TryParse(value, out _data06);
                        break;
                    case "Data07":
                        decimal.TryParse(value, out _data07);
                        break;
                    case "Data08":
                        decimal.TryParse(value, out _data08);
                        break;
                    case "Data09":
                        decimal.TryParse(value, out _data09);
                        break;
                    case "Data10":
                        decimal.TryParse(value, out _data10);
                        break;
                    case "ApproveFlag":
                        int.TryParse(value, out _approveflag);
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
