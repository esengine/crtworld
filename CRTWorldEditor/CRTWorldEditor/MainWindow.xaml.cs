using AvalonDock.Layout;
using AvalonDock.Themes;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CRTWorldEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            dockingManager.Theme = new Vs2013DarkTheme();
        }
    }
}
