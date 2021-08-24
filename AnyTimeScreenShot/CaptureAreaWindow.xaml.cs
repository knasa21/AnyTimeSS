using System;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Interop;

namespace AnyTimeScreenShot
{
    /// <summary>
    /// CaptureAreaWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class CaptureAreaWindow : ATWindow
    {
        private CaptureAreaWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 初期化処理
        /// </summary>
        public override void Initialize()
        {
            MouseLeftButtonDown += ( _, __ ) => { DragMove(); };
            this.DataContext = WindowManager.GetSettingWindow();
        }

        private void Window_Close(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;
        }

        private void Window_StateChanged( object sender, EventArgs e )
        {
            Console.WriteLine($"Window State = {WindowState}");
            if(WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
            }

            // CurrentDisplay を取得してフルスクリーン時にそのサイズに設定する
            Screen currentScreen = Screen.FromHandle( new WindowInteropHelper( this ).Handle );
            this.Left = currentScreen.Bounds.X;
            this.Top = currentScreen.Bounds.Y;
            this.Width = currentScreen.Bounds.Width;
            this.Height = currentScreen.Bounds.Height;
        }
    }
}
