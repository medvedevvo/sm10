using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;

namespace Terminal
{
    /***** Пользовательское окно *************************************************************************************/
    public partial class Form1 : Form
    {
        private COMPort com = new COMPort();
        private string path_root;
        private string path_file;

        //--- Конструктор по-умолчанию --------------------------------------------------------------------------------
        public Form1()
        {
            InitializeComponent();
            foreach(string port in SerialPort.GetPortNames())           // Заполнение списков COM-портов
                cb_port.Items.Add(port);

            cb_port.SelectedIndex = 1;
            cb_baudrate.SelectedIndex = 5;

            path_root = Path.GetFullPath(@"Terminal.exe").Remove(24);
            path_file = path_root + @"Test files\test_ok.txt";
            lbl_path.Text = path_file;

            of_dialog.InitialDirectory = path_root + @"Test files\";
        }

        //--- Действие при закрытии формы -----------------------------------------------------------------------------
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            com.close();
        }

        //--- Нажатие кнопки открытия/закрытия COM-порта --------------------------------------------------------------
        private void btn_open_close_Click(object sender, EventArgs e)
        {
            if (!com.state)                                             // Открыть COM-порт
            {
                string port_name = cb_port.Text;
                int port_rate = Convert.ToInt32(cb_baudrate.Text);
                com = new COMPort(port_name, port_rate);
                com.onRecieve += com_recieve_msg;
                com.onSend += com_send_msg;
                if (com.open())
                    btn_open_close.Text = "Закрыть";
            }
            else                                                        // Закрыть COM-порт
            {
                if (!com.close())
                    btn_open_close.Text = "Открыть";
            }
        }

        //--- Нажатие кнопки "Отправить" ------------------------------------------------------------------------------
        private void btn_send_Click(object sender, EventArgs e)
        {
            com_send(tb_send_command.Text);
        }

        //--- Выбор файла для отправки --------------------------------------------------------------------------------
        private void btn_open_file_Click(object sender, EventArgs e)
        {
            if(of_dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                path_file = of_dialog.FileName;
                lbl_path.Text = path_file;
            }

        }

        //--- Отправка файла ------------------------------------------------------------------------------------------
        private void btn_send_file_Click(object sender, EventArgs e)
        {
            StreamReader streamReader;
            bool txt = true; 

            try
            {
                streamReader = new StreamReader(path_file);
                while (!streamReader.EndOfStream && txt)
                {
                    string str = streamReader.ReadLine();
                    for (int i = 1; i < str.Length; i += 2)
                    {
                        if ((str[i] == '\0') && (str[i - 1] == '\0'))
                        {
                            txt = false;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка чтения файла: " + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return ;
            }

            if (!txt)
                MessageBox.Show("Ошибка чтения файла: файл не является текстовым.", "Error!",
                                     MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if(!com.state)
                MessageBox.Show("Ошибка открытия порта: COM-порт закрыт!", "Error!",
                                     MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                streamReader.BaseStream.Position = 0;
                while (!streamReader.EndOfStream)
                    com_send(streamReader.ReadLine());
            }               
        }

        //--- Отправка команды по COM-порту ---------------------------------------------------------------------------
        private void com_send(string text)
        {
            com.send(text);
        }
        
        //--- Обработчик отправки сообщений ---------------------------------------------------------------------------
        private void com_send_msg(string text)
        {
            rtb_send.Text += text + Environment.NewLine;
            rtb_send.SelectionStart = rtb_send.Text.Length;
            rtb_send.ScrollToCaret();
        }

        //--- Обработчик приема сообщений -----------------------------------------------------------------------------
        private void com_recieve_msg(string msg)
        {
            rtb_recieved.Invoke((MethodInvoker)(delegate()
            {
                rtb_recieved.Text += msg + Environment.NewLine;
                rtb_recieved.SelectionStart = rtb_recieved.Text.Length;
                rtb_recieved.ScrollToCaret();
            }));
        }
    }

}
