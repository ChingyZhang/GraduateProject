using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.CM;
using MCSFramework.BLL.Promotor;
using MCSFramework.BLL.Pub;
using MCSFramework.BLL.SVM;
using MCSFramework.Common;
using MCSFramework.Model.CM;
using MCSFramework.Model.Promotor;
using MCSFramework.Model.Pub;
using MCSFramework.Model.SVM;
using MCSFramework.UD_Control;
using System.Text;
public partial class SubModule_SVM_ImportExcel_ImportGift : System.Web.UI.Page
{
    protected StringBuilder ProdutctGifts;
    protected StringBuilder Testers;
    protected StringBuilder Gifts;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
            select_Staff.SelectValue = Session["UserID"].ToString();
            select_Staff.SelectText = Session["UserRealName"].ToString();
            ViewState["IsOpponent"] = Request.QueryString["IsOpponent"] != null ? int.Parse(Request.QueryString["IsOpponent"]) : 9;
            ViewState["ClientType"] = Request.QueryString["ClientType"] != null ? int.Parse(Request.QueryString["ClientType"]) : 3;
            #region 获取最迟的销量月份
            int JXCDelayDays = ConfigHelper.GetConfigInt("JXCDelayDays");
            AC_AccountMonth month = new AC_AccountMonthBLL(AC_AccountMonthBLL.GetMonthByDate(DateTime.Now.AddDays(-JXCDelayDays))).Model;
            lb_MonthTitle.Text = month.Name + "(" + month.BeginDate.ToString("yyyy-MM-dd") + "至" + month.EndDate.ToString("yyyy-MM-dd") + ")";
            ViewState["month"] = month;
            #endregion
            select_Staff_SelectChange(null, null);
            ViewState["ProdutctGifts"] = new StringBuilder();
            ViewState["Testers"] = new StringBuilder();
            ViewState["Gifts"] = new StringBuilder();

