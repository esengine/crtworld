﻿using AvalonDock.Layout;
using AvalonDock.Themes;
using System.Windows;

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
