using System.Collections.Generic;
using System.Windows.Input;

namespace AnZw.NavCodeEditor.Extensions.InputProcessors
{
    public class ClosingBracketInputProcessor : InputProcessor
    {

        protected Dictionary<string, string> ClosingTexts { get; }

        public ClosingBracketInputProcessor(CALKeyProcessor keyProcessor) : base(keyProcessor)
        {
            ClosingTexts = new Dictionary<string, string>
            {
                { "(", " )" },   //we have to add space before closing bracket to make intellisense work
                { "[", " ]" },   //the same as above
                { "{", "}" },
                { "'", "'" },
                { "\"", "\"" }
            };
        }

        public override void TextInput(TextCompositionEventArgs args)
        {
            if ((args.Handled) || (!Session.Current.Settings.AutoCloseElements))
                return;

            if (ClosingTexts.ContainsKey(args.Text))
            {
                var closingText = ClosingTexts[args.Text];

                KeyProcessor.EditorOperations.InsertText(args.Text);
                KeyProcessor.EditorOperations.InsertText(closingText);
                for (int i = 0; i < closingText.Length; i++)
                    KeyProcessor.EditorOperations.MoveToPreviousCharacter(false);

                args.Handled = true;
            }

        }


    }
}
