using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.OA;
using MCSFramework.Common;
using MCSFramework.Model.OA;
using MCSFramework.BLL.CM;

public partial class SubModule_OA_PN_index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ViewState["Catalog"] = Request.QueryString["Catalog"] == null ? 1 : int.Parse(Request.QueryString["Catalog"]);

            lb_Title.Text = DictionaryBLL.GetDicCollections("OA_NoticeCatalog")[ViewState["Catalog"].ToString()].Name;


            tbx_begin.Text = DateTime.Now.AddMonths(-12).ToString("yyyy-MM-dd");
            tbx_end.Text = DateTime.Now.ToString("yyyy-MM-dd");

            if (Request.QueryString["Flag"] != null)
            {
                MCSTabControl1.SelectedIndex = 1;
            }
            BindGrid();

        }
    }
    private void BindGrid()
    {
        int approveflag = 1;
        DateTime dtBegin = DateTime.Parse(this.tbx_begin.Text);
        DateTime dtEnd = DateTime.Parse(this.tbx_end.Text).AddDays(1);

        #region 列表列隐藏
        if (MCSTabControl1.SelectedIndex == 0)
        {
            //已审核公告
            approveflag = 1;

            //是否有权限撤销公告
            ud_Notice.Columns[ud_Notice.Columns.Count - 2].Visible = Right_Assign_BLL.GetAccessRight(Session["UserName"].ToString(), 2104, "UnApproveNotice");

            //是否有权限删除公告
            ud_Notice.Columns[ud_Notice.Columns.Count - 1].Visible = Right_Assign_BLL.GetAccessRight(Session["UserName"].ToString(), 2104, "DelNotice");

        }
        else if (MCSTabControl1.SelectedIndex == 1)
        {
            //未审核公告
            approveflag = 2;
            ud_Notice.Columns[ud_Notice.Columns.Count - 2].Visible = false;
            ud_Notice.Columns[ud_Notice.Columns.Count - 1].Visible = true;
        }
        else
        {
            //我的公告
            ud_Notice.Columns[ud_Notice.Columns.Count - 2].Visible = true;
            ud_Notice.Columns[ud_Notice.Columns.Count - 1].Visible = true;
        }
        #endregion

        IList<PN_Notice> notices = null;
        if (MCSTabControl1.SelectedIndex < 2)
        {
            if (Right_Assign_BLL.GetAccessRight(Session["UserName"].ToString(), 2104, "ViewALLNotice"))
            {
                //查看所有公告
                notices = PN_NoticeBLL.GetModelList("PN_Notice.IsDelete ='N'  AND PN_Notice.InsertTime BETWEEN '" + tbx_begin.Text + "' AND '"
                     + tbx_end.Text + " 23:59' AND PN_Notice.ApproveFlag=" + approveflag.ToString()
                     + " ORDER BY MCS_SYS.dbo.UF_Spilt(PN_Notice.ExtPropertys,'|',1) DESC, PN_Notice.InsertTime DESC");
            }
            else
            {
                notices = PN_NoticeBLL.GetNoticeByStaff((int)Session["UserID"], dtBegin, dtEnd, approveflag);
            }
        }
        else
        {
            //我发布的公告
            notices = PN_NoticeBLL.GetModelList("PN_Notice.IsDelete ='N'  AND PN_Notice.InsertTime BETWEEN '" + tbx_begin.Text + "' AND '"
                + tbx_end.Text + " 23:59' AND PN_Notice.InsertStaff=" + (int)Session["UserID"] + " Order BY  PN_Notice.InsertTime desc");
        }

        if (tbx_Search.Text.Trim() != "")
        {
            notices = notices.Where(p => p.KeyWord.Contains(tbx_Search.Text.Trim()) || p.Topic.Contains(tbx_Search.Text.Trim())).ToList();
        }
        if ((int)ViewState["Catalog"] > 1)
        {
            notices = notices.Where(p => p["Catalog"] == ViewState["Catalog"].ToString()).ToList();
        }
        else
        {
            notices = notices.Where(p => p["Catalog"] == "" || p["Catalog"] == "1").ToList();
        }
        ud_Notice.BindGrid(notices);
        return;
    }

    protected void btnwritenewmail_Click(object sender, EventArgs e)
    {
        Response.Redirect("NoticeDetail.aspx?Catalog=" + ViewState["Catalog"].ToString());
    }

    protected void btn_Search_Click(object sender, EventArgs e)
    {
        ud_Notice.PageIndex = 0;
        BindGrid();
    }
    protected void MCSTabControl1_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        BindGrid();
    }

    protected void ud_Notice_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ud_Notice.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    protected void ud_Notice_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int id = int.Parse(ud_Notice.DataKeys[e.RowIndex]["ID"].ToString());
        PN_NoticeBLL _bll = new PN_NoticeBLL(id);
        if (_bll.Model.InsertStaff != (int)Session["UserID"] && _bll.Model.ApproveFlag == 1 && !Right_Assign_BLL.GetAccessRight(Session["UserName"].ToString(), 2104, "DelNotice"))
        {
            MessageBox.Show(Page, "对不起，你没有删除已审核公告的权限");
            return;
        }
        _bll.SetIsDeleted();

        BindGrid();
    }
    protected void ud_Notice_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        int id = int.Parse(ud_Notice.DataKeys[e.NewSelectedIndex]["ID"].ToString());
        PN_NoticeBLL _bll = new PN_NoticeBLL(id);

        if (_bll.Model.ApproveFlag == 1)
        {
            if (_bll.Model.InsertStaff != (int)Session["UserID"] && !Right_Assign_BLL.GetAccessRight(Session["UserName"].ToString(), 2104, "UnApproveNotice"))
            {
                MessageBox.Show(Page, "对不起，你没有撤销已审核的公告的权限");
                return;
            }
            _bll.Approve(2, (int)Session["UserID"]);
        }

        BindGrid();
    }
}
