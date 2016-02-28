using ChinaUnion_Agent.Properties;
using ChinaUnion_Agent.Util;
using ChinaUnion_Agent.Wechat;
using ChinaUnion_BO;
using ChinaUnion_DataAccess;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ChinaUnion_Agent.StudyForm
{
    public partial class frmStudyPublish : frmBase
    {
        StudyDao studyDao = new StudyDao();
        AgentTypeDao agentTypeDao = new AgentTypeDao();
        GroupDao groupDao = new GroupDao();
        StudyReceiverDao studyReceiverDao = new StudyReceiverDao();
        public frmStudyPublish()
        {
            InitializeComponent();
        }

        private void frmStudyPublish_Load(object sender, EventArgs e)
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
           // this.cbType.Text = "";
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
            for (int i = 0; i < this.chkAgentType.Items.Count; i++)
            {
                this.chkAgentType.SetItemChecked(i, false);
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

            IList<Study> studyList = studyDao.GetAllList(condition);
            this.dgStudy.Rows.Clear();
            dgStudy.Columns.Clear();
            dgStudy.Columns.Add("序列号", "序列号");
           
            dgStudy.Columns.Add("类型", "类型");
            dgStudy.Columns.Add("标题", "标题");
            dgStudy.Columns.Add("内容", "内容");
            dgStudy.Columns.Add("附件", "附件");
            dgStudy.Columns.Add("有效期", "有效期");
            dgStudy.Columns.Add("创建人", "创建人");
            dgStudy.Columns.Add("创建时间", "创建时间");
            dgStudy.Columns[0].Visible = false;
            if (studyList != null && studyList.Count > 0)
            {

                for (int i = 0; i < studyList.Count; i++)
                {
                    dgStudy.Rows.Add();
                    DataGridViewRow row = dgStudy.Rows[dgStudy.RowCount - 1];
                    row.Cells[0].Value = studyList[i].sequence;
                    row.Cells[1].Value = studyList[i].type;
                    row.Cells[2].Value = studyList[i].subject;
                    row.Cells[3].Value = studyList[i].content;
                    row.Cells[4].Value = studyList[i].attachmentName;
                    row.Cells[5].Value = studyList[i].validateStartTime;
                    row.Cells[6].Value = studyList[i].sender;
                    row.Cells[7].Value = studyList[i].creatTime;

                }
            }
           // this.cbType.Text = "";
            this.dtStartDate.ResetText();
            this.txtAttachmentLocation.Clear();
            this.txtContent.Clear();
            this.txtSubject.Clear();
            this.txtSequence.Clear();
            this.txtAttachmentName.Text = "";

            this.dgStudy.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            this.dgStudy.AutoResizeColumns();
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
            Study study = new Study();
            study.sequence = this.txtSequence.Text.Trim();
            study.type = "在线学习";
            study.subject = this.txtSubject.Text.Trim();
            study.content = this.txtContent.Text.Trim();
            study.sender = this.loginUser.name;
            study.validateStartTime = this.dtStartDate.Value.ToString("yyyy-MM-dd");
            study.validateEndTime = this.dtEndDate.Value.ToString("yyyy-MM-dd");
            study.creatTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            study.isDelete = "N";
            study.isValidate = "Y";
            study.agentType = "";
            foreach (object item in this.chkAgentType.CheckedItems)
            {
                study.agentType = study.agentType + item.ToString()+";";
            }
          //  study.agentType = this.cboAgentType.Text;
            if (this.lstAgentType.CheckedItems.Contains("所有渠道"))
            {
                study.toAll = "Y";
            }
            else
            {
                study.toAll = "N";
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
                    study.attachmentName = this.txtAttachmentName.Text;
                    study.attachment = attachmentBytes;
                }

            }
            else
            {
                if (!String.IsNullOrEmpty(study.sequence))
                {
                    Study tempStudy = this.studyDao.Get(Int32.Parse(study.sequence));
                    study.attachment = tempStudy.attachment;
                    study.attachmentName = tempStudy.attachmentName;
                }
            }
            if (!String.IsNullOrEmpty(study.sequence))
            {

                this.studyDao.Update(study);

            }
            else
            {
                studyDao.Add(study);

                study = studyDao.GetBySubject(study.subject);
            }

            studyReceiverDao.Delete(study.sequence);


            for (int i = 0; i < lstAgentType.Items.Count; i++)
            {
                if (lstAgentType.GetItemChecked(i))
                {
                    StudyReceiver studyReceiver = new StudyReceiver();
                    studyReceiver.studySequence = study.sequence;
                    studyReceiver.receiver = lstAgentType.Items[i].ToString();
                    studyReceiver.type = "渠道类型";
                    studyReceiverDao.Add(studyReceiver);
                }
            }


            for (int i = 0; i < lstGroup.Items.Count; i++)
            {
                if (lstGroup.GetItemChecked(i))
                {
                    StudyReceiver studyReceiver = new StudyReceiver();
                    studyReceiver.studySequence = study.sequence;
                    studyReceiver.receiver = lstGroup.Items[i].ToString();
                    studyReceiver.type = "自定义组";
                    studyReceiverDao.Add(studyReceiver);
                }
            }

            this.prepareGrid(this.txtSearchCondition.Text);
            MessageBox.Show("操作完成");

            this.Cursor = Cursors.Default;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.dgStudy.CurrentRow == null)
            {
                MessageBox.Show("请从列表中选择选择一行进行删除");
                return;
            }
            this.Cursor = Cursors.WaitCursor;
            int seq = (Int32.Parse(this.dgStudy.CurrentRow.Cells[0].Value.ToString()));

            this.studyReceiverDao.Delete(seq.ToString());
            this.studyDao.Delete(seq);
            this.prepareGrid(this.txtSearchCondition.Text);

            this.Cursor = Cursors.Default;
            MessageBox.Show("操作完成");
        }

        private void dgStudy_SelectionChanged(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            if (this.dgStudy.CurrentRow != null)
            {
                if (this.dgStudy[0,this.dgStudy.CurrentRow.Index].Value != null)
                {
                    this.initControl();
                   
                    Study study = studyDao.Get(Int32.Parse(this.dgStudy[0, this.dgStudy.CurrentRow.Index].Value.ToString()));
                    if (study != null)
                    {
                        this.btnPublish.Enabled = true;
                        this.txtSubject.Text = study.subject;
                        this.txtContent.Text = study.content;
                        this.txtSequence.Text = study.sequence;
                        this.txtAttachmentName.Text = study.attachmentName;
                        //this.cbType.Text = study.type;
                       // this.cboAgentType.Text = study.agentType;
                        if (!String.IsNullOrEmpty(study.agentType))
                        {
                            IList<String> list = study.agentType.Split(';').ToList<String>();
                            for (int i = 0; i < chkAgentType.Items.Count; i++)
                            {
                                if (list.Contains(chkAgentType.Items[i].ToString()))
                                {
                                    chkAgentType.SetItemChecked(i, true);
                                }
                            }
                 
                        }
                       
                        this.dtStartDate.Value = DateTime.Parse(study.validateStartTime);
                        this.dtEndDate.Value = DateTime.Parse(study.validateEndTime);

                        IList<StudyReceiver> studyReceiverList = studyReceiverDao.GetList(study.sequence);
                        if (studyReceiverList != null)
                        {
                            
                            foreach (StudyReceiver studyReceiver in studyReceiverList)
                            {
                                if (studyReceiver.type.Equals("渠道类型"))
                                {
                                    int index = this.lstAgentType.FindStringExact(studyReceiver.receiver);
                                    if (index >= 0)
                                    {
                                        lstAgentType.SetItemCheckState(index, CheckState.Checked);
                                    }
                                }
                                if (studyReceiver.type.Equals("自定义组"))
                                {
                                    int index = this.lstGroup.FindStringExact(studyReceiver.receiver);
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
            int appId = MyConstant.APP_Online;

            AgentWechatAccountDao agentWechatAccountDao = new ChinaUnion_DataAccess.AgentWechatAccountDao();
            String agentType = "";

            foreach (object item in this.chkAgentType.CheckedItems)
            {
                agentType =  item.ToString() ;
                IList<String> list = agentWechatAccountDao.GetListByType( agentType);

                List<String> userIdsBuffer1 = new List<string>();
                for (int i = 1; i <= list.Count; i++)
                {
                    userIdsBuffer1.Add(list[i - 1]);
                    if (i % 500 == 0 || i == list.Count)
                    {
                        string userId = "";
                        userId = string.Join("|", userIdsBuffer1.ToArray());
                        userIdsBuffer1.Clear();

                        WechatAction wechatAction = new WechatAction();

                        String content = this.txtSubject.Text.Trim();
                        String state = this.txtSequence.Text;
                        String url = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=wx31204de5a3ae758e&redirect_uri=http%3a%2f%2f112.64.17.80%2fwechat%2fOnlineStudyDetail.aspx&response_type=code&scope=snsapi_base&state=" + state + "#wechat_redirect";

                        content = "你有新的消息，主题：" + content + ", <a href=\"" + url + "\">点击查询详情</a>";
                        HttpResult result = wechatAction.sendTextMessageToWechat(userId, content, Settings.Default.Wechat_Secret, appId);

                    }


                }
                 
            }
               
     
            IList<String> UserIdList = studyDao.GetAllUserIdListBySeq(this.txtSequence.Text);

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
                    String url = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=wx31204de5a3ae758e&redirect_uri=http%3a%2f%2f112.64.17.80%2fwechat%2fOnlineStudyDetail.aspx&response_type=code&scope=snsapi_base&state=" + state + "#wechat_redirect";

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
            
            Study study = studyDao.Get(Int32.Parse(this.txtSequence.Text));
            if (study != null)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "PDF|*.pdf";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.FileName = study.attachmentName;
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                   // System.IO.File.WriteAllBytes(saveFileDialog.FileName, study.attachment);

                    try
                    {
                        string saveFileName = saveFileDialog.FileName;
                        int arraysize = new int();
                        arraysize = study.attachment.GetUpperBound(0);
                        FileStream fs = new FileStream(saveFileName, FileMode.OpenOrCreate, FileAccess.Write);
                        fs.Write(study.attachment, 0, arraysize);
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
