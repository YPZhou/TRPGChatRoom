using System;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using SFML.Graphics;

namespace TRPGChatRoom.GUI
{
    public partial class FrmMain : Form
    {
        private RenderWindow sfmlRenderArea;

        private bool isHost;
        private IPEndPoint endPoint;

        public FrmMain()
        {
            InitializeComponent();
            this.sfmlRenderArea = new RenderWindow(this.sfmlView.Handle);
            this.isHost = false;
        }

        public bool IsHost
        {
            get
            {
                return this.isHost;
            }
            set
            {
                this.isHost = value;
                if (this.isHost)
                {
                    this.Text = "TRPGChatRoom - Host";
                }
                else
                {
                    this.Text = "TRPGChatRoom - Client";
                }
            }
        }

        public void StartTimer()
        {
            this.sfmlTick.Enabled = true;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {            
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                TRPGChatRoom.frmLogin.Show();
                TRPGChatRoom.frmLogin.Focus();
                this.sfmlTick.Enabled = false;
                this.Hide();
                e.Cancel = true;
            }
            else
            {
                this.sfmlRenderArea.Close();
                this.sfmlTick.Enabled = false;
            }
        }

        private void FrmMain_GotFocus(object sender, EventArgs e)
        {
            //this.sfmlRenderArea = new RenderWindow(this.sfmlView.Handle);
            this.sfmlTick.Enabled = true;
        }

        private void SFMLUpdate(object sender, EventArgs e)
        {
            this.sfmlRenderArea.Clear(Color.Black);
            this.sfmlRenderArea.Display();
        }


    }
}
