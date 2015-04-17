using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MCSFramework.WSI.Model
{
    [Serializable]
    public class APPTest
    {
        public string DeviceCode = "";
        public string NetworkCode = "";
        public string ClientCode = "";
        public string Address = "";
        public string Remark = "";
        public DateTime InsertTime = new DateTime(1900, 1, 1);

        public List<AppTestDetail> Items = new List<AppTestDetail>();

        public APPTest() { }
    }

    [Serializable]
    public class AppTestDetail
    {
        public int TestItem = 0;
        public DateTime BeginTime= new DateTime(1900, 1, 1);
        public DateTime EndTime = new DateTime(1900, 1, 1);
        public int Value = 0;
        public string Remark = "";

        public AppTestDetail() { }
    }
}