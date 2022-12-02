using CommunityToolkit.Mvvm.Messaging;
using CRTWorldEditor.Core;
using CRTWorldEditor.ViewModels;
using CRTWorldEditor.Windows;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Navigation;

namespace CRTWorldEditor.Pages
{
    /// <summary>
    /// ProjectPage.xaml 的交互逻辑
    /// </summary>
    public partial class ProjectPage : Page
    {
        public ProjectPage()
        {
            InitializeComponent();

            DataContext = new ProjectViewModel();
        }

        private void OnCreateProjectClick(object sender, RoutedEventArgs e)
        {
            var result = new CreateProjectWindow().ShowDialog();
            if (result == true) {
                
            }
        }

        private void OnLoadProjectClick(object sender, RoutedEventArgs e)
        {
            var folderDialog = new FolderBrowserDialog();
            if (folderDialog.ShowDialog() == DialogResult.OK) {
                if (CrtApp.LoadProject(folderDialog.SelectedPath)) {
                    new MainWindow().Show();
                }
            }
        }
    }
}
