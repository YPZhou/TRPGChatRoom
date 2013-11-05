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
        private int port;
        private UdpClient sendClient;
        private UdpClient recvClient;

        private String userName;

        private Queue<Byte[]> msgQueue;
        private Queue<IPEndPoint> endpointQueue;

        private Dictionary<String, IPAddress> userDict;
        private Dictionary<String, TabPage> whisperDict;

        private Random rand;

        public FrmMain()
        {
            InitializeComponent();
            this.sfmlRenderArea = new RenderWindow(this.sfmlView.Handle);

            this.isHost = false;
            this.port = 8802;
            this.sendClient = new UdpClient();
            this.recvClient = new UdpClient();
            this.recvClient.Client.Bind(new IPEndPoint(IPAddress.Any, this.port));

            this.msgQueue = new Queue<byte[]>();
            this.endpointQueue = new Queue<IPEndPoint>();

            this.userDict = new Dictionary<String, IPAddress>();
            this.whisperDict = new Dictionary<string, TabPage>();

            this.rand = new Random();

            // only for testing whisper, need to be removed later
            this.userDict.Add("test", IPAddress.Parse("192.168.0.100"));
            this.UpdateUserList();
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

        public String UserName
        {
            set
            {
                this.userName = value;
            }
        }

        public bool BindHost(String ip)
        {
            IPAddress ipAddress;
            try
            {
                ipAddress = IPAddress.Parse(ip);
            }
            catch (Exception e)
            {
                return false;
            }
            if (!this.userDict.ContainsKey("host"))
            {
                this.userDict.Add("host", ipAddress);
            }
            else
            {

                this.userDict["host"] = ipAddress;
            }
            return true;
        }

        public void Init()
        {
            this.sfmlTick.Enabled = true;            
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
                this.rtxtPublic.Text = "";
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

        private void FrmMain_EnterDown(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Return))
            {
                this.FrmMain_SendMessage(sender, e);
            }
        }

        private void FrmMain_SendMessage(object sender, EventArgs e)
        {
            int index = tabChat.SelectedIndex;
            if (index == 0)
            {
                this.SendPublicMessage();
            }
            else
            {
                this.SendPrivateMessage(index);
            }
        }

        private void FrmMain_StartWhisper(object sender, MouseEventArgs e)
        {
            int index = this.lstUser.IndexFromPoint(e.Location);
            if (index != System.Windows.Forms.ListBox.NoMatches)
            {
                String user = this.lstUser.Items[index].ToString();
                String ip = this.userDict[user].ToString();
                String key = user + ip;
                if (!this.whisperDict.ContainsKey(key))
                {
                    TabPage whisperPage = new TabPage(user);
                    RichTextBox whisperRtxt = new RichTextBox();
                    whisperRtxt.Location = new System.Drawing.Point(3, 3);
                    whisperRtxt.ReadOnly = true;
                    whisperRtxt.Size = new System.Drawing.Size(787, 203);
                    whisperRtxt.Text = "";
                    whisperPage.Controls.Add(whisperRtxt);

                    this.tabChat.TabPages.Add(whisperPage);
                    this.tabChat.SelectTab(whisperPage);

                    this.whisperDict.Add(user + ip, whisperPage);
                }                
            }
        }

        private void SendPublicMessage()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.txtChat.Text.Trim());
            
            if (sb.Length > 0)
            {
                sb.Append("||||" + this.userName);
                Byte[] data = Encoding.UTF32.GetBytes(sb.ToString());                

                if (this.isHost)
                {
                    this.msgQueue.Enqueue(data);
                    this.endpointQueue.Enqueue(new IPEndPoint(IPAddress.Any, this.port));
                }
                else
                {
                    IPEndPoint server = new IPEndPoint(this.userDict["host"], this.port);
                    this.sendClient.Send(data, data.Length, server);                    
                }

                this.txtChat.Text = "";
            }
        }

        private void SendPrivateMessage(int index)
        {
            String user = this.tabChat.TabPages[index].Text;
            String ip = this.userDict[user].ToString();

            StringBuilder sb = new StringBuilder();
            sb.Append(this.txtChat.Text.Trim());

            if (sb.Length > 0)
            {
                sb.Append("||||" + this.userName + "||||" + user);
                Byte[] data = Encoding.UTF32.GetBytes(sb.ToString());

                if (this.isHost)
                {
                    this.msgQueue.Enqueue(data);
                    this.endpointQueue.Enqueue(new IPEndPoint(IPAddress.Any, this.port));
                }
                else
                {
                    IPEndPoint server = new IPEndPoint(this.userDict["host"], this.port);
                    this.sendClient.Send(data, data.Length, server);
                }

                this.txtChat.Text = "";
            }
        }

        private String ProcessRollCommand(String rollMsg, String user)
        {
            char[] rollSeparator = { ' ' };
            String[] rollSegments = rollMsg.Split(rollSeparator);

            if (rollSegments.Length == 2)
            {
                char[] diceSeparator = { 'd', 'D' };
                String[] diceData = rollSegments[1].Split(diceSeparator);

                if (diceData.Length == 2)
                {
                    int diceNumber = 0;
                    int diceType = 0;
                    try
                    {
                        diceNumber = Int32.Parse(diceData[0]);
                        diceType = Int32.Parse(diceData[1]);
                    }
                    catch (Exception e)
                    {
                        return "";
                    }

                    int rollResult = 0;
                    for (int i = 0; i < diceNumber; i++)
                    {
                        rollResult += this.rand.Next(diceType) + 1;
                    }
                    String resultMsg = user + "掷" + diceNumber + "D" + diceType + "得" + rollResult;
                    return resultMsg;
                }
                else
                {
                    return "";
                }
            }
            else if (rollSegments.Length == 3)
            {
                char[] diceSeparator = { 'd', 'D' };
                String[] diceData = rollSegments[1].Split(diceSeparator);

                if (diceData.Length == 2)
                {
                    int diceNumber = 0;
                    int diceType = 0;
                    int diceModifier = 0;
                    try
                    {
                        diceNumber = Int32.Parse(diceData[0]);
                        diceType = Int32.Parse(diceData[1]);
                        diceModifier = Int32.Parse(rollSegments[2]);
                    }
                    catch (Exception e)
                    {
                        return "";
                    }

                    int rollResult = 0;
                    for (int i = 0; i < diceNumber; i++)
                    {
                        rollResult += this.rand.Next(diceType) + 1;
                    }
                    rollResult += diceModifier;
                    String modifierText = "";
                    if (diceModifier > 0) modifierText = "+" + diceModifier.ToString();
                    if (diceModifier < 0) modifierText = diceModifier.ToString();
                    String resultMsg = user + "掷" + diceNumber + "D" + diceType + modifierText + "得" + rollResult;
                    return resultMsg;
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }

        private bool CheckUserDictionary(String user, IPAddress ip)
        {
            if (this.userDict.ContainsKey(user))
            {
                if (this.userDict[user].Equals(ip))
                {
                    // do nothing
                }
                else
                {
                    // error
                    // 2 different users using the same username
                    return false;
                }
            }
            else
            {
                String removeKey = "";
                foreach (String key in this.userDict.Keys)
                {
                    if (userDict[key].Equals(ip))
                    {
                        removeKey = key;
                    }
                }
                if (removeKey.Length > 0) this.userDict.Remove(removeKey);
                this.userDict.Add(user, ip);
                this.UpdateUserList();
            }
            return true;
        }

        private String ProcessReceivedMessage(String rawMsg, IPEndPoint endpoint)
        {
            if (this.isHost)
            {
                String[] separator = {"||||"};
                String[] msgSegments = rawMsg.Split(separator, StringSplitOptions.RemoveEmptyEntries);

                if (msgSegments.Length == 2)
                {
                    // public message

                    String msg = msgSegments[0].Trim();
                    String user = msgSegments[1];

                    if (user.Equals(this.userName))
                    {
                        if (endpoint.Address.ToString().Equals("0.0.0.0"))
                        {
                            if (msg.StartsWith("/r"))
                            {
                                String resultMsg = this.ProcessRollCommand(msg, user);
                                if (resultMsg.Length > 0)
                                {
                                    Byte[] data = Encoding.UTF32.GetBytes(resultMsg + "||||" + user + "||||" + endpoint.Address.ToString());
                                    foreach (IPAddress ip in this.userDict.Values)
                                    {
                                        this.sendClient.Send(data, data.Length, new IPEndPoint(ip, this.port));
                                    }
                                    return resultMsg;
                                }
                            }
                            else
                            {
                                String resultMsg = user + "说：" + msg;
                                Byte[] data = Encoding.UTF32.GetBytes(resultMsg + "||||" + user + "||||" + endpoint.Address.ToString());
                                foreach (IPAddress ip in this.userDict.Values)
                                {
                                    this.sendClient.Send(data, data.Length, new IPEndPoint(ip, this.port));
                                }
                                return resultMsg;
                            }
                        }
                        else
                        {
                            // error
                            // same username as host

                            return "";
                        }
                    }
                    else
                    {
                        if (!this.CheckUserDictionary(user, endpoint.Address))
                        {
                            return "";
                        }
                    }

                    if (msg.StartsWith("/r"))
                    {
                        String resultMsg = this.ProcessRollCommand(msg, user);
                        if (resultMsg.Length > 0)
                        {
                            Byte[] data = Encoding.UTF32.GetBytes(resultMsg + "||||" + user + "||||" + endpoint.Address.ToString());
                            foreach (IPAddress ip in this.userDict.Values)
                            {
                                this.sendClient.Send(data, data.Length, new IPEndPoint(ip, this.port));
                            }
                            return resultMsg;
                        }
                    }
                    else
                    {
                        String resultMsg = user + "说：" + msg;
                        Byte[] data = Encoding.UTF32.GetBytes(resultMsg + "||||" + user + "||||" + endpoint.Address.ToString());
                        foreach (IPAddress ip in this.userDict.Values)
                        {
                            this.sendClient.Send(data, data.Length, new IPEndPoint(ip, this.port));
                        }
                        return resultMsg;
                    }
                }
                else if (msgSegments.Length == 3)
                {
                    // private message
                    String msg = msgSegments[0].Trim();
                    String user = msgSegments[1];
                    String destUser = msgSegments[2];
                    IPAddress destIp = null;
                    if (this.userDict.ContainsKey(destUser))
                    {
                        destIp = this.userDict[destUser];
                    }
                    else
                    {
                        return "";
                    }

                    if (user.Equals(this.userName))
                    {
                        if (endpoint.Address.ToString().Equals("0.0.0.0"))
                        {
                            if (msg.StartsWith("/r"))
                            {
                                String resultMsg = this.ProcessRollCommand(msg, user);
                                if (resultMsg.Length > 0)
                                {
                                    Byte[] data = Encoding.UTF32.GetBytes(resultMsg + "||||" + user + "||||" + endpoint.Address.ToString() + "||||x");
                                    this.sendClient.Send(data, data.Length, new IPEndPoint(destIp, this.port));
                                    Byte[] sendBackData = Encoding.UTF32.GetBytes(resultMsg + "||||" + destUser + "||||" + destIp.ToString() + "||||x");
                                    this.msgQueue.Enqueue(sendBackData);
                                    this.endpointQueue.Enqueue(endpoint);
                                    return "";
                                }
                            }
                            else
                            {
                                String resultMsg = user + "说：" + msg;
                                Byte[] data = Encoding.UTF32.GetBytes(resultMsg + "||||" + user + "||||" + endpoint.Address.ToString() + "||||x");
                                this.sendClient.Send(data, data.Length, new IPEndPoint(destIp, this.port));
                                Byte[] sendBackData = Encoding.UTF32.GetBytes(resultMsg + "||||" + destUser + "||||" + destIp.ToString() + "||||x");
                                this.msgQueue.Enqueue(sendBackData);
                                this.endpointQueue.Enqueue(endpoint);
                                return "";
                            }
                        }
                        else
                        {
                            // error
                            // same username as host

                            return "";
                        }
                    }
                    else
                    {
                        if (!this.CheckUserDictionary(user, endpoint.Address))
                        {
                            return "";
                        }
                    }

                    if (msg.StartsWith("/r"))
                    {
                        String resultMsg = this.ProcessRollCommand(msg, user);
                        if (resultMsg.Length > 0)
                        {
                            Byte[] data = Encoding.UTF32.GetBytes(resultMsg + "||||" + user + "||||" + endpoint.Address.ToString() + "||||x");
                            this.sendClient.Send(data, data.Length, new IPEndPoint(destIp, this.port));
                            Byte[] sendBackData = Encoding.UTF32.GetBytes(resultMsg + "||||" + destUser + "||||" + destIp.ToString() + "||||x");
                            this.sendClient.Send(sendBackData, sendBackData.Length, endpoint);
                            return "";
                        }
                    }
                    else
                    {
                        String resultMsg = user + "说：" + msg;
                        Byte[] data = Encoding.UTF32.GetBytes(resultMsg + "||||" + user + "||||" + endpoint.Address.ToString() + "||||x");
                        this.sendClient.Send(data, data.Length, new IPEndPoint(destIp, this.port));
                        Byte[] sendBackData = Encoding.UTF32.GetBytes(resultMsg + "||||" + destUser + "||||" + destIp.ToString() + "||||x");
                        this.sendClient.Send(sendBackData, sendBackData.Length, endpoint);
                        return "";
                    }
                }
                else if (msgSegments.Length == 4)
                {
                    String msg = msgSegments[0];
                    String user = msgSegments[1];
                    String ip = msgSegments[2];

                    if (!this.CheckUserDictionary(user, IPAddress.Parse(ip)))
                    {
                        return "";
                    }

                    String key = user + ip;
                    if (!this.whisperDict.ContainsKey(key))
                    {
                        TabPage whisperPage = new TabPage(user);
                        RichTextBox whisperRtxt = new RichTextBox();
                        whisperRtxt.Location = new System.Drawing.Point(3, 3);
                        whisperRtxt.ReadOnly = true;
                        whisperRtxt.Size = new System.Drawing.Size(787, 203);
                        whisperRtxt.Text = "";
                        whisperPage.Controls.Add(whisperRtxt);

                        this.tabChat.TabPages.Add(whisperPage);
                        this.tabChat.SelectTab(whisperPage);

                        this.whisperDict.Add(user + ip, whisperPage);
                    }

                    return msg + "||||" + user + ip;
                }
                else
                {
                    // error
                    // msg should always be segmented into 2, 3 or 4 parts
                }
            }
            else
            {
                String[] separator = { "||||" };
                String[] msgSegments = rawMsg.Split(separator, StringSplitOptions.RemoveEmptyEntries);

                if (msgSegments.Length == 3)
                {
                    String msg = msgSegments[0];
                    String user = msgSegments[1];
                    String ip = msgSegments[2];

                    if (ip.Equals("0.0.0.0"))
                    {
                        return msg;
                    }
                    else if (!this.CheckUserDictionary(user, IPAddress.Parse(ip)))
                    {
                        return "";
                    }

                    return msg;
                }
                else if (msgSegments.Length == 4)
                {
                    String msg = msgSegments[0];
                    String user = msgSegments[1];
                    String ip = msgSegments[2];

                    if (ip.Equals("0.0.0.0"))
                    {
                        // to implement
                        // add tabpage for host whisper

                        return msg + "||||host" + this.userDict["host"].ToString();
                    }
                    else if (!this.CheckUserDictionary(user, IPAddress.Parse(ip)))
                    {
                        return "";
                    }

                    String key = user + ip;
                    if (!this.whisperDict.ContainsKey(key))
                    {
                        TabPage whisperPage = new TabPage(user);
                        RichTextBox whisperRtxt = new RichTextBox();
                        whisperRtxt.Location = new System.Drawing.Point(3, 3);
                        whisperRtxt.ReadOnly = true;
                        whisperRtxt.Size = new System.Drawing.Size(787, 203);
                        whisperRtxt.Text = "";
                        whisperPage.Controls.Add(whisperRtxt);

                        this.tabChat.TabPages.Add(whisperPage);
                        this.tabChat.SelectTab(whisperPage);

                        this.whisperDict.Add(user + ip, whisperPage);
                    }

                    return msg + "||||" + user + ip;
                }
                else
                {
                    return "";
                }
            }
            return "";
        }

        private void Update(object sender, EventArgs e)
        {
            this.SFMLUpdate();
            this.MessageUpdate();
        }

        private void SFMLUpdate()
        {
            this.sfmlRenderArea.Clear(Color.Black);
            this.sfmlRenderArea.Display();
        }

        private void MessageUpdate()
        {
            while (this.msgQueue.Count > 0)
            {
                Byte[] data = this.msgQueue.Dequeue();
                String msg = Encoding.UTF32.GetString(data);
                IPEndPoint endpoint = this.endpointQueue.Dequeue();
                String processedMsg = this.ProcessReceivedMessage(msg, endpoint);
                if (processedMsg.Length > 0)
                {
                    String[] separator = { "||||" };
                    String[] msgSegments = processedMsg.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                    if (msgSegments.Length == 1)
                    {
                        if (this.rtxtPublic.Text.Length > 0) this.rtxtPublic.Text += "\n";
                        this.rtxtPublic.Text += processedMsg;
                        this.rtxtPublic.SelectionStart = this.rtxtPublic.Text.Length;
                        this.rtxtPublic.ScrollToCaret();
                    }
                    else if (msgSegments.Length == 2)
                    {
                        String privateMsg = msgSegments[0];
                        String key = msgSegments[1];
                        if (this.whisperDict.ContainsKey(key))
                        {
                            TabPage tabPage = this.whisperDict[key];
                            RichTextBox rtxtWhisper = (RichTextBox)tabPage.Controls[0];
                            if (rtxtWhisper.Text.Length > 0) rtxtWhisper.Text += "\n";
                            rtxtWhisper.Text += privateMsg;
                            rtxtWhisper.SelectionStart = rtxtWhisper.Text.Length;
                            rtxtWhisper.ScrollToCaret();
                        }
                    }
                }
            }
        }

        private void UpdateUserList()
        {
            if (this.userDict.Count > 0)
            {
                this.lstUser.Items.Clear();
                foreach (String user in this.userDict.Keys)
                {
                    this.lstUser.Items.Add(user);
                }
            }
        }

        private void ReceiveCallback(IAsyncResult result)
        {
            IPEndPoint remote = new IPEndPoint(IPAddress.Any, 8802);
            Byte[] data = this.recvClient.EndReceive(result, ref remote);
            this.msgQueue.Enqueue(data);
            this.endpointQueue.Enqueue(remote);
            this.recvClient.BeginReceive(new AsyncCallback(this.ReceiveCallback), null);
        }
    }
}
