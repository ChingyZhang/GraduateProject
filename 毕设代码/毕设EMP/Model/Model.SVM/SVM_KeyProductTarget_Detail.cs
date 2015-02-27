// ===================================================================
// 文件： SVM_KeyProductTarget_Detail.cs
// 项目名称：
// 创建时间：2013/4/7
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
	///SVM_KeyProductTarget_Detail数据实体类
	/// </summary>
	[Serializable]
	public class SVM_KeyProductTarget_Detail : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _targetid = 0;
		private int _product = 0;
		private decimal _amount = 0;
		private string _remark = string.Empty;
        private decimal _originalvalue = 0;
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public SVM_KeyProductTarget_Detail()
		{
		}
		
		///<summary>
		///
		///</summary>
		public SVM_KeyProductTarget_Detail(int id, int targetid, int product, decimal amount, string remark)
		{
			_id       = id;
			_targetid = targetid;
			_product  = product;
			_amount   = amount;
			_remark   = remark;
            _originalvalue = amount;
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
		///TargetID
		///</summary>
		public int TargetID
		{
			get	{ return _targetid; }
			set	{ _targetid = value; }
		}

		///<summary>
		///Product
		///</summary>
		public int Product
		{
			get	{ return _product; }
			set	{ _product = value; }
		}

		///<summary>
		///Amount
		///</summary>
		public decimal Amount
		{
			get	{ return _amount; }
			set	{ _amount = value; }
		}

		///<summary>
		///Remark
		///</summary>
		public string Remark
		{
			get	{ return _remark; }
			set	{ _remark = value; }
		}
        public decimal OriginalValue
        {
            get { return _originalvalue; }
            set { _originalvalue = value; }
        }
		#endregion
		
		public string ModelName
        {
            get { return "SVM_KeyProductTarget_Detail"; }
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
					case "TargetID":
						return _targetid.ToString();
					case "Product":
						return _product.ToString();
					case "Amount":
						return _amount.ToString();
					case "Remark":
						return _remark;
                    case "OriginalValue":
                        return _originalvalue.ToString();
					default:
						return "";
				}
			}
            set 
			{
				switch (FieldName)
                {
					case "ID":
						int.TryParse(value, out _id);
						break;
					case "TargetID":
						int.TryParse(value, out _targetid);
						break;
					case "Product":
						int.TryParse(value, out _product);
						break;
					case "Amount":
						decimal.TryParse(value, out _amount);
						break;
					case "Remark":
						_remark = value ;
                        break;
                    case "OriginalValue":
                        decimal.TryParse(value, out _originalvalue);
						break;

				}
			}
        }
		#endregion
	}
}
