// ===================================================================
// 文件： ORD_OrderSpentPoints.cs
// 项目名称：
// 创建时间：2013-12-21
// 作者:	   ShenGang
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.EBM
{
	/// <summary>
	///ORD_OrderSpentPoints数据实体类
	/// </summary>
	[Serializable]
	public class ORD_OrderSpentPoints : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _orderid = 0;
		private int _accounttype = 0;
		private int _flag = 0;
		private decimal _points = 0;
		private DateTime _inserttime = new DateTime(1900,1,1);
		
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public ORD_OrderSpentPoints()
		{
		}
		
		///<summary>
		///
		///</summary>
		public ORD_OrderSpentPoints(int id, int orderid, int accounttype, int flag, decimal points, DateTime inserttime)
		{
			_id           = id;
			_orderid      = orderid;
			_accounttype  = accounttype;
			_flag         = flag;
			_points       = points;
			_inserttime   = inserttime;
			
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
		///所属订单
		///</summary>
		public int OrderID
		{
			get	{ return _orderid; }
			set	{ _orderid = value; }
		}

		///<summary>
		///账户类型
		///</summary>
		public int AccountType
		{
			get	{ return _accounttype; }
			set	{ _accounttype = value; }
		}

		///<summary>
		///标志(1:扣减 2:返还)
		///</summary>
		public int Flag
		{
			get	{ return _flag; }
			set	{ _flag = value; }
		}

		///<summary>
		///积分值
		///</summary>
		public decimal Points
		{
			get	{ return _points; }
			set	{ _points = value; }
		}

		///<summary>
		///操作时间
		///</summary>
		public DateTime InsertTime
		{
			get	{ return _inserttime; }
			set	{ _inserttime = value; }
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
            get { return "ORD_OrderSpentPoints"; }
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
					case "OrderID":
						return _orderid.ToString();
					case "AccountType":
						return _accounttype.ToString();
					case "Flag":
						return _flag.ToString();
					case "Points":
						return _points.ToString();
					case "InsertTime":
						return _inserttime.ToString();
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
					case "OrderID":
						int.TryParse(value, out _orderid);
						break;
					case "AccountType":
						int.TryParse(value, out _accounttype);
						break;
					case "Flag":
						int.TryParse(value, out _flag);
						break;
					case "Points":
						decimal.TryParse(value, out _points);
						break;
					case "InsertTime":
						DateTime.TryParse(value, out _inserttime);
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
