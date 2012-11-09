namespace ChessOk.ModelFramework.Web
{
    /// <summary>
    /// ���������� ���� ��������� � ������ ������������� ��� �������������
    /// ������ ����� �� <see cref="ModelContext"/> ������ ������ �������.
    /// ������������� ����������� ������ ������ ������ CreateViewModel �����������.
    /// </summary>
    public interface IInitializableForm
    {
        void InitializeForm(ModelContext context);
    }
}