            if ((int)ViewState["IsOpponent"] == 1)
            {
                lb_PageTitle.Text = "从Excel导入零售商进货及销货数量";
                lb_setp1.Text = "第一步：下载门店进销量模板";
                lb_setp2.Text = "第二步：上传门店进销量EXCEL表格";
                if ((int)ViewState["ClientType"] == 2)
                {
                    lb_PageTitle.Text = "从Excel导入分销商进货";
                    lb_setp1.Text = "第一步：下载分销商进货量模板";
                    lb_setp2.Text = "第二步：上传分销商进货量EXCEL表格";
                    diproductprompt.Visible = false;
                    rtgiftprompt.Visible = false;
                    diproductprompt.Visible = true;
                }
                else
                {
                    rtgiftprompt.Visible = false;
                    rtproductprompt.Visible = true;
                    digiftprompt.Visible = false;
                    diproductprompt.Visible = false;
                }
                
                Header.Attributes["WebPageSubCode"] = "IsOpponent=1";
            }
            else
            {
                Header.Attributes["WebPageSubCode"] = "IsOpponent=9";
                if ((int)ViewState["ClientType"] == 2)
                {
                    lb_setp1.Text = "第一步：下载分销商赠品进货模板";
                    lb_setp2.Text = "第二步：上传分销商赠品进货Excel表格";
                    diproductprompt.Visible = false;
                    rtproductprompt.Visible = false;
                    rtgiftprompt.Visible = false;
                    digiftprompt.Visible = true;
                }
                
            }
        }
        ProdutctGifts = (StringBuilder)ViewState["ProdutctGifts"];
        Testers = (StringBuilder)ViewState["Testers"];
        Gifts = (StringBuilder)ViewState["Gifts"];
    }

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if ((int)ViewState["IsOpponent"] == 1)
        {
            lb_PageTitle.Text = (int)ViewState["ClientType"] == 2?"从Excel导入分销商进货":"从Excel导入零售商进货及销货数量";
        }
        else
        {
            lb_PageTitle.Text = (int)ViewState["ClientType"] == 2 ? "从Excel导入分销商赠品进货" : "从Excel导入零售商赠品进货";
        }
    }
    protected void chk_ID_CheckedChanged(object sender, EventArgs e)
    {
        System.Web.UI.WebControls.CheckBox cb = (System.Web.UI.WebControls.CheckBox)sender;
        GridViewRow drv = (GridViewRow)cb.NamingContainer;
        
        int rowIndex = drv.RowIndex;
        if (cb.Checked)
        {
            GetProducts(rowIndex);
        }
        else
        {
            RemoveProducts(rowIndex);
        }
    }

   
    private void BindProductGrid()
    {
        div_gift.Visible = true;
        string condtion = " PDT_Product.State=1 AND PDT_Product.ApproveFlag=1";
        switch (MCSTabControl2.SelectedTabItem.Value)
        {
            case "1"://3	婴幼儿本品赠品
                condtion += " AND PDT_Product.Brand=3";
                break;
            case "2":
                condtion += " AND PDT_Product.Brand IN (SELECT ID FROM MCS_Pub.dbo.PDT_Brand WHERE IsOpponent='9' AND Name LIKE '%试用装%')";
                break;
            case "3"://4-活动道具;5-印刷品及DM;6-异质赠品
                condtion += " AND PDT_Product.Brand IN (4,5,6) AND PDT_Product.FactoryPrice>=50";
                break;

        }
        gv_product.PageIndex = 0;
        gv_product.ConditionString = condtion;
        gv_product.BindGrid();
    }

    protected void bt_DownloadTemplate_Click(object sender, EventArgs e)
    {
        #region 获取最迟的销量日期
        AC_AccountMonth month = (AC_AccountMonth)ViewState["month"];
        DateTime day = DateTime.Today < month.EndDate ? DateTime.Today : month.EndDate;
        #endregion

        #region 判断有无选择业代
        if (string.IsNullOrEmpty(select_Staff.SelectValue) || select_Staff.SelectValue == "0")
        {
            MessageBox.Show(this, "对不起，请选择责任业代！");
            return;
        }
        #endregion

        #region 获取业代负责的零售商及所有产品数据
        int staff = int.Parse(select_Staff.SelectValue);
        string condtion = ViewState["ClientType"].ToString() == "2" ? " AND MCS_SYS.dbo.UF_Spilt(ExtPropertys,'|',7)='2'" : "";
        IList<CM_Client> clientlists = CM_ClientBLL.GetModelList("ClientType=" + ViewState["ClientType"].ToString() + " AND ClientManager=" + staff.ToString() +
            " AND ActiveFlag=1 AND ApproveFlag=1 " + condtion + "AND OpenTime<'" + day.ToString("yyyy-MM-dd") + " 23:59:59' ORDER BY Code");
        if (clientlists.Count == 0)
        {
            MessageBox.Show(this, "对不起，没有当前人直接负责的终端店！");
            return;
        }

        #endregion
        string title = (int)ViewState["ClientType"] == 2 ? "分销商" : "零售商";       
        string filename = title+((int)ViewState["IsOpponent"] == 9 ? "赠品进货导入模板-" : "产品进销货量导入模板") + select_Staff.SelectText + "-" + DateTime.Today.ToString("yyyyMMdd") + ".xls";
        if ((int)ViewState["ClientType"] == 2 && (int)ViewState["IsOpponent"] == 1)
        {
            filename = "分销商产品进货导入模板" + select_Staff.SelectText + "-" + DateTime.Today.ToString("yyyyMMdd") + ".xls";
        }
        SVM_DownloadTemplateBLL _bll = new SVM_DownloadTemplateBLL();
        _bll.Model.Name = filename;
        _bll.Model.AccountMonth = month.ID;
        _bll.Model.State = 1;
        _bll.Model.IsOpponent = (int)ViewState["IsOpponent"];
        _bll.Model.InsertStaff = (int)Session["UserID"];
        _bll.Model.InsertTime = DateTime.Now;
        _bll.Model.DownStaff = staff;
        if ((int)ViewState["IsOpponent"] == 9)
        {
            _bll.Model.ProductGifts = ProdutctGifts.ToString().EndsWith(",") ? ProdutctGifts.ToString().Substring(0,ProdutctGifts.Length - 1) : ProdutctGifts.ToString();
            _bll.Model.Testers = Testers.ToString().EndsWith(",") ? Testers.ToString().Substring(0,Testers.Length - 1) : Testers.ToString();
            _bll.Model.Gifts = Gifts.ToString().EndsWith(",") ? Gifts.ToString().Substring(0,Gifts.Length - 1) : Gifts.ToString();
        }
        _bll.Model["UserName"] = Session["UserName"].ToString();
        _bll.Model["ClientType"] = ViewState["ClientType"].ToString();
        _bll.Add();


        //StringBuilder builder = new StringBuilder();
        //builder.Append("alert('请到该页面下载模版!');");
        //builder.Append("window.open('ImportHistory.aspx?IsOpponent=" + ViewState["IsOpponent"].ToString()+"&ClientType=" + ViewState["ClientType"].ToString()+"','_blank')");

        //MessageBox.ResponseScript(this, builder.ToString());

        Response.Redirect("ImportHistory.aspx?IsOpponent=" + ViewState["IsOpponent"].ToString() + "&ClientType=" + ViewState["ClientType"].ToString());
        
    } 

    protected void select_Staff_SelectChange(object sender, MCSControls.MCSWebControls.SelectChangeEventArgs e)
    {
    
        ProdutctGifts = new StringBuilder();
        Testers = new StringBuilder();
        Gifts = new StringBuilder();
        #region 获取最迟的销量日期
        AC_AccountMonth month = (AC_AccountMonth)ViewState["month"];
        DateTime day = DateTime.Today < month.EndDate ? DateTime.Today : month.EndDate;
        #endregion

        #region 判断有无选择业代
        if (string.IsNullOrEmpty(select_Staff.SelectValue) || select_Staff.SelectValue == "0")
        {
            MessageBox.Show(this, "对不起，请选择责任业代！");
            return;
        }
        #endregion

        #region 获取业代负责的零售商及所有产品数据
        int staff = int.Parse(select_Staff.SelectValue);
        string condtion = ViewState["ClientType"].ToString() == "2" ? " AND MCS_SYS.dbo.UF_Spilt(ExtPropertys,'|',7)='2'" : "";
        IList<CM_Client> clientlists = CM_ClientBLL.GetModelList("ClientType=" + ViewState["ClientType"].ToString()+ " AND ClientManager=" + staff.ToString() +
            " AND ActiveFlag=1 AND ApproveFlag=1 "+condtion+"AND OpenTime<'" + day.ToString("yyyy-MM-dd") + " 23:59:59' ORDER BY Code");
        if (clientlists.Count == 0)
        {

            if (sender != null)
                MessageBox.Show(this, "对不起，没有当前人直接负责的客户！");
            return;
        }
        #endregion
        if ((int)ViewState["IsOpponent"] == 9)
        BindProductGrid();

    }

    
   
    protected void MCSTabControl2_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        BindProductGrid();
    }
    protected void chkHeader_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gv_product.Rows)
        {
            System.Web.UI.WebControls.CheckBox cb = (System.Web.UI.WebControls.CheckBox)row.FindControl("chk_ID");
            if (cb != null)
            {
                cb.Checked = ((System.Web.UI.WebControls.CheckBox)sender).Checked;
                if (cb.Checked)
                {
                    GetProducts(row.RowIndex);
                }
                else
                {
                    RemoveProducts(row.RowIndex);
                }
            }
        }
    }

    private void GetProducts(int rowIndex)
    {
        string productid = gv_product.DataKeys[rowIndex]["PDT_Product_ID"].ToString();
        switch (MCSTabControl2.SelectedTabItem.Value)
        {
            case "1"://3 婴幼儿本品赠品
                ProdutctGifts.Append(productid + ",");
                break;
            case "2":
                Testers.Append(productid + ",");
                break;
            case "3"://4-活动道具;5-印刷品及DM;6-异质赠品
                Gifts.Append(productid + ",");
                break;
        }
    }

    private void RemoveProducts(int rowIndex)
    {
        string productid = gv_product.DataKeys[rowIndex]["PDT_Product_ID"].ToString();
        switch (MCSTabControl2.SelectedTabItem.Value)
        {
            case "1"://3 婴幼儿本品赠品
                ProdutctGifts.Replace(productid + ",", "");
                break;
            case "2":
                Testers.Replace(productid + ",", "");
                break;
            case "3"://4-活动道具;5-印刷品及DM;6-异质赠品
                Gifts.Replace(productid + ",", "");
                break;
        }
    }

    protected void bt_UploadExcel_Click(object sender, EventArgs e)
    {

        #region 保存文件
        if (!FileUpload1.HasFile)
        {
            MessageBox.Show(this.Page, "请选择要上传的文件！");
            return;
        }
        int FileSize = (FileUpload1.PostedFile.ContentLength / 1024);

        if (FileSize > ConfigHelper.GetConfigInt("MaxAttachmentSize"))
        {
            MessageBox.Show(this.Page, "上传的文件不能大于" + ConfigHelper.GetConfigInt("MaxAttachmentSize") +
                "KB!当前上传文件大小为:" + FileSize.ToString() + "KB");
            return;
        }

        //判断文件格式
        string FileName = FileUpload1.PostedFile.FileName;
        FileName = FileName.Substring(FileName.LastIndexOf('\\') + 1);
        FileName = FileName.Substring(0, FileName.LastIndexOf('.'));

        string FileExtName = FileUpload1.PostedFile.FileName.Substring(FileUpload1.PostedFile.FileName.LastIndexOf(".") + 1).ToLowerInvariant();

        if (FileExtName != "xls" &&FileExtName != "csv")
        {
            MessageBox.Show(this, "对不起，必须上传扩展名为xls的Excel文件！");
            return;
        }

        string path = ConfigHelper.GetConfigString("AttachmentPath");
        if (path.StartsWith("~")) path = Server.MapPath(path);
        if (!path.EndsWith("\\")) path = path + "\\";
        path += "ImportExcelSVM\\Upload\\" + Session["UserName"].ToString() + "\\";
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);

        path += FileName + "." + FileExtName;

        FileUpload1.SaveAs(path);
        #endregion

        SVM_UploadTemplateBLL _bll = new SVM_UploadTemplateBLL();
        _bll.Model.Name = FileName + "." + FileExtName; ;
        _bll.Model.AccountMonth = ((AC_AccountMonth)ViewState["month"]).ID;
        _bll.Model.State = 1;
        _bll.Model.IsOpponent = (int)ViewState["IsOpponent"];
        _bll.Model.InsertStaff = (int)Session["UserID"];
        _bll.Model.UploadTime = DateTime.Now;
        _bll.Model["UserName"] = Session["UserName"].ToString();
        _bll.Model["ClientType"] = ViewState["ClientType"].ToString();
        _bll.Add();
        div_gift.Visible = false;

        //StringBuilder builder = new StringBuilder();
        //builder.Append("alert('请到该页面查询导入状态!');");
        //builder.Append("window.open('ImportHistory.aspx?IsOpponent=" + ViewState["IsOpponent"].ToString() + "&State=2&ClientType=" + ViewState["ClientType"].ToString() + "','_blank')");

        //MessageBox.ResponseScript(this, builder.ToString());
        Response.Redirect("ImportHistory.aspx?IsOpponent=" + ViewState["IsOpponent"].ToString() + "&State=2&ClientType=" + ViewState["ClientType"].ToString());
       
       
    }



 
}