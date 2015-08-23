using ChinaUnion_Agent.Properties;
using ChinaUnion_Agent.Util;
using ChinaUnion_Agent.Wechat;
using Newtonsoft.Json;
using System;
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
        public frmBroadcast()
        {
            InitializeComponent();
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

           
            foreach (String item in this.lstModule.CheckedItems)
            {
                switch (item)
                {
                    
                    case "佣金结算与支付查询"://12
                        sendMessage(12);
                        break;
                    case "报错处理"://2
                        sendMessage(2);
                        break;
                    case "通知公告与促销政策"://6
                        sendMessage(6);
                        break;
                    case "业绩查询"://7
                        sendMessage(7);
                        break;
                    case "服务监督"://9
                        sendMessage(9);
                        break;
                    case "投诉协查"://10
                        sendMessage(10);
                        break;
                    case "在线学习"://11
                        sendMessage(11);
                        break;
                    case "企业小助手"://0
                        sendMessage(0);
                        break;
                }

            }
            this.Cursor = Cursors.Default;
            MessageBox.Show("发送完毕");
           
           
        }

        private void sendMessage(int agentId)
        {
            WechatAction wechatAction = new WechatAction();
            HttpResult result = wechatAction.sendTextMessageToWechat("@all", this.txtContent.Text.Trim(), Wechat_secretId, agentId);
            this.Cursor = Cursors.Default;
          //  if (result.StatusCode == System.Net.HttpStatusCode.OK)
           // {
                //表示访问成功，具体的大家就参考HttpStatusCode类
               // MessageBox.Show("发送成功");
               // this.Close();
            //}
           // else
            //{
              //  MessageBox.Show(result.StatusDescription);
           // }
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
