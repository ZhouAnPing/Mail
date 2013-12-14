using Example.cn.tripolis.dialogue.import;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            String contactDatabaseId = "MjU1MTI1NTFFUVus6S83qA";//Dialogue Contact Database Id
            String groupId = "NzUxODU3NTEqAxK*euR6yw";//Dialogue Contact Group Id
            String fileName = "Contacts.xls";//Ftp File Name
            String ftpAccountId = "NTM5NTM5NTNW_uqPXJDMzQ";//Ftp account in Dialogue
            String reportReceiverAddress = "zapjx@hotmail.com";
            DateTime scheduleAt = DateTime.Now;

            String result;


            try
            {

                ImportService importService = new ImportService();
                AuthInfo importAuthInfo = new AuthInfo();
                importAuthInfo.client = "Training";
                importAuthInfo.username = "zapjx@hotmail.com";
                importAuthInfo.password = "Test123";
                importService.authInfo = importAuthInfo;

                ImportContactsFromFtpRequest request = new ImportContactsFromFtpRequest();

                request.contactDatabaseId = contactDatabaseId;
                string[] contactGroupIds = new string[1];
                contactGroupIds[0] = groupId;
                request.contactGroupIds = contactGroupIds;
                request.extension = cn.tripolis.dialogue.import.fileExtension.XLS;
                request.fileName = fileName;
                request.ftpAccountId = ftpAccountId;
                request.importMode = cn.tripolis.dialogue.import.importMode.ADD_TO_GROUP;
                request.reportReceiverAddress = reportReceiverAddress;
                request.scheduleAt = scheduleAt;

                ImportIDResponse response = importService.importContactsFromFtp(request);

                result = response.importId;


            }
            catch (System.Web.Services.Protocols.SoapException ex)
            {

                result = ex.Detail.InnerXml;

            }

        }
    }
}
