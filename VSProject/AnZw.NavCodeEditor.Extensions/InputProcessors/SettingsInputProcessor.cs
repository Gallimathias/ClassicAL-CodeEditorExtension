using System.Windows.Input;

namespace AnZw.NavCodeEditor.Extensions.InputProcessors
{
    public class SettingsInputProcessor : InputProcessor
    {
        public SettingsInputProcessor(CALKeyProcessor keyProcessor) : base(keyProcessor)
        {
        }

        public override void KeyDown(KeyEventArgs args, KeyStateInfo keyStateInfo)
        {
            if (args.Handled)
                return;

            if (keyStateInfo.Equals(Session.Current.Settings.SettingsKeyStateInfo))
            {
                Session.Current.ShowSettings();
                args.Handled = true;
            }

        }

    }
}
