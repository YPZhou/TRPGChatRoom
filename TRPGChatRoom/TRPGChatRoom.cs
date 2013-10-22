using System;
using System.Windows.Forms;

using SFML.Graphics;

using TRPGChatRoom.GUI;

namespace TRPGChatRoom
{
    static class TRPGChatRoom
    {
        public static Form frmLogin;
        public static Form frmMain;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            TRPGChatRoom.frmLogin = new FrmLogin();
            TRPGChatRoom.frmMain = new FrmMain();
            frmLogin.Show();

            Application.Run();
        }
    }
}
