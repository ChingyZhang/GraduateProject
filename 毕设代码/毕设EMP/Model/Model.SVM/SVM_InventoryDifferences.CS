// ===================================================================
// 文件： SVM_InventoryDifferences.cs
// 项目名称：
// 创建时间：2011/12/14
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
	///SVM_InventoryDifferences数据实体类
	/// </summary>
	[Serializable]
	public class SVM_InventoryDifferences : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _client = 0;
		private int _organizecity = 0;
		private int _accountmonth = 0;
		private DateTime _inventorydate = new DateTime(1900,1,1);
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
		public SVM_InventoryDifferences()
		{
		}
		
		///<summary>
		///
		///</summary>
		public SVM_InventoryDifferences(int id, int client, int organizecity, int accountmonth, DateTime inventorydate, int approveflag, DateTime inserttime, int insertstaff, DateTime updatetime, int updatestaff)
		{
			_id            = id;
			_client        = client;
			_organizecity  = organizecity;
			_accountmonth  = accountmonth;
			_inventorydate = inventorydate;
			_approveflag   = approveflag;
			_inserttime    = inserttime;
			_insertstaff   = insertstaff;
			_updatetime    = updatetime;
			_updatestaff   = updatestaff;
			
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
		///Client
		///</summary>
		public int Client
		{
			get	{ return _client; }
			set	{ _client = value; }
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
		///AccountMonth
		///</summary>
		public int AccountMonth
		{
			get	{ return _accountmonth; }
			set	{ _accountmonth = value; }
		}

		///<summary>
		///InventoryDate
		///</summary>
		public DateTime InventoryDate
		{
			get	{ return _inventorydate; }
			set	{ _inventorydate = value; }
		}

		///<summary>
		///ApproveFlag
		///</summary>
		public int ApproveFlag
		{
			get	{ return _approveflag; }
			set	{ _approveflag = value; }
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
		///InsertStaff
		///</summary>
		public int InsertStaff
		{
			get	{ return _insertstaff; }
			set	{ _insertstaff = value; }
		}

		///<summary>
		///UpdateTime
		///</summary>
		public DateTime UpdateTime
		{
			get	{ return _updatetime; }
			set	{ _updatetime = value; }
		}

		///<summary>
		///UpdateStaff
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
            get { return "SVM_InventoryDifferences"; }
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
					case "Client":
						return _client.ToString();
					case "OrganizeCity":
						return _organizecity.ToString();
					case "AccountMonth":
						return _accountmonth.ToString();
					case "InventoryDate":
						return _inventorydate.ToString();
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
					case "Client":
						int.TryParse(value, out _client);
						break;
					case "OrganizeCity":
						int.TryParse(value, out _organizecity);
						break;
					case "AccountMonth":
						int.TryParse(value, out _accountmonth);
						break;
					case "InventoryDate":
						DateTime.TryParse(value, out _inventorydate);
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
