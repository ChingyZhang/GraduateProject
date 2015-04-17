using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MCSFramework.Model.Pub;

namespace MCSFramework.WSI.Model
{
    /// <summary>
    /// 附件信息
    /// </summary>
    [Serializable]
    public class Attachment
    {
        public string AttName = string.Empty;
        public string ExtName = string.Empty;
        public Guid GUID = Guid.Empty;
        public DateTime UploadTime = new DateTime(1900, 1, 1);
        public int FileSize = 0;
        public bool IsFirstPicture = false; //是否首要图片

        public Attachment() { }

        public Attachment(ATMT_Attachment m)
        {
            if (m != null)
            {
                AttName = m.Name;
                ExtName = m.ExtName;
                GUID = m.GUID;
                UploadTime = m.UploadTime;
                FileSize = m.FileSize;
                IsFirstPicture = m["IsFirstPicture"] == "Y";
            }
        }
    }
}
