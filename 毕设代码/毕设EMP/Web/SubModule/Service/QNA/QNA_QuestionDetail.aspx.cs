using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.Model.QNA;
using MCSFramework.BLL.QNA;
using MCSFramework.Common;

public partial class SubModule_Service_QNA_QNA_QuestionDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        // 在此处放置用户代码以初始化页面
        if (!Page.IsPostBack)
        {
            #region 获取页面参数
            ViewState["ID"] = Request.QueryString["ID"] != null ? int.Parse(Request.QueryString["ID"]) : 0;
            ViewState["Project"] = Request.QueryString["Project"] != null ? int.Parse(Request.QueryString["Project"]) : 0;
            #endregion

            BindDropDown();

            if ((int)ViewState["ID"] != 0)
            {
                //修改
                BindData();

                #region 绑定跳转问题
                ddl_NextQuestion.DataSource = QNA_QuestionBLL.GetModelList("Project=" + ViewState["Project"].ToString() + " AND IsFirstQ='N' AND ID<>" + ViewState["ID"].ToString());
                ddl_NextQuestion.DataBind();
                ddl_NextQuestion.Items.Insert(0, new ListItem("请选择...", "0"));
                #endregion
            }
            else
            {
                //新增
                if ((int)ViewState["Project"] == 0)
                {
                    MessageBox.ShowAndRedirect(this, "参数错误，未包含所属问卷Project参数!", "QNA_ProjectList.aspx");
                    return;
                }
                BindProjectInfo();

                #region 新增标题时，界面控件默认设置
                tr_OptionList.Visible = false;


                ((DropDownList)pl_detail.FindControl("QNA_Question_IsFirstQ")).SelectedValue = "N";
                ((DropDownList)pl_detail.FindControl("QNA_Question_IsLastQ")).SelectedValue = "N";


                #endregion
            }
        }
    }

    #region 绑定下拉列表框
    private void BindDropDown()
    {

    }
    #endregion

    private void BindProjectInfo()
    {
        QNA_Project q = new QNA_ProjectBLL((int)ViewState["Project"]).Model;
        if (q != null)
        {
            lb_ProjectName.Text = q.Name;

            #region 绑定默认下一标题控件
            DropDownList ddl_DefaultNextQ = ((DropDownList)pl_detail.FindControl("QNA_Question_DefaultNextQ"));
            ddl_DefaultNextQ.DataTextField = "Title";
            ddl_DefaultNextQ.DataValueField = "ID";
            ddl_DefaultNextQ.DataSource = QNA_QuestionBLL.GetModelList("Project=" + ViewState["Project"].ToString() + " AND IsFirstQ='N' AND ID<>" + ViewState["ID"].ToString());
            ddl_DefaultNextQ.DataBind();
            ddl_DefaultNextQ.Items.Insert(0, new ListItem("无默认下一标题", "0"));
            #endregion

            if (q.DisplayType == 2)
            {
                //顺序显示
                ddl_NextQuestion.Enabled = false;
            }
        }
    }

    private void BindData()
    {
        QNA_Question m = new QNA_QuestionBLL((int)ViewState["ID"]).Model;
        if (m != null)
        {
            ViewState["Project"] = m.Project;
            BindProjectInfo();

            pl_detail.BindData(m);

            #region 绑定标题选项结果
            if (m.OptionMode == 1 || m.OptionMode == 2)
            {
                BindGrid();
                if (m.OptionMode == 2) ddl_NextQuestion.Enabled = false;
            }
            else
            {
                tr_OptionList.Visible = false;
            }
            #endregion
        }
    }

    private void BindGrid()
    {
        gv_List.BindGrid<QNA_QuestionOption>(new QNA_QuestionBLL((int)ViewState["ID"]).Items);
    }

    protected void bt_OK_Click(object sender, EventArgs e)
    {
        QNA_QuestionBLL _bll;
        if ((int)ViewState["ID"] != 0)
        {
            //修改
            _bll = new QNA_QuestionBLL((int)ViewState["ID"]);
        }
        else
        {
            //新增
            _bll = new QNA_QuestionBLL();
        }

        pl_detail.GetData(_bll.Model);

        #region 判断必填项
      /*  if (_bll.Model.IsLastQ == "N" && _bll.Model.DefaultNextQ == 0)
        {
            MessageBox.Show(this, "必须正确选择默认下一题!");
            return;
        }
       */

        if (_bll.Model.OptionMode == 0)
        {
            MessageBox.Show(this, "必须正确选择选项模式!");
            return;
        }
        if (_bll.Model.IsFirstQ == "0") _bll.Model.IsFirstQ = "N";
        if (_bll.Model.IsLastQ == "0") _bll.Model.IsLastQ = "N";

        if (_bll.Model.IsLastQ == "Y" && _bll.Model.DefaultNextQ != 0) _bll.Model.DefaultNextQ = 0;
        #endregion

        if ((int)ViewState["ID"] != 0)
        {
            //修改
            _bll.Model.UpdateStaff = (int)Session["UserID"];
            if (_bll.Update() == 0)
            {
                MessageBox.ShowAndRedirect(this, "修改成功!", "QNA_QuestionDetail.aspx?ID=" + ViewState["ID"].ToString());
            }
        }
        else
        {
            //新增
            _bll.Model.Project = (int)ViewState["Project"];
            _bll.Model.InsertStaff = (int)Session["UserID"];
            _bll.Model.ApproveFlag = 2;

            ViewState["ID"] = _bll.Add();
            if ((int)ViewState["ID"] > 0)
            {
                MessageBox.ShowAndRedirect(this, "新增成功!", "QNA_QuestionDetail.aspx?ID=" + ViewState["ID"].ToString());
            }
        }
    }

    protected void bt_ReturnProject_Click(object sender, EventArgs e)
    {
        Response.Redirect("QNA_ProjectDetail.aspx?ID=" + ViewState["Project"].ToString());
    }

    protected void bt_AddOption_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["ID"] > 0)
        {
            QNA_QuestionBLL q = new QNA_QuestionBLL((int)ViewState["ID"]);

            QNA_QuestionOption o = new QNA_QuestionOption();
            o.OptionName = tbx_OptionName.Text;
            o.NextQuestion = int.Parse(ddl_NextQuestion.SelectedValue);
            o.CanInputText = ddl_YesOrNo.SelectedValue;
            o.Question = (int)ViewState["ID"];
            q.AddDetail(o);

            BindGrid();

            tbx_OptionName.Text = "";
            ddl_NextQuestion.SelectedValue = "0";
            ddl_YesOrNo.SelectedValue = "N";
        }
    }

    protected void gv_List_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int i =Int32.Parse( gv_List.DataKeys[e.RowIndex].Value.ToString());
        QNA_QuestionBLL q = new QNA_QuestionBLL((int)ViewState["ID"]);
        q.DeleteDetail(i);
        gv_List.BindGrid<QNA_QuestionOption>(new QNA_QuestionBLL((int)ViewState["ID"]).Items);

    }
}
