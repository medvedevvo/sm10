using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Terminal
{
    /***** Обработчик команд *****************************************************************************************/
    public class CommandParcer
    {
        private static CommandParcer instance;                          // Ссылка на текущий объект
        private DBKeyWords dbKW = DBKeyWords.getInstance();             // БД ключевых слов протокола

        //--- Конструктор класса (внутренний) -------------------------------------------------------------------------
        private CommandParcer()
        {
        }

        //--- Конструктор класса (внешний) ----------------------------------------------------------------------------
        public static CommandParcer getInstance()
        {
            if (instance == null)
                instance = new CommandParcer();
            return instance;
        }

        //--- Разбивка команды ----------------------------------------------------------------------------------------
        public Command parce(string inboxMessage)
        {
            Command command = new Command("", false, "", new List<string>());

            //if (inboxMessage[0] != dbKW.StartTag[0]) return command;            // Проверка, что пришла именно команда

            List<string> full_msg = inboxMessage.Split(dbKW.StartTag[0]).ToList();
            if ((full_msg.Count != 2) || (full_msg[0].Length < 1))
                return command;
            string msg = full_msg[1];

            int char_num = Convert.ToInt32(full_msg[0]) - dbKW.StartTag.Length; // Проверка на целостность
            if(char_num != msg.Length)
                return command;

            List<string> temp = msg.Split(dbKW.SpaceTag[0]).ToList();           // Вычленение типа
            if (temp[0].Length > (dbKW.AnswerTag.Length + 1)) 
                return command;
            else if (temp[0].Length == (dbKW.AnswerTag.Length + 1))
            {
                string type = temp[0].Remove(1);
                temp[0] = temp[0].Remove(0, 1);
                if (temp[0] != dbKW.AnswerTag) return command;
                command.type = type;
                command.withApply = true;
                msg = msg.Remove(0, (dbKW.AnswerTag.Length + 2));
            }
            else
            {
                command.type = temp[0];
                msg = msg.Remove(0, 2);
            }

            string end_tag = msg.Substring(msg.Length - 1);
            msg = msg.Remove(msg.Length - 1);

            int pos_begin_tag = msg.IndexOf(dbKW.ParamBeginTag);                // Обработка команд с параметрами
            if(pos_begin_tag >= 0)                                              
            {
                int pos_end_tag = msg.IndexOf(dbKW.ParamEndTag);
                if (pos_end_tag < pos_begin_tag)                                // Потерян тег завершения 
                {
                    command.type = "";
                    return command;
                }
                string param_str = msg.Substring(pos_begin_tag + 1, pos_end_tag - pos_begin_tag - 1);
                command.data = param_str.Split(dbKW.SeparatorTag[0]).ToList();
                msg = msg.Remove(pos_begin_tag);

                List<string> temp2 = msg.Split(dbKW.CountTag[0]).ToList();
                if (temp2.Count < 2)
                {
                    command.type = "";
                    return command;
                }
                if (Convert.ToInt32(temp2[1]) != command.data.Count)
                {
                    command.type = "";
                    return command;
                }
                command.key = temp2[0];
            }
            else                                                                // Обработка команд без параметров
            {
                command.key = msg;
            }
            
            
            return command;
        }
    }
}
