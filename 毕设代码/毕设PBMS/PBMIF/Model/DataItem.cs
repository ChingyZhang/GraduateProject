using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MCSFramework.WSI.Model
{
    [Serializable]
    public class DicDataItem
    {
        public int ID = 0;
        public string Name = "";
        public string Value = "";
        public string Remark = "";

        public DicDataItem() { }
        public DicDataItem(int id, string name)
        {
            ID = id;
            Name = name;
        }

        public DicDataItem(int id, string name, string remark)
        {
            ID = id;
            Name = name;
            Remark = remark;
        }

        public DicDataItem(int id, string name, string value, string remark)
        {
            ID = id;
            Name = name;
            Value = value;
            Remark = remark;
        }

        public DicDataItem(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}