// ===================================================================
// 文件： INV_PutInStoreage.cs
// 项目名称：
// 创建时间：2012-7-23
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.EBM
{
	/// <summary>
	///INV_PutInStoreage数据实体类
	/// </summary>
	[Serializable]
	public class INV_PutInStoreage : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private string _sheetcode = string.Empty;
		private int _warehouse = 0;
		private int _warehousecell = 0;
		private int _source = 0;
		private int _state = 0;
		private DateTime _confirmtime = new DateTime(1900,1,1);
		private Guid _confirmuser = Guid.Empty;
		private DateTime _unputintime = new DateTime(1900,1,1);
		private Guid _unputinuser = Guid.Empty;
		private string _remark = string.Empty;
		private int _approvetask = 0;
		private int _approveflag = 0;
		private DateTime _inserttime = new DateTime(1900,1,1);
		private Guid _insertuser = Guid.Empty;
		private DateTime _updatetime = new DateTime(1900,1,1);
		private Guid _updateuser = Guid.Empty;
		
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public INV_PutInStoreage()
		{
		}
		
		///<summary>
		///
		///</summary>
		public INV_PutInStoreage(int id, string sheetcode, int warehouse, int warehousecell, int source, int state, DateTime confirmtime, Guid confirmuser, DateTime unputintime, Guid unputinuser, string remark, int approvetask, int approveflag, DateTime inserttime, Guid insertuser, DateTime updatetime, Guid updateuser)
		{
			_id            = id;
			_sheetcode     = sheetcode;
			_warehouse     = warehouse;
			_warehousecell = warehousecell;
			_source        = source;
			_state         = state;
			_confirmtime   = confirmtime;
			_confirmuser   = confirmuser;
			_unputintime   = unputintime;
			_unputinuser   = unputinuser;
			_remark        = remark;
			_approvetask   = approvetask;
			_approveflag   = approveflag;
			_inserttime    = inserttime;
			_insertuser    = insertuser;
			_updatetime    = updatetime;
			_updateuser    = updateuser;
			
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
		///单号
		///</summary>
		public string SheetCode
		{
			get	{ return _sheetcode; }
			set	{ _sheetcode = value; }
		}

		///<summary>
		///入库仓库
		///</summary>
		public int WareHouse
		{
			get	{ return _warehouse; }
			set	{ _warehouse = value; }
		}

		///<summary>
		///WareHouseCell
		///</summary>
		public int WareHouseCell
		{
			get	{ return _warehousecell; }
			set	{ _warehousecell = value; }
		}

		///<summary>
		///来源
		///</summary>
		public int Source
		{
			get	{ return _source; }
			set	{ _source = value; }
		}

		///<summary>
		///状态
		///</summary>
		public int State
		{
			get	{ return _state; }
			set	{ _state = value; }
		}

		///<summary>
		///确认入库时间
		///</summary>
		public DateTime ConfirmTime
		{
			get	{ return _confirmtime; }
			set	{ _confirmtime = value; }
		}

		///<summary>
		///确认入库人
		///</summary>
		public Guid ConfirmUser
		{
			get	{ return _confirmuser; }
			set	{ _confirmuser = value; }
		}

		///<summary>
		///撤销入库时间
		///</summary>
		public DateTime UnPutInTime
		{
			get	{ return _unputintime; }
			set	{ _unputintime = value; }
		}

		///<summary>
		///撤销入库人
		///</summary>
		public Guid UnPutInUser
		{
			get	{ return _unputinuser; }
			set	{ _unputinuser = value; }
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
		///审批任务
		///</summary>
		public int ApproveTask
		{
			get	{ return _approvetask; }
			set	{ _approvetask = value; }
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
		///录入时间
		///</summary>
		public DateTime InsertTime
		{
			get	{ return _inserttime; }
			set	{ _inserttime = value; }
		}

		///<summary>
		///录入人
		///</summary>
		public Guid InsertUser
		{
			get	{ return _insertuser; }
			set	{ _insertuser = value; }
		}

		///<summary>
		///更新时间
		///</summary>
		public DateTime UpdateTime
		{
			get	{ return _updatetime; }
			set	{ _updatetime = value; }
		}

		///<summary>
		///更新人
		///</summary>
		public Guid UpdateUser
		{
			get	{ return _updateuser; }
			set	{ _updateuser = value; }
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
            get { return "INV_PutInStoreage"; }
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
					case "SheetCode":
						return _sheetcode;
					case "WareHouse":
						return _warehouse.ToString();
					case "WareHouseCell":
						return _warehousecell.ToString();
					case "Source":
						return _source.ToString();
					case "State":
						return _state.ToString();
					case "ConfirmTime":
						return _confirmtime.ToString();
					case "ConfirmUser":
						return _confirmuser.ToString();
					case "UnPutInTime":
						return _unputintime.ToString();
					case "UnPutInUser":
						return _unputinuser.ToString();
					case "Remark":
						return _remark;
					case "ApproveTask":
						return _approvetask.ToString();
					case "ApproveFlag":
						return _approveflag.ToString();
					case "InsertTime":
						return _inserttime.ToString();
					case "InsertUser":
						return _insertuser.ToString();
					case "UpdateTime":
						return _updatetime.ToString();
					case "UpdateUser":
						return _updateuser.ToString();
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
					case "SheetCode":
						_sheetcode = value ;
						break;
					case "WareHouse":
						int.TryParse(value, out _warehouse);
						break;
					case "WareHouseCell":
						int.TryParse(value, out _warehousecell);
						break;
					case "Source":
						int.TryParse(value, out _source);
						break;
					case "State":
						int.TryParse(value, out _state);
						break;
					case "ConfirmTime":
						DateTime.TryParse(value, out _confirmtime);
						break;
					case "ConfirmUser":
						_confirmuser = new Guid(value);
						break;
					case "UnPutInTime":
						DateTime.TryParse(value, out _unputintime);
						break;
					case "UnPutInUser":
						_unputinuser = new Guid(value);
						break;
					case "Remark":
						_remark = value ;
						break;
					case "ApproveTask":
						int.TryParse(value, out _approvetask);
						break;
					case "ApproveFlag":
						int.TryParse(value, out _approveflag);
						break;
					case "InsertTime":
						DateTime.TryParse(value, out _inserttime);
						break;
					case "InsertUser":
						_insertuser = new Guid(value);
						break;
					case "UpdateTime":
						DateTime.TryParse(value, out _updatetime);
						break;
					case "UpdateUser":
						_updateuser = new Guid(value);
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
