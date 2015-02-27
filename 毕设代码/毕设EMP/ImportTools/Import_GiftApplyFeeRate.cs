using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Text;
using System.Windows.Forms;
using MCSFramework.BLL;
using MCSFramework.BLL.Logistics;
using MCSFramework.BLL.CM;
using MCSFramework.BLL.Pub;
using MCSFramework.Model;
using MCSFramework.Model.Logistics;
using MCSFramework.Model.Pub;
using MCSFramework.Model.CM;

namespace ImportTools
{
    public partial class Import_GiftApplyFeeRate : Form
    {
      DataTable dt_Amount;
      public Import_GiftApplyFeeRate()
        {
            InitializeComponent();
            bt_StartImport.Enabled = false;
            bt_StopImport.Enabled = false;

            CheckForIllegalCrossThreadCalls = false;
        }

        private void bt_RetrieveData_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.ShowDialog();
        }
        //开始导入
        private void bt_StartImport_Click(object sender, EventArgs e)
        {
            bt_RetrieveData.Enabled = false;
            bt_StartImport.Enabled = false;
            bt_StopImport.Enabled = true;

            backgroundWorker1.RunWorkerAsync();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            OleDbConnection oleConn = new OleDbConnection();
            dt_Amount = new DataTable();
            try
            {
                oleConn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + openFileDialog1.FileName + ";Extended Properties=EXCEL 8.0";
                oleConn.Open();

                OleDbCommand oledbcmd = new OleDbCommand("SELECT * FROM [经销商赠品费率$] where (导入标志='' or 导入标志 is null) and 序号 is not null ", oleConn);
                OleDbDataAdapter oleAdapter = new OleDbDataAdapter(oledbcmd);
                oleAdapter.Fill(dt_Amount);

                DataColumn[] columns_key = { dt_Amount.Columns["序号"] };
                dt_Amount.PrimaryKey = columns_key;

                gv_List.DataSource = dt_Amount;
                progressBar1.Value = 0;

                //bt_RetrieveData.Enabled = false;
                bt_StartImport.Enabled = true;
                bt_StopImport.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                oleConn.Close();
            }
        }

        private void bt_StopImport_Click(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();
        }
        #region 后台进程
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = (BackgroundWorker)sender;

