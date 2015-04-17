using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.Model;
using MCSFramework.Common;
using MCSFramework.BLL.OA;
using MCSFramework.Model.OA;

public partial class SubModule_OA_PN_NoticeDetail : System.Web.UI.Page
{
    private RadioButtonList rab_ToAllStaff, rab_ToAllOrganizeCity;
    protected void Page_Load(object sender, EventArgs e)
    {
        #region 界面自定底控件事件
        rab_ToAllStaff = (RadioButtonList)UC_Notice.FindControl("PN_Notice_ToAllStaff");
        if (rab_ToAllStaff != null)
        {
            rab_ToAllStaff.AutoPostBack = true;
            rab_ToAllStaff.SelectedIndexChanged += new EventHandler(rab_ToAllStaff_SelectedIndexChanged);
        }

        rab_ToAllOrganizeCity = (RadioButtonList)UC_Notice.FindControl("PN_Notice_ToAllOrganizeCity");
        if (rab_ToAllOrganizeCity != null)
        {
            rab_ToAllOrganizeCity.AutoPostBack = true;
            rab_ToAllOrganizeCity.SelectedIndexChanged += new EventHandler(rab_ToAllOrganizeCity_SelectedIndexChanged);
        }
        #endregion

        if (!Page.IsPostBack)
        {
            ViewState["ID"] = Request.QueryString["ID"] == null ? 0 : int.Parse(Request.QueryString["ID"]);
            ViewState["Catalog"] = Request.QueryString["Catalog"] == null ? 1 : int.Parse(Request.QueryString["Catalog"]);

            BindDropDown();

            if ((int)ViewState["ID"] > 0)
            {
                #region 页面设置
                bt_OK.Text = "修改保存";
                bt_OK.ForeColor = System.Drawing.Color.Red;
                #endregion

                BindData();
            }
            else
            {
                #region 页面设置
                bt_PreView.Visible = false;
                UploadFile1.Visible = false;
                tab_PNToPosition.Visible = false;
                tab_PNToOrganizeCity.Visible = false;
                bt_Approve.Visible = false;
                ckedit_content.Text = "";

                DropDownList ddl_IsSpecial = (DropDownList)UC_Notice.FindControl("PN_Notice_IsSpecial");
                if (ddl_IsSpecial != null) ddl_IsSpecial.SelectedValue = "2";

                DropDownList ddl_IsTop = (DropDownList)UC_Notice.FindControl("PN_Notice_IsTop");
                if (ddl_IsTop != null) ddl_IsTop.SelectedValue = "N";

                DropDownList ddl_Catalog = (DropDownList)UC_Notice.FindControl("PN_Notice_Catalog");
                if (ddl_Catalog != null && (int)ViewState["Catalog"]>0)
                {
                    ddl_Catalog.SelectedValue = ViewState["Catalog"].ToString();
                    ddl_Catalog.Enabled = false;
                }
                #endregion
            }

            #region 判断是否非总部职位，非总部职位不能面向全体发公告
            Org_StaffBLL staff = new Org_StaffBLL((int)Session["UserID"]);
            Org_Position p = new Org_PositionBLL(staff.Model.Position).Model;
            if (p != null && p.IsHeadOffice != "Y")
            {
                rab_ToAllStaff.SelectedValue = "N";
                rab_ToAllStaff.Enabled = false;
                rab_ToAllStaff_SelectedIndexChanged(null, null);

                rab_ToAllOrganizeCity.SelectedValue = "N";
                rab_ToAllOrganizeCity.Enabled = false;
                rab_ToAllOrganizeCity_SelectedIndexChanged(null, null);
            }
            #endregion
        }

    }

