using System;

namespace CRTWorldEditor.Datas
{
    [Serializable]
    public class ProjectSettings
    {
        public const string name = "ProjectSettings.asset";
        public PlayerSettings playerSettings;

        public ProjectSettings() {
            playerSettings = new PlayerSettings();
        }
    }

    [Serializable]
    public class PlayerSettings {
        public string projectName;
    }
}
