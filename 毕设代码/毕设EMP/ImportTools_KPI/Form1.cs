using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Text.RegularExpressions;
using System.IO;
using System.Reflection;
using System.Collections;
using MCSFramework.BLL.OA;
using MCSFramework.DBUtility;
using MCSFramework.Model.OA;
using MCSFramework.IFStrategy;
using MCSFramework.BLL.Pub;
using MCSFramework.Model.Pub;
using MCSFramework.BLL;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace ImportTools_KPI
{
    public partial class Form1 : Form
    {
        private string fileName = "";
        System.Data.DataTable dt_Data;
        int state = 0; //1 读取EXCEL 2写入数据库 0 空闲状态
    


        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            fileName = openFileDialog1.FileName;
            state = 1;
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            backgroundWorker1.RunWorkerAsync();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            state = 2;
            button1.Enabled = false;
            button2.Enabled = false;
            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            if (dt_Data != null)
            {

                HSSFWorkbook hssfworkbook;
                FileStream file = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite);

                hssfworkbook = new HSSFWorkbook(file);
                ISheet sheet = hssfworkbook.GetSheetAt(0);
                int RowBase = 1;
                if (sheet.GetRow(1).GetCell(sheet.GetRow(1).FirstCellNum).ToString() == "")
                {
                    RowBase = 2;
                }
                decimal lastValue;
                int staffid, month, Scheme;
                KPI_ScoreBLL _bll;
                Hashtable ht_table = new Hashtable();
                for (int x = 0; x < dt_Data.Rows.Count; x++)
                {
                    _bll = new KPI_ScoreBLL();
                    ((BackgroundWorker)sender).ReportProgress((x + 1) * 100 / dt_Data.Rows.Count, x + 1);
                    try
                    {
                        if (dt_Data.Rows[x]["导入标志"].ToString().Trim() != "")
                            continue;
                        string errorText = "";
                        staffid = int.Parse(dt_Data.Rows[x]["员工ID"].ToString());

                        if (ht_table[dt_Data.Rows[x]["会计月"].ToString()] == null)
                        {
                            IList<AC_AccountMonth> _monthlist = AC_AccountMonthBLL.GetModelList("Name='" + dt_Data.Rows[x]["会计月"].ToString() + "'");
                            if (_monthlist.Count > 0)
                            {
                                month = _monthlist[0].ID;
                                ht_table[dt_Data.Rows[x]["会计月"].ToString()] = month;
                            }
                            else
                            {
                                dt_Data.Rows[x]["导入标志"] += "会计月错误;";
                                continue;
                            }
                        }
                        else
                        {
                            month = (int)ht_table[dt_Data.Rows[x]["会计月"].ToString()];
                        }
                        if (ht_table[dt_Data.Rows[x]["考核方案"].ToString()] == null)
                        {
                            IList<KPI_Scheme> _schemelist = KPI_SchemeBLL.GetModelList("Name='" + dt_Data.Rows[x]["考核方案"].ToString() + "'");
                            if (_schemelist.Count > 0)
                            {
                                Scheme = _schemelist[0].ID;
                                ht_table[dt_Data.Rows[x]["考核方案"].ToString()] = Scheme;
                            }
                            else
                            {
                                dt_Data.Rows[x]["导入标志"] += "未找到相应考核方案;";
                                continue;
                            }
                        }
                        else
                        {
                            Scheme = (int)ht_table[dt_Data.Rows[x]["考核方案"].ToString()];
                        }
                        Org_StaffBLL _staffbll = new Org_StaffBLL(staffid);
                        if (_staffbll.Model == null)
                        {
                            dt_Data.Rows[x]["导入标志"] += "未找到相应员工;";
                            continue;
                        }
                        else
                        {
                            IList<KPI_Score> _kpilist = KPI_ScoreBLL.GetModelList("AccountMonth=" + month.ToString() + " AND RelateStaff=" + staffid.ToString() + " AND Scheme=" + Scheme.ToString());
                            if (_kpilist.Count > 0)
                            {
                                if (_kpilist.FirstOrDefault(p => p.ApproveFlag == 1) != null)
                                {
                                    dt_Data.Rows[x]["导入标志"] += "该员工绩效已审核，不能再次导入;";
                                    continue;
                                }
                                else
                                {
                                    _bll = new KPI_ScoreBLL(_kpilist[0].ID);
                                    _bll.Model.UpdateTime = DateTime.Now;
                                    _bll.Items.Clear();
                                }
                            }
                            else
                            {
                                _bll.Model.Scheme = Scheme;
                                _bll.Model.RelateStaff = staffid;
                                _bll.Model.AccountMonth = month;
                                _bll.Model.OrganizeCity = _staffbll.Model.OrganizeCity;
                                _bll.Model.ApproveFlag = 2;
                                _bll.Model.InsertStaff = 1;
                                _bll.Model.Position = _staffbll.Model.Position;
                                _bll.Model.InsertTime = DateTime.Now;

                            }
                        }
                        IList<KPI_ScoreDetail> _details = new List<KPI_ScoreDetail>();
                        for (int y = 9; y < dt_Data.Columns.Count; y++)
                        {
                            KPI_ScoreDetail detail = new KPI_ScoreDetail();
                            if (ht_table[dt_Data.Columns[y].ColumnName.ToString() + "(" + _bll.Model.Scheme.ToString() + ")"] == null)
                            {
                                IList<KPI_SchemeDetail> _SchemeDetailList = new KPI_SchemeBLL().GetDetail("Name='" + dt_Data.Columns[y].ColumnName.ToString() + "' AND Scheme=" + _bll.Model.Scheme.ToString());
                                if (_SchemeDetailList.Count > 0)
                                {
                                    detail.SchemeItem = _SchemeDetailList[0].ID;
                                    ht_table[dt_Data.Columns[y].ColumnName.ToString() + "(" + _bll.Model.Scheme.ToString() + ")"] = _SchemeDetailList[0].ID;
                                }
                                else
                                {
                                    dt_Data.Rows[x]["导入标志"] += "考核指标未找到;";
                                }
                            }
                            else
                            {
                                detail.SchemeItem = (int)ht_table[dt_Data.Columns[y].ColumnName.ToString() + "(" + _bll.Model.Scheme.ToString() + ")"];
                            }
                            if (!decimal.TryParse(dt_Data.Rows[x][y].ToString(), out lastValue))
                            {
                                errorText += dt_Data.Columns[y] + "转换出错;导入失败！";
                                continue;
                            }
                            else
                            {
                                detail.LastValue = lastValue;


                            }
                            errorText = errorText.Replace("导入成功", "");
                            dt_Data.Rows[x]["导入标志"] = errorText == "" ? "导入成功" : errorText;
                            _details.Add(detail);
                        }
                        if (_bll.Model.ID > 0)
                        {
                            _bll.DeleteDetail();     //先清除原先导入的数据
                            _bll.Items = _details;
                            _bll.AddDetail();
                            _bll.Update();
                        }
                        else
                        {
                            _bll.Items = _details;
                            _bll.Add();
                        }
                    }
                    catch (Exception ex)
                    {
                        dt_Data.Rows[x]["导入标志"] = "导入出错";
                        MessageBox.Show(ex.Message);
                        continue;
                    }
                    finally
                    {
                        sheet.GetRow(RowBase + x).GetCell(8).SetCellValue(dt_Data.Rows[x]["导入标志"].ToString());
                    }
                }
                FileStream writefile = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite);
                hssfworkbook.Write(writefile);
                writefile.Close();

                sheet = null;
            }
            else
            {
                if (fileName != "")
                {
                    ConvertToDataTable(fileName);
                }
            }
        }

        private void ConvertToDataTable(string fileName)
        {
            HSSFWorkbook hssfworkbook;
            using (FileStream file = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite))
            {
                hssfworkbook = new HSSFWorkbook(file);

            }

            ISheet sheet = hssfworkbook.GetSheetAt(0);
            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();
            dt_Data = new System.Data.DataTable();

            try
            {
                IRow headerRow = sheet.GetRow(0);
                int cellCount = headerRow.LastCellNum;
                rows.MoveNext();
                bool isquarter = false;
                string firstcolumnname, secondcolumnname;
                for (int i = headerRow.FirstCellNum; i < cellCount; i++)
                {

                    if (i > 8 && sheet.GetRow(1).GetCell(sheet.GetRow(1).FirstCellNum).ToString() == "")
                    {
                        isquarter = true;
                        firstcolumnname = "";
                        for (int x = i; x >= 1; x--)
                        {
                            firstcolumnname = headerRow.GetCell(x).ToString();
                            if (firstcolumnname != "")
                                break;
                        }
                        secondcolumnname = sheet.GetRow(1).GetCell(i).ToString();
                        dt_Data.Columns.Add(firstcolumnname + "→" + secondcolumnname);
                    }
                    else
                    {
                        dt_Data.Columns.Add(headerRow.GetCell(i).ToString());
                    }
                }
                if (!dt_Data.Columns.Contains("员工ID") || !dt_Data.Columns.Contains("姓名") || !dt_Data.Columns.Contains("会计月") || !dt_Data.Columns.Contains("考核方案")
                           || !dt_Data.Columns.Contains("考核周期") || !dt_Data.Columns.Contains("上次考核时间") || !dt_Data.Columns.Contains("导入标志")
                           )
                {
                    MessageBox.Show("外部表不是预期格式！");

                    return;
                }
                if (isquarter) rows.MoveNext();
                while (rows.MoveNext())
                {
                    
                    HSSFRow row = (HSSFRow)rows.Current;
                    if (row.GetCell(0).ToString() == "")
                    {
                        break;
                    }
                    DataRow dr = dt_Data.NewRow();

                    for (int i = 0; i < row.LastCellNum; i++)
                    {
                        
                        ICell cell = row.GetCell(i);
                        if (headerRow.GetCell(i).ToString() == "会计月")
                        {
                            ICellStyle cellStyle = cell.CellStyle;
                            IDataFormat format = hssfworkbook.CreateDataFormat();
                            cellStyle.DataFormat = format.GetFormat("yyyy-MM");
                            cell.CellStyle = cellStyle;

                        }

                        switch (cell.CellType)
                        {
                            case CellType.BLANK:
                                dr[i] = "";
                                break;
                            case CellType.BOOLEAN:
                                dr[i] = cell.BooleanCellValue;
                                break;
                            case CellType.NUMERIC:
                                dr[i] = cell.ToString();    //This is a trick to get the correct value of the cell. NumericCellValue will return a numeric value no matter the cell value is a date or a number.
                                break;
                            case CellType.STRING:
                                dr[i] = cell.ToString();
                                break;
                            case CellType.ERROR:
                                dr[i] = cell.ErrorCellValue;
                                break;
                            case CellType.FORMULA:
                            default:
                                dr[i] = "=" + cell.CellFormula;
                                break;
                        }
                    }
                    dt_Data.Rows.Add(dr);
                }

            }
            catch (System.Exception ex)
            {
                dt_Data.Clear();
                dt_Data.Columns.Clear();
                dt_Data.Columns.Add("出错了");
                DataRow dr = dt_Data.NewRow();
                dr[0] = ex.Message;
                dt_Data.Rows.Add(dr);
            }
            finally
            {
                hssfworkbook = null;

                sheet = null;
            }

        }
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            if (state == 1)
                label2.Text = "正在读取EXCEL文件，请稍后... 已读取" + progressBar1.Value + "%,第" + ((int)e.UserState + 1).ToString() + "条";
            else if (state == 2)
                label2.Text = "正在写入数据库，请稍后... 已写入" + progressBar1.Value + "%,第" + ((int)e.UserState + 1).ToString() + "条";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.backgroundWorker1.CancelAsync();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
        }
        private string WirteInSQL(int staffid, string Month, string cycleName, string schemeName, decimal lastValue)
        {
            return KPI_ScoreBLL.ImportExcel(staffid, Month, cycleName, schemeName, lastValue);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            
                GC.Collect();
             
        }

        private void backgroundWorker1_RunWorkerCompleted_1(object sender, RunWorkerCompletedEventArgs e)
        {
            if (dt_Data != null)
                dataGridView1.DataSource = dt_Data;
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            state = 0;
        }
    }
}
