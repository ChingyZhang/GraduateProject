// ===================================================================
// 文件： DB_IDMapTable.cs
// 项目名称：
// 创建时间：2010/1/19
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model
{
	/// <summary>
	///DB_IDMapTable数据实体类
	/// </summary>
	[Serializable]
	public class DB_IDMapTable : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private string _tablename = string.Empty;
		private int _idv3 = 0;
		private int _idv4 = 0;
		private string _namev3 = string.Empty;
		private string _namev4 = string.Empty;
				#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public DB_IDMapTable()
		{
		}
		
		///<summary>
		///
		///</summary>
		public DB_IDMapTable(int id, string tablename, int idv3, int idv4, string namev3, string namev4)
		{
			_id        = id;
			_tablename = tablename;
			_idv3      = idv3;
			_idv4      = idv4;
			_namev3    = namev3;
			_namev4    = namev4;
			
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
		///TableName
		///</summary>
		public string TableName
		{
			get	{ return _tablename; }
			set	{ _tablename = value; }
		}

		///<summary>
		///IDV3
		///</summary>
		public int IDV3
		{
			get	{ return _idv3; }
			set	{ _idv3 = value; }
		}

		///<summary>
		///IDV4
		///</summary>
		public int IDV4
		{
			get	{ return _idv4; }
			set	{ _idv4 = value; }
		}

		///<summary>
		///NameV3
		///</summary>
		public string NameV3
		{
			get	{ return _namev3; }
			set	{ _namev3 = value; }
		}

		///<summary>
		///NameV4
		///</summary>
		public string NameV4
		{
			get	{ return _namev4; }
			set	{ _namev4 = value; }
		}
		
		#endregion
		
		public string ModelName
        {
            get { return "DB_IDMapTable"; }
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
					case "TableName":
						return _tablename;
					case "IDV3":
						return _idv3.ToString();
					case "IDV4":
						return _idv4.ToString();
					case "NameV3":
						return _namev3;
					case "NameV4":
						return _namev4;
					default:
						return "";
				}
			}
            set 
			{
				switch (FieldName)
                {
					case "ID":
						int.TryParse(value, out _id);
						break;
					case "TableName":
						_tablename = value ;
						break;
					case "IDV3":
						int.TryParse(value, out _idv3);
						break;
					case "IDV4":
						int.TryParse(value, out _idv4);
						break;
					case "NameV3":
						_namev3 = value ;
						break;
					case "NameV4":
						_namev4 = value ;
						break;

				}
			}
        }
		#endregion
	}
}
