// ===================================================================
// 文件： PM_StdInsuranceCostInCity.cs
// 项目名称：
// 创建时间：2014/2/24
// 作者:	   Jace
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.Promotor
{
    /// <summary>
    ///PM_StdInsuranceCostInCity数据实体类
    /// </summary>
    [Serializable]
    public class PM_StdInsuranceCostInCity : IModel
    {
        #region 私有变量定义
        private int _id = 0;
        private int _insurance = 0;
        private int _city = 0;
        private DateTime _inserttime = new DateTime(1900, 1, 1);
        private int _insertstaff = 0;

        private NameValueCollection _extpropertys;
        #endregion

        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public PM_StdInsuranceCostInCity()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public PM_StdInsuranceCostInCity(int id, int insurance, int city, DateTime inserttime, int insertstaff)
        {
            _id = id;
            _insurance = insurance;
            _city = city;
            _inserttime = inserttime;
            _insertstaff = insertstaff;

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
        ///Insurance
        ///</summary>
        public int Insurance
        {
            get { return _insurance; }
            set { _insurance = value; }
        }

        ///<summary>
        ///City
        ///</summary>
        public int City
        {
            get { return _city; }
            set { _city = value; }
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
            get { return "PM_StdInsuranceCostInCity"; }
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
                    case "Insurance":
                        return _insurance.ToString();
                    case "City":
                        return _city.ToString();
                    case "InsertTime":
                        return _inserttime.ToString();
                    case "InsertStaff":
                        return _insertstaff.ToString();
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
                    case "Insurance":
                        int.TryParse(value, out _insurance);
                        break;
                    case "City":
                        int.TryParse(value, out _city);
                        break;
                    case "InsertTime":
                        DateTime.TryParse(value, out _inserttime);
                        break;
                    case "InsertStaff":
                        int.TryParse(value, out _insertstaff);
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
