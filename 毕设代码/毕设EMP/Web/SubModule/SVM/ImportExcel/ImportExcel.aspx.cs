using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.CM;
using MCSFramework.BLL.Promotor;
using MCSFramework.BLL.Pub;
using MCSFramework.BLL.SVM;
using MCSFramework.Common;
using MCSFramework.Model.CM;
using MCSFramework.Model.Promotor;
using MCSFramework.Model.Pub;
using MCSFramework.Model.SVM;
using MCSFramework.UD_Control;
using Microsoft.Office.Interop.Excel;


public partial class SubModule_SVM_ImportExcel_ImportExcel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["DataTable-SellIn"] = null;
            Session["DataTable-SellOut"] = null;

            select_Staff.SelectValue = Session["UserID"].ToString();
            select_Staff.SelectText = Session["UserRealName"].ToString();

            #region 获取最迟的销量月份
            int JXCDelayDays = ConfigHelper.GetConfigInt("JXCDelayDays");
            AC_AccountMonth month = new AC_AccountMonthBLL(AC_AccountMonthBLL.GetMonthByDate(DateTime.Now.AddDays(-JXCDelayDays))).Model;
            lb_MonthTitle.Text = month.Name + "(" + month.BeginDate.ToString("yyyy-MM-dd") + "至" + month.EndDate.ToString("yyyy-MM-dd") + ")";
            #endregion
        }
    }
    protected void bt_DownloadTemplate_Click(object sender, EventArgs e)
    {
        #region 获取最迟的销量日期
        int JXCDelayDays = ConfigHelper.GetConfigInt("JXCDelayDays");
        AC_AccountMonth month = new AC_AccountMonthBLL(AC_AccountMonthBLL.GetMonthByDate(DateTime.Now.AddDays(-JXCDelayDays))).Model;
        DateTime day = DateTime.Today < month.EndDate ? DateTime.Today : month.EndDate;
        #endregion

        #region 判断有无选择业代
        if (string.IsNullOrEmpty(select_Staff.SelectValue) || select_Staff.SelectValue == "0")
        {
            MessageBox.Show(this, "对不起，请选择责任业代！");
            return;
        }
        #endregion

        #region 获取业代负责的零售商及所有产品数据
        int staff = int.Parse(select_Staff.SelectValue);
        IList<CM_Client> clientlists = CM_ClientBLL.GetModelList("ClientType=3 AND ClientManager=" + staff.ToString() +
            " AND ActiveFlag=1 AND ApproveFlag=1 AND OpenTime<'" + day.ToString("yyyy-MM-dd") + " 23:59:59' ORDER BY Code");
        if (clientlists.Count == 0)
        {
            MessageBox.Show(this, "对不起，没有当前人直接负责的终端店！");
            return;
        }

        IList<PDT_Product> productlists = PDT_ProductBLL.GetModelList("Brand IN (SELECT ID FROM dbo.PDT_Brand WHERE IsOpponent='1') AND State=1 AND ApproveFlag=1 ORDER BY ISNULL(SubUnit,999999),Code");
        #endregion

        #region 组织文件路径及文件名
        string path = ConfigHelper.GetConfigString("AttachmentPath");
        if (path.StartsWith("~")) path = Server.MapPath(path);
        if (!path.EndsWith("\\")) path = path + "\\";
        path += "ImportExcelSVM\\Download\\";
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);

        string filename = "销量导入模板-" + select_Staff.SelectText + "-" + DateTime.Today.ToString("yyyyMMdd") + ".xls";
        path += filename;
        #endregion

        #region 生成Excel文件
        object missing = System.Reflection.Missing.Value;
        ApplicationClass ExcelApp = null;

        try
        {
            ExcelApp = new ApplicationClass();
            ExcelApp.Visible = false;
            ExcelApp.DisplayAlerts = false;

            Workbook workbook1 = null;
            Worksheet worksheet1 = null, worksheet2 = null;
            try
            {
                workbook1 = ExcelApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
                worksheet1 = (Worksheet)workbook1.Worksheets["sheet1"];
                worksheet2 = (Worksheet)workbook1.Worksheets.Add(missing, worksheet1, 1, missing);

                worksheet1.Name = "零售商进货";
                worksheet2.Name = "零售商销货";

                #region 创建表头
                worksheet1.Cells[1, 1] = "零售商ID";
                worksheet1.Cells[1, 2] = "零售商编号";
                worksheet1.Cells[1, 3] = "零售商名称";
                worksheet1.Cells[1, 4] = "零售商分类";
                worksheet1.Cells[1, 5] = "归属月份";

                worksheet1.get_Range("B2", "B2").ColumnWidth = 15;
                worksheet1.get_Range("C2", "C2").ColumnWidth = 20;
                worksheet1.get_Range("D2", "E2").ColumnWidth = 10;
                worksheet1.get_Range("A1", "A1").RowHeight = 50;
                worksheet1.get_Range("A1", "CC1").WrapText = true;
                worksheet1.get_Range("A1", "CC1").Font.Bold = true;
                worksheet1.get_Range("A1", "CC1000").Font.Size = 9;
                worksheet1.get_Range("A1", "CC1000").HorizontalAlignment = XlHAlign.xlHAlignCenter;

                worksheet2.Cells[1, 1] = "零售商ID";
                worksheet2.Cells[1, 2] = "零售商编号";
                worksheet2.Cells[1, 3] = "零售商名称";
                worksheet2.Cells[1, 4] = "零售商分类";
                worksheet2.Cells[1, 5] = "归属月份";
                worksheet2.Cells[1, 6] = "导购ID";
                worksheet2.Cells[1, 7] = "导购姓名";

                worksheet2.get_Range("B2", "B2").ColumnWidth = 15;
                worksheet2.get_Range("C2", "C2").ColumnWidth = 20;
                worksheet2.get_Range("D2", "G2").ColumnWidth = 10;
                worksheet2.get_Range("A1", "A1").RowHeight = 50;
                worksheet2.get_Range("A1", "CC1").WrapText = true;
                worksheet2.get_Range("A1", "CC1").Font.Bold = true;
                worksheet2.get_Range("A1", "CC1000").Font.Size = 9;
                worksheet2.get_Range("A1", "CC1000").HorizontalAlignment = XlHAlign.xlHAlignCenter;

                int bgcolor1 = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightYellow);
                int bgcolor2 = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGreen);

                //将产品写入表头
                for (int i = 0; i < productlists.Count; i++)
                {
                    worksheet1.Cells[1, 6 + i] = productlists[i].ShortName;
                    worksheet2.Cells[1, 8 + i] = productlists[i].ShortName;

                    #region 按品牌设置产品列颜色
                    if (i > 0)
                    {
                        if (productlists[i].Brand == productlists[i - 1].Brand)
                        {
                            worksheet1.get_Range(worksheet1.Cells[1, 6 + i], worksheet1.Cells[1000, 6 + i]).Interior.Color =
                                worksheet1.get_Range(worksheet1.Cells[1, 6 + i - 1], worksheet1.Cells[1000, 6 + i - 1]).Interior.Color;

                            worksheet2.get_Range(worksheet2.Cells[1, 8 + i], worksheet2.Cells[1000, 8 + i]).Interior.Color =
                                worksheet2.get_Range(worksheet2.Cells[1, 8 + i - 1], worksheet2.Cells[1000, 8 + i - 1]).Interior.Color;
                        }
                        else
                        {
                            if (int.Parse(worksheet1.get_Range(worksheet1.Cells[1, 6 + i - 1], worksheet1.Cells[1000, 6 + i - 1]).Interior.Color.ToString()) == bgcolor1)
                                worksheet1.get_Range(worksheet1.Cells[1, 6 + i], worksheet1.Cells[1000, 6 + i]).Interior.Color = bgcolor2;
                            else
                                worksheet1.get_Range(worksheet1.Cells[1, 6 + i], worksheet1.Cells[1000, 6 + i]).Interior.Color = bgcolor1;

                            if (int.Parse(worksheet2.get_Range(worksheet2.Cells[1, 8 + i - 1], worksheet2.Cells[1000, 8 + i - 1]).Interior.Color.ToString()) == bgcolor1)
                                worksheet2.get_Range(worksheet2.Cells[1, 8 + i], worksheet2.Cells[1000, 8 + i]).Interior.Color = bgcolor2;
                            else
                                worksheet2.get_Range(worksheet2.Cells[1, 8 + i], worksheet2.Cells[1000, 8 + i]).Interior.Color = bgcolor1;
                        }
                    }
                    else
                    {
                        worksheet1.get_Range(worksheet1.Cells[1, 6 + i], worksheet1.Cells[1000, 6 + i]).Interior.Color = bgcolor1;
                        worksheet2.get_Range(worksheet2.Cells[1, 8 + i], worksheet2.Cells[1000, 8 + i]).Interior.Color = bgcolor1;
                    }
                    #endregion
                }
                #endregion

                #region 将零售商信息写入表格内
                int sellinrow = 2, selloutrow = 2;
                foreach (CM_Client client in clientlists)
                {
                    worksheet1.Cells[sellinrow, 1] = client.ID;
                    worksheet1.Cells[sellinrow, 2] = client.Code;
                    worksheet1.Cells[sellinrow, 3] = client.FullName;
                    worksheet1.Cells[sellinrow, 4] = DictionaryBLL.GetDicCollections("CM_RT_Classify")[client["RTClassify"]].Name;
                    worksheet1.Cells[sellinrow, 5] = "'" + month.Name;
                    //worksheet1.Cells[sellinrow, 5] = day.ToString("yyyy-MM-dd");
                    sellinrow++;

                    worksheet2.Cells[selloutrow, 1] = client.ID;
                    worksheet2.Cells[selloutrow, 2] = client.Code;
                    worksheet2.Cells[selloutrow, 3] = client.FullName;
                    worksheet2.Cells[selloutrow, 4] = DictionaryBLL.GetDicCollections("CM_RT_Classify")[client["RTClassify"]].Name;
                    worksheet2.Cells[selloutrow, 5] = "'" + month.Name;
                    //worksheet2.Cells[selloutrow, 5] = day.ToString("yyyy-MM-dd");

                    IList<PM_Promotor> promotorlists = PM_PromotorBLL.GetModelList("ID IN (SELECT Promotor FROM dbo.PM_PromotorInRetailer WHERE Client = " + client.ID.ToString() + ") AND Dimission=1 AND ApproveFlag=1");
                    for (int j = 0; j < promotorlists.Count; j++)
                    {
                        if (j > 0)
                        {
                            worksheet2.Cells[selloutrow, 1] = client.ID;
                            worksheet2.Cells[selloutrow, 2] = client.Code;
                            worksheet2.Cells[selloutrow, 3] = client.FullName;
                            worksheet2.Cells[selloutrow, 4] = DictionaryBLL.GetDicCollections("CM_RT_Classify")[client["RTClassify"]].Name;
                            worksheet2.Cells[selloutrow, 5] = "'" + month.Name;
                            //worksheet2.Cells[selloutrow, 5] = day.ToString("yyyy-MM-dd");
                        }
                        worksheet2.Cells[selloutrow, 6] = promotorlists[j].ID;
                        worksheet2.Cells[selloutrow, 7] = promotorlists[j].Name;

                        if (j != promotorlists.Count - 1) selloutrow++;
                    }
                    selloutrow++;
                }
                #endregion

                #region 设置表格格式
                //设置行高
                worksheet1.get_Range(worksheet1.Cells[2, 1], worksheet1.Cells[sellinrow - 1, 1]).RowHeight = 16;
                worksheet2.get_Range(worksheet2.Cells[2, 1], worksheet2.Cells[selloutrow - 1, 1]).RowHeight = 16;

                //设置表格单元格格线
                worksheet1.get_Range(worksheet1.Cells[1, 1], worksheet1.Cells[sellinrow - 1, 6 + productlists.Count - 1]).Borders.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                worksheet2.get_Range(worksheet2.Cells[1, 1], worksheet2.Cells[selloutrow - 1, 8 + productlists.Count - 1]).Borders.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);

                #endregion

                worksheet1.Activate();
                ExcelApp.AlertBeforeOverwriting = false;
                workbook1.SaveAs(path, XlFileFormat.xlExcel8, "", "", false, false, XlSaveAsAccessMode.xlNoChange, 1, false, missing, missing, missing);
            }
            catch (System.Exception err)
            {
                string error = "Message:" + err.Message + "<br/>" + "Source:" + err.Source + "<br/>" +
                    "StackTrace:" + err.StackTrace + "<br/>";
                lb_ErrorInfo.Text = error;

                MessageBox.Show(this, "系统错误-1!" + err.Message);
            }
            finally
            {
                if (workbook1 != null) workbook1.Close(false, missing, missing);

                if (worksheet1 != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet1);
                if (worksheet2 != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet2);
                if (workbook1 != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook1);

                worksheet1 = null;
                worksheet2 = null;
                workbook1 = null;

                if (File.Exists(path)) Downloadfile(path, filename);
            }
        }
        catch (System.Exception err)
        {
            string error = "Message:" + err.Message + "<br/>" + "Source:" + err.Source + "<br/>" +
                "StackTrace:" + err.StackTrace + "<br/>";
            lb_ErrorInfo.Text = error;

            MessageBox.Show(this, "系统错误-2!" + err.Message);
        }
        finally
        {
            if (ExcelApp != null)
            {
                ExcelApp.Workbooks.Close();
                ExcelApp.Quit();

                System.Runtime.InteropServices.Marshal.ReleaseComObject(ExcelApp);
                ExcelApp = null;
            }
            GC.Collect();
            //GC.WaitForPendingFinalizers();
        }
        #endregion
    }

    private void Downloadfile(string path, string filename)
    {
        try
        {
            Response.Clear();
            Response.BufferOutput = true;
            Response.ContentType = "application/ms-excel";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(filename));
            Response.WriteFile(path);
            Response.Flush();

            File.Delete(path);

            Response.End();
        }
        catch (System.Exception err)
        {
            string error = "Message:" + err.Message + "<br/>" + "Source:" + err.Source + "<br/>" +
                "StackTrace:" + err.StackTrace + "<br/>";
            lb_ErrorInfo.Text = error;

            MessageBox.Show(this, "系统错误-3!" + err.Message);
        }
    }

    protected void bt_UploadExcel_Click(object sender, EventArgs e)
    {
        Session["DataTable-SellIn"] = null;
        Session["DataTable-SellOut"] = null;
        UpdatePanel3.Visible = false;
        bt_Import.Enabled = false;

        #region 保存文件
        if (!FileUpload1.HasFile)
        {
            MessageBox.Show(this.Page, "请选择要上传的文件！");
            return;
        }
        int FileSize = (FileUpload1.PostedFile.ContentLength / 1024);

        if (FileSize > ConfigHelper.GetConfigInt("MaxAttachmentSize"))
        {
            MessageBox.Show(this.Page, "上传的文件不能大于" + ConfigHelper.GetConfigInt("MaxAttachmentSize") +
                "KB!当前上传文件大小为:" + FileSize.ToString() + "KB");
            return;
        }

        //判断文件格式
        string FileName = FileUpload1.PostedFile.FileName;
        FileName = FileName.Substring(FileName.LastIndexOf('\\') + 1);
        FileName = FileName.Substring(0, FileName.LastIndexOf('.'));

        string FileExtName = FileUpload1.PostedFile.FileName.Substring(FileUpload1.PostedFile.FileName.LastIndexOf(".") + 1).ToLowerInvariant();

        if (FileExtName != "xls")
        {
            MessageBox.Show(this, "对不起，必须上传扩展名为xls的Excel文件！");
            return;
        }

        string path = ConfigHelper.GetConfigString("AttachmentPath");
        if (path.StartsWith("~")) path = Server.MapPath(path);
        if (!path.EndsWith("\\")) path = path + "\\";
        path += "ImportExcelSVM\\Upload\\" + Session["UserName"].ToString() + "\\";
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);

        path += FileName + "." + FileExtName;

        FileUpload1.SaveAs(path);
        #endregion

        #region 读取Excel文件
        string ErrorInfo = "";
        lb_ErrorInfo.Text = "";
        object missing = System.Reflection.Missing.Value;
        ApplicationClass ExcelApp = null;
        try
        {
            ExcelApp = new ApplicationClass();
            ExcelApp.Visible = false;
            ExcelApp.DisplayAlerts = false;

            Workbook workbook1 = null;
            Worksheet worksheet1 = null, worksheet2 = null;

            try
            {
                workbook1 = ExcelApp.Workbooks.Open(path, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing);

                #region 验证工作表数据格式
                if (workbook1.Worksheets.Count != 2)
                {
                    ErrorInfo += "Excel表格中必须且只能有2张工作表！";
                    goto End;
                }

                worksheet1 = (Worksheet)workbook1.Worksheets[1];
                worksheet2 = (Worksheet)workbook1.Worksheets[2];

                if (worksheet1.Name != "零售商进货")
                {
                    ErrorInfo += "Excel表格中第1个工作表名称必须为【零售商进货】！";
                    goto End;
                }
                if (worksheet2.Name != "零售商销货")
                {
                    ErrorInfo += "Excel表格中第2个工作表名称必须为【零售商销货】！";
                    goto End;
                }

                if (((Range)worksheet1.Cells[1, 1]).Text.ToString() != "零售商ID" ||
                    ((Range)worksheet1.Cells[1, 2]).Text.ToString() != "零售商编号" ||
                    ((Range)worksheet1.Cells[1, 3]).Text.ToString() != "零售商名称" ||
                    ((Range)worksheet1.Cells[1, 4]).Text.ToString() != "零售商分类" ||
                    ((Range)worksheet1.Cells[1, 5]).Text.ToString() != "归属月份")
                {
                    ErrorInfo += "零售商进货工作表表头(1~5列)错误!";
                    goto End;
                }

                if (((Range)worksheet2.Cells[1, 1]).Text.ToString() != "零售商ID" ||
                    ((Range)worksheet2.Cells[1, 2]).Text.ToString() != "零售商编号" ||
                    ((Range)worksheet2.Cells[1, 3]).Text.ToString() != "零售商名称" ||
                    ((Range)worksheet2.Cells[1, 4]).Text.ToString() != "零售商分类" ||
                    ((Range)worksheet2.Cells[1, 5]).Text.ToString() != "归属月份" ||
                    ((Range)worksheet2.Cells[1, 6]).Text.ToString() != "导购ID" ||
                    ((Range)worksheet2.Cells[1, 7]).Text.ToString() != "导购姓名")
                {
                    ErrorInfo += "零售商销货工作表表头(1~7列)错误!";
                    goto End;
                }


                string ParamValue = Addr_OrganizeCityParamBLL.GetValueByType(1, 26);
                ParamValue = string.IsNullOrEmpty(ParamValue) ? "0" : ParamValue;
                ParamValue = ParamValue.EndsWith(",") ? ParamValue.Remove(ParamValue.Length - 1) : ParamValue;

                IList<PDT_Product> productlists = PDT_ProductBLL.GetModelList(" Brand NOT IN(" + ParamValue + ") AND  Brand IN (SELECT ID FROM dbo.PDT_Brand WHERE IsOpponent='1') AND State=1 AND ApproveFlag=1 ORDER BY ISNULL(SubUnit,999999),Code");
                for (int i = 0; i < productlists.Count; i++)
                {
                    if (((Range)worksheet1.Cells[1, 6 + i]).Text.ToString() != productlists[i].ShortName)
                    {
                        ErrorInfo += "零售商进货工作表表头，(" + (6 + i).ToString() + "列)产品名称错误！必须为:" + productlists[i].ShortName;
                        goto End;
                    }
                    if (((Range)worksheet2.Cells[1, 8 + i]).Text.ToString() != productlists[i].ShortName)
                    {
                        ErrorInfo += "零售商进货工作表表头，(" + (8 + i).ToString() + "列)产品名称错误！必须为:" + productlists[i].ShortName;
                        goto End;
                    }
                }
                #endregion

                #region 创建DataTable
                System.Data.DataTable dtSellIn = new System.Data.DataTable();
                dtSellIn.Columns.Add(new System.Data.DataColumn("序号", Type.GetType("System.Int32")));
                dtSellIn.Columns.Add(new System.Data.DataColumn("零售商ID", Type.GetType("System.Int32")));
                dtSellIn.Columns.Add(new System.Data.DataColumn("零售商编号", Type.GetType("System.String")));
                dtSellIn.Columns.Add(new System.Data.DataColumn("零售商名称", Type.GetType("System.String")));
                dtSellIn.Columns.Add(new System.Data.DataColumn("零售商分类", Type.GetType("System.String")));
                dtSellIn.Columns.Add(new System.Data.DataColumn("归属月份", Type.GetType("System.String")));

                System.Data.DataTable dtSellOut = new System.Data.DataTable();
                dtSellOut.Columns.Add(new System.Data.DataColumn("序号", Type.GetType("System.Int32")));
                dtSellOut.Columns.Add(new System.Data.DataColumn("零售商ID", Type.GetType("System.Int32")));
                dtSellOut.Columns.Add(new System.Data.DataColumn("零售商编号", Type.GetType("System.String")));
                dtSellOut.Columns.Add(new System.Data.DataColumn("零售商名称", Type.GetType("System.String")));
                dtSellOut.Columns.Add(new System.Data.DataColumn("零售商分类", Type.GetType("System.String")));
                dtSellOut.Columns.Add(new System.Data.DataColumn("归属月份", Type.GetType("System.String")));
                dtSellOut.Columns.Add(new System.Data.DataColumn("导购ID", Type.GetType("System.Int32")));
                dtSellOut.Columns.Add(new System.Data.DataColumn("导购姓名", Type.GetType("System.String")));

                foreach (PDT_Product product in productlists)
                {
                    dtSellIn.Columns.Add(new System.Data.DataColumn("[" + product.ShortName + "]", Type.GetType("System.Int32")));
                    dtSellOut.Columns.Add(new System.Data.DataColumn("[" + product.ShortName + "]", Type.GetType("System.Int32")));
                }
                #endregion

                #region 读取Excel表格--零售商进货
                int sellinrow = 1;
                int emptyrow = 0;
                while (true)
                {
                    sellinrow++;
                    if (((Range)worksheet1.Cells[sellinrow, 1]).Text.ToString() == string.Empty)
                    {
                        emptyrow++;
                        if (emptyrow > 5)
                            break;
                        else
                            continue;
                    }

                    int clientid = 0;
                    if (!int.TryParse(((Range)worksheet1.Cells[sellinrow, 1]).Text.ToString(), out clientid))
                    {
                        continue;
                    }
                    //DateTime selldate;
                    //if (!DateTime.TryParse(((Range)worksheet1.Cells[sellinrow, 5]).Text.ToString(), out selldate))
                    //{
                    //    continue;
                    //}

                    System.Data.DataRow row = dtSellIn.NewRow();
                    row[0] = sellinrow - 1;
                    row[1] = clientid;
                    row[2] = ((Range)worksheet1.Cells[sellinrow, 2]).Text.ToString();
                    row[3] = ((Range)worksheet1.Cells[sellinrow, 3]).Text.ToString();
                    row[4] = ((Range)worksheet1.Cells[sellinrow, 4]).Text.ToString();
                    row[5] = ((Range)worksheet1.Cells[sellinrow, 5]).Text.ToString();

                    for (int i = 0; i < productlists.Count; i++)
                    {
                        int quantity = 0;
                        int.TryParse(((Range)worksheet1.Cells[sellinrow, 6 + i]).Text.ToString(), out quantity);

                        row[6 + i] = quantity;
                    }

                    dtSellIn.Rows.Add(row);
                }

                Session["DataTable-SellIn"] = dtSellIn;
                #endregion

                #region 读取Excel表格--零售商销货
                int selloutrow = 1;
                emptyrow = 0;
                while (true)
                {
                    selloutrow++;
                    if (((Range)worksheet2.Cells[selloutrow, 1]).Text.ToString() == string.Empty)
                    {
                        emptyrow++;
                        if (emptyrow > 5)
                            break;
                        else
                            continue;
                    }

                    int clientid = 0;
                    if (!int.TryParse(((Range)worksheet2.Cells[selloutrow, 1]).Text.ToString(), out clientid))
                    {
                        continue;
                    }
                    //DateTime selldate;
                    //if (!DateTime.TryParse(((Range)worksheet2.Cells[selloutrow, 5]).Text.ToString(), out selldate))
                    //{
                    //    continue;
                    //}
                    int promotorid = 0;
                    if (((Range)worksheet2.Cells[selloutrow, 6]).Text.ToString() != "" &&
                        !int.TryParse(((Range)worksheet2.Cells[selloutrow, 6]).Text.ToString(), out promotorid))
                    {
                        continue;
                    }

                    System.Data.DataRow row = dtSellOut.NewRow();
                    row[0] = selloutrow - 1;
                    row[1] = clientid;
                    row[2] = ((Range)worksheet2.Cells[selloutrow, 2]).Text.ToString();
                    row[3] = ((Range)worksheet2.Cells[selloutrow, 3]).Text.ToString();
                    row[4] = ((Range)worksheet2.Cells[selloutrow, 4]).Text.ToString();
                    row[5] = ((Range)worksheet2.Cells[selloutrow, 5]).Text.ToString();
                    row[6] = promotorid;
                    row[7] = ((Range)worksheet2.Cells[selloutrow, 7]).Text.ToString();

                    for (int i = 0; i < productlists.Count; i++)
                    {
                        int quantity = 0;
                        int.TryParse(((Range)worksheet2.Cells[selloutrow, 8 + i]).Text.ToString(), out quantity);

                        row[8 + i] = quantity;
                    }

                    dtSellOut.Rows.Add(row);
                }

                Session["DataTable-SellIn"] = dtSellIn;
                Session["DataTable-SellOut"] = dtSellOut;
                #endregion


            End:
                ;
            }
            catch (System.Exception err)
            {
                string error = "Message:" + err.Message + "<br/>" + "Source:" + err.Source + "<br/>" +
                    "StackTrace:" + err.StackTrace + "<br/>";
                lb_ErrorInfo.Text = error;

                MessageBox.Show(this, "系统错误-4!" + err.Message);
            }
            finally
            {
                if (workbook1 != null) workbook1.Close(false, missing, missing);

                if (worksheet1 != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet1);
                if (worksheet2 != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet2);
                if (workbook1 != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook1);

                worksheet1 = null;
                worksheet2 = null;
                workbook1 = null;
            }
        }
        catch (System.Exception err)
        {
            string error = "Message:" + err.Message + "<br/>" + "Source:" + err.Source + "<br/>" +
                "StackTrace:" + err.StackTrace + "<br/>";
            lb_ErrorInfo.Text = error;

            MessageBox.Show(this, "系统错误-5!" + err.Message);
        }
        finally
        {
            if (ExcelApp != null)
            {
                ExcelApp.Workbooks.Close();
                ExcelApp.Quit();

                System.Runtime.InteropServices.Marshal.ReleaseComObject(ExcelApp);
                ExcelApp = null;
            }
            GC.Collect();
            //GC.WaitForPendingFinalizers();

            if (ErrorInfo != "")
            {
                lb_ErrorInfo.Text = ErrorInfo;
                MessageBox.Show(this, "对不起，Excel文件打开错误，请确认格式是否正确。错误提示:" + ErrorInfo);
                bt_Import.Enabled = false;
            }
            else
            {
                BindGrid();
                MessageBox.Show(this, "Excel文件上传并成功读取，请确认表格中的数据，无误后点【确认提交】按钮！");
                bt_Import.Enabled = true;
            }

        }
        #endregion
    }

    protected void MCSTabControl1_OnTabClicked(object sender, MCSControls.MCSTabControl.MCSTabClickedEventArgs e)
    {
        BindGrid();
    }

    private void BindGrid()
    {
        UpdatePanel3.Visible = true;
        gv_SellIn.Visible = MCSTabControl1.SelectedIndex == 0;
        gv_SellOut.Visible = !gv_SellIn.Visible;

        if (Session["DataTable-SellIn"] != null && Session["DataTable-SellOut"] != null)
        {
            if (gv_SellIn.Visible)
            {
                System.Data.DataTable dtSellIn = (System.Data.DataTable)Session["DataTable-SellIn"];
                gv_SellIn.DataSource = dtSellIn;
                gv_SellIn.DataBind();
            }

            if (gv_SellOut.Visible)
            {
                System.Data.DataTable dtSellOut = (System.Data.DataTable)Session["DataTable-SellOut"];
                gv_SellOut.DataSource = dtSellOut;
                gv_SellOut.DataBind();
            }
        }
    }
    protected void gv_Sell_DataBound(object sender, EventArgs e)
    {
        UC_GridView gv = (UC_GridView)sender;
        foreach (GridViewRow row in gv.Rows)
        {
            //row.Cells[6].Text = row.Cells[6].Text.Replace(" 0:00:00", "");

            for (int i = 0; i < row.Cells.Count; i++)
            {
                if (row.Cells[i].Text == "0") row.Cells[i].Text = "";
            }
        }
        gv.Width = new Unit(70 * gv.Rows[0].Cells.Count);
    }
    protected void gv_SellIn_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (Session["DataTable-SellIn"] != null)
        {
            System.Data.DataTable dtSellIn = (System.Data.DataTable)Session["DataTable-SellIn"];
            dtSellIn.Rows.RemoveAt(e.RowIndex);
            BindGrid();
        }
    }
    protected void gv_SellOut_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (Session["DataTable-SellOut"] != null)
        {
            System.Data.DataTable dtSellOut = (System.Data.DataTable)Session["DataTable-SellOut"];
            dtSellOut.Rows.RemoveAt(e.RowIndex);
            BindGrid();
        }
    }
    protected void bt_Import_Click(object sender, EventArgs e)
    {
        string ImportInfo = "";
        lb_ErrorInfo.Text = "";

        System.Data.DataTable dtSellIn = null, dtSellOut = null;

        if (Session["DataTable-SellIn"] != null && Session["DataTable-SellOut"] != null)
        {
            dtSellIn = (System.Data.DataTable)Session["DataTable-SellIn"];
            dtSellOut = (System.Data.DataTable)Session["DataTable-SellOut"];
        }

        #region 获取允许最迟的销量日期
        int JXCDelayDays = ConfigHelper.GetConfigInt("JXCDelayDays");
        AC_AccountMonth month = new AC_AccountMonthBLL(AC_AccountMonthBLL.GetMonthByDate(DateTime.Now.AddDays(-JXCDelayDays))).Model;
        DateTime minday = month.BeginDate;
        DateTime maxday = DateTime.Today < month.EndDate ? DateTime.Today : month.EndDate;
        #endregion

        IList<PDT_Product> productlists = PDT_ProductBLL.GetModelList("Brand IN (SELECT ID FROM dbo.PDT_Brand WHERE IsOpponent='1') AND State=1 AND ApproveFlag=1 ORDER BY ISNULL(SubUnit,999999),Code");

        ImportInfo += "<span style='color: Red'>-------------------------------------------------------------------------</span><br/>";
        ImportInfo += "<span style='color: Red'>----批量导入零售商进货销量----</span><br/>";

        #region 开始导入零售商进货
        foreach (System.Data.DataRow dr in dtSellIn.Rows)
        {
            #region 验证数据
            int clientid = (int)dr["零售商ID"];
            CM_Client client = new CM_ClientBLL(clientid).Model;
            if (client == null || client.FullName != (string)dr["零售商名称"])
            {
                ImportInfo += "<span style='color: Red'>序号：" + dr["序号"].ToString() + "，零售商ID与零售商名称不匹配！</span><br/>";
                continue;
            }

            if (dr["归属月份"].ToString() != month.Name)
            {
                ImportInfo += "<span style='color: Red'>序号：" + dr["序号"].ToString() + "，归属月份必须为【" + month.Name + "】</span><br/>";
                continue;
            }
            //DateTime salesdate = (DateTime)dr["进货日期"];
            //if (salesdate < minday || salesdate > maxday)
            //{
            //    ImportInfo += "<span style='color: Red'>序号：" + dr["序号"].ToString() + "，进货日期必须在" + minday.ToString("yyyy-MM-dd") +
            //        "至" + maxday.ToString("yyyy-MM-dd") + "之间！</span><br/>";
            //    continue;
            //}
            #endregion

            #region 组织销量头
            SVM_SalesVolumeBLL bll = null;
            IList<SVM_SalesVolume> svmlists = SVM_SalesVolumeBLL.GetModelList("Client=" + clientid.ToString()
                + " AND Type=2 AND AccountMonth=" + month.ID.ToString() //+ " AND SalesDate='" + salesdate.ToString("yyyy-MM-dd")
                + " AND MCS_SYS.dbo.UF_Spilt(ExtPropertys,'|',1)='N' ");
            if (svmlists.Count > 0)
            {
                if (svmlists[0].ApproveFlag == 1)
                {
                    ImportInfo += "<span style='color: Red'>序号：" + dr["序号"].ToString() + "，该零售商当月的进货已审核，不可再次导入！</span><br/>";
                    continue;
                }

                bll = new SVM_SalesVolumeBLL(svmlists[0].ID);
                bll.Items.Clear();
            }
            else
            {
                bll = new SVM_SalesVolumeBLL();
                bll.Model.Client = clientid;
                bll.Model.OrganizeCity = client.OrganizeCity;
                bll.Model.Supplier = client.Supplier;
                bll.Model.AccountMonth = month.ID;
                bll.Model.SalesDate = maxday;
                bll.Model.Type = 2;
                bll.Model.ApproveFlag = 2;
                bll.Model.Flag = 1;             //成品销售
                bll.Model["SubmitFlag"] = "1";
                bll.Model["IsCXP"] = "N";
                bll.Model.InsertStaff = (int)Session["UserID"];
                bll.Model.Remark = "Excel批量导入";
            }
            #endregion

            #region 读取各产品销量
            IList<SVM_SalesVolume_Detail> details = new List<SVM_SalesVolume_Detail>();
            foreach (PDT_Product product in productlists)
            {
                int quantity = (int)dr["[" + product.ShortName + "]"];
                if (quantity != 0)
                {
                    decimal factoryprice = 0, salesprice = 0;
                    PDT_ProductPriceBLL.GetPriceByClientAndType(client.ID, product.ID, 2, out factoryprice, out salesprice);

                    if (factoryprice == 0) factoryprice = product.FactoryPrice;
                    if (salesprice == 0) salesprice = product.NetPrice;

                    SVM_SalesVolume_Detail detail = new SVM_SalesVolume_Detail();
                    detail.Product = product.ID;
                    detail.FactoryPrice = factoryprice;
                    detail.SalesPrice = salesprice;
                    detail.Quantity = quantity;

                    details.Add(detail);
                }
            }
            #endregion

            #region 更新销量至数据库
            if (bll.Model.ID > 0)
            {
                if (details.Count > 0)
                {
                    bll.DeleteDetail();     //先清除原先导入的数据

                    bll.Items = details;
                    bll.AddDetail();
                    bll.Update();

                    ImportInfo += "<span style='color: Blue'>序号：" + dr["序号"].ToString() + "，零售商：" + client.FullName
                        + " 的原有日期为：" + bll.Model.SalesDate.ToString("yyyy-MM-dd") + " 的进货单被成功更新！产品SKU数："
                        + bll.Items.Count.ToString() + "，产品总数量：" + bll.Items.Sum(p => p.Quantity).ToString() + "</span><br/>";
                }
            }
            else
            {
                //if (details.Count > 0)    //没有产品也新增一条空销量头
                {
                    bll.Items = details;
                    bll.Add();
                    ImportInfo += "<span style='color: Black'>序号：" + dr["序号"].ToString() + "，零售商：" + client.FullName
                        + " 的进货单已成功导入！产品SKU数：" + bll.Items.Count.ToString() + "，产品总数量："
                        + bll.Items.Sum(p => p.Quantity).ToString() + "</span><br/>";
                }
            }
            #endregion
        }
        #endregion

        ImportInfo += "<br/><br/>";
        ImportInfo += "<span style='color: Red'>-------------------------------------------------------------------------</span><br/>";
        ImportInfo += "<span style='color: Red'>----批量导入零售商销货销量----</span><br/>";

        #region 开始导入零售商销货
        foreach (System.Data.DataRow dr in dtSellOut.Rows)
        {
            #region 验证数据
            int clientid = (int)dr["零售商ID"];
            CM_Client client = new CM_ClientBLL(clientid).Model;
            if (client == null || client.FullName != (string)dr["零售商名称"])
            {
                ImportInfo += "<span style='color: Red'>序号：" + dr["序号"].ToString() + "，零售商ID与零售商名称不匹配！</span><br/>";
                continue;
            }

            if (dr["归属月份"].ToString() != month.Name)
            {
                ImportInfo += "<span style='color: Red'>序号：" + dr["序号"].ToString() + "，归属月份必须为【" + month.Name + "】</span><br/>";
                continue;
            }
            //DateTime salesdate = (DateTime)dr["销售日期"];
            //if (salesdate < minday || salesdate > maxday)
            //{
            //    ImportInfo += "<span style='color: Red'>序号：" + dr["序号"].ToString() + "，销售日期必须在" + minday.ToString("yyyy-MM-dd") +
            //        "至" + maxday.ToString("yyyy-MM-dd") + "之前！</span><br/>";
            //    continue;
            //}

            int promotorid = (int)dr["导购ID"];
            if (promotorid > 0)
            {
                if (PM_PromotorInRetailerBLL.GetModelList("Client=" + client.ID.ToString() +
                    " AND Promotor=" + promotorid.ToString()).Count == 0)
                {
                    ImportInfo += "<span style='color: Red'>序号：" + dr["序号"].ToString() + "，零售商中没有关联该导购员！</span><br/>";
                    continue;
                }
            }
            #endregion

            #region 组织销量头
            SVM_SalesVolumeBLL bll = null;
            IList<SVM_SalesVolume> svmlists = SVM_SalesVolumeBLL.GetModelList("Supplier=" + clientid.ToString()
                + " AND Type=3 AND AccountMonth=" + month.ID.ToString() //+ " AND SalesDate='" + salesdate.ToString("yyyy-MM-dd")
                + " AND MCS_SYS.dbo.UF_Spilt(ExtPropertys,'|',1)='N' AND ISNULL(Promotor,0)=" + promotorid.ToString());
            if (svmlists.Count > 0)
            {
                if (svmlists[0].ApproveFlag == 1)
                {
                    ImportInfo += "<span style='color: Red'>序号：" + dr["序号"].ToString() + "，该零售商当月的销量已审核，不可再次导入！</span><br/>";
                    continue;
                }

                bll = new SVM_SalesVolumeBLL(svmlists[0].ID);
                bll.Model["SubmitFlag"] = "1";
                bll.Items.Clear();
            }
            else
            {
                bll = new SVM_SalesVolumeBLL();
                bll.Model.Client = 0;
                bll.Model.OrganizeCity = client.OrganizeCity;
                bll.Model.Supplier = client.ID;
                bll.Model.Promotor = promotorid;
                bll.Model.AccountMonth = month.ID;
                bll.Model.SalesDate = maxday;
                bll.Model.Type = 3;
                bll.Model.ApproveFlag = 2;
                bll.Model.Flag = 1;             //成品销售
                bll.Model["SubmitFlag"] = "1";
                bll.Model["IsCXP"] = "N";
                bll.Model.InsertStaff = (int)Session["UserID"];
                bll.Model.Remark = "Excel批量导入";
            }
            #endregion

            #region 读取各产品销量
            IList<SVM_SalesVolume_Detail> details = new List<SVM_SalesVolume_Detail>();
            foreach (PDT_Product product in productlists)
            {
                int quantity = (int)dr["[" + product.ShortName + "]"];
                if (quantity != 0)
                {
                    decimal factoryprice = 0, salesprice = 0;
                    PDT_ProductPriceBLL.GetPriceByClientAndType(client.ID, product.ID, 3, out factoryprice, out salesprice);

                    if (factoryprice == 0) factoryprice = product.FactoryPrice;
                    if (salesprice == 0) salesprice = product.StdPrice;

                    SVM_SalesVolume_Detail detail = new SVM_SalesVolume_Detail();
                    detail.Product = product.ID;
                    detail.FactoryPrice = factoryprice;
                    detail.SalesPrice = salesprice;
                    detail.Quantity = quantity;

                    details.Add(detail);
                }
            }
            #endregion

            #region 更新销量至数据库
            if (bll.Model.ID > 0)
            {
                if (details.Count > 0)
                {
                    bll.DeleteDetail();     //先清除原先导入的数据

                    bll.Items = details;
                    bll.AddDetail();
                    bll.Update();

                    ImportInfo += "<span style='color: Blue'>序号：" + dr["序号"].ToString() + "，零售商：" + client.FullName
                        + " 的原有日期为：" + bll.Model.SalesDate.ToString("yyyy-MM-dd") + " 的销量单被成功更新！产品SKU数："
                        + bll.Items.Count.ToString() + "，产品总数量：" + bll.Items.Sum(p => p.Quantity).ToString() + "</span><br/>";
                }
            }
            else
            {
                //if (details.Count > 0)    //没有产品也新增一条空销量头
                {
                    bll.Items = details;
                    bll.Add();
                    ImportInfo += "<span style='color: Black'>序号：" + dr["序号"].ToString() + "，零售商：" + client.FullName
                        + " 的销量单已成功导入！产品SKU数：" + bll.Items.Count.ToString() + "，产品总数量："
                        + bll.Items.Sum(p => p.Quantity).ToString() + "</span><br/>";
                }
            }
            #endregion
        }
        #endregion

        lb_ErrorInfo.Text = ImportInfo;
        bt_Import.Enabled = false;

        Session["DataTable-SellIn"] = null;
        Session["DataTable-SellOut"] = null;
    }
}
