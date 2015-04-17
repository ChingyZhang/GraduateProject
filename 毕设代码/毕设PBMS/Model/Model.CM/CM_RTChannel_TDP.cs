// ===================================================================
// 文件： CM_RTChannel_TDP.cs
// 项目名称：
// 创建时间：2015/1/27
// 作者:	   lv
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.CM
{
	/// <summary>
	///CM_RTChannel_TDP数据实体类
	/// </summary>
	[Serializable]
	public class CM_RTChannel_TDP : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private string _code = string.Empty;
		private string _name = string.Empty;
		private int _ownerclient = 0;
		private string _remark = string.Empty;
		private DateTime _inserttime = new DateTime(1900,1,1);
		private int _insertstaff = 0;
		
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public CM_RTChannel_TDP()
		{
		}
		
		///<summary>
		///
		///</summary>
		public CM_RTChannel_TDP(int id, string code, string name, int ownerclient, string remark, DateTime inserttime, int insertstaff)
		{
			_id           = id;
			_code         = code;
			_name         = name;
			_ownerclient  = ownerclient;
			_remark       = remark;
			_inserttime   = inserttime;
			_insertstaff  = insertstaff;
			
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
		///渠道编码
		///</summary>
		public string Code
		{
			get	{ return _code; }
			set	{ _code = value; }
		}

		///<summary>
		///渠道名称
		///</summary>
		public string Name
		{
			get	{ return _name; }
			set	{ _name = value; }
		}

		///<summary>
		///所属经销商
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
            get { return "CM_RTChannel_TDP"; }
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
					case "OwnerClient":
						return _ownerclient.ToString();
					case "Remark":
						return _remark;
					case "InsertTime":
						return _inserttime.ToString();
					case "InsertStaff":
						return _insertstaff.ToString();
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
					case "OwnerClient":
						int.TryParse(value, out _ownerclient);
						break;
					case "Remark":
						_remark = value ;
						break;
					case "InsertTime":
						DateTime.TryParse(value, out _inserttime);
						break;
					case "InsertStaff":
						int.TryParse(value, out _insertstaff);
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
