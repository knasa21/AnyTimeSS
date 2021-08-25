﻿using System;
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
            mNotifyIcon = new NotifyIcon();

            mKeyWatcher = new KeyWatcher();
            mKeyWatcher.OnPressKey += ScreenShot;
            mKeyWatcher.WatchStart();

            WindowManager.GetSettingWindow().DataContext = mFileSaveViewModel;

            CaptureAreaWindow window = WindowManager.GetCaptureAreaWindow(); 
            window.DataContext = mFileSaveViewModel;
            window.SizeChanged += AdaptWindowSize;
            window.LocationChanged += AdaptWindowLocation;
        }

        private void AdaptWindowSize( object sender, SizeChangedEventArgs e )
        {
            MainWindow settingWindow = WindowManager.GetSettingWindow();
            CaptureAreaWindow areaWindow = WindowManager.GetCaptureAreaWindow();
            settingWindow.fCaptureW.Text = areaWindow.Width.ToString();
            settingWindow.fCaptureH.Text = areaWindow.Height.ToString();

            mCaptureRect.Width = (int)areaWindow.Width;
            mCaptureRect.Height = (int)areaWindow.Height;
        }

        private void AdaptWindowLocation( object sender, EventArgs e )
        {
            MainWindow settingWindow = WindowManager.GetSettingWindow();
            CaptureAreaWindow areaWindow = WindowManager.GetCaptureAreaWindow();
            settingWindow.fCaptureX.Text = areaWindow.Left.ToString();
            settingWindow.fCaptureY.Text = areaWindow.Top.ToString();

            mCaptureRect.X = (int)areaWindow.Left;
            mCaptureRect.Y = (int)areaWindow.Top;
        }

        protected override void OnExit( ExitEventArgs e )
        {
            mKeyWatcher.WatchStop();
            base.OnExit( e );
            mNotifyIcon.Dispose();
        }

        private void ScreenShot()
        {
            Console.WriteLine("Gooooood!!!!");
            ScreenCapture.Capture( mCaptureRect, $@"D:\Pictures\Capture\{mFileSaveViewModel.FileName}_{counter++}" );
        }

    }
}
