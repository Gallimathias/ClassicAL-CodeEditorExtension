using System;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text.Operations;
using Microsoft.VisualStudio.Utilities;
using Microsoft.VisualStudio.Text.Editor;

namespace AnZw.NavCodeEditor.Extensions
{

    [TextViewRole("INTERACTIVE"),
     ContentType("C/AL"),
     Name("AL2IntellisensePreKeyProcessor"),
     Order(Before = "ALEditorKeyProcessor"),
     Export(typeof(IKeyProcessorProvider))]
    public class CALCompletionHandlerProvider : IKeyProcessorProvider
    {

        [Import]
        internal IIntellisenseSessionStackMapService StackMapService { get; set; }
        [Import]
        internal IEditorOperationsFactoryService EditorOperationsFactoryService;

        public KeyProcessor GetAssociatedProcessor(IWpfTextView textView)
        {
            if (textView == null)
                throw new ArgumentNullException("textView");

            var stackForTextView = StackMapService.GetStackForTextView(textView);
            var editorOperations = EditorOperationsFactoryService.GetEditorOperations(textView);
            return new CALKeyProcessor(textView, editorOperations);
        }

    }

}
