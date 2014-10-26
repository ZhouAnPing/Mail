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
    public partial class frmProgress : Form
    {
      

        public frmProgress(BackgroundWorker worker)
        {
            InitializeComponent();
            worker.ProgressChanged +=new ProgressChangedEventHandler(worker_ProgressChanged);
            worker.RunWorkerCompleted +=new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
        }
 
        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Close();
        }
 
        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //lblStatus.Text = "";
           
            this.lblText.Text =this.lblText.Text+ e.UserState.ToString();//主窗体传过来的值，通过e.UserState.ToString()来接受
        }
 
        //工作完成后执行的事件  
        public void OnProcessCompleted(object sender, EventArgs e)  
        {
            this.Close();  
        } 
    }
}
