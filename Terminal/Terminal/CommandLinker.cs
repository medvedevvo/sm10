using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Terminal
{
    public class CommandLinker
    {
        private DBKeyWords dbKW = DBKeyWords.getInstance();

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

        private string linkGetBase(RealObject obj, bool withApply)
        {
            string command = dbKW.StartTag + dbKW.GetTypeTag;
            if (withApply) command += dbKW.AnswerTag;
            command += dbKW.SpaceTag + obj.key;

            return command;
        }

        public string linkGet(RealObject obj, bool withApply)
        {
            string command = linkGetBase(obj, withApply);
            command += dbKW.GetFinishTag;

            return command;
        }
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

        public string linkError(int code)
        {
            List<int> codes = new List<int>();
            codes.Add(code);

            return linkError(codes);
        }
    }
}
