using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserGroups.Code.API;

namespace UserGroups.Code.User
{
    public class Paymaster : IUser
    {
        public LevelAccess Access => LevelAccess.Paymaster;
        public string Name => "Кассир";
    }
}
