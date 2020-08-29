using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserGroups.Code.IO;

namespace UserGroups.Code.Functions.Application
{
    public class CompanyMoney : API.Function
    {
        public CompanyMoney(Controller ctrl) : base(ctrl) { }
        public override LevelAccess CmdLvl => LevelAccess.Application;

        public override string Command => "cmoney";

        public override string Description => "Возвращает бюджет компании";

        public override string Syntax => "";

        public override void Execute(ArgsController arg)
        {
            NotifyIO.PrintLn($"Текущий бюджет компании - {Controller.OwnCompany.AmountOfMoney}руб.");
        }
    }
}
