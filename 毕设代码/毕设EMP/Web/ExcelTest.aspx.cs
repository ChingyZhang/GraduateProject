using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Office.Interop.Excel;

public partial class ExcelTest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void bt_CreateXLSFile_Click(object sender, EventArgs e)
    {
        object missing = System.Reflection.Missing.Value;
        ApplicationClass ExcelApp = new ApplicationClass();

        try
        {
            Workbook workbook1 = ExcelApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            Worksheet worksheet1 = (Worksheet)workbook1.Worksheets["sheet1"];
            worksheet1.Name = "AAA";
            try
            {
                worksheet1.Cells[1, 1] = "姓名";
                worksheet1.Cells[1, 2] = "性别";
                worksheet1.Cells[1, 3] = "生日";
                worksheet1.Cells[2, 1] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                worksheet1.Cells[2, 2] = "=1+1";

                ExcelApp.DisplayAlerts = false;
                ExcelApp.AlertBeforeOverwriting = false;
                workbook1.SaveAs("D:\\" + tbx_FileName.Text, XlFileFormat.xlExcel8, "", "", false, false, XlSaveAsAccessMode.xlNoChange, 1, false, missing, missing, missing);
            }
            catch { }
            finally
            {
                workbook1.Close(false, missing, missing);

                System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet1);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook1);

                worksheet1 = null;
                workbook1 = null;
            }
        }
        catch { }
        finally
        {
            ExcelApp.Workbooks.Close();
            ExcelApp.Quit();

            System.Runtime.InteropServices.Marshal.ReleaseComObject(ExcelApp);
            ExcelApp = null;
            GC.Collect();
            //GC.WaitForPendingFinalizers();
        }
    }
    protected void bt_OpenXLS_Click(object sender, EventArgs e)
    {
        object missing = System.Reflection.Missing.Value;
        ApplicationClass ExcelApp = new ApplicationClass();

        try
        {
            Workbook workbook1 = ExcelApp.Workbooks.Open("D:\\" + tbx_FileName.Text, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing);

            Response.Write(workbook1.Worksheets.Count.ToString());
            Response.Write("<br/>");
            Worksheet worksheet1 = (Worksheet)workbook1.Worksheets[1];
            Response.Write(worksheet1.Name);
            Response.Write("<br/>");
            try
            {
                Response.Write(((Range)worksheet1.Cells[1, 1]).Text);
                Response.Write("<br/>");
                Response.Write(((Range)worksheet1.Cells[1, 2]).Text);
            }
            catch { }
            finally
            {
                ExcelApp.DisplayAlerts = false;
                ExcelApp.AlertBeforeOverwriting = false;
                workbook1.Close(false, missing, missing);
                ExcelApp.Workbooks.Close();
                ExcelApp.Quit();

                System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet1);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook1);

                worksheet1 = null;
                workbook1 = null;
            }
        }
        catch { }
        finally
        {
            ExcelApp.Workbooks.Close();
            ExcelApp.Quit();

            System.Runtime.InteropServices.Marshal.ReleaseComObject(ExcelApp);
            ExcelApp = null;
            GC.Collect();
            //GC.WaitForPendingFinalizers();
        }
    }
}
