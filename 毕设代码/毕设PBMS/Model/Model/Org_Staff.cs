// ===================================================================
// 文件： Org_Staff.cs
// 项目名称：
// 创建时间：2015-01-25
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model
{
    /// <summary>
    ///Org_Staff数据实体类
    /// </summary>
    [Serializable]
    public class Org_Staff : IModel
    {
        #region 私有变量定义
        private int _id = 0;
        private string _realname = string.Empty;
        private int _sex = 0;
        private string _staffcode = string.Empty;
        private string _idcode = string.Empty;
        private DateTime _birthday = new DateTime(1900, 1, 1);
        private int _officialcity = 0;
        private string _address = string.Empty;
        private string _postcode = string.Empty;
        private string _telenum = string.Empty;
        private string _mobile = string.Empty;
        private string _email = string.Empty;
        private DateTime _beginworktime = new DateTime(1900, 1, 1);
        private DateTime _endworktime = new DateTime(1900, 1, 1);
        private int _organizecity = 0;
        private int _position = 0;
        private int _dimission = 0;
        private int _ownertype = 0;
        private int _ownerclient = 0;
        private string _remark = string.Empty;
        private Guid _aspnetuserid = Guid.Empty;
        private int _approvetask = 0;
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
        public Org_Staff()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public Org_Staff(int id, string realname, int sex, string staffcode, string idcode, DateTime birthday, int officialcity, string address, string postcode, string telenum, string mobile, string email, DateTime beginworktime, DateTime endworktime, int organizecity, int position, int dimission, int ownertype, int ownerclient, string remark, int approvetask, int approveflag, DateTime inserttime, int insertstaff, DateTime updatetime, int updatestaff)
        {
            _id = id;
            _realname = realname;
            _sex = sex;
            _staffcode = staffcode;
            _idcode = idcode;
            _birthday = birthday;
            _officialcity = officialcity;
            _address = address;
            _postcode = postcode;
            _telenum = telenum;
            _mobile = mobile;
            _email = email;
            _beginworktime = beginworktime;
            _endworktime = endworktime;
            _organizecity = organizecity;
            _position = position;
            _dimission = dimission;
            _ownertype = ownertype;
            _ownerclient = ownerclient;
            _remark = remark;
            _approvetask = approvetask;
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
        ///真实姓名
        ///</summary>
        public string RealName
        {
            get { return _realname; }
            set { _realname = value; }
        }

        ///<summary>
        ///性别
        ///</summary>
        public int Sex
        {
            get { return _sex; }
            set { _sex = value; }
        }

        ///<summary>
        ///员工工号
        ///</summary>
        public string StaffCode
        {
            get { return _staffcode; }
            set { _staffcode = value; }
        }

        ///<summary>
        ///身份证号
        ///</summary>
        public string IDCode
        {
            get { return _idcode; }
            set { _idcode = value; }
        }

        ///<summary>
        ///出生日期
        ///</summary>
        public DateTime Birthday
        {
            get { return _birthday; }
            set { _birthday = value; }
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
        ///地址
        ///</summary>
        public string Address
        {
            get { return _address; }
            set { _address = value; }
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
        ///Email
        ///</summary>
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        ///<summary>
        ///开始工作时间
        ///</summary>
        public DateTime BeginWorkTime
        {
            get { return _beginworktime; }
            set { _beginworktime = value; }
        }

        ///<summary>
        ///截至工作时间
        ///</summary>
        public DateTime EndWorkTime
        {
            get { return _endworktime; }
            set { _endworktime = value; }
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
        ///职务
        ///</summary>
        public int Position
        {
            get { return _position; }
            set { _position = value; }
        }

        ///<summary>
        ///在职标志
        ///</summary>
        public int Dimission
        {
            get { return _dimission; }
            set { _dimission = value; }
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

        /// <summary>
        /// 用户登录账户ID
        /// </summary>
        public Guid aspnetUserId
        {
            get { return _aspnetuserid; }
            set { _aspnetuserid = value; }
        }
        ///<summary>
        ///审核流程
        ///</summary>
        public int ApproveTask
        {
            get { return _approvetask; }
            set { _approvetask = value; }
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
            get { return "Org_Staff"; }
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
                    case "RealName":
                        return _realname;
                    case "Sex":
                        return _sex.ToString();
                    case "StaffCode":
                        return _staffcode;
                    case "IDCode":
                        return _idcode;
                    case "Birthday":
                        return _birthday.ToString();
                    case "OfficialCity":
                        return _officialcity.ToString();
                    case "Address":
                        return _address;
                    case "PostCode":
                        return _postcode;
                    case "TeleNum":
                        return _telenum;
                    case "Mobile":
                        return _mobile;
                    case "Email":
                        return _email;
                    case "BeginWorkTime":
                        return _beginworktime.ToString();
                    case "EndWorkTime":
                        return _endworktime.ToString();
                    case "OrganizeCity":
                        return _organizecity.ToString();
                    case "Position":
                        return _position.ToString();
                    case "Dimission":
                        return _dimission.ToString();
                    case "OwnerType":
                        return _ownertype.ToString();
                    case "OwnerClient":
                        return _ownerclient.ToString();
                    case "Remark":
                        return _remark;
                    case "aspnetUserId":
                        return _aspnetuserid.ToString();
                    case "ApproveTask":
                        return _approvetask.ToString();
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
                    case "RealName":
                        _realname = value;
                        break;
                    case "Sex":
                        int.TryParse(value, out _sex);
                        break;
                    case "StaffCode":
                        _staffcode = value;
                        break;
                    case "IDCode":
                        _idcode = value;
                        break;
                    case "Birthday":
                        DateTime.TryParse(value, out _birthday);
                        break;
                    case "OfficialCity":
                        int.TryParse(value, out _officialcity);
                        break;
                    case "Address":
                        _address = value;
                        break;
                    case "PostCode":
                        _postcode = value;
                        break;
                    case "TeleNum":
                        _telenum = value;
                        break;
                    case "Mobile":
                        _mobile = value;
                        break;
                    case "Email":
                        _email = value;
                        break;
                    case "BeginWorkTime":
                        DateTime.TryParse(value, out _beginworktime);
                        break;
                    case "EndWorkTime":
                        DateTime.TryParse(value, out _endworktime);
                        break;
                    case "OrganizeCity":
                        int.TryParse(value, out _organizecity);
                        break;
                    case "Position":
                        int.TryParse(value, out _position);
                        break;
                    case "Dimission":
                        int.TryParse(value, out _dimission);
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
                    case "aspnetUserId":
                        try { _aspnetuserid = new Guid(value); }
                        catch { }
                        break;
                    case "ApproveTask":
                        int.TryParse(value, out _approvetask);
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
