// ===================================================================
// 文件： PM_SalaryLevelDetail.cs
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
	///PM_SalaryLevelDetail数据实体类
	/// </summary>
	[Serializable]
	public class PM_SalaryLevelDetail : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _levelid = 0;
		private decimal _complete1 = 0;
		private decimal _complete2 = 0;
		private decimal _rate = 0;
				#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public PM_SalaryLevelDetail()
		{
		}
		
		///<summary>
		///
		///</summary>
		public PM_SalaryLevelDetail(int id, int levelid, decimal complete1, decimal complete2, decimal rate)
		{
			_id        = id;
			_levelid   = levelid;
			_complete1 = complete1;
			_complete2 = complete2;
			_rate      = rate;
			
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
		///工资标准
		///</summary>
		public int LevelID
		{
			get	{ return _levelid; }
			set	{ _levelid = value; }
		}

		///<summary>
		///开始完成率
		///</summary>
		public decimal Complete1
		{
			get	{ return _complete1; }
			set	{ _complete1 = value; }
		}

		///<summary>
		///截止完成率
		///</summary>
		public decimal Complete2
		{
			get	{ return _complete2; }
			set	{ _complete2 = value; }
		}

		///<summary>
		///提成系数
		///</summary>
		public decimal Rate
		{
			get	{ return _rate; }
			set	{ _rate = value; }
		}
		
		#endregion
		
		public string ModelName
        {
            get { return "PM_SalaryLevelDetail"; }
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
					case "LevelID":
						return _levelid.ToString();
					case "Complete1":
						return _complete1.ToString();
					case "Complete2":
						return _complete2.ToString();
					case "Rate":
						return _rate.ToString();
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
					case "LevelID":
						int.TryParse(value, out _levelid);
						break;
					case "Complete1":
						decimal.TryParse(value, out _complete1);
						break;
					case "Complete2":
						decimal.TryParse(value, out _complete2);
						break;
					case "Rate":
						decimal.TryParse(value, out _rate);
						break;

				}
			}
        }
		#endregion
	}
}
