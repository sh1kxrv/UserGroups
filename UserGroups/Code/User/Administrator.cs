using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserGroups.Code.API;

namespace UserGroups.Code.User
{
    public class Administrator : IUser
    {
        public LevelAccess Access => LevelAccess.Admin;
        public string Name => "Администратор";
    }
}
