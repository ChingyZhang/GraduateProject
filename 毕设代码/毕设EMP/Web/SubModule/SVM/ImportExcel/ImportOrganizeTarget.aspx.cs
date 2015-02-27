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


public partial class SubModule_SVM_ImportExcel_ImportOrganizeTarget : System.Web.UI.Page
{
    protected StringBuilder Produtcts = new StringBuilder();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            ViewState["IsOpponent"] = Request.QueryString["IsOpponent"] != null ? int.Parse(Request.QueryString["IsOpponent"]) : 9;
            ViewState["ClientType"] = Request.QueryString["ClientType"] != null ? int.Parse(Request.QueryString["ClientType"]) : 3;
            ViewState["Type"] = Request.QueryString["Type"] != null ? int.Parse(Request.QueryString["Type"]) : 0;
            #region 获取最迟的销量月份
            int JXCDelayDays = ConfigHelper.GetConfigInt("JXCDelayDays");
            AC_AccountMonth month = new AC_AccountMonthBLL(AC_AccountMonthBLL.GetMonthByDate(DateTime.Now.AddDays(-JXCDelayDays))).Model;           
            ViewState["month"] = month;
            #endregion

            ViewState["Produtcts"] = new StringBuilder(",");
            BindDropDown();
            BindGrid();
        }
        Produtcts = (StringBuilder)ViewState["Produtcts"];

    }
    private void BindDropDown()
    {   

        chk_Citylist.DataSource = Addr_OrganizeCityBLL.GetModelList("Level=" + ConfigHelper.GetConfigString("OrganizePartCity-CityLevel"));
        chk_Citylist.DataBind();

        if ((int)ViewState["Type"] == 2)
        {
            MCSTabControl1.SelectedIndex = 2;
            tb_step1.Visible = MCSTabControl1.SelectedIndex == 0;
        }
        IList<PDT_Brand> _brandList = PDT_BrandBLL.GetModelList("IsOpponent=1");
        ddl_Brand.DataTextField = "Name";
        ddl_Brand.DataValueField = "ID";
        ddl_Brand.DataSource = _brandList;
        ddl_Brand.DataBind();
        ddl_Brand.Items.Insert(0, new ListItem("全部", "0"));
        //CheckOrganizeCity();  
        ddl_AccountMonth.DataSource = AC_AccountMonthBLL.GetModelList("");
        ddl_AccountMonth.DataBind();
        ddl_AccountMonth.SelectedValue = (AC_AccountMonthBLL.GetCurrentMonth() - 1).ToString();

    }
    private void BindGrid()
    {
        string condtion = " PDT_Product.Brand IN (SELECT ID FROM MCS_Pub.dbo.PDT_Brand WHERE IsOpponent='1') AND PDT_Product.State=1 AND PDT_Product.ApproveFlag=1  ";
        if (ddl_Brand.SelectedValue != "0")
        {
            condtion += "AND PDT_Product.Brand=" + ddl_Brand.SelectedValue;
        }
        gv_product.PageIndex = 0;
        gv_product.ConditionString = condtion + "  ORDER BY ISNULL(PDT_Product.SubUnit,999999),PDT_Product.Code"; ;
        gv_product.BindGrid();
    }
    private void CheckOrganizeCity()
    {
        IList<SVM_DownloadTemplate> _downlist = SVM_DownloadTemplateBLL.GetModelList("IsOpponent=2 AND State IN (1,2) AND AccountMonth=" + ddl_AccountMonth.SelectedValue);
        foreach (SVM_DownloadTemplate _m in _downlist)
        {
            ListItem item = chk_Citylist.Items.FindByValue(_m["OrganizeCity"]);
            if (!item.Text.EndsWith("已操作")) item.Text += "------已操作";
        }
    }
    protected void bt_DownloadTemplate_Click(object sender, EventArgs e)
    {
      
        SVM_DownloadTemplateBLL _bll;
        string filename;
        if (Produtcts.Length == 1)
        {
            MessageBox.Show(this, "对不起，请选择品项！");
            return;
        }
     
       // string filename = ddl_OrganizeCity.SelectedItem.Text.Replace("------已操作", "") + "重点品项目标导入模板" + "-" + DateTime.Today.ToString("yyyyMMdd") + ".xls";
        StringBuilder organizecitys = new StringBuilder();
        StringBuilder citynames = new StringBuilder();
        foreach (ListItem item in chk_Citylist.Items)
        {
            if (item.Selected)
            {
                organizecitys.Append(item.Value+",");
                citynames.Append(item.Text.Substring(0, item.Text.IndexOf("营业部"))+",");
            }
        }
        if (organizecitys.ToString().EndsWith(","))
        {
            organizecitys.Remove(organizecitys.Length - 1,1);
            citynames.Remove(citynames.Length - 1, 1);
        }
        filename = citynames.ToString() + "重点品项目标导入模板" + "-" + DateTime.Today.ToString("yyyyMMdd") + ".xls";
        if (filename.Length > 100) filename = ddl_AccountMonth.SelectedItem.Text + "重点品项目标导入模板" + "-" + DateTime.Today.ToString("yyyyMMdd") + ".xls";
        _bll = new SVM_DownloadTemplateBLL();
        _bll.Model.Name = filename;
        _bll.Model.AccountMonth =int.Parse(ddl_AccountMonth.SelectedValue);
        _bll.Model.State = 1;
        _bll.Model.IsOpponent = 2;
        _bll.Model.InsertStaff = (int)Session["UserID"];
        _bll.Model.InsertTime = DateTime.Now;
        _bll.Model["OrganizeCity"] = organizecitys.ToString();
        _bll.Model.ProductGifts = Produtcts.ToString().Substring(1, Produtcts.Length - 2);
        _bll.Model["UserName"] = Session["UserName"].ToString();
        _bll.Add();    
        Response.Redirect("ImportHistory.aspx?IsOpponent=2");
    }
    protected void chkHeader_CheckedChanged(object sender, EventArgs e)
    {
        //if (ddl_OrganizeCity.SelectedValue == "0")
        //{
        //    MessageBox.Show(this, "对不起，请选择营业部！");
        //    return;
        //}
        foreach (GridViewRow row in gv_product.Rows)
        {
            System.Web.UI.WebControls.CheckBox cb = (System.Web.UI.WebControls.CheckBox)row.FindControl("chk_ID");
            if (cb != null)
            {
                cb.Checked = ((System.Web.UI.WebControls.CheckBox)sender).Checked;
                string productid = gv_product.DataKeys[row.RowIndex]["PDT_Product_ID"].ToString();
                if (cb.Checked)
                {
                    Produtcts.Append(productid + ",");
                }
                else
                {
                    Produtcts.Replace(productid + ",", "");
                }
            }
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

        if (FileExtName != "xls" && FileExtName != "csv")
        {
            MessageBox.Show(this, "对不起，必须上传扩展名为xls的Excel文件！");
            return;
        }

        string path = ConfigHelper.GetConfigString("AttachmentPath");
        if (path.StartsWith("~")) path = Server.MapPath(path);
        if (!path.EndsWith("\\")) path = path + "\\";
        path += "ImportExcelSVM\\Upload\\" + ddl_AccountMonth.SelectedValue + "\\";
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);

        path += FileName + "." + FileExtName;

        FileUpload1.SaveAs(path);
        #endregion

        SVM_UploadTemplateBLL _bll = new SVM_UploadTemplateBLL();
        _bll.Model.Name = FileName + "." + FileExtName; ;
        _bll.Model.AccountMonth = int.Parse(ddl_AccountMonth.SelectedValue);
        _bll.Model.State = 1;
        _bll.Model.IsOpponent =MCSTabControl1.SelectedIndex==1? 2:3;
        _bll.Model.InsertStaff = (int)Session["UserID"];
        _bll.Model.UploadTime = DateTime.Now;
        _bll.Model["UserName"] = Session["UserName"].ToString();        
        _bll.Add();
        Response.Redirect("ImportHistory.aspx?IsOpponent=" + (MCSTabControl1.SelectedIndex == 1 ? "2" : "3") + "&State=2");
    }
    protected void chk_ID_CheckedChanged(object sender, EventArgs e)
    {
        
        System.Web.UI.WebControls.CheckBox cb = (System.Web.UI.WebControls.CheckBox)sender;
        GridViewRow drv = (GridViewRow)cb.NamingContainer;

        int rowIndex = drv.RowIndex;
        string productid = gv_product.DataKeys[rowIndex]["PDT_Product_ID"].ToString();
        if (cb.Checked)
        {
            Produtcts.Append(productid + ",");
        }
        else
        {
            Produtcts.Replace(productid + ",", "");
        }
    }

    protected void ddl_OrganizeCity_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ViewState["Produtcts"] = new StringBuilder(",");
        //Produtcts = (StringBuilder)ViewState["Produtcts"];
        //IList<SVM_DownloadTemplate> _downlist = SVM_DownloadTemplateBLL.GetModelList("IsOpponent=2 AND State IN (1,2) AND MCS_SYS.dbo.UF_Spilt(ExtPropertys,'|',3)=" + ddl_OrganizeCity.SelectedValue+" AND AccountMonth=" + (ViewState["month"] as AC_AccountMonth).ID.ToString()+" Order By ID DESC");
        //if (_downlist.Count > 0)
        //{
        //    Produtcts = Produtcts.Append( _downlist[0].ProductGifts);
        //}
        //BindGrid();       
    }
    protected void MCSTabControl1_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        tb_step1.Visible = MCSTabControl1.SelectedIndex == 0;
    }
    protected void ddl_Brand_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();
    }
}
