using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserGroups.Code.IO
{
    public class ConsoleManipulator
    {
        private Controller Controller;
        public ConsoleManipulator(Controller ctrl)
        {
            Controller = ctrl;
        }
        public LevelAccess Authentication()
        {
            try
            {
                NotifyIO.PrintLn("Возможные учётные записи:");
                for (int i = 0; i < Controller.UserTypes.Count; i++)
                    NotifyIO.PrintLn($"{i + 1}.{Controller.UserTypes[i].Name}");
                return LevelParser.GetAccess(int.Parse(GetInputByUser()));
            }
            catch { NotifyIO.ErrorLn("Неизвестный пользователь."); return LevelAccess.Unknown; }
        }
        public void InfinityRequesting()
        {
            while (true)
            {
                Controller.Executer.ExecuteFunction(GetInputByUser());
            }
        }
        public void UpdateTitleStatus()
        {
            if(Controller.AuthedAs == null)
                Console.Title = "Не авторизован | User Groups Manager v3.2";
            else
                Console.Title = $"Авторизован как '{Controller.AuthedAs.Name}' | User Groups Manager v3.2";
        }
        public string GetInputByUser()
        {
            NotifyIO.Print("> ");
            return Console.ReadLine();
        }
    }
}
