// ===================================================================
// 文件： PDT_Classify.cs
// 项目名称：
// 创建时间：2008-12-22
// 作者:	   
// ===================================================================
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using MCSFramework.IFStrategy;

namespace MCSFramework.Model.Pub
{
    /// <summary>
    ///PDT_Classify数据实体类
    /// </summary>
    [Serializable]
    public class PDT_Classify : IModel
    {
        #region 私有变量定义
        private int _id = 0;
        private string _name = string.Empty;
        private int _brand = 0;
        private int _sortid = 0;


        #endregion

        #region 构造函数
        ///<summary>
        ///
        ///</summary>
        public PDT_Classify()
        {
        }

        ///<summary>
        ///
        ///</summary>
        public PDT_Classify(int id, string name, int brand,int sortid)
        {
            _id = id;
            _name = name;
            _brand = brand;
            _sortid = sortid;
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
        ///Name
        ///</summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        ///<summary>
        ///Brand
        ///</summary>
        public int Brand
        {
            get { return _brand; }
            set { _brand = value; }
        }

        //分类排序
        public int SortID
        {
            get { return _sortid; }
            set { _sortid = value; }
        }
        #endregion

        public string ModelName
        {
            get { return "PDT_Classify"; }
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
                    case "Brand":
                        return _brand.ToString();
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
                        int.TryParse(value, out _id);
                        break;
                    case "Name":
                        _name = value;
                        break;
                    case "Brand":
                        int.TryParse(value, out _brand);
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
