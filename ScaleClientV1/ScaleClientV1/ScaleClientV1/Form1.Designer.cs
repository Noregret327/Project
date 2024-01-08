namespace ScaleClientV1
{
    partial class Form1
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
            textBox1 = new TextBox();
            label1 = new Label();
            label2 = new Label();
            textBox2 = new TextBox();
            button1 = new Button();
            groupBox1 = new GroupBox();
            listBox1 = new ListBox();
            button_del = new Button();
            button_excel = new Button();
            label3 = new Label();
            textBox3 = new TextBox();
            textBox4 = new TextBox();
            label77 = new Label();
            label4 = new Label();
            label7 = new Label();
            comboBox1 = new ComboBox();
            button_finish = new Button();
            button_load = new Button();
            textBox_code = new TextBox();
            label5 = new Label();
            button_start = new Button();
            groupBox2 = new GroupBox();
            label13 = new Label();
            label12 = new Label();
            label11 = new Label();
            label10 = new Label();
            label9 = new Label();
            label8 = new Label();
            label6 = new Label();
            textBox_connect = new TextBox();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Location = new Point(171, 20);
            textBox1.Margin = new Padding(6, 5, 6, 5);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(182, 35);
            textBox1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(27, 23);
            label1.Name = "label1";
            label1.Size = new Size(103, 30);
            label1.TabIndex = 1;
            label1.Text = "电子秤IP:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(414, 26);
            label2.Name = "label2";
            label2.Size = new Size(57, 30);
            label2.TabIndex = 3;
            label2.Text = "端口";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(558, 23);
            textBox2.Margin = new Padding(6, 5, 6, 5);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(182, 35);
            textBox2.TabIndex = 2;
            textBox2.Text = "55555";
            // 
            // button1
            // 
            button1.Location = new Point(811, 20);
            button1.Name = "button1";
            button1.Size = new Size(157, 42);
            button1.TabIndex = 4;
            button1.Text = "IP连接";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(listBox1);
            groupBox1.Location = new Point(12, 133);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(728, 783);
            groupBox1.TabIndex = 5;
            groupBox1.TabStop = false;
            groupBox1.Text = "称重数据";
            // 
            // listBox1
            // 
            listBox1.Dock = DockStyle.Fill;
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 28;
            listBox1.Location = new Point(3, 31);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(722, 749);
            listBox1.TabIndex = 0;
            // 
            // button_del
            // 
            button_del.Location = new Point(804, 826);
            button_del.Name = "button_del";
            button_del.Size = new Size(157, 42);
            button_del.TabIndex = 6;
            button_del.Text = "删除";
            button_del.UseVisualStyleBackColor = true;
            button_del.Click += button_del_Click;
            // 
            // button_excel
            // 
            button_excel.Location = new Point(790, 874);
            button_excel.Name = "button_excel";
            button_excel.Size = new Size(192, 42);
            button_excel.TabIndex = 8;
            button_excel.Text = "导入Excel模板";
            button_excel.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(821, 184);
            label3.Name = "label3";
            label3.Size = new Size(123, 30);
            label3.TabIndex = 9;
            label3.Text = "当前重量：";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(777, 238);
            textBox3.Margin = new Padding(6, 5, 6, 5);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(205, 35);
            textBox3.TabIndex = 10;
            // 
            // textBox4
            // 
            textBox4.Location = new Point(902, 238);
            textBox4.Margin = new Padding(6, 5, 6, 5);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(80, 35);
            textBox4.TabIndex = 11;
            // 
            // label77
            // 
            label77.AutoSize = true;
            label77.Location = new Point(811, 288);
            label77.Name = "label77";
            label77.Size = new Size(123, 30);
            label77.TabIndex = 12;
            label77.Text = "称重状态：";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(832, 341);
            label4.Name = "label4";
            label4.Size = new Size(57, 30);
            label4.TabIndex = 13;
            label4.Text = "状态";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(804, 393);
            label7.Name = "label7";
            label7.Size = new Size(164, 30);
            label7.TabIndex = 14;
            label7.Text = "产品数量(每盒):";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20" });
            comboBox1.Location = new Point(777, 445);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(205, 36);
            comboBox1.TabIndex = 15;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // button_finish
            // 
            button_finish.Location = new Point(804, 615);
            button_finish.Name = "button_finish";
            button_finish.Size = new Size(157, 91);
            button_finish.TabIndex = 17;
            button_finish.Text = "全部完成";
            button_finish.UseVisualStyleBackColor = true;
            button_finish.Click += button_finish_Click;
            // 
            // button_load
            // 
            button_load.Location = new Point(804, 750);
            button_load.Name = "button_load";
            button_load.Size = new Size(157, 42);
            button_load.TabIndex = 18;
            button_load.Text = "读取";
            button_load.UseVisualStyleBackColor = true;
            // 
            // textBox_code
            // 
            textBox_code.Location = new Point(746, 144);
            textBox_code.Margin = new Padding(6, 5, 6, 5);
            textBox_code.Name = "textBox_code";
            textBox_code.Size = new Size(268, 35);
            textBox_code.TabIndex = 19;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(821, 104);
            label5.Name = "label5";
            label5.Size = new Size(123, 30);
            label5.TabIndex = 20;
            label5.Text = "当前条码：";
            // 
            // button_start
            // 
            button_start.Location = new Point(804, 500);
            button_start.Name = "button_start";
            button_start.Size = new Size(157, 91);
            button_start.TabIndex = 21;
            button_start.Text = "开始";
            button_start.UseVisualStyleBackColor = true;
            button_start.Click += button_start_Click;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(label13);
            groupBox2.Controls.Add(label12);
            groupBox2.Controls.Add(label11);
            groupBox2.Controls.Add(label10);
            groupBox2.Controls.Add(label9);
            groupBox2.Controls.Add(label8);
            groupBox2.Controls.Add(label6);
            groupBox2.Location = new Point(1023, 24);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(276, 892);
            groupBox2.TabIndex = 22;
            groupBox2.TabStop = false;
            groupBox2.Text = "操作说明";
            groupBox2.Enter += groupBox2_Enter;
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
            textBox_connect.Location = new Point(15, 83);
            textBox_connect.Name = "textBox_connect";
            textBox_connect.ReadOnly = true;
            textBox_connect.Size = new Size(725, 35);
            textBox_connect.TabIndex = 23;
            textBox_connect.Text = "未连接";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(13F, 28F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1303, 928);
            Controls.Add(textBox_connect);
            Controls.Add(groupBox2);
            Controls.Add(button_start);
            Controls.Add(label5);
            Controls.Add(textBox_code);
            Controls.Add(button_load);
            Controls.Add(button_finish);
            Controls.Add(comboBox1);
            Controls.Add(label7);
            Controls.Add(label4);
            Controls.Add(label77);
            Controls.Add(textBox4);
            Controls.Add(textBox3);
            Controls.Add(label3);
            Controls.Add(button_excel);
            Controls.Add(button_del);
            Controls.Add(groupBox1);
            Controls.Add(button1);
            Controls.Add(label2);
            Controls.Add(textBox2);
            Controls.Add(label1);
            Controls.Add(textBox1);
            Font = new Font("Microsoft YaHei UI", 16F, FontStyle.Regular, GraphicsUnit.Point);
            Margin = new Padding(6, 5, 6, 5);
            Name = "Form1";
            Text = "OPK_电子秤";
            Load += Form1_Load;
            groupBox1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private Label label1;
        private Label label2;
        private TextBox textBox2;
        private Button button1;
        private GroupBox groupBox1;
        private ListBox listBox1;
        private Button button_del;
        private Button button_excel;
        private Label label3;
        private TextBox textBox3;
        private TextBox textBox4;
        private Label label77;
        private Label label4;
        private Label label7;
        private ComboBox comboBox1;
        private Button button_finish;
        private Button button_load;
        private TextBox textBox_code;
        private Label label5;
        private Button button_start;
        private GroupBox groupBox2;
        private TextBox textBox_connect;
        private Label label9;
        private Label label8;
        private Label label6;
        private Label label13;
        private Label label12;
        private Label label11;
        private Label label10;
    }
}