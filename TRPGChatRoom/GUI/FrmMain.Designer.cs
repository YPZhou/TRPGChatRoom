namespace TRPGChatRoom.GUI
{
    partial class FrmMain
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
            this.sfmlView = new System.Windows.Forms.Label();
            this.sfmlTick = new System.Windows.Forms.Timer(this.components);
            this.txtChat = new System.Windows.Forms.TextBox();
            this.rtxtPublic = new System.Windows.Forms.RichTextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.lstUser = new System.Windows.Forms.ListBox();
            this.lbNotice = new System.Windows.Forms.Label();
            this.tabChat = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabChat.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // sfmlView
            // 
            this.sfmlView.Location = new System.Drawing.Point(0, 0);
            this.sfmlView.Name = "sfmlView";
            this.sfmlView.Size = new System.Drawing.Size(800, 480);
            this.sfmlView.TabIndex = 0;
            // 
            // sfmlTick
            // 
            this.sfmlTick.Interval = 40;
            this.sfmlTick.Tick += new System.EventHandler(this.Update);
            // 
            // txtChat
            // 
            this.txtChat.Location = new System.Drawing.Point(0, 721);
            this.txtChat.Name = "txtChat";
            this.txtChat.Size = new System.Drawing.Size(729, 21);
            this.txtChat.TabIndex = 1;
            // 
            // rtxtPublic
            // 
            this.rtxtPublic.Enabled = false;
            this.rtxtPublic.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rtxtPublic.Location = new System.Drawing.Point(3, 3);
            this.rtxtPublic.Name = "rtxtPublic";
            this.rtxtPublic.ReadOnly = true;
            this.rtxtPublic.Size = new System.Drawing.Size(787, 203);
            this.rtxtPublic.TabIndex = 2;
            this.rtxtPublic.Text = "";
            // 
            // btnSend
            // 
            this.btnSend.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSend.Location = new System.Drawing.Point(735, 721);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(65, 21);
            this.btnSend.TabIndex = 3;
            this.btnSend.Text = "发送";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.FrmMain_SendMessage);
            // 
            // lstUser
            // 
            this.lstUser.FormattingEnabled = true;
            this.lstUser.ItemHeight = 12;
            this.lstUser.Location = new System.Drawing.Point(806, 247);
            this.lstUser.Name = "lstUser";
            this.lstUser.Size = new System.Drawing.Size(198, 484);
            this.lstUser.TabIndex = 4;
            // 
            // lbNotice
            // 
            this.lbNotice.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbNotice.Font = new System.Drawing.Font("KaiTi", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbNotice.Location = new System.Drawing.Point(806, 0);
            this.lbNotice.Name = "lbNotice";
            this.lbNotice.Size = new System.Drawing.Size(198, 241);
            this.lbNotice.TabIndex = 5;
            this.lbNotice.Text = "文字信息";
            this.lbNotice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabChat
            // 
            this.tabChat.Controls.Add(this.tabPage1);
            this.tabChat.Location = new System.Drawing.Point(2, 483);
            this.tabChat.Name = "tabChat";
            this.tabChat.SelectedIndex = 0;
            this.tabChat.Size = new System.Drawing.Size(798, 232);
            this.tabChat.TabIndex = 6;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.rtxtPublic);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(790, 206);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "公共";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 741);
            this.Controls.Add(this.tabChat);
            this.Controls.Add(this.lbNotice);
            this.Controls.Add(this.lstUser);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtChat);
            this.Controls.Add(this.sfmlView);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.ImeMode = System.Windows.Forms.ImeMode.On;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMain";
            this.Text = "TRPGChatRoom";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.tabChat.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label sfmlView;
        private System.Windows.Forms.Timer sfmlTick;
        private System.Windows.Forms.TextBox txtChat;
        private System.Windows.Forms.RichTextBox rtxtPublic;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.ListBox lstUser;
        private System.Windows.Forms.Label lbNotice;
        private System.Windows.Forms.TabControl tabChat;
        private System.Windows.Forms.TabPage tabPage1;

    }
}