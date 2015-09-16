using ChinaUnion_Agent.Properties;
using ChinaUnion_Agent.Util;
using ChinaUnion_Agent.Wechat;
using ChinaUnion_BO;
using ChinaUnion_DataAccess;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChinaUnion_Agent.PolicyForm
{
    public partial class frmBroadcast : Form
    {
        public String Wechat_secretId = Settings.Default.Wechat_Secret;
        public int Wechar_AppId = Settings.Default.Wechat_Agent_AppId;
        GroupDao groupDao = new GroupDao();
        public frmBroadcast()
        {
            InitializeComponent();

            IList<Group> groupList = groupDao.GetAll("");
            this.lstGroup.Items.Clear();

            foreach (Group group in groupList)
            {
                this.lstGroup.Items.Add(group.groupName);
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            
            if (this.txtContent.Text.Trim() == "")
            {
                MessageBox.Show("请输入公告内容");
                return;
            }

            if (this.lstModule.CheckedItems.Count<=0)
            {
                MessageBox.Show("请选择发送对象");
                return;
            }
            
            this.Cursor = Cursors.WaitCursor;

            UserDefinedGroupDao userDefinedGroupDao = new UserDefinedGroupDao();

            IList<String> list = new List<string>();
            foreach (String item in this.lstGroup.CheckedItems)
            {
                list.Add(item);
            }

            IList<String> recieveList = userDefinedGroupDao.GetReceiverList(list);
           


            foreach (String item in this.lstModule.CheckedItems)
            {
                switch (item)
                {
                    
                    case "佣金结算与支付查询"://12
                        sendMessage(12, recieveList);
                        break;
                    case "报错处理"://2
                        sendMessage(2, recieveList);
                        break;
                    case "通知公告与促销政策"://6
                        sendMessage(6, recieveList);
                        break;
                    case "业绩查询"://7
                        sendMessage(7, recieveList);
                        break;
                    case "服务监督"://9
                        sendMessage(9, recieveList);
                        break;
                    case "投诉协查"://10
                        sendMessage(10, recieveList);
                        break;
                    case "在线学习"://11
                        sendMessage(11, recieveList);
                        break;
                    case "企业小助手"://0
                        sendMessage(0, recieveList);
                        break;
                }

            }
            this.Cursor = Cursors.Default;
            MessageBox.Show("发送完毕");
           
           
        }

        private void sendMessage(int agentId, IList<String> recieveList)
        {
            
            WechatAction wechatAction = new WechatAction();
            if (recieveList == null || recieveList.Count <= 0)
            {
                HttpResult result = wechatAction.sendTextMessageToWechat("@all", this.txtContent.Text.Trim(), Wechat_secretId, agentId);
            }
            else
            {

                //var result1 = recieveList.GroupBy(i => int.Parse(i) % 10).Select(g => g.ToList()).ToList();

                

                 List<String> userIdsBuffer = new List<string>();
                 for (int i = 1; i <= recieveList.Count; i++)
                 {
                     userIdsBuffer.Add(recieveList[i - 1]);
                     if (i % 500 == 0 || i == recieveList.Count)
                     {
                         string userId = "";
                         userId = string.Join("|", userIdsBuffer.ToArray());
                         userIdsBuffer.Clear();
                         HttpResult result = wechatAction.sendTextMessageToWechat(userId, this.txtContent.Text.Trim(), Wechat_secretId, agentId);

                         
                     }
                 }
              
              

            }
                this.Cursor = Cursors.Default;
         
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default; 
            this.Close();
        }

        private void frmBroadcast_Load(object sender, EventArgs e)
        {
            this.BackgroundImage = Properties.Resources.Desktop;
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }
    }
}
