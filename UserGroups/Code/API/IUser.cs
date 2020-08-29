using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserGroups.Code.API
{
    /// <summary>
    /// Абстракция пользователя
    /// </summary>
    public interface IUser
    {
        LevelAccess Access { get; }
        string Name { get; }
    }
}
