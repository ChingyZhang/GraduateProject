using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MCSFramework.WSI.Model
{
    [Serializable]
    public class AppVersion
    {
        public string AppCode = "";     //APP代码
        public string AppName = "";     //APP名称
        public string AppAbout = "";    //APP关于
        public int CurrentVersion = 0;  //当前版本号
        public string VersionName = ""; //当前版本名称
        public int MiniVersion = 0;     //支持最低版本号

        public DateTime PublishDate = new DateTime(1900, 1, 1);     //发布日期
        public string PublishRemark = "";                           //发布说明
        public string AppDownloadURL = "";                          //APP更新路径
        public string AppDownloadURL2 = "";                         //专网APP更新路径

        public AppVersion() { }

        //public AppVersion(int ID)
        //{
        //}

        //private void FillModel() { }
    }
}