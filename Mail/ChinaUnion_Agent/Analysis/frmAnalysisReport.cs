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

namespace ChinaUnion_Agent.Analysis
{
    public partial class frmAnalysisReport : frmBase
    {
        PolicyDao policyDao = new PolicyDao();
        AgentTypeDao agentTypeDao = new AgentTypeDao();
        public frmAnalysisReport()
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
           
           
          
           
            this.Cursor = Cursors.Default;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {

            this.Cursor = Cursors.WaitCursor;

           

            this.Cursor = Cursors.Default;

           
        }
        private void prepareGrid(String condition, String type,String agentNo)
        {
            this.Cursor = Cursors.WaitCursor;

            AgentComplianSuggestionDao agentComplianSuggestionDao = new ChinaUnion_DataAccess.AgentComplianSuggestionDao();

            IList<AgentComplianSuggestion> agentComplianSuggestionList = null;

            agentComplianSuggestionList = agentComplianSuggestionDao.GetListByKeyword(condition, type, agentNo, "");

          
            this.initControl();
            this.Cursor = Cursors.Default;
        }

      

       
       

      
    }
}
