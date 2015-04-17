// ===================================================================
// 文件： PDT_Category.cs
// 项目名称：
// 创建时间：2015/1/27
// 作者:	   lv
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.Pub
{
	/// <summary>
	///PDT_Category数据实体类
	/// </summary>
	[Serializable]
	public class PDT_Category : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private string _code = string.Empty;
		private string _name = string.Empty;
		private int _superid = 0;
		private string _enabledflag = string.Empty;
		private int _ownertype = 0;
		private int _ownerclient = 0;
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
		public PDT_Category()
		{
		}
		
		///<summary>
		///
		///</summary>
		public PDT_Category(int id, string code, string name, int superid, string enabledflag, int ownertype, int ownerclient, string remark, int approveflag, DateTime inserttime, int insertstaff, DateTime updatetime, int updatestaff)
		{
			_id           = id;
			_code         = code;
			_name         = name;
			_superid      = superid;
			_enabledflag  = enabledflag;
			_ownertype    = ownertype;
			_ownerclient  = ownerclient;
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
		///编码
		///</summary>
		public string Code
		{
			get	{ return _code; }
			set	{ _code = value; }
		}

		///<summary>
		///名称
		///</summary>
		public string Name
		{
			get	{ return _name; }
			set	{ _name = value; }
		}

		///<summary>
		///上级ID
		///</summary>
		public int SuperID
		{
			get	{ return _superid; }
			set	{ _superid = value; }
		}

		///<summary>
		///启用标志
		///</summary>
		public string EnabledFlag
		{
			get	{ return _enabledflag; }
			set	{ _enabledflag = value; }
		}

		///<summary>
		///所有权属性
		///</summary>
		public int OwnerType
		{
			get	{ return _ownertype; }
			set	{ _ownertype = value; }
		}

		///<summary>
		///所有权人
		///</summary>
		public int OwnerClient
		{
			get	{ return _ownerclient; }
			set	{ _ownerclient = value; }
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
            get { return "PDT_Category"; }
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
					case "Code":
						return _code;
					case "Name":
						return _name;
					case "SuperID":
						return _superid.ToString();
					case "EnabledFlag":
						return _enabledflag;
					case "OwnerType":
						return _ownertype.ToString();
					case "OwnerClient":
						return _ownerclient.ToString();
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
					case "Code":
						_code = value ;
						break;
					case "Name":
						_name = value ;
						break;
					case "SuperID":
						int.TryParse(value, out _superid);
						break;
					case "EnabledFlag":
						_enabledflag = value ;
						break;
					case "OwnerType":
						int.TryParse(value, out _ownertype);
						break;
					case "OwnerClient":
						int.TryParse(value, out _ownerclient);
						break;
					case "Remark":
						_remark = value ;
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
