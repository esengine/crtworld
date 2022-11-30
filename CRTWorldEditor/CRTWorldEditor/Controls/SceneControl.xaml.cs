using CRTWorldEditor.Core;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using Panel = System.Windows.Forms.Panel;

namespace CRTWorldEditor.Controls
{
    /// <summary>
    /// SceneControl.xaml 的交互逻辑
    /// </summary>
    public partial class SceneControl : UserControl
    {
        private AppContainer container;

        public SceneControl()
        {
            InitializeComponent();

            var unityHost = new Panel();
            Host.Child = unityHost;

            var exePath = Path.Combine(Environment.CurrentDirectory, "unity", "CRTWorld.exe");
            if (!File.Exists(exePath) )
                return;

            container = new AppContainer(unityHost);
            container.StartAndEmbedProcess(exePath);
        }
    }
}
