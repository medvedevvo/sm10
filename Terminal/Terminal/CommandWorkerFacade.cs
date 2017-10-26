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
        private CommandLinker CL = CommandLinker.getInstance();
        private CommandParcer CP = CommandParcer.getInstance();
        private DBObjects dbObj = DBObjects.getInstance();
        private CommandAnswer CA = CommandAnswer.getInstance();

        //--- Конструктор класса --------------------------------------------------------------------------------------
        public CommandWorkerFacade()
        {
            objects_list = dbObj.objects_list;

            List<string> parameters = new List<string>();
            parameters.Add(objects_list[0].parameters[0].key);
            //parameters.Add(objects_list[0].parameters[1].key);
            string msg = //CL.linkGet(objects_list[0], false);    
                         CL.linkGet(objects_list[0], parameters, false);
                         //CL.linkSet(objects_list[0], true);
            string answ = CA.answer(objects_list, msg, false);
            //Command command = CP.parce(msg);
            //MessageBox.Show(CL.linkApply("ACCU"));
            int a = 0;
        }

        public string answer(string msg, bool withApply)
        {
            return CA.answer(objects_list, msg, withApply);
        }
    }
}
