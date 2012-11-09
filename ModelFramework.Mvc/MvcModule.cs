using Autofac;
using Autofac.Integration.Mvc;

using ChessOk.ModelFramework.Web.ViewModels;

namespace ChessOk.ModelFramework.Web
{
    public class MvcModule : AutoloadModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Add HTTP request lifetime scoped registrations for the HTTP abstraction classes
            builder.RegisterModule(new AutofacWebTypesModule());

            builder.RegisterModule(new CoreModule());

            builder.Register(x => new ModelContext(x.Resolve<ILifetimeScope>()))
                .AsSelf()
                .InstancePerHttpRequest();

            builder.Register(x => new ViewModelFactory(x.Resolve<ModelContext>()))
                .AsSelf().InstancePerModelContext();
        }

        public override int Order
        {
            get { return 0; }
        }
    }
}
