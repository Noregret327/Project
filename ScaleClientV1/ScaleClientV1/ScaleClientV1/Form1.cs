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
using System.Data.SQLite;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;

namespace ScaleClientV1
{
    public partial class Form_MainUI : Form
    {
        // 变量设置
        int currentBatch = 1;                   // 批号
        int currentItemNumber = 1;              // 编号
        int selectedNumber = 5;                 // 产品数量选择（默认为5）
        int TotalNumber = 6;   // 总数量（总重量+产品数量）
        //
       /* bool bIsOpen = false;
        Thread r_thread = null;*/
        public delegate void SetString(string text);
        public delegate void SetBytes(byte[] bytes);
        private bool StableDisplayed = false;
        // 
        private CancellationTokenSource cancellationTokenSource;
        private Thread r_thread;
        private bool bIsOpen = false;
        public Form_MainUI()
        {
            InitializeComponent();
        }
        /****************************************/
        //               电子秤IP连接
        /****************************************/
        /* private void button1_Click(object sender, EventArgs e)
         {
             if (bIsOpen)
             {
                 button_IPConnect.Text = "IP连接";
                 bIsOpen = false;
                 if (r_thread != null) r_thread.Abort();
             }
             else
             {
                 Socket ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                 IPAddress iPAddress = IPAddress.Parse(textBox_IP.Text);
                 System.Net.EndPoint endpoint = new IPEndPoint(iPAddress, Convert.ToInt32(textBox_Port.Text));


                 //2.连接服务器
                 ClientSocket.Connect(endpoint);
                 //Console.WriteLine("客户端连接成功。。。");
                 textBox_connect.Text = "客户端已连接";
                 textBox_connect.ForeColor = Color.Green;
                 //开启一个新线程，执行接收消息方法
                 r_thread = new Thread(Received);
                 r_thread.IsBackground = true;
                 r_thread.Start(ClientSocket);
                 button_IPConnect.Text = "断开IP";
                 bIsOpen = true;
             }
         }*/
        private void button1_Click(object sender, EventArgs e)
        {
            if (bIsOpen)
            {
                button_IPConnect.Text = "IP连接";
                bIsOpen = false;
                cancellationTokenSource?.Cancel(); // 取消任务
                r_thread?.Join(); // 等待线程结束
            }
            else
            {
                // 创建 CancellationTokenSource
                cancellationTokenSource = new CancellationTokenSource();

                Socket ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPAddress iPAddress = IPAddress.Parse(textBox_IP.Text);
                System.Net.EndPoint endpoint = new IPEndPoint(iPAddress, Convert.ToInt32(textBox_Port.Text));

                // 连接服务器
                ClientSocket.Connect(endpoint);
                textBox_connect.Text = "客户端已连接";
                textBox_connect.ForeColor = Color.Green;

                // 开启一个新线程，执行接收消息方法
                r_thread = new Thread(() => Received(ClientSocket, cancellationTokenSource.Token));
                r_thread.IsBackground = true;
                r_thread.Start();

                button_IPConnect.Text = "断开IP";

                bIsOpen = true;
            }
        }

        /****************************************/
        //               电子秤数据接收
        /****************************************/
        /*void Received(object o)
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
        }*/
        async void Received(object o, CancellationToken cancellationToken)
        {
            try
            {
                Socket socketSend = o as Socket;
                byte[] buffer = new byte[128]; // 客户端连接服务器成功后，服务器接收客户端发送的消息

                while (!cancellationToken.IsCancellationRequested)
                {
                    // 使用异步接收数据
                    int len = await Task.Run(() => socketSend.Receive(buffer, 0, buffer.Length, SocketFlags.None));

                    if (len == 0)
                    {
                        break;
                    }

                    string str = Encoding.UTF8.GetString(buffer, 0, len);

                    // 使用 InvokeRequired 来检查是否在 UI 线程上调用
                    if (this.InvokeRequired)
                    {
                        SetBytes stcb = new SetBytes(SetWeight);
                        this.Invoke(stcb, buffer);
                    }
                    else
                    {
                        SetWeight(buffer);
                    }
                }
            }
            catch (Exception ex)
            {
                // 处理异常，例如记录日志
                Console.WriteLine($"Exception in Received method: {ex.Message}");
            }
            finally
            {
                // 执行清理操作，确保关闭 socket
                if (o is Socket socketSend)
                {
                    socketSend.Close();
                }
            }
        }
        /****************************************/
        //               显示信息设置
        /****************************************/
        // 显示函数
        void ShowMsg(string Text)
        {
            SetString stcb = new SetString(AddListBoxString);
            this.Invoke(stcb, Text + "\r\n");
            SaveListBoxContents("tempDat.txt");
        }

