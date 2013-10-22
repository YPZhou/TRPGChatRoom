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
            this.SuspendLayout();
            // 
            // sfmlView
            // 
            this.sfmlView.Location = new System.Drawing.Point(0, 0);
            this.sfmlView.Name = "sfmlView";
            this.sfmlView.Size = new System.Drawing.Size(800, 600);
            this.sfmlView.TabIndex = 0;
            // 
            // sfmlTick
            // 
            this.sfmlTick.Enabled = true;
            this.sfmlTick.Interval = 40;
            this.sfmlTick.Tick += new System.EventHandler(this.SFMLUpdate);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 741);
            this.Controls.Add(this.sfmlView);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.ImeMode = System.Windows.Forms.ImeMode.On;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMain";
            this.Text = "TRPGChatRoom";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmMain_FormClosed);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label sfmlView;
        private System.Windows.Forms.Timer sfmlTick;

    }
}