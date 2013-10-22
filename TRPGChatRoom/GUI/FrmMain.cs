using System;
using System.Windows.Forms;

using SFML.Graphics;

namespace TRPGChatRoom.GUI
{
    public partial class FrmMain : Form
    {
        private RenderWindow sfmlRenderArea;

        public FrmMain()
        {
            InitializeComponent();
            sfmlRenderArea = new RenderWindow(this.sfmlView.Handle);
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {            
        }

        private void FrmMain_FormClosed(object sender, EventArgs e)
        {
            this.sfmlTick.Enabled = false;
            this.Hide();
            TRPGChatRoom.frmLogin.Show();
            TRPGChatRoom.frmLogin.Focus();
        }

        private void SFMLUpdate(object sender, EventArgs e)
        {
            sfmlRenderArea.Clear(Color.Black);
            sfmlRenderArea.Display();
        }

        public void CleanUp()
        {
            this.sfmlRenderArea.Close();
        }
    }
}
