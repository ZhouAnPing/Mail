using ChinaUnion_BO;
using ChinaUnion_DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
            this.txtSubject.Clear();
            this.txtContent.Clear();
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

            IList<Policy> policyList = policyDao.GetList(condition);
            this.dgPolicy.Rows.Clear();
            dgPolicy.Columns.Clear();
            dgPolicy.Columns.Add("标题", "标题");
            dgPolicy.Columns.Add("内容", "内容");
            dgPolicy.Columns.Add("创建人", "创建人");
            dgPolicy.Columns.Add("创建时间", "创建时间");
            if (policyList != null && policyList.Count > 0)
            {

                for (int i = 0; i < policyList.Count; i++)
                {
                    dgPolicy.Rows.Add();
                    DataGridViewRow row = dgPolicy.Rows[dgPolicy.RowCount - 1];
                    row.Cells[0].Value = policyList[i].subject;
                    row.Cells[1].Value = policyList[i].content;
                    row.Cells[2].Value = policyList[i].sender;
                    row.Cells[3].Value = policyList[i].sendTime;

                }
            }
            this.dgPolicy.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
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
            if (String.IsNullOrEmpty(this.txtSubject.Text.Trim()))
            {
                MessageBox.Show("请输入标题！");
                this.txtSubject.Focus();
                return;
            }
            if (String.IsNullOrEmpty(this.txtContent.Text.Trim()))
            {
                MessageBox.Show("请输入正文！");
                this.txtContent.Focus();
                return;
            }
           
            this.Cursor = Cursors.WaitCursor;
            Policy policy = new Policy();
            policy.subject = this.txtSubject.Text.Trim();
            policy.content = this.txtContent.Text.Trim();
            policy.sender = this.loginUser.name;
            policy.sendTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            this.policyDao.Delete(policy.subject);
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
            String subject = this.dgPolicy.CurrentRow.Cells[0].Value.ToString();
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
                   
                    Policy policy = policyDao.Get(this.dgPolicy[0, this.dgPolicy.CurrentRow.Index].Value.ToString());
                    if (policy != null)
                    {
                        this.txtSubject.Text = policy.subject;
                        this.txtContent.Text = policy.content;
                        this.txtSubject.Enabled = false;
                        
                    }
                }
            }
            this.Cursor = Cursors.Default;
        }

        private void btnPublish_Click(object sender, EventArgs e)
        {
           // this.btnSave_Click(sender, e);
            Policy policy = new Policy();
            policy.subject = this.txtSubject.Text.Trim();
            policy.content = this.txtContent.Text.Trim();
            policy.sender = this.loginUser.name; 
            frmPublishPreview frmPublishPreview = new frmPublishPreview();
            frmPublishPreview.policy = policy;
            frmPublishPreview.ShowDialog();
            this.prepareGrid("");
        }
    }
}
