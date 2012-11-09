namespace ChessOk.ModelFramework.Web
{
    /// <summary>
    /// Реализуйте этот интерфейс в модели представления для инициализации
    /// данных формы из <see cref="ModelContext"/> внутри самого объекта.
    /// Инициализация выполняется только внутри метода CreateViewModel контроллера.
    /// </summary>
    public interface IInitializableForm
    {
        void InitializeForm(ModelContext context);
    }
}