// ===================================================================
// 文件： CM_WareHouse.cs
// 项目名称：
// 创建时间：2012-7-21
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.CM
{
    /// <summary>
    ///CM_WareHouse数据实体类
    /// </summary>
    [Serializable]
    public class CM_WareHouse : IModel
    {
        #region 私有变量定义
        private int _id = 0;
        private int _client = 0;
        private string _code = string.Empty;
        private string _name = string.Empty;
        private int _officialcity = 0;
        private string _address = string.Empty;
        private string _telenum = string.Empty;
        private int _capacity = 0;
        private int _area = 0;
        private string _keeper = string.Empty;
        private string _mobile = string.Empty;
        private int _classify = 0;
        private int _relateVehicle = 0;
        private decimal _longitude = 0;
        private decimal _latitude = 0;
        private int _activestate = 0;
        private string _remark = string.Empty;
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
        public CM_WareHouse()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public CM_WareHouse(int id, int client, string code, string name, int officialcity, string address, string telenum, int capacity, int area, string keeper, string mobile, int classify, int relateVehicle, decimal longitude, decimal latitude, int activestate, string remark, int approvetask, int approveflag, DateTime inserttime, int insertstaff, DateTime updatetime, int updatestaff)
        {
            _id = id;
            _client = client;
            _code = code;
            _name = name;
            _officialcity = officialcity;
            _address = address;
            _telenum = telenum;
            _capacity = capacity;
            _area = area;
            _keeper = keeper;
            _mobile = mobile;
            _classify = classify;
            _relateVehicle = relateVehicle;
            _longitude = longitude;
            _latitude = latitude;
            _activestate = activestate;
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
        ///所属客户
        ///</summary>
        public int Client
        {
            get { return _client; }
            set { _client = value; }
        }

        ///<summary>
        ///代码
        ///</summary>
        public string Code
        {
            get { return _code; }
            set { _code = value; }
        }

        ///<summary>
        ///仓库名称
        ///</summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        ///<summary>
        ///所在城市
        ///</summary>
        public int OfficialCity
        {
            get { return _officialcity; }
            set { _officialcity = value; }
        }

        ///<summary>
        ///所在地址
        ///</summary>
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }

        ///<summary>
        ///联系电话
        ///</summary>
        public string TeleNum
        {
            get { return _telenum; }
            set { _telenum = value; }
        }

        /// <summary>
        /// 仓库容量
        /// </summary>
        public int Capacity
        {
            get { return _capacity; }
            set { _capacity = value; }
        }

        ///<summary>
        ///面积
        ///</summary>
        public int Area
        {
            get { return _area; }
            set { _area = value; }
        }

        ///<summary>
        ///库管姓名
        ///</summary>
        public string Keeper
        {
            get { return _keeper; }
            set { _keeper = value; }
        }

        ///<summary>
        ///库管手机
        ///</summary>
        public string Mobile
        {
            get { return _mobile; }
            set { _mobile = value; }
        }

        /// <summary>
        /// 仓库类型
        /// </summary>
        public int Classify
        {
            get { return _classify; }
            set { _classify = value; }
        }

        /// <summary>
        /// 关联车辆
        /// </summary>
        public int RelateVehicle
        {
            get { return _relateVehicle; }
            set { _relateVehicle = value; }
        }

        ///<summary>
        ///经度
        ///</summary>
        public decimal Longitude
        {
            get { return _longitude; }
            set { _longitude = value; }
        }

        ///<summary>
        ///纬度
        ///</summary>
        public decimal Latitude
        {
            get { return _latitude; }
            set { _latitude = value; }
        }

        ///<summary>
        ///活跃状态
        ///</summary>
        public int ActiveState
        {
            get { return _activestate; }
            set { _activestate = value; }
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
        ///审批任务
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
            get { return "CM_WareHouse"; }
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
                    case "Code":
                        return _code;
                    case "Name":
                        return _name;
                    case "OfficialCity":
                        return _officialcity.ToString();
                    case "Address":
                        return _address;
                    case "TeleNum":
                        return _telenum;
                    case "Capacity":
                        return _capacity.ToString();
                    case "Area":
                        return _area.ToString();
                    case "Keeper":
                        return _keeper;
                    case "Mobile":
                        return _mobile;
                    case "Classify":
                        return _classify.ToString();
                    case "RelateVehicle":
                        return _relateVehicle.ToString();
                    case "Longitude":
                        return _longitude.ToString();
                    case "Latitude":
                        return _latitude.ToString();
                    case "ActiveState":
                        return _activestate.ToString();
                    case "Remark":
                        return _remark;
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
                    case "Client":
                        int.TryParse(value, out _client);
                        break;
                    case "Code":
                        _code = value;
                        break;
                    case "Name":
                        _name = value;
                        break;
                    case "OfficialCity":
                        int.TryParse(value, out _officialcity);
                        break;
                    case "Address":
                        _address = value;
                        break;
                    case "TeleNum":
                        _telenum = value;
                        break;
                    case "Capacity":
                        int.TryParse(value, out _capacity);
                        break;
                    case "Area":
                        int.TryParse(value, out _area);
                        break;
                    case "Keeper":
                        _keeper = value;
                        break;
                    case "Mobile":
                        _mobile = value;
                        break;
                    case "Classify":
                        int.TryParse(value, out _classify);
                        break;
                    case "RelateVehicle":
                        int.TryParse(value, out _relateVehicle);
                        break;
                    case "Longitude":
                        decimal.TryParse(value, out _longitude);
                        break;
                    case "Latitude":
                        decimal.TryParse(value, out _latitude);
                        break;
                    case "ActiveState":
                        int.TryParse(value, out _activestate);
                        break;
                    case "Remark":
                        _remark = value;
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