    #region 修改时数据绑定
    private void BindData()
    {
        PN_NoticeBLL bll = new PN_NoticeBLL((int)ViewState["ID"]);
        UC_Notice.BindData(bll.Model);
        if (bll.Model["Catalog"] != "") ViewState["Catalog"] = int.Parse(bll.Model["Catalog"]);

        if (bll.Model.ApproveFlag == 1)
        {
            bt_Approve.Visible = false;
            bt_OK.Visible = false;

            bt_Clear1.Visible = false;
            bt_Detele1.Visible = false;
            bt_Insert1.Visible = false;
            bt_Clear2.Visible = false;
            bt_Detele2.Visible = false;
            bt_Insert2.Visible = false;

            bt_PreView.Visible = false;
            UploadFile1.CanDelete = false;
            UploadFile1.CanUpload = false;
        }

        tab_PNToPosition.Visible = bll.Model.ToAllStaff == "N";
        tab_PNToOrganizeCity.Visible = bll.Model.ToAllOrganizeCity == "N";

        UploadFile1.RelateID = (int)ViewState["ID"];
        UploadFile1.BindGrid();

        #region 显示所属职位，片区
        if (tab_PNToPosition.Visible)
        {
            List<int> List = new List<int>();
            List = PN_ToPositionBLL.GetPositionByNoticeID((int)ViewState["ID"]);
            lb_PositionChild.Items.Clear();
            if (List != null)
            {
                foreach (int id in List)
                {
                    Org_PositionBLL positionbll = new Org_PositionBLL(id);
                    lb_PositionChild.Items.Add(new ListItem(positionbll.Model.Name, id.ToString()));
                }
            }
        }

        if (tab_PNToOrganizeCity.Visible)
        {
            List<int> List = new List<int>();
            List = PN_ToOrganizeCityBLL.GetOrganizeCityByNoticeID((int)ViewState["ID"]);
            lb_CityChild.Items.Clear();
            if (List != null)
            {
                foreach (int id in List)
                {
                    Addr_OrganizeCityBLL organizeCitybll = new Addr_OrganizeCityBLL(id);
                    lb_CityChild.Items.Add(new ListItem(organizeCitybll.Model.Name, id.ToString()));
                }
            }
        }
        #endregion

        ckedit_content.Text = bll.Model.Content;
    }
    #endregion

    #region 树形框绑定
    private void BindDropDown()
    {
        Org_StaffBLL staff = new Org_StaffBLL((int)Session["UserID"]);

        #region 绑定职位
        tr_ToPosition.DataSource = Org_PositionBLL.GetAllPostion();

        #region 如果非总部职位，其只能选择自己职位及以下职位
        Org_Position p = new Org_PositionBLL(staff.Model.Position).Model;
        if (p != null && p.IsHeadOffice != "Y")
        {
            tr_ToPosition.RootValue = p.SuperID.ToString();
            tr_ToPosition.SelectValue = staff.Model.Position.ToString();
        }
        else
        {
            tr_ToPosition.RootValue = "0";
            tr_ToPosition.SelectValue = "1";
        }

        #endregion

        tr_ToOrganizeCity.DataBind();
        #endregion

        #region 绑定管理片区
        tr_ToOrganizeCity.DataSource = staff.GetStaffOrganizeCity();

        if (tr_ToOrganizeCity.DataSource.Select("ID = 1").Length > 0)
        {
            tr_ToOrganizeCity.RootValue = "0";
            tr_ToOrganizeCity.SelectValue = "1";
        }
        else
        {
            tr_ToOrganizeCity.RootValue = new Addr_OrganizeCityBLL(staff.Model.OrganizeCity).Model.SuperID.ToString();
            tr_ToOrganizeCity.SelectValue = staff.Model.OrganizeCity.ToString();
        }
        #endregion

    }
    #endregion

    #region 活动事件
    protected void rab_ToAllStaff_SelectedIndexChanged(object sender, EventArgs e)
    {
        tab_PNToPosition.Visible = rab_ToAllStaff.SelectedValue == "N";

        if (!tab_PNToPosition.Visible) lb_PositionChild.Items.Clear();
    }

    protected void rab_ToAllOrganizeCity_SelectedIndexChanged(object sender, EventArgs e)
    {
        tab_PNToOrganizeCity.Visible = rab_ToAllOrganizeCity.SelectedValue == "N";

        if (!tab_PNToOrganizeCity.Visible) lb_CityChild.Items.Clear();
    }
    #endregion

