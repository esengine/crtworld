using Enterwell.Clients.Wpf.Notifications;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CRTWorldEditor.Windows
{
    /// <summary>
    /// StartupWindow.xaml 的交互逻辑
    /// </summary>
    public partial class StartupWindow : Window
    {
        public StartupWindow()
        {
            InitializeComponent();

            MessageContainer.Manager = new NotificationMessageManager();
            MessageContainer.Manager.CreateMessage()
               .Animates(true)
               .AnimationInDuration(0.75)
               .AnimationOutDuration(1)
               .Accent("#1751C3")
               .Background("#333")
               .HasBadge("信息")
               .HasMessage("欢迎使用CRTWorld编辑器")
               .WithAdditionalContent(ContentLocation.Bottom,
                   new Border
                   {
                       BorderThickness = new Thickness(0, 1, 0, 0),
                       BorderBrush = new SolidColorBrush(Color.FromArgb(128, 28, 28, 28)),
                       Child = new CheckBox
                       {
                           Margin = new Thickness(12, 8, 12, 8),
                           HorizontalAlignment = HorizontalAlignment.Left,
                           Content = "不再显示"
                       }
                   })
               .Dismiss()
               .WithButton("关闭", btn => { })
               .Queue();
        }
    }
}
