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
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ChinaUnion_Agent.WechatForm
{
    public partial class frmAddWechatUser : Form
    {
        private string secret = "JH40o2nk6C6Q3Ym9tjVzSl9WD3x3c5sPwP8HzE5sccNW9CjoYCEugEzI4XyHNEnj";
        public WechatJsonUser wechatJsonUser = new WechatJsonUser();
        WechatAction wechatAction = new WechatAction();

        public frmAddWechatUser()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            
            if (String.IsNullOrEmpty(this.txtName.Text.Trim()))
            {
                MessageBox.Show("姓名不能为空");
                this.txtName.Focus();
                return;
            }
            if (String.IsNullOrEmpty(this.txtUserId.Text.Trim()))
            {
                MessageBox.Show("账号不能为空");
                this.txtUserId.Focus();
                return;
            }

            if (String.IsNullOrEmpty(this.txtWeixinId.Text.Trim()) && String.IsNullOrEmpty(this.txtMobile.Text.Trim()) && String.IsNullOrEmpty(this.txtEmail.Text.Trim()))
            {
                MessageBox.Show("微信号，手机和邮箱不能同时为空");               
                return;
            }

            String EmailReg = @"^\w+([\.\-]\w+)*\@\w+([\.\-]\w+)*\.\w+$";
            if (!String.IsNullOrEmpty(txtEmail.Text.Trim())&&!Regex.IsMatch(txtEmail.Text.Trim(), EmailReg))
            {
                MessageBox.Show("请输入有效的邮箱地址");
                this.txtEmail.Focus();
                return;
            }

            String MobileReg = @"^[1]+[3,5,8]+\d{9}";
            if (!String.IsNullOrEmpty(txtMobile.Text.Trim()) && !Regex.IsMatch(this.txtMobile.Text.Trim(), MobileReg))
            {
                MessageBox.Show("请输入有效的手机号码");
                this.txtMobile.Focus();
                return;
            }
            this.Cursor = Cursors.WaitCursor;
            wechatJsonUser.name = this.txtName.Text.Trim();
            wechatJsonUser.userid = this.txtUserId.Text.Trim();
            wechatJsonUser.weixinid = this.txtWeixinId.Text.Trim();
            wechatJsonUser.mobile = this.txtMobile.Text.Trim();
            wechatJsonUser.email = this.txtEmail.Text.Trim();
            wechatJsonUser.position = this.txtPosition.Text.Trim();

             String msgJson = JsonConvert.SerializeObject(wechatJsonUser, Formatting.Indented);
            HttpResult httpResult = null;
            if (String.IsNullOrEmpty(wechatJsonUser.userid))
            {
                httpResult = wechatAction.addUserToWechat(this.secret, msgJson);
            }
            else
            {
                httpResult = wechatAction.updateUserToWechat(this.secret, msgJson);
            }
             ReturnMessage returnMessage = (ReturnMessage)JsonConvert.DeserializeObject(httpResult.Html, typeof(ReturnMessage));
             this.Cursor = Cursors.Default;
            if (returnMessage != null && returnMessage.errcode.Equals("0"))
             {
                 DialogResult = System.Windows.Forms.DialogResult.OK;
             }
             else
             {
                 MessageBox.Show("操作失败," + returnMessage.getErrorDescrition());
                 return;

             }
          
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void frmAddWechatUser_Load(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(wechatJsonUser.userid))
            {
                this.txtUserId.Enabled = true;
            }
            else
            {
                this.txtUserId.Enabled = false;
            }
        }
    }
}
