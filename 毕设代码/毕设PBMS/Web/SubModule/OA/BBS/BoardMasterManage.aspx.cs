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

public partial class SubModule_OA_BBS_BoardMasterManage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        // 在此处放置用户代码以初始化页面
        if (!Page.IsPostBack)
        {
            ViewState["BoardID"] = (Request.QueryString["BoardID"] == null) ? 0 : Convert.ToInt32(Request.QueryString["BoardID"]);
            if (ViewState["BoardID"] != null)
            {
                BBS_BoardBLL boardbll = new BBS_BoardBLL((int)ViewState["BoardID"]);
                if (boardbll.Model.IsPublic == "1")
                {
                    isPublic.Visible = true;
                    BindPublic();
                }
                else
                {
                    isPublic.Visible = false;
                    BindView();
                }
            }
        }
    }

    private void BindPublic()
    {
        lbRemainStaff.Items.Clear();
        tr_Position.DataSource = Org_PositionBLL.GetAllPostion();
        tr_Position.DataBind();
        tr_Position.SelectValue = "1";
        BBS_BoardUserMemberBLL userMemberbll = new BBS_BoardUserMemberBLL((int)ViewState["BoardID"]);      
        if (userMemberbll != null && userMemberbll._GetModelList(" Board=" + (int)ViewState["BoardID"]).Count > 0)
        {
            lbBoardMasterList.Items.Clear();
            foreach (BBS_BoardUserMember user in userMemberbll._GetModelList(" Board=" + (int)ViewState["BoardID"] + " and Role = 1"))
            {
                lbBoardMasterList.Items.Add(new ListItem(user.UserName + "[" + UserBLL.GetStaffByUsername(user.UserName).RealName + "]", user.UserName));
            }
        }
    }
    private void BindView()
    {
        BBS_BoardUserMemberBLL userMemberbll = new BBS_BoardUserMemberBLL((int)ViewState["BoardID"]);
        if (userMemberbll != null && userMemberbll._GetModelList(" Board=" + (int)ViewState["BoardID"]).Count > 0)
        {
            lbRemainStaff.Items.Clear();
            lbBoardMasterList.Items.Clear();
            foreach (BBS_BoardUserMember user in userMemberbll._GetModelList(" Board=" + (int)ViewState["BoardID"] + " and Role <> 1"))
            {
                lbRemainStaff.Items.Add(new ListItem(user.UserName + "[" + UserBLL.GetStaffByUsername(user.UserName).RealName + "]", user.UserName));
            }
            foreach (BBS_BoardUserMember user in userMemberbll._GetModelList(" Board=" + (int)ViewState["BoardID"] + " and Role = 1"))
            {
                lbBoardMasterList.Items.Add(new ListItem(user.UserName + "[" + UserBLL.GetStaffByUsername(user.UserName).RealName + "]", user.UserName));
            }
        }
    }

    protected void btn_in_Click(object sender, System.EventArgs e)
    {
        BBS_BoardUserMemberBLL userMemberbll = new BBS_BoardUserMemberBLL();
        BBS_BoardBLL boardbll = new BBS_BoardBLL((int)ViewState["BoardID"]);
        //便历listbox得到选中的带成为板主的人员id
        if (boardbll.Model.IsPublic == "2")
        {
            for (int i = 0; i < lbRemainStaff.Items.Count; i++)
            {
                if (lbRemainStaff.Items[i].Selected)
                {
                    userMemberbll.UpdateRoleByUserNameAndBoard(lbRemainStaff.Items[i].Value, (int)ViewState["BoardID"], 1);
                }
            }
            BindView();
        }
        else
        {
            for (int i = 0; i < lbRemainStaff.Items.Count; i++)
            {
                if (lbRemainStaff.Items[i].Selected)
                {
                    userMemberbll.Model.Board = (int)ViewState["BoardID"];
                    userMemberbll.Model.UserName = lbRemainStaff.Items[i].Value;
                    userMemberbll.Model.Role = 1;
                    userMemberbll.Add();
                }
            }
            BindPublic();
        }
    }

    protected void btn_out_Click(object sender, System.EventArgs e)
    {
        BBS_BoardUserMemberBLL userMemberbll = new BBS_BoardUserMemberBLL();
         BBS_BoardBLL boardbll = new BBS_BoardBLL((int)ViewState["BoardID"]);
        //便历listbox得到选中的的人员id
         if (boardbll.Model.IsPublic == "2")
         {
             for (int i = 0; i < lbBoardMasterList.Items.Count; i++)
             {
                 if (lbBoardMasterList.Items[i].Selected)
                 {
                     userMemberbll.UpdateRoleByUserNameAndBoard(lbBoardMasterList.Items[i].Value, (int)ViewState["BoardID"], 4);
                 }
             }
             BindView();
         }
         else
         {
             for (int i = 0; i < lbBoardMasterList.Items.Count; i++)
             {
                 if (lbBoardMasterList.Items[i].Selected)
                 {
                     userMemberbll.DeleteByUserNameAndBoard(lbBoardMasterList.Items[i].Value, (int)ViewState["BoardID"]);
                 }
             }
             BindPublic();
         }
    }
    protected void bt_Back_Click(object sender, EventArgs e)
    {
        Response.Redirect("index.aspx");
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
            lbRemainStaff.Items.Clear();

            foreach (DataRow row in dt.Rows)
            {
                
                lbRemainStaff.Items.Add(new ListItem((string)row["UserName"] + "[" + (string)row["RealName"] + "]", (string)row["UserName"]));
            }
        }
    }
}

