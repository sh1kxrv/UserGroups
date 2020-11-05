using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserGroups.Code.API;
using UserGroups.Code.Company;
using UserGroups.Code.IO;
using UserGroups.Code.User.Custom;

namespace UserGroups.Code.Functions.Accountant
{
    public class Payroll : Function
    {
        public Payroll(Controller ctrl) : base (ctrl) { }
        public override LevelAccess CmdLvl => LevelAccess.Accountant;

        public override string Command => "payroll";

        public override string Syntax => "\"ФИО\"";

        public override string Description => "Выплата зарплаты";

        public override void Execute(ArgsController arg)
        {
            Employee emp = Controller.GetUserByName(arg.StrArgs[0]);
            int staffIndex = Controller.GetStaffIndex(emp);
            double RK = emp.Salary * (double)(15.0 / 100.0);
            NotifyIO.PrintLn($"РК -> {RK}");
            NotifyIO.PrintLn($"Оклад -> {emp.Salary}");
            double result = RK + emp.Salary;
            double NDFL = result - ReceiveOfSalary(13.0, result);
            double PFR = result - ReceiveOfSalary(22.0, result);
            double FFOMC = result - ReceiveOfSalary(5.1, result);
            double FSS = result - ReceiveOfSalary(2.1, result);
            double USNO = result - ReceiveOfSalary(6.0, result);
            double NSL = result - ReceiveOfSalary(0.2, result);
            NotifyIO.PrintLn($"Налоги:\nНДФЛ: {NDFL}руб.\nПФР: {PFR}руб.\nФФОМС: {FFOMC}руб.\nФСС: {FSS}руб.\nУСНО: {USNO}руб.\nН/СЛ: {NSL}руб.");
            Controller.OwnCompany.AmountOfMoney -= result - (PFR + FFOMC + FSS + USNO + NSL);
            emp.ToPaid = result - NDFL;
            emp.TotalyPaid += emp.ToPaid;
            Controller.Staff[staffIndex] = emp;
            Controller.FileController.SaveStaff();
            Controller.FileController.SaveCompany();
            NotifyIO.PrintLn($"Итоговая зарплата к выдачи {emp.ToPaid}руб.\nВсего было выплачено - {emp.TotalyPaid}");
        }
        private double ReceiveOfSalary(double percent, double salary)
        {
            return salary - ((salary * percent) / 100);
        }
    }
    public class Payment : Function
    {
        public Payment(Controller ctrl) : base(ctrl) { }
        public override LevelAccess CmdLvl => LevelAccess.Accountant;

        public override string Command => "payment";

        public override string Syntax => "";

        public override string Description => "Расчёт затрат компании";

        public override void Execute(ArgsController arg)
        {
            double refund = 0;
            double saled = 0;
            double saledWithNDS = 0;
            foreach (var toRef in Controller.OwnCompany.RefundedProducts)
            {
                if (toRef.Refunded)
                    continue;
                toRef.Refunded = true;
                refund += toRef.Price;
            }
            foreach (var sal in Controller.OwnCompany.SaledProducts)
            {
                if (sal.Saled)
                    continue;
                sal.Saled = true;
                saledWithNDS += sal.Price - sal.Price * ((sal.Type == ProductType.DefaultProduct ? 20.0 : 10.0) / 100);
                saled += sal.Price;
            }
            Controller.OwnCompany.AmountOfMoney -= refund;
            Controller.OwnCompany.AmountOfMoney += saledWithNDS;
            Controller.FileController.SaveCompany();
            NotifyIO.PrintLn($"Сумма с учётом НДС - {saledWithNDS}\nСумма без учёта НДС - {saled}\nБюджет: {Controller.OwnCompany.AmountOfMoney}");
        }
    }
}
