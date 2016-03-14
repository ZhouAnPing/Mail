using ChinaUnion_BO;
using ChinaUnion_DataAccess;
using System;
using System.Collections;
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
        IList<AgentType> agentTypeList = null;
        IList<AgentWechatAccount> agentWechatAccountList = null;
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
            agentTypeList = agentTypeDao.GetDistinctType();
            this.lstAgentType.Items.Clear();
            this.lstAllType.Items.Clear();
            this.lstAssignType.Items.Clear();
          //  this.lstAgentType.Items.Add("所有渠道");
            foreach (AgentType agentType in agentTypeList)
            {
                this.lstAgentType.Items.Add(agentType.agentType);

                this.lstAllType.Items.Add(agentType.agentType);
            }


            AgentWechatAccountDao agentWechatAccountDao = new AgentWechatAccountDao();
             agentWechatAccountList = agentWechatAccountDao.GetAllAgentOrBranch();
            this.lstUser.Items.Clear();
            lstAllAgent.Items.Clear();
            lstAssignAgent.Items.Clear();
            //  this.lstAgentType.Items.Add("所有渠道");
            ArrayList list = new ArrayList();
            foreach (AgentWechatAccount agentWechatAccount in agentWechatAccountList)
            {
                if (!String.IsNullOrEmpty(agentWechatAccount.regionName))
                {
                    if (!list.Contains(agentWechatAccount.branchNo))
                    {
                        this.lstUser.Items.Add(agentWechatAccount.branchNo + ":" + agentWechatAccount.branchName);
                        this.lstAllAgent.Items.Add(agentWechatAccount.branchNo + ":" + agentWechatAccount.branchName);
                        list.Add(agentWechatAccount.branchNo);
                    }
                }
                else
                {
                    if (!String.IsNullOrEmpty(agentWechatAccount.branchNo))
                    {
                        if (!list.Contains(agentWechatAccount.branchNo))
                        {
                            this.lstUser.Items.Add(agentWechatAccount.branchNo + ":" + agentWechatAccount.branchName);
                            this.lstAllAgent.Items.Add(agentWechatAccount.branchNo + ":" + agentWechatAccount.branchName);
                            list.Add(agentWechatAccount.branchNo);
                        }
                    }
                    else
                    {
                        if (!list.Contains(agentWechatAccount.agentNo))
                        {
                            this.lstUser.Items.Add(agentWechatAccount.agentNo + ":" + agentWechatAccount.agentName);
                            this.lstAllAgent.Items.Add(agentWechatAccount.agentNo + ":" + agentWechatAccount.agentName);
                            list.Add(agentWechatAccount.agentNo);
                        }
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
                if (currentRowIndex<dgGroup.RowCount&&this.dgGroup[0, currentRowIndex] != null && this.dgGroup[0, currentRowIndex].Value != null)
                {
                    this.initControl();
                    currentRowIndex = dgGroup.CurrentRow.Index;
                    Group group = groupDao.Get(this.dgGroup[0, currentRowIndex].Value.ToString());
                    if (group != null)
                    {
                        
                        this.txtGroupName.Text = group.groupName;
                        this.txtDescription.Text = group.description;
                        this.txtGroupName.Enabled = false;
                        lstAssignType.Items.Clear();
                        lstAssignAgent.Items.Clear();

                        IList<UserDefinedGroup> userDefinedGroupList = userDefinedGroupDao.GetList(group.groupName);
                        foreach (UserDefinedGroup userDefinedGroup in userDefinedGroupList)
                        {
                            if (userDefinedGroup.type.Equals("渠道类型"))
                            {
                                this.lstAssignType.Items.Add(userDefinedGroup.member);
                               // int index = this.lstAgentType.FindStringExact(userDefinedGroup.member);
                               // if (index >= 0)
                               // {
                                    //lstAgentType.SetItemCheckState(index, CheckState.Checked);
                               // }
                            }
                            if (userDefinedGroup.type.Equals("代理商/渠道"))
                            {
                               int index= this.lstAllAgent.FindString(userDefinedGroup.member);
                               if (index >= 0)
                               {
                                   this.lstAssignAgent.Items.Add(lstAllAgent.Items[index]);
                               }
                               // int index = this.lstUser.FindString(userDefinedGroup.member);
                               // if (index >= 0)
                                //{
                                   // lstUser.SetItemCheckState(index, CheckState.Checked);
                               // }
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
           
            foreach ( Object item in this.lstAssignType.Items)
            {
                UserDefinedGroup userDefinedGroup = new UserDefinedGroup();
                userDefinedGroup.groupName = group.groupName;
                userDefinedGroup.member = item.ToString();
                userDefinedGroup.type = "渠道类型";
                userDefinedGroupDao.Add(userDefinedGroup);
            }

            foreach (Object item in this.lstAssignAgent.Items)
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

        private void txtType_TextChanged(object sender, EventArgs e)
        {
            /*
            foreach (String item in lstAllType.Items)
            {
                if (item.Contains(this.txtType.Text.Trim()))
                {
                    lstAllType.SelectedItem = item;
                    return;
                }
            }
              */
            lstAllType.Items.Clear();
            
            foreach (AgentType item in this.agentTypeList)
            {
                if (String.IsNullOrEmpty(this.txtType.Text.Trim()))
                {

                    this.lstAllType.Items.Add(item.agentType);

                    continue;
                }

                if (item.agentType.Contains(this.txtType.Text.Trim()) && !lstAllType.Items.Contains(item))
                {
                    this.lstAllType.Items.Add(item.agentType);
                }
            }
        }

        private void txtAgent_TextChanged(object sender, EventArgs e)
        {
            /*
            foreach (String item in lstAllAgent.Items)
            {
                if (item.Contains(this.txtAgent.Text.Trim()))
                {
                    this.lstAllAgent.SelectedItem = item;
                    return;
                }
            }*/
            lstAllAgent.Items.Clear();
            foreach (AgentWechatAccount agentWechatAccount in this.agentWechatAccountList)
            {
                String item = "";
                if (!String.IsNullOrEmpty(agentWechatAccount.regionName))
                {
                    // this.lstUser.Items.Add(agentWechatAccount.branchNo + ":" + agentWechatAccount.branchName);
                    item = agentWechatAccount.branchNo + ":" + agentWechatAccount.branchName;
                }
                else
                {
                    if (!String.IsNullOrEmpty(agentWechatAccount.branchNo))
                    {
                        //  this.lstUser.Items.Add(agentWechatAccount.branchNo + ":" + agentWechatAccount.branchName);
                        item = agentWechatAccount.branchNo + ":" + agentWechatAccount.branchName;

                    }
                    else
                    {
                        // this.lstUser.Items.Add(agentWechatAccount.agentNo + ":" + agentWechatAccount.agentName);
                        item = agentWechatAccount.agentNo + ":" + agentWechatAccount.agentName;

                    }
                }
                if (String.IsNullOrEmpty(this.txtAgent.Text.Trim()))
                {

                    this.lstAllAgent.Items.Add(item);

                    continue;
                }

                if (item.Contains(this.txtAgent.Text.Trim())&& !this.lstAgentType.Items.Contains(item))
                {
                    this.lstAllAgent.Items.Add(item);
                }
            }


        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            foreach (String item in lstAllType.SelectedItems)
            {
                if (!lstAssignType.Items.Contains(item))
                {
                    lstAssignType.Items.Add(item);    
                }
            }
            lstAssignType.Refresh();
        }

        private void btnRightAll_Click(object sender, EventArgs e)
        {
            foreach (String item in lstAllType.Items)
            {
                if (!lstAssignType.Items.Contains(item))
                {
                    lstAssignType.Items.Add(item);
                }
            }
            lstAssignType.Refresh();
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {

            if (lstAssignType.SelectedItem != null)
            {
                lstAssignType.Items.Remove(this.lstAssignType.SelectedItem);
                lstAssignType.Refresh();

            }
            
        }

        private void btnLeftAll_Click(object sender, EventArgs e)
        {
            lstAssignType.Items.Clear();
        }

        private void lstAllType_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lstAllType.SelectedItem != null)
            {
                if (!lstAssignType.Items.Contains(lstAllType.SelectedItem))
                {
                    lstAssignType.Items.Add(lstAllType.SelectedItem);
                    lstAssignType.Refresh();
                }
            }
        }

        private void lstAssignType_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lstAssignType.SelectedItem != null)
            {

                lstAssignType.Items.Remove(this.lstAssignType.SelectedItem);
                lstAssignType.Refresh();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (String item in lstAllAgent.SelectedItems)
            {
                if (!lstAssignAgent.Items.Contains(item))
                {
                    lstAssignAgent.Items.Add(item);
                }
            }
            lstAssignAgent.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (String item in lstAllAgent.Items)
            {
                if (!lstAssignAgent.Items.Contains(item))
                {
                    lstAssignAgent.Items.Add(item);
                }
            }
            lstAssignAgent.Refresh();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (lstAssignAgent.SelectedItem != null)
            {
                lstAssignAgent.Items.Remove(this.lstAssignAgent.SelectedItem);
                lstAssignAgent.Refresh();

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            lstAssignAgent.Items.Clear();
        }

        private void lstAllAgent_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lstAllAgent.SelectedItem != null)
            {
                if (!lstAssignAgent.Items.Contains(lstAllAgent.SelectedItem))
                {
                    lstAssignAgent.Items.Add(lstAllAgent.SelectedItem);
                    lstAssignAgent.Refresh();
                }
            }
        }

        private void lstAssignAgent_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lstAssignAgent.SelectedItem != null)
            {
                lstAssignAgent.Items.Remove(this.lstAssignAgent.SelectedItem);
                lstAssignAgent.Refresh();
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            frmUserGroupImport frmUserGroupImport = new frmUserGroupImport();
            frmUserGroupImport.ShowDialog();
            this.btnQuery_Click(sender, e);
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (this.lstAssignAgent.Items.Count <= 0)
            {
                return;
            }
            this.Cursor = Cursors.WaitCursor;

            

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "csv格式|*.csv";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.FileName = "代理商.csv";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {


                try
                {
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(saveFileDialog.FileName, false, System.Text.Encoding.GetEncoding("gb2312")); //写入流 

                 
                        sw.Write("代理商编号");
                        //sw.Write(",");
                  
                    sw.WriteLine();// sw.Write("\r\n");

                    foreach (Object item in this.lstAssignAgent.Items)
                    {
                       
                            sw.Write(item.ToString().Substring(0, item.ToString().IndexOf(":")));
                            //sw.Write(",");
                       
                        sw.WriteLine();// ("\r\n");
                    }
                    sw.Flush();
                    sw.Close();
                    MessageBox.Show("成功导出[" + lstAssignAgent.Items.Count.ToString() + "]行数据！");
                }
                catch
                {
                    MessageBox.Show("导出失败！");
                }
            }


            this.Cursor = Cursors.WaitCursor;

        }

    }
}
