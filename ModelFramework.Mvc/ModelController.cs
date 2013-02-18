using System;
using System.Web.Mvc;

using Autofac;

using ChessOk.ModelFramework.Validation;
using ChessOk.ModelFramework.Web.ViewModels;

namespace ChessOk.ModelFramework.Web
{
    public class ModelControllerBase : Controller
    {
        public static readonly object ContextTag = typeof(ModelControllerBase);

        private IModelContext _modelContext;
        private IApplicationBus _bus;

        protected IModelContext Context
        {
            get
            {
                if (_modelContext == null)
                {
                    var parentContext = DependencyResolver.Current.GetService<ModelContext>();
                    _modelContext = parentContext.CreateChildContext(
                        ContextTag, ConfigureContainer);
                }

                return _modelContext;
            }
        }

        protected IApplicationBus Bus
        {
            get
            {
                return _bus ?? (_bus = Context.Get<IApplicationBus>());
            }
        }

        protected virtual void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterInstance(ModelState).AsSelf();
            builder.Register(
                x => new ModelStateValidationContext(
                x.Resolve<IModelContext>(), x.Resolve<ModelStateDictionary>()))
                .As<IValidationContext>()
                .SingleInstance();
        }

        protected override void Dispose(bool disposing)
        {
            if (_bus != null) _bus.Dispose();

            base.Dispose(disposing);
        }

        /// <summary>
        /// <para>1. Создает объект модели представления указанного типа, используя контейнер</para>
        /// <para>2. Если модель реализует интерфейс <see cref="IInitializableForm"/>, то выполняет инициализацию</para>
        /// </summary>
        /// <typeparam name="TViewModel">Тип модели представления</typeparam>
        /// <returns>Объект модели представления</returns>
        protected TViewModel CreateViewModel<TViewModel>()
        {
            return CreateViewModel<TViewModel>(null);
        }

        /// <summary>
        /// <para>1. Создает объект модели представления указанного типа, используя контейнер</para>
        /// <para>2. Выполняет указанный в параметрах метод инициализации</para>
        /// <para>3. Если модель реализует интерфейс <see cref="IInitializableForm"/>, то выполняет инициализацию</para>
        /// <exception cref="InvalidOperationException" />
        /// </summary>
        /// <typeparam name="TViewModel">Тип модели представления</typeparam>
        /// <param name="initialization">Предварительная инициализация</param>
        /// <returns>Объект модели представления</returns>
        protected TViewModel CreateViewModel<TViewModel>(Action<TViewModel> initialization)
        {
            var creator = Context.Get<ViewModelFactory>();
            return creator.Create(initialization);
        }

        #region View methods overrides

        /// <summary>
        /// Инициализирует модель представления, если она реализует интерфейс <see cref="IInitializableViewModel"/> и вызывает базовый метод View
        /// </summary>
        protected new ViewResult View(object model)
        {
            TryInitializeViewModel(model);
            return base.View(model);
        }

        /// <summary>
        /// Инициализирует модель представления, если она реализует интерфейс <see cref="IInitializableViewModel"/> и вызывает базовый метод View
        /// </summary>
        protected new ViewResult View(IView view, object model)
        {
            TryInitializeViewModel(model);
            return base.View(view, model);
        }

        /// <summary>
        /// Инициализирует модель представления, если она реализует интерфейс <see cref="IInitializableViewModel"/> и вызывает базовый метод View
        /// </summary>
        protected new ViewResult View(string viewName, object model)
        {
            TryInitializeViewModel(model);
            return base.View(viewName, model);
        }

        /// <summary>
        /// Инициализирует модель представления, если она реализует интерфейс <see cref="IInitializableViewModel"/> и вызывает базовый метод View
        /// </summary>
        protected new ViewResult View(string viewName, string masterName, object model)
        {
            TryInitializeViewModel(model);
            return base.View(viewName, masterName, model);
        }

        #endregion

        #region PartialView methods overrides

        /// <summary>
        /// Инициализирует модель представления, если она реализует интерфейс <see cref="IInitializableViewModel"/> и вызывает базовый метод PartialView
        /// </summary>
        protected new PartialViewResult PartialView(object model)
        {
            TryInitializeViewModel(model);
            return base.PartialView(model);
        }

        /// <summary>
        /// Инициализирует модель представления, если она реализует интерфейс <see cref="IInitializableViewModel"/> и вызывает базовый метод PartialView
        /// </summary>
        protected new PartialViewResult PartialView(string viewName, object model)
        {
            TryInitializeViewModel(model);
            return base.PartialView(viewName, model);
        }

        #endregion

        private void TryInitializeViewModel(object model)
        {
            var initializableModel = model as IInitializableViewModel;
            if (initializableModel != null)
            {
                var factory = Context.Get<ViewModelFactory>();
                factory.Initialize(initializableModel);
            }
        }
    }
}
