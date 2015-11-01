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

namespace ChinaUnion_Agent.OnlineExamForm
{
    public partial class frmAgentScoreQuery : frmBase
    {
        public frmAgentScoreQuery()
        {
            InitializeComponent();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            prepareGrid(this.txtSearchCondition.Text.Trim());
        }

        private void prepareGrid(String condition)
        {
            String type = "";
            if (rdoExam.Checked)
            {
                type = "Exam";
            }
            if (rdoSurvey.Checked)
            {
                type = "Survey";
            }
            if (String.IsNullOrEmpty(type))
            {
                MessageBox.Show("请选择试题类型");

                return;
            }

            this.Cursor = Cursors.WaitCursor;
            ExamDao examDao = new ChinaUnion_DataAccess.ExamDao();
            IList<Exam> examList = examDao.GetList(condition, type);
            this.dgExam.Rows.Clear();
            this.dgScore.Rows.Clear();
            
            dgExam.Columns.Clear();
            dgExam.Columns.Add("序列号", "序列号");
            dgExam.Columns.Add("名称", "名称");

            dgExam.Columns.Add("生效时间", "生效时间");
            dgExam.Columns.Add("失效时间", "失效时间");

            dgExam.Columns[0].Visible = false;
            if (examList != null && examList.Count > 0)
            {

                for (int i = 0; i < examList.Count; i++)
                {
                    dgExam.Rows.Add();
                    DataGridViewRow row = dgExam.Rows[dgExam.RowCount - 1];
                    row.Cells[0].Value = examList[i].sequence;

                    row.Cells[1].Value = examList[i].subject;

                    row.Cells[2].Value = examList[i].validateStartTime;
                    row.Cells[3].Value = examList[i].validateEndTime;


                }
            }


            this.dgExam.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            this.dgExam.AutoResizeColumns();
            this.Cursor = Cursors.Default;
        }
        private void dgExam_SelectionChanged(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            if (this.dgExam.CurrentRow != null)
            {
                if (this.dgExam[0, this.dgExam.CurrentRow.Index].Value != null)
                {

                    String examSeq = this.dgExam[0, this.dgExam.CurrentRow.Index].Value.ToString();

                    AgentExamDao examDao = new ChinaUnion_DataAccess.AgentExamDao();
                    IList<AgentExam> examList = examDao.GetList(examSeq);
                    this.dgScore.Rows.Clear();
                    dgScore.Columns.Clear();
                    dgScore.Columns.Add("用户编号", "用户编号");
                    dgScore.Columns.Add("成绩", "成绩");
                    foreach (AgentExam exam in examList)
                    {
                        AgentExamScoreDao agentExamScoreDao = new AgentExamScoreDao();
                        IList<ExamQuestion> examQuestionList = agentExamScoreDao.GetUserExamQuestion(examSeq, exam.userId);
                        int wrongCount = 0;
                        foreach (ExamQuestion examQuestion in examQuestionList)
                        {

                            if (examQuestion.standardAnswer != null && !examQuestion.standardAnswer.Equals(examQuestion.answer))
                            {

                                wrongCount++;
                            }
                        }
                        int totalCount = examQuestionList.Count;
                        int rightCount = totalCount - wrongCount;
                        String strMessage = "总共：" + totalCount + "题,答对：" + rightCount + "题,答错：" + wrongCount;



                        dgScore.Rows.Add();
                        DataGridViewRow row = dgScore.Rows[dgScore.RowCount - 1];
                        row.Cells["用户编号"].Value = exam.userId;
                        row.Cells["成绩"].Value = strMessage;

                        dgScore.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
                        dgScore.AutoResizeColumns();
                        //this.dgScore.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        //this.dgScore.AutoResizeColumns();
                    }



                }

            }
            this.Cursor = Cursors.Default;
        }
    }

}
