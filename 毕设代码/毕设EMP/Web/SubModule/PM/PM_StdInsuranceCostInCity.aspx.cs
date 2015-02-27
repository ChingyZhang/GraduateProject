using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.Pub;
using MCSFramework.Common;
using MCSFramework.Model.Pub;
using MCSFramework.BLL.Promotor;
using MCSFramework.Model.Promotor;

public partial class SubModule_PM_PM_StdInsuranceCostInCity : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["Insurance"] = Request.QueryString["Insurance"] == null ? 0 : int.Parse(Request.QueryString["Insurance"]);

            if ((int)ViewState["Insurance"] > 0)
            {
                BindCheckBoxList();
            }
            else
            {
                MessageBox.Show(this, "无导购保险ID参数!");
                return;
            }
        }
    }

    private void BindCheckBoxList()
    {
        if ((int)ViewState["Insurance"] > 0)
        {
            cbl_ApplyCity.Items.Clear();

            PM_StdInsuranceCostBLL bll = new PM_StdInsuranceCostBLL((int)ViewState["Insurance"]);
            IList<PM_StdInsuranceCostInCity> pms =  PM_StdInsuranceCostInCityBLL.GetModelList("Insurance=" + (int)ViewState["Insurance"]);
            foreach (PM_StdInsuranceCostInCity p in pms)
            {
                string cityname = TreeTableBLL.GetFullPathName("MCS_SYS.dbo.Addr_OrganizeCity", p.City);
                cbl_ApplyCity.Items.Add(new ListItem(cityname, p.ID.ToString()));
            }

            tr_OrganizeCity.DataSource = TreeTableBLL.GetAllChildNodeByNodes("MCS_SYS.dbo.Addr_OrganizeCity", "ID", "SuperID","1");
            tr_OrganizeCity.RootValue = "1";
            tr_OrganizeCity.SelectValue = tr_OrganizeCity.RootValue;

            if (bll.Model.ApproveFlag == 1)
            {
                bt_Add.Visible = false;
                bt_Delete.Visible = false;
                tr_OrganizeCity.Enabled = false;
            }
        }
    }
    protected void bt_Add_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["Insurance"] > 0)
        {
            if (tr_OrganizeCity.SelectValue != "0" && tr_OrganizeCity.SelectValue != tr_OrganizeCity.RootValue)
            {
                PM_StdInsuranceCostBLL bll = new PM_StdInsuranceCostBLL((int)ViewState["Insurance"]);

                Addr_OrganizeCityBLL selectedcity = new Addr_OrganizeCityBLL(int.Parse(tr_OrganizeCity.SelectValue));
                string[] allparent = selectedcity.GetAllSuperNodeIDs().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                IList<PM_StdInsuranceCostInCity> pms = PM_StdInsuranceCostInCityBLL.GetModelList("Insurance=" + (int)ViewState["Insurance"]);
                foreach (PM_StdInsuranceCostInCity p in pms)
                {
                    if (selectedcity.Model.ID == p.City)
                    {
                        MessageBox.Show(this, "对不起,该区域已在适用区域内，请勿重复添加!");
                        return;
                    }

                    if (allparent.Contains(p.City.ToString()))
                    {
                        MessageBox.Show(this, "对不起,要新增的区域的上级【" + TreeTableBLL.GetFullPathName("MCS_SYS.dbo.Addr_OrganizeCity", p.City) + "】已在适用区域内!");
                        return;
                    }
                }

                PM_StdInsuranceCostInCityBLL c = new PM_StdInsuranceCostInCityBLL();
                c.Model.City = int.Parse(tr_OrganizeCity.SelectValue);
                c.Model.Insurance = bll.Model.ID;
                c.Model.InsertStaff = (int)Session["UserID"];
                c.Model.InsertTime = DateTime.Now;
                c.Add();

                BindCheckBoxList();
            }
        }
    }
    protected void bt_Delete_Click(object sender, EventArgs e)
    {
        if ((int)ViewState["Insurance"] > 0)
        {
            foreach (ListItem item in cbl_ApplyCity.Items)
            {
                if (item.Selected)
                {
                    PM_StdInsuranceCostInCityBLL c = new PM_StdInsuranceCostInCityBLL();
                    c.Delete(int.Parse(item.Value));
                }
            }

            BindCheckBoxList();
        }
    }
}
