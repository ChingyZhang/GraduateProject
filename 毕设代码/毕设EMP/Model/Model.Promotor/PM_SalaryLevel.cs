// ===================================================================
// 文件： PM_SalaryLevel.cs
// 项目名称：
// 创建时间：2009/3/19
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
	///PM_SalaryLevel数据实体类
	/// </summary>
	[Serializable]
	public class PM_SalaryLevel : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _organizecity = 0;
		private string _name = string.Empty;
		private int _bonusmode = 0;
		private int _computmethd = 0;
		private int _approveflag = 0;
		private DateTime _inputtime = new DateTime(1900,1,1);
		private int _inputstaff = 0;
		private DateTime _updatetime = new DateTime(1900,1,1);
		private int _updatestaff = 0;
		
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public PM_SalaryLevel()
		{
		}
		
		///<summary>
		///
		///</summary>
		public PM_SalaryLevel(int id, int organizecity, string name, int bonusmode, int computmethd, int approveflag, DateTime inputtime, int inputstaff, DateTime updatetime, int updatestaff)
		{
			_id           = id;
			_organizecity = organizecity;
			_name         = name;
			_bonusmode    = bonusmode;
			_computmethd  = computmethd;
			_approveflag  = approveflag;
			_inputtime    = inputtime;
			_inputstaff   = inputstaff;
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
		///管理片区
		///</summary>
		public int OrganizeCity
		{
			get	{ return _organizecity; }
			set	{ _organizecity = value; }
		}

		///<summary>
		///标准名称
		///</summary>
		public string Name
		{
			get	{ return _name; }
			set	{ _name = value; }
		}

		///<summary>
		///奖金方式(销量数量\销售额提成)
		///</summary>
		public int BonusMode
		{
			get	{ return _bonusmode; }
			set	{ _bonusmode = value; }
		}

		///<summary>
		///计算方法(直接计算\分段累加)
		///</summary>
		public int ComputMethd
		{
			get	{ return _computmethd; }
			set	{ _computmethd = value; }
		}

		///<summary>
		///审批标志
		///</summary>
		public int ApproveFlag
		{
			get	{ return _approveflag; }
			set	{ _approveflag = value; }
		}

		///<summary>
		///录入日期
		///</summary>
		public DateTime InputTime
		{
			get	{ return _inputtime; }
			set	{ _inputtime = value; }
		}

		///<summary>
		///录入人
		///</summary>
		public int InputStaff
		{
			get	{ return _inputstaff; }
			set	{ _inputstaff = value; }
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
            get { return "PM_SalaryLevel"; }
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
					case "Name":
						return _name;
					case "BonusMode":
						return _bonusmode.ToString();
					case "ComputMethd":
						return _computmethd.ToString();
					case "ApproveFlag":
						return _approveflag.ToString();
					case "InputTime":
						return _inputtime.ToString();
					case "InputStaff":
						return _inputstaff.ToString();
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
					case "OrganizeCity":
						int.TryParse(value, out _organizecity);
						break;
					case "Name":
						_name = value ;
						break;
					case "BonusMode":
						int.TryParse(value, out _bonusmode);
						break;
					case "ComputMethd":
						int.TryParse(value, out _computmethd);
						break;
					case "ApproveFlag":
						int.TryParse(value, out _approveflag);
						break;
					case "InputTime":
						DateTime.TryParse(value, out _inputtime);
						break;
					case "InputStaff":
						int.TryParse(value, out _inputstaff);
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
