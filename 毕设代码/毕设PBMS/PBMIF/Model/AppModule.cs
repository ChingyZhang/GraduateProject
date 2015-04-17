using MCSFramework.BLL.Pub;
using MCSFramework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MCSFramework.WSI.Model
{

    /// <summary>
    /// Right_ModuleWithApp 的摘要说明
    /// </summary>
    [Serializable]
    public class AppModule
    {
        public int ID = 0;
        public string TextName = "";     //模块名称
        public int SuperID = 0;              //上级模块
        public int SortValue = 0;            //显示顺序
        public string IsAnonymous = "N";     //匿名访问
        public string IsMenu = "N";          //菜单标识
        public string IsHttp = "N";          //是否HTTP请求
        public string PageName = "";         //链接页面名称
        public Guid ImageGuid = Guid.Empty;  //模块显示图像

        public AppModule(Right_ModuleWithApp _moduleapp)
        {
            ID = _moduleapp.ID;
            TextName = _moduleapp.Name;
            SuperID = _moduleapp.SuperID;
            SortValue = _moduleapp.SortValue;
            IsAnonymous = _moduleapp.IsAnonymous;
            IsMenu = _moduleapp.IsMenu;
            IsHttp = _moduleapp.IsHttp;
            PageName = _moduleapp.PageName;
            MCSFramework.BLL.Pub.ATMT_AttachmentBLL _attachbll = new MCSFramework.BLL.Pub.ATMT_AttachmentBLL(_moduleapp.DefaultIco);
            if (_attachbll.Model != null)
            {
                ImageGuid = _attachbll.Model.GUID;
            }

        }
    }
}