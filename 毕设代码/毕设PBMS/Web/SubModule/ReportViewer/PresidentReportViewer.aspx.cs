using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL.RPT;
using MCSFramework.Model.RPT;
using MCSFramework.BLL;

public partial class SubModule_ReportViewer_PresidentReportViewer : System.Web.UI.Page
{
    public int superid = 1;
    public string pathname = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //初始化当前目录ID
            ViewState["FolderID"] = Request.QueryString["FolderID"] != null ? Request.QueryString["FolderID"].ToString() : "76";
            //上层目录
            Rpt_FolderBLL fbll = new Rpt_FolderBLL(int.Parse((string)ViewState["FolderID"]));
            superid = fbll.Model.SuperID != 0 ? fbll.Model.SuperID : 1;
            //当前目录路径
            //pathname = TreeTableBLL.GetFullPathName("MCS_Reports.dbo.Rpt_Folder", "ID", "Name", "SuperID", 0, int.Parse((string)ViewState["FolderID"]));
            pathname = TreeTableBLL.GetFullPathName("MCS_Reports.dbo.Rpt_Folder", int.Parse((string)ViewState["FolderID"]));
            
            BindData();
        }
    }
    private void BindData()
    {
        IList<Rpt_Folder> _folder_list = Rpt_FolderBLL.GetModelList(" superid=" + (string)ViewState["FolderID"]);
        rp_1.DataSource = _folder_list;
        rp_1.DataBind();

        IList<Rpt_Report> _file_list = Rpt_ReportBLL.GetModelList(" folder=" + (string)ViewState["FolderID"]);
        rp_2.DataSource = _file_list;
        rp_2.DataBind();
    }
}