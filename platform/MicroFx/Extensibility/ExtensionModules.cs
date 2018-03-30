using System;
using System.Collections.Generic;
using System.Linq;

namespace MicroFx.Extensibility
{
    public class ExtensionModules
    {
        private readonly List<Func<IExtensionModule>> moduleFuncs;
        private List<IExtensionModule> modules;

        public ExtensionModules()
        {
            moduleFuncs = new List<Func<IExtensionModule>>();
        }
        private void Initialise()
        {
            modules = moduleFuncs.Select(y => y()).ToList();
            if (!modules.Any()) return;
            var current = modules[0];
            foreach (var next in modules)
            {
                if (current == next)
                    continue;
                var next1 = next;
                current.Init(c => next1.Register(c));
                current = next;
            }
        }

        public void Register(IRegisterContext context)
        {
            Initialise();
            if (!modules.Any()) return;
            var result = modules[0].Register(context);
        }

        public void Add(IExtensionModule mod)
        {
            moduleFuncs.Add(()=> mod);
        }
    }
}