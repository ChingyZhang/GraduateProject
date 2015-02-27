// ===================================================================
// 文件： PM_PromotorNumberLimit.cs
// 项目名称：
// 创建时间：2010/7/22
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.Promotor
{
	/// <summary>
	///PM_PromotorNumberLimit数据实体类
	/// </summary>
	[Serializable]
	public class PM_PromotorNumberLimit : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _organizecity = 0;
		private int _classify = 0;
		private int _numberlimit = 0;
		private int _budgetnumber = 0;
		private string _remark = string.Empty;
		private int _insertstaff = 0;
		private DateTime _inserttime = new DateTime(1900,1,1);
		private int _updatestaff = 0;
		private DateTime _updatetime = new DateTime(1900,1,1);
		
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public PM_PromotorNumberLimit()
		{
		}
		
		///<summary>
		///
		///</summary>
		public PM_PromotorNumberLimit(int id, int organizecity, int classify, int numberlimit, int budgetnumber, string remark, int insertstaff, DateTime inserttime, int updatestaff, DateTime updatetime)
		{
			_id           = id;
			_organizecity = organizecity;
			_classify     = classify;
			_numberlimit  = numberlimit;
			_budgetnumber = budgetnumber;
			_remark       = remark;
			_insertstaff  = insertstaff;
			_inserttime   = inserttime;
			_updatestaff  = updatestaff;
			_updatetime   = updatetime;
			
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
		///OrganizeCity
		///</summary>
		public int OrganizeCity
		{
			get	{ return _organizecity; }
			set	{ _organizecity = value; }
		}

		///<summary>
		///促销员类别
		///</summary>
		public int Classify
		{
			get	{ return _classify; }
			set	{ _classify = value; }
		}

		///<summary>
		///NumberLimit
		///</summary>
		public int NumberLimit
		{
			get	{ return _numberlimit; }
			set	{ _numberlimit = value; }
		}

		///<summary>
		///BudgetNumber
		///</summary>
		public int BudgetNumber
		{
			get	{ return _budgetnumber; }
			set	{ _budgetnumber = value; }
		}

		///<summary>
		///Remark
		///</summary>
		public string Remark
		{
			get	{ return _remark; }
			set	{ _remark = value; }
		}

		///<summary>
		///InsertStaff
		///</summary>
		public int InsertStaff
		{
			get	{ return _insertstaff; }
			set	{ _insertstaff = value; }
		}

		///<summary>
		///InsertTime
		///</summary>
		public DateTime InsertTime
		{
			get	{ return _inserttime; }
			set	{ _inserttime = value; }
		}

		///<summary>
		///UpdateStaff
		///</summary>
		public int UpdateStaff
		{
			get	{ return _updatestaff; }
			set	{ _updatestaff = value; }
		}

		///<summary>
		///UpdateTime
		///</summary>
		public DateTime UpdateTime
		{
			get	{ return _updatetime; }
			set	{ _updatetime = value; }
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
            get { return "PM_PromotorNumberLimit"; }
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
					case "Classify":
						return _classify.ToString();
					case "NumberLimit":
						return _numberlimit.ToString();
					case "BudgetNumber":
						return _budgetnumber.ToString();
					case "Remark":
						return _remark;
					case "InsertStaff":
						return _insertstaff.ToString();
					case "InsertTime":
						return _inserttime.ToString();
					case "UpdateStaff":
						return _updatestaff.ToString();
					case "UpdateTime":
						return _updatetime.ToString();
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
					case "OrganizeCity":
						int.TryParse(value, out _organizecity);
						break;
					case "Classify":
						int.TryParse(value, out _classify);
						break;
					case "NumberLimit":
						int.TryParse(value, out _numberlimit);
						break;
					case "BudgetNumber":
						int.TryParse(value, out _budgetnumber);
						break;
					case "Remark":
						_remark = value ;
						break;
					case "InsertStaff":
						int.TryParse(value, out _insertstaff);
						break;
					case "InsertTime":
						DateTime.TryParse(value, out _inserttime);
						break;
					case "UpdateStaff":
						int.TryParse(value, out _updatestaff);
						break;
					case "UpdateTime":
						DateTime.TryParse(value, out _updatetime);
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