    #region 添加公告
    protected void bt_OK_Click(object sender, EventArgs e)
    {
        PN_NoticeBLL _noticebll = null;
        if ((int)ViewState["ID"] == 0)
            _noticebll = new PN_NoticeBLL();
        else
            _noticebll = new PN_NoticeBLL((int)ViewState["ID"]);

        DropDownList ddl_Catalog = (DropDownList)UC_Notice.FindControl("PN_Notice_Catalog");
        if (ddl_Catalog != null && ddl_Catalog.SelectedValue == "0")
        {
            MessageBox.Show(this, "请选择公告目录!");
            return;
        }

        UC_Notice.GetData(_noticebll.Model);

        #region 判断置顶公告的截止日期
        if (_noticebll.Model["IsTop"] == "Y")
        {
            if (_noticebll.Model["TopEndDate"] == "" || _noticebll.Model["TopEndDate"] == "1900-01-01")
            {
                MessageBox.Show(this, "请设定公告的置顶截止日期!");
                return;
            }

            if (DateTime.Parse(_noticebll.Model["TopEndDate"]) < DateTime.Today)
            {
                MessageBox.Show(this, "设定公告的置顶截止日期不能小于今天!");
                return;
            }
        }
        else
        {
            _noticebll.Model["TopEndDate"] = "1900-01-01";
        }
        #endregion

        #region 判断非面向全体公告是否选择了面向职位及面向区域
        if (_noticebll.Model.ToAllStaff == "N" && lb_PositionChild.Items.Count == 0)
        {
            MessageBox.Show(this, "请设定公告要面向发布的职位！");
            return;
        }

        if (_noticebll.Model.ToAllOrganizeCity == "N" && lb_CityChild.Items.Count == 0)
        {
            MessageBox.Show(this, "请设定公告要面向发布的管理片区！");
            return;
        }
        #endregion

        if ((int)ViewState["ID"] == 0)
        {
            _noticebll.Model.InsertStaff = (int)Session["UserID"];
            _noticebll.Model.IsDelete = "N";
            _noticebll.Model.ApproveFlag = 2;
            _noticebll.Model.Content = ckedit_content.Text;
            ViewState["ID"] = _noticebll.Add();

            if (tab_PNToPosition.Visible == true) AddFaceTo();
        }
        else
        {

            _noticebll.Model.UpdateStaff = (int)Session["UserID"];
            _noticebll.Model.UpdateTime = Convert.ToDateTime(DateTime.Now.ToString());
            _noticebll.Model.Content = ckedit_content.Text;
            _noticebll.Update();
            UpdateFaceTo();
        }
        //添加附件
        UploadFile1.RelateID = (int)ViewState["ID"];

        if (sender != null)
            MessageBox.ShowAndRedirect(this, "保存成功", "NoticeDetail.aspx?ID=" + ViewState["ID"].ToString());
    }
    #endregion

    #region 添加公告时所属范围添加
    private void AddFaceTo()
    {
        int id = (int)ViewState["ID"];
        List<string> PositionList, OrganizeCityList;

        GetListItem(out PositionList, out OrganizeCityList);

        if (PositionList != null)
        {
            PN_ToPositionBLL p_toPositionbll = new PN_ToPositionBLL();
            foreach (string child in PositionList)
            {
                if (PN_ToPositionBLL.GetModelList(string.Format("NoticeID={0} AND Position={1}", id, child)).Count == 0)
                {
                    p_toPositionbll.Model.NoticeID = id;
                    p_toPositionbll.Model.Position = int.Parse(child);
                    p_toPositionbll.Add();
                }
            }
        }

        if (OrganizeCityList != null)
        {
            PN_ToOrganizeCityBLL p_toOrganizeCitybll = new PN_ToOrganizeCityBLL();
            foreach (string child in OrganizeCityList)
            {
                if (PN_ToOrganizeCityBLL.GetModelList(string.Format("NoticeID={0} AND OrganizeCity={1}", id, child)).Count == 0)
                {
                    p_toOrganizeCitybll.Model.NoticeID = id;
                    p_toOrganizeCitybll.Model.OrganizeCity = int.Parse(child);
                    p_toOrganizeCitybll.Add();
                }
            }
        }
    }
    #endregion

    #region 修改公告时所属范围修改
    private void UpdateFaceTo()
    {
        foreach (int id in PN_ToPositionBLL.GetPositionByNoticeID((int)ViewState["ID"]))
        {
            if (lb_PositionChild.Items.FindByValue(id.ToString()) == null)
            {
                PN_ToPositionBLL.DeletePosition((int)ViewState["ID"], id);
            }
        }

        foreach (int id in PN_ToOrganizeCityBLL.GetOrganizeCityByNoticeID((int)ViewState["ID"]))
        {
            if (lb_CityChild.Items.FindByValue(id.ToString()) == null)
            {
                PN_ToOrganizeCityBLL.DeleteOrganizeCity((int)ViewState["ID"], id);
            }
        }
        AddFaceTo();
    }
    #endregion

    #region 获取ListBox中的值
    private void GetListItem(out List<string> PositionList, out List<string> OrganizeCityList)
    {
        if (tab_PNToPosition.Visible)
        {
            List<string> positionList = new List<string>();
            for (int i = 0; i < lb_PositionChild.Items.Count; i++)
            {
                positionList.Add(lb_PositionChild.Items[i].Value);
            }
            PositionList = positionList;
        }
        else
            PositionList = null;

        if (tab_PNToOrganizeCity.Visible)
        {
            List<string> cityList = new List<string>();
            for (int i = 0; i < lb_CityChild.Items.Count; i++)
            {
                cityList.Add(lb_CityChild.Items[i].Value);
            }
            OrganizeCityList = cityList;
        }
        else
            OrganizeCityList = null;
    }
    #endregion

