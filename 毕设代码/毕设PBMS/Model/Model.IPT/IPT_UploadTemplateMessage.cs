// ===================================================================
// 文件： IPT_UploadTemplateMessage.cs
// 项目名称：
// 创建时间：2015/3/17
// 作者:	   Jace
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.IPT
{
	/// <summary>
	///IPT_UploadTemplateMessage数据实体类
	/// </summary>
	[Serializable]
	public class IPT_UploadTemplateMessage : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _templateid = 0;
		private int _messagetype = 0;
		private string _content = string.Empty;
		private string _remark = string.Empty;
		private DateTime _inserttime = new DateTime(1900,1,1);
		private int _dint01 = 0;
		private int _dint02 = 0;
		private int _dint03 = 0;
		private decimal _ddec01 = 0;
		private decimal _ddec02 = 0;
		private decimal _ddec03 = 0;
		private string _dstr01 = string.Empty;
		private string _dstr02 = string.Empty;
		private string _dstr03 = string.Empty;
		
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public IPT_UploadTemplateMessage()
		{
		}
		
		///<summary>
		///
		///</summary>
		public IPT_UploadTemplateMessage(int id, int templateid, int messagetype, string content, string remark, DateTime inserttime, int dint01, int dint02, int dint03, decimal ddec01, decimal ddec02, decimal ddec03, string dstr01, string dstr02, string dstr03)
		{
			_id                = id;
            _templateid        = templateid;
			_messagetype       = messagetype;
			_content           = content;
			_remark            = remark;
			_inserttime        = inserttime;
			_dint01            = dint01;
			_dint02            = dint02;
			_dint03            = dint03;
			_ddec01            = ddec01;
			_ddec02            = ddec02;
			_ddec03            = ddec03;
			_dstr01            = dstr01;
			_dstr02            = dstr02;
			_dstr03            = dstr03;
			
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
		///TemplateMessageID
		///</summary>
		public int TemplateID
		{
			get	{ return _templateid; }
			set	{ _templateid = value; }
		}

		///<summary>
		///MessageType
		///</summary>
		public int MessageType
		{
			get	{ return _messagetype; }
			set	{ _messagetype = value; }
		}

		///<summary>
		///Content
		///</summary>
		public string Content
		{
			get	{ return _content; }
			set	{ _content = value; }
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
		///InsertTime
		///</summary>
		public DateTime InsertTime
		{
			get	{ return _inserttime; }
			set	{ _inserttime = value; }
		}

		///<summary>
		///DINT01
		///</summary>
		public int DINT01
		{
			get	{ return _dint01; }
			set	{ _dint01 = value; }
		}

		///<summary>
		///DINT02
		///</summary>
		public int DINT02
		{
			get	{ return _dint02; }
			set	{ _dint02 = value; }
		}

		///<summary>
		///DINT03
		///</summary>
		public int DINT03
		{
			get	{ return _dint03; }
			set	{ _dint03 = value; }
		}

		///<summary>
		///DDEC01
		///</summary>
		public decimal DDEC01
		{
			get	{ return _ddec01; }
			set	{ _ddec01 = value; }
		}

		///<summary>
		///DDEC02
		///</summary>
		public decimal DDEC02
		{
			get	{ return _ddec02; }
			set	{ _ddec02 = value; }
		}

		///<summary>
		///DDEC03
		///</summary>
		public decimal DDEC03
		{
			get	{ return _ddec03; }
			set	{ _ddec03 = value; }
		}

		///<summary>
		///DSTR01
		///</summary>
		public string DSTR01
		{
			get	{ return _dstr01; }
			set	{ _dstr01 = value; }
		}

		///<summary>
		///DSTR02
		///</summary>
		public string DSTR02
		{
			get	{ return _dstr02; }
			set	{ _dstr02 = value; }
		}

		///<summary>
		///DSTR03
		///</summary>
		public string DSTR03
		{
			get	{ return _dstr03; }
			set	{ _dstr03 = value; }
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
            get { return "IPT_UploadTemplateMessage"; }
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
					case "TemplateID":
						return _templateid.ToString();
					case "MessageType":
						return _messagetype.ToString();
					case "Content":
						return _content;
					case "Remark":
						return _remark;
					case "InsertTime":
						return _inserttime.ToString();
					case "DINT01":
						return _dint01.ToString();
					case "DINT02":
						return _dint02.ToString();
					case "DINT03":
						return _dint03.ToString();
					case "DDEC01":
						return _ddec01.ToString();
					case "DDEC02":
						return _ddec02.ToString();
					case "DDEC03":
						return _ddec03.ToString();
					case "DSTR01":
						return _dstr01;
					case "DSTR02":
						return _dstr02;
					case "DSTR03":
						return _dstr03;
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
					case "TemplateID":
						int.TryParse(value, out _templateid);
						break;
					case "MessageType":
						int.TryParse(value, out _messagetype);
						break;
					case "Content":
						_content = value;
						break;
					case "Remark":
						_remark = value;
						break;
					case "InsertTime":
						DateTime.TryParse(value, out _inserttime);
						break;
					case "DINT01":
						int.TryParse(value, out _dint01);
						break;
					case "DINT02":
						int.TryParse(value, out _dint02);
						break;
					case "DINT03":
						int.TryParse(value, out _dint03);
						break;
					case "DDEC01":
						decimal.TryParse(value, out _ddec01);
						break;
					case "DDEC02":
						decimal.TryParse(value, out _ddec02);
						break;
					case "DDEC03":
						decimal.TryParse(value, out _ddec03);
						break;
					case "DSTR01":
						_dstr01 = value;
						break;
					case "DSTR02":
						_dstr02 = value;
						break;
					case "DSTR03":
						_dstr03 = value;
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
