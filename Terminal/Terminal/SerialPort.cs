﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace Terminal
{
    /***** Класс COM-порта *******************************************************************************************/
    public class COMPort
    {
        private SerialPort port;
        private CommandWorkerFacade cwf = new CommandWorkerFacade();
        public bool state = false;


        public delegate void ComReciever(string msg);
        public event ComReciever onRecieve;
        public delegate void ComSender(string msg);
        public event ComSender onSend;

        //--- Конструкторы --------------------------------------------------------------------------------------------
        public COMPort()
        {
            port = new SerialPort();
        }
        public COMPort(string port_name, int rate)
        {
            port = new SerialPort(port_name, rate, Parity.None, 8, StopBits.One);
            port.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

            string temp = "";
            for (int i = 0; i < cwf.objects_list.Count; i++)
            {
                temp += "Имя объекта: " + cwf.objects_list[i].name + "\n";
                for (int j = 0; j < cwf.objects_list[i].parameters.Count; j++)
                {
                    temp += "\t" + cwf.objects_list[i].parameters[j].name + //" = " + 
                                   //cwf.objects_list[i].paramenters[j].key + 
                                   "\n";
                }

                temp += "\n";
            }
                MessageBox.Show(temp);
        }

        //--- Открыть COM-порт ----------------------------------------------------------------------------------------
        public bool open()
        {
            try
            {
                port.Handshake = Handshake.None;
                port.WriteTimeout = 500;
                port.Open();
                state = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка открытия порта: " + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return state;
        }

        //--- Закрыть COM-порт ----------------------------------------------------------------------------------------
        public bool close()
        {
            port.Close();
            state = false;

            return state;
        }

        //--- Отправить строку по COM-порту ---------------------------------------------------------------------------
        public string send(string str)
        {
            try
            {
                port.Write(str);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка отправки: " + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }

            onSend(str);

            return str;
        }

        //--- Прием данных по COM-порту -------------------------------------------------------------------------------
        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string indata = sp.ReadExisting();
            onRecieve(indata);
        }       
    }
}
