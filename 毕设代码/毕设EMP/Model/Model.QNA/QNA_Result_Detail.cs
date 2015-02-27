// ===================================================================
// 文件： QNA_Result_Detail.cs
// 项目名称：
// 创建时间：2009/11/29
// 作者:	   Shen Gang
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.QNA
{
	/// <summary>
	///QNA_Result_Detail数据实体类
	/// </summary>
	[Serializable]
	public class QNA_Result_Detail : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _result = 0;
		private int _question = 0;
		private int _option = 0;
		private string _optiontext = string.Empty;
		
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public QNA_Result_Detail()
		{
		}
		
		///<summary>
		///
		///</summary>
		public QNA_Result_Detail(int id, int result, int question, int option, string optiontext)
		{
			_id           = id;
			_result       = result;
			_question     = question;
			_option       = option;
			_optiontext   = optiontext;
			
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
		///结果头ID
		///</summary>
		public int Result
		{
			get	{ return _result; }
			set	{ _result = value; }
		}

		///<summary>
		///问题
		///</summary>
		public int Question
		{
			get	{ return _question; }
			set	{ _question = value; }
		}

		///<summary>
		///所选选项
		///</summary>
		public int Option
		{
			get	{ return _option; }
			set	{ _option = value; }
		}

		///<summary>
		///调研结果文本
		///</summary>
		public string OptionText
		{
			get	{ return _optiontext; }
			set	{ _optiontext = value; }
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
            get { return "QNA_Result_Detail"; }
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
					case "Result":
						return _result.ToString();
					case "Question":
						return _question.ToString();
					case "Option":
						return _option.ToString();
					case "OptionText":
						return _optiontext;
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
					case "Result":
						int.TryParse(value, out _result);
						break;
					case "Question":
						int.TryParse(value, out _question);
						break;
					case "Option":
						int.TryParse(value, out _option);
						break;
					case "OptionText":
						_optiontext = value ;
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
