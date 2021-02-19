namespace Minesweaper.Helpers
{
    public interface IUIHelper
    {
        void Write(object message);
        void WriteLine(object message);
        int GetInteger(string message);
        string GetString(string message);
        void Clear();
    }
}
