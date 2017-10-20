using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Terminal
{
    /***** Обработчик команд в рамках протокола передачи данных ******************************************************/
    public class CommandWorkerFacade
    {
        public List<RealObject> objects_list;                           // Список объектов
        private CommandLinker CL = new CommandLinker();
        private CommandParcer CP = new CommandParcer();

        //--- Конструктор класса --------------------------------------------------------------------------------------
        public CommandWorkerFacade()
        {
            objects_list = new List<RealObject>();
            RealObject temp_obj = new RealObject("Аккумулятор", "ACCU");
            temp_obj.AddParameter(new RealObjectParameter("Напряжение", "V", "R", "12.1"));
            temp_obj.AddParameter(new RealObjectParameter("Ток нагрузки", "A", "R", "3.14"));
            temp_obj.AddParameter(new RealObjectParameter("Остаток заряда", "CHARGE", "R", "67"));
            objects_list.Add(temp_obj);

            List<string> parameters = new List<string>();
            parameters.Add(objects_list[0].parameters[0].name);
            parameters.Add(objects_list[0].parameters[1].name);
            string msg = //CL.linkGet(objects_list[0], true);    
                         CL.linkGet(objects_list[0], parameters, true);
            Command command = CP.parce("#M_ACCU@2(ACCU:V=12.1;ACCU:A=3.14)!");
            MessageBox.Show(msg);
        }
    }
}
