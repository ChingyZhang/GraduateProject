// ===================================================================
// 文件： CM_Client.cs
// 项目名称：
// 创建时间：2015/1/27
// 作者:	   Jace
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.CM
{
    /// <summary>
    ///CM_Client数据实体类
    /// </summary>
    [Serializable]
    public class CM_Client : IModel
    {
        #region 私有变量定义
        private int _id = 0;
        private string _code = string.Empty;
        private string _fullname = string.Empty;
        private string _shortname = string.Empty;
        private int _officialcity = 0;
        private string _postcode = string.Empty;
        private string _address = string.Empty;
        private string _deliveryaddress = string.Empty;
        private string _linkmanname = string.Empty;
        private string _telenum = string.Empty;
        private string _mobile = string.Empty;
        private string _fax = string.Empty;
        private int _chieflinkman = 0;
        private DateTime _opentime = new DateTime(1900, 1, 1);
        private DateTime _closetime = new DateTime(1900, 1, 1);
        private string _businesslicensecode = string.Empty;
        private string _organizationcode = string.Empty;
        private string _taxescode = string.Empty;
        private int _clienttype = 0;
        private int _ownertype = 0;
        private int _ownerclient = 0;
        private string _remark = string.Empty;
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
        public CM_Client()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public CM_Client(int id, string code, string fullname, string shortname, int officialcity, string postcode, string address, string deliveryaddress, string linkmanname, string telenum, string mobile, string fax, int chieflinkman, DateTime opentime, DateTime closetime, string businesslicensecode, string organizationcode, string taxescode, int clienttype, int ownertype, int ownerclient, string remark, int approveflag, DateTime inserttime, int insertstaff, DateTime updatetime, int updatestaff)
        {
            _id = id;
            _code = code;
            _fullname = fullname;
            _shortname = shortname;
            _officialcity = officialcity;
            _postcode = postcode;
            _address = address;
            _deliveryaddress = deliveryaddress;
            _linkmanname = linkmanname;
            _telenum = telenum;
            _mobile = mobile;
            _fax = fax;
            _chieflinkman = chieflinkman;
            _opentime = opentime;
            _closetime = closetime;
            _businesslicensecode = businesslicensecode;
            _organizationcode = organizationcode;
            _taxescode = taxescode;
            _clienttype = clienttype;
            _ownertype = ownertype;
            _ownerclient = ownerclient;
            _remark = remark;
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
        ///编号
        ///</summary>
        public string Code
        {
            get { return _code; }
            set { _code = value; }
        }

        ///<summary>
        ///客户全程
        ///</summary>
        public string FullName
        {
            get { return _fullname; }
            set { _fullname = value; }
        }

        ///<summary>
        ///简称
        ///</summary>
        public string ShortName
        {
            get { return _shortname; }
            set { _shortname = value; }
        }

        ///<summary>
        ///行政城市
        ///</summary>
        public int OfficialCity
        {
            get { return _officialcity; }
            set { _officialcity = value; }
        }

        ///<summary>
        ///邮编
        ///</summary>
        public string PostCode
        {
            get { return _postcode; }
            set { _postcode = value; }
        }

        ///<summary>
        ///地址
        ///</summary>
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }

        ///<summary>
        ///送货地址

        ///</summary>
        public string DeliveryAddress
        {
            get { return _deliveryaddress; }
            set { _deliveryaddress = value; }
        }

        ///<summary>
        ///联系人姓名
        ///</summary>
        public string LinkManName
        {
            get { return _linkmanname; }
            set { _linkmanname = value; }
        }

        ///<summary>
        ///电话号码
        ///</summary>
        public string TeleNum
        {
            get { return _telenum; }
            set { _telenum = value; }
        }

        ///<summary>
        ///手机号码
        ///</summary>
        public string Mobile
        {
            get { return _mobile; }
            set { _mobile = value; }
        }

        ///<summary>
        ///传真
        ///</summary>
        public string Fax
        {
            get { return _fax; }
            set { _fax = value; }
        }

        ///<summary>
        ///首要联系人
        ///</summary>
        public int ChiefLinkMan
        {
            get { return _chieflinkman; }
            set { _chieflinkman = value; }
        }

        ///<summary>
        ///开门时间
        ///</summary>
        public DateTime OpenTime
        {
            get { return _opentime; }
            set { _opentime = value; }
        }

        ///<summary>
        ///打烊时间
        ///</summary>
        public DateTime CloseTime
        {
            get { return _closetime; }
            set { _closetime = value; }
        }

        ///<summary>
        ///营业执照号

        ///</summary>
        public string BusinessLicenseCode
        {
            get { return _businesslicensecode; }
            set { _businesslicensecode = value; }
        }

        ///<summary>
        ///组织机构代码证号
        ///</summary>
        public string OrganizationCode
        {
            get { return _organizationcode; }
            set { _organizationcode = value; }
        }

        ///<summary>
        ///税务登记证号
        ///</summary>
        public string TaxesCode
        {
            get { return _taxescode; }
            set { _taxescode = value; }
        }

        ///<summary>
        ///客户类型
        ///</summary>
        public int ClientType
        {
            get { return _clienttype; }
            set { _clienttype = value; }
        }

        ///<summary>
        ///所有权属性

        ///</summary>
        public int OwnerType
        {
            get { return _ownertype; }
            set { _ownertype = value; }
        }

        ///<summary>
        ///所有权人
        ///</summary>
        public int OwnerClient
        {
            get { return _ownerclient; }
            set { _ownerclient = value; }
        }

        ///<summary>
        ///备注

        ///</summary>
        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }

        ///<summary>
        ///审批标志
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
            get { return "CM_Client"; }
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
                    case "Code":
                        return _code;
                    case "FullName":
                        return _fullname;
                    case "ShortName":
                        return _shortname;
                    case "OfficialCity":
                        return _officialcity.ToString();
                    case "PostCode":
                        return _postcode;
                    case "Address":
                        return _address;
                    case "DeliveryAddress":
                        return _deliveryaddress;
                    case "LinkManName":
                        return _linkmanname;
                    case "TeleNum":
                        return _telenum;
                    case "Mobile":
                        return _mobile;
                    case "Fax":
                        return _fax;
                    case "ChiefLinkMan":
                        return _chieflinkman.ToString();
                    case "OpenTime":
                        return _opentime.ToString();
                    case "CloseTime":
                        return _closetime.ToString();
                    case "BusinessLicenseCode":
                        return _businesslicensecode;
                    case "OrganizationCode":
                        return _organizationcode;
                    case "TaxesCode":
                        return _taxescode;
                    case "ClientType":
                        return _clienttype.ToString();
                    case "OwnerType":
                        return _ownertype.ToString();
                    case "OwnerClient":
                        return _ownerclient.ToString();
                    case "Remark":
                        return _remark;
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
                    case "Code":
                        _code = value;
                        break;
                    case "FullName":
                        _fullname = value;
                        break;
                    case "ShortName":
                        _shortname = value;
                        break;
                    case "OfficialCity":
                        int.TryParse(value, out _officialcity);
                        break;
                    case "PostCode":
                        _postcode = value;
                        break;
                    case "Address":
                        _address = value;
                        break;
                    case "DeliveryAddress":
                        _deliveryaddress = value;
                        break;
                    case "LinkManName":
                        _linkmanname = value;
                        break;
                    case "TeleNum":
                        _telenum = value;
                        break;
                    case "Mobile":
                        _mobile = value;
                        break;
                    case "Fax":
                        _fax = value;
                        break;
                    case "ChiefLinkMan":
                        int.TryParse(value, out _chieflinkman);
                        break;
                    case "OpenTime":
                        DateTime.TryParse(value, out _opentime);
                        break;
                    case "CloseTime":
                        DateTime.TryParse(value, out _closetime);
                        break;
                    case "BusinessLicenseCode":
                        _businesslicensecode = value;
                        break;
                    case "OrganizationCode":
                        _organizationcode = value;
                        break;
                    case "TaxesCode":
                        _taxescode = value;
                        break;
                    case "ClientType":
                        int.TryParse(value, out _clienttype);
                        break;
                    case "OwnerType":
                        int.TryParse(value, out _ownertype);
                        break;
                    case "OwnerClient":
                        int.TryParse(value, out _ownerclient);
                        break;
                    case "Remark":
                        _remark = value;
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
