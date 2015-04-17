// ===================================================================
// 文件： PDT_StandardPrice.cs
// 项目名称：
// 创建时间：2015-03-25
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.Pub
{
	/// <summary>
	///PDT_StandardPrice数据实体类
	/// </summary>
	[Serializable]
	public class PDT_StandardPrice : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private string _name = string.Empty;
		private int _supplier = 0;
		private int _fitsalesarea = 0;
		private int _fitrtchannel = 0;
		private string _isdefault = string.Empty;
		private string _isenabled = string.Empty;
		private string _remark = string.Empty;
		private int _approveflag = 0;
		private DateTime _inserttime = new DateTime(1900,1,1);
		private int _insertstaff = 0;
		private DateTime _updatetime = new DateTime(1900,1,1);
		private int _updatestaff = 0;
		
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public PDT_StandardPrice()
		{
		}
		
		///<summary>
		///
		///</summary>
		public PDT_StandardPrice(int id, string name, int supplier, int fitsalesarea, int fitrtchannel, string isdefault, string isenabled, string remark, int approveflag, DateTime inserttime, int insertstaff, DateTime updatetime, int updatestaff)
		{
			_id           = id;
			_name         = name;
			_supplier     = supplier;
			_fitsalesarea = fitsalesarea;
			_fitrtchannel = fitrtchannel;
			_isdefault    = isdefault;
			_isenabled    = isenabled;
			_remark       = remark;
			_approveflag  = approveflag;
			_inserttime   = inserttime;
			_insertstaff  = insertstaff;
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
		///价盘名称
		///</summary>
		public string Name
		{
			get	{ return _name; }
			set	{ _name = value; }
		}

		///<summary>
		///供货商
		///</summary>
		public int Supplier
		{
			get	{ return _supplier; }
			set	{ _supplier = value; }
		}

		///<summary>
		///适用区域
		///</summary>
		public int FitSalesArea
		{
			get	{ return _fitsalesarea; }
			set	{ _fitsalesarea = value; }
		}

		///<summary>
		///适用渠道
		///</summary>
		public int FitRTChannel
		{
			get	{ return _fitrtchannel; }
			set	{ _fitrtchannel = value; }
		}

		///<summary>
		///是否默认价表
		///</summary>
		public string IsDefault
		{
			get	{ return _isdefault; }
			set	{ _isdefault = value; }
		}

		///<summary>
		///价表是否启用
		///</summary>
		public string IsEnabled
		{
			get	{ return _isenabled; }
			set	{ _isenabled = value; }
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
		///新增日期
		///</summary>
		public DateTime InsertTime
		{
			get	{ return _inserttime; }
			set	{ _inserttime = value; }
		}

		///<summary>
		///新增人
		///</summary>
		public int InsertStaff
		{
			get	{ return _insertstaff; }
			set	{ _insertstaff = value; }
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
            get { return "PDT_StandardPrice"; }
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
					case "Name":
						return _name;
					case "Supplier":
						return _supplier.ToString();
					case "FitSalesArea":
						return _fitsalesarea.ToString();
					case "FitRTChannel":
						return _fitrtchannel.ToString();
					case "IsDefault":
						return _isdefault;
					case "IsEnabled":
						return _isenabled;
					case "Remark":
						return _remark;
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
					case "Name":
						_name = value;
						break;
					case "Supplier":
						int.TryParse(value, out _supplier);
						break;
					case "FitSalesArea":
						int.TryParse(value, out _fitsalesarea);
						break;
					case "FitRTChannel":
						int.TryParse(value, out _fitrtchannel);
						break;
					case "IsDefault":
						_isdefault = value;
						break;
					case "IsEnabled":
						_isenabled = value;
						break;
					case "Remark":
						_remark = value;
						break;
					case "ApproveFlag":
						int.TryParse(value, out _approveflag);
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
