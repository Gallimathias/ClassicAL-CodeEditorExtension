using System.IO;
using AnZw.NavCodeEditor.Extensions.UI;
using AnZw.NavCodeEditor.Extensions.Snippets;

namespace AnZw.NavCodeEditor.Extensions
{
    public class Session
    {

        public static Session Current
        {
            get
            {
                if (current == null)
                    current = new Session();

                return current;
            }
        }

        protected string GlobalSettingsPath => Path.Combine(DirectoryHelper.CurrentAssemblyPath(), SettingsFileName);
        protected string UserSettingsPath => Path.Combine(DirectoryHelper.GetApplicationDataPath(), SettingsFileName);

        protected static string SettingsFileName;

        private static Session current;

        public SessionSettings Settings { get; private set; }
        public SnippetManager SnippetManager { get; }
        public SessionSettings GlobalSettings { get; private set; }


        public Session()
        {
            this.Settings = new SessionSettings();
            this.SnippetManager = new SnippetManager(this);
            LoadSettings();
        }
        static Session()
        {
            current = null;
            SettingsFileName = "AnZw.NavCodeEditor.Extensions.xml";
        }

        public bool ShowSettings()
        {
            var editableSettings = new SessionSettings(Settings);
            var settingsWindow = new ExtensionSettings
            {
                DataContext = editableSettings
            };

            var result = settingsWindow.ShowDialog();
            if (result.HasValue && result.Value)
            {
                Settings.CopyFrom(editableSettings, false);
                SaveSettings();
                return true;
            }

            return false;
        }

        public void CreateDefaultSnippets()
        {
            Settings.Snippets.Add(new Snippet() { Name = "FFOR", Description = "FOR loop template", Content = "FOR <<variable>> := 1 TO <<counter>> DO BEGIN\n  {{Selection}}\nEND;\n", HotKey = "Ctrl+Shift+I" });
            Settings.Snippets.Add(new Snippet() { Name = "RREPEAT", Description = "REPEAT UNTIL loop template", Content = "REPEAT\n  {{Selection}}\nUNTIL (<<condition>>);" });
            Settings.Snippets.Add(new Snippet() { Name = "//-", Description = "Start Modification Comment", Content = "//DOC {{Project}} {{Date}} {{User}} >>" });
            Settings.Snippets.Add(new Snippet() { Name = "//+", Description = "End Modification Comment", Content = "//DOC {{Project}} {{Date}} {{User}} <<" });
            Settings.Snippets.Add(new Snippet() { Name = "DDATABASE::", Description = "Convert number to DATABASE::number", Content = "DATABASE::\"{{Selection}}\"", HotKey = "Ctrl+Shift+D" });

            Settings.Variables.Add(new SnippetVariable() { Name = "User", Description = "Current user initials", Value = "AZ" });
            Settings.Variables.Add(new SnippetVariable() { Name = "Project", Description = "Code of current project/modification", Value = "OP06984" });
        }

        #region Loading and saving settings

        public void LoadSettings()
        {
            GlobalSettings = SessionSettings.LoadSettings(GlobalSettingsPath, true);
            Settings = SessionSettings.LoadSettings(UserSettingsPath, false);
            if (Settings == null)
                Settings = new SessionSettings(GlobalSettings);
        }

        public void SaveSettings() => Settings.SaveSettings(UserSettingsPath, true);

        #endregion

    }
}
