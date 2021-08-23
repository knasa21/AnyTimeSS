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
        private Rectangle mCaptureRect = new Rectangle( 0, 0, 1920, 1080);

        private bool isInputUI = true;

        public Rectangle GetRectangle() { return mCaptureRect; }

        public int RectX
        {
            get { return mCaptureRect.X; }
            set
            {
                mCaptureRect.X = value;

                CaptureAreaWindow window = WindowManager.GetCaptureAreaWindow();
                window.Left = mCaptureRect.X;
            }
        }
        public int RectY
        {
            get { return mCaptureRect.Y; }
            set
            {
                mCaptureRect.Y = value;

                CaptureAreaWindow window = WindowManager.GetCaptureAreaWindow();
                window.Top = mCaptureRect.Y;
            }
        }
        public int RectW
        {
            get { return mCaptureRect.Width; }
            set
            {
                mCaptureRect.Width = value;

                CaptureAreaWindow window = WindowManager.GetCaptureAreaWindow();
                window.Width = mCaptureRect.Width;
            }
        }
        public int RectH
        {
            get { return mCaptureRect.Height; }
            set
            {
                mCaptureRect.Height = value;

                CaptureAreaWindow window = WindowManager.GetCaptureAreaWindow();
                window.Height = mCaptureRect.Height;
            }
        }


        private MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 初期化処理
        /// </summary>
        public override void Initialize()
        {
            this.DataContext = this;
            CaptureAreaWindow window = WindowManager.GetCaptureAreaWindow(); 
            window.SizeChanged += OnWindowSizeChanged;
            window.LocationChanged += OnWindowLocationChanged;
        }

        private void CheckRect()
        {
            if(isInputUI)
            {
                Console.WriteLine( $"{mCaptureRect.ToString()}" );
            }
            isInputUI = true;
        }

        private void AdaptWindowSize()
        {
            CaptureAreaWindow window = WindowManager.GetCaptureAreaWindow();
            fCaptureW.Text = window.Width.ToString();
            fCaptureH.Text = window.Height.ToString();
        }

        private void AdaptWindowLocation()
        {
            CaptureAreaWindow window = WindowManager.GetCaptureAreaWindow();
            fCaptureX.Text = window.Left.ToString();
            fCaptureY.Text = window.Top.ToString();
        }

        /// <summary>
        /// Windowサイズが変更された時のイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowSizeChanged( object sender, SizeChangedEventArgs e )
        {
            AdaptWindowSize();
        }

        /// <summary>
        /// Windowの位置が変更された時のイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowLocationChanged( object sender, EventArgs e )
        {
            AdaptWindowLocation();
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
            AdaptWindowSize();
            AdaptWindowLocation();
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
                        window.Height = value;
                        break;
                }

            }
        }
    }
}
