// ===================================================================
// 文件： JN_WorkingPlan.cs
// 项目名称：
// 创建时间：2009/6/18
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.OA
{
    /// <summary>
    ///JN_WorkingPlan数据实体类
    /// </summary>
    [Serializable]
    public class JN_WorkingPlan : IModel
    {
        #region 私有变量定义
        private int _id = 0;
        private int _organizecity = 0;
        private string _title = string.Empty;
        private DateTime _begindate = new DateTime(1900, 1, 1);
        private DateTime _enddate = new DateTime(1900, 1, 1);
        private int _state = 0;
        private int _staff = 0;
        private int _approveflag = 0;
        private DateTime _inserttime = new DateTime(1900, 1, 1);
        private int _insertstaff = 0;
        private DateTime _updatetime = new DateTime(1900, 1, 1);
        private int _updatestaff = 0;
        private string _remark = string.Empty;
        private NameValueCollection _extpropertys;
        #endregion

        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public JN_WorkingPlan()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public JN_WorkingPlan(int id, int organizecity, string title, DateTime begindate, DateTime enddate, int state, int staff, int approveflag, DateTime inserttime, int insertstaff, DateTime updatetime, int updatestaff, string remark)
        {
            _id = id;
            _organizecity = organizecity;
            _title = title;
            _begindate = begindate;
            _enddate = enddate;
            _state = state;
            _staff = staff;
            _approveflag = approveflag;
            _inserttime = inserttime;
            _insertstaff = insertstaff;
            _updatetime = updatetime;
            _updatestaff = updatestaff;
            _remark = remark;
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
        ///管理片区
        ///</summary>
        public int OrganizeCity
        {
            get { return _organizecity; }
            set { _organizecity = value; }
        }

        ///<summary>
        ///标题
        ///</summary>
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        ///<summary>
        ///开始日期
        ///</summary>
        public DateTime BeginDate
        {
            get { return _begindate; }
            set { _begindate = value; }
        }

        ///<summary>
        ///截止日期
        ///</summary>
        public DateTime EndDate
        {
            get { return _enddate; }
            set { _enddate = value; }
        }

        ///<summary>
        ///审批状态
        ///</summary>
        public int State
        {
            get { return _state; }
            set { _state = value; }
        }

        ///<summary>
        ///计划的员工
        ///</summary>
        public int Staff
        {
            get { return _staff; }
            set { _staff = value; }
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

        ///<summary>
        ///备注
        ///</summary>
        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
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
            get { return "JN_WorkingPlan"; }
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
                    case "Title":
                        return _title;
                    case "BeginDate":
                        return _begindate.ToString();
                    case "EndDate":
                        return _enddate.ToString();
                    case "State":
                        return _state.ToString();
                    case "Staff":
                        return _staff.ToString();
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
                    case "Remark":
                        return _remark;
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
                    case "Title":
                        _title = value;
                        break;
                    case "BeginDate":
                        DateTime.TryParse(value, out _begindate);
                        break;
                    case "EndDate":
                        DateTime.TryParse(value, out _enddate);
                        break;
                    case "State":
                        int.TryParse(value, out _state);
                        break;
                    case "Staff":
                        int.TryParse(value, out _staff);
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
                    case "Remark":
                        _remark = value;
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
