using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserGroups.Code.API;
using UserGroups.Code.API.Any_Loader;
using UserGroups.Code.FileSystem;
using UserGroups.Code.IO;
using UserGroups.Code.User;
using UserGroups.Code.User.Custom;

namespace UserGroups.Code
{
    /// <summary>
    /// TODO:
    /// 1.Основные команды приложения.
    /// 2.Доделать зацикленную функцию выполнения команд
    /// 3.Создать смену пользователя.
    /// </summary>
    public class Controller
    {
        public ConsoleManipulator Manipulator { get; private set; }
        public FuncExecuter Executer { get; private set; }
        public FileController FileController { get; private set; }
        public Company.Company OwnCompany { get; set; }
        public List<IUser> UserTypes { get; set; }
        public List<Employee> Staff { get; set; }
        public List<Function> CurrentFunctions { get; set; }
        public IUser AuthedAs { get; set; }
        public Controller()
        {
            Manipulator = new ConsoleManipulator(this);
            Executer = new FuncExecuter(this);
            FileController = new FileController(this);
            OwnCompany = new Company.Company(this);
            CurrentFunctions = new List<Function>();
            Staff = new List<Employee>();
        }
        public void InitializeApplication()
        {
            //Загрузка всех типов пользователей.
            UserTypes = LoadIt.LoadSomething<IUser>(System.Reflection.Assembly.GetExecutingAssembly());
            //Создание системных директорий
            FileController.CreateDirectories();
            //Обновление статуса в заголовке программы
            Manipulator.UpdateTitleStatus();
            //Загрузка сотрудников
            FileController.LoadStaff();
            FileController.LoadCompany();
        }
        public void ExecuteApplication()
        {
            //Авторизация пользователя
            Auth(Manipulator.Authentication());
            //Загрузка всех команд
            InitCommand();
            Console.Clear();
            //Бесконечный запрос команд
            Manipulator.InfinityRequesting();
        }
        public void Auth(LevelAccess lvl)
        {
            AuthedAs = null;
            Manipulator.UpdateTitleStatus();
            AuthedAs = GetUserTypeByLvl(lvl);
            NotifyIO.PrintLn($"Вы авторизовались под пользователем '{AuthedAs.Name}'");
            Manipulator.UpdateTitleStatus();
        }
        public void InitCommand()
        {
            CurrentFunctions = GetFunctionsByLvl(AuthedAs.Access);
            CurrentFunctions.AddRange(GetFunctionsByLvl(LevelAccess.Application));
        }
        public Employee GetUserByName(string name) => Staff.Where(emp => emp.Name == name).FirstOrDefault();
        public int GetStaffIndex(Employee emp) => Staff.FindIndex(a => a.Equals(emp));
        public Employee GetUserByPartialName(string pname) => Staff.Where(emp => emp.Name.Contains(pname)).FirstOrDefault();
        private List<Function> GetFunctionsByLvl(LevelAccess access) => LoadIt.LoadSomething<Function>(System.Reflection.Assembly.GetExecutingAssembly(), new object[] { this })
            .Where(
                func => func.CmdLvl == access
            ).ToList();
        private IUser GetUserTypeByName(string name) => UserTypes.Where(user => user.Name == name).FirstOrDefault();
        private IUser GetUserTypeByLvl(LevelAccess access) => UserTypes.Where(user => user.Access == access).FirstOrDefault();
    }
}
