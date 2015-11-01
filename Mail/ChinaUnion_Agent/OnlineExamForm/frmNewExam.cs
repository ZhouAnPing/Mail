using ChinaUnion_BO;
using ChinaUnion_DataAccess;
using LinqToExcel;
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
    public partial class frmNewExam : Form
    {
        public frmNewExam()
        {
            InitializeComponent();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.CurrentDirectory;
            // openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Excel(*.xlsx)|*.xlsx|Excel 2000-2003(*.xls)|*.xls|CSV(*.csv)|*.csv|所有文件(*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                Cursor.Current = Cursors.WaitCursor;
                string FileName = openFileDialog.FileName;

                var execelfile = new ExcelQueryFactory(FileName);

               

                List<string> sheetNames = execelfile.GetWorksheetNames().ToList();





                //单选题
                List<Row> singleList = execelfile.Worksheet("单选题").ToList(); ;

                if (singleList != null && singleList.Count > 0)
                {

                   
                    this.dgExamSingleChoice.Rows.Clear();
                    dgExamSingleChoice.Columns.Clear();
                    foreach (String coloumn in singleList[0].ColumnNames)
                    {
                        this.dgExamSingleChoice.Columns.Add(coloumn, coloumn);
                    }

                    for (int i = 0; i < singleList.Count; i++)
                    {
                        if (String.IsNullOrEmpty(singleList[i][0]))
                            continue;
                        dgExamSingleChoice.Rows.Add();
                        DataGridViewRow row = dgExamSingleChoice.Rows[i];
                        foreach (String coloumn in singleList[0].ColumnNames)
                        {
                            row.Cells[coloumn].Value = singleList[i][coloumn];
                        }

                    }
                }

                //多选题
                List<Row> multiList = execelfile.Worksheet("多选题").ToList(); ;

                if (multiList != null && multiList.Count > 0)
                {

                    this.dgExamMultiChoice.Rows.Clear();
                    dgExamMultiChoice.Columns.Clear();
                    foreach (String coloumn in multiList[0].ColumnNames)
                    {
                        this.dgExamMultiChoice.Columns.Add(coloumn, coloumn);
                    }

                    for (int i = 0; i < multiList.Count; i++)
                    {
                        if (String.IsNullOrEmpty(multiList[i][0]))
                            continue;
                        dgExamMultiChoice.Rows.Add();
                        DataGridViewRow row = dgExamMultiChoice.Rows[i];
                        foreach (String coloumn in multiList[0].ColumnNames)
                        {
                            row.Cells[coloumn].Value = multiList[i][coloumn];
                        }

                    }
                }

                //多选题
                List<Row> judgementList = execelfile.Worksheet("判断题").ToList(); ;

                if (judgementList != null && judgementList.Count > 0)
                {

                    this.dgExamJugement.Rows.Clear();
                    dgExamJugement.Columns.Clear();
                    foreach (String coloumn in judgementList[0].ColumnNames)
                    {
                        this.dgExamJugement.Columns.Add(coloumn, coloumn);
                    }

                    for (int i = 0; i < judgementList.Count; i++)
                    {
                        if (String.IsNullOrEmpty(judgementList[i][0]))
                            continue;
                        dgExamJugement.Rows.Add();
                        DataGridViewRow row = dgExamJugement.Rows[i];
                        foreach (String coloumn in judgementList[0].ColumnNames)
                        {
                            row.Cells[coloumn].Value = judgementList[i][coloumn];
                        }

                    }
                }



                this.dgExamSingleChoice.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

                dgExamSingleChoice.AutoResizeColumns();

                Cursor.Current = Cursors.Default;
            }
        }

        private void frmNewExam_Load(object sender, EventArgs e)
        {
           
            AgentTypeDao agentTypeDao = new AgentTypeDao();
            GroupDao groupDao = new GroupDao();
            this.dtStartDate.Value = DateTime.Now;
            this.dtEndDate.Value = DateTime.Now.AddMonths(1);
            IList<AgentType> agentTypeList = agentTypeDao.GetDistinctType();
            this.lstAgentType.Items.Clear();
            this.lstAgentType.Items.Add("所有渠道");
            foreach (AgentType agentType in agentTypeList)
            {
                this.lstAgentType.Items.Add(agentType.agentType);
            }

            IList<Group> groupList = groupDao.GetAll("");
            this.lstGroup.Items.Clear();

            foreach (Group group in groupList)
            {
                this.lstGroup.Items.Add(group.groupName);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (String.IsNullOrEmpty(this.txtExamName.Text.Trim()))
            {
                MessageBox.Show("名称不能为空");
                txtExamName.Focus();
                return;
            }
            if (this.dtEndDate.Value.CompareTo(this.dtStartDate.Value) <= 0)
            {
                MessageBox.Show("有效期结束时间必须大于开始时间");

                return;
            }
            this.Cursor = Cursors.WaitCursor;

            Exam exam = new Exam();
            if (rdoExam.Checked)
            {
                exam.type = "Exam";
            }
            if (rdoSurvey.Checked)
            {
                exam.type = "Survey";
            }
            if (String.IsNullOrEmpty(exam.type))
            {
                MessageBox.Show("请选择试题类型");

                return;
            }
            exam.subject = this.txtExamName.Text;
            //exam.type = "Exam";
            exam.validateStartTime = this.dtStartDate.Value.ToString("yyyy-MM-dd");
            exam.validateEndTime = this.dtEndDate.Value.ToString("yyyy-MM-dd");
            exam.creatTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");            
            exam.isValidate = "Y";
            exam.agentType = this.cboAgentType.Text;

            ExamDao examDao = new ExamDao();
            examDao.Add(exam);

            exam = examDao.GetByName(exam.subject);
            if (exam != null)
            {
                ExamQuestionDao examQuestionDao = new ExamQuestionDao();

                for (int i = 0; i < this.dgExamSingleChoice.RowCount; i++)
                {
                    ExamQuestion examQuestion = new ExamQuestion();
                    examQuestion.question = dgExamSingleChoice[0, i].Value.ToString();
                    examQuestion.answer = dgExamSingleChoice[1, i].Value.ToString();
                    examQuestion.option1 = dgExamSingleChoice[2, i].Value.ToString();
                    examQuestion.option2 = dgExamSingleChoice[3, i].Value.ToString();
                    examQuestion.option3 = dgExamSingleChoice[4, i].Value.ToString();
                    examQuestion.option4 = dgExamSingleChoice[5, i].Value.ToString();
                    examQuestion.option5 = dgExamSingleChoice[6, i].Value.ToString();
                    examQuestion.option6 = dgExamSingleChoice[7, i].Value.ToString();
                    examQuestion.option7 = dgExamSingleChoice[8, i].Value.ToString();
                   // examQuestion.option8 = dgExamSingleChoice[9, i].Value.ToString();
                    examQuestion.questionType = "Single";
                    examQuestion.exam_sequence = exam.sequence;
                    examQuestionDao.Add(examQuestion);

                }

                for (int i = 0; i < this.dgExamMultiChoice.RowCount; i++)
                {
                    ExamQuestion examQuestion = new ExamQuestion();
                    examQuestion.question = dgExamMultiChoice[0, i].Value.ToString();
                    examQuestion.answer = dgExamMultiChoice[1, i].Value.ToString();
                    examQuestion.option1 = dgExamMultiChoice[2, i].Value.ToString();
                    examQuestion.option2 = dgExamMultiChoice[3, i].Value.ToString();
                    examQuestion.option3 = dgExamMultiChoice[4, i].Value.ToString();
                    examQuestion.option4 = dgExamMultiChoice[5, i].Value.ToString();
                    examQuestion.option5 = dgExamMultiChoice[6, i].Value.ToString();
                    examQuestion.option6 = dgExamMultiChoice[7, i].Value.ToString();
                    examQuestion.option7 = dgExamMultiChoice[8, i].Value.ToString();
                  //  examQuestion.option8 = dgExamMultiChoice[9, i].Value.ToString();
                    examQuestion.questionType = "Multi";
                    examQuestion.exam_sequence = exam.sequence;
                    examQuestionDao.Add(examQuestion);

                }

                for (int i = 0; i < this.dgExamJugement.RowCount; i++)
                {
                    ExamQuestion examQuestion = new ExamQuestion();
                    examQuestion.question = dgExamJugement[0, i].Value.ToString();
                    examQuestion.answer = dgExamJugement[1, i].Value.ToString();
                    examQuestion.exam_sequence = exam.sequence;

                    examQuestion.questionType = "Jugement";

                    examQuestionDao.Add(examQuestion);

                }

                ExamReceiverDao examReceiverDao = new ChinaUnion_DataAccess.ExamReceiverDao();
                examReceiverDao.Delete(exam.sequence);


                for (int i = 0; i < lstAgentType.Items.Count; i++)
                {
                    if (lstAgentType.GetItemChecked(i))
                    {
                        ExamReceiver examReceiver = new ExamReceiver();
                        examReceiver.examSequence = exam.sequence;
                        examReceiver.receiver = lstAgentType.Items[i].ToString();
                        examReceiver.type = "渠道类型";
                        examReceiverDao.Add(examReceiver);
                    }
                }


                for (int i = 0; i < lstGroup.Items.Count; i++)
                {
                    if (lstGroup.GetItemChecked(i))
                    {
                        ExamReceiver examReceiver = new ExamReceiver();
                        examReceiver.examSequence = exam.sequence;
                        examReceiver.receiver = lstGroup.Items[i].ToString();
                        examReceiver.type = "自定义组";
                        examReceiverDao.Add(examReceiver);
                    }
                }
            }



            MessageBox.Show("操作完毕");

            this.Cursor = Cursors.Default;
        }
    }
}
