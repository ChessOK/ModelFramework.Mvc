using System.Web.Mvc;

using Autofac;
using Autofac.Integration.Mvc;

using ChessOk.ModelFramework.Web;

[assembly: WebActivator.PreApplicationStartMethod(typeof(DependencyResolverBuilder), "Build")]

namespace ChessOk.ModelFramework.Web
{
    public static class DependencyResolverBuilder
    {
        private static bool _buildWasCalled;

        public static readonly ContainerBuilder Instance = new ContainerBuilder();

        public static void Build()
        {
            if (_buildWasCalled) return;
            _buildWasCalled = true;

            var builder = new ContainerBuilder();

            var modules = AutoloadModule.ScanAssembliesForAutoloadModules();
            foreach (var module in modules)
            {
                builder.RegisterModule(module);
            }

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
