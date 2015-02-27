// ===================================================================
// 文件： PDT_ProductPrice.cs
// 项目名称：
// 创建时间：2009-3-10
// 作者:	   
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.Pub
{
    /// <summary>
    ///PDT_ProductPrice数据实体类
    /// </summary>
    [Serializable]
    public class PDT_ProductPrice : IModel
    {
        #region 私有变量定义
        private int _id = 0;
        private int _client = 0;
        private int _standardprice = 0;
        private DateTime _begindate = new DateTime(1900, 1, 1);
        private DateTime _enddate = new DateTime(1900, 1, 1);
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
        public PDT_ProductPrice()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public PDT_ProductPrice(int id, int client, DateTime begindate, DateTime enddate, int approveflag, DateTime inserttime, int insertstaff, DateTime updatetime, int updatestaff)
        {
            _id = id;
            _client = client;
            _begindate = begindate;
            _enddate = enddate;
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

        /// <summary>
        /// 标准价表
        /// </summary>
        public int StandardPrice
        {
            get { return _standardprice; }
            set { _standardprice = value; }
        }

        ///<summary>
        ///BeginDate
        ///</summary>
        public DateTime BeginDate
        {
            get { return _begindate; }
            set { _begindate = value; }
        }

        ///<summary>
        ///EndDate
        ///</summary>
        public DateTime EndDate
        {
            get { return _enddate; }
            set { _enddate = value; }
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
            get { return "PDT_ProductPrice"; }
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
                    case "StandardPrice":
                        return _standardprice.ToString();
                    case "BeginDate":
                        return _begindate.ToString();
                    case "EndDate":
                        return _enddate.ToString();
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
                    case "StandardPrice":
                        int.TryParse(value, out _standardprice);
                        break;
                    case "BeginDate":
                        DateTime.TryParse(value, out _begindate);
                        break;
                    case "EndDate":
                        DateTime.TryParse(value, out _enddate);
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
