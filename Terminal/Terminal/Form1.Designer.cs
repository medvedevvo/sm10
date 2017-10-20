namespace Terminal
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cb_baudrate = new System.Windows.Forms.ComboBox();
            this.btn_open_close = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cb_port = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.rtb_recieved = new System.Windows.Forms.RichTextBox();
            this.rtb_send = new System.Windows.Forms.RichTextBox();
            this.btn_send = new System.Windows.Forms.Button();
            this.tb_send_command = new System.Windows.Forms.TextBox();
            this.btn_send_file = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.lbl_path = new System.Windows.Forms.Label();
            this.btn_open_file = new System.Windows.Forms.Button();
            this.of_dialog = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cb_baudrate);
            this.groupBox1.Controls.Add(this.btn_open_close);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cb_port);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(283, 77);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Настройки соединения";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(115, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "BaudRate";
            // 
            // cb_baudrate
            // 
            this.cb_baudrate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_baudrate.FormattingEnabled = true;
            this.cb_baudrate.Items.AddRange(new object[] {
            "9600",
            "14400",
            "19200",
            "38400",
            "57600",
            "115200"});
            this.cb_baudrate.Location = new System.Drawing.Point(118, 32);
            this.cb_baudrate.Name = "cb_baudrate";
            this.cb_baudrate.Size = new System.Drawing.Size(73, 21);
            this.cb_baudrate.TabIndex = 2;
            // 
            // btn_open_close
            // 
            this.btn_open_close.Location = new System.Drawing.Point(210, 32);
            this.btn_open_close.Name = "btn_open_close";
            this.btn_open_close.Size = new System.Drawing.Size(67, 21);
            this.btn_open_close.TabIndex = 1;
            this.btn_open_close.Text = "Открыть";
            this.btn_open_close.UseVisualStyleBackColor = true;
            this.btn_open_close.Click += new System.EventHandler(this.btn_open_close_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Port";
            // 
            // cb_port
            // 
            this.cb_port.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_port.FormattingEnabled = true;
            this.cb_port.Location = new System.Drawing.Point(6, 32);
            this.cb_port.Name = "cb_port";
            this.cb_port.Size = new System.Drawing.Size(88, 21);
            this.cb_port.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(237, 172);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Получено";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(58, 172);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Отправлено";
            // 
            // rtb_recieved
            // 
            this.rtb_recieved.Location = new System.Drawing.Point(182, 188);
            this.rtb_recieved.Name = "rtb_recieved";
            this.rtb_recieved.ReadOnly = true;
            this.rtb_recieved.Size = new System.Drawing.Size(164, 180);
            this.rtb_recieved.TabIndex = 9;
            this.rtb_recieved.Text = "";
            // 
            // rtb_send
            // 
            this.rtb_send.Location = new System.Drawing.Point(12, 188);
            this.rtb_send.Name = "rtb_send";
            this.rtb_send.ReadOnly = true;
            this.rtb_send.Size = new System.Drawing.Size(164, 180);
            this.rtb_send.TabIndex = 8;
            this.rtb_send.Text = "";
            // 
            // btn_send
            // 
            this.btn_send.Location = new System.Drawing.Point(223, 91);
            this.btn_send.Name = "btn_send";
            this.btn_send.Size = new System.Drawing.Size(72, 23);
            this.btn_send.TabIndex = 12;
            this.btn_send.Text = "Отправить";
            this.btn_send.UseVisualStyleBackColor = true;
            this.btn_send.Click += new System.EventHandler(this.btn_send_Click);
            // 
            // tb_send_command
            // 
            this.tb_send_command.Location = new System.Drawing.Point(13, 94);
            this.tb_send_command.Name = "tb_send_command";
            this.tb_send_command.Size = new System.Drawing.Size(204, 20);
            this.tb_send_command.TabIndex = 13;
            // 
            // btn_send_file
            // 
            this.btn_send_file.Location = new System.Drawing.Point(123, 120);
            this.btn_send_file.Name = "btn_send_file";
            this.btn_send_file.Size = new System.Drawing.Size(106, 23);
            this.btn_send_file.TabIndex = 15;
            this.btn_send_file.Text = "Отправить файл";
            this.btn_send_file.UseVisualStyleBackColor = true;
            this.btn_send_file.Click += new System.EventHandler(this.btn_send_file_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 146);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Путь:";
            // 
            // lbl_path
            // 
            this.lbl_path.AutoSize = true;
            this.lbl_path.Location = new System.Drawing.Point(49, 146);
            this.lbl_path.Name = "lbl_path";
            this.lbl_path.Size = new System.Drawing.Size(22, 13);
            this.lbl_path.TabIndex = 17;
            this.lbl_path.Text = "C:/";
            // 
            // btn_open_file
            // 
            this.btn_open_file.Location = new System.Drawing.Point(14, 120);
            this.btn_open_file.Name = "btn_open_file";
            this.btn_open_file.Size = new System.Drawing.Size(98, 23);
            this.btn_open_file.TabIndex = 18;
            this.btn_open_file.Text = "Открыть файл...";
            this.btn_open_file.UseVisualStyleBackColor = true;
            this.btn_open_file.Click += new System.EventHandler(this.btn_open_file_Click);
            // 
            // of_dialog
            // 
            this.of_dialog.Filter = "Текстовый файл (*.txt)|*.txt|All files (*.*)|*.*";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(351, 380);
            this.Controls.Add(this.btn_open_file);
            this.Controls.Add(this.lbl_path);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btn_send_file);
            this.Controls.Add(this.tb_send_command);
            this.Controls.Add(this.btn_send);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.rtb_recieved);
            this.Controls.Add(this.rtb_send);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Терминал V1.0";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cb_baudrate;
        private System.Windows.Forms.Button btn_open_close;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cb_port;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RichTextBox rtb_recieved;
        private System.Windows.Forms.RichTextBox rtb_send;
        private System.Windows.Forms.Button btn_send;
        private System.Windows.Forms.TextBox tb_send_command;
        private System.Windows.Forms.Button btn_send_file;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbl_path;
        private System.Windows.Forms.Button btn_open_file;
        private System.Windows.Forms.OpenFileDialog of_dialog;
    }
}

