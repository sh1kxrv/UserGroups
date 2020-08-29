using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserGroups.Code.IO;

namespace UserGroups.Code.API
{
    public abstract class Function
    {
        public Controller Controller;
        public Function(Controller ctrl) { Controller = ctrl; }
        public abstract LevelAccess CmdLvl { get; }
        public abstract string Command { get; }
        public abstract string Syntax { get; }
        public abstract string Description { get; }
        public abstract void Execute(ArgsController arg);
    }
}
