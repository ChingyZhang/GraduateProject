// ===================================================================
// 文件路径:SubModule/Service/QNA/QNA_ProjectDetail.aspx.cs 
// 生成日期:2009/12/13 17:56:40 
// 作者:	  Shen Gang
// ===================================================================

using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.Common;
using MCSFramework.BLL.QNA;
using MCSFramework.Model.QNA;
using MCSFramework.BLL;
using MCSFramework.Model;
public partial class SubModule_Service_QNA_QNA_ProjectDetail : System.Web.UI.Page
{
    private RadioButtonList rab_ToAllStaff, rab_ToAllOrganizeCity;
    private DropDownList ddl_FaceTo;
    protected void Page_Load(object sender, System.EventArgs e)
    {
        #region 界面自定底控件事件
        rab_ToAllStaff = (RadioButtonList)pl_detail.FindControl("QNA_Project_ToAllStaff");
        if (rab_ToAllStaff != null)
        {
            rab_ToAllStaff.AutoPostBack = true;
            rab_ToAllStaff.SelectedIndexChanged += new EventHandler(rab_ToAllStaff_SelectedIndexChanged);
        }

        rab_ToAllOrganizeCity = (RadioButtonList)pl_detail.FindControl("QNA_Project_ToAllOrganizeCity");
        if (rab_ToAllOrganizeCity != null)
        {
            rab_ToAllOrganizeCity.AutoPostBack = true;
            rab_ToAllOrganizeCity.SelectedIndexChanged += new EventHandler(rab_ToAllOrganizeCity_SelectedIndexChanged);
        }

        ddl_FaceTo = (DropDownList)pl_detail.FindControl("QNA_Project_FaceTo");
        if (ddl_FaceTo != null)
        {
            ddl_FaceTo.AutoPostBack = true;
            ddl_FaceTo.SelectedIndexChanged +=new EventHandler(ddl_FaceTo_SelectedIndexChanged);
        }
        #endregion
        // 在此处放置用户代码以初始化页面
        if (!Page.IsPostBack)
        {
            #region 获取页面参数
            ViewState["ID"] = Request.QueryString["ID"] != null ? int.Parse(Request.QueryString["ID"]) : 0;
            #endregion

            BindDropDown();

            if ((int)ViewState["ID"] != 0)
            {
                //修改
                BindData();
            }
            else
            {
                //新增
                tr_QuestionList.Visible = false;
                bt_Enabled.Visible = false;
                bt_Disabled.Visible = false;
                bt_ViewStatistics.Visible = false;
                tab_QNAToPosition.Visible = false;
                tab_QNAToOrganizeCity.Visible = false;
               
            }


            #region 判断是否非总部职位，不是总部职位没有权限进行设置
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
        tab_QNAToPosition.Visible =( rab_ToAllStaff.SelectedValue == "N");

        if (!tab_QNAToPosition.Visible) lb_PositionChild.Items.Clear();
    }

    protected void rab_ToAllOrganizeCity_SelectedIndexChanged(object sender, EventArgs e)
    {
        tab_QNAToOrganizeCity.Visible = (rab_ToAllOrganizeCity.SelectedValue == "N");

        if (!tab_QNAToOrganizeCity.Visible) lb_CityChild.Items.Clear();
    }
    #endregion

    private void BindData()
    {
        QNA_Project m = new QNA_ProjectBLL((int)ViewState["ID"]).Model;
        if (m != null)
        {
           
            pl_detail.BindData(m);

            if (m.Enabled == "Y")
                bt_Enabled.Visible = false;
            else
                bt_Disabled.Visible = false;

            tab_QNAToPosition.Visible =m.ToAllStaff == "N";
            tab_QNAToOrganizeCity.Visible = m.ToAllOrganizeCity == "N";
            ddl_FaceTo_SelectedIndexChanged(null, null);
            gv_List.ConditionString = "QNA_Question.Project=" + m.ID.ToString() + " ORDER BY QNA_Question.SortCode";
            gv_List.BindGrid();

            #region 显示所属职位，片区
            if (tab_QNAToPosition.Visible)
            {
                List<int> List = new List<int>();
                List = new QNA_ToPositionBLL().GetPositionByProjectID((int)ViewState["ID"]);
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

            if (tab_QNAToOrganizeCity.Visible)
            {
                List<int> List = new List<int>();
                List = new QNA_ToOrganizeCityBLL().GetOrganizeCityByProjectID((int)ViewState["ID"]);
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
        }
    }

    protected void bt_OK_Click(object sender, EventArgs e)
    {
        QNA_ProjectBLL _bll;
        if ((int)ViewState["ID"] != 0)
        {
            //修改
            _bll = new QNA_ProjectBLL((int)ViewState["ID"]);
        }
        else
        {
            //新增
            _bll = new QNA_ProjectBLL();
        }

        pl_detail.GetData(_bll.Model);

        #region 判断必填项
        if (_bll.Model.Classify == 0)
        {
            MessageBox.Show(this, "必须正确选择问卷分类!");
            return;
        }
        if (_bll.Model.DisplayType == 0)
        {
            MessageBox.Show(this, "必须正确选择问卷显示方式!");
            return;
        }
        if (_bll.Model.Enabled == "0") _bll.Model.Enabled = "N";
        #endregion


        #region 判断置顶公告的截止日期
       if (_bll.Model["CloseTime"] == "" || _bll.Model["CloseTime"] == "1900-01-01")
         {
                MessageBox.Show(this, "请设定问卷的截止日期!");
                return;
        }

        else if (DateTime.Parse(_bll.Model["CloseTime"]) < DateTime.Today)
        {
            MessageBox.Show(this, "设定问卷的截止日期不能小于今天!");
            return;
        }
       _bll.Model["CloseTime"] = DateTime.Parse(_bll.Model["CloseTime"]).ToShortDateString();
       
        #endregion

        #region 判断非面向全体公告是否选择了面向职位及面向区域
        if (_bll.Model.ToAllStaff == "N" && lb_PositionChild.Items.Count == 0)
        {
            MessageBox.Show(this, "请设定问卷要面向发布的职位！");
            return;
        }

        if (_bll.Model.ToAllOrganizeCity == "N" && lb_CityChild.Items.Count == 0)
        {
            MessageBox.Show(this, "请设定问卷要面向发布的管理片区！");
            return;
        }
        #endregion

        if ((int)ViewState["ID"] != 0)
        {
            //修改
            _bll.Model.UpdateStaff = (int)Session["UserID"];
            if (_bll.Update() == 0)
            {
                MessageBox.ShowAndRedirect(this, "修改成功!", "QNA_ProjectList.aspx");
            }
            UpdateFaceTo();
        }
        else
        {
            //新增
            _bll.Model.InsertStaff = (int)Session["UserID"];
            _bll.Model.Enabled = "N";
            _bll.Model.ApproveFlag = 2;

            ViewState["ID"] = _bll.Add();

            if (tab_QNAToPosition.Visible || tab_QNAToOrganizeCity.Visible ) AddFaceTo();

            if ((int)ViewState["ID"] > 0)
            {
                MessageBox.ShowAndRedirect(this, "新增成功!", "QNA_ProjectList.aspx");
            }
        }

    }

    protected void gv_List_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        int id = (int)gv_List.DataKeys[e.NewSelectedIndex][0];
        Response.Redirect("QNA_QuestionDetail.aspx?ID=" + id.ToString());
    }

    protected void bt_AddQuestion_Click(object sender, EventArgs e)
    {
        Response.Redirect("QNA_QuestionDetail.aspx?Project=" + ViewState["ID"].ToString());
    }

    protected void bt_Enabled_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ID"] != 0)
        {
            if (QNA_QuestionBLL.GetModelList("Project=" + ViewState["ID"].ToString() + " AND IsFirstQ='Y'").Count != 1)
            {
                MessageBox.Show(this, "对不起，问卷的标题中必须有，且只能有一项“问卷入口”！");
                return;
            }

            if (QNA_QuestionBLL.GetModelList("Project=" + ViewState["ID"].ToString() + " AND IsLastQ='Y'").Count != 1)
            {
                MessageBox.Show(this, "对不起，问卷的标题中必须有，且只能有一项“问卷出口”！");
                return;
            }

            QNA_ProjectBLL p = new QNA_ProjectBLL((int)ViewState["ID"]);
            p.Model.Enabled = "Y";

            p.Model.UpdateStaff = (int)Session["UserID"];
            p.Update();

            MessageBox.ShowAndRedirect(this, "启用成功!", "QNA_ProjectList.aspx");
        }
    }

    protected void bt_Disabled_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ID"] != 0)
        {
            QNA_ProjectBLL p = new QNA_ProjectBLL((int)ViewState["ID"]);
            p.Model.Enabled = "N";

            p.Model.UpdateStaff = (int)Session["UserID"];
            p.Update();

            MessageBox.ShowAndRedirect(this, "禁用成功!", "QNA_ProjectList.aspx");
        }
    }

