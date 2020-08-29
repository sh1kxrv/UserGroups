using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserGroups.Code.IO;

namespace UserGroups.Code.Functions.Application
{
    public class Clear : API.Function
    {
        public Clear(Controller ctrl) : base (ctrl) { }
        public override LevelAccess CmdLvl => LevelAccess.Application;

        public override string Command => "cls";

        public override string Syntax => "";

        public override string Description => "Очистка консоли";

        public override void Execute(ArgsController arg)
        {
            Console.Clear();
        }
    }
    public class WipeAll : API.Function
    {
        public WipeAll(Controller ctrl) : base(ctrl) { }
        public override LevelAccess CmdLvl => LevelAccess.Application;

        public override string Command => "wipe";

        public override string Syntax => "";

        public override string Description => "Очистка всех данных";

        public override void Execute(ArgsController arg)
        {
            Controller.Staff.Clear();
            Controller.OwnCompany.AmountOfMoney = 50_000_000;
            Controller.OwnCompany.Infos.Clear();
            Controller.OwnCompany.Products.Clear();
            Controller.OwnCompany.RefundedProducts.Clear();
            Controller.OwnCompany.SaledProducts.Clear();
            Controller.FileController.SaveCompany();
            Controller.FileController.SaveStaff();
        }
    }
}
