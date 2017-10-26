using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Terminal
{
    /***** Линковщик команд ******************************************************************************************/
    public class CommandLinker
    {
        private static CommandLinker instance;                          // Ссылка на текущий объект
        private DBKeyWords dbKW = DBKeyWords.getInstance();             // БД ключевых слов протокола
        private DBObjects dbObj = DBObjects.getInstance();              // БД объектов системы

        //--- Конструктор класса (внутренний) -------------------------------------------------------------------------
        private CommandLinker()
        {
        }

        //--- Конструктор класса (внешний) ----------------------------------------------------------------------------
        public static CommandLinker getInstance()
        {
            if (instance == null)
                instance = new CommandLinker();
            return instance;
        } 

        //--- Основа команды запроса ----------------------------------------------------------------------------------
        private string linkGetBase(RealObject obj, bool withApply)
        {
            string command = dbKW.StartTag + dbKW.GetTypeTag;
            if (withApply) command += dbKW.AnswerTag;
            command += dbKW.SpaceTag + obj.key;

            return command;
        }

        //--- Основа команды ответа ----------------------------------------------------------------------------------
        private string linkAnswerBase(RealObject obj, bool withApply)
        {
            string command = dbKW.StartTag + dbKW.MessageTypeTag;
            if (withApply) command += dbKW.AnswerTag;
            command += dbKW.SpaceTag + obj.key;

            return command;
        }

        //--- Команда запроса всех параметров объекта -----------------------------------------------------------------
        public string linkGet(RealObject obj, bool withApply)
        {
            string command = linkGetBase(obj, withApply);
            command += dbKW.GetFinishTag;

            return command;
        }

        //--- Команда запроса выбранных параметров объекта ------------------------------------------------------------
        public string linkGet(RealObject obj, List<string> params_keys, bool withApply)
        {
            if (params_keys.Count == 0)                                 // Если список параметров пуст
                return linkGet(obj, withApply);
               
            string command = linkGetBase(obj, withApply);
            command += dbKW.CountTag + params_keys.Count.ToString();
            command += dbKW.ParamBeginTag;

            foreach (string key in params_keys)
            {
                string temp = obj.MakeParamKey(key);
                if (temp == "") return linkError(0);
                command += temp + dbKW.SeparatorTag;
            }
            command = command.Remove(command.Length - 1);

            command += dbKW.ParamEndTag + dbKW.GetFinishTag;

            return command;
        }

        //--- Ответ со списком ошибок ---------------------------------------------------------------------------------
        public string linkError(List<int> codes)
        {
            string command = dbKW.StartTag + dbKW.MessageTypeTag +
                             dbKW.SpaceTag + dbKW.ErrorTag + dbKW.CountTag;
            command += codes.Count.ToString();
            command += dbKW.ParamBeginTag;
            foreach (int code in codes)
            {
                command += code.ToString() + dbKW.SeparatorTag;
            }

            command = command.Remove(command.Length - 1);
            command += dbKW.ParamEndTag + dbKW.MessageFinishTag;

            return command;
        }

        //--- Ответ с кодом одной ошибки ------------------------------------------------------------------------------
        public string linkError(int code)
        {
            List<int> codes = new List<int>();
            codes.Add(code);

            return linkError(codes);
        }

        //--- Подтверждение приема ------------------------------------------------------------------------------------
        public string linkApply(string object_key)
        {
            string command = dbKW.StartTag + dbKW.ApplyTypeTag +
                             dbKW.SpaceTag + object_key + dbKW.ApplyFinishTag;


            return command;
        }

        //--- Формирование списка значений с параметрами --------------------------------------------------------------
        public string linkParamKeyValueListBase(RealObject obj)
        {
            string command = dbKW.CountTag + (obj.parameters.Count+1).ToString();
            command += dbKW.ParamBeginTag;
            command += dbObj.objects_list[1].MakeParamKey(dbObj.objects_list[1].parameters[0].key) + dbKW.AssignTag + 
                       dbObj.objects_list[1].parameters[0].val + dbKW.SeparatorTag;

            foreach (RealObjectParameter param in obj.parameters)
            {
                command += obj.key + dbKW.HierarchicalTag + param.key + dbKW.AssignTag + param.val + dbKW.SeparatorTag;
            }
            command = command.Remove(command.Length - 1);

            command += dbKW.ParamEndTag + dbKW.SetFinishTag;

            return command;
        }
        public string linkParamKeyValueListBase(RealObject obj, List<string> params_keys)
        {
            if (params_keys.Count == 0)                                 // Если список параметров пуст
                return linkParamKeyValueListBase(obj);

            string command = dbKW.CountTag + (params_keys.Count + 1).ToString();
            command += dbKW.ParamBeginTag;
            command += dbObj.objects_list[1].MakeParamKey(dbObj.objects_list[1].parameters[0].key) + dbKW.AssignTag +
                       dbObj.objects_list[1].parameters[0].val + dbKW.SeparatorTag;

            foreach (string param_key in params_keys)
            {
                foreach (RealObjectParameter param in obj.parameters)
                {
                    if (param_key == param.key)
                        command += param.val + dbKW.SeparatorTag;
                }
            }
            
            command = command.Remove(command.Length - 1);

            command += dbKW.ParamEndTag + dbKW.SetFinishTag;

            return command;
        }

        //--- Установка значения --------------------------------------------------------------------------------------
        public string linkSet(RealObject obj, bool withApply)
        {
            string command = dbKW.StartTag + dbKW.SetTypeTag;
            if (withApply) command += dbKW.AnswerTag;
            command += dbKW.SpaceTag + obj.key + linkParamKeyValueListBase(obj);

            return command;
        }

        //--- Команда ответа о параметрах объекта ---------------------------------------------------------------------
        public string linkAnswer(RealObject obj, bool withApply)
        {
            string command = linkAnswerBase(obj, withApply) + linkParamKeyValueListBase(obj);

            return command;
        }
        public string linkAnswer(RealObject obj, bool withApply, List<string> params_keys)
        {
            string command = linkAnswerBase(obj, withApply) + linkParamKeyValueListBase(obj, params_keys);

            return command;
        }
    }
}
