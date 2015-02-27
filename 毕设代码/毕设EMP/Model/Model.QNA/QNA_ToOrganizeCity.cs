// ===================================================================
// 文件： QNA_ToOrganizeCity.cs
// 项目名称：
// 创建时间：2011/9/7
// 作者:	   TT
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.QNA
{
    /// <summary>
    ///QNA_ToOrganizeCity数据实体类
    /// </summary>
    [Serializable]
    public class QNA_ToOrganizeCity : IModel
    {
        #region 私有变量定义
        private int _id = 0;
        private int _projectid = 0;
        private int _organizecity = 0;
        #endregion

        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public QNA_ToOrganizeCity()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public QNA_ToOrganizeCity(int id, int projectid, int organizecity)
        {
            _id = id;
            _projectid = projectid;
            _organizecity = organizecity;

        }
        #endregion

        #region 公共属性
        ///<summary>
        ///ID
        ///</summary>
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        ///<summary>
        ///公告ID
        ///</summary>
        public int ProjectID
        {
            get { return _projectid; }
            set { _projectid = value; }
        }

        ///<summary>
        ///管理片区
        ///</summary>
        public int OrganizeCity
        {
            get { return _organizecity; }
            set { _organizecity = value; }
        }

        #endregion

        public string ModelName
        {
            get { return "QNA_ToOrganizeCity"; }
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
                    case "ProjectID":
                        return _projectid.ToString();
                    case "OrganizeCity":
                        return _organizecity.ToString();
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
                    case "ProjectID":
                        int.TryParse(value, out _projectid);
                        break;
                    case "OrganizeCity":
                        int.TryParse(value, out _organizecity);
                        break;

                }
            }
        }
        #endregion
    }
}
