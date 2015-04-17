// ===================================================================
// 文件： UD_Panel_Table.cs
// 项目名称：
// 创建时间：2008-12-9
// 作者:	   
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
//using MCSFramework.Common;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model
{
	/// <summary>
	///UD_Panel_Table数据实体类
	/// </summary>
	[Serializable]
    public class UD_Panel_Table : IModel
	{
		#region 变量定义
        private Guid _id = Guid.NewGuid();
        private Guid _panelid ;
        private Guid _tableid ;
				
		#endregion
		
		#region 构造函数
		///<summary>
		///
		///</summary>
		public UD_Panel_Table()
		{
		}
		
		///<summary>
		///
		///</summary>
        public UD_Panel_Table(Guid id, Guid panelid, Guid tableid)
		{
			_id      = id;
			_panelid = panelid;
			_tableid = tableid;
			
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
		///TableID
		///</summary>
        public Guid TableID
		{
			get 
			{	return _tableid;	}
			set 
			{	_tableid = value;	}
		}
		

		#endregion

        public string ModelName
        {
            get { return "UD_Panel_Table"; }
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
                    case "TableID":
                        return _tableid.ToString();
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
                    case "TableID":
                        _tableid = new Guid(value);
                        break;

                }
            }
        }
        #endregion
	}
}
