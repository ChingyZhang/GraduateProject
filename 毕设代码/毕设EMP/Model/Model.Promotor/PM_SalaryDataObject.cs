// ===================================================================
// 文件： PM_SalaryDataObject.cs
// 项目名称：
// 创建时间：2011/11/17
// 作者:	   chf
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.Promotor
{
    /// <summary>
    ///PM_SalaryDataObject数据实体类
    /// </summary>
    [Serializable]
    public class PM_SalaryDataObject : IModel
    {
        #region 私有变量定义
        private int _id = 0;
        private int _promotor = 0;
        private int _accountmonth = 0;
        private decimal _salestarget = 0;
        private decimal _salesbase = 0;
        private int _actworkdays = 0;
        private DateTime _inserttime = new DateTime(1900, 1, 1);
        private int _insertstaff = 0;
        private DateTime _updatetime = new DateTime(1900, 1, 1);
        private int _updatestaff = 0;
        private int _approveflag = 0;
        private decimal _data01 = 0;
        private decimal _data02 = 0;
        private decimal _data03 = 0;
        private decimal _data04 = 0;
        private decimal _data05 = 0;
        private decimal _data06 = 0;
        private decimal _data07 = 0;
        private decimal _data08 = 0;
        private decimal _data09 = 0;
        private decimal _data10 = 0;
        private decimal _data11 = 0;
        private decimal _data12 = 0;
        private decimal _data13 = 0;
        private decimal _data14 = 0;
        private decimal _data15 = 0;
        private decimal _data16 = 0;
        private decimal _data17 = 0;
        private decimal _data18 = 0;
        private decimal _data19 = 0;
        private decimal _data20 = 0;
        private decimal _data21 = 0;
        private decimal _data22 = 0;
        private decimal _data23 = 0;
        private decimal _data24 = 0;
        private decimal _data25 = 0;
        private decimal _data26 = 0;
        private decimal _data27 = 0;
        private decimal _data28 = 0;
        private decimal _data29 = 0;
        private decimal _data30 = 0;
        private NameValueCollection _extpropertys;
        #endregion

        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public PM_SalaryDataObject()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public PM_SalaryDataObject(int id, int promotor, int accountmonth, decimal salestarget, decimal salesbase, int actworkdays,int approveflag, DateTime inserttime, int insertstaff, DateTime updatetime, int updatestaff, decimal data01, decimal data02, decimal data03, decimal data04, decimal data05, decimal data06, decimal data07, decimal data08, decimal data09, decimal data10, decimal data11, decimal data12, decimal data13, decimal data14, decimal data15, decimal data16, decimal data17, decimal data18, decimal data19, decimal data20, decimal data21, decimal data22, decimal data23, decimal data24, decimal data25, decimal data26, decimal data27, decimal data28, decimal data29, decimal data30)
        {
            _id = id;
            _promotor = promotor;
            _accountmonth = accountmonth;
            _salestarget = salestarget;
            _salesbase = salesbase;
            _actworkdays = actworkdays;
            _inserttime = inserttime;
            _insertstaff = insertstaff;
            _updatetime = updatetime;
            _updatestaff = updatestaff;
            _approveflag = approveflag;
            _data01 = data01;
            _data02 = data02;
            _data03 = data03;
            _data04 = data04;
            _data05 = data05;
            _data06 = data06;
            _data07 = data07;
            _data08 = data08;
            _data09 = data09;
            _data10 = data10;
            _data11 = data11;
            _data12 = data12;
            _data13 = data13;
            _data14 = data14;
            _data15 = data15;
            _data16 = data16;
            _data17 = data17;
            _data18 = data18;
            _data19 = data19;
            _data20 = data20;
            _data21 = data21;
            _data22 = data22;
            _data23 = data23;
            _data24 = data24;
            _data25 = data25;
            _data26 = data26;
            _data27 = data27;
            _data28 = data28;
            _data29 = data29;
            _data30 = data30;

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
        ///Promotor
        ///</summary>
        public int Promotor
        {
            get { return _promotor; }
            set { _promotor = value; }
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
        ///SalesTarget
        ///</summary>
        public decimal SalesTarget
        {
            get { return _salestarget; }
            set { _salestarget = value; }
        }

        ///<summary>
        ///SalesBase
        ///</summary>
        public decimal SalesBase
        {
            get { return _salesbase; }
            set { _salesbase = value; }
        }

        ///<summary>
        ///ActWorkDays
        ///</summary>
        public int ActWorkDays
        {
            get { return _actworkdays; }
            set { _actworkdays = value; }
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
        ///<summary>
        ///Data01
        ///</summary>
        public decimal Data01
        {
            get { return _data01; }
            set { _data01 = value; }
        }

        ///<summary>
        ///Data02
        ///</summary>
        public decimal Data02
        {
            get { return _data02; }
            set { _data02 = value; }
        }

        ///<summary>
        ///Data03
        ///</summary>
        public decimal Data03
        {
            get { return _data03; }
            set { _data03 = value; }
        }

        ///<summary>
        ///Data04
        ///</summary>
        public decimal Data04
        {
            get { return _data04; }
            set { _data04 = value; }
        }

        ///<summary>
        ///Data05
        ///</summary>
        public decimal Data05
        {
            get { return _data05; }
            set { _data05 = value; }
        }

        ///<summary>
        ///Data06
        ///</summary>
        public decimal Data06
        {
            get { return _data06; }
            set { _data06 = value; }
        }

        ///<summary>
        ///Data07
        ///</summary>
        public decimal Data07
        {
            get { return _data07; }
            set { _data07 = value; }
        }

        ///<summary>
        ///Data08
        ///</summary>
        public decimal Data08
        {
            get { return _data08; }
            set { _data08 = value; }
        }

        ///<summary>
        ///Data09
        ///</summary>
        public decimal Data09
        {
            get { return _data09; }
            set { _data09 = value; }
        }

        ///<summary>
        ///Data10
        ///</summary>
        public decimal Data10
        {
            get { return _data10; }
            set { _data10 = value; }
        }

        ///<summary>
        ///Data11
        ///</summary>
        public decimal Data11
        {
            get { return _data11; }
            set { _data11 = value; }
        }

        ///<summary>
        ///Data12
        ///</summary>
        public decimal Data12
        {
            get { return _data12; }
            set { _data12 = value; }
        }

        ///<summary>
        ///Data13
        ///</summary>
        public decimal Data13
        {
            get { return _data13; }
            set { _data13 = value; }
        }

        ///<summary>
        ///Data14
        ///</summary>
        public decimal Data14
        {
            get { return _data14; }
            set { _data14 = value; }
        }

        ///<summary>
        ///Data15
        ///</summary>
        public decimal Data15
        {
            get { return _data15; }
            set { _data15 = value; }
        }

        ///<summary>
        ///Data16
        ///</summary>
        public decimal Data16
        {
            get { return _data16; }
            set { _data16 = value; }
        }

        ///<summary>
        ///Data17
        ///</summary>
        public decimal Data17
        {
            get { return _data17; }
            set { _data17 = value; }
        }

        ///<summary>
        ///Data18
        ///</summary>
        public decimal Data18
        {
            get { return _data18; }
            set { _data18 = value; }
        }

        ///<summary>
        ///Data19
        ///</summary>
        public decimal Data19
        {
            get { return _data19; }
            set { _data19 = value; }
        }

        ///<summary>
        ///Data20
        ///</summary>
        public decimal Data20
        {
            get { return _data20; }
            set { _data20 = value; }
        }

        ///<summary>
        ///Data21
        ///</summary>
        public decimal Data21
        {
            get { return _data21; }
            set { _data21 = value; }
        }

        ///<summary>
        ///Data22
        ///</summary>
        public decimal Data22
        {
            get { return _data22; }
            set { _data22 = value; }
        }

        ///<summary>
        ///Data23
        ///</summary>
        public decimal Data23
        {
            get { return _data23; }
            set { _data23 = value; }
        }

        ///<summary>
        ///Data24
        ///</summary>
        public decimal Data24
        {
            get { return _data24; }
            set { _data24 = value; }
        }

        ///<summary>
        ///Data25
        ///</summary>
        public decimal Data25
        {
            get { return _data25; }
            set { _data25 = value; }
        }

        ///<summary>
        ///Data26
        ///</summary>
        public decimal Data26
        {
            get { return _data26; }
            set { _data26 = value; }
        }

        ///<summary>
        ///Data27
        ///</summary>
        public decimal Data27
        {
            get { return _data27; }
            set { _data27 = value; }
        }

        ///<summary>
        ///Data28
        ///</summary>
        public decimal Data28
        {
            get { return _data28; }
            set { _data28 = value; }
        }

        ///<summary>
        ///Data29
        ///</summary>
        public decimal Data29
        {
            get { return _data29; }
            set { _data29 = value; }
        }

        ///<summary>
        ///Data30
        ///</summary>
        public decimal Data30
        {
            get { return _data30; }
            set { _data30 = value; }
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
            get { return "PM_SalaryDataObject"; }
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
                    case "Promotor":
                        return _promotor.ToString();
                    case "AccountMonth":
                        return _accountmonth.ToString();
                    case "SalesTarget":
                        return _salestarget.ToString();
                    case "SalesBase":
                        return _salesbase.ToString();
                    case "ActWorkDays":
                        return _actworkdays.ToString();
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
                    case "Data01":
                        return _data01.ToString();
                    case "Data02":
                        return _data02.ToString();
                    case "Data03":
                        return _data03.ToString();
                    case "Data04":
                        return _data04.ToString();
                    case "Data05":
                        return _data05.ToString();
                    case "Data06":
                        return _data06.ToString();
                    case "Data07":
                        return _data07.ToString();
                    case "Data08":
                        return _data08.ToString();
                    case "Data09":
                        return _data09.ToString();
                    case "Data10":
                        return _data10.ToString();
                    case "Data11":
                        return _data11.ToString();
                    case "Data12":
                        return _data12.ToString();
                    case "Data13":
                        return _data13.ToString();
                    case "Data14":
                        return _data14.ToString();
                    case "Data15":
                        return _data15.ToString();
                    case "Data16":
                        return _data16.ToString();
                    case "Data17":
                        return _data17.ToString();
                    case "Data18":
                        return _data18.ToString();
                    case "Data19":
                        return _data19.ToString();
                    case "Data20":
                        return _data20.ToString();
                    case "Data21":
                        return _data21.ToString();
                    case "Data22":
                        return _data22.ToString();
                    case "Data23":
                        return _data23.ToString();
                    case "Data24":
                        return _data24.ToString();
                    case "Data25":
                        return _data25.ToString();
                    case "Data26":
                        return _data26.ToString();
                    case "Data27":
                        return _data27.ToString();
                    case "Data28":
                        return _data28.ToString();
                    case "Data29":
                        return _data29.ToString();
                    case "Data30":
                        return _data30.ToString();
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
                    case "Promotor":
                        int.TryParse(value, out _promotor);
                        break;
                    case "AccountMonth":
                        int.TryParse(value, out _accountmonth);
                        break;
                    case "SalesTarget":
                        decimal.TryParse(value, out _salestarget);
                        break;
                    case "SalesBase":
                        decimal.TryParse(value, out _salesbase);
                        break;
                    case "ActWorkDays":
                        int.TryParse(value, out _actworkdays);
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
                    case "ApproveFlag":
                        int.TryParse(value, out _approveflag);
                        break;
                    case "Data01":
                        decimal.TryParse(value, out _data01);
                        break;
                    case "Data02":
                        decimal.TryParse(value, out _data02);
                        break;
                    case "Data03":
                        decimal.TryParse(value, out _data03);
                        break;
                    case "Data04":
                        decimal.TryParse(value, out _data04);
                        break;
                    case "Data05":
                        decimal.TryParse(value, out _data05);
                        break;
                    case "Data06":
                        decimal.TryParse(value, out _data06);
                        break;
                    case "Data07":
                        decimal.TryParse(value, out _data07);
                        break;
                    case "Data08":
                        decimal.TryParse(value, out _data08);
                        break;
                    case "Data09":
                        decimal.TryParse(value, out _data09);
                        break;
                    case "Data10":
                        decimal.TryParse(value, out _data10);
                        break;
                    case "Data11":
                        decimal.TryParse(value, out _data11);
                        break;
                    case "Data12":
                        decimal.TryParse(value, out _data12);
                        break;
                    case "Data13":
                        decimal.TryParse(value, out _data13);
                        break;
                    case "Data14":
                        decimal.TryParse(value, out _data14);
                        break;
                    case "Data15":
                        decimal.TryParse(value, out _data15);
                        break;
                    case "Data16":
                        decimal.TryParse(value, out _data16);
                        break;
                    case "Data17":
                        decimal.TryParse(value, out _data17);
                        break;
                    case "Data18":
                        decimal.TryParse(value, out _data18);
                        break;
                    case "Data19":
                        decimal.TryParse(value, out _data19);
                        break;
                    case "Data20":
                        decimal.TryParse(value, out _data20);
                        break;
                    case "Data21":
                        decimal.TryParse(value, out _data21);
                        break;
                    case "Data22":
                        decimal.TryParse(value, out _data22);
                        break;
                    case "Data23":
                        decimal.TryParse(value, out _data23);
                        break;
                    case "Data24":
                        decimal.TryParse(value, out _data24);
                        break;
                    case "Data25":
                        decimal.TryParse(value, out _data25);
                        break;
                    case "Data26":
                        decimal.TryParse(value, out _data26);
                        break;
                    case "Data27":
                        decimal.TryParse(value, out _data27);
                        break;
                    case "Data28":
                        decimal.TryParse(value, out _data28);
                        break;
                    case "Data29":
                        decimal.TryParse(value, out _data29);
                        break;
                    case "Data30":
                        decimal.TryParse(value, out _data30);
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
