using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserGroups.Code.IO
{
    //Шаблон для новых аргов
    public abstract class ArgsTemplate
    {
        public ArgsController CONTROLLER;
        public ArgsTemplate(ArgsController controller)
        {
            CONTROLLER = controller;
        }
        public abstract void Handle();
    }
}
