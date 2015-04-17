// ===================================================================
// 文件： Rpt_Folder.cs
// 项目名称：
// 创建时间：2010/9/25
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.RPT
{
	/// <summary>
	///Rpt_Folder数据实体类
	/// </summary>
	[Serializable]
	public class Rpt_Folder : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private string _name = string.Empty;
		private int _superid = 0;
		private int _level = 0;
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public Rpt_Folder()
		{
		}
		
		///<summary>
		///
		///</summary>
		public Rpt_Folder(int id, string name, int superid, int level)
		{
			_id      = id;
			_name    = name;
			_superid = superid;
			_level   = level;
			
		}
		#endregion
		
		#region 公共属性		
		///<summary>
		///目录ID
		///</summary>
		public int ID
		{
			get	{ return _id; }
			set	{ _id = value; }
		}

		///<summary>
		///报表目录名称
		///</summary>
		public string Name
		{
			get	{ return _name; }
			set	{ _name = value; }
		}

		///<summary>
		///上级目录
		///</summary>
		public int SuperID
		{
			get	{ return _superid; }
			set	{ _superid = value; }
		}

		///<summary>
		///目录级别
		///</summary>
		public int Level
		{
			get	{ return _level; }
			set	{ _level = value; }
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
            get { return "Rpt_Folder"; }
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
					case "Name":
						return _name;
					case "SuperID":
						return _superid.ToString();
					case "Level":
						return _level.ToString();
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
					case "Name":
						_name = value ;
						break;
					case "SuperID":
						int.TryParse(value, out _superid);
						break;
					case "Level":
						int.TryParse(value, out _level);
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
