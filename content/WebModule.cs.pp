using System.Reflection;

using Autofac;
using Autofac.Integration.Mvc;

using ChessOk.ModelFramework.Web;

namespace $rootnamespace$
{
    public class WebModule : AutoloadModule
    {
        protected override void Load(ContainerBuilder builder)
        {
			// Register your controllers and their dependencies
            builder.RegisterControllers();
		
			// Register commands, queries & view models conventions
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(x => x.Name.EndsWith("ViewModel") || x.Name.EndsWith("Command") || x.Name.EndsWith("Query"))
                .AsSelf();
        }
    }
}
