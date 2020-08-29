using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using System.Threading.Tasks;
using UserGroups.Code.User.Custom;

namespace UserGroups.Code.FileSystem
{
    public class FileController
    {
        private Controller Controller;
        public FileController(Controller ctrl)
        {
            Controller = ctrl;
        }
        public void CreateDirectories()
        {
            if(!Directory.Exists("System/Staff"))
                Directory.CreateDirectory("System/Staff");
            if (!Directory.Exists("System/Company"))
                Directory.CreateDirectory("System/Company");
        }
        public void LoadStaff()
        {
            if(File.Exists("System/Staff/Staff.json"))
                Controller.Staff = JsonConvert.DeserializeObject<List<Employee>>(File.ReadAllText("System/Staff/Staff.json"));
        }
        public void SaveStaff()
        {
            File.WriteAllText("System/Staff/Staff.json", JsonConvert.SerializeObject(Controller.Staff));
        }
        public void LoadCompany()
        {
            if (File.Exists("System/Company/Company.json"))
                Controller.OwnCompany = JsonConvert.DeserializeObject<Company.Company>(File.ReadAllText("System/Company/Company.json"));
        }
        public void SaveCompany()
        {
            File.WriteAllText("System/Company/Company.json", JsonConvert.SerializeObject(Controller.OwnCompany));
        }
    }
}
