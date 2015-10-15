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

namespace ChinaUnion_Agent.PolicyForm
{
    public partial class frmPolicyPublish : frmBase
    {
        PolicyDao policyDao = new PolicyDao();
        AgentTypeDao agentTypeDao = new AgentTypeDao();
        GroupDao groupDao = new GroupDao();
        PolicyReceiverDao policyReceiverDao = new PolicyReceiverDao();
        public frmPolicyPublish()
        {
            InitializeComponent();
        }

        private void frmPolicyPublish_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.initControl();
        }

        private void initControl()
        {
            this.Cursor = Cursors.WaitCursor;
            this.btnSave.Enabled = false;
            this.btnDelete.Enabled = false;
            this.btnPreview.Enabled = false;
            this.btnPublish.Enabled = false;
            this.cbType.Text = "";
            this.dtStartDate.Value = DateTime.Now;
            this.dtEndDate.Value = DateTime.Now.AddMonths(1);
            this.txtAttachmentLocation.Clear();
            this.txtContent.Clear();
            this.txtSubject.Clear();
            this.txtSequence.Clear();
            this.txtAttachmentName.Text = "";
            IList<AgentType> agentTypeList = agentTypeDao.GetDistinctType();
            this.lstAgentType.Items.Clear();
            this.lstAgentType.Items.Add("所有渠道");
            foreach(AgentType agentType in agentTypeList){
                this.lstAgentType.Items.Add(agentType.agentType);
            }

            IList<Group> groupList = groupDao.GetAll("");
            this.lstGroup.Items.Clear();

            foreach (Group group in groupList)
            {
                this.lstGroup.Items.Add(group.groupName);
            }

            this.txtSubject.Focus();
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

            IList<Policy> policyList = policyDao.GetAllList(condition);
            this.dgPolicy.Rows.Clear();
            dgPolicy.Columns.Clear();
            dgPolicy.Columns.Add("序列号", "序列号");
           
            dgPolicy.Columns.Add("类型", "类型");
            dgPolicy.Columns.Add("标题", "标题");
            dgPolicy.Columns.Add("内容", "内容");
            dgPolicy.Columns.Add("附件", "附件");
            dgPolicy.Columns.Add("有效期", "有效期");
            dgPolicy.Columns.Add("创建人", "创建人");
            dgPolicy.Columns.Add("创建时间", "创建时间");
            dgPolicy.Columns[0].Visible = false;
            if (policyList != null && policyList.Count > 0)
            {

                for (int i = 0; i < policyList.Count; i++)
                {
                    dgPolicy.Rows.Add();
                    DataGridViewRow row = dgPolicy.Rows[dgPolicy.RowCount - 1];
                    row.Cells[0].Value = policyList[i].sequence;
                    row.Cells[1].Value = policyList[i].type;
                    row.Cells[2].Value = policyList[i].subject;
                    row.Cells[3].Value = policyList[i].content;
                    row.Cells[4].Value = policyList[i].attachmentName;
                    row.Cells[5].Value = policyList[i].validateStartTime;
                    row.Cells[6].Value = policyList[i].sender;
                    row.Cells[7].Value = policyList[i].creatTime;

                }
            }
            this.cbType.Text = "";
            this.dtStartDate.ResetText();
            this.txtAttachmentLocation.Clear();
            this.txtContent.Clear();
            this.txtSubject.Clear();
            this.txtSequence.Clear();
            this.txtAttachmentName.Text = "";

            this.dgPolicy.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            this.dgPolicy.AutoResizeColumns();
            this.Cursor = Cursors.Default;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            this.initControl();
        }

