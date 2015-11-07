using ChinaUnion_Agent.Properties;
using ChinaUnion_Agent.Util;
using ChinaUnion_Agent.Wechat;
using ChinaUnion_BO;
using ChinaUnion_DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ChinaUnion_Agent.ComplainForm
{
    public partial class frmComplainManagement : frmBase
    {
        AgentComplainDao agentComplainDao = new AgentComplainDao();

        //初始化绑定默认关键词（此数据源可以从数据库取）
        List<string> listOnit = new List<string>();
        //输入key之后，返回的关键词
        List<string> listNew = new List<string>();

        public frmComplainManagement()
        {
            InitializeComponent();
        }

        private void frmComplainManagement_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.initControl();
        }

        private void initControl()
        {
            this.Cursor = Cursors.WaitCursor;
            this.btnSave.Enabled = false;
            this.btnDelete.Enabled = false;

            this.txtUserName.Clear();
            this.txtProcessCode.Clear();
            this.txtJoinTime.Clear();
            this.txtJoinMenu.Clear();
            this.txtComplainContent.Clear();
            this.txtBranchCode.Clear();
            this.txtBranchName.Clear();
            this.cboAgentNo.ResetText();
            this.dtReplyTime.ResetText();
            this.txtComent.Clear();
            this.txtSequence.Clear();

            this.dtReplyTime.Value = DateTime.Now.AddDays(10);


            AgentWechatAccountDao agentWechatAccountDao = new AgentWechatAccountDao();
            IList<AgentWechatAccount> agentWechatAccountList = agentWechatAccountDao.GetAllAgentOrBranch();
            this.cboAgentNo.Items.Clear();
            //  this.lstAgentType.Items.Add("所有渠道");
            foreach (AgentWechatAccount agentWechatAccount in agentWechatAccountList)
            {
                if (agentWechatAccount.type.Contains("代理商"))
                {
                    listOnit.Add(agentWechatAccount.agentNo + ":" + agentWechatAccount.agentName);
                       // this.cboAgentNo.Items.Add(agentWechatAccount.agentNo + ":" + agentWechatAccount.agentName);

                }
            }
            this.cboAgentNo.Items.AddRange(listOnit.ToArray());

            this.Cursor = Cursors.Default;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {

         //   this.Cursor = Cursors.WaitCursor;

            prepareGrid(this.txtSearchCondition.Text.Trim());

           // this.Cursor = Cursors.Default;

           
        }
        private void prepareGrid(String condition)
        {
            this.Cursor = Cursors.WaitCursor;

            IList<AgentComplain> agentComplainList = agentComplainDao.GetList(condition);
            this.dgComplain.Rows.Clear();
            dgComplain.Columns.Clear();
            dgComplain.Columns.Add("序列号", "序列号");

            dgComplain.Columns.Add("受理编码", "受理编码");
            dgComplain.Columns.Add("用户名", "用户名");
            
            dgComplain.Columns.Add("内容", "内容");
            dgComplain.Columns.Add("回复截止时间", "回复截止时间");
            dgComplain.Columns[0].Visible = false;
            if (agentComplainList != null && agentComplainList.Count > 0)
            {

                for (int i = 0; i < agentComplainList.Count; i++)
                {
                    dgComplain.Rows.Add();
                    DataGridViewRow row = dgComplain.Rows[dgComplain.RowCount - 1];
                    row.Cells[0].Value = agentComplainList[i].sequence;
                    row.Cells[1].Value = agentComplainList[i].processCode;
                    row.Cells[2].Value = agentComplainList[i].userName;
                    row.Cells[3].Value = agentComplainList[i].content;
                    if (!String.IsNullOrEmpty(agentComplainList[i].content) && agentComplainList[i].content.Length>20)
                    {
                        row.Cells[3].Value = agentComplainList[i].content.Substring(0, 20)+"...";
                    }
                    
                    row.Cells[4].Value = agentComplainList[i].replyTime;
                   

                }
            }

            this.initControl();
            
            
            this.dgComplain.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            this.dgComplain.AutoResizeColumns();
            this.Cursor = Cursors.Default;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            this.initControl();
        }

        private void txtSubject_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(this.txtJoinTime.Text.Trim()))
            {
                this.btnDelete.Enabled = false;
                this.btnSave.Enabled = false;
               // this.btnPreview.Enabled = false;
               // this.btnPublish.Enabled = false;
            }
            else
            {
                this.btnDelete.Enabled = true;
                this.btnSave.Enabled = true;
              //  this.btnPreview.Enabled = true;
               // this.btnPublish.Enabled = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(this.lstAgentType.CheckedItems.Count.ToString());
            //return;
            
            if (String.IsNullOrEmpty(this.txtComplainContent.Text.Trim()))
            {
                MessageBox.Show("请输入投诉！");
                this.txtComplainContent.Focus();
                return;
            }
            if (String.IsNullOrEmpty(this.cboAgentNo.Text.Trim()))
            {
                MessageBox.Show("请选择代理商！");
               // this.txtComplainContent.Focus();
                return;
            }
            

            this.Cursor = Cursors.WaitCursor;
            AgentComplain agentComplain = new AgentComplain();
            agentComplain.sequence = this.txtSequence.Text.Trim();
            agentComplain.userName = this.txtUserName.Text;
            agentComplain.processCode = this.txtProcessCode.Text;
            agentComplain.processBranchCode = this.txtBranchCode.Text;
            agentComplain.processBranchName = this.txtBranchName.Text;
            agentComplain.joinTime = this.txtJoinTime.Text;
            agentComplain.joinMenu = this.txtJoinMenu.Text;
            agentComplain.comment = this.txtComent.Text;
            agentComplain.content = this.txtComplainContent.Text;
            agentComplain.agentNo = this.cboAgentNo.Text.ToString().Substring(0, this.cboAgentNo.Text.ToString().IndexOf(":"));

            agentComplain.replyTime = this.dtReplyTime.Value.ToString("yyyy-MM-dd HH:mm:ss");

           
            if (!String.IsNullOrEmpty(agentComplain.sequence))
            {

                this.agentComplainDao.Update(agentComplain);

            }
            else
            {
                agentComplain.sequence = agentComplainDao.Add(agentComplain) ;

                this.txtSequence.Text = agentComplain.sequence;
               // agentComplain = agentComplainDao.GetByProcessCode("");
            }

           // agentComplainReceiverDao.Delete(agentComplain.sequence);


           

            this.prepareGrid(this.txtSearchCondition.Text);
            MessageBox.Show("操作完成");

            this.Cursor = Cursors.Default;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.dgComplain.CurrentRow == null)
            {
                MessageBox.Show("请从列表中选择选择一行进行删除");
                return;
            }
            this.Cursor = Cursors.WaitCursor;
            int seq = (Int32.Parse(this.dgComplain.CurrentRow.Cells[0].Value.ToString()));

           // this.agentComplainReceiverDao.Delete(seq.ToString());
            this.agentComplainDao.Delete(seq);
            this.prepareGrid(this.txtSearchCondition.Text);

            this.Cursor = Cursors.Default;
            MessageBox.Show("操作完成");
        }

        private void dgAgentComplain_SelectionChanged(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            if (this.dgComplain.CurrentRow != null)
            {
                if (this.dgComplain[0,this.dgComplain.CurrentRow.Index].Value != null)
                {
                    this.initControl();
                   
                    AgentComplain agentComplain = agentComplainDao.Get(Int32.Parse(this.dgComplain[0, this.dgComplain.CurrentRow.Index].Value.ToString()));
                    if (agentComplain != null)
                    {
                        this.txtUserName.Text = agentComplain.userName;
                        this.txtProcessCode.Text = agentComplain.processCode;
                        this.txtJoinMenu.Text = agentComplain.joinMenu;
                        this.txtJoinTime.Text = agentComplain.joinTime;

                        this.txtComplainContent.Text = agentComplain.content;
                        this.txtBranchName.Text = agentComplain.processBranchName;
                        this.txtBranchCode.Text = agentComplain.processBranchCode;

                        for (int i = 0; i < this.cboAgentNo.Items.Count; i++)
                        {
                            if (cboAgentNo.Items[i].ToString().Contains(agentComplain.agentNo))
                            {
                                this.cboAgentNo.SelectedIndex = i;
                                break;
                            }
                        }
                        
                        this.txtComent.Text = agentComplain.comment;
                        this.dtReplyTime.Value = DateTime.Parse(agentComplain.replyTime);

                        this.txtSequence.Text = agentComplain.sequence;

                        AgentComplainReplyDao agentComplainReplyDao = new AgentComplainReplyDao();
                        IList<AgentComplainReply> agentComplainReplyList = agentComplainReplyDao.GetList(agentComplain.sequence);
                        foreach (AgentComplainReply agentComplainReply in agentComplainReplyList)
                        {
                            this.txtHistory.Text += "【" + agentComplainReply.replyTime + "】" + agentComplainReply.replyContent + "\n";
                        }

                                               
                    }
                }
            }
            this.Cursor = Cursors.Default;
        }

        private void btnPublish_Click(object sender, EventArgs e)
        {
            //this.btnSave_Click(sender, e);
          //  this.prepareGrid(this.txtSearchCondition.Text);
            if (String.IsNullOrEmpty(this.txtSequence.Text))
            {
                MessageBox.Show("请先保存，再发布");
                return;
            }
            this.Cursor = Cursors.WaitCursor;
            int appId = 0;

            IList<String> UserIdList = new List<String>();//agentComplainDao.GetAllAgentNoListBySeq(this.txtSequence.Text);

            List<String> userIdsBuffer = new List<string>();
            for (int i = 1; i <= UserIdList.Count; i++)
            {
                userIdsBuffer.Add(UserIdList[i - 1]);
                if (i % 500 == 0 || i == UserIdList.Count)
                {
                    string userId = "";
                    userId = string.Join("|", userIdsBuffer.ToArray());
                    userIdsBuffer.Clear();

                    WechatAction wechatAction = new WechatAction();

                    String content = this.txtJoinTime.Text.Trim();
                    String state = this.txtSequence.Text;
                    String url = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=wx31204de5a3ae758e&redirect_uri=http%3a%2f%2f112.64.17.80%2fwechat%2fBusinessAgentComplainDetail.aspx&response_type=code&scope=snsapi_base&state=" + state + "#wechat_redirect";

                    content = "你有新的消息，主题：" + content + ", <a href=\"" + url + "\">点击查询详情</a>";
                    HttpResult result = wechatAction.sendTextMessageToWechat(userId, content, Settings.Default.Wechat_Secret, appId);

                }

              
            }
            this.Cursor = Cursors.Default;
            
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.CurrentDirectory;
            // openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "PDF(*.pdf)|*.pdf";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                Cursor.Current = Cursors.WaitCursor;
              
            }
        }

        private void cboAgentNo_TextUpdate(object sender, EventArgs e)
        {
            //清空combobox
            this.cboAgentNo.Items.Clear();
            //清空listNew
            listNew.Clear();
            //遍历全部备查数据
            foreach (var item in listOnit)
            {
                if (item.Contains(this.cboAgentNo.Text))
                {
                    //符合，插入ListNew
                    listNew.Add(item);
                }
            }
            //combobox添加已经查到的关键词
            this.cboAgentNo.Items.AddRange(listNew.ToArray());
            //设置光标位置，否则光标位置始终保持在第一列，造成输入关键词的倒序排列
            this.cboAgentNo.SelectionStart = this.cboAgentNo.Text.Length;
            //保持鼠标指针原来状态，有时候鼠标指针会被下拉框覆盖，所以要进行一次设置。
            Cursor = Cursors.Default;
            //自动弹出下拉框
            this.cboAgentNo.DroppedDown = true;
        }

        

      
    }
}