        // 列表显示（批号、编号信息设置）
        void AddListBoxString(string msg)
        {


            // 将消息添加到列表框，包括批次号和编号
            listBox_Weight1.Items.Add($"第{currentBatch}批\t编号{currentItemNumber}：\t{msg}");

            // 当每组数据达到“TotalNumber”时，增加批次号并重新编号
            if (currentItemNumber % TotalNumber == 0)
            {
                currentBatch++;
                currentItemNumber = 1;

            }
            else
            {
                currentItemNumber++;
            }

            // 将视图滚动到最底部
            listBox_Weight1.TopIndex = listBox_Weight1.Items.Count - 1;
        }

        // 重量设置
        void SetWeight(byte[] bytes)
        {
            // 当前称重显示（重量、单位）
            textBox_Weight.Text = Encoding.ASCII.GetString(bytes, 6, 8);
            textBox_Flats.Text = Encoding.ASCII.GetString(bytes, 14, 3);
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
            label_State.Text = status;
            label_State.ForeColor = statusColor;
            label_State.Font = new Font(label_State.Font, FontStyle.Bold);

            // 完整称重信息输出
            if (status == "稳定" && !StableDisplayed)
            {
                // 字符串转换（当前重量、单位）
                string str1 = Encoding.ASCII.GetString(bytes, 6, 8);
                string str2 = Encoding.ASCII.GetString(bytes, 14, 3);
                // 编码转换（当前重量）
                string str3 = BitConverter.ToString(bytes, 6, 8).Replace("-", "");
                if (!string.Equals(str3, "202020302E303030") && !string.Equals(str3, "2020202020202030") && comboBox_ChanpinNumbei.Enabled==false)    //判断当前重量有无0g和0.000kg——202020302E303030:0.000kg    2020202020202030:0g
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
            if (int.TryParse(comboBox_ChanpinNumbei.SelectedItem.ToString(), out selectedNumber))
            {
                // 你可以在这里使用 selectedNumber，它是 comboBox1 中选择的数字
                MessageBox.Show($"当前产品数量为：{selectedNumber}");
                TotalNumber = selectedNumber + 1;
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
                foreach (var item in listBox_Weight1.Items)
                {
                    writer.WriteLine(item.ToString());
                }
            }

            //MessageBox.Show($"列表内容已保存到文件：{fileName}");
        }
        // 保存列表信息按键
        //private void button_msg_Click(object sender, EventArgs e)
        //{
        //    SaveFileDialog saveFileDialog = new SaveFileDialog();
        //    saveFileDialog.Filter = "文本文件 (*.txt)|*.txt";
        //    saveFileDialog.Title = "保存ListBox1内容";

        //    if (saveFileDialog.ShowDialog() == DialogResult.OK)
        //    {
        //        SaveListBoxContents(saveFileDialog.FileName);
        //    }
        //}

        private void button_del_Click(object sender, EventArgs e)
        {
            if (listBox_Weight1.Items.Count > 0)
            {
                // 记录当前组号和编号
                int currentBatchBackup = currentBatch;
                int currentItemNumberBackup = currentItemNumber;

                // 删除最后一行数据
                listBox_Weight1.Items.RemoveAt(listBox_Weight1.Items.Count - 1);

                // 如果删除的是该组的最后一条数据，则将组号减一，编号重置
                if (currentItemNumberBackup % TotalNumber == 1)
                {
                    currentBatch--;
                    currentItemNumber = TotalNumber;
                }
                else
                {
                    // 如果删除的是该组的中间数据，则将编号减一
                    currentItemNumber--;
                }
            }
            else
            {
                MessageBox.Show("列表为空，无法删除数据。");
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            //readTxtToListBox("tempDat.txt");
        }

        private void listBox1_FormatStringChanged(object sender, EventArgs e)
        {
            //MessageBox.Show("Test");
        }

        private void readTxtToListBox(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                // 处理每一行数据
                //Console.WriteLine(line);
                listBox_Weight1.Items.Add($"{line}");
            }
        }

        private void button_finish_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("本次称重全部完成了吗？", "称重", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.OK)
            {
                DateTime currentTime = DateTime.Now;
                string ssz = "ssz";
                string d = currentTime.ToString("yyyyMMddHHmmSS");
                string srcPath = "tempDat.txt";
                string dstPath = "数据/" + currentTime.ToString("yyyy-MM-dd_HH-mm-ss") + ".txt";
                string excelFilePath = "报表/"+d+".xlsx";
                //string excelFilePath = "sss.xlsx";
                try
                {
                    saveToDB(srcPath);//将数据存储到数据库DB
                    File.Copy(srcPath, dstPath);
                    comboBox_ChanpinNumbei.Enabled = true;

                    txtToExcel(srcPath, excelFilePath, int.Parse(comboBox_ChanpinNumbei.Text));
                    MessageBox.Show("本次称重完成！");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            
        }

        private void saveToDB(string filePath)
        {
            SQLiteQuery sq = new SQLiteQuery();
            string[] lines = File.ReadAllLines(filePath);
            //MessageBox.Show("lines="+lines.Length);//20
            foreach (string line in lines)
            {
                //MessageBox.Show("Test"+line.Length);
                // 处理每一行数据
                //Console.WriteLine(line);
                if(line.Length > 0)
                {
                    string insert_sql = "INSERT INTO opk_data(code,data,group_times,datetime_1) VALUES(" +
                        $"'{textBox_code.Text}'," +
                        $"'{line.Substring(9).Replace("：","").Replace("	", "")}'," +
                        $"'{comboBox_ChanpinNumbei.Text}','{DateTime.Now}')";
                    //MessageBox.Show("sql="+insert_sql);
                    sq.Execute(insert_sql);
                }

            }
        }

        private void button_start_Click(object sender, EventArgs e)
        {
            if (comboBox_ChanpinNumbei.Enabled)
            {
                comboBox_ChanpinNumbei.Enabled = false;
                button_start.Text = "重新开始";

                // 客户信息显示
                textBox_ChanpinName.Text = "平移内倒门";
                textBox_ClientName.Text = "黄XX";
            }
            else
            {
                comboBox_ChanpinNumbei.Enabled = true;
                button_start.Text = "开始";
            }

        }

        private void txtToExcel(string filePath,string excelFilePath, int every)
        {
            try
            {
                XSSFWorkbook wb = new XSSFWorkbook();
                // 创建工作表
                ISheet sheet = wb.CreateSheet("Sheet1");
                IRow title_row = sheet.CreateRow(0);
                title_row.CreateCell(0).SetCellValue("测试报表");
                CellRangeAddress region = new CellRangeAddress(0, 0, 0, (every * 2) + 2);//合并
                //CellRangeAddress region = new CellRangeAddress(0, 0, 0, 4);//合并
                sheet.AddMergedRegion(region);
                IRow times_row = sheet.CreateRow(1);
                times_row.CreateCell(0).SetCellValue("总重量");
                for (int i = 1; i < every + 1; i++)
                {
                    times_row.CreateCell(i).SetCellValue(i);
                }
                //data_row.CreateCell(every + 1).SetCellValue("总重量");
                //for (int j = every + 1; j < every * 2 + 1; j++)
                //{
                //    data_row.CreateCell(j).SetCellValue(j);
                //}
                string[] lines = File.ReadAllLines(filePath);
                //MessageBox.Show("lines="+lines.Length);//20
                int lineNum = 2;
                int every_times = int.Parse(comboBox_ChanpinNumbei.Text) + 1;
                int j = 0;
                IRow data_row = sheet.CreateRow(lineNum);
                //for (int w = 0; w < lines.Length; w++)
                foreach (string line in lines)
                {
                        if(j==every_times)
                        {
                            
                            lineNum++;
                            data_row = sheet.CreateRow(lineNum);
                            j = 0;
                        }
                        
                        if (line.Length > 0)
                        {
                            string weight = line.Substring(9).Replace("：", "").Replace("	", "");
                            //IRow data_row = sheet.CreateRow(lineNum);
                            if(j< every_times)
                            {
                            
                                data_row.CreateCell(j).SetCellValue(weight);
                                j++;
                            }
                        }
                }
                

                foreach (string line in lines)
                {

                    //string weight = line.Substring(9).Replace("：", "").Replace("	", "");
                    //int every_times = int.Parse(comboBox1.Text);
                    //IRow data_row = sheet.CreateRow(lineNum);

                    //data_row.CreateCell(lineNum).SetCellValue(weight);
                    //for (int j = 0; j < every_times; j++)
                    //{
                    //    if (line.Length > 0)
                    //    {
                    //        data_row.CreateCell(lineNum).SetCellValue(weight);
                    //    }
                    //}

                    //lineNum++;

                }

                // 保存文件
                FileStream sw = File.Create(excelFilePath);
                wb.Write(sw);
                sw.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("导出excel错误;"+ex.StackTrace);
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
