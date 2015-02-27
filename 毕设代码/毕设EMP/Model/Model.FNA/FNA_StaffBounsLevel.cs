// ===================================================================
// 文件： FNA_StaffBounsLevel.cs
// 项目名称：
// 创建时间：2013-08-02
// 作者:	   Jace
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.FNA
{
    /// <summary>
    ///FNA_StaffBounsLevel数据实体类
    /// </summary>
    [Serializable]
    public class FNA_StaffBounsLevel : IModel
    {
        #region 私有变量定义
        private int _id = 0;
        private int _organizecity = 0;
        private int _quarter = 0;
        private int _begainmonth = 0;
        private int _endmonth = 0;
        private int _level = 0;
        private decimal _salesvolume1 = 0;
        private decimal _salesvolume2 = 0;
        private decimal _bouns = 0;
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
        public FNA_StaffBounsLevel()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public FNA_StaffBounsLevel(int id, int organizecity, int quarter, int begainmonth, int endmonth, int level, decimal salesvolume1, decimal salesvolume2, decimal bouns, int approveflag, DateTime inserttime, int insertstaff, DateTime updatetime, int updatestaff)
        {
            _id = id;
            _organizecity = organizecity;
            _quarter = quarter;
            _begainmonth = begainmonth;
            _endmonth = endmonth;
            _level = level;
            _salesvolume1 = salesvolume1;
            _salesvolume2 = salesvolume2;
            _bouns = bouns;
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
        ///OrganizeCity
        ///</summary>
        public int OrganizeCity
        {
            get { return _organizecity; }
            set { _organizecity = value; }
        }

        ///<summary>
        ///Quarter
        ///</summary>
        public int Quarter
        {
            get { return _quarter; }
            set { _quarter = value; }
        }

        ///<summary>
        ///BegainMonth
        ///</summary>
        public int BegainMonth
        {
            get { return _begainmonth; }
            set { _begainmonth = value; }
        }

        ///<summary>
        ///EndMonth
        ///</summary>
        public int EndMonth
        {
            get { return _endmonth; }
            set { _endmonth = value; }
        }

        ///<summary>
        ///Level
        ///</summary>
        public int Level
        {
            get { return _level; }
            set { _level = value; }
        }

        ///<summary>
        ///SalesVolume1
        ///</summary>
        public decimal SalesVolume1
        {
            get { return _salesvolume1; }
            set { _salesvolume1 = value; }
        }

        ///<summary>
        ///SalesVolume2
        ///</summary>
        public decimal SalesVolume2
        {
            get { return _salesvolume2; }
            set { _salesvolume2 = value; }
        }

        ///<summary>
        ///Bouns
        ///</summary>
        public decimal Bouns
        {
            get { return _bouns; }
            set { _bouns = value; }
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
            get { return "FNA_StaffBounsLevel"; }
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
                    case "Quarter":
                        return _quarter.ToString();
                    case "BegainMonth":
                        return _begainmonth.ToString();
                    case "EndMonth":
                        return _endmonth.ToString();
                    case "Level":
                        return _level.ToString();
                    case "SalesVolume1":
                        return _salesvolume1.ToString();
                    case "SalesVolume2":
                        return _salesvolume2.ToString();
                    case "Bouns":
                        return _bouns.ToString();
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
                    case "Quarter":
                        int.TryParse(value, out _quarter);
                        break;
                    case "BegainMonth":
                        int.TryParse(value, out _begainmonth);
                        break;
                    case "EndMonth":
                        int.TryParse(value, out _endmonth);
                        break;
                    case "Level":
                        int.TryParse(value, out _level);
                        break;
                    case "SalesVolume1":
                        decimal.TryParse(value, out _salesvolume1);
                        break;
                    case "SalesVolume2":
                        decimal.TryParse(value, out _salesvolume2);
                        break;
                    case "Bouns":
                        decimal.TryParse(value, out _bouns);
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
