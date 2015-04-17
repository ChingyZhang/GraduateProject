// ===================================================================
// 文件路径:CM/LinkMan/LinkManDetail.aspx.cs 
// 生成日期:2008-12-19 10:05:59 
// 作者:	  yangwei
// ===================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using MCSFramework.Common;
using MCSFramework.Model.CM;
using MCSFramework.BLL.CM;
using MCSControls.MCSWebControls;

public partial class CM_LinkMan_LinkManDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        // 在此处放置用户代码以初始化页面
        if (!Page.IsPostBack)
        {
            ViewState["ID"] = Request.QueryString["ID"] == null ? 0 : int.Parse(Request.QueryString["ID"]);
            if ((int)ViewState["ID"] > 0)
            {
                BindData();
            }
            if (Request.QueryString["ClientID"] != null)
            {
                int clientid = int.Parse(Request.QueryString["ClientID"]);

                MCSSelectControl control = (MCSSelectControl)UC_DetailView1.FindControl("CM_LinkMan_ClientID");
                if (control != null)
                {
                    CM_ClientBLL client = new CM_ClientBLL(clientid);
                    control.SelectValue = clientid.ToString();
                    control.SelectText = client.Model.FullName;
                    control.Enabled = false;
                }
            }
        }
    }

    private void BindData()
    {
        CM_LinkMan _lm = new CM_LinkManBLL((int)ViewState["ID"]).Model;
        UC_DetailView1.BindData(_lm);
        bt_OK.Text = "修 改";
        bt_OK.ForeColor = System.Drawing.Color.Red;
    }

    protected void bt_OK_Click(object sender, EventArgs e)
    {
        CM_LinkManBLL _lm = null;
        if (bt_OK.Text == "确 定")
        {
            _lm = new CM_LinkManBLL();
        }
        else
        {
            _lm = new CM_LinkManBLL((int)ViewState["ID"]);
        }

        UC_DetailView1.GetData(_lm.Model);

        if (bt_OK.Text == "确 定")
        {
            ViewState["ID"] = _lm.Add();
            string path = "LinkManDetail.aspx?ID=" + ViewState["ID"].ToString();
            if (Request.QueryString["URL"]!=null) path = Page.ResolveUrl(Request.QueryString["URL"] + "?ClientID=" + Request.QueryString["ClientID"]);
            MessageBox.ShowAndRedirect(this, "保存联系人资料成功！", path);
        }
        else
        {
            _lm.Update();
            MessageBox.ShowAndRedirect(this, "保存联系人资料成功！", "LinkManDetail.aspx?ID=" + ViewState["ID"].ToString());
        }



    }

}