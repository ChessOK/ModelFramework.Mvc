using System;
using System.Collections.Generic;
using System.Linq;

using Module = Autofac.Module;

namespace ChessOk.ModelFramework.Web
{
    public abstract class AutoloadModule : Module
    {
        public virtual int Order { get { return 100; } }

        public static IEnumerable<AutoloadModule> ScanAssembliesForAutoloadModules()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .Where(x => !x.IsDynamic)
                .SelectMany(x => x.GetExportedTypes())
                .Where(x => typeof(AutoloadModule).IsAssignableFrom(x) && !x.IsAbstract)
                .Select(Activator.CreateInstance)
                .Cast<AutoloadModule>()
                .OrderBy(x => x.Order)
                .ToArray();
        }
    }
}
