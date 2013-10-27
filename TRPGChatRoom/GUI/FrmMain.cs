using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Text;
using SFML.Graphics;

namespace TRPGChatRoom.GUI
{
    public partial class FrmMain : Form
    {
        private RenderWindow sfmlRenderArea;

        private bool isHost;
        private UdpClient sendClient;
        private UdpClient recvClient;

        private Queue<Byte[]> msgQueue;

        public FrmMain()
        {
            InitializeComponent();
            this.sfmlRenderArea = new RenderWindow(this.sfmlView.Handle);

            this.isHost = false;
            this.sendClient = new UdpClient();
            this.recvClient = new UdpClient();

            this.msgQueue = new Queue<byte[]>();
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

        public void Init()
        {
            this.sfmlTick.Enabled = true;

            IPEndPoint remote = new IPEndPoint(IPAddress.Any, 8802);
            this.recvClient.Client.Bind(remote);
            this.recvClient.BeginReceive(new AsyncCallback(this.ReceiveCallback), null);
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
                this.sendClient.Close();
                this.recvClient.Close();
            }
        }

        private void FrmMain_SendMessage(object sender, EventArgs e)
        {
            String msg = this.txtChat.Text;
            if (msg.Length > 0)
            {
                Byte[] data = Encoding.UTF32.GetBytes(msg);
                IPEndPoint server = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8802);
                this.sendClient.Send(data, data.Length, server);
            }
        }

        //private void FrmMain_GotFocus(object sender, EventArgs e)
        //{
        //    //this.sfmlRenderArea = new RenderWindow(this.sfmlView.Handle);
        //    this.sfmlTick.Enabled = true;
        //}

        private void Update(object sender, EventArgs e)
        {
            this.SFMLUpdate();
            this.MSGUpdate();
        }

        private void SFMLUpdate()
        {
            this.sfmlRenderArea.Clear(Color.Black);
            this.sfmlRenderArea.Display();
        }

        private void MSGUpdate()
        {
            while (this.msgQueue.Count > 0)
            {
                Byte[] data = this.msgQueue.Dequeue();
                String msg = Encoding.UTF32.GetString(data);
                this.rtxtPublic.Text += "\n" + msg;
            }
        }

        private void ReceiveCallback(IAsyncResult result)
        {
            IPEndPoint remote = new IPEndPoint(IPAddress.Any, 8802);
            Byte[] data = this.recvClient.EndReceive(result, ref remote);
            this.msgQueue.Enqueue(data);
            this.recvClient.BeginReceive(new AsyncCallback(this.ReceiveCallback), null);
        }
    }
}
