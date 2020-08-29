using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserGroups.Code.API;
using UserGroups.Code.Company;
using UserGroups.Code.IO;

namespace UserGroups.Code.Functions.Paymaster
{
    public class Sale : Function
    {
        public Sale(Controller ctrl) : base(ctrl) {}
        public override LevelAccess CmdLvl => LevelAccess.Paymaster;

        public override string Command => "sale";

        public override string Syntax => "\"Имя товара\" [Кол-во]";

        public override string Description => "Продажа товара";

        public override void Execute(ArgsController arg)
        {
            string pname = arg.StrArgs[0];
            int count = (int)arg.NumbArgs[0];
            Product prByName = Controller.OwnCompany.GetProductByName(pname);
            if(prByName != null)
            {
                if (!(prByName.Count >= count))
                    NotifyIO.ErrorLn("Недостаточное кол-во товара на складе.");
                else
                {
                    prByName.Count -= count;
                    prByName.AllChanges.Add( new Changes($"Продано {count}шт на сумму {count * prByName.Price}руб."));
                    Controller.OwnCompany.Products[prByName.Index - 1] = prByName;
                    Controller.OwnCompany.SaledProducts.Add(new SaledProduct(count * prByName.Price, count, pname, prByName.Type));
                    NotifyIO.PrintLn($"Товар '{prByName.Name}' успешно продан в размере '{count}шт', остаток '{prByName.Count}'");
                }
            }
            Controller.FileController.SaveCompany();
        }
    }
    public class Refund : Function
    {
        public Refund(Controller ctrl) : base(ctrl) { }
        public override LevelAccess CmdLvl => LevelAccess.Paymaster;

        public override string Command => "refund";

        public override string Syntax => "\"Имя возв. товара\" [Кол-во]";

        public override string Description => "Возврат товара";

        public override void Execute(ArgsController arg)
        {
            string pname = arg.StrArgs[0];
            int count = (int)arg.NumbArgs[0];
            Product prByName = Controller.OwnCompany.GetProductByName(pname);
            if(prByName != null)
            {
                //Псевдо-защита от возврата несуществующего товара, возможно изменить на более сложную с штрих-кодами.
                if (!(Controller.OwnCompany.SaledProducts.Where(x => x.Name == pname).FirstOrDefault().Count >= count))
                {
                    NotifyIO.ErrorLn("Товар в таком количестве не был продан, и не может быть возвращён.");
                    return;
                }
                prByName.Count += count;
                prByName.AllChanges.Add(new Changes($"Возврат {count}шт на сумму {count * prByName.Price}"));
                Controller.OwnCompany.Products[prByName.Index - 1] = prByName;
                Controller.OwnCompany.RefundedProducts.Add(new RefundedProduct(pname, count * prByName.Price, count, prByName.Type));
                NotifyIO.PrintLn($"Товар '{prByName.Name}' успешно возвращён в размере '{count}шт', остаток '{prByName.Count}'");
            }
            Controller.FileController.SaveCompany();
        }
    }
}