        private void txtSubject_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(this.txtSubject.Text.Trim()))
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
            if (String.IsNullOrEmpty(this.cbType.Text.Trim()))
            {
                MessageBox.Show("请选择类型！");
                this.txtSubject.Focus();
                return;
            }
            if (String.IsNullOrEmpty(this.txtSubject.Text.Trim()))
            {
                MessageBox.Show("请输入名称！");
                this.txtSubject.Focus();
                return;
            }
            if (String.IsNullOrEmpty(this.txtContent.Text.Trim()))
            {
                MessageBox.Show("请输入内容！");
                this.txtContent.Focus();
                return;
            }
            if (this.dtEndDate.Value.CompareTo(this.dtStartDate.Value) <= 0)
            {
                MessageBox.Show("有效期结束时间必须大于开始时间");

                return;
            }

            this.Cursor = Cursors.WaitCursor;
            Policy policy = new Policy();
            policy.sequence = this.txtSequence.Text.Trim();
            policy.type = this.cbType.Text;
            policy.subject = this.txtSubject.Text.Trim();
            policy.content = this.txtContent.Text.Trim();
            policy.sender = this.loginUser.name;
            policy.validateStartTime = this.dtStartDate.Value.ToString("yyyy-MM-dd");
            policy.validateEndTime = this.dtEndDate.Value.ToString("yyyy-MM-dd");
            policy.creatTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            policy.isDelete = "N";
            policy.isValidate = "Y";
            policy.agentType = this.cboAgentType.Text;
            if (this.lstAgentType.CheckedItems.Contains("所有渠道"))
            {
                policy.toAll = "Y";
            }
            else
            {
                policy.toAll = "N";
            }

            byte[] b = new byte[0];
            String fullpath = this.txtAttachmentLocation.Text;
            if (!String.IsNullOrEmpty(fullpath))
            {
                FileStream fs = new FileStream(fullpath, FileMode.Open, FileAccess.Read);
                byte[] attachmentBytes = new byte[fs.Length];

                fs.Read(attachmentBytes, 0, System.Convert.ToInt32(fs.Length));

                //  BinaryReader br = new BinaryReader(fs);
                // attachmentBytes = br.ReadBytes(Convert.ToInt32(fs.Length));
                fs.Close();
                // br.Close();

                if (attachmentBytes.Length > 0)
                {
                    policy.attachmentName = this.txtAttachmentName.Text;
                    policy.attachment = attachmentBytes;
                }

            }
            else
            {
                if (!String.IsNullOrEmpty(policy.sequence))
                {
                    Policy tempPolicy = this.policyDao.Get(Int32.Parse(policy.sequence));
                    policy.attachment = tempPolicy.attachment;
                    policy.attachmentName = tempPolicy.attachmentName;
                }
            }
            if (!String.IsNullOrEmpty(policy.sequence))
            {

                this.policyDao.Update(policy);

            }
            else
            {
                policyDao.Add(policy);

                policy = policyDao.GetBySubject(policy.subject);
            }

            policyReceiverDao.Delete(policy.sequence);


            for (int i = 0; i < lstAgentType.Items.Count; i++)
            {
                if (lstAgentType.GetItemChecked(i))
                {
                    PolicyReceiver policyReceiver = new PolicyReceiver();
                    policyReceiver.policySequence = policy.sequence;
                    policyReceiver.receiver = lstAgentType.Items[i].ToString();
                    policyReceiver.type = "渠道类型";
                    policyReceiverDao.Add(policyReceiver);
                }
            }


            for (int i = 0; i < lstGroup.Items.Count; i++)
            {
                if (lstGroup.GetItemChecked(i))
                {
                    PolicyReceiver policyReceiver = new PolicyReceiver();
                    policyReceiver.policySequence = policy.sequence;
                    policyReceiver.receiver = lstGroup.Items[i].ToString();
                    policyReceiver.type = "自定义组";
                    policyReceiverDao.Add(policyReceiver);
                }
            }

            this.prepareGrid(this.txtSearchCondition.Text);
            MessageBox.Show("操作完成");

            this.Cursor = Cursors.Default;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.dgPolicy.CurrentRow == null)
            {
                MessageBox.Show("请从列表中选择选择一行进行删除");
                return;
            }
            this.Cursor = Cursors.WaitCursor;
            int seq = (Int32.Parse(this.dgPolicy.CurrentRow.Cells[0].Value.ToString()));

            this.policyReceiverDao.Delete(seq.ToString());
            this.policyDao.Delete(seq);
            this.prepareGrid(this.txtSearchCondition.Text);

            this.Cursor = Cursors.Default;
            MessageBox.Show("操作完成");
        }

        private void dgPolicy_SelectionChanged(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            if (this.dgPolicy.CurrentRow != null)
            {
                if (this.dgPolicy[0,this.dgPolicy.CurrentRow.Index].Value != null)
                {
                    this.initControl();
                   
                    Policy policy = policyDao.Get(Int32.Parse(this.dgPolicy[0, this.dgPolicy.CurrentRow.Index].Value.ToString()));
                    if (policy != null)
                    {
                        this.btnPublish.Enabled = true;
                        this.txtSubject.Text = policy.subject;
                        this.txtContent.Text = policy.content;
                        this.txtSequence.Text = policy.sequence;
                        this.txtAttachmentName.Text = policy.attachmentName;
                        this.cbType.Text = policy.type;
                        this.cboAgentType.Text = policy.agentType;
                        this.dtStartDate.Value = DateTime.Parse(policy.validateStartTime);
                        this.dtEndDate.Value = DateTime.Parse(policy.validateEndTime);

                        IList<PolicyReceiver> policyReceiverList = policyReceiverDao.GetList(policy.sequence);
                        if (policyReceiverList != null)
                        {
                            
                            foreach (PolicyReceiver policyReceiver in policyReceiverList)
                            {
                                if (policyReceiver.type.Equals("渠道类型"))
                                {
                                    int index = this.lstAgentType.FindStringExact(policyReceiver.receiver);
                                    if (index >= 0)
                                    {
                                        lstAgentType.SetItemCheckState(index, CheckState.Checked);
                                    }
                                }
                                if (policyReceiver.type.Equals("自定义组"))
                                {
                                    int index = this.lstGroup.FindStringExact(policyReceiver.receiver);
                                    if (index >= 0)
                                    {
                                        this.lstGroup.SetItemCheckState(index, CheckState.Checked);
                                    }
                                }
                            }
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
            switch (this.cbType.Text)
            {
                case "政策":
                    appId = 6;
                    break;
                case "通知公告/重点关注":
                    appId = 6;
                    break;
                case "服务规范":
                    appId = 9;
                    break;
            }
            IList<String> UserIdList = policyDao.GetAllAgentNoListBySeq(this.txtSequence.Text);

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

                    String content = this.txtSubject.Text.Trim();
                    String state = this.txtSequence.Text;
                    String url = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=wx31204de5a3ae758e&redirect_uri=http%3a%2f%2f112.64.17.80%2fwechat%2fBusinessPolicyDetail.aspx&response_type=code&scope=snsapi_base&state=" + state + "#wechat_redirect";

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
               this.txtAttachmentLocation.Text = openFileDialog.FileName;
               this.txtAttachmentName.Text = Path.GetFileName(openFileDialog.FileName); ;
            }
        }

        private void txtAttachmentName_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
            Policy policy = policyDao.Get(Int32.Parse(this.txtSequence.Text));
            if (policy != null)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "PDF|*.pdf";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.FileName = policy.attachmentName;
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                   // System.IO.File.WriteAllBytes(saveFileDialog.FileName, policy.attachment);

                    try
                    {
                        string saveFileName = saveFileDialog.FileName;
                        int arraysize = new int();
                        arraysize = policy.attachment.GetUpperBound(0);
                        FileStream fs = new FileStream(saveFileName, FileMode.OpenOrCreate, FileAccess.Write);
                        fs.Write(policy.attachment, 0, arraysize);
                        fs.Close();
                        if (MessageBox.Show("文件存储成功，是否立即打开文件？","保存文件",MessageBoxButtons.YesNoCancel) ==   System.Windows.Forms.DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(saveFileName);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("操作失败！");
                    }
                }
            }
        }

      
    }
}
