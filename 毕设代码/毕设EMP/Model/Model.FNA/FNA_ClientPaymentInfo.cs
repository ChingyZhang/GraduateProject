// ===================================================================
// 文件： FNA_ClientPaymentInfo.cs
// 项目名称：
// 创建时间：2009/2/22
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.FNA
{
    /// <summary>
    ///FNA_ClientPaymentInfo数据实体类
    /// </summary>
    [Serializable]
    public class FNA_ClientPaymentInfo : IModel
    {
        #region 私有变量定义
        private int _id = 0;
        private int _client = 0;
        private DateTime _paydate = new DateTime(1900, 1, 1);
        private decimal _payamount = 0;
        private string _receiveaccount = string.Empty;
        private DateTime _confirmdate = new DateTime(1900, 1, 1);
        private int _confirmstaff = 0;
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
        public FNA_ClientPaymentInfo()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public FNA_ClientPaymentInfo(int id, int client, DateTime paydate, decimal payamount, string receiveaccount, DateTime confirmdate, int confirmstaff, int approveflag, DateTime inserttime, int insertstaff, DateTime updatetime, int updatestaff)
        {
            _id = id;
            _client = client;
            _paydate = paydate;
            _payamount = payamount;
            _receiveaccount = receiveaccount;
            _confirmdate = confirmdate;
            _confirmstaff = confirmstaff;
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
        ///客户
        ///</summary>
        public int Client
        {
            get { return _client; }
            set { _client = value; }
        }

        ///<summary>
        ///付款时间
        ///</summary>
        public DateTime PayDate
        {
            get { return _paydate; }
            set { _paydate = value; }
        }

        ///<summary>
        ///付款金额
        ///</summary>
        public decimal PayAmount
        {
            get { return _payamount; }
            set { _payamount = value; }
        }

        ///<summary>
        ///收款帐户
        ///</summary>
        public string ReceiveAccount
        {
            get { return _receiveaccount; }
            set { _receiveaccount = value; }
        }

        ///<summary>
        ///确认日期
        ///</summary>
        public DateTime ConfirmDate
        {
            get { return _confirmdate; }
            set { _confirmdate = value; }
        }

        ///<summary>
        ///确认人
        ///</summary>
        public int ConfirmStaff
        {
            get { return _confirmstaff; }
            set { _confirmstaff = value; }
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
            get { return "FNA_ClientPaymentInfo"; }
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
                    case "Client":
                        return _client.ToString();
                    case "PayDate":
                        return _paydate.ToString();
                    case "PayAmount":
                        return _payamount.ToString();
                    case "ReceiveAccount":
                        return _receiveaccount;
                    case "ConfirmDate":
                        return _confirmdate.ToString();
                    case "ConfirmStaff":
                        return _confirmstaff.ToString();
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
                    case "Client":
                        int.TryParse(value, out _client);
                        break;
                    case "PayDate":
                        DateTime.TryParse(value, out _paydate);
                        break;
                    case "PayAmount":
                        decimal.TryParse(value, out _payamount);
                        break;
                    case "ReceiveAccount":
                        _receiveaccount = value;
                        break;
                    case "ConfirmDate":
                        DateTime.TryParse(value, out _confirmdate);
                        break;
                    case "ConfirmStaff":
                        int.TryParse(value, out _confirmstaff);
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
