using CommunityToolkit.Mvvm.Messaging;
using CRTWorldEditor.Datas;
using CRTWorldEditor.ViewModels;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CRTWorldEditor.Windows
{
    /// <summary>
    /// StartupWindow.xaml 的交互逻辑
    /// </summary>
    public partial class StartupWindow : Window, IRecipient<CreateProjectCompletedMessage>
    {
        public StartupWindow()
        {
            InitializeComponent();

            DataContext = new StartupViewModel();

            listBox.SelectionChanged += ListBox_SelectionChanged;
            listBox.SelectedIndex = 0;

            Task.Factory.StartNew(() => Thread.Sleep(1000)).ContinueWith(t =>
            {
                MainSnackbar.MessageQueue?.Enqueue("欢迎使用CRTWORLD编辑器");
            }, TaskScheduler.FromCurrentSynchronizationContext());


            WeakReferenceMessenger.Default.Register(this);
        }

        public void Receive(CreateProjectCompletedMessage message)
        {
            new MainWindow().Show();
            Hide();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = listBox.SelectedValue as ListBoxItem;
            if (item != null)
            {
                frame.Navigate(new Uri($"Pages/{item.Tag}.xaml", UriKind.Relative), this);
            }
        }
    }
}
