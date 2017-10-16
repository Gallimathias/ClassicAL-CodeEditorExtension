using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Microsoft.VisualStudio.Text;
using AnZw.NavCodeEditor.Extensions.LanguageService;

namespace AnZw.NavCodeEditor.Extensions.InputProcessors
{
    public class XmlDocInputProcessor : InputProcessor
    {

        public XmlDocInputProcessor(CALKeyProcessor keyProcessor) : base(keyProcessor)
        {
        }

        public override void KeyDown(KeyEventArgs args, KeyStateInfo keyStateInfo)
        {
            if ((args.Handled) || (!Session.Current.Settings.EnableXmlDocumentation))
                return;

            if ((args.Key == Key.Enter) || (args.Key == Key.Return))
            {
                CurrentLineInformation lineInformation = KeyProcessor.CurrentLineInformation;

                if ((lineInformation.LineText.StartsWith("///")) && (lineInformation.CaretColumn > 2))
                {
                    KeyProcessor.EditorOperations.InsertNewLine();
                    KeyProcessor.EditorOperations.InsertText("/// ");
                    args.Handled = true;
                }
            }

        }

        public override void TextInput(TextCompositionEventArgs args)
        {
            if ((args.Handled) || (!Session.Current.Settings.EnableXmlDocumentation))
                return;

            if (args.Text != "/")
                return;

            CurrentLineInformation lineInformation = KeyProcessor.CurrentLineInformation;

            if ((lineInformation.LineText == "//") && (lineInformation.CaretColumn == 2))
            {
                IMethod activeMethod = KeyProcessor.MethodManager.ActiveMethod;
                if (activeMethod != null)
                {
                    Tuple<SnapshotPoint, SnapshotPoint> methodInterval = activeMethod.GetCodeInterval();
                    int size = lineInformation.CaretPosition - methodInterval.Item1.Position;
                    //we don't want to process too long texts, let's assume that max. indent is 30 characters and it doc. shoudl always start in the first line
                    if (size == 2)
                    {
                        IEnumerable<string> methodLines = activeMethod.GetLines();
                        if (CanGenerateXmlDocumentation(methodLines))
                        {
                            int moveUp = 2;

                           KeyProcessor.EditorOperations.InsertText("/ <summary>");
                           KeyProcessor.EditorOperations.InsertNewLine();
                           KeyProcessor.EditorOperations.InsertText("/// ");
                           KeyProcessor.EditorOperations.InsertNewLine();
                           KeyProcessor.EditorOperations.InsertText("/// </summary>");
                           KeyProcessor.EditorOperations.InsertNewLine();

                            //try to find current method
                            try
                            {
                                List<SignatureInfo> signatures = KeyProcessor.NavConnector.TypeInfoManager.GetSignatures(activeMethod.Name).ToList();

                                if (signatures.Count > 0)
                                {
                                    SignatureInfo signature = signatures[0];
                                    foreach (ParameterInfo parameterInfo in signature.Parameters)
                                    {
                                        KeyProcessor.EditorOperations.InsertText($"/// <param name=\"{parameterInfo.ParameterName}\"></param>");
                                        KeyProcessor.EditorOperations.InsertNewLine();
                                        moveUp++;
                                    }
                                    if ((signature.ReturnType != null) && (!String.IsNullOrWhiteSpace(signature.ReturnType.TypeName)))
                                    {
                                        KeyProcessor.EditorOperations.InsertText("/// <returns></returns>");
                                        KeyProcessor.EditorOperations.InsertNewLine();
                                        moveUp++;
                                    }
                                }
                            }
                            catch (Exception e)
                            {
                                DebugLog.WriteLogEntry(e.Message);
                                DebugLog.WriteLogEntry(e.Source);
                                DebugLog.WriteLogEntry(e.StackTrace);
                            }

                            for (int i = 0; i < moveUp; i++)
                                KeyProcessor.EditorOperations.MoveLineUp(false);

                            KeyProcessor.EditorOperations.MoveToEndOfLine(false);

                            args.Handled = true;
                        }
                    }
                }
            }
        }

        protected bool CanGenerateXmlDocumentation(IEnumerable<string> lines)
        {
            foreach (var line in lines)
            {
                var trimLine = line.TrimStart();
                if (!string.IsNullOrWhiteSpace(trimLine))
                    return (!line.StartsWith("///"));
            }

            return true;
        }


    }
}
