// ===================================================================
// 文件： MT_MaterialClassifyModel.cs
// 项目名称：
// 创建时间：2008-12-22
// 作者:	   yangwei
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.Pub
{
	/// <summary>
    ///MT_MaterialClassifyModel数据实体类
	/// </summary>
	[Serializable]
	public class MT_MaterialClassify : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private string _name = string.Empty;
		private int _superid = 0;
	    #endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public MT_MaterialClassify()
		{
		}
		
		///<summary>
		///
		///</summary>
		public MT_MaterialClassify(int id, string name, int superid)
		{
			_id    = id;
			_name  = name;
			_superid = superid;
			
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
		///Name
		///</summary>
		public string Name
		{
			get	{ return _name; }
			set	{ _name = value; }
		}

		///<summary>
		///SuperID
		///</summary>
		public int SuperID
		{
			get	{ return _superid; }
			set	{ _superid = value; }
		}
		
		#endregion
		
		public string ModelName
        {
            get { return "MT_MaterialClassify"; }
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
					case "Name":
						_name = value ;
						break;
					case "SuperID":
						int.TryParse(value, out _superid);
						break;

				}
			}
        }
		#endregion
	}
}
