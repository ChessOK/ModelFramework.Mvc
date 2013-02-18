using System;

namespace ChessOk.ModelFramework.Web.ViewModels
{
    public class ViewModelFactory
    {
        private readonly IModelContext _context;

        public ViewModelFactory(IModelContext context)
        {
            _context = context;
        }

        public TViewModel Create<TViewModel>()
        {
            return Create<TViewModel>(null);
        }

        public virtual TViewModel Create<TViewModel>(Action<TViewModel> initialization)
        {
            var viewModel = _context.Get<TViewModel>();

            if (initialization != null)
            {
                initialization(viewModel);
            }

            var initializableForm = viewModel as IInitializableForm;
            if (initializableForm != null)
            {
                initializableForm.InitializeForm(_context);
            }

            return viewModel;
        }

        public virtual void Initialize(object viewModel)
        {
            var initializable = viewModel as IInitializableViewModel;
            if (initializable != null)
            {
                initializable.Initialize(_context);
            }
        }
    }
}
