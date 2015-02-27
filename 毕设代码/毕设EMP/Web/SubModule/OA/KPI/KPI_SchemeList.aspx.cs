using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.Model;
using MCSFramework.BLL.OA;
using MCSFramework.Model.OA;
public partial class SubModule_OA_KPI_KPI_SchemeList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDropDown();
            BindGrid();
        }

    }
    private void BindDropDown()
    {
        Org_StaffBLL staff = new Org_StaffBLL((int)Session["UserID"]);
      
        tr_Position.DataSource = Org_PositionBLL.GetAllPostion();

        #region 如果非总部职位，其只能选择自己职位及以下职位
        Org_Position p = new Org_PositionBLL(staff.Model.Position).Model;
        if (p != null && p.IsHeadOffice != "Y" && p.Remark != "OfficeHR")
        {
            //tr_Position.RootValue = staff.Model.Position.ToString();// p.SuperID.ToString();
            tr_Position.RootValue = p.SuperID.ToString();
            tr_Position.SelectValue = staff.Model.Position.ToString();
        }
        else
        {
            tr_Position.RootValue = "1";
            //tr_Position.SelectValue = "1";
        }
        tr_Position.SelectValue = tr_Position.RootValue;
        #endregion
        ddl_ApproveFlag.DataSource = DictionaryBLL.GetDicCollections("PUB_ApproveFlag");
        ddl_ApproveFlag.DataBind();
        ddl_ApproveFlag.Items.Insert(0, new ListItem("请选择", "0"));
    }
    private void BindGrid()
    {
        string condition = "1=1";
            
        #region 根据职位的范围查询
        if (tr_Position.SelectValue != tr_Position.RootValue && tr_Position.SelectValue != "0")
        {
            if (chb_ToPositionChild.Checked)
            {
                #region 绑定子职位
                Org_PositionBLL _bll = new Org_PositionBLL(int.Parse(tr_Position.SelectValue));
                string ids = "";
                ids = _bll.GetAllChildPosition();
                #endregion

                if (ids != "")
                    condition += " and RelatePosition in(" + tr_Position.SelectValue + "," + ids + ")";
                else
                    condition += " and RelatePosition =" + tr_Position.SelectValue;
            }
            else
            {
                condition += " and RelatePosition =" + tr_Position.SelectValue;
            }
        }
        #endregion
        if (ddl_ApproveFlag.SelectedValue != "0")
        {
            condition += " AND ApproveFlag="+ddl_ApproveFlag.SelectedValue;
        }
        gv_List.ConditionString = condition;
        gv_List.BindGrid();
    }
    protected void bt_Select_Click(object sender, EventArgs e)
    {
        gv_List.PageIndex = 0;
        BindGrid();
    }

    protected void gv_List_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_List.PageIndex = e.NewPageIndex;
        BindGrid();
    }
}
