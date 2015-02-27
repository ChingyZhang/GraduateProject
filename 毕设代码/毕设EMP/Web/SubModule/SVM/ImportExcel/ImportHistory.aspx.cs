using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL.Pub;
using MCSFramework.BLL;
using MCSFramework.Model;
using MCSFramework.Common;
using MCSFramework.Model.Pub;
using MCSFramework.BLL.SVM;
using MCSFramework.Model.SVM;

public partial class SubModule_SVM_ImportExcel_ImportHistory : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["IsOpponent"] = Request.QueryString["IsOpponent"] == null ? 9 : int.Parse(Request.QueryString["IsOpponent"]);//1成品，2赠品，3重点品项，9费率

            ViewState["State"] = Request.QueryString["State"] == null ? 1 : int.Parse(Request.QueryString["State"]);//1.下载 2.上传
            ViewState["ClientType"] = Request.QueryString["ClientType"] == null ? 0 : int.Parse(Request.QueryString["ClientType"]);
            BindDropdown();
            BindGrid();
            tb_keytarget.Visible = (int)(ViewState["IsOpponent"]) == 2 || (int)(ViewState["IsOpponent"]) == 3;
            tb_jxc.Visible = !tb_keytarget.Visible;
            if (tb_keytarget.Visible)
            {
                MCSTabControl1.Items[1].Text = "目标上传状态";
                lb_PageTitle.Text = (int)(ViewState["IsOpponent"]) == 2 ? "从Excel导入办事处重点品项记录" : "从Excel导入办事处费率";
            }
        }
    }
    private void BindDropdown()
    {
        IList<AC_AccountMonth> _monthlist = AC_AccountMonthBLL.GetModelList("");
        ddl_AccountMonth.DataSource = _monthlist;
        ddl_AccountMonth.DataBind();

        ddl_AccountMonth2.DataSource = AC_AccountMonthBLL.GetModelList("Year>=" + (DateTime.Now.Year - 1).ToString());
        ddl_AccountMonth2.DataBind();


        int JXCDelayDays = ConfigHelper.GetConfigInt("JXCDelayDays");

        ddl_AccountMonth.SelectedValue = (AC_AccountMonthBLL.GetCurrentMonth() - 1).ToString();

        ddl_AccountMonth2.SelectedValue = "0";


        select_Staff.SelectValue = Session["UserID"].ToString();
        select_Staff.SelectText = Session["UserRealName"].ToString();

        if ((int)(ViewState["IsOpponent"]) != 2 && (int)(ViewState["IsOpponent"]) != 3)
        {
            ddl_Opponent.SelectedValue = ViewState["IsOpponent"].ToString();
        }

        string condition = " 1=1 ";
        if (!string.IsNullOrEmpty(ConfigHelper.GetConfigString("OrganizePartCity-CityLevel")))
        {
            condition = " Level=" + ConfigHelper.GetConfigString("OrganizePartCity-CityLevel");
        }

        ddl_OrganizeCity.DataSource = Addr_OrganizeCityBLL.GetModelList(condition);
        ddl_OrganizeCity.DataTextField = "Name";
        ddl_OrganizeCity.DataValueField = "ID";
        ddl_OrganizeCity.DataBind();
        ddl_OrganizeCity.Items.Insert(0, new ListItem("请选择", "0"));

        ddl_ExcelState.DataSource = DictionaryBLL.GetDicCollections((int)(ViewState["State"]) == 2 ? "SVM_UploadState" : "SVM_DownloadState");
        ddl_ExcelState.DataBind();
        ddl_ExcelState.Items.Insert(0, new ListItem("所有", "0"));

        ddl_ExcelState2.DataSource = ddl_ExcelState.DataSource;
        ddl_ExcelState2.DataBind();
        ddl_ExcelState2.Items.Insert(0, new ListItem("所有", "0"));
        if ((int)(ViewState["State"]) == 1)
        {
            ddl_ExcelState.SelectedValue = "2";
            ddl_ExcelState2.SelectedValue = "2";
        }


        MCSTabControl1.SelectedIndex = (int)ViewState["State"] - 1;

        ddl_ClientType.SelectedValue = ViewState["ClientType"].ToString();

    }

    private void BindGrid()
    {
        string condtion = "";


        if ((int)(ViewState["IsOpponent"]) == 2 || (int)(ViewState["IsOpponent"]) == 3)
        {
            condtion = "AccountMonth=" + ddl_AccountMonth2.SelectedValue;
            condtion += " AND IsOpponent=" + ViewState["IsOpponent"].ToString();
            if (ddl_ExcelState2.SelectedValue != "0")
            {
                condtion += " AND State=" + ddl_ExcelState2.SelectedValue;
            }
        }
        else
        {
            condtion = "AccountMonth=" + ddl_AccountMonth.SelectedValue;
            if (ddl_ExcelState.SelectedValue != "0")
            {
                condtion += " AND State=" + ddl_ExcelState.SelectedValue;
            }
            if (ddl_Opponent.SelectedValue != "0")
            {
                condtion += " AND IsOpponent=" + ddl_Opponent.SelectedValue;
            }
        }

        if (MCSTabControl1.SelectedIndex == 0)
        {
            if (ViewState["IsOpponent"].ToString() != "2" && ViewState["IsOpponent"].ToString() != "3")
            {
                condtion += " AND SVM_DownloadTemplate.InsertStaff=" + (select_Staff.SelectValue == "" ? Session["UserID"].ToString() : select_Staff.SelectValue);
                if (ddl_ClientType.SelectedValue != "0")
                {
                    condtion += " AND MCS_SYS.dbo.UF_Spilt(SVM_DownloadTemplate.ExtPropertys,'|',2)=" + ddl_ClientType.SelectedValue;
                }
            }
            else
            {
                if (ddl_OrganizeCity.SelectedValue != "0")
                {
                    condtion += " AND MCS_SYS.dbo.UF_Spilt(SVM_DownloadTemplate.ExtPropertys,'|',3)=" + ddl_OrganizeCity.SelectedValue;
                }
            }
            gv_downtemplate.ConditionString = condtion + " ORDER BY SVM_DownloadTemplate.ID DESC";
            gv_downtemplate.BindGrid();
        }
        else
        {
            if (ViewState["IsOpponent"].ToString() != "2" && ViewState["IsOpponent"].ToString() != "3")
            {
                if (ddl_ClientType.SelectedValue != "0")
                {
                    condtion += " AND MCS_SYS.dbo.UF_Spilt(SVM_UploadTemplate.ExtPropertys,'|',2)=" + ddl_ClientType.SelectedValue;
                }
                condtion += " AND SVM_UploadTemplate.InsertStaff=" + (select_Staff.SelectValue == "" ? Session["UserID"].ToString() : select_Staff.SelectValue);
            }
            else
            {
                if (ddl_OrganizeCity.SelectedValue != "0")
                {
                    condtion += @" AND SVM_UploadTemplate.Name LIKE '%" + ddl_OrganizeCity.SelectedItem.Text.Trim() + "%'";
                }

            }
            gv_uptemplate.ConditionString = condtion + " ORDER BY SVM_UploadTemplate.ID DESC"; ;
            gv_uptemplate.BindGrid();

        }
    }
    protected void bt_search_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void bt_DownloadTemplate_Click(object sender, EventArgs e)
    {
        Button bt = (Button)sender;
        GridViewRow drv = (GridViewRow)bt.NamingContainer;
        int rowIndex = drv.RowIndex;
        SVM_DownloadTemplateBLL _bll;

        int templateid = int.Parse(gv_downtemplate.DataKeys[rowIndex]["SVM_DownloadTemplate_ID"].ToString());
        _bll = new SVM_DownloadTemplateBLL(templateid);
        if (_bll.Model.State == 1)
        {
            MessageBox.Show(this, "模版还未生成，目前在生成队列中第" + GetDownWait(templateid.ToString()) + "个");
            return;
        }
        string path = ConfigHelper.GetConfigString("AttachmentPath");
        if (path.StartsWith("~")) path = Server.MapPath(path);
        if (!path.EndsWith("\\")) path = path + "\\";
        if (ViewState["IsOpponent"].ToString() != "2" && ViewState["IsOpponent"].ToString() != "3")
        {
            path += "ImportExcelSVM\\Download\\" + Session["UserName"].ToString() + "\\" + _bll.Model.Name;
        }
        else
        {
            path += "ImportExcelSVM\\Download\\" + _bll.Model.AccountMonth.ToString() + "\\" + _bll.Model.Name;
        }
        Response.Write(path);
        _bll.Model.State = 3;
        Downloadfile(path, _bll.Model.Name);
        _bll.Update();
    }
    private void Downloadfile(string path, string filename)
    {
        try
        {
            Response.Clear();
            Response.BufferOutput = true;
            Response.ContentType = "application/ms-excel";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(filename));
            Response.WriteFile(path);
            Response.Flush();
            Response.End();
        }
        catch (System.Exception err)
        {
            MessageBox.Show(this, "系统错误-3!" + err.Message);
        }
    }
    protected void MCSTabControl1_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        ViewState["State"] = int.Parse(MCSTabControl1.SelectedTabItem.Value);
        ddl_ExcelState.DataSource = DictionaryBLL.GetDicCollections((int)(ViewState["State"]) == 2 ? "SVM_UploadState" : "SVM_DownloadState");
        ddl_ExcelState.DataBind();
        ddl_ExcelState.Items.Insert(0, new ListItem("所有", "0"));
        gv_downtemplate.Visible = MCSTabControl1.SelectedIndex == 0;
        gv_uptemplate.Visible = !gv_downtemplate.Visible;
        BindGrid();
    }
    protected void bt_ViewRemark_Click(object sender, EventArgs e)
    {

        Button bt = (Button)sender;
        GridViewRow drv = (GridViewRow)bt.NamingContainer;
        int rowIndex = drv.RowIndex;
        int templateid = int.Parse(gv_uptemplate.DataKeys[rowIndex]["SVM_UploadTemplate_ID"].ToString());
        SVM_UploadTemplateBLL _bll = new SVM_UploadTemplateBLL(templateid);

        lb_ErrorInfo.Text = _bll.Model.Remark;
        if (_bll.Model.State > 1 && _bll.Model.Remark != "") BindGrid();

    }
    protected string GetDownWait(string templateid)
    {
        IList<SVM_DownloadTemplate> downlist = SVM_DownloadTemplateBLL.GetModelList("State=1 AND AccountMonth=" + ddl_AccountMonth.SelectedValue + " AND ID<=" + templateid);
        return downlist.Count > 0 ? (downlist.Count).ToString() : "";
    }
    protected string GetUpWait(string templateid)
    {
        IList<SVM_UploadTemplate> uplist = SVM_UploadTemplateBLL.GetModelList("State=1 AND AccountMonth=" + ddl_AccountMonth.SelectedValue + " AND ID<=" + templateid);
        return uplist.Count > 0 ? (uplist.Count).ToString() : "";
    }
    protected string GetUpWaitTime(string templateid)
    {
        IList<SVM_UploadTemplate> uplist = SVM_UploadTemplateBLL.GetModelList("State=1 AND AccountMonth=" + ddl_AccountMonth.SelectedValue + " AND ID<=" + templateid);
        if (uplist.Count > 0)
        {
            return DateTime.Now.AddSeconds(30 * uplist.Count).ToString();
        }
        return "";
    }

    protected void ddl_AccountMonth2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if ((int)(ViewState["IsOpponent"]) == 3 && SVM_DownloadTemplateBLL.GetModelList("AccountMonth=" + ddl_AccountMonth2.SelectedValue + " AND IsOpponent=3 AND MCS_SYS.dbo.UF_Spilt(ExtPropertys,'|',3)='1'").Count == 0)
        {
            string filename = "办事处月度费率导入模板" + "-" + DateTime.Today.ToString("yyyyMMdd") + ".xls";
            SVM_DownloadTemplateBLL _bll = new SVM_DownloadTemplateBLL();
            _bll.Model.Name = filename;
            _bll.Model.AccountMonth = int.Parse(ddl_AccountMonth2.SelectedValue);
            _bll.Model.State = 1;
            _bll.Model.IsOpponent = 3;
            _bll.Model.InsertStaff = (int)Session["UserID"];
            _bll.Model.InsertTime = DateTime.Now;
            _bll.Model["OrganizeCity"] = "1";
            _bll.Model["UserName"] = Session["UserName"].ToString();
            _bll.Add();
        }
    }
}
