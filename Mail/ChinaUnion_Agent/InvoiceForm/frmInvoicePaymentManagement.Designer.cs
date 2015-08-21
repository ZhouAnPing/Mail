﻿namespace ChinaUnion_Agent.InvoiceForm
{
    partial class frmInvoicePaymentManagement
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtInvoiceNo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAgentName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnFind = new System.Windows.Forms.Button();
            this.txtAgentNo = new System.Windows.Forms.TextBox();
            this.lblType = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.grpList = new System.Windows.Forms.GroupBox();
            this.dgInvoicePayment = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuModify = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.grpList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgInvoicePayment)).BeginInit();
            this.contextMenuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtInvoiceNo);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtAgentName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnExport);
            this.groupBox1.Controls.Add(this.btnFind);
            this.groupBox1.Controls.Add(this.txtAgentNo);
            this.groupBox1.Controls.Add(this.lblType);
            this.groupBox1.Controls.Add(this.btnCancel);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1016, 56);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // txtInvoiceNo
            // 
            this.txtInvoiceNo.Location = new System.Drawing.Point(553, 19);
            this.txtInvoiceNo.Name = "txtInvoiceNo";
            this.txtInvoiceNo.Size = new System.Drawing.Size(120, 21);
            this.txtInvoiceNo.TabIndex = 24;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(484, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 23;
            this.label2.Text = "发票编号：";
            // 
            // txtAgentName
            // 
            this.txtAgentName.Location = new System.Drawing.Point(275, 19);
            this.txtAgentName.Name = "txtAgentName";
            this.txtAgentName.Size = new System.Drawing.Size(187, 21);
            this.txtAgentName.TabIndex = 22;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(206, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 21;
            this.label1.Text = "代理商名称:";
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(781, 16);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 31);
            this.btnExport.TabIndex = 20;
            this.btnExport.Text = "数据导出";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnFind
            // 
            this.btnFind.AutoSize = true;
            this.btnFind.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnFind.Location = new System.Drawing.Point(679, 16);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(55, 31);
            this.btnFind.TabIndex = 17;
            this.btnFind.Text = "查询";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // txtAgentNo
            // 
            this.txtAgentNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAgentNo.Location = new System.Drawing.Point(81, 19);
            this.txtAgentNo.Name = "txtAgentNo";
            this.txtAgentNo.Size = new System.Drawing.Size(110, 21);
            this.txtAgentNo.TabIndex = 19;
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(12, 22);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(71, 12);
            this.lblType.TabIndex = 18;
            this.lblType.Text = "代理商编号:";
            // 
            // btnCancel
            // 
            this.btnCancel.AutoSize = true;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(893, 16);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(55, 31);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "关闭";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // grpList
            // 
            this.grpList.Controls.Add(this.dgInvoicePayment);
            this.grpList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpList.Location = new System.Drawing.Point(0, 56);
            this.grpList.Name = "grpList";
            this.grpList.Size = new System.Drawing.Size(1016, 685);
            this.grpList.TabIndex = 12;
            this.grpList.TabStop = false;
            this.grpList.Text = "发票支付信息";
            // 
            // dgInvoicePayment
            // 
            this.dgInvoicePayment.AllowUserToAddRows = false;
            this.dgInvoicePayment.AllowUserToDeleteRows = false;
            this.dgInvoicePayment.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(233)))), ((int)(((byte)(207)))));
            this.dgInvoicePayment.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgInvoicePayment.ContextMenuStrip = this.contextMenuStrip2;
            this.dgInvoicePayment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgInvoicePayment.Location = new System.Drawing.Point(3, 17);
            this.dgInvoicePayment.Name = "dgInvoicePayment";
            this.dgInvoicePayment.ReadOnly = true;
            this.dgInvoicePayment.RowHeadersWidth = 10;
            this.dgInvoicePayment.RowTemplate.Height = 23;
            this.dgInvoicePayment.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgInvoicePayment.Size = new System.Drawing.Size(1010, 665);
            this.dgInvoicePayment.TabIndex = 7;
            this.dgInvoicePayment.DoubleClick += new System.EventHandler(this.menuModify_Click);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuModify});
            this.contextMenuStrip2.Name = "contextMenuStrip1";
            this.contextMenuStrip2.Size = new System.Drawing.Size(95, 26);
            // 
            // menuModify
            // 
            this.menuModify.Name = "menuModify";
            this.menuModify.Size = new System.Drawing.Size(94, 22);
            this.menuModify.Text = "修改";
            this.menuModify.Click += new System.EventHandler(this.menuModify_Click);
            // 
            // frmInvoicePaymentManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(1016, 741);
            this.Controls.Add(this.grpList);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmInvoicePaymentManagement";
            this.Text = "发票支付记录管理";
            this.Load += new System.EventHandler(this.frmAgentInvoicePaymentManagement_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgInvoicePayment)).EndInit();
            this.contextMenuStrip2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.TextBox txtAgentNo;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox grpList;
        private System.Windows.Forms.DataGridView dgInvoicePayment;
        private System.Windows.Forms.TextBox txtAgentName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem menuModify;
        private System.Windows.Forms.TextBox txtInvoiceNo;
        private System.Windows.Forms.Label label2;
    }
}