using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CRTWorldEditor.Windows
{
    /// <summary>
    /// CreateProjectWindow.xaml 的交互逻辑
    /// </summary>
    public partial class CreateProjectWindow : Window
    {
        public CreateProjectWindow()
        {
            InitializeComponent();

            projectTypeList.SelectedIndex = 0;
        }

        private void OnBrowserClick(object sender, RoutedEventArgs e)
        {
            var folderDialog = new FolderBrowserDialog();
            folderDialog.ShowNewFolderButton = true;
            if (folderDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                projectPath.Text = folderDialog.SelectedPath;
            }
        }
    }
}
