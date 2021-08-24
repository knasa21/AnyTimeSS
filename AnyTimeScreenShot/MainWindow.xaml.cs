using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Drawing;
using Xceed.Wpf.Toolkit;

namespace AnyTimeScreenShot
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : ATWindow
    {
        public delegate void FileNameChange( string fileName );
        private FileNameChange mOnFileNameChange;
        private bool isInputUI = true;
        

        private MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 初期化処理
        /// </summary>
        public override void Initialize()
        {
        }

        private void CheckRect()
        {
            if(isInputUI)
            {
            }
            isInputUI = true;
        }

        private void TextBoxNumberValidation( object sender, TextCompositionEventArgs e )
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Window_Close(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;
        }

        private void Button_Click_View( object sender, RoutedEventArgs e )
        {
            WindowManager.SetVisible( WindowName.CAPTURE, Visibility.Visible );
        }

        private void Button_Click_Close( object sender, RoutedEventArgs e )
        {
            WindowManager.SetVisible( WindowName.CAPTURE, Visibility.Hidden );
        }

        private void FCaptureX_ValueChanged( object sender, RoutedPropertyChangedEventArgs<object> e )
        {
            SingleUpDown form = sender as SingleUpDown;
            if(form != null)
            {
                CaptureAreaWindow window = WindowManager.GetCaptureAreaWindow();
                int value = Int32.Parse(form.Text);
                switch ( form.Name )
                {
                    case "fCaptureX":
                        window.Left = value;
                        break;
                    case "fCaptureY":
                        window.Top = value;
                        break;
                    case "fCaptureW":
                        window.Width = value;
                        break;
                    case "fCaptureH":
                        //window.Height = value;
                        break;
                }

            }
        }

        private void FFileName_TextChanged( object sender, TextChangedEventArgs e )
        {
            TextBox form = sender as TextBox;
        }
    }
}
