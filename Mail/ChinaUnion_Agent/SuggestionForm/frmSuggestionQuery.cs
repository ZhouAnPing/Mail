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

namespace ChinaUnion_Agent.SuggestionForm
{
    public partial class frmSuggestionQuery : frmBase
    {
      
        public frmSuggestionQuery()
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

            this.txtType.Clear();
            this.txtUserId.Clear();
            this.txtAgent.Clear();
            this.txtCreatetime.Clear();
            this.txtReadtime.Clear();
        
            this.txtContent.Clear();
            this.txtSubject.Clear();
            this.txtSequence.Clear();

            this.txtOnwerDepartment.Clear();
            this.txtOwnerReply.Clear();
            this.txtCheckStatus.Clear();
            this.txtReplyContent.Clear();
           
          
           
            this.Cursor = Cursors.Default;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {

            this.Cursor = Cursors.WaitCursor;

            prepareGrid(this.txtSearchCondition.Text.Trim(), this.cboType.Text,this.txtAgentNoSearch.Text.Trim());

            this.Cursor = Cursors.Default;

           
        }
        private void prepareGrid(String condition, String type,String agentNo)
        {
            this.Cursor = Cursors.WaitCursor;

            AgentComplianSuggestionDao agentComplianSuggestionDao = new ChinaUnion_DataAccess.AgentComplianSuggestionDao();

            IList<AgentComplianSuggestion> agentComplianSuggestionList = null;

            agentComplianSuggestionList = agentComplianSuggestionDao.GetListByKeyword(condition, type, agentNo, "");

            this.dgSuggestion.Rows.Clear();
            dgSuggestion.Columns.Clear();
            dgSuggestion.Columns.Add("序列号", "序列号");
            dgSuggestion.Columns.Add("类型", "类型");
            dgSuggestion.Columns.Add("标题", "标题");
            dgSuggestion.Columns.Add("内容", "内容");
            dgSuggestion.Columns.Add("代理商/门店", "代理商/门店");
            dgSuggestion.Columns.Add("吐槽建议用户", "吐槽建议用户");
            dgSuggestion.Columns.Add("创建时间", "创建时间");
            dgSuggestion.Columns.Add("阅读时间", "阅读时间");
            if (agentComplianSuggestionList != null && agentComplianSuggestionList.Count > 0)
            {

                for (int i = 0; i < agentComplianSuggestionList.Count; i++)
                {
                    dgSuggestion.Rows.Add();
                    DataGridViewRow row = dgSuggestion.Rows[dgSuggestion.RowCount - 1];
                    row.Cells[0].Value = agentComplianSuggestionList[i].sequence;
                    row.Cells[1].Value = agentComplianSuggestionList[i].type;
                    row.Cells[2].Value = agentComplianSuggestionList[i].subject;
                    row.Cells[3].Value = agentComplianSuggestionList[i].content;
                    row.Cells[4].Value = agentComplianSuggestionList[i].agentNo;
                    row.Cells[5].Value = agentComplianSuggestionList[i].userId;
                    row.Cells[6].Value = agentComplianSuggestionList[i].createTime;
                    row.Cells[7].Value = agentComplianSuggestionList[i].agentReadtime;

                }
            }


            dgSuggestion.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
            this.dgSuggestion.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dgSuggestion.AutoResizeColumns();
            this.initControl();
            this.Cursor = Cursors.Default;
        }

      

        private void txtReplyContent_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(this.txtReplyContent.Text.Trim()))
            {
                
                this.btnSave.Enabled = false;
                this.btnClear.Enabled = false;
               
            }
            else
            {
              
                this.btnSave.Enabled = true;
                this.btnClear.Enabled = true;
              
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (String.IsNullOrEmpty(this.txtReplyContent.Text.Trim()))
            {
                MessageBox.Show("请输入回复内容！");
                this.txtSubject.Focus();
                return;
            }


            this.Cursor = Cursors.WaitCursor;
            AgentComplianSuggestionDao agentComplianSuggestionDao = new ChinaUnion_DataAccess.AgentComplianSuggestionDao();
            AgentComplianSuggestion agentComplianSuggestion = new AgentComplianSuggestion();

            agentComplianSuggestion.subject = this.txtSubject.Text;
            agentComplianSuggestion.content = this.txtContent.Text;
            agentComplianSuggestion.sequence = Int32.Parse(this.txtSequence.Text);
            agentComplianSuggestion.type = this.txtType.Text;



            agentComplianSuggestion.userId = this.txtUserId.Text;
            agentComplianSuggestion.agentNo = this.txtAgent.Text;
            agentComplianSuggestion.createTime = this.txtCreatetime.Text;
            agentComplianSuggestion.agentReadtime = this.txtReadtime.Text;



            agentComplianSuggestion.ownerDepartment = this.txtOnwerDepartment.Text;
            agentComplianSuggestion.ownerReplyContent = this.txtOwnerReply.Text;
            agentComplianSuggestion.checkStatus = this.txtCheckStatus.Text;
            agentComplianSuggestion.replyContent = this.txtReplyContent.Text;
            agentComplianSuggestion.replyTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            agentComplianSuggestionDao.update(agentComplianSuggestion);


            prepareGrid(this.txtSearchCondition.Text.Trim(), this.cboType.Text, this.txtAgentNoSearch.Text.Trim());

            MessageBox.Show("操作完成");

            this.Cursor = Cursors.Default;
        }

       

        private void dgSuggestion_SelectionChanged(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            if (this.dgSuggestion.CurrentRow != null)
            {
                if (this.dgSuggestion[0,this.dgSuggestion.CurrentRow.Index].Value != null)
                {
                    this.initControl();
                   
                  
                    AgentComplianSuggestionDao agentComplianSuggestionDao = new ChinaUnion_DataAccess.AgentComplianSuggestionDao();
                    AgentComplianSuggestion agentComplianSuggestion = agentComplianSuggestionDao.Get(Int32.Parse(this.dgSuggestion[0, this.dgSuggestion.CurrentRow.Index].Value.ToString()));

                    if (agentComplianSuggestion != null)
                    {
                        this.txtSubject.Text = agentComplianSuggestion.subject;
                        this.txtContent.Text = agentComplianSuggestion.content;
                        this.txtSequence.Text = agentComplianSuggestion.sequence.ToString();
                        this.txtType.Text = agentComplianSuggestion.type;
                       

                       
                        this.txtUserId.Text = agentComplianSuggestion.userId;
                        this.txtAgent.Text = agentComplianSuggestion.agentNo;
                        this.txtCreatetime.Text = agentComplianSuggestion.createTime;
                        this.txtReadtime.Text = agentComplianSuggestion.agentReadtime;
                        if (!String.IsNullOrEmpty(this.txtReadtime.Text))
                        {
                            this.txtReadtime.BackColor = Color.Blue;
                        }
                        else
                        {
                            this.txtReadtime.BackColor = Color.Bisque;
                        }
                      

                        this.txtOnwerDepartment.Text = agentComplianSuggestion.ownerDepartment;
                        this.txtOwnerReply.Text = agentComplianSuggestion.ownerReplyContent;
                        this.txtCheckStatus.Text = agentComplianSuggestion.checkStatus;
                        this.txtReplyContent.Text = agentComplianSuggestion.replyContent;

                       // this.dtValidateDate.Value = DateTime.Parse(policy.validateTime);
                        
                    }
                }
            }
            this.Cursor = Cursors.Default;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.txtOnwerDepartment.Undo();
            this.txtOwnerReply.Undo();
            this.txtCheckStatus.Undo();
            this.txtReplyContent.Undo();
        }

       

      
    }
}
