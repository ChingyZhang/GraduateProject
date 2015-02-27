// ===================================================================
// 文件： FNA_StaffBounsLevelDetail.cs
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
    ///FNA_StaffBounsLevelDetail数据实体类
    /// </summary>
    [Serializable]
    public class FNA_StaffBounsLevelDetail : IModel
    {
        #region 私有变量定义
        private int _id = 0;
        private int _organizecity = 0;
        private int _accountquarter = 0;
        private decimal _salesvolume = 0;
        private decimal _salesadjust = 0;
        private int _level = 0;
        private decimal _bouns = 0;
        private string _remark = string.Empty;
        private decimal _budgetfeerate = 0;
        private decimal _actfeerate = 0;
        private DateTime _inserttime = new DateTime(1900, 1, 1);
        private int _insertstaff = 0;
        private DateTime _updatetime = new DateTime(1900, 1, 1);
        private int _updatestaff = 0;
        private int _approveflag = 0;


        private NameValueCollection _extpropertys;
        #endregion

        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public FNA_StaffBounsLevelDetail()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public FNA_StaffBounsLevelDetail(int id, int organizecity, int accountquarter, decimal salesvolume, decimal salesadjust, int level, decimal bouns, string remark, decimal budgetfeerate, decimal actfeerate, DateTime inserttime, int insertstaff, DateTime updatetime, int updatestaff)
        {
            _id = id;
            _organizecity = organizecity;
            _accountquarter = accountquarter;
            _salesvolume = salesvolume;
            _salesadjust = salesadjust;
            _level = level;
            _bouns = bouns;
            _remark = remark;
            _budgetfeerate = budgetfeerate;
            _actfeerate = actfeerate;
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

        /// <summary>
        /// AccountQuarter
        /// </summary>
        public int AccountQuarter
        {
            get { return _accountquarter; }
            set { _accountquarter = value; }
        }

        ///<summary>
        ///SalesVolume
        ///</summary>
        public decimal SalesVolume
        {
            get { return _salesvolume; }
            set { _salesvolume = value; }
        }

        ///<summary>
        ///SalesAdjust
        ///</summary>
        public decimal SalesAdjust
        {
            get { return _salesadjust; }
            set { _salesadjust = value; }
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
        ///Bouns
        ///</summary>
        public decimal Bouns
        {
            get { return _bouns; }
            set { _bouns = value; }
        }

        ///<summary>
        ///Remark
        ///</summary>
        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }


        /// <summary>
        /// BudgetFeeRate
        /// </summary>
        public decimal BudgetFeeRate
        {
            get { return _budgetfeerate; }
            set { _budgetfeerate = value; }
        }

        /// <summary>
        /// ActFeeRate
        /// </summary>
        public decimal ActFeeRate
        {
            get { return _actfeerate; }
            set { _actfeerate = value; }
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
            get { return "FNA_StaffBounsLevelDetail"; }
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
                    case "AccountQuarter":
                        return _accountquarter.ToString();
                    case "SalesVolume":
                        return _salesvolume.ToString();
                    case "SalesAdjust":
                        return _salesadjust.ToString();
                    case "Level":
                        return _level.ToString();
                    case "Bouns":
                        return _bouns.ToString();
                    case "Remark":
                        return _remark;
                    case "BudgetFeeRate":
                        return _budgetfeerate.ToString();
                    case "ActFeeRate":
                        return _actfeerate.ToString();
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
                    case "AccountQuarter":
                        int.TryParse(value, out _accountquarter);
                        break;
                    case "SalesVolume":
                        decimal.TryParse(value, out _salesvolume);
                        break;
                    case "SalesAdjust":
                        decimal.TryParse(value, out _salesadjust);
                        break;
                    case "Level":
                        int.TryParse(value, out _level);
                        break;
                    case "Bouns":
                        decimal.TryParse(value, out _bouns);
                        break;
                    case "Remark":
                        _remark = value;
                        break;
                    case "BudgetFeeRate":
                        decimal.TryParse(value, out _budgetfeerate);
                        break;
                    case "ActFeeRate":
                        decimal.TryParse(value, out _actfeerate);
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
