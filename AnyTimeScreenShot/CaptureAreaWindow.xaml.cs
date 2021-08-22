﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AnyTimeScreenShot
{
    /// <summary>
    /// CaptureAreaWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class CaptureAreaWindow : Window
    {
        public CaptureAreaWindow()
        {
            InitializeComponent();
            MouseLeftButtonDown += ( _, __ ) => { DragMove(); };
        }

        private void Window_Close(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;
        }
    }
}
