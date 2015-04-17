// ===================================================================
// 文件： PBM_DeliveryPayInfo.cs
// 项目名称：
// 创建时间：2015-03-15
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.PBM
{
	/// <summary>
	///PBM_DeliveryPayInfo数据实体类
	/// </summary>
	[Serializable]
	public class PBM_DeliveryPayInfo : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _deliveryid = 0;
		private int _paymode = 0;
		private decimal _amount = 0;
		private string _remark = string.Empty;
		private int _approveflag = 0;
		private int _insertstaff = 0;
		private DateTime _inserttime = new DateTime(1900,1,1);
		private DateTime _updatetime = new DateTime(1900,1,1);
		private int _updatestaff = 0;
		
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public PBM_DeliveryPayInfo()
		{
		}
		
		///<summary>
		///
		///</summary>
		public PBM_DeliveryPayInfo(int id, int deliveryid, int paymode, decimal amount, string remark, int approveflag, int insertstaff, DateTime inserttime, DateTime updatetime, int updatestaff)
		{
			_id           = id;
			_deliveryid   = deliveryid;
			_paymode      = paymode;
			_amount       = amount;
			_remark       = remark;
			_approveflag  = approveflag;
			_insertstaff  = insertstaff;
			_inserttime   = inserttime;
			_updatetime   = updatetime;
			_updatestaff  = updatestaff;
			
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
		///发货单ID
		///</summary>
		public int DeliveryID
		{
			get	{ return _deliveryid; }
			set	{ _deliveryid = value; }
		}

		///<summary>
		///收款方式
		///</summary>
		public int PayMode
		{
			get	{ return _paymode; }
			set	{ _paymode = value; }
		}

		///<summary>
		///收款金额
		///</summary>
		public decimal Amount
		{
			get	{ return _amount; }
			set	{ _amount = value; }
		}

		///<summary>
		///备注
		///</summary>
		public string Remark
		{
			get	{ return _remark; }
			set	{ _remark = value; }
		}

		///<summary>
		///审核标志
		///</summary>
		public int ApproveFlag
		{
			get	{ return _approveflag; }
			set	{ _approveflag = value; }
		}

		///<summary>
		///收款人
		///</summary>
		public int InsertStaff
		{
			get	{ return _insertstaff; }
			set	{ _insertstaff = value; }
		}

		///<summary>
		///新增日期
		///</summary>
		public DateTime InsertTime
		{
			get	{ return _inserttime; }
			set	{ _inserttime = value; }
		}

		///<summary>
		///更新日期
		///</summary>
		public DateTime UpdateTime
		{
			get	{ return _updatetime; }
			set	{ _updatetime = value; }
		}

		///<summary>
		///更新人
		///</summary>
		public int UpdateStaff
		{
			get	{ return _updatestaff; }
			set	{ _updatestaff = value; }
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
            get { return "PBM_DeliveryPayInfo"; }
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
					case "DeliveryID":
						return _deliveryid.ToString();
					case "PayMode":
						return _paymode.ToString();
					case "Amount":
						return _amount.ToString();
					case "Remark":
						return _remark;
					case "ApproveFlag":
						return _approveflag.ToString();
					case "InsertStaff":
						return _insertstaff.ToString();
					case "InsertTime":
						return _inserttime.ToString();
					case "UpdateTime":
						return _updatetime.ToString();
					case "UpdateStaff":
						return _updatestaff.ToString();
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
					case "DeliveryID":
						int.TryParse(value, out _deliveryid);
						break;
					case "PayMode":
						int.TryParse(value, out _paymode);
						break;
					case "Amount":
						decimal.TryParse(value, out _amount);
						break;
					case "Remark":
						_remark = value;
						break;
					case "ApproveFlag":
						int.TryParse(value, out _approveflag);
						break;
					case "InsertStaff":
						int.TryParse(value, out _insertstaff);
						break;
					case "InsertTime":
						DateTime.TryParse(value, out _inserttime);
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
