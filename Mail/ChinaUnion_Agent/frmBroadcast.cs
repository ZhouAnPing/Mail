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

namespace ChinaUnion_Agent
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
            this.Cursor = Cursors.WaitCursor;
            WechatAction wechatAction = new WechatAction();
            HttpResult result = wechatAction.sendTextMessageToWechat("@all", this.txtContent.Text.Trim(), Wechat_secretId, Wechar_AppId);
           this.Cursor = Cursors.Default; 
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //表示访问成功，具体的大家就参考HttpStatusCode类
                MessageBox.Show("发送成功");
                this.Close();
            }
            else
            {
                MessageBox.Show(result.StatusDescription);
            }
           
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
