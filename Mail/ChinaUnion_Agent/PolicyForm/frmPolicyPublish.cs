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
            this.dtValidateDate.ResetText();
            this.txtAttachmentLocation.Clear();
            this.txtContent.Clear();
            this.txtSubject.Clear();
            this.txtSequence.Clear();
            this.txtAttachmentName.Text = "";
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
                    row.Cells[5].Value = policyList[i].validateTime;
                    row.Cells[6].Value = policyList[i].sender;
                    row.Cells[7].Value = policyList[i].creatTime;

                }
            }
            this.cbType.Text = "";
            this.dtValidateDate.ResetText();
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
                this.btnPreview.Enabled = false;
                this.btnPublish.Enabled = false;
            }
            else
            {
                this.btnDelete.Enabled = true;
                this.btnSave.Enabled = true;
                this.btnPreview.Enabled = true;
                this.btnPublish.Enabled = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
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

            this.Cursor = Cursors.WaitCursor;
            Policy policy = new Policy();
            policy.sequence = this.txtSequence.Text.Trim();
            policy.type = this.cbType.Text;
            policy.subject = this.txtSubject.Text.Trim();
            policy.content = this.txtContent.Text.Trim();
            policy.sender = this.loginUser.name;
            policy.validateTime = this.dtValidateDate.Value.ToString("yyyy-MM-dd");
            policy.creatTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            policy.isDelete = "N";
            policy.isValidate = "Y";

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
                this.policyDao.Delete(Int32.Parse(policy.sequence));
            }
            policyDao.Add(policy);
            this.prepareGrid("");
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
            int subject = (Int32.Parse(this.dgPolicy.CurrentRow.Cells[0].Value.ToString()));
            this.policyDao.Delete(subject);
            this.prepareGrid("");

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
                        this.txtSubject.Text = policy.subject;
                        this.txtContent.Text = policy.content;
                        this.txtSequence.Text = policy.sequence;
                        this.txtAttachmentName.Text = policy.attachmentName;
                        this.cbType.Text = policy.type;
                        this.dtValidateDate.Value = DateTime.Parse(policy.validateTime);
                        
                    }
                }
            }
            this.Cursor = Cursors.Default;
        }

        private void btnPublish_Click(object sender, EventArgs e)
        {
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
            Policy policy = new Policy();
            if (!string.IsNullOrEmpty(this.txtSequence.Text.Trim()))
            {
                policy = policyDao.Get(Int32.Parse(this.txtSequence.Text.Trim()));
            }
            policy.type = this.cbType.Text;
            policy.validateTime = this.dtValidateDate.Value.ToString("yyyy-MM-dd hh:mm:ss");
            policy.subject = this.txtSubject.Text.Trim();
            policy.content = this.txtContent.Text.Trim();
            policy.sender = this.loginUser.name;
            policy.sequence = this.txtSequence.Text.Trim();
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
                    policy.attachment = attachmentBytes;
                    policy.attachmentName = this.txtAttachmentName.Text;
                }
            }
            frmPublishPreview frmPublishPreview = new frmPublishPreview();
            frmPublishPreview.policy = policy;
            frmPublishPreview.ShowDialog();
            this.prepareGrid("");
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
