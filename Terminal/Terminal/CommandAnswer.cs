using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Terminal
{
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

        //--- Сформировать ответ --------------------------------------------------------------------------------------
        public string answer(List<RealObject> objects_list, string msg, bool withApply)
        {
            string answ = " ";

            Command command = CP.parce(msg);
            if (command.withApply)                                      // Формирование подтверждения приема
                answ += CL.linkApply(command.key) + dbKW.EndOfLineTag;

            if (command.type == dbKW.GetTypeTag)                        // Поступила команда запроса значения
            {
                int index = -1;                                         // Поиск индекса объекта
                for(int i = 0; i < objects_list.Count; i++)
                {
                    if(objects_list[i].key == command.key)
                    {
                        index = i;
                        break;
                    }
                }
                if (index < 0) answ += CL.linkError(0);                 // Если объект не зарегистрирован в системе
                else
                {
                    RealObject obj = objects_list[index];

                    List<string> params_keys = new List<string>();      // Формирование списка ключей
                    if(command.data.Count != 0)                         // Запрос c параметрами
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
            }

            if(answ[answ.Length-1] == dbKW.EndOfLineTag[0])             // Удаление символа конца строки
                answ = answ.Remove(answ.Length-1);

            answ = answ.Trim();

            return answ;
        }
    }
}
