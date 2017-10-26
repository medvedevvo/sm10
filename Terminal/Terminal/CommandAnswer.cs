using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Terminal
{
    /***** Генератор ответов *****************************************************************************************/
    public class CommandAnswer
    {
        private static CommandAnswer instance;                          // Ссылка на текущий объект
        private DBKeyWords dbKW = DBKeyWords.getInstance();             // БД ключевых слов протокола
        private CommandLinker CL = CommandLinker.getInstance();         // Линковщик
        private CommandParcer CP = CommandParcer.getInstance();         // Парсер     

        //--- Конструктор класса (внутренний) -------------------------------------------------------------------------
        private CommandAnswer()
        {
        }

        //--- Конструктор класса (внешний) ----------------------------------------------------------------------------
        public static CommandAnswer getInstance()
        {
            if (instance == null)
                instance = new CommandAnswer();
            return instance;
        }

        //--- Ответ на команду запроса значения -----------------------------------------------------------------------
        private string get_answer(List<RealObject> objects_list, string msg, bool withApply, Command command)
        {
            string answ = " ";
            
            int index = -1;                                             // Поиск индекса объекта
            for (int i = 0; i < objects_list.Count; i++)
            {
                if (objects_list[i].key == command.key)
                {
                    index = i;
                    break;
                }
            }
            if (index < 0) answ += CL.linkError(0);                     // Если объект не зарегистрирован в системе
            else
            {
                RealObject obj = objects_list[index];

                List<string> params_keys = new List<string>();          // Формирование списка ключей
                if (command.data.Count != 0)                            // Запрос c параметрами
                {
                    foreach (string full_key in command.data)
                    {
                        List<string> temp = full_key.Split(dbKW.HierarchicalTag[0]).ToList();
                        if (temp[temp.Count - 2] == obj.key)
                            params_keys.Add(temp[temp.Count - 1]);
                        else
                            params_keys.Add(full_key);
                    }
                }
                answ += CL.linkAnswer(obj, withApply, params_keys);
            }

            answ = answ.Trim();

            return answ;
        }

        //--- Ответ на команду установки значения -----------------------------------------------------------------------
        private string set_answer(List<RealObject> objects_list, string msg, bool withApply, Command command)
        {
            string answ = " ";

            int index = -1;                                             // Поиск индекса объекта
            for (int i = 0; i < objects_list.Count; i++)
            {
                if (objects_list[i].key == command.key)
                {
                    index = i;
                    break;
                }
            }
            if (index < 0)                                              // Если объект не зарегистрирован в системе
                return CL.linkError(0);

            RealObject obj = objects_list[index];
            if (command.data.Count != 0)                                // Установка параметров
            {
                foreach (string full_data in command.data)
                {
                    List<string> key_value = full_data.Split(dbKW.AssignTag[0]).ToList();
                    if (key_value.Count != 2)                           // Если не задано значение
                        return CL.linkError(0);

                    List<string> temp = key_value[0].Split(dbKW.HierarchicalTag[0]).ToList();
                    string key = (temp[temp.Count - 1]);
                    int i = obj.IndexOf(key);
                    if (i < 0)                                          // Параметр с задаваемым индексом отсутствует
                        return CL.linkError(0);
                    obj.parameters[i].val = key_value[1];
                }
            }

            answ = answ.Trim();

            return answ;
        }

        //--- Сформировать ответ --------------------------------------------------------------------------------------
        public string answer(List<RealObject> objects_list, string msg, bool withApply)
        {
            string answ = " ";

            Command command = CP.parce(msg);
            if (command.withApply)                                      // Формирование подтверждения приема
                answ += CL.linkApply(command.key) + dbKW.EndOfLineTag;

            if (command.type == dbKW.GetTypeTag)                        // Поступила команда запроса значения
            {
                answ += get_answer(objects_list, msg, withApply, command);
            }
            if (command.type == dbKW.SetTypeTag)                        // Поступила команда установки значения
            {
                answ += set_answer(objects_list, msg, withApply, command);
            }

            if(answ[answ.Length-1] == dbKW.EndOfLineTag[0])             // Удаление символа конца строки
                answ = answ.Remove(answ.Length-1);

            answ = answ.Trim();

            return answ;
        }
    }
}
