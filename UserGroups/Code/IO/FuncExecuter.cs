using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UserGroups.Code.API;

namespace UserGroups.Code.IO
{
    /// <summary>
    /// Запуск функций
    /// </summary>
    public class FuncExecuter
    {
        public Controller Controller;
        public ArgsController ArgumentsController;
        public FuncExecuter(Controller ctrl)
        {
            Controller = ctrl;
            ArgumentsController = new ArgsController();
        }
        public void ExecuteFunction(string CMD)
        {
            if (CMD.StartsWith("/"))
            {
                try
                {
                    Function ThisFunc = GetFunctionByInput(CMD.Split(' ')[0].Remove(0, 1));
                    ArgumentsController.CommandDist(CMD);
                    Thread thr = new Thread(new ThreadStart(() =>
                    {
                        try
                        {
                            if (ThisFunc == null)
                                return;
                            ThisFunc.Execute(ArgumentsController);
                        }
                        catch { NotifyIO.ErrorLn("Проверьте правильность команды, либо сообщите об этом администратору."); }
                    }));
                    thr.Start();
                    thr.Join();
                }
                catch { NotifyIO.ErrorLn("Проверьте правильность команды, либо сообщите об этом администратору."); }
            }
            else
                NotifyIO.ErrorLn("Неизвестное выражение.");
        }
        private Function GetFunctionByInput(string input) => Controller.CurrentFunctions.Where(a => a.Command == input).FirstOrDefault();
    }
}
