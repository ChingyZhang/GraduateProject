using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.Model.CM;
using MCSFramework.BLL.CM;
using MCSFramework.Common;
using MCSFramework.Model.Pub;
using MCSFramework.BLL.Pub;
using MCSFramework.BLL;
using System.Collections.Specialized;
using MCSFramework.BLL.EWF;

public partial class SubModule_CM_PD_PropertyContractDetail : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            #region 获取页面参数
            ViewState["ContractID"] = Request.QueryString["ContractID"] == null ? 0 : int.Parse(Request.QueryString["ContractID"]);
            if (Request.QueryString["ClientID"] != null)
            {
                ViewState["ClientID"] = Int32.Parse(Request.QueryString["ClientID"]);
                Session["ClientID"] = ViewState["ClientID"];
            }
            else if (Session["ClientID"] != null)
            {
                ViewState["ClientID"] = Int32.Parse(Session["ClientID"].ToString());
            }

            if ((int)ViewState["ContractID"] == 0 && (int)ViewState["ClientID"] == 0) Response.Redirect("~/SubModule/DeskTop.aspx");
            #endregion

            BindDropDown();

            #region 创建空的列表
            ListTable<CM_ContractDetail> _details = new ListTable<CM_ContractDetail>(new CM_ContractBLL((int)ViewState["ContractID"]).Items, "AccountTitle");
            ViewState["Details"] = _details;
            #endregion

            if ((int)ViewState["ContractID"] != 0)
            {
                BindData();
            }
            else if ((int)ViewState["ClientID"] != 0)
            {
                CM_Contract _c = new CM_Contract();
                _c.Client = (int)ViewState["ClientID"];
                _c.BeginDate = DateTime.Today;
                _c.EndDate = DateTime.Today.AddYears(1);
                _c.Classify = 21;       //物业租赁合同
                _c.ApproveFlag = 2;
                _c.State = 1;
                pl_detail.BindData(_c);

                bt_del.Visible = false;
                bt_Submit.Visible = false;
                bt_Disable.Visible = false;
                bt_FeeApply.Visible = false;
                UploadFile1.Visible = false;
            }
            else
            {
                Response.Redirect("PropertyList.aspx");
            }
        }
    }

    private void BindDropDown()
    {
        #region 绑定物业租赁所需的费用科目
        int ContractFeeType = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["ContractFeeType-PD"]);
        int ContractAccountTitle = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["ContractAccountTitle-PD"]);
        ddl_AccountTitle.DataSource = AC_AccountTitleBLL.GetListByFeeType(ContractFeeType).Where(p => p.SuperID == ContractAccountTitle || p.ID == ContractAccountTitle);
        ddl_AccountTitle.DataBind();
        #endregion

        #region 绑定付款周期
        ddl_PayMode.DataSource = DictionaryBLL.GetDicCollections("PUB_PayMode");
        ddl_PayMode.DataBind();
        ddl_PayMode.Items.Insert(0, new ListItem("所有", "0"));
        ddl_PayMode.SelectedValue = "1";
        #endregion
    }

    public void BindData()
    {
        CM_Contract c = new CM_ContractBLL((int)ViewState["ContractID"]).Model;
        if (c != null)
        {
            ViewState["ClientID"] = c.Client;
            pl_detail.BindData(c);
            BindGrid();
            UploadFile1.RelateID = c.ID;

            if (c.ApproveFlag == 1 || c.State > 1)
            {
                //已审核
                pl_detail.SetControlsEnable(false);
                bt_Submit.Visible = false;
                bt_OK.Visible = false;
                tr_AddDetail.Visible = false;
                bt_AddDetail.Visible = false;
                gv_Detail.Columns[gv_Detail.Columns.Count - 1].Visible = false;
                bt_del.Visible = false;

                UploadFile1.CanDelete = false;
                //UploadFile1.CanUpload = false;
            }

            if (c.State != 3)
            {
                bt_FeeApply.Visible = false;
                bt_Disable.Visible = false;
            }
            ///审批过程中，可以修改编码 Decision参数为在审批过程中传进来的参数
            //if (Request.QueryString["Decision"] != "" && Request.QueryString["Decision"] == "Y")
            //{
            //    bt_OK.Visible = true;
            //    gv_Detail.Enabled = false;
            //    pl_detail.SetControlsEnable(true);
            //}
        }
    }

    private void BindGrid()
    {
        ListTable<CM_ContractDetail> _details = ViewState["Details"] as ListTable<CM_ContractDetail>;
        gv_Detail.BindGrid<CM_ContractDetail>(_details.GetListItem());
    }

    protected void bt_OK_Click(object sender, EventArgs e)
    {
        CM_ContractBLL _bll = null;

        #region 判断合同编码是否重复
        //合同编码的获取
        TextBox tbx_ContractCode = pl_detail.FindControl("CM_Contract_ContractCode") == null ? null : (TextBox)pl_detail.FindControl("CM_Contract_ContractCode");
        if ((int)ViewState["ContractID"] == 0)
        {
            _bll = new CM_ContractBLL();

            if (tbx_ContractCode != null && tbx_ContractCode.Text != "" && CM_ContractBLL.GetModelList("ContractCode='" + tbx_ContractCode.Text.Trim() + "'").Count > 0)
            {
                MessageBox.Show(this, "对不起，合同编码" + tbx_ContractCode.Text.Trim() + "数据库已存在。");
                return;
            }
        }
        else
        {
            _bll = new CM_ContractBLL((int)ViewState["ContractID"]);
            if (tbx_ContractCode != null && tbx_ContractCode.Text != "" && CM_ContractBLL.GetModelList("ContractCode='" + tbx_ContractCode.Text.Trim() + "' AND ID !=" + _bll.Model.ID.ToString()).Count > 0)
            {
                MessageBox.Show(this, "对不起，合同编码" + tbx_ContractCode.Text.Trim() + "数据库已存在。");
                return;
            }
        }
        #endregion

        pl_detail.GetData(_bll.Model);

        ListTable<CM_ContractDetail> _details = ViewState["Details"] as ListTable<CM_ContractDetail>;
        if ((int)ViewState["ContractID"] == 0)
        {
            _bll.Model.Classify = 21;       //租赁合同
            _bll.Model.State = 1;
            _bll.Model.ApproveFlag = 2;
            _bll.Model.InsertTime = DateTime.Now;
            _bll.Model.InsertStaff = (int)Session["UserID"];
            _bll.Model.Client = int.Parse(ViewState["ClientID"].ToString());
            _bll.Items = _details.GetListItem();
            ViewState["ContractID"] = _bll.Add();
        }
        else
        {
            _bll.Model.UpdateTime = DateTime.Now;
            _bll.Model.UpdateStaff = (int)Session["UserID"];
            _bll.Update();
            #region 修改明细
            _bll.Items = _details.GetListItem(ItemState.Added);
            _bll.AddDetail();

            foreach (CM_ContractDetail _deleted in _details.GetListItem(ItemState.Deleted))
            {
                _bll.DeleteDetail(_deleted.ID);
            }

            _bll.Items = _details.GetListItem(ItemState.Modified);
            _bll.UpdateDetail();

            #endregion
        }
        if (Request.QueryString["Decision"] != "" && Request.QueryString["Decision"] == "Y")
        {
            MessageBox.Show(this, "协议编码保存成功！");
        }
        else
        {
            MessageBox.ShowAndRedirect(this, "保存物业协议详细信息成功！", "PropertyContractDetail.aspx?ContractID=" + ViewState["ContractID"].ToString());
        }
    }

    protected void bt_AddDetail_Click(object sender, EventArgs e)
    {
        ListTable<CM_ContractDetail> _details = ViewState["Details"] as ListTable<CM_ContractDetail>;

        DateTime begindate = new DateTime();
        DateTime enddate = new DateTime();
        TextBox tbx_BeginDate = pl_detail.FindControl("CM_Contract_BeginDate") == null ? null : (TextBox)pl_detail.FindControl("CM_Contract_BeginDate");
        TextBox tbx_EndDate = pl_detail.FindControl("CM_Contract_EndDate") == null ? null : (TextBox)pl_detail.FindControl("CM_Contract_EndDate");
        if (tbx_BeginDate != null && tbx_EndDate != null)
        {
            DateTime.TryParse(tbx_BeginDate.Text, out begindate);
            DateTime.TryParse(tbx_EndDate.Text, out enddate);
            if (enddate < begindate)
            {
                MessageBox.Show(this, "协议终止日期不能小于起始日期。");
                return;
            }
        }

        if (ddl_PayMode.SelectedValue == "0")
        {
            MessageBox.Show(this, "付款周期必选！");
            return;
        }


        CM_ContractDetail item;
        if (ViewState["Selected"] == null)
        {
            //新增科目
            if (_details.Contains(ddl_AccountTitle.SelectedValue))
            {
                MessageBox.Show(this, "该科目已添加！");
                return;
            }

            item = new CM_ContractDetail();
            item.AccountTitle = int.Parse(ddl_AccountTitle.SelectedValue);
            if (TreeTableBLL.GetChild("MCS_PUB.dbo.AC_AccountTitle", "ID", "SuperID", item.AccountTitle).Rows.Count > 0)
            {
                MessageBox.Show(this, "费用科目必须选择最底级会计科目!" + ddl_AccountTitle.SelectedItem.Text);
                return;
            }
        }
        else
        {//修改科目
            if (!_details.Contains(ddl_AccountTitle.SelectedValue))
            {
                MessageBox.Show(this, "要修改的项目不存在！");
                return;
            }
            item = _details[ViewState["Selected"].ToString()];
        }
        item.ApplyLimit = decimal.Parse(tbx_ApplyLimit.Text);
        if (item.ApplyLimit == 0)
        {
            MessageBox.Show(this, "对不起,月费用金额不能为0！");
            return;
        }
        item.PayMode = int.Parse(ddl_PayMode.SelectedValue);

        if (ViewState["Selected"] == null)
            _details.Add(item);
        else
            _details.Update(item);

        BindGrid();

        tbx_ApplyLimit.Text = "0";
        bt_AddDetail.Text = "新增";
        ViewState["Selected"] = null;
    }

    #region 科目的明细及删除
    protected void gv_Detail_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int title = (int)gv_Detail.DataKeys[e.RowIndex]["AccountTitle"];
        ListTable<CM_ContractDetail> _details = ViewState["Details"] as ListTable<CM_ContractDetail>;
        _details.Remove(title.ToString());
        BindGrid();
    }
    #endregion


    protected void gv_Detail_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        int accounttitle = int.Parse(gv_Detail.DataKeys[e.NewSelectedIndex]["AccountTitle"].ToString());
        ddl_AccountTitle.SelectedValue = accounttitle.ToString();
        ListTable<CM_ContractDetail> _details = ViewState["Details"] as ListTable<CM_ContractDetail>;
        tbx_ApplyLimit.Text = _details[accounttitle.ToString()].ApplyLimit.ToString();
        ddl_PayMode.SelectedValue = _details[accounttitle.ToString()].PayMode.ToString();
        ViewState["Selected"] = accounttitle.ToString();
        bt_AddDetail.Text = "修改";
    }

    protected void bt_Submit_Click(object sender, EventArgs e)
    {
        CM_ContractBLL _bll = new CM_ContractBLL((int)ViewState["ContractID"]);
        CM_Client client = new CM_ClientBLL(_bll.Model.Client).Model;

        if (_bll.Items.Count == 0)
        {
            MessageBox.Show(this, "对不起，物业租赁合同必须要录入合同具体的付款科目才能提交申请！");
            return;
        }
        decimal applycost = _bll.Items.Sum(p => p.ApplyLimit);

        NameValueCollection dataobjects = new NameValueCollection();
        dataobjects.Add("ID", _bll.Model.ID.ToString());
        dataobjects.Add("Classify", _bll.Model.Classify.ToString());
        dataobjects.Add("ApplyCost", applycost.ToString("0.##"));

        dataobjects.Add("OrganizeCityID", client.OrganizeCity.ToString());
        dataobjects.Add("PDClassify", client["PDClassify"]);        //物业类别

        #region 组织任务标题
        string _title = "";
        Label lb_Client = (Label)pl_detail.FindControl("CM_Contract_Client");
        if (lb_Client != null) _title += "物业名称:" + lb_Client.Text;

        if (_bll.Model.ContractCode != "") _title += " 合同编码:" + _bll.Model.ContractCode;
        #endregion

        int TaskID = EWF_TaskBLL.NewTask("CM_Contract_Flow", (int)Session["UserID"], _title,
            "~/SubModule/CM/PD/PropertyDetail.aspx?ClientID=" + _bll.Model.Client.ToString(), dataobjects);
        if (TaskID > 0)
        {
            _bll.Model.State = 2;
            _bll.Model.ApproveTask = TaskID;
            _bll.Update();
            new EWF_TaskBLL(TaskID).Start();        //直接启动流程
        }

        Response.Redirect("~/SubModule/EWF/TaskDetail.aspx?TaskID=" + TaskID.ToString());
    }

    protected void bt_del_Click(object sender, EventArgs e)
    {
        CM_ContractBLL _bll = new CM_ContractBLL((int)ViewState["ContractID"]);
        int Client = _bll.Model.Client;
        _bll.Delete();
        MessageBox.ShowAndRedirect(this, "删除成功！", "PropertyDetail.aspx?ClientID=" + Client.ToString());
    }

    protected void bt_Disable_Click(object sender, EventArgs e)
    {
        CM_ContractBLL _bll = new CM_ContractBLL((int)ViewState["ContractID"]);
        if (_bll != null && _bll.Model.ApproveFlag == 1 && _bll.Model.State == 3)
        {
            _bll.Disable((int)Session["UserID"]);
            MessageBox.ShowAndRedirect(this, "协议中止成功！", "PropertyDetail.aspx?ClientID=" + _bll.Model.Client.ToString());
        }
    }
    protected void bt_FeeApply_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/SubModule/FNA/FeeApply/FeeApply_Contract.aspx?ContractID=" + ViewState["ContractID"].ToString());
    }
}
