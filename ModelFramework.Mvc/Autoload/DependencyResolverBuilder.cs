using System.Web.Mvc;

using Autofac.Integration.Mvc;

using ChessOk.ModelFramework.Web;

[assembly: WebActivator.PreApplicationStartMethod(typeof(DependencyResolverBuilder), "Build")]

namespace ChessOk.ModelFramework.Web
{
    public static class DependencyResolverBuilder
    {
        private static bool _buildWasCalled;

        public static void Build()
        {
            if (_buildWasCalled) return;
            _buildWasCalled = true;

            var container = GlobalLifetimeScope.Instance;
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
