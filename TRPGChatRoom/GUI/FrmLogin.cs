using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TRPGChatRoom.GUI
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {

        }

        private void FrmLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            //((FrmMain)TRPGChatRoom.frmMain).CleanUp();
            TRPGChatRoom.frmLogin.Close();
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ((FrmMain)TRPGChatRoom.frmMain).IsHost = true;
            if (this.txtName.Text.Length > 0)
            {
                ((FrmMain)TRPGChatRoom.frmMain).UserName = this.txtName.Text;
            }
            else
            {
                MessageBox.Show("昵称没填...", "错误");
                return;
            }            
            ((FrmMain)TRPGChatRoom.frmMain).Init();
            TRPGChatRoom.frmMain.Show();
            TRPGChatRoom.frmMain.Focus();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ((FrmMain)TRPGChatRoom.frmMain).IsHost = false;
            if (this.txtName.Text.Length > 0)
            {
                ((FrmMain)TRPGChatRoom.frmMain).UserName = this.txtName.Text;
            }
            else
            {
                MessageBox.Show("昵称没填...", "错误");
                return;
            }
            if (((FrmMain)TRPGChatRoom.frmMain).BindHost(this.txtIP.Text))
            {
            }
            else
            {
                MessageBox.Show("IP地址格式错误", "错误");
                return;
            }
            ((FrmMain)TRPGChatRoom.frmMain).Init();
            TRPGChatRoom.frmMain.Show();
            TRPGChatRoom.frmMain.Focus();
            this.Hide();
        }
    }
}
