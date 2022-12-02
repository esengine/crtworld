using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CRTWorldEditor.Core;
using CRTWorldEditor.Datas;
using Newtonsoft.Json;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace CRTWorldEditor.ViewModels
{
    public partial class CreateProjectViewModel : ObservableValidator
    {
        private string? projectPath;

        [Required(AllowEmptyStrings = false, ErrorMessage = "此字段是必填字段，不得留空")]
        public string? ProjectPath {
            get => projectPath;
            set => SetProperty(ref projectPath, value, true);
        }

        private string? projectName;

        [Required(AllowEmptyStrings = false, ErrorMessage = "此字段是必填字段，不得留空")]
        public string? ProjectName {
            get => projectName;
            set => SetProperty(ref projectName, value, true);
        }

        public ICommand OnBrowserCommand { get; }
        public ICommand OnCreateProjectCommand { get; }

        public CreateProjectViewModel() {
            OnBrowserCommand = new RelayCommand(OnBrowser);
            OnCreateProjectCommand = new RelayCommand(OnCreateProject);

            ValidateAllProperties();
        }

        public void OnBrowser() {
            var folderDialog = new FolderBrowserDialog();
            folderDialog.ShowNewFolderButton = true;
            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                ProjectPath = folderDialog.SelectedPath;
            }
        }

        public void OnCreateProject()
        {
            if (string.IsNullOrEmpty(ProjectName) || string.IsNullOrEmpty(ProjectPath))
                return;

            if (!Directory.Exists(ProjectPath))
            {
                return;
            }

            if (CrtApp.CreateProject(ProjectName, ProjectPath)) {

                if (AppSettings.Default["recentProjects"] == null) {
                    AppSettings.Default["recentProjects"] = new StringCollection();
                }
                AppSettings.Default.recentProjects.Add(ProjectPath);
                AppSettings.Default.Save();
                WeakReferenceMessenger.Default.Send(new CreateProjectCompletedMessage());
            }
        }
    }
}
