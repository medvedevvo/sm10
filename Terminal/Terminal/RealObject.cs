using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Terminal
{
    /***** Параметр объекта ******************************************************************************************/
    public class RealObjectParameter
    {
        public string name = "";                                        // Имя параметра
        public string key = "";                                         // Ключ параметра
        public string type = "";                                        // Тип параметра
       
        //--- Конструктор класса --------------------------------------------------------------------------------------
        public RealObjectParameter(string name, string key, string type)
        {
            this.name = name;
            this.key = key;
            this.type = type;
        }
    }

    /***** Реальный объект *******************************************************************************************/
    public class RealObject
    {
        public string name = "";                                        // Имя параметра
        public string key = "";                                         // Ключ параметра
        public List<RealObjectParameter> paramenters;                   // Список параметров

        //--- Конструктор класса --------------------------------------------------------------------------------------
        public RealObject(string name, string key)
        {
            this.name = name;
            this.key = key;
            this.paramenters = new List<RealObjectParameter>();
        }

        //--- Добавление параметра ------------------------------------------------------------------------------------
        public void AddParameter(string name, string key, string type)
        {
            paramenters.Add(new RealObjectParameter(name, key, type));
        }
        public void AddParameter(RealObjectParameter parameter)
        {
            paramenters.Add(parameter);
        }
    }
}
