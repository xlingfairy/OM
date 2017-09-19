using Notifications.Wpf;

namespace OM.App
{
    public interface IShell
    {
        void ShowTab<T>(bool allowMuti = false) where T : BaseVM;

        void ShowTab<T>(T vm, bool allowMuti = false) where T : BaseVM;

        void AddLog(string tip);
    }
}