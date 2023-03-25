using System;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

namespace Gravity_Editor
{
    [Serializable]
    public class Settings
    {
        public int SelectedIndex;

        // Gravity Settings
        public bool GravityDisabled;
        public float Mass;
    }

    internal class SettingsManager
    {
        public static Settings Settings = new Settings();

        private static string FilePath = Application.dataPath + "/gravity_editor_crafterbot.xml";
        public static void Load()
        {
            if (!File.Exists(FilePath)) Save();

            StreamReader streamReader = new StreamReader(FilePath);
            XmlSerializer Xml = new XmlSerializer(typeof(Settings));
            Settings = (Gravity_Editor.Settings)Xml.Deserialize(streamReader);

            streamReader.Close();
        }

        public static void Save()
        {
            Stream stream = File.Create(FilePath);

            XmlSerializer Xml = new XmlSerializer(typeof(Settings));
            Xml.Serialize(stream, Settings);

            stream.Close();
        }

        public static void Reset()
        {
            Settings = new Settings();
            Save();
        }
    }
}
