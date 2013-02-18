using System;
using System.ComponentModel;
using System.Security;

using Autofac;

using ChessOk.ModelFramework.Testing;
using ChessOk.ModelFramework.Web;
using ChessOk.ModelFramework.Web.ViewModels;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessOk.ModelFramework.Mvc.Tests
{
    [TestClass]
    public class ViewModelFactoryTests : ModelContextTest
    {
        private ViewModelFactory _factory;

        [TestInitialize]
        public void SetUp()
        {
            _factory = Model.Get<ViewModelFactory>();
        }

        [TestMethod]
        [ExpectedException(typeof(VerificationException))]
        public void Create_ShouldCall_InitializeFormMethod_IfSupported()
        {
            _factory.Create<Sample>();
        }

        [TestMethod]
        [ExpectedException(typeof(Win32Exception))]
        public void Create_ShouldCall_InitializationAction_IfGiven()
        {
            _factory.Create<Sample>(x => x.ThrowWin32Exception());
        }

        [TestMethod]
        [ExpectedException(typeof(VerificationException))]
        public void Initialize_ShouldCall_InitializeMethod_IfSupported()
        {
            var model = _factory.Create<Sample>();
            _factory.Initialize(model);
        }

        [TestMethod]
        public void ShouldNotFail_IfInterfacesAreNotImplemented()
        {
            var model = _factory.Create<Simple>();
            _factory.Initialize(model);
        }

        protected override void ConfigureContainer(ContainerBuilder builder)
        {
            base.ConfigureContainer(builder);
            builder.Register(x => new ViewModelFactory(x.Resolve<IModelContext>())).AsSelf();
            builder.RegisterType<Sample>().AsSelf();
            builder.RegisterType<Simple>().AsSelf();
        }

        public class Sample : IInitializableForm, IInitializableViewModel
        {
            public void ThrowWin32Exception()
            {
                throw new Win32Exception();
            }

            public void InitializeForm(IModelContext context)
            {
                throw new VerificationException();
            }

            public void Initialize(IModelContext context)
            {
                throw new VerificationException();
            }
        }

        public class Simple
        {
        }
    }
}
