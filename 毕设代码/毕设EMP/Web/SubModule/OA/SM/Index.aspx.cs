using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.Common;
using MCSFramework.Model.OA;
using MCSFramework.BLL.CM;
using MCSFramework.Model.Pub;
using MCSFramework.BLL.OA;
using MCSFramework.BLL;
using MCSFramework.Model;

public partial class SubModule_OA_SM_index : System.Web.UI.Page
{
    /// <summary>
    /// 短讯的主页面
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            ViewState["PageIndex"] = 0;
            BindGridRecvMsg();
            
        }

    }


    /// <summary>
    /// 设置显示哪个grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void MCSTabControl2_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        tbx_SelectContent.Text = "";
        if (e.Index == 0)
        {
            tr_msglist.Visible = true;
            tr_send.Visible = false;

            MCSTabControl2.SelectedIndex = 0;
            ddl_sampleSelect.Items[0].Enabled = true;
            ddl_sampleSelect.Items[1].Enabled = false;
            BindGridRecvMsg();
        }
        else
        {
            tr_send.Visible = true;
            tr_msglist.Visible = false;
            ddl_sampleSelect.Items[0].Enabled = false;
            ddl_sampleSelect.Items[1].Enabled = true;
            BindGridSendMsg();
            MCSTabControl2.SelectedIndex = 1;
        }

    }

    /// <summary>
    /// 绑定SendMsg
    /// </summary>
    public void BindGridSendMsg()
    {
        string sender = (string)Session["UserName"];
        DataTable dt = SM_MsgBLL.GetSendMsg(sender);

        if (ViewState["Sort"] != null)
        {
            dt.DefaultView.Sort = ViewState["Sort"].ToString() + " " + ViewState["SortDirect"];
        }
        if (ViewState["PageIndex"] != null)
        {
            ud_Grid_Send.PageIndex = Int32.Parse(ViewState["PageIndex"].ToString());
        }

        if (tbx_SelectContent.Text != "")
        {
            dt.DefaultView.RowFilter = "" + ddl_sampleSelect.SelectedValue + " like  '" + tbx_SelectContent.Text.Trim() + "%'";
        }

        ud_Grid_Send.DataSource = dt.DefaultView;
        ud_Grid_Send.DataBind();
    }

    /// <summary>
    /// 绑定接受信息ReceiverMsg
    /// </summary>
    public void BindGridRecvMsg()
    {
        string receiver = (string)Session["UserName"];
        DataTable dt = SM_ReceiverBLL.GetMyMsg(receiver);

        if (ViewState["Sort"] != null)
        {
            dt.DefaultView.Sort = ViewState["Sort"].ToString() + " " + ViewState["SortDirect"];
        }
        if (ViewState["PageIndex"] != null)
        {
            ud_Grid_Recv.PageIndex = Int32.Parse(ViewState["PageIndex"].ToString());
        }
        if (tbx_SelectContent.Text != "")
        {
            dt.DefaultView.RowFilter = "" + ddl_sampleSelect.SelectedValue + " like  '" + tbx_SelectContent.Text.Trim() + "%'";
        }

        ud_Grid_Recv.DataSource = dt.DefaultView;
        ud_Grid_Recv.DataBind();
    }

    /// <summary>
    /// ud_grid_msg删除事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ud_Grid_Recv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int id = int.Parse(ud_Grid_Recv.DataKeys[e.RowIndex]["ID"].ToString());
        new SM_ReceiverBLL(id).Delete();
        BindGridRecvMsg();
    }

    /// <summary>
    /// ud_grid添加删除事件
    /// </summary>
    protected void ud_Grid_Send_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int id = int.Parse(ud_Grid_Send.DataKeys[e.RowIndex]["MsgID"].ToString());
        new SM_MsgBLL(id).Delete();
        BindGridSendMsg();
    }

    protected void btnMsgDelete_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gr in ud_Grid_Recv.Rows)
        {
            if (((CheckBox)gr.FindControl("chk_Msg_ID")).Checked == true)
            {
                int id = int.Parse(ud_Grid_Recv.DataKeys[gr.RowIndex]["ID"].ToString());
                new SM_ReceiverBLL(id).Delete();
            }
        }
        BindGridRecvMsg();
    }
    protected void bt_MsgDeleteAll_Click(object sender, EventArgs e)
    {
        SM_ReceiverBLL.DeleteAll((string)Session["UserName"]);
        BindGridRecvMsg();
    }

    protected void btn_SendDelete_Click(object sender, EventArgs e)
    {
        string senders = (string)Session["UserName"];
        foreach (GridViewRow gr in ud_Grid_Send.Rows)
        {
            if (((CheckBox)gr.FindControl("chk_ID")).Checked == true)
            {
                int id = int.Parse(ud_Grid_Send.DataKeys[gr.RowIndex]["MsgID"].ToString());
                new SM_MsgBLL(id).Delete();
            }
        }
        BindGridSendMsg();
    }
    protected void bt_SendDeleteAll_Click(object sender, EventArgs e)
    {
        SM_MsgBLL.DeleteAll((string)Session["UserName"]);
        BindGridSendMsg();
    }

    #region ud_Grid_Recv控件的分页、排序、选中等事件
    protected void ud_Grid_Recv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ViewState["PageIndex"] = e.NewPageIndex;
        BindGridRecvMsg();
    }
    protected void ud_Grid_Recv_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        int id = int.Parse(ud_Grid_Recv.DataKeys[e.NewSelectedIndex]["MsgID"].ToString());
        SM_ReceiverBLL.IsRead(id, (string)Session["UserName"]);
        BindGridRecvMsg();
    }
    protected void ud_Grid_Recv_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (ViewState["SortDirect"] != null)
        {
            if (ViewState["SortDirect"].ToString() == "DESC")
                ViewState["SortDirect"] = "ASC";
            else
                ViewState["SortDirect"] = "DESC";
        }
        else
            ViewState["SortDirect"] = "ASC";

        ViewState["SortField"] = (string)e.SortExpression;

        ViewState["Sort"] = e.SortExpression;
        BindGridRecvMsg();
    }

    #endregion

    #region ud_grid控件的分页、排序、选中等事件
    protected void ud_Grid_Send_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ViewState["PageIndex"] = e.NewPageIndex;
        BindGridSendMsg();
    }
    protected void ud_Grid_Send_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (ViewState["SortDirect"] != null)
        {
            if (ViewState["SortDirect"].ToString() == "DESC")
                ViewState["SortDirect"] = "ASC";
            else
                ViewState["SortDirect"] = "DESC";
        }
        else
            ViewState["SortDirect"] = "ASC";

        ViewState["SortField"] = (string)e.SortExpression;

        ViewState["Sort"] = e.SortExpression;
        BindGridSendMsg();
    }
    #endregion


    protected void bt_sampleSelect_Click(object sender, EventArgs e)
    {
        if (MCSTabControl2.SelectedIndex == 0)
        {
            BindGridRecvMsg();
        }
        else
        {
            BindGridSendMsg();
        }

    }
    protected void bt_AllRead_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gr in ud_Grid_Recv.Rows)
        {
            if (((CheckBox)gr.FindControl("chk_Msg_ID")).Checked == true)
            {
                int id = int.Parse(ud_Grid_Recv.DataKeys[gr.RowIndex]["MsgID"].ToString());
                SM_ReceiverBLL.IsRead(id, (string)Session["UserName"]);
            }
        }
        BindGridRecvMsg();
    }
}
