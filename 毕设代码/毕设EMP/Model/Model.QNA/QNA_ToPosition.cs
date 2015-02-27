// ===================================================================
// 文件： QNA_ToPosition.cs
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
    ///QNA_ToPosition数据实体类
    /// </summary>
    [Serializable]
    public class QNA_ToPosition : IModel
    {
        #region 私有变量定义
        private int _id = 0;
        private int _projectid = 0;
        private int _position = 0;
        #endregion

        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public QNA_ToPosition()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public QNA_ToPosition(int id, int projectid, int position)
        {
            _id = id;
            _projectid = projectid;
            _position = position;

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
        ///职务
        ///</summary>
        public int Position
        {
            get { return _position; }
            set { _position = value; }
        }

        #endregion

        public string ModelName
        {
            get { return "QNA_ToPosition"; }
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
                    case "Position":
                        return _position.ToString();
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
                    case "Position":
                        int.TryParse(value, out _position);
                        break;

                }
            }
        }
        #endregion
    }
}
