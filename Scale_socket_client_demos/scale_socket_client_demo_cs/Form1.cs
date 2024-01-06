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
using System.IO;

namespace scale_socket_client_demo_cs
{
    public partial class OPK : Form
    {
        // 变量设置
        int currentBatch = 1;      // 批号
        int currentItemNumber = 1; // 编号
        int selectedNumber = 5;    // 产品数量选择（默认为5）
        //
        bool bIsOpen = false;
        public delegate void SetString(string text);
        public delegate void SetBytes(byte[] bytes);
        private bool StableDisplayed = false;
        Thread r_thread=null;
        public OPK()
        {
            InitializeComponent();
        }
        /****************************************/
        //               电子秤IP连接
        /****************************************/
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
        /****************************************/
        //               电子秤数据接收
        /****************************************/
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
        /****************************************/
        //               显示信息设置
        /****************************************/
        // 显示函数
        void ShowMsg(string Text)
        {
            SetString stcb = new SetString(AddListBoxString);
            this.Invoke(stcb, Text + "\r\n");
        }

        // 列表显示（批号、编号信息设置）
        void AddListBoxString(string msg)
        {
            // 如果是新的一组，先添加批次信息
            if (currentItemNumber == 1)
            {
                listBox1.Items.Add($"第{currentBatch}批");
            }

            // 将消息添加到列表框，包括编号和数据
            listBox1.Items.Add($"编号{currentItemNumber}: {msg}");

            // 当每组数据达到“selectedNumber”时，增加批次号并重新编号
            if (currentItemNumber % selectedNumber == 0)
            {
                currentBatch++;
                currentItemNumber = 1;
                listBox1.Items.Add(""); // 空一行
            }
            else
            {
                currentItemNumber++;
            }

            // 将视图滚动到最底部
            listBox1.TopIndex = listBox1.Items.Count - 1;
        }

        // 重量设置
        void SetWeight(byte[] bytes)
        {
            // 当前称重显示（重量、单位）
            textBox3.Text = Encoding.ASCII.GetString(bytes, 6, 8);
            textBox4.Text = Encoding.ASCII.GetString(bytes, 14, 3);
            // 当前称重状态判断
            string status;
            Color statusColor;
            if (bytes[0] == 'S')
            {
                status = "稳定";
                statusColor = Color.Green; // 设置 "稳定" 状态的颜色为绿色
            }
            else if (bytes[0] == 'U')
            {
                status = "不稳定";
                statusColor = Color.Orange; // 设置 "不稳定" 状态的颜色为绿色
            }
            else if (bytes[0] == 'O')
            {
                status = "超载";
                statusColor = Color.Red; // 设置 "超载" 状态的颜色为绿色
            }
            else
            {
                status = "未知状态";
                statusColor = Color.Gray; // 设置 "未知状态" 状态的颜色为绿色
            }
            // 当前称重状态输出
            label4.Text = status;
            label4.ForeColor = statusColor;
            label4.Font = new Font(label4.Font, FontStyle.Bold);

            // 完整称重信息输出
            if (status == "稳定" && !StableDisplayed)
            {
                // 字符串转换（当前重量、单位）
                string str1 = Encoding.ASCII.GetString(bytes, 6, 8);
                string str2 = Encoding.ASCII.GetString(bytes, 14, 3);
                // 编码转换（当前重量）
                string str3 = BitConverter.ToString(bytes, 6, 8).Replace("-", "");
                if (!string.Equals(str3, "202020302E303030") && !string.Equals(str3, "2020202020202030"))    //判断当前重量有无0g和0.000kg——202020302E303030:0.000kg    2020202020202030:0g
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
        /****************************************/
        //               产品数量选择
        /****************************************/
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 检查是否选择了有效的数字
            if (int.TryParse(comboBox1.SelectedItem.ToString(), out selectedNumber))
            {
                // 你可以在这里使用 selectedNumber，它是 comboBox1 中选择的数字
                MessageBox.Show($"当前产品数量为：{selectedNumber}");
            }
            else
            {
                MessageBox.Show("无效的产品数量选择");
            }
        }
        /****************************************/
        //               信息保存
        /****************************************/
        // 保存列表信息函数
        void SaveListBoxContents(string fileName)
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                foreach (var item in listBox1.Items)
                {
                    writer.WriteLine(item.ToString());
                }
            }

            MessageBox.Show($"列表内容已保存到文件：{fileName}");
        }
        // 保存列表信息按键
        private void button_msg_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "文本文件 (*.txt)|*.txt";
            saveFileDialog.Title = "保存ListBox1内容";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                SaveListBoxContents(saveFileDialog.FileName);
            }
        }

        private void button_del_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count > 0)
            {
                // 删除最新一行信息
                int lastIndex = listBox1.Items.Count - 1;
                listBox1.Items.RemoveAt(lastIndex);

                // 如果当前编号小于等于1，需要减少批次号
                if (currentItemNumber <= 1)
                {
                    // 减少批次号
                    if (currentBatch > 1)
                    {
                        currentBatch--;
                    }

                    // 重新编号
                    currentItemNumber = selectedNumber;
                }
                else
                {
                    // 仅减少编号
                    currentItemNumber--;
                }

                // 更新视图
                listBox1.TopIndex = listBox1.Items.Count - 1;
            }
            else
            {
                MessageBox.Show("列表中没有要删除的行。");
            }
        }
    }
}
