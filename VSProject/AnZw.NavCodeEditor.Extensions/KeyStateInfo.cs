using System;
using System.Windows.Input;

namespace AnZw.NavCodeEditor.Extensions
{
    public class KeyStateInfo
    {
        public Key Key { get; set; }
        public bool Control { get; set; }
        public bool Alt { get; set; }
        public bool Shift { get; set; }

        public KeyStateInfo()
        {
            Control = false;
            Alt = false;
            Shift = false;
            Key = Key.None;
        }
        public KeyStateInfo(string hotKey) : this() => SetHotKey(hotKey);

        public void SetHotKey(string hotKey)
        {
            Control = hotKey.Contains("Ctrl");
            Shift = hotKey.Contains("Shift");
            Alt = hotKey.Contains("Alt");

            if (Control)
                hotKey = hotKey.Replace("Ctrl", "");

            if (Shift)
                hotKey = hotKey.Replace("Shift", "");

            if (Alt)
                hotKey = hotKey.Replace("Alt", "");

            hotKey = hotKey.Replace("+", "");

            if (Enum.TryParse(hotKey, out Key keyValue))
                Key = keyValue;
            else
                Key = Key.None;
        }

        public KeyStateInfo(KeyEventArgs args)
        {
            Key = args.Key;
            Control = args.KeyboardDevice.IsKeyDown(Key.LeftCtrl) || args.KeyboardDevice.IsKeyDown(Key.RightCtrl);
            Alt = args.KeyboardDevice.IsKeyDown(Key.LeftAlt) || args.KeyboardDevice.IsKeyDown(Key.RightAlt);
            Shift = args.KeyboardDevice.IsKeyDown(Key.LeftShift) || args.KeyboardDevice.IsKeyDown(Key.RightShift);
        }

        public bool Equals(KeyStateInfo value) =>
            (Control == value.Control) &&
            (Alt == value.Alt) &&
            (Shift == value.Shift) &&
            (Key == value.Key) &&
            (Key != Key.None) &&
            (value.Key != Key.None);
    }
}
