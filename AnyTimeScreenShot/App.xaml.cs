using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;

namespace AnyTimeScreenShot
{
    public class FileSaveViewMode : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged( [CallerMemberName]string propertyName = null )
            => PropertyChanged?.Invoke( this, new PropertyChangedEventArgs(propertyName) );

        string mFileName;

        public string FileName
        {
            get { return mFileName; }
            set
            {
                if ( mFileName != value ) { mFileName = value; RaisePropertyChanged(); }
            }
        }

        string mFolderPath = System.Environment.GetFolderPath( Environment.SpecialFolder.MyPictures );
        public string FolderPath
        {
            get { return mFolderPath; }
            set
            {
                if ( mFolderPath != value ) { mFolderPath = value; RaisePropertyChanged(); }
            }
        }

        bool mWithTimeStamp = true;
        public bool WithTimeStamp
        {
            get { return mWithTimeStamp; }
            set
            {
                if ( mWithTimeStamp != value ) { mWithTimeStamp = value; RaisePropertyChanged(); }
            }
        }

    }

    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        private NotifyIcon mNotifyIcon;
        private int counter = 0;
        private FileSaveViewMode mFileSaveViewModel = new FileSaveViewMode();

        KeyWatcher mKeyWatcher;
        private Rectangle mCaptureRect = new Rectangle( 0, 0, 400, 600);

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

        /// <summary>
        /// スタートアップイベント
        /// </summary>
        /// <param name="e"></param>
        protected override void OnStartup( StartupEventArgs e )
        {
            base.OnStartup( e );
            ShutdownMode = ShutdownMode.OnExplicitShutdown;

            // タスクアイコン
            mNotifyIcon = new NotifyIcon();

            // キー監視スレッド
            mKeyWatcher = new KeyWatcher();
            mKeyWatcher.OnPressKey += ScreenShot;
            mKeyWatcher.WatchStart();

            // 値のバインド
            WindowManager.GetSettingWindow().DataContext = mFileSaveViewModel;

            // キャプチャウィンドウにイベント設定
            CaptureAreaWindow window = WindowManager.GetCaptureAreaWindow(); 
            window.DataContext = mFileSaveViewModel;
            window.SizeChanged += AdaptWindowSize;
            window.LocationChanged += AdaptWindowLocation;

            // 起動時オプション開く
            WindowManager.ShowWindow( WindowName.SETTING );

            // 起動時にキャプチャウィンドウを表示して値取得
            WindowManager.ShowWindow( WindowName.CAPTURE );
            AdaptWindowSize( null, null );
            AdaptWindowLocation( null, null );
            // すぐ閉じる
            WindowManager.SetVisible( WindowName.CAPTURE, Visibility.Hidden );
        }

        /// <summary>
        /// ウィンドウサイズを反映
        /// </summary>
        private void AdaptWindowSize( object sender, SizeChangedEventArgs e )
        {
            MainWindow settingWindow = WindowManager.GetSettingWindow();
            CaptureAreaWindow areaWindow = WindowManager.GetCaptureAreaWindow();
            settingWindow.fCaptureW.Text = areaWindow.Width.ToString();
            settingWindow.fCaptureH.Text = areaWindow.Height.ToString();

            mCaptureRect.Width = (int)areaWindow.Width;
            mCaptureRect.Height = (int)areaWindow.Height;
        }

        /// <summary>
        /// ウィンドウ位置を反映
        /// </summary>
        private void AdaptWindowLocation( object sender, EventArgs e )
        {
            MainWindow settingWindow = WindowManager.GetSettingWindow();
            CaptureAreaWindow areaWindow = WindowManager.GetCaptureAreaWindow();
            settingWindow.fCaptureX.Text = areaWindow.Left.ToString();
            settingWindow.fCaptureY.Text = areaWindow.Top.ToString();

            mCaptureRect.X = (int)areaWindow.Left;
            mCaptureRect.Y = (int)areaWindow.Top;
        }

        /// <summary>
        /// OnExit
        /// </summary>
        protected override void OnExit( ExitEventArgs e )
        {
            mKeyWatcher.WatchStop();
            base.OnExit( e );
            mNotifyIcon.Dispose();
        }

        /// <summary>
        /// スクリーンショット
        /// </summary>
        private void ScreenShot()
        {
            string fileName = mFileSaveViewModel.FileName;
            string folderPath = mFileSaveViewModel.FolderPath;
            if(mFileSaveViewModel.WithTimeStamp)
            {
                fileName += GetTimeStamp();
            }
            ScreenCapture.Capture( mCaptureRect, $@"{folderPath}\{fileName}" );
        }

        private string GetTimeStamp()
        {
            DateTime date = DateTime.Now;

            return date.ToString( "yyyyMMdd_HHmmssfff" );
        }
    }
}
