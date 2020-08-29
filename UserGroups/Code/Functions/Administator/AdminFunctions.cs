using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserGroups.Code.API;
using UserGroups.Code.IO;
using UserGroups.Code.User.Custom;

namespace UserGroups.Code.Functions.Administator
{
    public class DeleteUser : Function
    {
        public DeleteUser(Controller ctrl):base(ctrl) { }
        public override LevelAccess CmdLvl => LevelAccess.Admin;

        public override string Command => "duser";

        public override string Syntax => "\"Полное имя сотрудника\"";

        public override string Description => "Увольнение сотрудника";

        public override void Execute(ArgsController arg)
        {
            string fullname = arg.StrArgs[0];
            Employee emp = Controller.GetUserByName(fullname);
            Controller.Staff.Remove(emp);
            NotifyIO.PrintLn($"Сотрудник {emp.Name} был успешно уволен.");
            Controller.FileController.SaveStaff();
        }
    }
    public class AddUser : Function
    {
        public AddUser(Controller ctrl):base(ctrl) { }
        public override LevelAccess CmdLvl => LevelAccess.Admin;

        public override string Command => "auser";

        public override string Description => "Добавление сотрудника";

        public override string Syntax => "\"Полное имя сотрудника\" \"Должность\" [Зарплата(руб)]";

        public override void Execute(ArgsController arg)
        {
            string fullname = arg.StrArgs[0];
            string job = arg.StrArgs[1];
            int salary = (int)arg.NumbArgs[0];
            Employee emp = new Employee(fullname, job, salary);
            Controller.Staff.Add(emp);
            NotifyIO.PrintLn($"Сотрудник '{emp.Name}' был успешно добавлен.");
            Controller.FileController.SaveStaff();
        }
    }
}
