using CommunityToolkit.Mvvm.ComponentModel;
using CRTWorldEditor.Core;
using CRTWorldEditor.Datas;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace CRTWorldEditor.ViewModels
{
    public partial class ProjectViewModel : ObservableRecipient
    {
        [ObservableProperty]
        public ObservableCollection<ProjectViewData> projectViewDatas = new ObservableCollection<ProjectViewData>();

        public ProjectViewModel() {
            var recentProjects = AppSettings.Default.recentProjects;
            if (recentProjects == null)
                return;

            foreach (var recentProject in recentProjects)
            {
                if (recentProject == null)
                    continue;

                var projectSettings = CrtApp.ReadProject(recentProject);
                if (projectSettings == null)
                    continue;

                if (projectSettings.playerSettings == null)
                    continue;

                var data = new ProjectViewData
                {
                    Name = projectSettings.playerSettings.projectName,
                    Platform = projectSettings.playerSettings.platform.ToString(),
                    LastOpenTime = projectSettings.playerSettings.lastOpenedTime.ToString()
                };
                projectViewDatas.Add(data);
            }
        }
    }
}
