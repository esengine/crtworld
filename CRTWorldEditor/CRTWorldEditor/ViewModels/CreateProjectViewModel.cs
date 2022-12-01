using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel.DataAnnotations;
using System.Windows.Forms;

namespace CRTWorldEditor.ViewModels
{
    public partial class CreateProjectViewModel : ObservableValidator
    {
        private string projectPath;

        [Required(AllowEmptyStrings = false, ErrorMessage = "此字段是必填字段，不得留空")]
        public string ProjectPath {
            get => projectPath;
            set => SetProperty(ref projectPath, value, true);
        }

        private string projectName;

        [Required(AllowEmptyStrings = false, ErrorMessage = "此字段是必填字段，不得留空")]
        public string ProjectName {
            get => projectName;
            set => SetProperty(ref projectName, value, true);
        }

        public CreateProjectViewModel() {
            ValidateAllProperties();
        }

        [RelayCommand]
        public void OnBrowserCommand() {
            var folderDialog = new FolderBrowserDialog();
            folderDialog.ShowNewFolderButton = true;
            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                ProjectPath = folderDialog.SelectedPath;
            }
        }
    }
}
