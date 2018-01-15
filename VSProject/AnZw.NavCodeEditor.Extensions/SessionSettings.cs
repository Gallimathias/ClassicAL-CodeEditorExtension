using System;
using System.ComponentModel;
using System.Linq;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using AnZw.NavCodeEditor.Extensions.Snippets;
using System.Windows;

namespace AnZw.NavCodeEditor.Extensions
{
    public class SessionSettings : ObservableObject<SessionSettings>
    {

        [XmlIgnore]
        public KeyStateInfo SettingsKeyStateInfo { get; set; }
        [XmlIgnore]
        public KeyStateInfo SnippetSelectionKeyStateInfo { get; set; }

        public string SettingsHotKey
        {
            get => settingsHotKey;
            set
            {
                SetProperty(ref settingsHotKey, value);
                SettingsKeyStateInfo.SetHotKey(settingsHotKey);
            }
        }
        public string SnippetSelectionHotKey
        {
            get => snippetSelectionHotKey;
            set
            {
                SetProperty(ref snippetSelectionHotKey, value);
                SnippetSelectionKeyStateInfo.SetHotKey(snippetSelectionHotKey);
            }
        }
        public bool EnableXmlDocumentation { get; set; }
        public bool AutoCloseElements { get; set; }
        public bool DetectWordsInNames { get; set; }
        public bool SetZoom { get; set; }
        public double Zoom { get; set; }

        private string settingsHotKey;
        private string snippetSelectionHotKey;


        public BindingList<Snippet> Snippets { get; }
        public BindingList<SnippetVariable> Variables { get; }

        public SessionSettings()
        {
            Snippets = new BindingList<Snippet>();
            Variables = new BindingList<SnippetVariable>();
            SettingsKeyStateInfo = new KeyStateInfo();
            SnippetSelectionKeyStateInfo = new KeyStateInfo();
            Clear();
        }
        public SessionSettings(SessionSettings source) : this() => CopyFrom(source);

        public void Clear()
        {
            SnippetSelectionHotKey = "Ctrl+Shift+T";
            SettingsHotKey = "Ctrl+Shift+E";

            EnableXmlDocumentation = true;
            AutoCloseElements = false;
            DetectWordsInNames = true;
            SetZoom = false;
            Zoom = 100;
            Snippets.Clear();
            Variables.Clear();
        }

        public override void CopyFrom(SessionSettings source)
        {
            base.CopyFrom(source);

            Snippets.Clear();
            Variables.Clear();

            //append snippets
            foreach (var sourceSnippet in source.Snippets)
                Snippets.Add(new Snippet(sourceSnippet));

            //append variables
            foreach (var sourceVariable in source.Variables)
                Variables.Add(new SnippetVariable(sourceVariable));

        }

        public bool SaveSettings(string fileName, bool displayError)
        {
            try
            {
                using (var xmlWriter = XmlWriter.Create(fileName))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(SessionSettings));
                    serializer.Serialize(xmlWriter, this);
                }
            }
            catch (Exception e)
            {
                if (displayError)
                    MessageBox.Show(
                        $"Saving settings to file {fileName} failed. Error message: {e.Message}",
                        "Error",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);

                return false;
            }

            return true;
        }

        protected Snippet FindSnippet(string name) => Snippets.FirstOrDefault(s => s.Name == name);

        protected SnippetVariable FindVariable(string name) => Variables.FirstOrDefault(v => v.Name == name);

        public static SessionSettings LoadSettings(string fileName, bool createIfNotFound, bool displayError = false)
        {
            try
            {

                if (File.Exists(fileName))
                {
                    using (var xmlReader = XmlReader.Create(fileName))
                    {
                        var serializer = new XmlSerializer(typeof(SessionSettings));
                        var settings = (SessionSettings)serializer.Deserialize(xmlReader);

                        if (settings != null)
                            return settings;
                    }
                }
            }
            catch (Exception e)
            {
                if (displayError)
                    MessageBox.Show(
                        $"Settings file cannot be loaded. {e.Message}",
                        "Error",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
            }

            if (createIfNotFound)
                return new SessionSettings();

            return null;
        }

    }
}
