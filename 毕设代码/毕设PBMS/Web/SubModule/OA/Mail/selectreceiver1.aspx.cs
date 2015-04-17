using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using MCSFramework.SQLDAL.OA;
using MCSFramework.BLL.OA;
using MCSFramework.BLL;
public partial class SubModule_OA_Mail_selectreceiver1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindPosition();
            PopulateData();
        }
    }

    private void BindPosition()
    {
        //ddl_Position.DataSource = Org_PositionBLL.GetAllPostion();
        //ddl_Position.DataBind();
        //ddl_Position.Items.Insert(0, new ListItem("请选择员工职位", "0"));

        tr_Position.DataSource = Org_PositionBLL.GetAllPostion();
        tr_Position.DataBind();
        tr_Position.SelectedValue = "1";
        //tr_Position.SelectValue = "1";
    }

    private void PopulateData()
    {
        //    //ML_AttachFileBLL bll=new  ML_AttachFileBLL();
        //    //listAccount.Items.Clear();
        //    //listAccount.DataSource = bll.GetPeo();
        //    //listAccount.DataBind();
        //    //listDept.DataSource = Position.GetAllPosition();
        //    //listDept.DataBind();
        //    //listDept.Items.Insert(0, new ListItem("公司所有部门", "0"));
        //    //listDept.SelectedIndex = 0;
        //    //listDept.Attributes["onclick"] = "SaveValue()";
    }

    protected void tr_Position_Selected(object sender, MCSControls.MCSWebControls.SelectedEventArgs e)
    {
        if (tr_Position.SelectedValue != "0")
        {
            string positions = "";
            if (cb_IncludeChild.Checked)
            {
                Org_PositionBLL pos = new Org_PositionBLL(int.Parse(tr_Position.SelectedValue));
                positions = pos.GetAllChildPosition();
                if (positions != "") positions += ",";
                positions += tr_Position.SelectedValue;
            }
            else
                positions = tr_Position.SelectedValue;

            DataTable dt = Org_StaffBLL.GetRealNameAndUserNameByPosition(positions);

            listAccount.Items.Clear();

            foreach (DataRow row in dt.Rows)
            {
                listAccount.Items.Add(new ListItem((string)row["UserName"] + "[" + (string)row["RealName"] + "]", (string)row["UserName"]));
            }
        }
    }

    protected void tr_Position_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (tr_Position.SelectedValue != "0")
        {
            string positions = "";
            if (cb_IncludeChild.Checked)
            {
                Org_PositionBLL pos = new Org_PositionBLL(int.Parse(tr_Position.SelectedValue));
                positions = pos.GetAllChildPosition();
                if (positions != "") positions += ",";
                positions += tr_Position.SelectedValue;
            }
            else
                positions = tr_Position.SelectedValue;

            DataTable dt = Org_StaffBLL.GetRealNameAndUserNameByPosition(positions);

            listAccount.Items.Clear();

            foreach (DataRow row in dt.Rows)
            {
                listAccount.Items.Add(new ListItem((string)row["UserName"] + "[" + (string)row["RealName"] + "]", (string)row["UserName"]));
            }
        }
    }

    //protected void tbx_Name_TextChanged(object sender, EventArgs e)
    //{

    //}
    protected void bt_serch_Click(object sender, EventArgs e)
    {
        if (tr_Position.SelectedValue != "0")
        {
            string positions = "";
            if (cb_IncludeChild.Checked)
            {
                Org_PositionBLL pos = new Org_PositionBLL(int.Parse(tr_Position.SelectedValue));
                positions = pos.GetAllChildPosition();
                if (positions != "") positions += ",";
                positions += tr_Position.SelectedValue;
            }
            else
                positions = tr_Position.SelectedValue;

            DataTable dt = Org_StaffBLL.GetRealNameAndUserNameByPosition(positions);
            dt.DefaultView.RowFilter = " RealName like '%" + tbx_Name.Text + "%' ";

            listAccount.Items.Clear();

            for (int i = 0; i < dt.DefaultView.Count; i++)
            {
                listAccount.Items.Add(new ListItem((string)dt.DefaultView[i]["UserName"] + "[" + (string)dt.DefaultView[i]["RealName"] + "]", (string)dt.DefaultView[i]["UserName"]));
            }
        }
    }
}
