using System.IO;

namespace AnZw.NavCodeEditor.Extensions
{
    public static class DebugLog
    {
        public static void WriteLogEntry(string messageText) => File.AppendAllText(@"c:\temp\log.txt", messageText);
    }
}
