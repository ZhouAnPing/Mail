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
    public partial class frmUserModification : Form
    {
        public frmUserModification()
        {
            InitializeComponent();
        }

        private void frmUserModification_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }
    }
}
