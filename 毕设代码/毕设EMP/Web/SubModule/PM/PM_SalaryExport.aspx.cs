using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL.Pub;
using MCSFramework.Model;
using MCSFramework.BLL;
using System.IO;
using MCSFramework.Common;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Data;
using MCSFramework.BLL.Promotor;
using System.Text;

public partial class SubModule_PM_PM_SalaryExport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDropDown();
        }
    }

    /// <summary>
    ///  绑定下拉列表框
    /// </summary>
    private void BindDropDown()
    {
        #region 绑定用户可管辖的管理片区
        Org_StaffBLL staff = new Org_StaffBLL((int)Session["UserID"]);
        tr_OrganizeCity.DataSource = staff.GetStaffOrganizeCity();

        if (tr_OrganizeCity.DataSource.Select("ID = 1").Length > 0)
        {
            tr_OrganizeCity.RootValue = "0";
            tr_OrganizeCity.SelectValue = "1";
        }
        else
        {
            tr_OrganizeCity.RootValue = new Addr_OrganizeCityBLL(staff.Model.OrganizeCity).Model.SuperID.ToString();
            tr_OrganizeCity.SelectValue = staff.Model.OrganizeCity.ToString();
        }
        #endregion


        ddl_AccountMonth.DataSource = AC_AccountMonthBLL.GetModelList("");
        ddl_AccountMonth.DataBind();
        ddl_AccountMonth.SelectedValue = (AC_AccountMonthBLL.GetCurrentMonth() - 1).ToString();

    }

    protected void btn_Command(object sender, CommandEventArgs e)
    {
        int AccountMonth = int.Parse(ddl_AccountMonth.SelectedValue);
        int OrganizeCity = int.Parse(tr_OrganizeCity.SelectValue);
        //总部>>西南大区>>西南大区>>四川营业部(23)
        string OrganizeCityName = tr_OrganizeCity.SelectText;
        if (OrganizeCityName.LastIndexOf(">>") == -1)
        {
            MessageBox.Show(this, "请选择营业部");
            return;
        }
        OrganizeCityName = OrganizeCityName.Remove(0, OrganizeCityName.LastIndexOf(">>") + 2);
        if (OrganizeCityName.IndexOf('(') > 0)
        {
            OrganizeCityName = OrganizeCityName.Remove(OrganizeCityName.IndexOf('('));
        }
        OrganizeCityName += ddl_AccountMonth.SelectedItem.Text;

        int bankCode = 0;
        if (e.CommandName == "ABC")//农行导出为txt
        {
            bankCode = 1;
        }
        else if (e.CommandName == "CCB")//工行导出为Excel
        {
            bankCode = 2;
        }
        else if (e.CommandName == "ABCExcel")//工行导出为Excel
        {
            bankCode = 3;
        }
        string filePath = GetFilePath(bankCode, OrganizeCityName);

        bool flag = Salary_Export(AccountMonth, OrganizeCity, bankCode, "工资", filePath);
        if (flag)
        {
            DownLoadFile(filePath);
        }
        else
        {
            MessageBox.Show(Page, "无工资数据");
        }
    }

    /// <summary>
    /// 获取文件存放路径
    /// </summary>
    /// <param name="bankCode">银行分类（1农行，2建行）</param>
    /// <returns></returns>
    public string GetFilePath(int bankCode, string fileName)
    {
        string path = ConfigHelper.GetConfigString("AttachmentPath");
        if (path.StartsWith("~")) path = Server.MapPath(path);
        if (!path.EndsWith("\\")) path = path + "\\";
        path += "Attachment\\SalaryExport\\";
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        string filePath = path + fileName;
        if (bankCode == 1)//农行导出为txt
        {
            filePath += "农行工资.txt";
        }
        else if (bankCode == 2)//工行导出为Excel
        {
            filePath += "建行工资.xlsx";
        }
        else if (bankCode == 3)
        {
            filePath += "农行工资.xlsx";
        }
        return filePath;
    }

    /// <summary>
    /// 提供工资文件下载
    /// </summary>
    /// <param name="filePath"></param>
    public void DownLoadFile(string filePath)
    {
        //获取文件名（包括文件类型）
        string fileName = filePath.Remove(0, filePath.LastIndexOf("\\") + 1);

        Response.Clear();
        Response.BufferOutput = true;
        Response.ContentType = "application/ms-excel";
        Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
        if (File.Exists(filePath))
        {
            Response.WriteFile(filePath);//通知浏览器下载文件
        }
        Response.Flush();

        File.Delete(filePath);

        Response.End();
    }

    /// <summary>
    /// 导出工资单并写入文件路径
    /// </summary>
    /// <param name="AccountMonth"></param>
    /// <param name="OrganizeCity"></param>
    /// <param name="BankCode">1：农行（导出为txt）；2：建行（导出为Excel）；3：导出农行Excel</param>
    /// <param name="Remark"></param>
    /// <param name="filePath">文件存放路径</param>
    public bool Salary_Export(int AccountMonth, int OrganizeCity, int BankCode, string Remark, string filePath)
    {
        int bankCode = BankCode == 1 || BankCode == 3 ? 1 : 2;
        DataTable table = PM_SalaryBLL.Salary_Table_Export(AccountMonth, OrganizeCity, bankCode, Remark);

        if (table == null || table.Columns.Count == 0 || table.Rows.Count == 0)
        {
            return false;
        }

        bool flag = false;
        if (BankCode == 1)
        {
            flag = SalaryTableToTxt(table, filePath);
        }
        else if (BankCode == 2)
        {
            flag = SalaryTableToExcel(table, filePath);
        }
        else if (BankCode == 3)
        {
            flag = SalaryTableToExcel(table, filePath);
        }
        return flag;
    }

    /// <summary>
    /// 工资数据导入Excel
    /// </summary>
    /// <param name="table"></param>
    /// <param name="filePath"></param>
    /// <returns></returns>
    public bool SalaryTableToExcel(DataTable table, string filePath)
    {
        if (table == null || table.Columns.Count == 0 || table.Rows.Count == 0)
        {
            return false;
        }

        IWorkbook hssfworkbook = new XSSFWorkbook();
        ISheet sheet = hssfworkbook.CreateSheet("sheet1");


        ICellStyle cellStyle = hssfworkbook.CreateCellStyle();
        cellStyle.Alignment = HorizontalAlignment.Center;
        cellStyle.VerticalAlignment = VerticalAlignment.Center;

        IRow row;
        ICell cell;
        for (int i = 0; i < table.Rows.Count; i++)
        {
            row = sheet.CreateRow(i);
            for (int j = 0; j < table.Columns.Count; j++)
            {
                cell = row.CreateCell(j);
                cell.CellStyle = cellStyle;
                cell.SetCellValue(table.Rows[i][j].ToString());
            }
        }
        try
        {
            FileStream fs = File.Create(filePath);//new FileStream(filePath, FileMode.Create);
            hssfworkbook.Write(fs);
            fs.Close();
        }
        catch (Exception e1)
        {
            MessageBox.Show(Page, "errorMessage=" + e1.Message + ";filePath=" + filePath);
            return false;
        }
        return true;
    }

    /// <summary>
    /// 工资数据导入txt
    /// </summary>
    /// <param name="table"></param>
    /// <param name="filePath"></param>
    /// <returns></returns>
    public bool SalaryTableToTxt(DataTable table, string filePath)
    {
        //如果文件不存在，就新建该文件   
        if (!File.Exists(filePath))
        {
            StreamWriter sr = File.CreateText(filePath);
            sr.Close();
        }
        //向文件写入内容   
        StreamWriter sw = new StreamWriter(filePath, true, System.Text.Encoding.Default);
        try
        {
            StringBuilder sb;
            for (int i = 0; i < table.Rows.Count; i++)
            {
                sb = new StringBuilder();
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    sb.Append(table.Rows[i][j].ToString() + ",");
                }
                sb.Remove(sb.Length - 1, 1);//一处最后一个逗号
                sw.WriteLine(sb.ToString());
            }
            sw.Close();
            return true;
        }
        catch (Exception ex)
        {
            sw.Close();
            return false;
        }
    }
}
