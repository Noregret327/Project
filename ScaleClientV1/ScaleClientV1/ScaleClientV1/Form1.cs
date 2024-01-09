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
        // ��������
        int currentBatch = 1;                   // ����
        int currentItemNumber = 1;              // ���
        int selectedNumber = 5;                 // ��Ʒ����ѡ��Ĭ��Ϊ5��
        int TotalNumber = 6;   // ��������������+��Ʒ������
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
        //               ���ӳ�IP����
        /****************************************/
        /* private void button1_Click(object sender, EventArgs e)
         {
             if (bIsOpen)
             {
                 button_IPConnect.Text = "IP����";
                 bIsOpen = false;
                 if (r_thread != null) r_thread.Abort();
             }
             else
             {
                 Socket ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                 IPAddress iPAddress = IPAddress.Parse(textBox_IP.Text);
                 System.Net.EndPoint endpoint = new IPEndPoint(iPAddress, Convert.ToInt32(textBox_Port.Text));


                 //2.���ӷ�����
                 ClientSocket.Connect(endpoint);
                 //Console.WriteLine("�ͻ������ӳɹ�������");
                 textBox_connect.Text = "�ͻ���������";
                 textBox_connect.ForeColor = Color.Green;
                 //����һ�����̣߳�ִ�н�����Ϣ����
                 r_thread = new Thread(Received);
                 r_thread.IsBackground = true;
                 r_thread.Start(ClientSocket);
                 button_IPConnect.Text = "�Ͽ�IP";
                 bIsOpen = true;
             }
         }*/
        private void button1_Click(object sender, EventArgs e)
        {
            if (bIsOpen)
            {
                button_IPConnect.Text = "IP����";
                bIsOpen = false;
                cancellationTokenSource?.Cancel(); // ȡ������
                r_thread?.Join(); // �ȴ��߳̽���
            }
            else
            {
                // ���� CancellationTokenSource
                cancellationTokenSource = new CancellationTokenSource();

                Socket ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPAddress iPAddress = IPAddress.Parse(textBox_IP.Text);
                System.Net.EndPoint endpoint = new IPEndPoint(iPAddress, Convert.ToInt32(textBox_Port.Text));

                // ���ӷ�����
                ClientSocket.Connect(endpoint);
                textBox_connect.Text = "�ͻ���������";
                textBox_connect.ForeColor = Color.Green;

                // ����һ�����̣߳�ִ�н�����Ϣ����
                r_thread = new Thread(() => Received(ClientSocket, cancellationTokenSource.Token));
                r_thread.IsBackground = true;
                r_thread.Start();

                button_IPConnect.Text = "�Ͽ�IP";

                bIsOpen = true;
            }
        }

        /****************************************/
        //               ���ӳ����ݽ���
        /****************************************/
        /*void Received(object o)
        {
            try
            {
                Socket socketSend = o as Socket;
                byte[] buffer = new byte[128];//�ͻ������ӷ������ɹ��󣬷��������տͻ��˷��͵���Ϣ

                while (bIsOpen)
                {
                    //ʵ�ʽ��յ�����Ч�ֽ���
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
                byte[] buffer = new byte[128]; // �ͻ������ӷ������ɹ��󣬷��������տͻ��˷��͵���Ϣ

                while (!cancellationToken.IsCancellationRequested)
                {
                    // ʹ���첽��������
                    int len = await Task.Run(() => socketSend.Receive(buffer, 0, buffer.Length, SocketFlags.None));

                    if (len == 0)
                    {
                        break;
                    }

                    string str = Encoding.UTF8.GetString(buffer, 0, len);

                    // ʹ�� InvokeRequired ������Ƿ��� UI �߳��ϵ���
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
                // �����쳣�������¼��־
                Console.WriteLine($"Exception in Received method: {ex.Message}");
            }
            finally
            {
                // ִ�����������ȷ���ر� socket
                if (o is Socket socketSend)
                {
                    socketSend.Close();
                }
            }
        }
        /****************************************/
        //               ��ʾ��Ϣ����
        /****************************************/
        // ��ʾ����
        void ShowMsg(string Text)
        {
            SetString stcb = new SetString(AddListBoxString);
            this.Invoke(stcb, Text + "\r\n");
            SaveListBoxContents("tempDat.txt");
        }

        // �б���ʾ�����š������Ϣ���ã�
        void AddListBoxString(string msg)
        {


            // ����Ϣ��ӵ��б�򣬰������κźͱ��
            listBox_Weight1.Items.Add($"��{currentBatch}��\t���{currentItemNumber}��\t{msg}");

            // ��ÿ�����ݴﵽ��TotalNumber��ʱ���������κŲ����±��
            if (currentItemNumber % TotalNumber == 0)
            {
                currentBatch++;
                currentItemNumber = 1;

            }
            else
            {
                currentItemNumber++;
            }

            // ����ͼ��������ײ�
            listBox_Weight1.TopIndex = listBox_Weight1.Items.Count - 1;
        }

        // ��������
        void SetWeight(byte[] bytes)
        {
            // ��ǰ������ʾ����������λ��
            textBox_Weight.Text = Encoding.ASCII.GetString(bytes, 6, 8);
            textBox_Flats.Text = Encoding.ASCII.GetString(bytes, 14, 3);
            // ��ǰ����״̬�ж�
            string status;
            Color statusColor;
            if (bytes[0] == 'S')
            {
                status = "�ȶ�";
                statusColor = Color.Green; // ���� "�ȶ�" ״̬����ɫΪ��ɫ
            }
            else if (bytes[0] == 'U')
            {
                status = "���ȶ�";
                statusColor = Color.Orange; // ���� "���ȶ�" ״̬����ɫΪ��ɫ
            }
            else if (bytes[0] == 'O')
            {
                status = "����";
                statusColor = Color.Red; // ���� "����" ״̬����ɫΪ��ɫ
            }
            else
            {
                status = "δ֪״̬";
                statusColor = Color.Gray; // ���� "δ֪״̬" ״̬����ɫΪ��ɫ
            }
            // ��ǰ����״̬���
            label_State.Text = status;
            label_State.ForeColor = statusColor;
            label_State.Font = new Font(label_State.Font, FontStyle.Bold);

            // ����������Ϣ���
            if (status == "�ȶ�" && !StableDisplayed)
            {
                // �ַ���ת������ǰ��������λ��
                string str1 = Encoding.ASCII.GetString(bytes, 6, 8);
                string str2 = Encoding.ASCII.GetString(bytes, 14, 3);
                // ����ת������ǰ������
                string str3 = BitConverter.ToString(bytes, 6, 8).Replace("-", "");
                if (!string.Equals(str3, "202020302E303030") && !string.Equals(str3, "2020202020202030") && comboBox_ChanpinNumbei.Enabled==false)    //�жϵ�ǰ��������0g��0.000kg����202020302E303030:0.000kg    2020202020202030:0g
                {
                    ShowMsg(str1 + str2);
                    StableDisplayed = true;
                }
            }
            else if (status != "�ȶ�")
            {
                StableDisplayed = false;
            }
        }
        /****************************************/
        //               ��Ʒ����ѡ��
        /****************************************/
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // ����Ƿ�ѡ������Ч������
            if (int.TryParse(comboBox_ChanpinNumbei.SelectedItem.ToString(), out selectedNumber))
            {
                // �����������ʹ�� selectedNumber������ comboBox1 ��ѡ�������
                MessageBox.Show($"��ǰ��Ʒ����Ϊ��{selectedNumber}");
                TotalNumber = selectedNumber + 1;
            }
            else
            {
                MessageBox.Show("��Ч�Ĳ�Ʒ����ѡ��");
            }
        }
        /****************************************/
        //               ��Ϣ����
        /****************************************/
        // �����б���Ϣ����
        void SaveListBoxContents(string fileName)
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                foreach (var item in listBox_Weight1.Items)
                {
                    writer.WriteLine(item.ToString());
                }
            }

            //MessageBox.Show($"�б������ѱ��浽�ļ���{fileName}");
        }
        // �����б���Ϣ����
        //private void button_msg_Click(object sender, EventArgs e)
        //{
        //    SaveFileDialog saveFileDialog = new SaveFileDialog();
        //    saveFileDialog.Filter = "�ı��ļ� (*.txt)|*.txt";
        //    saveFileDialog.Title = "����ListBox1����";

        //    if (saveFileDialog.ShowDialog() == DialogResult.OK)
        //    {
        //        SaveListBoxContents(saveFileDialog.FileName);
        //    }
        //}

        private void button_del_Click(object sender, EventArgs e)
        {
            if (listBox_Weight1.Items.Count > 0)
            {
                // ��¼��ǰ��źͱ��
                int currentBatchBackup = currentBatch;
                int currentItemNumberBackup = currentItemNumber;

                // ɾ�����һ������
                listBox_Weight1.Items.RemoveAt(listBox_Weight1.Items.Count - 1);

                // ���ɾ�����Ǹ�������һ�����ݣ�����ż�һ���������
                if (currentItemNumberBackup % TotalNumber == 1)
                {
                    currentBatch--;
                    currentItemNumber = TotalNumber;
                }
                else
                {
                    // ���ɾ�����Ǹ�����м����ݣ��򽫱�ż�һ
                    currentItemNumber--;
                }
            }
            else
            {
                MessageBox.Show("�б�Ϊ�գ��޷�ɾ�����ݡ�");
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
                // ����ÿһ������
                //Console.WriteLine(line);
                listBox_Weight1.Items.Add($"{line}");
            }
        }

        private void button_finish_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("���γ���ȫ���������", "����", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.OK)
            {
                DateTime currentTime = DateTime.Now;
                string ssz = "ssz";
                string d = currentTime.ToString("yyyyMMddHHmmSS");
                string srcPath = "tempDat.txt";
                string dstPath = "����/" + currentTime.ToString("yyyy-MM-dd_HH-mm-ss") + ".txt";
                string excelFilePath = "����/"+d+".xlsx";
                //string excelFilePath = "sss.xlsx";
                try
                {
                    saveToDB(srcPath);//�����ݴ洢�����ݿ�DB
                    File.Copy(srcPath, dstPath);
                    comboBox_ChanpinNumbei.Enabled = true;

                    txtToExcel(srcPath, excelFilePath, int.Parse(comboBox_ChanpinNumbei.Text));
                    MessageBox.Show("���γ�����ɣ�");
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
                // ����ÿһ������
                //Console.WriteLine(line);
                if(line.Length > 0)
                {
                    string insert_sql = "INSERT INTO opk_data(code,data,group_times,datetime_1) VALUES(" +
                        $"'{textBox_code.Text}'," +
                        $"'{line.Substring(9).Replace("��","").Replace("	", "")}'," +
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
                button_start.Text = "���¿�ʼ";

                // �ͻ���Ϣ��ʾ
                textBox_ChanpinName.Text = "ƽ���ڵ���";
                textBox_ClientName.Text = "��XX";
            }
            else
            {
                comboBox_ChanpinNumbei.Enabled = true;
                button_start.Text = "��ʼ";
            }

        }

        private void txtToExcel(string filePath,string excelFilePath, int every)
        {
            try
            {
                XSSFWorkbook wb = new XSSFWorkbook();
                // ����������
                ISheet sheet = wb.CreateSheet("Sheet1");
                IRow title_row = sheet.CreateRow(0);
                title_row.CreateCell(0).SetCellValue("���Ա���");
                CellRangeAddress region = new CellRangeAddress(0, 0, 0, (every * 2) + 2);//�ϲ�
                //CellRangeAddress region = new CellRangeAddress(0, 0, 0, 4);//�ϲ�
                sheet.AddMergedRegion(region);
                IRow times_row = sheet.CreateRow(1);
                times_row.CreateCell(0).SetCellValue("������");
                for (int i = 1; i < every + 1; i++)
                {
                    times_row.CreateCell(i).SetCellValue(i);
                }
                //data_row.CreateCell(every + 1).SetCellValue("������");
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
                            string weight = line.Substring(9).Replace("��", "").Replace("	", "");
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

                    //string weight = line.Substring(9).Replace("��", "").Replace("	", "");
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

                // �����ļ�
                FileStream sw = File.Create(excelFilePath);
                wb.Write(sw);
                sw.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("����excel����;"+ex.StackTrace);
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
