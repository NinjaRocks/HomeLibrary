using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace MicroFx
{
    public static class AssemblyHelper
    {
        static AssemblyHelper()
        {
            AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += (s, e) =>
            {
                var a = Assembly.ReflectionOnlyLoad(e.Name);
                if (a == null) throw new TypeLoadException("Could not load assembly " + e.Name);
                return a;
            };
        }

        private static List<T> GetTypes<T>(Assembly assembly)
        {
            var modules = assembly.GetTypes()
                .Where(t => typeof(T).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
                .Select(t=> (T)Activator.CreateInstance(t))
                .ToList();

            return modules;
        }

       
        public static List<T> Scan<T>(Assembly assembly, bool includeReferencedAssemblies = false)
        {
            var types = new List<T>();
           
            types.AddRange(GetTypes<T>(assembly));

            if (!includeReferencedAssemblies)
                return types;
            

            foreach (var assemblyName in assembly.GetReferencedAssemblies())
            {
                try
                {
                    var referedAssembly = Assembly.ReflectionOnlyLoad(assemblyName.FullName);
                    types.AddRange(GetTypes<T>(referedAssembly));

                }
                catch (FileNotFoundException)
                {
                   
                }
            }

            return types;
        }
    }
}