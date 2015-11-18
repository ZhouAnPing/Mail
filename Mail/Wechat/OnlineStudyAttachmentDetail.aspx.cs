using ChinaUnion_BO;
using ChinaUnion_DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wechat
{
    public partial class OnlineStudyAttachmentDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string sequence = Request.QueryString["seq"];
            string userId = Request.QueryString["userId"];
            StudyDao studyDao = new ChinaUnion_DataAccess.StudyDao();
            Study study = studyDao.Get(Int32.Parse(sequence));

            if (study != null)
            {
                StudyReceiverLogDao studyReceiverLogDao = new StudyReceiverLogDao();
                StudyReceiverLog studyReceiverLog = new StudyReceiverLog();
                studyReceiverLog.studySequence = study.sequence;
                studyReceiverLog.readtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                studyReceiverLog.userId = userId;
                studyReceiverLogDao.Add(studyReceiverLog);

                Response.ContentType = "Application/pdf";
                this.Response.Clear();

                System.IO.Stream fs = this.Response.OutputStream;
                fs.Write(study.attachment, 0, study.attachment.Length);

                fs.Close();
                this.Response.End();
            }

        }
    }
}