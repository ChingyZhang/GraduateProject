// ===================================================================
// 文件： QNA_QuestionOption.cs
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
	///QNA_QuestionOption数据实体类
	/// </summary>
	[Serializable]
	public class QNA_QuestionOption : IModel
	{
		#region 私有变量定义
		private int _id = 0;
		private int _question = 0;
		private string _optionname = string.Empty;
		private int _nextquestion = 0;
		private string _caninputtext = string.Empty;
		
		private NameValueCollection _extpropertys;		
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public QNA_QuestionOption()
		{
		}
		
		///<summary>
		///
		///</summary>
		public QNA_QuestionOption(int id, int question, string optionname, int nextquestion, string caninputtext)
		{
			_id           = id;
			_question     = question;
			_optionname   = optionname;
			_nextquestion = nextquestion;
			_caninputtext = caninputtext;
			
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
		///问题
		///</summary>
		public int Question
		{
			get	{ return _question; }
			set	{ _question = value; }
		}

		///<summary>
		///选项名称
		///</summary>
		public string OptionName
		{
			get	{ return _optionname; }
			set	{ _optionname = value; }
		}

		///<summary>
		///下一问题
		///</summary>
		public int NextQuestion
		{
			get	{ return _nextquestion; }
			set	{ _nextquestion = value; }
		}

		///<summary>
		///是否可输入文本
		///</summary>
		public string CanInputText
		{
			get	{ return _caninputtext; }
			set	{ _caninputtext = value; }
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
            get { return "QNA_QuestionOption"; }
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
					case "Question":
						return _question.ToString();
					case "OptionName":
						return _optionname;
					case "NextQuestion":
						return _nextquestion.ToString();
					case "CanInputText":
						return _caninputtext;
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
					case "Question":
						int.TryParse(value, out _question);
						break;
					case "OptionName":
						_optionname = value ;
						break;
					case "NextQuestion":
						int.TryParse(value, out _nextquestion);
						break;
					case "CanInputText":
						_caninputtext = value ;
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
