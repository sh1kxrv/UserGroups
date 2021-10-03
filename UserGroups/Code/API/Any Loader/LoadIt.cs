using System;
using System.Collections.Generic;

namespace UserGroups.Code.API.Any_Loader
{
    public static class LoadIt
    {
        public static List<T> LoadSomething<T>(System.Reflection.Assembly asm, params object[] args) where T : class
        {
            List<T> Something = new List<T>();
            foreach (var type in asm.GetTypes())
                if (typeof(T).IsAssignableFrom(type) && type != typeof(T))
                    Something.Add(Activator.CreateInstance(type, args) as T);
            return Something;
        }
    }
}
