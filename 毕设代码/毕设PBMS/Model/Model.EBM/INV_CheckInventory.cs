// ===================================================================
// 文件： INV_CheckInventory.cs
// 项目名称：
// 创建时间：2012-8-8
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
	///INV_CheckInventory数据实体类
	/// </summary>
	[Serializable]
	public class INV_CheckInventory : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _warehouse = 0;
		private int _state = 0;
		private DateTime _confirmdate = new DateTime(1900,1,1);
		private Guid _confirmuser = Guid.Empty;
		private string _description = string.Empty;
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
		public INV_CheckInventory()
		{
		}
		
		///<summary>
		///
		///</summary>
		public INV_CheckInventory(int id, int warehouse, int state, DateTime confirmdate, Guid confirmuser, string description, string remark, int approvetask, int approveflag, DateTime inserttime, Guid insertuser, DateTime updatetime, Guid updateuser)
		{
			_id           = id;
			_warehouse    = warehouse;
			_state        = state;
			_confirmdate  = confirmdate;
			_confirmuser  = confirmuser;
			_description  = description;
			_remark       = remark;
			_approvetask  = approvetask;
			_approveflag  = approveflag;
			_inserttime   = inserttime;
			_insertuser   = insertuser;
			_updatetime   = updatetime;
			_updateuser   = updateuser;
			
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
		///仓库
		///</summary>
		public int WareHouse
		{
			get	{ return _warehouse; }
			set	{ _warehouse = value; }
		}

		///<summary>
		///盘点状态
		///</summary>
		public int State
		{
			get	{ return _state; }
			set	{ _state = value; }
		}

		///<summary>
		///确认日期
		///</summary>
		public DateTime ConfirmDate
		{
			get	{ return _confirmdate; }
			set	{ _confirmdate = value; }
		}

		///<summary>
		///确认人
		///</summary>
		public Guid ConfirmUser
		{
			get	{ return _confirmuser; }
			set	{ _confirmuser = value; }
		}

		///<summary>
		///描述
		///</summary>
		public string Description
		{
			get	{ return _description; }
			set	{ _description = value; }
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
            get { return "INV_CheckInventory"; }
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
					case "WareHouse":
						return _warehouse.ToString();
					case "State":
						return _state.ToString();
					case "ConfirmDate":
						return _confirmdate.ToString();
					case "ConfirmUser":
						return _confirmuser.ToString();
					case "Description":
						return _description;
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
					case "WareHouse":
						int.TryParse(value, out _warehouse);
						break;
					case "State":
						int.TryParse(value, out _state);
						break;
					case "ConfirmDate":
						DateTime.TryParse(value, out _confirmdate);
						break;
					case "ConfirmUser":
                        _confirmuser = new Guid(value);
						break;
					case "Description":
						_description = value ;
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