    #region 将一个含逗号的字符串换成字符数组
    private string[] StringSplit(string parent)
    {
        if (parent.EndsWith(",")) parent = parent.Substring(0, parent.Length - 1);
        parent = parent.Replace("，", ",");
        string[] separators = new string[] { "," };
        string[] childList;
        childList = parent.Split(separators, StringSplitOptions.RemoveEmptyEntries);
        return childList;
    }
    #endregion

    #region 将选择的范围在ListBox中显示
    protected void bt_Insert1_Click(object sender, EventArgs e)
    {
        string positionID = tr_ToPosition.SelectValue;
        if (positionID != null && positionID != "" && positionID != "1")
        {
            Org_PositionBLL _positionbll = new Org_PositionBLL(int.Parse(positionID));
            lb_PositionChild.Items.Add(new ListItem(_positionbll.Model.Name, positionID.ToString()));
            if (this.chb_ToPositionChild.Checked)
            {
                string[] childList = StringSplit(_positionbll.GetAllChildPosition());

                foreach (string child in childList)
                {
                    if (lb_PositionChild.Items.FindByValue(child) == null)
                    {
                        _positionbll = new Org_PositionBLL(int.Parse(child));
                        lb_PositionChild.Items.Add(new ListItem(_positionbll.Model.Name, child));
                    }
                }
            }
        }


    }

    protected void bt_Insert2_Click(object sender, EventArgs e)
    {
        string organizeCityID = this.tr_ToOrganizeCity.SelectValue;
        if (organizeCityID != null && organizeCityID != "" && organizeCityID != "1")
        {
            Addr_OrganizeCityBLL _organizeCitybll = new Addr_OrganizeCityBLL(int.Parse(organizeCityID));
            lb_CityChild.Items.Add(new ListItem(_organizeCitybll.Model.Name, organizeCityID.ToString()));
            if (this.chb_ToOganizeCityChild.Checked)
            {
                string[] childList = StringSplit(_organizeCitybll.GetAllChildNodeIDs());

                foreach (string child in childList)
                {
                    if (lb_CityChild.Items.FindByValue(child) == null)
                    {
                        _organizeCitybll = new Addr_OrganizeCityBLL(int.Parse(child));
                        lb_CityChild.Items.Add(new ListItem(_organizeCitybll.Model.Name, child));
                    }
                }
            }
        }
    }
    #endregion

    #region 删除ListBox选中项
    protected void bt_Detele1_Click(object sender, EventArgs e)
    {
        IList<int> remove = new List<int>();

        foreach (ListItem item in lb_PositionChild.Items)
        {
            if (item.Selected)
            {
                remove.Add(int.Parse(item.Value));
            }
        }

        foreach (int v in remove)
        {
            ListItem item = lb_PositionChild.Items.FindByValue(v.ToString());
            if (item != null)
            {
                lb_PositionChild.Items.Remove(item);
            }
        }


    }
    protected void bt_Detele2_Click(object sender, EventArgs e)
    {
        IList<int> remove = new List<int>();

        foreach (ListItem item in lb_CityChild.Items)
        {
            if (item.Selected)
            {
                remove.Add(int.Parse(item.Value));
            }
        }

        foreach (int v in remove)
        {
            ListItem item = lb_CityChild.Items.FindByValue(v.ToString());
            if (item != null)
            {
                lb_CityChild.Items.Remove(item);
            }
        }

    }
    #endregion

    #region 清除ListBox中的所有项
    protected void bt_Clear1_Click(object sender, EventArgs e)
    {
        lb_PositionChild.Items.Clear();
    }
    protected void bt_Clear2_Click(object sender, EventArgs e)
    {
        lb_CityChild.Items.Clear();
    }
    #endregion

    protected void bt_PreView_Click(object sender, EventArgs e)
    {
        bt_OK_Click(null, null);
        Response.Redirect("read.aspx?ID=" + ViewState["ID"].ToString());
    }
    protected void bt_Approve_Click(object sender, EventArgs e)
    {
        bt_OK_Click(null, null);

        PN_NoticeBLL noticebll = new PN_NoticeBLL((int)ViewState["ID"]);
        noticebll.Approve(1, (int)Session["UserID"]);
        MessageBox.ShowAndRedirect(this, "审核成功", "index.aspx?Catalog=" + ViewState["Catalog"].ToString());
    }
    protected void bt_Back_Click(object sender, EventArgs e)
    {
        Response.Redirect("index.aspx?Catalog=" + ViewState["Catalog"].ToString());
    }

}
