using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserGroups.Code.IO;
using UserGroups.Code.User.Custom;

namespace UserGroups.Code.Functions.HRD
{
    public class RedactIt : API.Function
    {
        public RedactIt(Controller ctrl) : base(ctrl) { }
        public override LevelAccess CmdLvl => LevelAccess.HRD;

        public override string Command => "redact";

        public override string Syntax => "\"ФИО\" \"Поле(ЗП, ДОЛЖ, ФИО)\" \"Значение\"";

        public override string Description => "Редактирование информации сотрудников";

        public override void Execute(ArgsController arg)
        {
            string fullname = arg.StrArgs[0];
            string field = arg.StrArgs[1];
            string value = arg.StrArgs[2];
            Employee emp = Controller.GetUserByName(fullname);
            int empIndex = Controller.GetStaffIndex(emp);
            switch (field)
            {
                case "ЗП":
                    int salary = int.Parse(value);
                    if (salary >= 0)
                        emp.Salary = salary;
                    else
                    {
                        NotifyIO.ErrorLn("Неправильное значение.");
                        return;
                    }
                    break;
                case "ДОЛЖ":
                    emp.Position = value;
                    break;
                case "ФИО":
                    emp.Name = value;
                    break;
            }
            Controller.Staff[empIndex] = emp;
            Controller.FileController.SaveStaff();
            NotifyIO.PrintLn($"Сотрудник успешно отредактирован!");
        }
    }
    public class GetInfo : API.Function
    {
        public GetInfo(Controller ctrl) : base(ctrl) { }
        public override LevelAccess CmdLvl => LevelAccess.HRD;

        public override string Command => "ginfo";

        public override string Syntax => "\"ФИО\"";

        public override string Description => "Получение информации о сотрудниках";

        public override void Execute(ArgsController arg)
        {
            string FN = arg.StrArgs[0];
            Company.InformationOfStaff[] Infos = Controller.OwnCompany.GetInformationsByName(FN);
            foreach (var info in Infos)
                NotifyIO.PrintLn($"№{info.Index}.{info.EMP.Name} | {info.Event} | {info.AddingTime.ToLongDateString()}");
        }
    }
    public class SetInfo : API.Function
    {
        public SetInfo(Controller ctrl) : base(ctrl) { }
        public override LevelAccess CmdLvl => LevelAccess.HRD;

        public override string Command => "sinfo";

        public override string Syntax => "\"ФИО\" \"Событие\"";

        public override string Description => "Редактирование событий сотрудников";

        public override void Execute(ArgsController arg)
        {
            string FN = arg.StrArgs[0];
            string Event = arg.StrArgs[1];
            Employee emp = Controller.GetUserByName(FN);
            if(emp == null)
            {
                NotifyIO.ErrorLn("Сотрудник не найден!");
                return;
            }
            Controller.OwnCompany.AddInfo(Controller.GetUserByName(FN), Event);
            Controller.FileController.SaveCompany();
            NotifyIO.PrintLn("Событие успешно записано.");
        }
    }
    public class DeleteInfo : API.Function
    {
        public DeleteInfo(Controller ctrl) : base(ctrl) { }
        public override LevelAccess CmdLvl => LevelAccess.HRD;

        public override string Command => "dinfo";

        public override string Syntax => "[Номер события]";

        public override string Description => "Удаление событий сотрудников";

        public override void Execute(ArgsController arg)
        {
            int index = (int)arg.NumbArgs[0];
            Controller.OwnCompany.DeleteInfo(index);
            Controller.FileController.SaveCompany();
            NotifyIO.PrintLn("Событие успешно удалено.");
        }
    }
    public class ListInfo : API.Function
    {
        public ListInfo(Controller ctrl) : base(ctrl) { }
        public override LevelAccess CmdLvl => LevelAccess.HRD;

        public override string Command => "listinfo";

        public override string Syntax => "";

        public override string Description => "Список информации о всех сотрудниках";

        public override void Execute(ArgsController arg)
        {
            if(Controller.OwnCompany.Infos.Count == 0)
            {
                NotifyIO.ErrorLn("События не записаны.");
                return;
            }
            foreach (var info in Controller.OwnCompany.Infos)
                NotifyIO.PrintLn($"№{info.Index}.{info.EMP.Name} | {info.Event} | {info.AddingTime.ToLongTimeString()} {info.AddingTime.ToLongDateString()}");
        }
    }
}
