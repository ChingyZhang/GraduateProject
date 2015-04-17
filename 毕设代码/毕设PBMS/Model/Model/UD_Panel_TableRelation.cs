// ===================================================================
// 文件： UD_Panel_TableRelation.cs
// 项目名称：
// 创建时间：2008-12-9
// 作者:	   
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using MCSFramework.Common;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model
{
	/// <summary>
	///UD_Panel_TableRelation数据实体类
	/// </summary>
	[Serializable]
    public class UD_Panel_TableRelation : IModel
	{
		#region 变量定义
        private Guid _id = Guid.NewGuid();
        private Guid _panelid ;
        private Guid _parenttableid ;
        private Guid _parentfieldid ;
        private Guid _childtableid ;
        private Guid _childfieldid ;
        private string _joinmode = "INNER JOIN";
        private string _relationcondition = "";
        private int _sortid = 0;
       	#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public UD_Panel_TableRelation()
		{
		}
		
		///<summary>
		///
		///</summary>
        public UD_Panel_TableRelation(Guid id, Guid panelid, Guid parenttableid, Guid parentfieldid, Guid childtableid, Guid childfieldid)
		{
			_id            = id;
			_panelid       = panelid;
			_parenttableid = parenttableid;
			_parentfieldid = parentfieldid;
			_childtableid  = childtableid;
			_childfieldid  = childfieldid;
			
		}
		#endregion
		
		#region 公共属性
		
		///<summary>
		///ID
		///</summary>
        public Guid ID
		{
			get 
			{	return _id;	}
			set 
			{	_id = value;	}
		}

		///<summary>
		///PanelID
		///</summary>
        public Guid PanelID
		{
			get 
			{	return _panelid;	}
			set 
			{	_panelid = value;	}
		}

		///<summary>
		///ParentTableID
		///</summary>
        public Guid ParentTableID
		{
			get 
			{	return _parenttableid;	}
			set 
			{	_parenttableid = value;	}
		}

		///<summary>
		///ParentFieldID
		///</summary>
        public Guid ParentFieldID
		{
			get 
			{	return _parentfieldid;	}
			set 
			{	_parentfieldid = value;	}
		}

		///<summary>
		///ChildTableID
		///</summary>
        public Guid ChildTableID
		{
			get 
			{	return _childtableid;	}
			set 
			{	_childtableid = value;	}
		}

		///<summary>
		///ChildFieldID
		///</summary>
        public Guid ChildFieldID
		{
			get 
			{	return _childfieldid;	}
			set 
			{	_childfieldid = value;	}
		}

        /// <summary>
        /// 表关联方式
        /// </summary>
        public string JoinMode
        {
            get { return _joinmode; }
            set { _joinmode = value; }
        }

        /// <summary>
        /// 关联条件
        /// </summary>
        public string RelationCondition
        {
            get { return _relationcondition; }
            set { _relationcondition = value; }
        }

        public int SortID
        {
            get { return _sortid; }
            set { _sortid = value; }
        }
		#endregion

        public string ModelName
        {
            get { return "UD_Panel_TableRelation"; }
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
                    case "PanelID":
                        return _panelid.ToString();
                    case "ParentTableID":
                        return _parenttableid.ToString();
                    case "ParentFieldID":
                        return _parentfieldid.ToString();
                    case "ChildTableID":
                        return _childtableid.ToString();
                    case "ChildFieldID":
                        return _childfieldid.ToString();
                    case "JoinMode":
                        return _joinmode;
                    case "RelationCondition":
                        return _relationcondition;
                    case "SortID":
                        return _sortid.ToString();
                    default:
                        return "";
                }
            }
            set
            {
                switch (FieldName)
                {
                    case "ID":
                        _id = new Guid(value);
                        break;
                    case "PanelID":
                        _panelid = new Guid(value);
                        break;
                    case "ParentTableID":
                        _parenttableid = new Guid(value);
                        break;
                    case "ParentFieldID":
                        _parentfieldid = new Guid(value);
                        break;
                    case "ChildTableID":
                        _childtableid = new Guid(value);
                        break;
                    case "ChildFieldID":
                        _childfieldid = new Guid(value);
                        break;
                    case "JoinMode":
                        _joinmode = value;
                        break;
                    case "RelationCondition":
                        _relationcondition = value;
                        break;
                    case "SortID":
                        int.TryParse(value, out _sortid);
                        break;
                }
            }
        }
        #endregion
	
	}
}
