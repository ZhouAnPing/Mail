﻿using ChinaUnion_Agent.Properties;
using ChinaUnion_Agent.Util;
using ChinaUnion_Agent.Wechat;
using LinqToExcel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChinaUnion_Agent.WechatForm
{
    public partial class frmErrorCodeWechat : Form
    {
        public frmErrorCodeWechat()
        {
            InitializeComponent();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.CurrentDirectory;
            // openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Excel(*.xlsx)|*.xlsx|Excel 2000-2003(*.xls)|*.xls|CSV(*.csv)|*.csv|所有文件(*.*)|*.*";

            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                Cursor.Current = Cursors.WaitCursor;
                string FileName = openFileDialog.FileName;

                // String path= Path.GetDirectoryName(FileName);

                var execelfile = new ExcelQueryFactory(FileName);
                this.txtErrorCode.Text = FileName;
                this.txtErrorCode.Enabled = false;

                List<string> sheetNames = execelfile.GetWorksheetNames().ToList();
                if (!sheetNames.Contains("微信用户"))
                {
                    MessageBox.Show("Excel格式不正确，必须含有名称:微信用户的sheet.");
                    return;
                }

                this.dgWechat.Rows.Clear();
                dgWechat.Columns.Clear();

                dgWechat.Columns.Add("姓名", "姓名");
                dgWechat.Columns.Add("账号", "账号");
                dgWechat.Columns.Add("邮箱", "邮箱");
                dgWechat.Columns.Add("手机号", "手机号");

                dgWechat.Columns.Add("微信号", "微信号");



                List<Row> WechatList = execelfile.Worksheet("微信用户").ToList();
                //int count = 0;
                if (WechatList != null && WechatList.Count > 0)
                {
                    //count = ESSList.Count;
                    this.btnSync.Enabled = true;

                    for (int i = 0; i < WechatList.Count; i++)
                    {
                        if (String.IsNullOrEmpty(WechatList[i][0]) || String.IsNullOrWhiteSpace(WechatList[i][0]))
                        {
                            break;
                        }
                        this.dgWechat.Rows.Add();
                        DataGridViewRow row = dgWechat.Rows[dgWechat.RowCount - 1];
                        row.Cells["姓名"].Value = WechatList[i][0].ToString().Trim();
                        row.Cells["账号"].Value = WechatList[i][1].ToString().Trim();
                        row.Cells["邮箱"].Value = WechatList[i][2].ToString().Trim();
                        row.Cells["手机号"].Value = WechatList[i][3].ToString().Trim();
                        row.Cells["微信号"].Value = WechatList[i][4].ToString().Trim(); 
                       

                    }
                }
            }
            this.dgWechat.AutoResizeColumns();
          

        }

        private void btnSync_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            //异步执行开始
            worker.RunWorkerAsync();
            frmProgress frm = new frmProgress(this.worker);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog(this);
            frm.Close();

           
            this.Cursor = Cursors.Default;
        }
        BackgroundWorker worker; 
        private void frmErrorCodeWechat_Load(object sender, EventArgs e)
        {
            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
        }
        /// <summary>
        /// 异步 开始事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            //需要执行的代码
            WechatAction wechatAction = new Wechat.WechatAction();

            worker.ReportProgress(1, "开始同步微信账号...\r\n");


            bool isNewUser = false;


            for (int i = 0; i < this.dgWechat.RowCount; i++)
            {

                WechatJsonUser toWechatJsonUser = new WechatJsonUser();

                toWechatJsonUser.userid = this.dgWechat[1, i].Value.ToString();

                worker.ReportProgress(2, "同步微信账号" + toWechatJsonUser.userid + "\r\n");
                HttpResult result = wechatAction.getUserFromWechat(toWechatJsonUser.userid, Settings.Default.Wechat_Secret);


                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    //表示访问成功，具体的大家就参考HttpStatusCode类
                    toWechatJsonUser = JsonConvert.DeserializeObject<WechatJsonUser>(result.Html);
                    if (String.IsNullOrEmpty(toWechatJsonUser.userid))
                    {
                        isNewUser = true;
                    }
                    toWechatJsonUser.name = this.dgWechat[0, i].Value.ToString();
                    toWechatJsonUser.userid = this.dgWechat[1, i].Value.ToString();

                    toWechatJsonUser.weixinid = this.dgWechat[2, i].Value.ToString();
                    toWechatJsonUser.email = this.dgWechat[4, i].Value.ToString();
                    toWechatJsonUser.mobile = this.dgWechat[3, i].Value.ToString();

                    if (toWechatJsonUser.department == null)
                    {
                        toWechatJsonUser.department = new List<int>();
                    }

                    WechatUser fromWechatUser = wechatAction.getUserFromWechatByDepartment(Settings.Default.Wechat_Error_Department, Settings.Default.Wechat_Secret);
                    if (fromWechatUser.userlist.Count <= 1000)
                    {
                        toWechatJsonUser.department.Add(Settings.Default.Wechat_Error_Sub_Department_1);
                    }
                    else
                    {
                        if (fromWechatUser.userlist.Count > 1000 && fromWechatUser.userlist.Count <= 2000)
                        {
                            toWechatJsonUser.department.Add(Settings.Default.Wechat_Error_Sub_Department_2);
                        }
                        else
                        {
                            if (fromWechatUser.userlist.Count > 2000 && fromWechatUser.userlist.Count <= 3000)
                            {
                                toWechatJsonUser.department.Add(Settings.Default.Wechat_Error_Sub_Department_3);
                            }
                            else
                            {
                                toWechatJsonUser.department.Add(Settings.Default.Wechat_Error_Department);
                            }
                        }
                    }
                    var userData = new
                    {
                        userid = toWechatJsonUser.userid,
                        name = toWechatJsonUser.name,
                        department = toWechatJsonUser.department,
                        position = toWechatJsonUser.name,
                        mobile = toWechatJsonUser.mobile,
                        email = toWechatJsonUser.email,
                        weixinid = toWechatJsonUser.weixinid
                    };

                    string userJson = JsonConvert.SerializeObject(userData, Formatting.Indented);


                    if (!isNewUser)
                    {
                        result = wechatAction.updateUserToWechat(Settings.Default.Wechat_Secret, userJson);
                    }
                    else
                    {
                        result = wechatAction.addUserToWechat(Settings.Default.Wechat_Secret, userJson);
                    }
                }

            }

            worker.ReportProgress(2, "同步微信账号完毕...\r\n");


        }


        /// <summary>
        /// 事件: 异步执行完成后 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("微信同步完毕。", "微信同步", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel格式|*.xlsx";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.FileName = "微信用户导入模板.xlsx";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {

                File.Copy("./Template/微信用户导入模板.xlsx", saveFileDialog.FileName);
               
              
            }
        }
    }
}
