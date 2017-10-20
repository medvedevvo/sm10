using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Terminal
{
    /***** Команда ***************************************************************************************************/
    public class Command
    {
        public string type;                                             // Тип
        public bool withApply;                                          // С/без подтверждения
        public string key;                                              // Идентификатор
        public List<string> data;                                       // Параметры

        //--- Конструктор класса --------------------------------------------------------------------------------------
        public Command(string type, bool withApply, string key, List<string> data)
        {
            this.type = type;
            this.withApply = withApply;
            this.key = key;
            this.data = data;
        }
    }

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
            string msg2 = CL.linkSet(objects_list[0], false);
            Command command = CP.parce(msg2);
            MessageBox.Show(CL.linkApply("ACCU"));
        }
    }
}
