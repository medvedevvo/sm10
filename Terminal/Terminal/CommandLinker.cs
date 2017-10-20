using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Terminal
{
    public class CommandLinker
    {
        private DBKeyWords dbKW = DBKeyWords.getInstance();

        public string linkGet(RealObject obj, bool withApply)
        {
            string command = dbKW.StartTag + dbKW.GetTypeTag;
            if (withApply) command += dbKW.AnswerTag;
            command += dbKW.SpaceTag + obj.key + dbKW.GetFinishTag;

            return command;
        }
    }
}
