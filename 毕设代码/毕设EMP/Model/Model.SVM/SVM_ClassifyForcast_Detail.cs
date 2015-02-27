// ===================================================================
// 文件： SVM_ClassifyForcast_Detail.cs
// 项目名称：
// 创建时间：2011/10/13
// 作者:	   chf
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.SVM
{
	/// <summary>
	///SVM_ClassifyForcast_Detail数据实体类
	/// </summary>
	[Serializable]
	public class SVM_ClassifyForcast_Detail : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _forcastid = 0;
		private int _classify = 0;
		private decimal _amount = 0;
        private decimal _rate = 0;

        private decimal _avgsales = 0;
		private string _remark = string.Empty;
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public SVM_ClassifyForcast_Detail()
		{
		}
		
		///<summary>
		///
		///</summary>
        public SVM_ClassifyForcast_Detail(int id, int forcastid, int classify, decimal amount, string remark, decimal rate, decimal avgsales)
		{
			_id        = id;
			_forcastid = forcastid;
			_classify  = classify;
			_amount    = amount;
			_remark    = remark;
            _rate = rate;
            _avgsales = avgsales;
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
		///ForcastID
		///</summary>
		public int ForcastID
		{
			get	{ return _forcastid; }
			set	{ _forcastid = value; }
		}

		///<summary>
		///Classify
		///</summary>
		public int Classify
		{
			get	{ return _classify; }
			set	{ _classify = value; }
		}

		///<summary>
		///Amount
		///</summary>
		public decimal Amount
		{
			get	{ return _amount; }
			set	{ _amount = value; }
		}
        /// <summary>
        /// 增长比
        /// </summary>
        public decimal Rate
        {
            get { return _rate; }
            set { _rate = value; }
        }
        /// <summary>
        /// 平均值
        /// </summary>
        public decimal AvgSales
        {
            get { return _avgsales; }
            set { _avgsales = value; }
        }
		///<summary>
		///Remark
		///</summary>
		public string Remark
		{
			get	{ return _remark; }
			set	{ _remark = value; }
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
            get { return "SVM_ClassifyForcast_Detail"; }
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
					case "ForcastID":
						return _forcastid.ToString();
					case "Classify":
						return _classify.ToString();
                    case "Rate":
                        return _rate.ToString();
					case "Amount":
						return _amount.ToString();
                    case "AvgSales":
                        return _avgsales.ToString();
					case "Remark":
						return _remark;
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
					case "ForcastID":
						int.TryParse(value, out _forcastid);
						break;
					case "Classify":
						int.TryParse(value, out _classify);
						break;
                    case "Rate":
                        decimal.TryParse(value, out _rate);
                        break;
                    case "Amount":
                        decimal.TryParse(value, out _amount);
                        break;
                    case "AvgSales":
                        decimal.TryParse(value, out _avgsales);						
						break;
					case "Remark":
						_remark = value ;
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
