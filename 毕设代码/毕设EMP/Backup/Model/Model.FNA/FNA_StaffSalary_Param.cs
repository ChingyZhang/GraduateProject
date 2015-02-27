// ===================================================================
// 文件： FNA_StaffSalary_Param.cs
// 项目名称：
// 创建时间：2014/2/18
// 作者:	   chf
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.FNA
{
    /// <summary>
    ///FNA_StaffSalary_Param数据实体类
    /// </summary>
    [Serializable]
    public class FNA_StaffSalary_Param : IModel
    {
        #region 私有变量定义
        private int _id = 0;
        private int _position = 0;
        private decimal _salesrateulimit = 0;
        private decimal _salesratellimit = 0;
        private decimal _salesrateweight = 0;
        private decimal _saleskeylevel = 0;
        private decimal _saleskeyrateulevelulimit = 0;
        private decimal _saleskeyratellevelulimit = 0;
        private decimal _saleskeyratellimit = 0;
        private decimal _saleskeyrateweight = 0;
        private decimal _feerateulimit = 0;
        private decimal _feeratellimit = 0;
        private decimal _feerateweight = 0;
        private decimal _data01 = 0;
        private decimal _data02 = 0;
        private decimal _data03 = 0;
        private decimal _data04 = 0;
        private decimal _data05 = 0;

        private NameValueCollection _extpropertys;
        #endregion

        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public FNA_StaffSalary_Param()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public FNA_StaffSalary_Param(int id, int position, decimal salesrateulimit, decimal salesratellimit, decimal salesrateweight, decimal saleskeylevel, decimal saleskeyrateulevelulimit, decimal saleskeyratellevelulimit, decimal saleskeyratellimit, decimal saleskeyrateweight, decimal feerateulimit, decimal feeratellimit, decimal feerateweight, decimal data01, decimal data02, decimal data03, decimal data04, decimal data05)
        {
            _id = id;
            _position = position;
            _salesrateulimit = salesrateulimit;
            _salesratellimit = salesratellimit;
            _salesrateweight = salesrateweight;
            _saleskeylevel = saleskeylevel;
            _saleskeyrateulevelulimit = saleskeyrateulevelulimit;
            _saleskeyratellevelulimit = saleskeyratellevelulimit;
            _saleskeyratellimit = saleskeyratellimit;
            _saleskeyrateweight = saleskeyrateweight;
            _feerateulimit = feerateulimit;
            _feeratellimit = feeratellimit;
            _feerateweight = feerateweight;
            _data01 = data01;
            _data02 = data02;
            _data03 = data03;
            _data04 = data04;
            _data05 = data05;

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
        ///职位
        ///</summary>
        public int Position
        {
            get { return _position; }
            set { _position = value; }
        }

        ///<summary>
        ///销售业绩达成率上限
        ///</summary>
        public decimal SalesRateULimit
        {
            get { return _salesrateulimit; }
            set { _salesrateulimit = value; }
        }

        ///<summary>
        ///销售业绩达成下限
        ///</summary>
        public decimal SalesRateLLimit
        {
            get { return _salesratellimit; }
            set { _salesratellimit = value; }
        }

        ///<summary>
        ///销售业绩占比权重
        ///</summary>
        public decimal SalesRateWeight
        {
            get { return _salesrateweight; }
            set { _salesrateweight = value; }
        }

        ///<summary>
        ///重品销售坎级
        ///</summary>
        public decimal SalesKeyLevel
        {
            get { return _saleskeylevel; }
            set { _saleskeylevel = value; }
        }

        ///<summary>
        ///重品达成率高于坎级上限
        ///</summary>
        public decimal SalesKeyRateULevelULimit
        {
            get { return _saleskeyrateulevelulimit; }
            set { _saleskeyrateulevelulimit = value; }
        }

        ///<summary>
        ///重品达成率低于坎级上限
        ///</summary>
        public decimal SalesKeyRateLLevelULimit
        {
            get { return _saleskeyratellevelulimit; }
            set { _saleskeyratellevelulimit = value; }
        }

        ///<summary>
        ///重品达成下限
        ///</summary>
        public decimal SalesKeyRateLLimit
        {
            get { return _saleskeyratellimit; }
            set { _saleskeyratellimit = value; }
        }

        ///<summary>
        ///重品达成占比权重
        ///</summary>
        public decimal SalesKeyRateWeight
        {
            get { return _saleskeyrateweight; }
            set { _saleskeyrateweight = value; }
        }

        ///<summary>
        ///费率达成上限
        ///</summary>
        public decimal FeeRateULimit
        {
            get { return _feerateulimit; }
            set { _feerateulimit = value; }
        }

        ///<summary>
        ///费率达成下限
        ///</summary>
        public decimal FeeRateLLimit
        {
            get { return _feeratellimit; }
            set { _feeratellimit = value; }
        }

        ///<summary>
        ///FeeRateWeight
        ///</summary>
        public decimal FeeRateWeight
        {
            get { return _feerateweight; }
            set { _feerateweight = value; }
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
            get { return "FNA_StaffSalary_Param"; }
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
                    case "Position":
                        return _position.ToString();
                    case "SalesRateULimit":
                        return _salesrateulimit.ToString();
                    case "SalesRateLLimit":
                        return _salesratellimit.ToString();
                    case "SalesRateWeight":
                        return _salesrateweight.ToString();
                    case "SalesKeyLevel":
                        return _saleskeylevel.ToString();
                    case "SalesKeyRateULevelULimit":
                        return _saleskeyrateulevelulimit.ToString();
                    case "SalesKeyRateLLevelULimit":
                        return _saleskeyratellevelulimit.ToString();
                    case "SalesKeyRateLLimit":
                        return _saleskeyratellimit.ToString();
                    case "SalesKeyRateWeight":
                        return _saleskeyrateweight.ToString();
                    case "FeeRateULimit":
                        return _feerateulimit.ToString();
                    case "FeeRateLLimit":
                        return _feeratellimit.ToString();
                    case "FeeRateWeight":
                        return _feerateweight.ToString();
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
                    case "Position":
                        int.TryParse(value, out _position);
                        break;
                    case "SalesRateULimit":
                        decimal.TryParse(value, out _salesrateulimit);
                        break;
                    case "SalesRateLLimit":
                        decimal.TryParse(value, out _salesratellimit);
                        break;
                    case "SalesRateWeight":
                        decimal.TryParse(value, out _salesrateweight);
                        break;
                    case "SalesKeyLevel":
                        decimal.TryParse(value, out _saleskeylevel);
                        break;
                    case "SalesKeyRateULevelULimit":
                        decimal.TryParse(value, out _saleskeyrateulevelulimit);
                        break;
                    case "SalesKeyRateLLevelULimit":
                        decimal.TryParse(value, out _saleskeyratellevelulimit);
                        break;
                    case "SalesKeyRateLLimit":
                        decimal.TryParse(value, out _saleskeyratellimit);
                        break;
                    case "SalesKeyRateWeight":
                        decimal.TryParse(value, out _saleskeyrateweight);
                        break;
                    case "FeeRateULimit":
                        decimal.TryParse(value, out _feerateulimit);
                        break;
                    case "FeeRateLLimit":
                        decimal.TryParse(value, out _feeratellimit);
                        break;
                    case "FeeRateWeight":
                        decimal.TryParse(value, out _feerateweight);
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
