using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;
using AnZw.NavCodeEditor.Extensions.Snippets.CodeGenerators;

namespace AnZw.NavCodeEditor.Extensions.Snippets
{

    public class SnippetManager
    {
        /// <summary>
        /// list of snippet functions
        /// </summary>
        public Dictionary<string, SnippetFunction> Functions { get; set; }
        /// <summary>
        /// snippet function returning text selected in editor
        /// </summary>
        public SnippetVariable SelectedTextFunction { get; private set; }
        public BindingList<SnippetVariable> Variables => Settings.Variables;

        public List<CodeGeneratorSnippet> CodeGeneratorSnippets { get; }
        public Session Session { get; }
        public SessionSettings Settings
        {
            get
            {
                if (Session != null)
                    return Session.Settings;

                return localSettings;
            }
            set => localSettings = value;

        }

        private SessionSettings localSettings;
        
        public SnippetManager()
        {
            localSettings = null;
            Session = null;
            Functions = new Dictionary<string, SnippetFunction>();
            CodeGeneratorSnippets = new List<CodeGeneratorSnippet>();
            CreateFunctions();
            CreateCodeGenerators();
        }
        public SnippetManager(Session session) : this()
        {
            Session = session;
        }
        public SnippetManager(SessionSettings settings) : this()
        {
            localSettings = settings;
        }

        protected void AddFunction(SnippetFunction function) => Functions.Add(function.Name, function);
        
        protected void CreateFunctions()
        {
            SelectedTextFunction = new SnippetVariable() { Name = "SelectedText", Description = "Selected text" };
            AddFunction(SelectedTextFunction);
            AddFunction(new SnippetDateTimeFunction());
        }

        protected void CreateCodeGenerators()
        {
            CodeGeneratorSnippets.Add(new RecordFieldListCodeGenerator());
            CodeGeneratorSnippets.Add(new RecordAssignmentCodeGenerator());
        }

        public string ParseSnippet(Snippet snippet, int indent, CALKeyProcessor keyProcessor)
        {
            var content = snippet.Run(keyProcessor);

            if (content == null)
                return null;

            int pos = content.IndexOf("{{");
            if (pos >= 0)
            {
                var builder = new StringBuilder();
                int startPos = 0;
                while (pos >= 0)
                {
                    if (content.Substring(pos, 4) == "{{{{")
                    {
                        builder.Append(content.Substring(startPos, pos - startPos + 2));
                        startPos = pos + 4;
                        pos = startPos;
                    }
                    else
                    {
                        int endPos = content.IndexOf("}}", pos);
                        if (endPos > 0)
                        {
                            string variableName = content.Substring(pos + 2, endPos - pos - 2).Trim();
                            string variableValue = GetVariableValue(variableName);

                            //check if text has to be indented
                            int prevNewLinePos = content.LastIndexOf('\n', pos);
                            if (prevNewLinePos >= 0)
                                variableValue = IndentText(variableValue, pos - prevNewLinePos - 1);

                            //add text to string builder
                            builder.Append(content.Substring(startPos, pos - startPos));
                            builder.Append(variableValue);
                            startPos = endPos + 2;

                            //move pos
                            pos = startPos;
                        }
                    }
                    pos = content.IndexOf("{{", pos);
                }

                if (startPos < content.Length)
                    builder.Append(content.Substring(startPos));

                content = builder.ToString();
            }

            return IndentText(content, indent);
        }

        protected string IndentText(string text, int indent)
        {
            if (indent <= 0)
                return text;
            
            return text.Replace("\n", "\n".PadRight(indent + 1));
        }

        protected SnippetVariable FindVariable(string name)
        {
            var settings = Settings;

            for (int i = 0; i < settings.Variables.Count; i++)
            {
                if (settings.Variables[i].Name == name)
                    return settings.Variables[i];
            }

            return null;
        }

        protected string GetVariableValue(string variableName)
        {
            var pos = variableName.IndexOf(':');
            var formatString = "";

            if (pos > 0)
            {
                formatString = variableName.Substring(pos + 1);
                variableName = variableName.Substring(0, pos);
            }

            if (string.IsNullOrWhiteSpace(variableName))
                return "";

            var snippetVariable = FindVariable(variableName);
            if (snippetVariable != null)
                return snippetVariable.GetValue(formatString);

            //predefined variables
            if (Functions.ContainsKey(variableName))
                return Functions[variableName].GetValue(formatString);

            return "";
        }

    }

}
