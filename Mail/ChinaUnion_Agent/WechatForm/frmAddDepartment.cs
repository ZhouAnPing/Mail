using ChinaUnion_Agent.Wechat;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ChinaUnion_Agent.WechatForm
{
    public partial class frmAddDepartment : Form
    {
        public Department department = new Department();
        public frmAddDepartment()
        {
            InitializeComponent();
        }

        private void frmAddDepartment_Load(object sender, EventArgs e)
        {
            this.txtDepartment.Text = this.department.name;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(this.txtDepartment.Text.Trim()))
            {
                MessageBox.Show("部门名称不能为空");
                this.txtDepartment.Focus();
                return;
            }
            if (this.txtDepartment.Text.Length>32)
            {
                MessageBox.Show("部门名称长度不能超过32个字");
                this.txtDepartment.Focus();
                return;
            }
            this.department.name = this.txtDepartment.Text.Trim();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
