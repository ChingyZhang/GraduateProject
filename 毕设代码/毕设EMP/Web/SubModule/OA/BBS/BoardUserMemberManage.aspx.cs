using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL.OA;
using MCSFramework.Model.OA;
using MCSFramework.BLL;
using System.Data;

public partial class SubModule_OA_BBS_BoardUserMemberManage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            // 在此处放置用户代码以初始化页面
            ViewState["BoardID"] = (Request.QueryString["BoardID"] == null) ? 0 : Convert.ToInt32(Request.QueryString["BoardID"]);
            if (ViewState["BoardID"] != null)
            {
                if (new BBS_BoardBLL((int)ViewState["BoardID"]).Model.IsPublic == "1")
                    Response.Redirect("index.aspx");
                else
                {
                    BindView();
                    BindPosition();
                }
            }
        }
    }
    private void BindPosition()
    {
        listAccount.Items.Clear();
        tr_Position.DataSource = Org_PositionBLL.GetAllPostion();
        tr_Position.DataBind();
        tr_Position.SelectValue = "1";
    }
    private void BindView()
    {
        BBS_BoardUserMemberBLL userMemberbll = new BBS_BoardUserMemberBLL((int)ViewState["BoardID"]);
        if (userMemberbll != null && userMemberbll._GetModelList(" Board=" + (int)ViewState["BoardID"]).Count > 0)
        {
            lbReadStaff.Items.Clear();
            lbDiscussStaff.Items.Clear();
            lbPublicStaff.Items.Clear();
            foreach (BBS_BoardUserMember user in userMemberbll._GetModelList(" Board=" + (int)ViewState["BoardID"] + " and Role = 4"))
            {

                lbReadStaff.Items.Add(new ListItem(user.UserName+"["+UserBLL.GetStaffByUsername(user.UserName).RealName+"]", user.UserName));
            }
            foreach (BBS_BoardUserMember user in userMemberbll._GetModelList(" Board=" + (int)ViewState["BoardID"] + " and Role = 3"))
            {
                lbDiscussStaff.Items.Add(new ListItem(user.UserName+"["+UserBLL.GetStaffByUsername(user.UserName).RealName+"]", user.UserName));
            }
            foreach (BBS_BoardUserMember user in userMemberbll._GetModelList(" Board=" + (int)ViewState["BoardID"] + " and Role = 2"))
            {
                lbPublicStaff.Items.Add(new ListItem(user.UserName+"["+UserBLL.GetStaffByUsername(user.UserName).RealName+"]", user.UserName));
            }
        }
    }

    protected void btn_in_Click(object sender, System.EventArgs e)
    {

        BBS_BoardUserMemberBLL userMemberbll = new BBS_BoardUserMemberBLL();
        //便历listbox得到选中的讨论者id
        for (int i = 0; i < lbReadStaff.Items.Count; i++)
        {
            if (lbReadStaff.Items[i].Selected)
            {

                userMemberbll.UpdateRoleByUserNameAndBoard(lbReadStaff.Items[i].Value, (int)ViewState["BoardID"], 3);
            }
        }
        BindView();
    }

    protected void btn_out_Click(object sender, System.EventArgs e)
    {
        BBS_BoardUserMemberBLL userMemberbll = new BBS_BoardUserMemberBLL();
        //便历listbox得到选中的讨论者id
        for (int i = 0; i < lbDiscussStaff.Items.Count; i++)
        {
            if (lbDiscussStaff.Items[i].Selected)
            {
                userMemberbll.UpdateRoleByUserNameAndBoard(lbDiscussStaff.Items[i].Value, (int)ViewState["BoardID"], 4);
            }
        }
        BindView();
    }

    protected void bt_Back_Click(object sender, EventArgs e)
    {
        Response.Redirect("index.aspx");
    }
    protected void btn_PublicIn_Click(object sender, EventArgs e)
    {
        BBS_BoardUserMemberBLL userMemberbll = new BBS_BoardUserMemberBLL();
        //便历listbox得到选中的讨论者id
        for (int i = 0; i < lbDiscussStaff.Items.Count; i++)
        {
            if (lbDiscussStaff.Items[i].Selected)
            {
                userMemberbll.UpdateRoleByUserNameAndBoard(lbDiscussStaff.Items[i].Value, (int)ViewState["BoardID"], 2);
            }
        }
        BindView();
    }
    protected void btn_PublicOut_Click(object sender, EventArgs e)
    {
        BBS_BoardUserMemberBLL userMemberbll = new BBS_BoardUserMemberBLL();
        //便历listbox得到选中的发布者id
        for (int i = 0; i < lbPublicStaff.Items.Count; i++)
        {
            if (lbPublicStaff.Items[i].Selected)
            {
                userMemberbll.UpdateRoleByUserNameAndBoard(lbPublicStaff.Items[i].Value, (int)ViewState["BoardID"], 3);
            }
        }
        BindView();
    }
    protected void bt_Search_Click(object sender, EventArgs e)
    {
        Response.Redirect("Search/index.aspx");
    }
    protected void tr_Position_Selected(object sender, MCSControls.MCSWebControls.SelectedEventArgs e)
    {
        if (tr_Position.SelectValue != "0")
        {
            string positions = "";
            if (cb_IncludeChild.Checked)
            {
                Org_PositionBLL pos = new Org_PositionBLL(int.Parse(tr_Position.SelectValue));
                positions = pos.GetAllChildPosition();
                if (positions != "") positions += ",";
                positions += tr_Position.SelectValue;
            }
            else
                positions = tr_Position.SelectValue;

            DataTable dt = Org_StaffBLL.GetRealNameAndUserNameByPosition(positions);

            listAccount.Items.Clear();

            foreach (DataRow row in dt.Rows)
            {
                listAccount.Items.Add(new ListItem((string)row["UserName"] + "[" + (string)row["RealName"] + "]", (string)row["UserName"]));
            }
        }
    }
    protected void btn_ReadIn_Click(object sender, EventArgs e)
    {
        BBS_BoardUserMemberBLL userMemberbll = new BBS_BoardUserMemberBLL();
        //便历listbox得到选中的讨论者id
        for (int i = 0; i < listAccount.Items.Count; i++)
        {
            if (listAccount.Items[i].Selected)
            {
                userMemberbll.Model.UserName = listAccount.Items[i].Value;
                userMemberbll.Model.Board=(int)ViewState["BoardID"];
                userMemberbll.Model.Role = 4;
                userMemberbll.Add();
            }
        }
        BindView();
    }
}
