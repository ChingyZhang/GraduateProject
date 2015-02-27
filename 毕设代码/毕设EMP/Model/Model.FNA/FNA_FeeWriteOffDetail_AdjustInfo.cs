// ===================================================================
// 文件： FNA_FeeWriteOffDetail_AdjustInfo.cs
// 项目名称：
// 创建时间：2013-01-26
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.FNA
{
	/// <summary>
	///FNA_FeeWriteOffDetail_AdjustInfo数据实体类
	/// </summary>
	[Serializable]
	public class FNA_FeeWriteOffDetail_AdjustInfo : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _writeoffdetailid = 0;
		private int _adjustmode = 0;
		private decimal _adjustcost = 0;
		private string _adjustreason = string.Empty;
		private int _insertstaff = 0;
		private DateTime _inserttime = new DateTime(1900,1,1);
		private string _remark = string.Empty;
		
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public FNA_FeeWriteOffDetail_AdjustInfo()
		{
		}
		
		///<summary>
		///
		///</summary>
		public FNA_FeeWriteOffDetail_AdjustInfo(int id, int writeoffdetailid, int adjustmode, decimal adjustcost, string adjustreason, int insertstaff, DateTime inserttime, string remark)
		{
			_id               = id;
			_writeoffdetailid = writeoffdetailid;
			_adjustmode       = adjustmode;
			_adjustcost       = adjustcost;
			_adjustreason     = adjustreason;
			_insertstaff      = insertstaff;
			_inserttime       = inserttime;
			_remark           = remark;
			
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
		///明细ID
		///</summary>
		public int WriteOffDetailID
		{
			get	{ return _writeoffdetailid; }
			set	{ _writeoffdetailid = value; }
		}

		///<summary>
		///扣减方式
		///</summary>
		public int AdjustMode
		{
			get	{ return _adjustmode; }
			set	{ _adjustmode = value; }
		}

		///<summary>
		///扣减金额
		///</summary>
		public decimal AdjustCost
		{
			get	{ return _adjustcost; }
			set	{ _adjustcost = value; }
		}

		///<summary>
		///扣减原因
		///</summary>
		public string AdjustReason
		{
			get	{ return _adjustreason; }
			set	{ _adjustreason = value; }
		}

		///<summary>
		///扣减人
		///</summary>
		public int InsertStaff
		{
			get	{ return _insertstaff; }
			set	{ _insertstaff = value; }
		}

		///<summary>
		///扣减时间
		///</summary>
		public DateTime InsertTime
		{
			get	{ return _inserttime; }
			set	{ _inserttime = value; }
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
            get { return "FNA_FeeWriteOffDetail_AdjustInfo"; }
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
					case "WriteOffDetailID":
						return _writeoffdetailid.ToString();
					case "AdjustMode":
						return _adjustmode.ToString();
					case "AdjustCost":
						return _adjustcost.ToString();
					case "AdjustReason":
						return _adjustreason;
					case "InsertStaff":
						return _insertstaff.ToString();
					case "InsertTime":
						return _inserttime.ToString();
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
					case "WriteOffDetailID":
						int.TryParse(value, out _writeoffdetailid);
						break;
					case "AdjustMode":
						int.TryParse(value, out _adjustmode);
						break;
					case "AdjustCost":
						decimal.TryParse(value, out _adjustcost);
						break;
					case "AdjustReason":
						_adjustreason = value;
						break;
					case "InsertStaff":
						int.TryParse(value, out _insertstaff);
						break;
					case "InsertTime":
						DateTime.TryParse(value, out _inserttime);
						break;
					case "Remark":
						_remark = value;
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
