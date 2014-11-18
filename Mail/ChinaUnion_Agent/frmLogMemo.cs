using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ChinaUnion_Agent
{
    public partial class frmLogMemo : Form
    {
        public frmLogMemo()
        {
            InitializeComponent();
        }

        private void frmLogMemo_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;   
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
