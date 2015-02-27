// ===================================================================
// 文件： FNA_FeeApply.cs
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
    ///FNA_FeeApply数据实体类
    /// </summary>
    [Serializable]
    public class FNA_FeeApply : IModel
    {
        #region 私有变量定义
        private int _id = 0;
        private string _sheetcode = string.Empty;
        private int _organizecity = 0;
        private int _accountmonth = 0;
        private int _feetype = 0;
        private int _client = 0;
        private int _productbrand = 0;
        private int _state = 0;
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
        public FNA_FeeApply()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public FNA_FeeApply(int id, string sheetcode, int organizecity, int accountmonth, int feetype, int client, int productbrand, int state, int approveflag, DateTime inserttime, int insertstaff, DateTime updatetime, int updatestaff)
        {
            _id = id;
            _sheetcode = sheetcode;
            _organizecity = organizecity;
            _accountmonth = accountmonth;
            _feetype = feetype;
            _client = client;
            _productbrand = productbrand;
            _state = state;
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
        ///申请单号
        ///</summary>
        public string SheetCode
        {
            get { return _sheetcode; }
            set { _sheetcode = value; }
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
        ///申请月
        ///</summary>
        public int AccountMonth
        {
            get { return _accountmonth; }
            set { _accountmonth = value; }
        }

        ///<summary>
        ///费用类型
        ///</summary>
        public int FeeType
        {
            get { return _feetype; }
            set { _feetype = value; }
        }

        ///<summary>
        ///终端客户
        ///</summary>
        public int Client
        {
            get { return _client; }
            set { _client = value; }
        }

        ///<summary>
        ///品牌
        ///</summary>
        public int ProductBrand
        {
            get { return _productbrand; }
            set { _productbrand = value; }
        }

        ///<summary>
        ///申请单状态
        ///</summary>
        public int State
        {
            get { return _state; }
            set { _state = value; }
        }

        ///<summary>
        ///审批工作流ID
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
            get { return "FNA_FeeApply"; }
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
                    case "SheetCode":
                        return _sheetcode;
                    case "OrganizeCity":
                        return _organizecity.ToString();
                    case "AccountMonth":
                        return _accountmonth.ToString();
                    case "FeeType":
                        return _feetype.ToString();
                    case "Client":
                        return _client.ToString();
                    case "ProductBrand":
                        return _productbrand.ToString();
                    case "State":
                        return _state.ToString();
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
                    case "SheetCode":
                        _sheetcode = value;
                        break;
                    case "OrganizeCity":
                        int.TryParse(value, out _organizecity);
                        break;
                    case "AccountMonth":
                        int.TryParse(value, out _accountmonth);
                        break;
                    case "FeeType":
                        int.TryParse(value, out _feetype);
                        break;
                    case "Client":
                        int.TryParse(value, out _client);
                        break;
                    case "ProductBrand":
                        int.TryParse(value, out _productbrand);
                        break;
                    case "State":
                        int.TryParse(value, out _state);
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
