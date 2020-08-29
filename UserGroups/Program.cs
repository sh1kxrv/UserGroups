using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserGroups.Code;

namespace UserGroups
{
    class Program
    {
        static void Main(string[] args)
        {
            Controller ctrl = new Controller();
            ctrl.InitializeApplication();
            ctrl.ExecuteApplication();
            Console.Read();
        }
    }
}
