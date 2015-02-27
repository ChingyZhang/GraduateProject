// ===================================================================
// 文件： SVM_MYSaleInFlag.cs
// 项目名称：
// 创建时间：2014/11/12
// 作者:	   Jace
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.SVM
{
    /// <summary>
    ///SVM_MYSaleInFlag数据实体类
    /// </summary>
    [Serializable]
    public class SVM_MYSaleInFlag : IModel
    {
        #region 私有变量定义
        private int _id = 0;
        private int _organizecity = 0;
        private int _accountmonth = 0;
        private int _clientid = 0;
        private string _clientcode = string.Empty;
        private string _clientname = string.Empty;
        private int _diid = 0;
        private string _diname = string.Empty;
        private string _dicode = string.Empty;
        private int _clientmanager = 0;
        private string _managercode = string.Empty;
        private string _managername = string.Empty;
        private int _flag = 0;
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
        public SVM_MYSaleInFlag()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public SVM_MYSaleInFlag(int id,int organizecity, int accountmonth, int clientid, string clientcode, string clientname, int diid, string diname, string dicode, int clientmanager, string managercode, string managername, int flag, int approveflag, DateTime inserttime, int insertstaff, DateTime updatetime, int updatestaff)
        {
            _id = id;
            _organizecity = organizecity;
            _accountmonth = accountmonth;
            _clientid = clientid;
            _clientcode = clientcode;
            _clientname = clientname;
            _diid = diid;
            _diname = diname;
            _dicode = dicode;
            _clientmanager = clientmanager;
            _managercode = managercode;
            _managername = managername;
            _flag = flag;
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

        /// <summary>
        /// OrganizeCity
        /// </summary>
        public int OrganizeCity
        {
            get { return _organizecity; }
            set { _organizecity = value; }
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
        ///ClientID
        ///</summary>
        public int ClientID
        {
            get { return _clientid; }
            set { _clientid = value; }
        }

        ///<summary>
        ///ClientCode
        ///</summary>
        public string ClientCode
        {
            get { return _clientcode; }
            set { _clientcode = value; }
        }

        ///<summary>
        ///ClientName
        ///</summary>
        public string ClientName
        {
            get { return _clientname; }
            set { _clientname = value; }
        }

        ///<summary>
        ///DIID
        ///</summary>
        public int DIID
        {
            get { return _diid; }
            set { _diid = value; }
        }

        ///<summary>
        ///DIName
        ///</summary>
        public string DIName
        {
            get { return _diname; }
            set { _diname = value; }
        }

        ///<summary>
        ///DICode
        ///</summary>
        public string DICode
        {
            get { return _dicode; }
            set { _dicode = value; }
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
        ///ManagerCode
        ///</summary>
        public string ManagerCode
        {
            get { return _managercode; }
            set { _managercode = value; }
        }

        ///<summary>
        ///ManagerName
        ///</summary>
        public string ManagerName
        {
            get { return _managername; }
            set { _managername = value; }
        }

        ///<summary>
        ///Flag
        ///</summary>
        public int Flag
        {
            get { return _flag; }
            set { _flag = value; }
        }

        ///<summary>
        ///ApproveFlag
        ///</summary>
        public int ApproveFlag
        {
            get { return _approveflag; }
            set { _approveflag = value; }
        }

        ///<summary>
        ///InsertTime
        ///</summary>
        public DateTime InsertTime
        {
            get { return _inserttime; }
            set { _inserttime = value; }
        }

        ///<summary>
        ///InsertStaff
        ///</summary>
        public int InsertStaff
        {
            get { return _insertstaff; }
            set { _insertstaff = value; }
        }

        ///<summary>
        ///UpdateTime
        ///</summary>
        public DateTime UpdateTime
        {
            get { return _updatetime; }
            set { _updatetime = value; }
        }

        ///<summary>
        ///UpdateStaff
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
            get { return "SVM_MYSaleInFlag"; }
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
                    case "OrganizeCity":
                        return _organizecity.ToString();
                    case "AccountMonth":
                        return _accountmonth.ToString();
                    case "ClientID":
                        return _clientid.ToString();
                    case "ClientCode":
                        return _clientcode;
                    case "ClientName":
                        return _clientname;
                    case "DIID":
                        return _diid.ToString();
                    case "DIName":
                        return _diname;
                    case "DICode":
                        return _dicode;
                    case "ClientManager":
                        return _clientmanager.ToString();
                    case "ManagerCode":
                        return _managercode;
                    case "ManagerName":
                        return _managername;
                    case "Flag":
                        return _flag.ToString();
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
                    case "OrganizeCity":
                        int.TryParse(value, out _organizecity);
                        break;
                    case "AccountMonth":
                        int.TryParse(value, out _accountmonth);
                        break;
                    case "ClientID":
                        int.TryParse(value, out _clientid);
                        break;
                    case "ClientCode":
                        _clientcode = value;
                        break;
                    case "ClientName":
                        _clientname = value;
                        break;
                    case "DIID":
                        int.TryParse(value, out _diid);
                        break;
                    case "DIName":
                        _diname = value;
                        break;
                    case "DICode":
                        _dicode = value;
                        break;
                    case "ClientManager":
                        int.TryParse(value, out _clientmanager);
                        break;
                    case "ManagerCode":
                        _managercode = value;
                        break;
                    case "ManagerName":
                        _managername = value;
                        break;
                    case "Flag":
                        int.TryParse(value, out _flag);
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
