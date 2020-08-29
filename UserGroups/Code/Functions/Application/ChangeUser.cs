using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using UserGroups.Code.IO;

namespace UserGroups.Code.Functions.Application
{
    public class ChangeUser : API.Function
    {
        public ChangeUser(Controller ctrl) : base(ctrl) { }
        public override LevelAccess CmdLvl => LevelAccess.Application;

        public override string Command => "cuser";
        public override string Description => "Смена пользователя";

        public override string Syntax => "";

        public override void Execute(ArgsController arg)
        {
            Controller.Auth(Controller.Manipulator.Authentication());
            Controller.InitCommand();
        }
    }
}
