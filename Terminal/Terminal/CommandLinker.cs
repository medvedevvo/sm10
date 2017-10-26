﻿using System;
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

        //--- Получение ключа параметра -------------------------------------------------------------------------------
        private string MakeParamKey(RealObject obj, string name)
        {
            string key = obj.key + dbKW.HierarchicalTag;

            foreach (RealObjectParameter rop in obj.parameters)
            {
                if (rop.name == name)
                {
                    key += rop.key;
                    return key;
                }  
            }

            return "";
        }

        //--- Основа команды запроса ----------------------------------------------------------------------------------
        private string linkGetBase(RealObject obj, bool withApply)
        {
            string command = dbKW.StartTag + dbKW.GetTypeTag;
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
        public string linkGet(RealObject obj, List<string> params_names, bool withApply)
        {
            string command = linkGetBase(obj, withApply);
            command += dbKW.CountTag + params_names.Count.ToString();
            command += dbKW.ParamBeginTag;

            foreach (string name in params_names)
            {
                string temp = MakeParamKey(obj, name);
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
        public string linkApply(string object_name)
        {
            string command = dbKW.StartTag + dbKW.ApplyTypeTag +
                             dbKW.SpaceTag + object_name + dbKW.ApplyFinishTag;


            return command;
        }

        public string linkSet(RealObject obj, bool withApply)
        {
            string command = dbKW.StartTag + dbKW.SetTypeTag;
            if (withApply) command += dbKW.AnswerTag;
            command += dbKW.SpaceTag + obj.key;

            command += dbKW.CountTag + obj.parameters.Count.ToString();
            command += dbKW.ParamBeginTag;

            foreach (RealObjectParameter param in obj.parameters)
            {
                command += obj.key + dbKW.HierarchicalTag + param.key + dbKW.AssignTag + param.val + dbKW.SeparatorTag;
            }
            command = command.Remove(command.Length - 1);

            command += dbKW.ParamEndTag + dbKW.SetFinishTag;

            return command;
        }
    }
}
