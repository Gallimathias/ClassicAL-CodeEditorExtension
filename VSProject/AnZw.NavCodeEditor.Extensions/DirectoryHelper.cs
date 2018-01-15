using System;
using System.IO;

namespace AnZw.NavCodeEditor.Extensions
{
    public class DirectoryHelper
    {
        public static string GetAssemblyPath(Type type) => Path.GetDirectoryName(type.Assembly.Location);
        public static string CurrentAssemblyPath() => GetAssemblyPath(typeof(DirectoryHelper));
        
        public static string GetApplicationDataPath()
        {
            var dataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var applicationDataPath = Path.Combine(dataPath, "AnZw.NavCodeEditor.Extensions");

            if (!Directory.Exists(applicationDataPath))
                Directory.CreateDirectory(applicationDataPath);

            return applicationDataPath;
        }

    }
}
