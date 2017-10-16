using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace AnZw.NavCodeEditor.Extensions
{
    public class KeyStateInfo
    {
        public HashSet<Key> Keys { get; private set; }

        public KeyStateInfo()
        {
            Keys = new HashSet<Key>();
        }
        public KeyStateInfo(string hotKey) : this() => SetHotKey(hotKey);
        public KeyStateInfo(KeyEventArgs args) : this()
        {
            //Keys.Add(args.Key);

            foreach (var key in Enum.GetValues(typeof(Key)))
            {
                if (args.KeyboardDevice.IsKeyDown((Key)key))
                    Keys.Add((Key)key);
            }
        }

        public void SetHotKey(string hotKey)
        {
            foreach (var keyString in hotKey.Split('+'))
            {
                if (Enum.TryParse(hotKey, out Key keyValue))
                    Keys.Add(keyValue);
            }
        }
        
        public override bool Equals(object obj)
        {
            if (obj is KeyStateInfo value)
            {
                foreach (var key in value.Keys)
                {
                    if (!Keys.Contains(key))
                        return false;
                }
            }

            return true;
        }
    }
}
