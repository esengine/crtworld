using CRTWorldEditor.Datas;
using CRTWorldEditor.Pages;
using Newtonsoft.Json;
using System;
using System.IO;

namespace CRTWorldEditor.Core
{
    public class CrtApp
    {
        public static ProjectSettings? ProjectSettings;

        public static bool LoadProject(string path) {
            try
            {
                // 加载projectSettings
                var projectSettings = Path.Combine(path, "ProjectSettings", ProjectSettings.name);
                if (!File.Exists(projectSettings))
                    return false;

                ProjectSettings = JsonConvert.DeserializeObject<ProjectSettings>(File.ReadAllText(projectSettings));

                return true;
            }
            catch {
                return false;
            }
        }

        public static bool CreateProject(string name, string path) {
            try
            {
                var projectSettingsFolder = Path.Combine(path, "ProjectSettings");
                Directory.CreateDirectory(projectSettingsFolder);

                var projectSettings = new ProjectSettings();
                projectSettings.playerSettings.projectName = name;
                projectSettings.playerSettings.platform = TargetPlatform.Windows;
                projectSettings.playerSettings.lastOpenedTime = DateTime.Now;
                using (var streamWriter = new StreamWriter(Path.Combine(projectSettingsFolder, ProjectSettings.name)))
                {
                    streamWriter.Write(JsonConvert.SerializeObject(projectSettings));
                }

                return true;
            }
            catch {
                return false;
            }
        }

        public static ProjectSettings? ReadProject(string path) {
            var projectSettingFile = Path.Combine(path, "ProjectSettings", ProjectSettings.name);
            if (!File.Exists(projectSettingFile)) {
                return null;
            }

            try
            {
                using (var streamReader = new StreamReader(projectSettingFile))
                {
                    return JsonConvert.DeserializeObject<ProjectSettings>(streamReader.ReadToEnd());
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
