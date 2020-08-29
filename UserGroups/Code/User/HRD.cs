using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserGroups.Code.API;

namespace UserGroups.Code.User
{
    /// <summary>
    /// Отдел кадров
    /// </summary>
    public class HRD : IUser
    {
        public LevelAccess Access => LevelAccess.HRD;
        public string Name => "Отдел кадров";
    }
}
