using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MCSFramework.BLL;
using MCSFramework.Model;
using MCSFramework.Common;

public partial class SubModule_AddressManage_OrganizeCity_AddOfficialCity : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDropDown();

            if (Request.QueryString["OrganizeCity"] == null)
            {
                MessageBox.ShowAndClose(this, "参数错误!");
                return;
            }
            else
            {
                ViewState["OrganizeCity"] = int.Parse(Request.QueryString["OrganizeCity"]);

                Addr_OrganizeCityBLL organizecity = new Addr_OrganizeCityBLL((int)ViewState["OrganizeCity"]);
                if (organizecity.Model == null)
                {
                    MessageBox.ShowAndClose(this, "参数错误!");
                    return;
                }
                else if (organizecity.GetAllChildNode().Rows.Count != 0)
                {
                    MessageBox.ShowAndClose(this, "要加入的管理片区必须是最低一层的架构单元!");
                    return;
                }
                else
                {
                    lb_OrganizeCityName.Text = organizecity.Model.Name;

                    IList<Addr_OfficialCity> lists = Addr_OfficialCityBLL.GetModelList("Name like '%" + organizecity.Model.Name + "%'");
                    if (lists.Count > 0)
                    {
                        tr_OfficialCity.SelectValue = lists[0].ID.ToString();
                        tr_OfficialCity_Selected(null, null);
                    }

                }
            }
        }
    }

    private void BindDropDown()
    {
        
    }

    protected void tr_OfficialCity_Selected(object sender, MCSControls.MCSWebControls.SelectedEventArgs e)
    {
        cbl_OfficialList.Items.Clear();
        DataTable dt = TreeTableBLL.GetAllChildByNodes("MCS_SYS.dbo.Addr_OfficialCity", "ID", "SuperID", tr_OfficialCity.SelectValue);

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            Addr_OfficialCityBLL city = new Addr_OfficialCityBLL((int)dt.Rows[i]["ID"]);
            if (city.Model.Level == 3)
            {
                string fullname = TreeTableBLL.GetFullPathName("MCS_SYS.dbo.Addr_OfficialCity", city.Model.ID);
                ListItem item = new ListItem(fullname, city.Model.ID.ToString());

                IList<Addr_OfficialCityInOrganizeCity> organizecitys = Addr_OfficialCityInOrganizeCityBLL.GetModelList("OfficialCity=" + city.Model.ID.ToString());
                if (organizecitys.Count > 0)
                {
                    Addr_OrganizeCity organizecity = new Addr_OrganizeCityBLL(organizecitys[0].OrganizeCity).Model;
                    if (organizecity != null)
                    {
                        item.Text += "; 已归属于片区:【" + organizecity.Name + "】";
                    }
                    item.Enabled = false;
                }

                cbl_OfficialList.Items.Add(item);
            }
        }
    }
    protected void cb_CheckAll_CheckedChanged(object sender, EventArgs e)
    {
        foreach (ListItem item in cbl_OfficialList.Items)
        {
            if (item.Enabled)
            {
                item.Selected = cb_CheckAll.Checked;
            }
        }
    }

    protected void bt_AddOfficialCity_Click(object sender, EventArgs e)
    {
        int count = 0;
        foreach (ListItem item in cbl_OfficialList.Items)
        {
            if (item.Selected && item.Enabled)
            {
                Addr_OfficialCityInOrganizeCityBLL bll = new Addr_OfficialCityInOrganizeCityBLL();
                bll.Model.OrganizeCity = (int)ViewState["OrganizeCity"];
                bll.Model.OfficialCity = int.Parse(item.Value);

                if (bll.Add() > 0)
                {
                    count++;
                }
            }
        }

        MessageBox.ShowAndClose(this, "成功加入" + count.ToString() + "个行政区县!");
    }
}
