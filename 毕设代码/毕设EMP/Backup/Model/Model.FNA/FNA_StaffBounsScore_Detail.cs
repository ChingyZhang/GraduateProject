// ===================================================================
// 文件： FNA_StaffBounsScore_Detail.cs
// 项目名称：
// 创建时间：2013-11-06
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
    ///FNA_StaffBounsScore_Detail数据实体类
    /// </summary>
    [Serializable]
    public class FNA_StaffBounsScore_Detail : IModel
    {
        #region 私有变量定义
        private int _id = 0;
        private int _organizecity = 0;
        private int _scoreid = 0;
        private string _itemname = string.Empty;
        private decimal _fullscore = 0;
        private decimal _itemvalue = 0;
        private int _sortid = 0;
        private int _approveflag = 0;
        private DateTime _inserttime = new DateTime(1900, 1, 1);
        private int _insertstaff = 0;
        private int _approvestaff = 0;

        private NameValueCollection _extpropertys;
        #endregion

        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public FNA_StaffBounsScore_Detail()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public FNA_StaffBounsScore_Detail(int id, int organizecity, int scoreid, string itemname, decimal fullscore,decimal itemvalue, int sortid, int approveflag, DateTime inserttime, int insertstaff, int approvestaff)
        {
            _id = id;
            _organizecity = organizecity;
            _scoreid = scoreid;
            _itemname = itemname;
            _fullscore = fullscore;
            _itemvalue = itemvalue;
            _sortid = sortid;
            _approveflag = approveflag;
            _inserttime = inserttime;
            _insertstaff = insertstaff;
            _approvestaff = approvestaff;

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
        ///ScoreID
        ///</summary>
        public int ScoreID
        {
            get { return _scoreid; }
            set { _scoreid = value; }
        }

        ///<summary>
        ///ItemName
        ///</summary>
        public string ItemName
        {
            get { return _itemname; }
            set { _itemname = value; }
        }

        public decimal FullScore
        {
            get { return _fullscore; }
            set { _fullscore = value; }
        }
        ///<summary>
        ///ItemValue
        ///</summary>
        public decimal ItemValue
        {
            get { return _itemvalue; }
            set { _itemvalue = value; }
        }

        ///<summary>
        ///SortID
        ///</summary>
        public int SortID
        {
            get { return _sortid; }
            set { _sortid = value; }
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
        ///ApproveStaff
        ///</summary>
        public int ApproveStaff
        {
            get { return _approvestaff; }
            set { _approvestaff = value; }
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
            get { return "FNA_StaffBounsScore_Detail"; }
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
                    case "ScoreID":
                        return _scoreid.ToString();
                    case "ItemName":
                        return _itemname;
                    case "FullScore":
                        return _fullscore.ToString("0.#");
                    case "ItemValue":
                        return _itemvalue.ToString("0.###");
                    case "SortID":
                        return _sortid.ToString();
                    case "ApproveFlag":
                        return _approveflag.ToString();
                    case "InsertTime":
                        return _inserttime.ToShortDateString();
                    case "InsertStaff":
                        return _insertstaff.ToString();
                    case "ApproveStaff":
                        return _approvestaff.ToString();
                    default:
                        if (_extpropertys == null)
                            return "";
                        else
                            return _extpropertys[FieldName]; return "";
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
                    case "ScoreID":
                        int.TryParse(value, out _scoreid);
                        break;
                    case "ItemName":
                        _itemname = value;
                        break;
                    case "FullScore":
                        decimal.TryParse(value,out _fullscore);
                        break;
                    case "ItemValue":
                        decimal.TryParse(value, out _itemvalue);
                        break;
                    case "SortID":
                        int.TryParse(value, out _sortid);
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
                    case "ApproveStaff":
                        int.TryParse(value, out _approvestaff);
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
