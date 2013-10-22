using System;

namespace TRPGChatRoom.GUI
{
    partial class FrmLogin
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
            this.btnServer = new System.Windows.Forms.Button();
            this.lbName = new System.Windows.Forms.Label();
            this.lbIP = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.btnClient = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnServer
            // 
            this.btnServer.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnServer.Location = new System.Drawing.Point(58, 141);
            this.btnServer.Name = "btnServer";
            this.btnServer.Size = new System.Drawing.Size(116, 38);
            this.btnServer.TabIndex = 0;
            this.btnServer.Text = "建立主机";
            this.btnServer.UseVisualStyleBackColor = true;
            this.btnServer.Click += new System.EventHandler(this.button1_Click);
            // 
            // lbName
            // 
            this.lbName.Location = new System.Drawing.Point(24, 33);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(41, 19);
            this.lbName.TabIndex = 1;
            this.lbName.Text = "昵称";
            this.lbName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbIP
            // 
            this.lbIP.Location = new System.Drawing.Point(24, 78);
            this.lbIP.Name = "lbIP";
            this.lbIP.Size = new System.Drawing.Size(41, 19);
            this.lbIP.TabIndex = 2;
            this.lbIP.Text = "IP";
            this.lbIP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.ImeMode = System.Windows.Forms.ImeMode.On;
            this.txtName.Location = new System.Drawing.Point(71, 29);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(142, 26);
            this.txtName.TabIndex = 3;
            this.txtName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtName.WordWrap = false;
            // 
            // txtIP
            // 
            this.txtIP.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIP.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txtIP.Location = new System.Drawing.Point(71, 74);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(142, 26);
            this.txtIP.TabIndex = 4;
            this.txtIP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnClient
            // 
            this.btnClient.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClient.Location = new System.Drawing.Point(58, 203);
            this.btnClient.Name = "btnClient";
            this.btnClient.Size = new System.Drawing.Size(116, 38);
            this.btnClient.TabIndex = 5;
            this.btnClient.Text = "加入主机";
            this.btnClient.UseVisualStyleBackColor = true;
            this.btnClient.Click += new System.EventHandler(this.button2_Click);
            // 
            // FrmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(244, 275);
            this.Controls.Add(this.btnClient);
            this.Controls.Add(this.txtIP);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lbIP);
            this.Controls.Add(this.lbName);
            this.Controls.Add(this.btnServer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmLogin";
            this.Text = "TRPGChatRoom";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmLogin_FormClosed);
            this.Load += new System.EventHandler(this.FrmLogin_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnServer;
        private System.Windows.Forms.Label lbName;
        private System.Windows.Forms.Label lbIP;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.Button btnClient;
    }
}

