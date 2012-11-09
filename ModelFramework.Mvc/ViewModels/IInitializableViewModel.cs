using System.Web.Mvc;

namespace ChessOk.ModelFramework.Web
{
    /// <summary>
    /// Реализуйте этот интерфейс в модели представления для инициализации дополнительных
    /// данных модели представления с помощью <see cref="ModelContext"/> внутри объекта.
    /// Инициализация выполняется каждый раз при вызове перегруженного метода View контроллера.
    /// </summary>
    public interface IInitializableViewModel
    {
        void Initialize(ModelContext context);
    }
}
