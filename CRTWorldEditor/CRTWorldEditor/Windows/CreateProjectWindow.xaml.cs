using CommunityToolkit.Mvvm.Messaging;
using CRTWorldEditor.Datas;
using CRTWorldEditor.ViewModels;
using System.Windows;

namespace CRTWorldEditor.Windows
{
    /// <summary>
    /// CreateProjectWindow.xaml 的交互逻辑
    /// </summary>
    public partial class CreateProjectWindow : Window, IRecipient<CreateProjectCompletedMessage>
    {
        public CreateProjectWindow()
        {
            InitializeComponent();

            DataContext = new CreateProjectViewModel();
            projectTypeList.SelectedIndex = 0;

            WeakReferenceMessenger.Default.Register(this);
        }

        public void Receive(CreateProjectCompletedMessage message)
        {
            Close();
        }
    }
}
