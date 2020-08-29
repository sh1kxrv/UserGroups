using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserGroups.Code.IO;

namespace UserGroups.Code.Functions.Application
{
    public class StaffList : API.Function
    {
        public StaffList(Controller ctrl) : base(ctrl) { }
        public override LevelAccess CmdLvl => LevelAccess.Application;

        public override string Command => "staff";

        public override string Syntax => "";

        public override string Description => "Список сотрудников";

        public override void Execute(ArgsController arg)
        {
            if (Controller.Staff.Count == 0)
            {
                NotifyIO.ErrorLn("Сотрудники не найдены.");
                return;
            }
            for(int i = 0; i < Controller.Staff.Count; i++)
                NotifyIO.PrintLn($"\t№{i + 1}.{Controller.Staff[i].Name} | Должность: {Controller.Staff[i].Position} | Оклад: {Controller.Staff[i].Salary}руб. | ЗП на руки : {Controller.Staff[i].ToPaid}руб. | Выплачено всего: {Controller.Staff[i].TotalyPaid}руб.");
        }
    }
}
