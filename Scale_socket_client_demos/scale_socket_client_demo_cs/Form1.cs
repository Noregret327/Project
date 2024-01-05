using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Data.SqlTypes;

namespace scale_socket_client_demo_cs
{
    public partial class OPK : Form
    {
        bool bIsOpen = false;
        public delegate void SetString(string text);
        public delegate void SetBytes(byte[] bytes);
        private bool StableDisplayed = false;
        Thread r_thread=null;
        public OPK()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (bIsOpen)
            {
                button1.Text = "IP连接";
                bIsOpen = false;
                if (r_thread != null) r_thread.Abort();
            }
            else
            {
                Socket ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPAddress iPAddress = IPAddress.Parse(textBox1.Text);
                System.Net.EndPoint endpoint = new IPEndPoint(iPAddress,Convert.ToInt32(textBox2.Text));

                //2.连接服务器
                ClientSocket.Connect(endpoint);
                Console.WriteLine("客户端连接成功。。。");

                //开启一个新线程，执行接收消息方法
                r_thread = new Thread(Received);
                r_thread.IsBackground = true;
                r_thread.Start(ClientSocket);
                button1.Text = "断开IP";
                bIsOpen = true;
            }
        }
        void Received(object o)
        {
            try
            {
                Socket socketSend = o as Socket;
                byte[] buffer = new byte[128];//客户端连接服务器成功后，服务器接收客户端发送的消息

                while (bIsOpen)
                {
                    //实际接收到的有效字节数
                    int len = socketSend.Receive(buffer);
                    if (len == 0)
                    {
                        break;
                    }
                    string str = Encoding.UTF8.GetString(buffer, 0, len);
                    SetBytes stcb = new SetBytes(SetWeight);
                    this.Invoke(stcb, buffer);

                }
                socketSend.Close();
            }
            catch { }
        }
        void ShowMsg(string Text)
        {
            SetString stcb = new SetString(AddListBoxString);
            this.Invoke(stcb, Text + "\r\n");
        }
        void AddListBoxString(string msg)
        {
            listBox1.Items.Add(msg);
        }
        void SetWeight(byte[] bytes)
        {
            // 显示重量
            textBox3.Text = Encoding.ASCII.GetString(bytes, 6, 8);
            textBox4.Text = Encoding.ASCII.GetString(bytes, 14, 3);

            // 设置状态
            string status;
            if (bytes[0] == 'S')
                status = "稳定";
            else if (bytes[0] == 'U')
                status = "不稳定";
            else if (bytes[0] == 'O')
                status = "超载";
            else
                status = "未知状态";

            // 输出状态
            label4.Text = status;

            // 输出显示
            if (status == "稳定" && !StableDisplayed)
            {
                string str1 = Encoding.ASCII.GetString(bytes, 6, 8);
                string str2 = Encoding.ASCII.GetString(bytes, 14, 3);

                string str3 = BitConverter.ToString(bytes, 6, 8).Replace("-", "");
                if (!string.Equals(str3, "202020302E303030") && !string.Equals(str3, "2020202020202030"))    //202020302E303030:0.000kg    2020202020202030:0g
                {
                    ShowMsg(str1 + str2);
                    StableDisplayed = true;
                }
            }
            else if (status != "稳定")
            {
                StableDisplayed = false;
            }
        }
    }
}
