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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.lstUser = new System.Windows.Forms.ListBox();
            this.lbNotice = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
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
            this.sfmlTick.Tick += new System.EventHandler(this.SFMLUpdate);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(0, 721);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(729, 21);
            this.textBox1.TabIndex = 1;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Enabled = false;
            this.richTextBox1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.richTextBox1.Location = new System.Drawing.Point(3, 3);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(787, 203);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
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
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(2, 483);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(798, 232);
            this.tabControl1.TabIndex = 6;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.richTextBox1);
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
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.lbNotice);
            this.Controls.Add(this.lstUser);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.textBox1);
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
            this.GotFocus += new System.EventHandler(this.FrmMain_GotFocus);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label sfmlView;
        private System.Windows.Forms.Timer sfmlTick;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.ListBox lstUser;
        private System.Windows.Forms.Label lbNotice;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;

    }
}