using System;
using System.Windows.Input;

namespace AnZw.NavCodeEditor.Extensions
{
    public class KeyEventArgsHelper
    {
        public static bool Equals(KeyEventArgs args, string shortcut)
        {
            var ctrl = shortcut.Contains("Ctrl");
            var shift = shortcut.Contains("Shift");
            var alt = shortcut.Contains("Alt");

            if (ctrl)
                shortcut = shortcut.Replace("Ctrl", "");

            if (shift)
                shortcut = shortcut.Replace("Shift", "");

            if (alt)
                shortcut = shortcut.Replace("Alt", "");

            shortcut = shortcut.Replace("+", "");

            if (!Enum.TryParse(shortcut, out Key keyValue))
                return false;

            var ctrlDown = args.KeyboardDevice.IsKeyDown(Key.LeftCtrl) || args.KeyboardDevice.IsKeyDown(Key.RightCtrl);
            var altDown = args.KeyboardDevice.IsKeyDown(Key.LeftAlt) || args.KeyboardDevice.IsKeyDown(Key.RightAlt);
            var shiftDown = args.KeyboardDevice.IsKeyDown(Key.LeftShift) || args.KeyboardDevice.IsKeyDown(Key.RightShift);

            return (ctrl == ctrlDown) && (shift == shiftDown) && (alt == altDown) && (keyValue == args.Key);
        }
    }
}
