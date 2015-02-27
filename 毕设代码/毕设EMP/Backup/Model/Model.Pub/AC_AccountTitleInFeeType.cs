// ===================================================================
// 文件： AC_AccountTitleInFeeType.cs
// 项目名称：
// 创建时间：2010/7/20
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.Pub
{
	/// <summary>
	///AC_AccountTitleInFeeType数据实体类
	/// </summary>
	[Serializable]
	public class AC_AccountTitleInFeeType : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _accounttitle = 0;
		private int _feetype = 0;
				#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public AC_AccountTitleInFeeType()
		{
		}
		
		///<summary>
		///
		///</summary>
		public AC_AccountTitleInFeeType(int id, int accounttitle, int feetype)
		{
			_id           = id;
			_accounttitle = accounttitle;
			_feetype      = feetype;
			
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
		///费用科目
		///</summary>
		public int AccountTitle
		{
			get	{ return _accounttitle; }
			set	{ _accounttitle = value; }
		}

		///<summary>
		///FeeType
		///</summary>
		public int FeeType
		{
			get	{ return _feetype; }
			set	{ _feetype = value; }
		}
		
		#endregion
		
		public string ModelName
        {
            get { return "AC_AccountTitleInFeeType"; }
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
					case "AccountTitle":
						return _accounttitle.ToString();
					case "FeeType":
						return _feetype.ToString();
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
					case "AccountTitle":
						int.TryParse(value, out _accounttitle);
						break;
					case "FeeType":
						int.TryParse(value, out _feetype);
						break;

				}
			}
        }
		#endregion
	}
}
