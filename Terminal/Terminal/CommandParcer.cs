using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Terminal
{
    /***** Обработчик команд *****************************************************************************************/
    public class CommandParcer
    {
        private DBKeyWords dbKW = DBKeyWords.getInstance();                     // БД ключевых слов протокола

        //--- Разбивка команды ----------------------------------------------------------------------------------------
        public Command parce(string inboxMessage)
        {
            Command command = new Command("", false, "", new List<string>());

            if (inboxMessage[0] != dbKW.StartTag[0]) return command;            // Проверка, что пришла именно команда

            string msg = inboxMessage.Remove(0, 1);

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
