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

namespace ChinaUnion_Agent.UserManagement
{
    public partial class frmUserGroup : Form
    {
        private GroupDao groupDao = new GroupDao();
        private UserDefinedGroupDao userDefinedGroupDao = new UserDefinedGroupDao();
    

        public frmUserGroup()
        {
            InitializeComponent();
        }

        private void frmGroupManagement_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            initControl();
        }

        private void prepareGrid(String group)
        {
            this.Cursor = Cursors.WaitCursor;


            IList<Group> groupList = groupDao.GetAll(group);

            this.dgGroup.Rows.Clear();
            dgGroup.Columns.Clear();

            dgGroup.Columns.Add("名称", "名称");
            dgGroup.Columns.Add("描述", "描述");

            if (groupList != null && groupList.Count > 0)
            {
                for (int i = 0; i < groupList.Count; i++)
                {
                    dgGroup.Rows.Add();
                    DataGridViewRow row = dgGroup.Rows[dgGroup.RowCount - 1];
                    row.Cells[0].Value = groupList[i].groupName;
                    row.Cells[1].Value = groupList[i].description;
                }
            }
            dgGroup.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
            dgGroup.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgGroup.AutoResizeColumns();
            this.Cursor = Cursors.Default;
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            
            this.Cursor = Cursors.WaitCursor;

            prepareGrid(this.txtKeyword.Text.Trim());

            this.Cursor = Cursors.Default;
        }

        private void initControl()
        {
            this.Cursor = Cursors.WaitCursor;
            this.btnSave.Enabled = false;
            this.btnDelete.Enabled = false;
            this.txtGroupName.Enabled = true;
            //this.btnClear.Enabled = true;
            this.txtGroupName.Clear();
            this.txtDescription.Clear();
            AgentTypeDao agentTypeDao = new AgentTypeDao();
            IList<AgentType> agentTypeList = agentTypeDao.GetDistinctType();
            this.lstAgentType.Items.Clear();
          //  this.lstAgentType.Items.Add("所有渠道");
            foreach (AgentType agentType in agentTypeList)
            {
                this.lstAgentType.Items.Add(agentType.agentType);
            }


            AgentWechatAccountDao agentWechatAccountDao = new AgentWechatAccountDao();
            IList<AgentWechatAccount> agentWechatAccountList = agentWechatAccountDao.GetAllAgentOrBranch();
            this.lstUser.Items.Clear();
            //  this.lstAgentType.Items.Add("所有渠道");
            foreach (AgentWechatAccount agentWechatAccount in agentWechatAccountList)
            {
                if (!String.IsNullOrEmpty(agentWechatAccount.regionName))
                {
                    this.lstUser.Items.Add(agentWechatAccount.branchNo + ":" + agentWechatAccount.branchName);
                }
                else
                {
                    if (!String.IsNullOrEmpty(agentWechatAccount.branchNo))
                    {
                        this.lstUser.Items.Add(agentWechatAccount.branchNo + ":" + agentWechatAccount.branchName);

                    }
                    else
                    {
                        this.lstUser.Items.Add(agentWechatAccount.agentNo + ":" + agentWechatAccount.agentName);

                    }
                }
            }

            this.Cursor = Cursors.Default;
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            initControl();

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtGroupName_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(this.txtGroupName.Text.Trim()))
            {
                this.btnDelete.Enabled = false;
                this.btnSave.Enabled = false;
            }
            else
            {
                this.btnDelete.Enabled = true;
                this.btnSave.Enabled = true;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.initControl();
        }

        int currentRowIndex = 0;
        private void dgGroup_SelectionChanged(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            if (dgGroup.CurrentRow != null)
            {
                if (this.dgGroup[0, currentRowIndex].Value != null)
                {
                    this.initControl();
                    currentRowIndex = dgGroup.CurrentRow.Index;
                    Group group = groupDao.Get(this.dgGroup[0, currentRowIndex].Value.ToString());
                    if (group != null)
                    {
                        
                        this.txtGroupName.Text = group.groupName;
                        this.txtDescription.Text = group.description;
                        this.txtGroupName.Enabled = false;

                        IList<UserDefinedGroup> userDefinedGroupList = userDefinedGroupDao.GetList(group.groupName);
                        foreach (UserDefinedGroup userDefinedGroup in userDefinedGroupList)
                        {
                            if (userDefinedGroup.type.Equals("渠道类型"))
                            {
                                int index = this.lstAgentType.FindStringExact(userDefinedGroup.member);
                                if (index >= 0)
                                {
                                    lstAgentType.SetItemCheckState(index, CheckState.Checked);
                                }
                            }
                            if (userDefinedGroup.type.Equals("代理商/渠道"))
                            {
                                int index = this.lstUser.FindString(userDefinedGroup.member);
                                if (index >= 0)
                                {
                                    lstUser.SetItemCheckState(index, CheckState.Checked);
                                }
                            }
                        }

                       // this.btnClear.Enabled = false;

                      
                    }
                }
            }
            this.Cursor = Cursors.Default;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(this.txtGroupName.Text.Trim()))
            {
                MessageBox.Show("请输入组名！");
                this.txtGroupName.Focus();
                return;
            }
           
            this.Cursor = Cursors.WaitCursor;
            Group group = new Group();
            group.groupName = this.txtGroupName.Text.Trim();
            group.description = this.txtDescription.Text.Trim();
           
            userDefinedGroupDao.Delete(group.groupName);
            groupDao.Delete(group);
            groupDao.Add(group);
           
            foreach ( Object item in this.lstAgentType.CheckedItems)
            {
                UserDefinedGroup userDefinedGroup = new UserDefinedGroup();
                userDefinedGroup.groupName = group.groupName;
                userDefinedGroup.member = item.ToString();
                userDefinedGroup.type = "渠道类型";
                userDefinedGroupDao.Add(userDefinedGroup);
            }

            foreach (Object item in this.lstUser.CheckedItems)
            {
                UserDefinedGroup userDefinedGroup = new UserDefinedGroup();
                userDefinedGroup.groupName = group.groupName;
                userDefinedGroup.member = item.ToString().Substring(0, item.ToString().IndexOf(":"));
                userDefinedGroup.type = "代理商/渠道";
                userDefinedGroupDao.Add(userDefinedGroup);
            }
            this.prepareGrid(this.txtKeyword.Text);
            MessageBox.Show("操作完成");

            this.Cursor = Cursors.Default;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgGroup.CurrentRow == null)
            {
                MessageBox.Show("请从组列表中选择选择一行进行删除");
                return;
            }
            this.Cursor = Cursors.WaitCursor;
            String groupName = dgGroup.CurrentRow.Cells[0].Value.ToString();
            userDefinedGroupDao.Delete(groupName);
            Group group = new Group();
            group.groupName = this.txtGroupName.Text.Trim();
            group.description = this.txtDescription.Text.Trim();
            groupDao.Delete(group);
            this.prepareGrid(this.txtKeyword.Text);
           
            this.Cursor = Cursors.Default;
            MessageBox.Show("操作完成");
        }

    }
}
