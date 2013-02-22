using Autofac;

namespace ChessOk.ModelFramework.Web
{
    public class GlobalLifetimeScope
    {
        public ILifetimeScope LifetimeScope;

        public GlobalLifetimeScope()
        {
            var builder = new ContainerBuilder();

            var modules = AutoloadModule.ScanAssembliesForAutoloadModules();
            foreach (var module in modules)
            {
                builder.RegisterModule(module);
            }

            LifetimeScope = builder.Build();
        }

        public static ILifetimeScope Instance
        {
            get { return InternalLifetimeScope.InternalInstance.LifetimeScope; }
        }

        private static class InternalLifetimeScope
        {
            static InternalLifetimeScope()
            {
                 
            }

            internal static readonly GlobalLifetimeScope InternalInstance = new GlobalLifetimeScope();
        }
    }
}
