using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MCSFramework.Model.OA;

namespace MCSFramework.WSI.Model
{
    [Serializable]
    public class Message
    {
        public int MsgID = 0;
        public string Sender = "";
        public string SenderRealName = "";
        public string Content = "";
        public string IsRead = "N";
        public DateTime SendTime = new DateTime(1900, 1, 1);
        
        //10:公告ID 20:直销订单ID
        public string KeyType = "";
        public string KeyValue = "";

        public Message() { }       

    }


}