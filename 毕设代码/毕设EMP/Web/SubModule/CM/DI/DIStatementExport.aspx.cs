using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCSFramework.BLL;
using MCSFramework.BLL.Pub;
using System.Data;
using MCSFramework.Common;
using System.IO;
//PDF命名空间
using iTextSharp.text;
using iTextSharp.text.pdf;
using MCSFramework.BLL.CM;
using System.Text;
using MCSFramework.Model.CM;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Checksums;
using System.Collections;

public partial class SubModule_CM_DI_DIStatementExport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDropDown();
        }
    }

    #region 绑定下拉列表框
    private void BindDropDown()
    {
        //select_Client.SelectValue = client.Model.ID.ToString();
        //select_Client.SelectText = client.Model.FullName;

        ddl_AccountMonth.DataSource = AC_AccountMonthBLL.GetModelList("Year>=" + (DateTime.Today.Year - 1).ToString());
        ddl_AccountMonth.DataBind();
        ddl_AccountMonth.SelectedValue = (AC_AccountMonthBLL.GetCurrentMonth() - 1).ToString();

        ddl_AccountMonthEnd.DataSource = AC_AccountMonthBLL.GetModelList("Year>=" + (DateTime.Today.Year - 1).ToString());
        ddl_AccountMonthEnd.DataBind();
        ddl_AccountMonthEnd.SelectedValue = AC_AccountMonthBLL.GetCurrentMonth().ToString();

    }
    #endregion

    protected void bt_Refresh_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Select_Client_Begin.SelectValue) || string.IsNullOrEmpty(Select_Client_End.SelectValue))
        {
            MessageBox.Show(this, "请选择经销商范围");
            return;
        }
        int beginClinetID = int.Parse(Select_Client_Begin.SelectValue);
        int endClinetID = int.Parse(Select_Client_End.SelectValue);


        DateTime dateBegin = DateTime.ParseExact(ddl_AccountMonth.SelectedItem.Text, "yyyy-MM", null);
        DateTime dateEnd = DateTime.ParseExact(ddl_AccountMonthEnd.SelectedItem.Text, "yyyy-MM", null);
        if (dateBegin.CompareTo(dateEnd) > 0)
        {
            DateTime dateTemp = dateEnd;
            dateEnd = dateBegin;
            dateBegin = dateTemp;
        }

        StringBuilder organizeCityStr = new StringBuilder();
        #region 绑定用户可管辖的管理片区
        if ((int)Session["AccountType"] == 1)//账户类型 1：员工，2：商业客户 3:导购
        {
            Org_StaffBLL staff = new Org_StaffBLL((int)Session["UserID"]);
            DataTable dt = staff.GetStaffOrganizeCity();
            foreach (DataRow row in dt.Rows)
            {
                organizeCityStr.Append(row["ID"].ToString() + ",");
            }

            //获取当前员工的关联经销商
            int _relateclient = 0;
            if (staff.Model["RelateClient"] != "" && int.TryParse(staff.Model["RelateClient"], out _relateclient))
            {
                organizeCityStr.Append(_relateclient.ToString() + ",");
            }
        }
        else if ((int)Session["AccountType"] == 2)
        {
            CM_Client client = new CM_ClientBLL((int)Session["UserID"]).Model;
            if (client != null)
            {
                Addr_OrganizeCityBLL citybll = new Addr_OrganizeCityBLL(client.OrganizeCity);
                DataTable dt = citybll.GetAllChildNodeIncludeSelf();
                foreach (DataRow row in dt.Rows)
                {
                    organizeCityStr.Append(row["ID"].ToString() + ",");
                }
            }
        }
        if (organizeCityStr.Length > 0)//移除最后一处的逗号
        {
            organizeCityStr = organizeCityStr.Remove(organizeCityStr.Length - 1, 1);
        }

        #endregion

        string beginClinetCode = new CM_ClientBLL(beginClinetID).Model.Code;
        string endClinetCode = new CM_ClientBLL(endClinetID).Model.Code;
        string connStr = " ApproveFlag=1 AND ActiveFlag=1  AND ClientType=2 AND Code BETWEEN '" + beginClinetCode + "' AND '" + endClinetCode + "'  AND MCS_SYS.dbo.UF_Spilt(ExtPropertys,'|',7)!='2'";
        if (!organizeCityStr.ToString().Contains("1,"))
        {
            connStr += " AND OrganizeCity IN(" + organizeCityStr + ") ";
        }
        IList<CM_Client> clientList = CM_ClientBLL.GetModelList(connStr);

        string folderPath = GetPdfFolder();
        foreach (var client in clientList)
        {
            this.CreateClientPDF(folderPath, "[" + client.Code + "]" + client.FullName + ".pdf", client, dateBegin, dateEnd);
        }
        string zipPath = folderPath.Remove(folderPath.LastIndexOf("\\")) + ".zip";
        ZipDir(folderPath, zipPath, 9);
        DownLoadFile(zipPath);
        //删除临时文件夹及文件
        File.Delete(zipPath);
        Directory.Delete(folderPath, true);
    }

    #region ZIP文件下载
    /// <summary>
    /// ZIP文件下载
    /// </summary>
    /// <param name="filePath"></param>
    public void DownLoadFile(string filePath)
    {
        //获取文件名（包括文件类型）
        string fileName = filePath.Remove(0, filePath.LastIndexOf("\\") + 1);

        Response.Clear();
        Response.BufferOutput = true;
        Response.Charset = "GB2312";
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
        Response.ContentType = "application/zip";
        Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
        if (File.Exists(filePath))
        {
            Response.WriteFile(filePath);//通知浏览器下载文件
        }
        Response.Flush();
        //Response.End();
        /*
        FileInfo fileInfo = new FileInfo(filePath);
        Response.Clear();
        Response.ClearContent();
        Response.ClearHeaders();
        Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
        Response.AddHeader("Content-Length", fileInfo.Length.ToString());
        Response.AddHeader("Content-Transfer-Encoding", "binary");
        Response.ContentType = "application/octet-stream";
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");
        Response.WriteFile(fileInfo.FullName);
        //Response.Flush();
         */
    }
    #endregion

    #region 获取PDF文件夹存放路径
    /// <summary>
    /// 获取PDF文件存放路径
    /// </summary>
    /// <returns></returns>
    public string GetPdfFolder()
    {
        //获取文件夹路径并创建
        string folderPath = ConfigHelper.GetConfigString("AttachmentPath");
        if (folderPath.StartsWith("~")) folderPath = Server.MapPath(folderPath);
        if (!folderPath.EndsWith("\\")) folderPath = folderPath + "\\";
        folderPath += "StatementPDFDownload" + DateTime.Now.ToString("yyyyMMddHHmmss") + "\\";
        if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
        return folderPath;
    }
    #endregion

    #region 获取中文字体

    public iTextSharp.text.pdf.BaseFont GetPdfBaseFont()
    {
        //中文字体：SIMHEI.TTF；SIMLI.TTF
        string fontPath = Environment.GetEnvironmentVariable("WINDIR") + "\\FONTS\\SIMHEI.TTF";//强制中文字体，否则无法显示中文,@"c:\windows\fonts\kaiu.ttf"
        BaseFont baseFont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
        return baseFont;
    }

    /// <summary>
    /// 获取中文字体
    /// </summary>
    /// <returns></returns>
    public iTextSharp.text.Font GetPdfFont()
    {
        iTextSharp.text.Font font = new Font(GetPdfBaseFont(), 9);//new iTextSharp.text.Font(baseFont, 14, Font.NORMAL, BaseColor.BLACK);
        return font;
    }

    #endregion

    #region 读取或创建Pdf文档
    /// <summary>
    /// 读取或创建Pdf文档并打开写入文件流
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="folderPath"></param>
    public Document GetOrCreatePDF(string fileName, string folderPath)
    {
        string filePath = folderPath + fileName;
        FileStream fs = null;
        if (!File.Exists(filePath))
        {
            fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
        }
        else
        {
            fs = new FileStream(filePath, FileMode.Append, FileAccess.Write, FileShare.None);
        }
        //获取A4纸尺寸
        Rectangle rec = new Rectangle(PageSize.A4);
        Document doc = new Document(rec);
        //创建一个 iTextSharp.text.pdf.PdfWriter 对象: 它有助于把Document书写到特定的FileStream:
        PdfWriter writer = PdfWriter.GetInstance(doc, fs);
        doc.AddTitle(fileName.Remove(fileName.LastIndexOf('.')));
        doc.AddSubject(fileName.Remove(fileName.LastIndexOf('.')));
        doc.AddKeywords("Metadata, iTextSharp 5.4.4, Chapter 1, Tutorial");
        doc.AddCreator("MCS");
        doc.AddAuthor("Chingy");
        doc.AddHeader("Nothing", "No Header");
        //打开 Document:
        doc.Open();
        ////关闭 Document:
        //doc.Close();
        return doc;
    }
    #endregion

    #region 将DataTable转换为PDF的Table格式
    /// <summary>
    /// 将DataTable转换为PDF的Table格式
    /// </summary>
    /// <param name="dataTable">需要转换的Table</param>
    /// <param name="tableHeaders">转换出的Table表头</param>
    /// <returns></returns>
    public PdfPTable TableToPDFTable(DataTable dataTable)//, List<string> tableHeaders)
    {
        iTextSharp.text.Font font = GetPdfFont();

        if (dataTable == null || dataTable.Columns.Count == 0 || dataTable.Rows.Count == 0)
        {
            return null;
        }

        int columnCount = dataTable.Columns.Count;
        int rowCount = dataTable.Rows.Count;

        PdfPTable table = new PdfPTable(columnCount);
        //设置表格
        table.TotalWidth = 520f;
        table.LockedWidth = true;

        //table.SpacingBefore = 10f;
        //table.SpacingAfter = 10f;
        table.HorizontalAlignment = Element.ALIGN_LEFT;

        PdfPCell cell;

        for (int i = 0; i < columnCount; i++)
        {
            string cellText = dataTable.Columns[i].ColumnName;
            cell = new PdfPCell(new Phrase(cellText, font));
            table.AddCell(cell);
        }
        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < columnCount; j++)
            {
                string cellText = dataTable.Rows[i][j] == null ? string.Empty : dataTable.Rows[i][j].ToString();
                bool flag = dataTable.Columns[j].DataType == Type.GetType("System.Decimal");
                if (flag)
                    cellText = dataTable.Rows[i][j] == null ? string.Empty : string.Format("{0:0,0.00}", dataTable.Rows[i][j]);
                cell = new PdfPCell(new Phrase(cellText, font));
                if (flag)
                    cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                table.AddCell(cell);
            }
        }
        return table;
    }

    #endregion

    #region 获取单据头
    /// <summary>
    /// 获取单据头
    /// </summary>
    /// <param name="document"></param>
    /// <param name="documentTitle"></param>
    /// <param name="documentDate"></param>
    /// <param name="client"></param>
    /// <returns></returns>
    public Document GetDocumentHeader(Document document, string documentTitle, string documentDate, CM_Client client)
    {
        Font font = GetPdfFont();

        Paragraph paragraph = null;

        paragraph = new Paragraph(documentTitle, new Font(GetPdfBaseFont(), 16));
        paragraph.Alignment = iTextSharp.text.Element.TITLE;
        document.Add(paragraph);

        paragraph.Clear();
        paragraph.Font = GetPdfFont();

        string str1 = "经销商：";
        str1 += client == null || string.IsNullOrEmpty(client.Code) ? string.Empty : " [" + client.Code + "] " + client.FullName;
        str1 += "           期间项：" + documentDate;
        Chunk chunk1 = new Chunk(str1, font);
        paragraph.Add(chunk1);
        paragraph.Alignment = Element.ALIGN_LEFT;
        document.Add(paragraph);

        paragraph.Clear();

        string str2 = "打印时间：" + DateTime.Now.ToString("yyyy-MM-dd") + "                           币别：人民币";
        paragraph.Add(new Chunk(str2, font));
        document.Add(paragraph);

        return document;
    }
    #endregion

    #region 获取文档加注文字
    /// <summary>
    /// 获取文档加注文字
    /// </summary>
    /// <param name="document"></param>
    /// <returns></returns>
    public Document GetDocumentNotice(Document document)
    {
        Font font = GetPdfFont();
        Paragraph paragraph = new Paragraph();
        paragraph.Alignment = Element.ALIGN_CENTER;
        // paragraph.Add(new Chunk("注：", font));
        List list = new List(true, 10);
        //list.Numbered = true;        
        list.Add(new iTextSharp.text.ListItem("经销商必须盖章（只限于盖公章或财务专用章）并签名确认。", font));
        list.Add(new iTextSharp.text.ListItem("期末余额正数表示经销商已垫付的市场费用，期末余额负数表示多返还经销商的市场费用。", font));
        list.Add(new iTextSharp.text.ListItem("如有疑问请与我司会计联系。", font));

        paragraph.Add(list);
        document.Add(paragraph);
        return document;
    }

    public Document GetDocumentNotice(Document document, List<string> strList)
    {
        Font font = GetPdfFont();
        Paragraph paragraph = new Paragraph();
        paragraph.Alignment = Element.ALIGN_CENTER;
        // paragraph.Add(new Chunk("注：", font));
        iTextSharp.text.List list = new iTextSharp.text.List(true, 10);
        //list.Numbered = true;
        foreach (var str in strList)
        {
            list.Add(new iTextSharp.text.ListItem(str, font));
        }
        paragraph.Add(list);
        document.Add(paragraph);
        return document;
    }

    #endregion

    #region 获取文档签字部分
    /// <summary>
    /// 获取文档签字部分
    /// </summary>
    /// <param name="document"></param>
    /// <returns></returns>
    public Document GetDocumentSign(Document document)
    {
        Font font = GetPdfFont();
        Paragraph paragraph = new Paragraph("办事处主任签名：                                         经销商盖章并签名：", font);
        document.Add(paragraph);
        paragraph = new Paragraph("确认日期：                                               确认日期：", font);
        paragraph.Alignment = iTextSharp.text.Element.ALIGN_LEFT;
        document.Add(paragraph);

        return document;
    }
    #endregion

    #region 关闭PDF文档流

    /// <summary>
    /// 关闭PDF文档流
    /// </summary>
    /// <param name="doc"></param>
    public void DisposePdf(Document doc)
    {
        if (doc.IsOpen())
        {
            doc.Close();
        }
        doc.Dispose();
    }

    #endregion

    #region 压缩文件夹
    /// <summary>  
    /// 压缩文件夹的方法  
    /// </summary>  
    /// <param name="DirToZip">被压缩的文件名称(包含文件路径)</param>  
    /// <param name="ZipedFile">压缩后的文件名称(包含文件路径)</param>  
    /// <param name="CompressionLevel">压缩率0（无压缩）,9（压缩率最高）</param>  
    public void ZipDir(string DirToZip, string ZipedFile, int CompressionLevel)
    {
        //压缩文件为空时默认与压缩文件夹同一级目录  
        if (ZipedFile == string.Empty)
        {
            ZipedFile = DirToZip.Substring(DirToZip.LastIndexOf("/") + 1);
            ZipedFile = DirToZip.Substring(0, DirToZip.LastIndexOf("/")) + "//" + ZipedFile + ".zip";
        }

        if (Path.GetExtension(ZipedFile) != ".zip")
        {
            ZipedFile = ZipedFile + ".zip";
        }

        using (ZipOutputStream zipoutputstream = new ZipOutputStream(File.Create(ZipedFile)))
        {
            zipoutputstream.SetLevel(CompressionLevel);
            Crc32 crc = new Crc32();
            Hashtable fileList = getAllFies(DirToZip);
            foreach (DictionaryEntry item in fileList)
            {
                FileStream fs = File.OpenRead(item.Key.ToString());
                byte[] buffer = new byte[fs.Length];
                fs.Read(buffer, 0, buffer.Length);
                ZipEntry entry = new ZipEntry(item.Key.ToString().Substring(DirToZip.Length));
                entry.DateTime = (DateTime)item.Value;
                entry.Size = fs.Length;
                fs.Close();
                crc.Reset();
                crc.Update(buffer);
                entry.Crc = crc.Value;
                zipoutputstream.PutNextEntry(entry);
                zipoutputstream.Write(buffer, 0, buffer.Length);
            }
        }
    }

    /// <summary>  
    /// 获取所有文件  
    /// </summary>  
    /// <returns></returns>  
    private Hashtable getAllFies(string dir)
    {
        Hashtable FilesList = new Hashtable();
        DirectoryInfo fileDire = new DirectoryInfo(dir);
        if (!fileDire.Exists)
        {
            throw new System.IO.FileNotFoundException("目录:" + fileDire.FullName + "没有找到!");
        }

        this.getAllDirFiles(fileDire, FilesList);
        this.getAllDirsFiles(fileDire.GetDirectories(), FilesList);
        return FilesList;
    }

    /// <summary>  
    /// 获取一个文件夹下的所有文件夹里的文件  
    /// </summary>  
    /// <param name="dirs"></param>  
    /// <param name="filesList"></param>  
    private void getAllDirsFiles(DirectoryInfo[] dirs, Hashtable filesList)
    {
        foreach (DirectoryInfo dir in dirs)
        {
            foreach (FileInfo file in dir.GetFiles("*.*"))
            {
                filesList.Add(file.FullName, file.LastWriteTime);
            }
            this.getAllDirsFiles(dir.GetDirectories(), filesList);
        }
    }
    /// <summary>  
    /// 获取一个文件夹下的文件  
    /// </summary>  
    /// <param name="strDirName">目录名称</param>  
    /// <param name="filesList">文件列表HastTable</param>  
    private void getAllDirFiles(DirectoryInfo dir, Hashtable filesList)
    {
        foreach (FileInfo file in dir.GetFiles("*.*"))
        {
            filesList.Add(file.FullName, file.LastWriteTime);
        }
    }

    #endregion

    #region 为单个经销商创建PDF文档

    /// <summary>
    /// 为单个经销商创建PDF文档
    /// </summary>
    /// <param name="folderPath"></param>
    /// <param name="fileName"></param>
    /// <param name="client"></param>
    /// <param name="dateBegin"></param>
    /// <param name="dateEnd"></param>
    public void CreateClientPDF(string folderPath, string fileName, CM_Client client, DateTime dateBegin, DateTime dateEnd)
    {
        Document doc = GetOrCreatePDF(fileName, folderPath);
        //文档期间项
        string documentDate = dateBegin.ToString("yyyy年第MM期") + "至" + dateEnd.ToString("yyyy年第MM期");
        doc = ReceiveBillPDF(doc, client, documentDate, dateBegin, dateEnd);
        doc.Add(Chunk.NEXTPAGE);
        doc = RangLiDetailPDF(doc, client, documentDate, dateBegin, dateEnd);
        doc.Add(Chunk.NEXTPAGE);
        doc = TransferDetailPDF(doc, client, documentDate, dateBegin, dateEnd);
        doc.Add(Chunk.NEXTPAGE);

        dateBegin = dateBegin.AddMonths(1);

        DisposePdf(doc);
    }
    #endregion

    #region 创建客户对账单
    /// <summary>
    /// 创建客户对账单
    /// </summary>
    /// <param name="document"></param>
    /// <param name="client"></param>
    /// <param name="documentDate"></param>
    /// <param name="year"></param>
    /// <param name="month"></param>
    /// <returns></returns>
    private Document ReceiveBillPDF(Document document, CM_Client client, string documentDate, DateTime dateBegin, DateTime dateEnd)
    {
        if (!document.IsOpen())
        {
            return null;
        }
        document = this.GetDocumentHeader(document, "客户对账单", documentDate, client);
        document.Add(Chunk.NEWLINE);

        string procName = "KD.AIS20140109134912.dbo.STKD_ARREPORT_CXCAI";//应收账款存储过程名

        DataTable dt = new DataTable();
        while (DateTime.Compare(dateBegin, dateEnd) <= 0)
        {
            //文档期间项
            int year = dateBegin.Year;
            int month = dateBegin.Month;
            DataTable dtTemp = new CM_ClientBLL().GetStatement(procName, year, month, client.Code);
            dt.Merge(dtTemp);

            dateBegin = dateBegin.AddMonths(1);
        }


        //DataColumn col = new DataColumn("dateStr");
        //col.DefaultValue = ViewState["dateStr"].ToString();
        //dt.Columns.Add(col);
        //dt.Columns["dateStr"].SetOrdinal(0);

        List<string> tableHeaders = new List<string>(5);
        tableHeaders.Add("单据日期");
        tableHeaders.Add("单据类型");
        tableHeaders.Add("单据编号");
        tableHeaders.Add("摘要");
        tableHeaders.Add("本期应收");
        tableHeaders.Add("本期实收");
        tableHeaders.Add("期末余额");
        for (int i = 0; i < dt.Columns.Count; i++)
        {
            dt.Columns[i].ColumnName = tableHeaders[i];
        }

        PdfPTable ptable = TableToPDFTable(dt);
        float[] widths = new float[] { 1f, 1.5f, 1f, 2f, 1f, 1f, 1f };
        ptable.SetWidths(widths);

        Paragraph paragraph = new Paragraph();
        paragraph.Add(ptable);
        paragraph.Alignment = Element.PTABLE;
        document.Add(paragraph);

        document.Add(Chunk.NEWLINE);

        this.GetDocumentSign(document);

        return document;
    }

    #endregion

    #region 创建让利明细表
    /// <summary>
    /// 创建让利明细表
    /// </summary>
    /// <param name="document"></param>
    /// <param name="client"></param>
    /// <param name="documentDate"></param>
    /// <param name="year"></param>
    /// <param name="month"></param>
    /// <returns></returns>
    private Document RangLiDetailPDF(Document document, CM_Client client, string documentDate, DateTime dateBegin, DateTime dateEnd)// int year, int month)
    {
        if (!document.IsOpen())
        {
            return null;
        }
        document = this.GetDocumentHeader(document, "让利明细表", documentDate, client);
        document.Add(Chunk.NEWLINE);

        string procName = "JD.AIS_YSL.dbo.STKD_DDFYREPORT01_CXCAI";//代垫费用余额(让利明细表)存储过程名

        DataTable dt = new DataTable();
        while (DateTime.Compare(dateBegin, dateEnd) <= 0)
        {
            //文档期间项
            int year = dateBegin.Year;
            int month = dateBegin.Month;
            DataTable dtTemp = new CM_ClientBLL().GetStatement(procName, year, month, client.Code);
            dt.Merge(dtTemp);

            dateBegin = dateBegin.AddMonths(1);
        }
        dt.Columns.RemoveAt(1);//去除“期间”列
        //dt.Columns[1].SetOrdinal(0);//“期间”列移至首列

        List<string> tableHeaders = new List<string>(5);
        //tableHeaders.Add("期间");
        tableHeaders.Add("日期");
        tableHeaders.Add("摘要");
        tableHeaders.Add("转账金额");
        tableHeaders.Add("市场费用投入");
        tableHeaders.Add("期末余额");
        for (int i = 0; i < dt.Columns.Count; i++)
        {
            dt.Columns[i].ColumnName = tableHeaders[i];
        }

        PdfPTable ptable = TableToPDFTable(dt);
        //ptable.TotalWidth = 520f;
        //ptable.LockedWidth = true;
        float[] widths = new float[] { 1f, 2f, 1f, 1f, 1f };
        ptable.SetWidths(widths);

        document.Add(ptable);

        document = this.GetDocumentNotice(document);

        document.Add(Chunk.NEWLINE);

        this.GetDocumentSign(document);

        return document;
    }

    #endregion

    #region 创建转账明细表
    /// <summary>
    /// 创建转账明细表
    /// </summary>
    /// <param name="document"></param>
    /// <param name="client"></param>
    /// <param name="documentDate"></param>
    /// <param name="year"></param>
    /// <param name="month"></param>
    /// <returns></returns>
    private Document TransferDetailPDF(Document document, CM_Client client, string documentDate, DateTime dateBegin, DateTime dateEnd)
    {
        if (!document.IsOpen())
        {
            return null;
        }
        document = this.GetDocumentHeader(document, "转账明细表", documentDate, client);
        document.Add(Chunk.NEWLINE);

        string procName = "JD.AIS_YSL.dbo.STKD_DDFYREPORT02_CXCAI";//代垫费用余额(让利明细表)存储过程名
        DataTable dt = new DataTable();
        while (DateTime.Compare(dateBegin, dateEnd) <= 0)
        {
            //文档期间项
            int year = dateBegin.Year;
            int month = dateBegin.Month;
            DataTable dtTemp = new CM_ClientBLL().GetStatement(procName, year, month, client.Code);
            dt.Merge(dtTemp);

            dateBegin = dateBegin.AddMonths(1);
        }
        dt.Columns.RemoveAt(1);//去除“期间”列
        //dt.Columns[1].SetOrdinal(0);

        List<string> tableHeaders = new List<string>(5);
        //tableHeaders.Add("期间");
        tableHeaders.Add("日期");
        tableHeaders.Add("摘要");
        tableHeaders.Add("转账金额");
        tableHeaders.Add("市场费用投入");
        tableHeaders.Add("期末余额");
        for (int i = 0; i < dt.Columns.Count; i++)
        {
            dt.Columns[i].ColumnName = tableHeaders[i];
        }

        PdfPTable ptable = TableToPDFTable(dt);
        //ptable.TotalWidth = 520f;
        //ptable.LockedWidth = true;
        float[] widths = new float[] { 1f, 2f, 1f, 1f, 1f };
        ptable.SetWidths(widths);

        document.Add(ptable);

        document = this.GetDocumentNotice(document);
        document.Add(Chunk.NEWLINE);
        this.GetDocumentSign(document);

        return document;
    }

    #endregion
}
