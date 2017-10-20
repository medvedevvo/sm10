using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terminal
{
    /***** Ключевые слова в C# (одиночка) ****************************************************************************/
    public class DBKeyWords
    {
        private static DBKeyWords instance;                             // Ссылка на текущий объект

        public string StartTag = "#";
        public string EndOfLineTag = "/n";
        public string SpaceTag = "_";

        public string GetTypeTag = "G";
        public string ApplyTypeTag = "A";
        public string SetTypeTag = "S";
        public string MessageTypeTag = "M";
        public string AnswerTag = "A";
        public string CountTag = "@";
        public string ParamBeginTag = "(";
        public string ParamEndTag = ")";
        public string SeparatorTag = ";";

        public string GetFinishTag = "?";
        public string ApplyFinishTag = "!";
        public string SetFinishTag = "!";
        public string MessageFinishTag = "!";


        //--- Конструктор класса (внутренний) -------------------------------------------------------------------------
        private DBKeyWords()
        {
        }

        //--- Конструктор класса (внешний) ----------------------------------------------------------------------------
        public static DBKeyWords getInstance()
        {
            if (instance == null)
                instance = new DBKeyWords();
            return instance;
        }
    }
}
