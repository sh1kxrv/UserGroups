using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserGroups.Code.API;

namespace UserGroups.Code.User
{
    /// <summary>
    /// Бухгалтер
    /// </summary>
    public class Accountant : IUser
    {
        public LevelAccess Access => LevelAccess.Accountant;

        public string Name => "Бухгалтер";
    }
}
