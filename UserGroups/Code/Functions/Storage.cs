using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserGroups.Code.Company;
using UserGroups.Code.IO;

namespace UserGroups.Code.Functions
{
    public class AddProduct : API.Function
    {
        public AddProduct(Controller ctrl) : base(ctrl) { }
        public override LevelAccess CmdLvl => LevelAccess.Storage;

        public override string Command => "aproduct";

        public override string Syntax => "\"Имя товара\" [Стоимость] [Кол-во] \"Тип товара(Товар/Продукт)\"";

        public override string Description => "Добавление товара";

        public override void Execute(ArgsController arg)
        {
            string prodName = arg.StrArgs[0];
            string type = arg.StrArgs[1];
            double price = arg.NumbArgs[0];
            int count = (int)arg.NumbArgs[1];
            Controller.OwnCompany.AddProduct(prodName, price, count, GetType(type));
            Controller.FileController.SaveCompany();
            NotifyIO.PrintLn("Товар добавлен!");
        }
        private ProductType GetType(string type)
        {
            switch (type)
            {
                case "Товар":
                    return ProductType.DefaultProduct;
                case "Продукт":
                    return ProductType.EatableProduct;
                default:
                    throw new Exception("Неизвестный тип товара.");
            }
        }
    }
    public class WriteOffProduct : API.Function
    {
        public WriteOffProduct(Controller ctrl) : base(ctrl) { }
        public override LevelAccess CmdLvl => LevelAccess.Storage;

        public override string Command => "wprod";

        public override string Syntax => "\"Имя товара\" \"Причина\" [Кол-во]";

        public override string Description => "Списание товара";

        public override void Execute(ArgsController arg)
        {
            string prodName = arg.StrArgs[0];
            string reason = arg.StrArgs[1];
            int count = (int)arg.NumbArgs[0];
            Controller.OwnCompany.WriteOff(prodName, count, reason);
            Controller.FileController.SaveCompany();
            NotifyIO.PrintLn("Продукт списан.");
        }
    }
    public class ProductsList : API.Function
    {
        public ProductsList(Controller ctrl) : base(ctrl) { }
        public override LevelAccess CmdLvl => LevelAccess.Application;

        public override string Command => "products";

        public override string Syntax => "";

        public override string Description => "Список всех товаров";

        public override void Execute(ArgsController arg)
        {
            if(Controller.OwnCompany.Products.Count == 0)
            {
                NotifyIO.ErrorLn("Склад пуст.");
                return;
            }
            int summ = 0;
            foreach (var prod in Controller.OwnCompany.Products)
            {
                summ += prod.Count;
                NotifyIO.PrintLn($" \t№{prod.Index}.{prod.Name} | {prod.Count}шт. | {(double)prod.Price}руб. за шт. | добавлен {prod.AddingTime.ToLongDateString()} {prod.AddingTime.ToLongTimeString()}");
                foreach (var change in prod.AllChanges)
                    NotifyIO.PrintLn($"\t-> {change.Change} | {prod.AddingTime.ToLongDateString()} {change.ChangeTime.ToLongTimeString()}");
            }
            NotifyIO.PrintLn($"Суммарно -> {summ}шт.");
        }
    }
}
