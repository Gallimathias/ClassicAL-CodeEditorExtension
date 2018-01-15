using System.Windows.Input;

namespace AnZw.NavCodeEditor.Extensions.InputProcessors
{
    public class InputProcessor
    {
        public CALKeyProcessor KeyProcessor { get; }

        public InputProcessor(CALKeyProcessor keyProcessor)
        {
            KeyProcessor = keyProcessor;
        }

        public virtual void KeyDown(KeyEventArgs args, KeyStateInfo keyStateInfo)
        {
        }

        public virtual void TextInput(TextCompositionEventArgs args)
        {
        }

    }
}
