using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.Text.Editor;
using System.Windows.Input;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Operations;
using AnZw.NavCodeEditor.Extensions.LanguageService;
using AnZw.NavCodeEditor.Extensions.InputProcessors;

namespace AnZw.NavCodeEditor.Extensions
{
    public class CALKeyProcessor : KeyProcessor
    {
        public List<InputProcessor> InputProcessors { get; }
        public ITextView TextView { get; }
        public IEditorOperations EditorOperations { get; }
        public IMethodManager MethodManager { get; }
        public Connector NavConnector { get; }
        public CurrentLineInformation CurrentLineInformation
        {
            get
            {
                if (currentLineInformation == null)
                {
                    currentLineInformation = new CurrentLineInformation();

                    var snapshot = TextView.TextSnapshot;
                    var carretPoint = TextView.Caret.Position.BufferPosition;
                    var line = carretPoint.GetContainingLine();

                    currentLineInformation.LineText = line.GetText();
                    currentLineInformation.LineStartPosition = line.Start.Position;
                    currentLineInformation.CaretPosition = carretPoint.Position;
                    currentLineInformation.CaretColumn = currentLineInformation.CaretPosition - currentLineInformation.LineStartPosition;
                }
                return currentLineInformation;
            }
        }
        public string SelectedText
        {
            get
            {
                if (selectedText == null)
                    selectedText = TextView.Selection.StreamSelectionSpan.GetText();

                if (selectedText == null)
                    selectedText = "";

                return selectedText;
            }
        }

        protected bool zoomUpdated;

        private CurrentLineInformation currentLineInformation;
        private string selectedText;

        public CALKeyProcessor(ITextView textView, IEditorOperations editorOperations)
        {
            zoomUpdated = false;

            InputProcessors = new List<InputProcessor>();
            TextView = textView;
            EditorOperations = editorOperations;

            NavConnector = new Connector(textView);
            MethodManager = NavConnector.MethodManager;

            CreateInputProcessors();
        }

        public void ClearCacheData()
        {
            currentLineInformation = null;
            selectedText = null;
        }

        public override void KeyDown(KeyEventArgs args)
        {
            UpdateZoom();

            if (args == null)
                throw new ArgumentNullException("args");
            if (args.Handled)
                return;

            KeyStateInfo keyStateInfo = new KeyStateInfo(args);

            for (int i = 0; i < InputProcessors.Count; i++)
            {
                InputProcessors[i].KeyDown(args, keyStateInfo);

                if (args.Handled)
                {
                    ClearCacheData();
                    return;
                }
            }

            ClearCacheData();

            base.KeyDown(args);
        }

        public override void TextInput(TextCompositionEventArgs args)
        {
            if (args.Handled)
                return;

            for (int i = 0; i < InputProcessors.Count; i++)
            {
                InputProcessors[i].TextInput(args);

                if (args.Handled)
                {
                    ClearCacheData();
                    return;
                }
            }

            ClearCacheData();

            base.TextInput(args);
        }

        protected void CreateInputProcessors()
        {
            InputProcessors.Add(new ClosingBracketInputProcessor(this));
            InputProcessors.Add(new SettingsInputProcessor(this));
            InputProcessors.Add(new SnippetsInputProcessor(this));
            InputProcessors.Add(new XmlDocInputProcessor(this));
        }

        protected void UpdateZoom()
        {
            if (zoomUpdated)
                return;

            if ((Session.Current.Settings.SetZoom) && (Session.Current.Settings.Zoom > 0))
                EditorOperations.ZoomTo(Session.Current.Settings.Zoom);

            zoomUpdated = true;

        }
    }
}
