using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using UserGroups.Code.IO;

namespace UserGroups.Code.Functions.Application
{
    public class Help : API.Function
    {
        public Help(Controller ctrl) : base(ctrl) { }
        public override LevelAccess CmdLvl => LevelAccess.Application;

        public override string Command => "help";

        public override string Description => "";

        public override string Syntax => "";

        public override void Execute(ArgsController arg)
        {
            NotifyIO.PrintLn("Команды:");
            LevelAccess access = Controller.AuthedAs.Access;
            NotifyIO.PrintLn(CommandNotifier(access));

            foreach (var func in Controller.CurrentFunctions)
            {
                if(access != func.CmdLvl)
                    NotifyIO.PrintLn(CommandNotifier(func.CmdLvl));
                NotifyIO.PrintLn($"\t/{func.Command} -> {(string.IsNullOrEmpty(func.Syntax) ? "[не требует аргументов]" : func.Syntax)} |" +
                    $" {(string.IsNullOrEmpty(func.Description) ? "[не имеет описания]" : func.Description)}");
                access = func.CmdLvl;
            }
        }
        private string CommandNotifier(LevelAccess lvl)
        {
            switch (lvl)
            {
                case LevelAccess.Admin:
                    return "Функции администратора:";
                case LevelAccess.HRD:
                    return "Функции отдела кадров:";
                case LevelAccess.Accountant:
                    return "Функции бухгалтера:";
                case LevelAccess.Paymaster:
                    return "Функции кассира:";
                case LevelAccess.Storage:
                    return "Функции склада:";
                case LevelAccess.Application:
                    return "Системные функции:";
                default:
                    return "Прочее:";
            }
        }
    }
}
