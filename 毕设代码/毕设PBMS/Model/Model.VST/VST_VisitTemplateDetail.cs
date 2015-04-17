// ===================================================================
// 文件： VST_VisitTemplateDetail.cs
// 项目名称：
// 创建时间：2015/1/27
// 作者:	   ChingyZhang
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.VST
{
	/// <summary>
	///VST_VisitTemplateDetail数据实体类
	/// </summary>
	[Serializable]
	public class VST_VisitTemplateDetail : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _templateid = 0;
		private int _processid = 0;
		private int _sortid = 0;
		private string _canskip = string.Empty;
		private string _remark = string.Empty;
		
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public VST_VisitTemplateDetail()
		{
		}
		
		///<summary>
		///
		///</summary>
		public VST_VisitTemplateDetail(int id, int templateid, int processid, int sortid, string canskip, string remark)
		{
			_id           = id;
			_templateid   = templateid;
			_processid    = processid;
			_sortid       = sortid;
			_canskip      = canskip;
			_remark       = remark;
			
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
		///模板ID
		///</summary>
		public int TemplateID
		{
			get	{ return _templateid; }
			set	{ _templateid = value; }
		}

		///<summary>
		///步骤ID
		///</summary>
		public int ProcessID
		{
			get	{ return _processid; }
			set	{ _processid = value; }
		}

		///<summary>
		///顺序
		///</summary>
		public int SortID
		{
			get	{ return _sortid; }
			set	{ _sortid = value; }
		}

		///<summary>
		///是否可跳过
		///</summary>
		public string CanSkip
		{
			get	{ return _canskip; }
			set	{ _canskip = value; }
		}

		///<summary>
		///备注
		///</summary>
		public string Remark
		{
			get	{ return _remark; }
			set	{ _remark = value; }
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
            get { return "VST_VisitTemplateDetail"; }
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
					case "ProcessID":
						return _processid.ToString();
					case "SortID":
						return _sortid.ToString();
					case "CanSkip":
						return _canskip;
					case "Remark":
						return _remark;
					default:
						if (_extpropertys==null)
							return "";
						else
							return _extpropertys[FieldName];						return "";
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
					case "ProcessID":
						int.TryParse(value, out _processid);
						break;
					case "SortID":
						int.TryParse(value, out _sortid);
						break;
					case "CanSkip":
						_canskip = value ;
						break;
					case "Remark":
						_remark = value ;
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
