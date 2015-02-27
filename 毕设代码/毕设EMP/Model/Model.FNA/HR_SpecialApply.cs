// ===================================================================
// 文件： HR_SpecialApply.cs
// 项目名称：
// 创建时间：2011/1/5
// 作者:	   MDF
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.FNA
{
	/// <summary>
	///HR_SpecialApply数据实体类
	/// </summary>
	[Serializable]
	public class HR_SpecialApply : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private string _sheetcode = string.Empty;
		private int _task = 0;
		private int _organizecity = 0;
		private int _accountmonth = 0;
		private int _accounttitletype = 0;
		private string _remark = string.Empty;
		private int _approveflag = 0;
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
		public HR_SpecialApply()
		{
		}
		
		///<summary>
		///
		///</summary>
		public HR_SpecialApply(int id, string sheetcode, int task, int organizecity, int accountmonth, int accounttitletype, string remark, int approveflag, int insertstaff, DateTime inserttime, int updatestaff, DateTime updatetime)
		{
			_id               = id;
			_sheetcode        = sheetcode;
			_task             = task;
			_organizecity     = organizecity;
			_accountmonth     = accountmonth;
			_accounttitletype = accounttitletype;
			_remark           = remark;
			_approveflag     = approveflag;
			_insertstaff      = insertstaff;
			_inserttime       = inserttime;
			_updatestaff      = updatestaff;
			_updatetime       = updatetime;
			
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
		///SheetCode
		///</summary>
		public string SheetCode
		{
			get	{ return _sheetcode; }
			set	{ _sheetcode = value; }
		}

		///<summary>
		///Task
		///</summary>
		public int Task
		{
			get	{ return _task; }
			set	{ _task = value; }
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
		///AccountTitleType
		///</summary>
		public int AccountTitleType
		{
			get	{ return _accounttitletype; }
			set	{ _accounttitletype = value; }
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
		///ApproveFlag
		///</summary>
		public int ApproveFlag
		{
			get	{ return _approveflag; }
			set	{ _approveflag = value; }
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
            get { return "HR_SpecialApply"; }
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
					case "Task":
						return _task.ToString();
					case "OrganizeCity":
						return _organizecity.ToString();
					case "AccountMonth":
						return _accountmonth.ToString();
					case "AccountTitleType":
						return _accounttitletype.ToString();
					case "Remark":
						return _remark;
					case "ApproveFlag":
						return _approveflag.ToString();
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
					case "SheetCode":
						_sheetcode = value ;
						break;
					case "Task":
						int.TryParse(value, out _task);
						break;
					case "OrganizeCity":
						int.TryParse(value, out _organizecity);
						break;
					case "AccountMonth":
						int.TryParse(value, out _accountmonth);
						break;
					case "AccountTitleType":
						int.TryParse(value, out _accounttitletype);
						break;
					case "Remark":
						_remark = value ;
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
