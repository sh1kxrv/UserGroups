using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserGroups.Code.API;

namespace UserGroups.Code.User
{
    /// <summary>
    /// Склад
    /// </summary>
    public class Storage : IUser
    {
        public LevelAccess Access => LevelAccess.Storage;
        public string Name => "Склад";
    }
}
