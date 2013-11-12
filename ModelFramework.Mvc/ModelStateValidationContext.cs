using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using Autofac;

using ChessOk.ModelFramework.Validation;

namespace ChessOk.ModelFramework.Web
{
    /// <summary>
    /// Предоставляет реализацию <see cref="IValidationContext"/>,
    /// основанного на MVC'шном <see cref="ModelStateDictionary"/>.
    /// </summary>
    public class ModelStateValidationContext : IValidationContext
    {
        private readonly IModelContext _context;

        private readonly ModelStateDictionary _modelState;
        private readonly ValidationErrorKeyTransformer _transformer =
            new ValidationErrorKeyTransformer();

        public ModelStateValidationContext(IModelContext context, ModelStateDictionary modelState)
        {
            _context = context;
            _modelState = modelState;

            _context.CreateChildContext(ContextHierarchy.ValidationContext,
                x => x.RegisterInstance(this).As<IValidationContext>());
        }

        public void Dispose()
        {
        }

        public void AddError(string key, string message)
        {
            key = _transformer.NormalizeKeyAndApplyReplaces(key);

            if (!_modelState.ContainsKey(key))
            {
                _modelState.AddModelError(String.Empty, message);
            }
            else
            {
                _modelState.AddModelError(key, message);
            }
        }

        public void ThrowExceptionIfInvalid()
        {
            if (!_modelState.IsValid)
            {
                throw new ValidationException(this);
            }
        }

        public ICollection<string> GetErrors(string key)
        {
            return _modelState.ContainsKey(key)
                ? _modelState[key].Errors.Select(x => x.ErrorMessage).ToArray()
                : new string[0];
        }

        public void RemoveErrors(string key)
        {
            if (!_modelState.ContainsKey(key))
            {
                throw new KeyNotFoundException();
            }

            _modelState[key].Errors.Clear();
        }

        public void Clear()
        {
            foreach (var key in Keys)
            {
                _modelState[key].Errors.Clear();
            }
        }

        public IDisposable ModifyKeys(string pattern, string replacement)
        {
            return _transformer.ModifyKeys(pattern, replacement);
        }

        public IModelContext ModelContext
        {
            get
            {
                return _context;
            }
        }

        public bool IsValid
        {
            get
            {
                return _modelState.IsValid;
            }
        }

        public ICollection<string> this[string key]
        {
            get
            {
                return GetErrors(key);
            }
        }

        public ICollection<string> Keys
        {
            get
            {
                return _modelState.Keys;
            }
        }
    }
}