            threadStartImport(worker, e);

        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            lb_Current.Text = ((int)e.UserState + 1).ToString();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            bt_RetrieveData.Enabled = true;
            bt_StartImport.Enabled = false;
            bt_StopImport.Enabled = false;
        }
        #endregion


        private void threadStartImport(BackgroundWorker worker, DoWorkEventArgs e)
        {
            bool bFind = false;

            StringBuilder _xhs_success = new StringBuilder("");//成功导入的记录序号（用于定期批量更新导入标志）
            string XH = "";
            int ClientID = 0, AccountMonth = 0, Brand = 0, Classify = 0,DIClassify=0;
            decimal FeeRate;
            try
            {
                lb_Count.Text = dt_Amount.Rows.Count.ToString();
                for (int i = 0; i < dt_Amount.Rows.Count; i++)
                {
                    worker.ReportProgress((i + 1) * 100 / this.dt_Amount.Rows.Count, i);
                    if (worker.CancellationPending)
                    {
                        e.Cancel = true;
                        break;
                    }
                    XH = dt_Amount.Rows[i]["序号"].ToString();
                    IList<CM_Client> _cmlist = CM_ClientBLL.GetModelList("Code='" + dt_Amount.Rows[i]["经销商代码"].ToString() + "' AND ActiveFlag=1 AND ApproveFlag=1");

                    if (_cmlist.Count == 1 && _cmlist[0].FullName == dt_Amount.Rows[i]["经销商名称"].ToString() && _cmlist[0]["DIClassify"]=="1")
                    {                      
                      ClientID = _cmlist[0].ID;//经销商为子户头时，取主户头                        
                    }
                    else
                    {
                        UpdateImportFlag(i, "导入失败，经销商代码不正确");
                        continue;
                    }

                    if (dt_Amount.Rows[i]["品牌"].ToString()!= "")
                    {
                        IList<PDT_Brand> _listbrand = PDT_BrandBLL.GetModelList("Name='" + dt_Amount.Rows[i]["品牌"].ToString()+"'");
                        if (_listbrand.Count == 0)
                        {
                            UpdateImportFlag(i, "导入失败，该品牌不存在");
                            continue;
                        }
                        else
                        {
                            Brand = _listbrand[0].ID;
                        }
                    } 

                   

                    foreach (Dictionary_Data item in DictionaryBLL.Dictionary_Data_GetAlllList("Type IN (SELECT ID FROM MCS_SYS.dbo.Dictionary_Type WHERE TableName='ORD_GiftClassify')"))
                    {
                        if (item.Name == dt_Amount.Rows[i]["有导无导标志"].ToString().Trim())
                        {
                            Classify = int.Parse(item.Code);
                            bFind = true;
                            break;
                        }
                    }
                    if (!bFind)
                    {
                        UpdateImportFlag(i, "导入失败，有导无导标志错误！");
                        continue;
                    }

                    IList<PDT_ClassifyGiftCostRate> _listamount = PDT_ClassifyGiftCostRateBLL.GetModelList("Enabled='Y' AND Client=" + ClientID.ToString()
                                                            + " AND PDTBrand=" + Brand.ToString() + " AND GiftCostClassify=" + Classify.ToString());
                    PDT_ClassifyGiftCostRateBLL _bll = new PDT_ClassifyGiftCostRateBLL();     
                    if (_listamount.Count > 0)
                    {
                        _bll = new PDT_ClassifyGiftCostRateBLL(_listamount[0].ID);                    
                    }
                    else
                    {
                        _bll.Model.AccountMonth = AccountMonth;
                        _bll.Model.Client = ClientID;
                        _bll.Model.PDTBrand = Brand;
                        _bll.Model.GiftCostClassify = Classify;
                        _bll.Model.Enabled = "Y";
                        _bll.Model.OrganizeCity = 1;
                    }
                     
                    #region 记录需更新费用
                    decimal.TryParse(dt_Amount.Rows[i]["赠品费率%"].ToString().Trim(), out FeeRate);
                    _bll.Model.GiftCostRate = FeeRate;
                    _bll.Model.Remark = dt_Amount.Rows[i]["备注"].ToString().Trim();
                    if (_bll.Model.ID > 0)
                    {
                        _bll.Update();
                    }
                    else
                    {
                        _bll.Add();
                    }
                    dt_Amount.Rows[i]["导入标志"] = "导入成功";
                    _xhs_success.Append(XH + ",");
                    #endregion

                }
                UpdateSuccess(_xhs_success.ToString());
            }
            catch (System.Exception err)
            {
                UpdateSuccess(_xhs_success.ToString());
                MessageBox.Show(err.Source + "~r~n" + err.StackTrace, err.Message);
            }

            return;
        }

        private int UpdateSuccess(string xhs)
        {
            if (xhs.EndsWith(",")) xhs = xhs.Substring(0, xhs.Length - 1);
            if (xhs.Length > 0)
            {
                string strcmd = "";
                strcmd = "Update [经销商赠品费率$] set 导入标志='导入成功' where 序号 in (" + xhs + ")";
                OleDbConnection oleConn = new OleDbConnection();
                oleConn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + openFileDialog1.FileName + ";Extended Properties=EXCEL 8.0";
                OleDbCommand oledbcmd = new OleDbCommand(strcmd, oleConn);
                try
                {
                    oleConn.Open();
                    oledbcmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + strcmd);
                }
                oleConn.Close();
            }
            return 1;
        }

        #region 更新导入标志信息
        /// <summary>
        /// 更新导入标志信息
        /// </summary>
        /// <param name="Flag"></param>
        /// <param name="Row"></param>
        /// <param name="Result"></param>
        /// <returns></returns>
        private int UpdateImportFlag(int Row, string Result)
        {
            string cmd;
            cmd = "Update [经销商赠品费率$] set 导入标志='" + Result + "' where 序号=";
            dt_Amount.Rows[Row]["导入标志"] = Result;                  //更新DataTable中的导入标志
            string xh = dt_Amount.Rows[Row]["序号"].ToString();
            if (char.IsDigit(xh, 0))
                cmd += xh;            //更新Excel中对应记录的失败信息
            else
                cmd += "'" + xh + "'";            //更新Excel中对应记录的失败信息

            OleDbConnection oleConn = new OleDbConnection();
            oleConn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + openFileDialog1.FileName + ";Extended Properties=EXCEL 8.0";
            OleDbCommand oledbcmd = new OleDbCommand(cmd, oleConn);
            try
            {
                oleConn.Open();
                oledbcmd.ExecuteNonQuery();
                oleConn.Close();
            }
            catch (Exception ex)
            {
                oleConn.Close();
                MessageBox.Show(ex.Message + cmd);
                return -2;
            }

            return 1;
        }

        #endregion
       
    }
}
