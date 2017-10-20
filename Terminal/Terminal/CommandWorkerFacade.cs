using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Terminal
{
    //*** Обработчик команд в рамках протокола передачи данных ********************************************************
    public class CommandWorkerFacade
    {
        public List<RealObject> objects_list;                           // Список объектов

        //--- Конструктор класса --------------------------------------------------------------------------------------
        public CommandWorkerFacade()
        {
            objects_list = new List<RealObject>();
            RealObject temp_obj = new RealObject("Аккумулятор", "ACCU");
            temp_obj.AddParameter(new RealObjectParameter("Напряжение", "V", "R"));
            temp_obj.AddParameter(new RealObjectParameter("Ток нагрузки", "A", "R"));
            temp_obj.AddParameter(new RealObjectParameter("Остаток заряда", "CHARGE", "R"));
            objects_list.Add(temp_obj);
        }
    }
}
