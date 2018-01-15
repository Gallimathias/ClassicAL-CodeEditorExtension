using System;

namespace AnZw.NavCodeEditor.Extensions.Snippets
{
    public class SnippetDateTimeFunction : SnippetFunction
    {

        public SnippetDateTimeFunction()
        {
            Name = "DateTime";
            Description = "Returns current date time. Value can be formatted by providing format string after : character (i.e. DateTime:dd-MM-yyy";
        }

        public override string GetValue(string formatString)
        {
            var dateTime = DateTime.Now;

            if (string.IsNullOrWhiteSpace(formatString))
                return dateTime.ToString();

            return dateTime.ToString(formatString);
        }

    }
}
