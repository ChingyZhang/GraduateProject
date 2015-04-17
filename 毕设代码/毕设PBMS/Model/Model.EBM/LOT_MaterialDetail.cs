// ===================================================================
// 文件： LOT_MaterialDetail.cs
// 项目名称：
// 创建时间：2012-11-11
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
	///LOT_MaterialDetail数据实体类
	/// </summary>
	[Serializable]
	public class LOT_MaterialDetail : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _lotid = 0;
		private int _material = 0;
		private int _origincountry = 0;
		private DateTime _departuredate = new DateTime(1900,1,1);
		private DateTime _arrivaldate = new DateTime(1900,1,1);
		private string _entrycustomscode = string.Empty;
		private DateTime _exitcustomsdate = new DateTime(1900,1,1);
		private string _remark = string.Empty;
		private DateTime _inserttime = new DateTime(1900,1,1);
		private Guid _insertuser = Guid.Empty;
		
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public LOT_MaterialDetail()
		{
		}
		
		///<summary>
		///
		///</summary>
		public LOT_MaterialDetail(int id, int lotid, int material, int origincountry, DateTime departuredate, DateTime arrivaldate, string entrycustomscode, DateTime exitcustomsdate, string remark, DateTime inserttime, Guid insertuser)
		{
			_id               = id;
			_lotid            = lotid;
			_material         = material;
			_origincountry    = origincountry;
			_departuredate    = departuredate;
			_arrivaldate      = arrivaldate;
			_entrycustomscode = entrycustomscode;
			_exitcustomsdate  = exitcustomsdate;
			_remark           = remark;
			_inserttime       = inserttime;
			_insertuser       = insertuser;
			
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
		///批号ID
		///</summary>
		public int LotID
		{
			get	{ return _lotid; }
			set	{ _lotid = value; }
		}

		///<summary>
		///主要原料
		///</summary>
		public int Material
		{
			get	{ return _material; }
			set	{ _material = value; }
		}

		///<summary>
		///原产地
		///</summary>
		public int OriginCountry
		{
			get	{ return _origincountry; }
			set	{ _origincountry = value; }
		}

		///<summary>
		///出港日期
		///</summary>
		public DateTime DepartureDate
		{
			get	{ return _departuredate; }
			set	{ _departuredate = value; }
		}

		///<summary>
		///到港日期
		///</summary>
		public DateTime ArrivalDate
		{
			get	{ return _arrivaldate; }
			set	{ _arrivaldate = value; }
		}

		///<summary>
		///入关单号
		///</summary>
		public string EntryCustomsCode
		{
			get	{ return _entrycustomscode; }
			set	{ _entrycustomscode = value; }
		}

		///<summary>
		///通关日期
		///</summary>
		public DateTime ExitCustomsDate
		{
			get	{ return _exitcustomsdate; }
			set	{ _exitcustomsdate = value; }
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
            get { return "LOT_MaterialDetail"; }
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
					case "LotID":
						return _lotid.ToString();
					case "Material":
						return _material.ToString();
					case "OriginCountry":
						return _origincountry.ToString();
					case "DepartureDate":
						return _departuredate.ToString();
					case "ArrivalDate":
						return _arrivaldate.ToString();
					case "EntryCustomsCode":
						return _entrycustomscode;
					case "ExitCustomsDate":
						return _exitcustomsdate.ToString();
					case "Remark":
						return _remark;
					case "InsertTime":
						return _inserttime.ToString();
					case "InsertUser":
						return _insertuser.ToString();
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
					case "LotID":
						int.TryParse(value, out _lotid);
						break;
					case "Material":
						int.TryParse(value, out _material);
						break;
					case "OriginCountry":
						int.TryParse(value, out _origincountry);
						break;
					case "DepartureDate":
						DateTime.TryParse(value, out _departuredate);
						break;
					case "ArrivalDate":
						DateTime.TryParse(value, out _arrivaldate);
						break;
					case "EntryCustomsCode":
						_entrycustomscode = value;
						break;
					case "ExitCustomsDate":
						DateTime.TryParse(value, out _exitcustomsdate);
						break;
					case "Remark":
						_remark = value;
						break;
					case "InsertTime":
						DateTime.TryParse(value, out _inserttime);
						break;
					case "InsertUser":
						_insertuser = new Guid(value);
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
