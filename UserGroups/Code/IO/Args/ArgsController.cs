using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace UserGroups.Code.IO
{
    //Контроллер аргументов
    //Основной центр контроля аргов
    public partial class ArgsController
    {
        public string ROWArguments;
        public NumberArgs NumbArgs; //Отвечает за числа  - %d%
        public StringArgs StrArgs;  //Отвечает за строки - "str"
        public ArgsController()
        {
            NumbArgs = new NumberArgs(this);
            StrArgs = new StringArgs(this);
        }
        public void CommandDist(string row)
        {
            ROWArguments = row.Trim();
            StrArgs.Handle();
            NumbArgs.Handle();
        }
    }
}
