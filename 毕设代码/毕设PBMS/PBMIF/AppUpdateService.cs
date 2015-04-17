using MCSFramework.Common;
using MCSFramework.WSI.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MCSFramework.WSI
{
    public class AppUpdateService
    {
        public AppUpdateService()
        {
            LogWriter.FILE_PATH = "C:\\MCSLog_PBMIF";
        }

        /// <summary>
        /// 获取当前版本
        /// </summary>
        /// <param name="AppCode">APP编码</param>
        /// <returns>未加密Json字符串</returns>

        public static AppVersion GetLastVersionInfo(string AppCode)
        {
            if (AppCode.StartsWith("PBMSAPP"))
            {
                AppVersion ver = new AppVersion();
                ver.AppCode = AppCode;
                ver.AppName = "PBMS-APP";
                ver.AppAbout = "   ";

                ver.CurrentVersion = ConfigHelper.GetConfigInt("CurrentVersion-" + AppCode);
                ver.VersionName = ConfigHelper.GetConfigString("VersionName-" + AppCode);
                ver.MiniVersion = ConfigHelper.GetConfigInt("MinAppVersion-" + AppCode);

                DateTime.TryParse(ConfigHelper.GetConfigString("PublishDate-" + AppCode), out ver.PublishDate);
                ver.PublishRemark = ConfigHelper.GetConfigString("PublishRemark-" + AppCode);

                ver.AppDownloadURL = ConfigHelper.GetConfigString("AppDownloadURL-" + AppCode);

                return ver;
            }
            else
            {
                return null;
            }
        }
    }
}