    protected void bt_ViewStatistics_Click(object sender, EventArgs e)
    {
        Response.Redirect("QNA_ProjectStatistics.aspx?Project="+ViewState["ID"].ToString());
    }
   
    
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

    #region 添加公告时所属范围添加
    private void AddFaceTo()
    {
        int id = (int)ViewState["ID"];
        List<string> PositionList, OrganizeCityList;

        GetListItem(out PositionList, out OrganizeCityList);

        if (PositionList != null)
        {
            QNA_ToPositionBLL p_toPositionbll = new QNA_ToPositionBLL();
            foreach (string child in PositionList)
            {
                if (QNA_ToPositionBLL.GetModelList(string.Format("ProjectID={0} AND Position={1}", id, child)).Count == 0)
                {
                    p_toPositionbll.Model.ProjectID = id;
                    p_toPositionbll.Model.Position = int.Parse(child);
                    p_toPositionbll.Add();
                }
            }
        }

        if (OrganizeCityList != null)
        {
            QNA_ToOrganizeCityBLL p_toOrganizeCitybll = new QNA_ToOrganizeCityBLL();
            foreach (string child in OrganizeCityList)
            {
                if (QNA_ToOrganizeCityBLL.GetModelList(string.Format("ProjectID={0} AND OrganizeCity={1}", id, child)).Count == 0)
                {
                    p_toOrganizeCitybll.Model.ProjectID = id;
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
        foreach (int id in new QNA_ToPositionBLL().GetPositionByProjectID((int)ViewState["ID"]))
        {
            if (lb_PositionChild.Items.FindByValue(id.ToString()) == null)
            {
               new QNA_ToPositionBLL().DeletePosition((int)ViewState["ID"], id);
            }
        }

        foreach (int id in new QNA_ToOrganizeCityBLL().GetOrganizeCityByProjectID((int)ViewState["ID"]))
        {
            if (lb_CityChild.Items.FindByValue(id.ToString()) == null)
            {
                new  QNA_ToOrganizeCityBLL().DeleteOrganizeCity((int)ViewState["ID"], id);
            }
        }
        AddFaceTo();
    }
    #endregion

    #region 获取ListBox中的值
    private void GetListItem(out List<string> PositionList, out List<string> OrganizeCityList)
    {
        if (tab_QNAToPosition.Visible)
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

        if (tab_QNAToOrganizeCity.Visible)
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


    protected void ddl_FaceTo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_FaceTo.SelectedValue == "1")//员工
        {
            rab_ToAllStaff.Enabled = true;
            
        }
        else
        {
            rab_ToAllStaff.SelectedValue = "Y";
            rab_ToAllStaff_SelectedIndexChanged(null, null);
            rab_ToAllStaff.Enabled = false;
        }
    }
}