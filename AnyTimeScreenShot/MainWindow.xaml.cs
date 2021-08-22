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
    public partial class MainWindow : Window
    {
        private Rectangle mCaptureRect = new Rectangle( 0, 0, 1920, 1080);

        private CaptureAreaWindow mCaptureWindow;
        private bool isInputUI = true;

        public Rectangle GetRectangle() { return mCaptureRect; }

        public int RectX
        {
            get { return mCaptureRect.X; }
            set
            {
                mCaptureRect.X = value;
                mCaptureWindow.Left = mCaptureRect.X;
            }
        }
        public int RectY
        {
            get { return mCaptureRect.Y; }
            set
            {
                mCaptureRect.Y = value;
                mCaptureWindow.Top = mCaptureRect.Y;
            }
        }
        public int RectW
        {
            get { return mCaptureRect.Width; }
            set
            {
                mCaptureRect.Width = value;
                mCaptureWindow.Width = mCaptureRect.Width;
            }
        }
        public int RectH
        {
            get { return mCaptureRect.Height; }
            set
            {
                mCaptureRect.Height = value;
                mCaptureWindow.Height = mCaptureRect.Height;
            }
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
            fCaptureW.Text = mCaptureWindow.Width.ToString();
            fCaptureH.Text = mCaptureWindow.Height.ToString();
        }

        private void AdaptWindowLocation()
        {
            fCaptureX.Text = mCaptureWindow.Left.ToString();
            fCaptureY.Text = mCaptureWindow.Top.ToString();
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


        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            mCaptureWindow = new CaptureAreaWindow();
            mCaptureWindow.Show();
            mCaptureWindow.Visibility = Visibility.Hidden;
            mCaptureWindow.SizeChanged += OnWindowSizeChanged;
            mCaptureWindow.LocationChanged += OnWindowLocationChanged;
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
            mCaptureWindow.Visibility = Visibility.Visible;
            AdaptWindowSize();
            AdaptWindowLocation();
        }

        private void Button_Click_Close( object sender, RoutedEventArgs e )
        {
            mCaptureWindow.Visibility = Visibility.Hidden;
        }

        private void FCaptureX_ValueChanged( object sender, RoutedPropertyChangedEventArgs<object> e )
        {
            SingleUpDown form = sender as SingleUpDown;
            if(form != null)
            {
                Console.WriteLine($"{form.Name} = {form.Text}");
            }
        }
    }
}
