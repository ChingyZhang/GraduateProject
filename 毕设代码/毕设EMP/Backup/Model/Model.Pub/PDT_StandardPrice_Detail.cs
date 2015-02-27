// ===================================================================
// 文件： PDT_StandardPrice_Detail.cs
// 项目名称：
// 创建时间：2011/8/23
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
	///PDT_StandardPrice_Detail数据实体类
	/// </summary>
	[Serializable]
	public class PDT_StandardPrice_Detail : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _standardprice = 0;
		private int _product = 0;
		private decimal _factoryprice = 0;
		private decimal _tradeoutprice = 0;
		private decimal _tradeinprice = 0;
		private decimal _stdprice = 0;
        private decimal _rebateprice = 0;
        private decimal _direbateprice = 0;
        private int _ISFL = 0;
        private int _ISJH = 0;
        private int _ischeckjf = 0;
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public PDT_StandardPrice_Detail()
		{
		}
		
		///<summary>
		///
		///</summary>
		public PDT_StandardPrice_Detail(int id, int standardprice, int product, decimal factoryprice, decimal tradeoutprice, decimal tradeinprice, decimal stdprice)
		{
			_id            = id;
			_standardprice = standardprice;
			_product       = product;
			_factoryprice  = factoryprice;
			_tradeoutprice = tradeoutprice;
			_tradeinprice  = tradeinprice;
			_stdprice      = stdprice;
			
		}
		#endregion
		
		#region 公共属性		
		///<summary>
		///ID
		///</summary>
		public int ID
		{
			get	{ return _id; }
			set	{ _id = value; }
		}

		///<summary>
		///所属标准价表
		///</summary>
		public int StandardPrice
		{
			get	{ return _standardprice; }
			set	{ _standardprice = value; }
		}

		///<summary>
		///产品
		///</summary>
		public int Product
		{
			get	{ return _product; }
			set	{ _product = value; }
		}

		///<summary>
		///出厂价(经销价)
		///</summary>
		public decimal FactoryPrice
		{
			get	{ return _factoryprice; }
			set	{ _factoryprice = value; }
		}

		///<summary>
        ///分销价
		///</summary>
		public decimal TradeOutPrice
		{
			get	{ return _tradeoutprice; }
			set	{ _tradeoutprice = value; }
		}

		///<summary>
        ///零供价
		///</summary>
		public decimal TradeInPrice
		{
			get	{ return _tradeinprice; }
			set	{ _tradeinprice = value; }
		}

		///<summary>
        ///零售价
		///</summary>
		public decimal StdPrice
		{
			get	{ return _stdprice; }
			set	{ _stdprice = value; }
		}
        /// <summary>
        /// 返利
        /// </summary>
        public decimal RebatePrice
        {
            get { return _rebateprice; }
            set { _rebateprice = value; }
        }
        /// <summary>
        /// 经销商返利
        /// </summary>
        public decimal DIRebatePrice
        {
            get { return _direbateprice; }
            set { _direbateprice = value; }
        }
        /// <summary>
        /// 是否返利
        /// </summary>
        public int ISFL
        {
            get { return _ISFL; }
            set { _ISFL = value; }
        }
        /// <summary>
        /// 是否进货
        /// </summary>
        public int ISJH
        {
            get { return _ISJH; }
            set { _ISJH = value; }
        }

        public int ISCheckJF
        {
            get { return _ischeckjf; }
            set { _ischeckjf = value; }
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
            get { return "PDT_StandardPrice_Detail"; }
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
					case "StandardPrice":
						return _standardprice.ToString();
					case "Product":
						return _product.ToString();
					case "FactoryPrice":
						return _factoryprice.ToString();
					case "TradeOutPrice":
						return _tradeoutprice.ToString();
					case "TradeInPrice":
						return _tradeinprice.ToString();
					case "StdPrice":
						return _stdprice.ToString();
                    case "RebatePrice":
                        return _rebateprice.ToString();
                    case "DIRebatePrice":
                        return _direbateprice.ToString();
                    case "ISFL":
                        return _ISFL.ToString();
                    case "ISJH":
                        return _ISJH.ToString();
                    case "ISCheckJF":
                        return _ischeckjf.ToString();
					default:
						if (_extpropertys==null)
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
					case "StandardPrice":
						int.TryParse(value, out _standardprice);
						break;
					case "Product":
						int.TryParse(value, out _product);
						break;
					case "FactoryPrice":
						decimal.TryParse(value, out _factoryprice);
						break;
					case "TradeOutPrice":
						decimal.TryParse(value, out _tradeoutprice);
						break;
					case "TradeInPrice":
						decimal.TryParse(value, out _tradeinprice);
						break;
					case "StdPrice":
						decimal.TryParse(value, out _stdprice);
						break;
                    case "RebatePrice":
                        decimal.TryParse(value, out _rebateprice);
                        break;
                    case "DIRebatePrice":
                        decimal.TryParse(value, out _direbateprice);
                        break;
                    case "ISFL":
                        int.TryParse(value, out _ISFL);
                        break;
                    case "ISJH":
                        int.TryParse(value, out _ISJH);
                        break;
                    case "ISCheckJF":
                        int.TryParse(value, out _ischeckjf);
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
