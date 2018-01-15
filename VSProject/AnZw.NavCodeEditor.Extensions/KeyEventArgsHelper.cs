using System.Windows.Input;

namespace AnZw.NavCodeEditor.Extensions
{
    public class KeyEventArgsHelper
    {
        public static bool Equals(KeyEventArgs args, string shortcut)
        {
            var keyStateInfoShortcut = new KeyStateInfo(shortcut);
            var keyStateInfoArgs = new KeyStateInfo(args);

            return keyStateInfoArgs == keyStateInfoShortcut;
        }
    }
}
