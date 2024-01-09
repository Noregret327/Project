namespace ScaleClientV1
{
    partial class Form_MainUI
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            textBox_IP = new TextBox();
            label_IP = new Label();
            label_Port = new Label();
            textBox_Port = new TextBox();
            button_IPConnect = new Button();
            groupBox_Weight1 = new GroupBox();
            listBox_Weight1 = new ListBox();
            button_del = new Button();
            button_excel = new Button();
            label_Current_Weight = new Label();
            textBox_Weight = new TextBox();
            textBox_Flats = new TextBox();
            label_CurrentState = new Label();
            label_State = new Label();
            label_ChanpinNumber = new Label();
            comboBox_ChanpinNumbei = new ComboBox();
            button_finish = new Button();
            button_load = new Button();
            textBox_code = new TextBox();
            label_CurrentCode = new Label();
            button_start = new Button();
            groupBox_Controlillustrate = new GroupBox();
            label13 = new Label();
            label12 = new Label();
            label11 = new Label();
            label10 = new Label();
            label9 = new Label();
            label8 = new Label();
            label6 = new Label();
            textBox_connect = new TextBox();
            label_ChanpinName = new Label();
            textBox_ChanpinName = new TextBox();
            label_ClientName = new Label();
            textBox_ClientName = new TextBox();
            groupBox_Weight1.SuspendLayout();
            groupBox_Controlillustrate.SuspendLayout();
            SuspendLayout();
            // 
            // textBox_IP
            // 
            textBox_IP.Location = new Point(139, 18);
            textBox_IP.Margin = new Padding(6, 5, 6, 5);
            textBox_IP.Name = "textBox_IP";
            textBox_IP.Size = new Size(266, 35);
            textBox_IP.TabIndex = 0;
            textBox_IP.TextAlign = HorizontalAlignment.Center;
            // 
            // label_IP
            // 
            label_IP.AutoSize = true;
            label_IP.Location = new Point(18, 23);
            label_IP.Name = "label_IP";
            label_IP.Size = new Size(103, 30);
            label_IP.TabIndex = 1;
            label_IP.Text = "电子秤IP:";
            // 
            // label_Port
            // 
            label_Port.AutoSize = true;
            label_Port.Location = new Point(425, 23);
            label_Port.Name = "label_Port";
            label_Port.Size = new Size(79, 30);
            label_Port.TabIndex = 3;
            label_Port.Text = "端口：";
            // 
            // textBox_Port
            // 
            textBox_Port.Location = new Point(524, 18);
            textBox_Port.Margin = new Padding(6, 5, 6, 5);
            textBox_Port.Name = "textBox_Port";
            textBox_Port.Size = new Size(216, 35);
            textBox_Port.TabIndex = 2;
            textBox_Port.Text = "55555";
            textBox_Port.TextAlign = HorizontalAlignment.Center;
            // 
            // button_IPConnect
            // 
            button_IPConnect.Location = new Point(756, 14);
            button_IPConnect.Name = "button_IPConnect";
            button_IPConnect.Size = new Size(258, 99);
            button_IPConnect.TabIndex = 4;
            button_IPConnect.Text = "IP连接";
            button_IPConnect.UseVisualStyleBackColor = true;
            button_IPConnect.Click += button1_Click;
            // 
            // groupBox_Weight1
            // 
            groupBox_Weight1.Controls.Add(listBox_Weight1);
            groupBox_Weight1.Location = new Point(12, 216);
            groupBox_Weight1.Name = "groupBox_Weight1";
            groupBox_Weight1.Size = new Size(728, 700);
            groupBox_Weight1.TabIndex = 5;
            groupBox_Weight1.TabStop = false;
            groupBox_Weight1.Text = "重量";
            // 
            // listBox_Weight1
            // 
            listBox_Weight1.Dock = DockStyle.Fill;
            listBox_Weight1.FormattingEnabled = true;
            listBox_Weight1.ItemHeight = 28;
            listBox_Weight1.Location = new Point(3, 31);
            listBox_Weight1.Name = "listBox_Weight1";
            listBox_Weight1.Size = new Size(722, 666);
            listBox_Weight1.TabIndex = 0;
            // 
            // button_del
            // 
            button_del.Location = new Point(811, 757);
            button_del.Name = "button_del";
            button_del.Size = new Size(157, 42);
            button_del.TabIndex = 6;
            button_del.Text = "删除";
            button_del.UseVisualStyleBackColor = true;
            button_del.Click += button_del_Click;
            // 
            // button_excel
            // 
            button_excel.Location = new Point(797, 822);
            button_excel.Name = "button_excel";
            button_excel.Size = new Size(192, 42);
            button_excel.TabIndex = 8;
            button_excel.Text = "导入Excel模板";
            button_excel.UseVisualStyleBackColor = true;
            // 
            // label_Current_Weight
            // 
            label_Current_Weight.AutoSize = true;
            label_Current_Weight.Location = new Point(832, 332);
            label_Current_Weight.Name = "label_Current_Weight";
            label_Current_Weight.Size = new Size(123, 30);
            label_Current_Weight.TabIndex = 9;
            label_Current_Weight.Text = "当前重量：";
            // 
            // textBox_Weight
            // 
            textBox_Weight.Location = new Point(756, 377);
            textBox_Weight.Margin = new Padding(6, 5, 6, 5);
            textBox_Weight.Name = "textBox_Weight";
            textBox_Weight.Size = new Size(241, 35);
            textBox_Weight.TabIndex = 10;
            // 
            // textBox_Flats
            // 
            textBox_Flats.Location = new Point(934, 377);
            textBox_Flats.Margin = new Padding(6, 5, 6, 5);
            textBox_Flats.Name = "textBox_Flats";
            textBox_Flats.Size = new Size(80, 35);
            textBox_Flats.TabIndex = 11;
            // 
            // label_CurrentState
            // 
            label_CurrentState.AutoSize = true;
            label_CurrentState.Location = new Point(832, 243);
            label_CurrentState.Name = "label_CurrentState";
            label_CurrentState.Size = new Size(123, 30);
            label_CurrentState.TabIndex = 12;
            label_CurrentState.Text = "称重状态：";
            // 
            // label_State
            // 
            label_State.AutoSize = true;
            label_State.Location = new Point(851, 289);
            label_State.Name = "label_State";
            label_State.Size = new Size(57, 30);
            label_State.TabIndex = 13;
            label_State.Text = "状态";
            // 
            // label_ChanpinNumber
            // 
            label_ChanpinNumber.AutoSize = true;
            label_ChanpinNumber.Location = new Point(425, 181);
            label_ChanpinNumber.Name = "label_ChanpinNumber";
            label_ChanpinNumber.Size = new Size(164, 30);
            label_ChanpinNumber.TabIndex = 14;
            label_ChanpinNumber.Text = "产品数量(每盒):";
            // 
            // comboBox_ChanpinNumbei
            // 
            comboBox_ChanpinNumbei.FormattingEnabled = true;
            comboBox_ChanpinNumbei.Items.AddRange(new object[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20" });
            comboBox_ChanpinNumbei.Location = new Point(607, 175);
            comboBox_ChanpinNumbei.Name = "comboBox_ChanpinNumbei";
            comboBox_ChanpinNumbei.Size = new Size(130, 36);
            comboBox_ChanpinNumbei.TabIndex = 15;
            comboBox_ChanpinNumbei.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // button_finish
            // 
            button_finish.Location = new Point(811, 563);
            button_finish.Name = "button_finish";
            button_finish.Size = new Size(157, 91);
            button_finish.TabIndex = 17;
            button_finish.Text = "全部完成";
            button_finish.UseVisualStyleBackColor = true;
            button_finish.Click += button_finish_Click;
            // 
            // button_load
            // 
            button_load.Location = new Point(811, 698);
            button_load.Name = "button_load";
            button_load.Size = new Size(157, 42);
            button_load.TabIndex = 18;
            button_load.Text = "读取";
            button_load.UseVisualStyleBackColor = true;
            // 
            // textBox_code
            // 
            textBox_code.Location = new Point(139, 127);
            textBox_code.Margin = new Padding(6, 5, 6, 5);
            textBox_code.Name = "textBox_code";
            textBox_code.Size = new Size(258, 35);
            textBox_code.TabIndex = 19;
            textBox_code.TextAlign = HorizontalAlignment.Center;
            // 
            // label_CurrentCode
            // 
            label_CurrentCode.AutoSize = true;
            label_CurrentCode.Location = new Point(18, 132);
            label_CurrentCode.Name = "label_CurrentCode";
            label_CurrentCode.Size = new Size(123, 30);
            label_CurrentCode.TabIndex = 20;
            label_CurrentCode.Text = "当前条码：";
            // 
            // button_start
            // 
            button_start.Location = new Point(756, 127);
            button_start.Name = "button_start";
            button_start.Size = new Size(258, 91);
            button_start.TabIndex = 21;
            button_start.Text = "开始";
            button_start.UseVisualStyleBackColor = true;
            button_start.Click += button_start_Click;
            // 
            // groupBox_Controlillustrate
            // 
            groupBox_Controlillustrate.Controls.Add(label13);
            groupBox_Controlillustrate.Controls.Add(label12);
            groupBox_Controlillustrate.Controls.Add(label11);
            groupBox_Controlillustrate.Controls.Add(label10);
            groupBox_Controlillustrate.Controls.Add(label9);
            groupBox_Controlillustrate.Controls.Add(label8);
            groupBox_Controlillustrate.Controls.Add(label6);
            groupBox_Controlillustrate.Location = new Point(1023, 24);
            groupBox_Controlillustrate.Name = "groupBox_Controlillustrate";
            groupBox_Controlillustrate.Size = new Size(276, 892);
            groupBox_Controlillustrate.TabIndex = 22;
            groupBox_Controlillustrate.TabStop = false;
            groupBox_Controlillustrate.Text = "操作说明";
            groupBox_Controlillustrate.Enter += groupBox2_Enter;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(6, 353);
            label13.Name = "label13";
            label13.Size = new Size(183, 30);
            label13.TabIndex = 8;
            label13.Text = "在”报表“文件夹中";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(6, 304);
            label12.Name = "label12";
            label12.Size = new Size(219, 30);
            label12.TabIndex = 7;
            label12.Text = "点击完成后生成excel";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(6, 255);
            label11.Name = "label11";
            label11.Size = new Size(255, 30);
            label11.TabIndex = 6;
            label11.Text = "称重过程中支持自动存储";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(6, 206);
            label10.Name = "label10";
            label10.Size = new Size(229, 30);
            label10.TabIndex = 5;
            label10.Text = "4.完成后，点全部完成";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(6, 157);
            label9.Name = "label9";
            label9.Size = new Size(126, 30);
            label9.TabIndex = 4;
            label9.Text = "3.点击 开始";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(6, 108);
            label8.Name = "label8";
            label8.Size = new Size(251, 30);
            label8.TabIndex = 3;
            label8.Text = "2.选择产品数量（每盒）";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(6, 59);
            label6.Name = "label6";
            label6.Size = new Size(138, 30);
            label6.TabIndex = 2;
            label6.Text = "1.点击IP连接";
            // 
            // textBox_connect
            // 
            textBox_connect.ForeColor = Color.White;
            textBox_connect.Location = new Point(18, 78);
            textBox_connect.Name = "textBox_connect";
            textBox_connect.ReadOnly = true;
            textBox_connect.Size = new Size(719, 35);
            textBox_connect.TabIndex = 23;
            textBox_connect.Text = "未连接";
            textBox_connect.TextAlign = HorizontalAlignment.Center;
            // 
            // label_ChanpinName
            // 
            label_ChanpinName.AutoSize = true;
            label_ChanpinName.Location = new Point(19, 181);
            label_ChanpinName.Name = "label_ChanpinName";
            label_ChanpinName.Size = new Size(123, 30);
            label_ChanpinName.TabIndex = 12;
            label_ChanpinName.Text = "产品名称：";
            // 
            // textBox_ChanpinName
            // 
            textBox_ChanpinName.Location = new Point(140, 176);
            textBox_ChanpinName.Name = "textBox_ChanpinName";
            textBox_ChanpinName.Size = new Size(257, 35);
            textBox_ChanpinName.TabIndex = 24;
            textBox_ChanpinName.TextAlign = HorizontalAlignment.Center;
            // 
            // label_ClientName
            // 
            label_ClientName.AutoSize = true;
            label_ClientName.Location = new Point(425, 130);
            label_ClientName.Name = "label_ClientName";
            label_ClientName.Size = new Size(123, 30);
            label_ClientName.TabIndex = 12;
            label_ClientName.Text = "客户名称：";
            // 
            // textBox_ClientName
            // 
            textBox_ClientName.Location = new Point(537, 130);
            textBox_ClientName.Name = "textBox_ClientName";
            textBox_ClientName.Size = new Size(200, 35);
            textBox_ClientName.TabIndex = 24;
            textBox_ClientName.TextAlign = HorizontalAlignment.Center;
            // 
            // Form_MainUI
            // 
            AutoScaleDimensions = new SizeF(13F, 28F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1303, 919);
            Controls.Add(textBox_ClientName);
            Controls.Add(textBox_ChanpinName);
            Controls.Add(textBox_connect);
            Controls.Add(groupBox_Controlillustrate);
            Controls.Add(button_start);
            Controls.Add(label_CurrentCode);
            Controls.Add(textBox_code);
            Controls.Add(button_load);
            Controls.Add(button_finish);
            Controls.Add(comboBox_ChanpinNumbei);
            Controls.Add(label_ChanpinNumber);
            Controls.Add(label_State);
            Controls.Add(label_ClientName);
            Controls.Add(label_ChanpinName);
            Controls.Add(label_CurrentState);
            Controls.Add(textBox_Flats);
            Controls.Add(textBox_Weight);
            Controls.Add(label_Current_Weight);
            Controls.Add(button_excel);
            Controls.Add(button_del);
            Controls.Add(groupBox_Weight1);
            Controls.Add(button_IPConnect);
            Controls.Add(label_Port);
            Controls.Add(textBox_Port);
            Controls.Add(label_IP);
            Controls.Add(textBox_IP);
            Font = new Font("Microsoft YaHei UI", 16F, FontStyle.Regular, GraphicsUnit.Point);
            Margin = new Padding(6, 5, 6, 5);
            Name = "Form_MainUI";
            Text = "欧派克智能称重系统";
            Load += Form1_Load;
            groupBox_Weight1.ResumeLayout(false);
            groupBox_Controlillustrate.ResumeLayout(false);
            groupBox_Controlillustrate.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox_IP;
        private Label label_IP;
        private Label label_Port;
        private TextBox textBox_Port;
        private Button button_IPConnect;
        private GroupBox groupBox_Weight1;
        private ListBox listBox_Weight1;
        private Button button_del;
        private Button button_excel;
        private Label label_Current_Weight;
        private TextBox textBox_Weight;
        private TextBox textBox_Flats;
        private Label label_CurrentState;
        private Label label_State;
        private Label label_ChanpinNumber;
        private ComboBox comboBox_ChanpinNumbei;
        private Button button_finish;
        private Button button_load;
        private TextBox textBox_code;
        private Label label_CurrentCode;
        private Button button_start;
        private GroupBox groupBox_Controlillustrate;
        private TextBox textBox_connect;
        private Label label9;
        private Label label8;
        private Label label6;
        private Label label13;
        private Label label12;
        private Label label11;
        private Label label10;
        private Label label_ChanpinName;
        private TextBox textBox_ChanpinName;
        private Label label_ClientName;
        private TextBox textBox_ClientName;
    }
}