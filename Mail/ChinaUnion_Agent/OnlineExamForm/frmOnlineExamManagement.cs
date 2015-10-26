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
    public partial class frmOnlineExamManagement : frmBase
    {
        public frmOnlineExamManagement()
        {
            InitializeComponent();
        }

        private void frmOnlineExamManagement_Load(object sender, EventArgs e)
        {

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            frmNewExam frmNewExam = new frmNewExam();
            frmNewExam.ShowIcon = false;
            frmNewExam.ShowInTaskbar = false;
            frmNewExam.StartPosition = FormStartPosition.CenterScreen;
            frmNewExam.TopMost = true;
            frmNewExam.Show();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            prepareGrid(this.txtSearchCondition.Text.Trim());
        }

        private void prepareGrid(String condition)
        {
            this.Cursor = Cursors.WaitCursor;
            ExamDao examDao = new ChinaUnion_DataAccess.ExamDao();
            IList<Exam> examList = examDao.GetList(condition);
            this.dgExam.Rows.Clear();
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

                    ExamQuestionDao examQuestionDao = new ExamQuestionDao();
                    IList<ExamQuestion> questionList = examQuestionDao.GetList(examSeq);

                    this.dgExamSingleChoice.Rows.Clear();
                    dgExamSingleChoice.Columns.Clear();
                    dgExamSingleChoice.Columns.Add("序列号", "序列号");
                    dgExamSingleChoice.Columns.Add("题目", "题目");
                    dgExamSingleChoice.Columns.Add("答案", "答案");
                    dgExamSingleChoice.Columns.Add("选项1", "选项1");
                    dgExamSingleChoice.Columns.Add("选项2", "选项2");
                    dgExamSingleChoice.Columns.Add("选项3", "选项3");
                    dgExamSingleChoice.Columns.Add("选项4", "选项4");
                    dgExamSingleChoice.Columns.Add("选项5", "选项5");
                    dgExamSingleChoice.Columns.Add("选项6", "选项6");
                    dgExamSingleChoice.Columns.Add("选项7", "选项7");
                    dgExamSingleChoice.Columns[0].Visible = false;

                    this.dgExamMultiChoice.Rows.Clear();
                    dgExamMultiChoice.Columns.Clear();
                    dgExamMultiChoice.Columns.Add("序列号", "序列号");
                    dgExamMultiChoice.Columns.Add("题目", "题目");
                    dgExamMultiChoice.Columns.Add("答案", "答案");
                    dgExamMultiChoice.Columns.Add("选项1", "选项1");
                    dgExamMultiChoice.Columns.Add("选项2", "选项2");
                    dgExamMultiChoice.Columns.Add("选项3", "选项3");
                    dgExamMultiChoice.Columns.Add("选项4", "选项4");
                    dgExamMultiChoice.Columns.Add("选项5", "选项5");
                    dgExamMultiChoice.Columns.Add("选项6", "选项6");
                    dgExamMultiChoice.Columns.Add("选项7", "选项7");
                    dgExamMultiChoice.Columns[0].Visible = false;

                    this.dgExamJugement.Rows.Clear();
                    dgExamJugement.Columns.Clear();
                    dgExamJugement.Columns.Add("序列号", "序列号");
                    dgExamJugement.Columns.Add("题目", "题目");
                    dgExamJugement.Columns.Add("答案", "答案");
                    dgExamJugement.Columns[0].Visible = false;
                    if (questionList != null && questionList.Count > 0)
                    {
                        for (int i = 0; i < questionList.Count; i++)
                        {
                            ExamQuestion examQuestion = questionList[i];
                            DataGridViewRow row = null;
                            switch (examQuestion.questionType)
                            {
                                case "Single":
                                    dgExamSingleChoice.Rows.Add();
                                     row = dgExamSingleChoice.Rows[dgExamSingleChoice.RowCount - 1];
                                    row.Cells["序列号"].Value = examQuestion.sequence;
                                    row.Cells["题目"].Value = examQuestion.question;
                                    row.Cells["答案"].Value = examQuestion.answer;
                                    row.Cells["选项1"].Value = examQuestion.option1;
                                    row.Cells["选项2"].Value = examQuestion.option2;
                                    row.Cells["选项3"].Value = examQuestion.option3;
                                    row.Cells["选项4"].Value = examQuestion.option4;
                                    row.Cells["选项5"].Value = examQuestion.option5;
                                    row.Cells["选项6"].Value = examQuestion.option6;
                                    row.Cells["选项7"].Value = examQuestion.option7;


                                    break;
                                case "Multi":
                                    dgExamMultiChoice.Rows.Add();
                                    row = dgExamMultiChoice.Rows[dgExamMultiChoice.RowCount - 1];
                                    row.Cells["序列号"].Value = examQuestion.sequence;
                                    row.Cells["题目"].Value = examQuestion.question;
                                    row.Cells["答案"].Value = examQuestion.answer;
                                    row.Cells["选项1"].Value = examQuestion.option1;
                                    row.Cells["选项2"].Value = examQuestion.option2;
                                    row.Cells["选项3"].Value = examQuestion.option3;
                                    row.Cells["选项4"].Value = examQuestion.option4;
                                    row.Cells["选项5"].Value = examQuestion.option5;
                                    row.Cells["选项6"].Value = examQuestion.option6;
                                    row.Cells["选项7"].Value = examQuestion.option7;
                                    break;
                                case "Jugement":
                                     this.dgExamJugement.Rows.Add();
                                     row = dgExamJugement.Rows[dgExamJugement.RowCount - 1];
                                    row.Cells["序列号"].Value = examQuestion.sequence;
                                    row.Cells["题目"].Value = examQuestion.question;
                                    row.Cells["答案"].Value = examQuestion.answer;
                                    break;

                            }
                        }
                    }

                }
            }
            this.Cursor = Cursors.Default;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
             this.Cursor = Cursors.WaitCursor;

             if (this.dgExam.CurrentRow != null)
             {
                 if (this.dgExam[0, this.dgExam.CurrentRow.Index].Value != null)
                 {

                     String examSeq = this.dgExam[0, this.dgExam.CurrentRow.Index].Value.ToString();

                     ExamQuestionDao examQuestionDao = new ExamQuestionDao();
                     examQuestionDao.Delete(examSeq);

                     ExamDao examDao = new ExamDao();
                     examDao.Delete(examSeq);
                     prepareGrid(this.txtSearchCondition.Text.Trim());
                 }
             }
        }
    }
}
