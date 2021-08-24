using System;
using System.Windows;
using AnyTimeScreenShotSystem;

namespace AnyTimeScreenShot
{
    public enum WindowName
    {
        SETTING,
        CAPTURE,
    }

    static public class WindowManager
    {
        public static MainWindow GetSettingWindow()
        {
            return (MainWindow)WindowManagerInternal.Instance.GetWindow( WindowName.SETTING );
        }

        public static CaptureAreaWindow GetCaptureAreaWindow()
        {
            return (CaptureAreaWindow)WindowManagerInternal.Instance.GetWindow( WindowName.CAPTURE );
        }

        public static void ShowWindow(WindowName name)
        {
            WindowManagerInternal.Instance.ShowWindow(name);
        }

        public static void CloseWindow(WindowName name)
        {
            WindowManagerInternal.Instance.CloseWindow(name);
        }

        public static void SetVisible(WindowName name, Visibility visibility)
        {
            WindowManagerInternal.Instance.SetVisible(name, visibility);
        }
    }



    /// <summary>
    /// Windowを他クラスから統一してアクセスするためのクラス
    /// </summary>
    sealed class WindowManagerInternal
    {

        private static WindowManagerInternal mInstance;
        private static readonly object mLockObj = new object();

        private static ATWindow[] mWindows;

        private WindowManagerInternal()
        {
            mWindows = new ATWindow[] {
                (ATWindow)Activator.CreateInstance(typeof(MainWindow), true),          // SETTING
                (ATWindow)Activator.CreateInstance(typeof(CaptureAreaWindow), true),   // CAPTURE
            };
        }

        private static void WindowInitialize()
        {
            foreach(var window in mWindows)
            {
                window.Initialize();
            }
        }

        public static WindowManagerInternal Instance
        {
            get
            {
                if ( mInstance == null )
                {
                    lock ( mLockObj )
                    {

                        if ( mInstance == null )
                        {
                            mInstance = new WindowManagerInternal();
                            WindowInitialize();
                        }
                    }
                }
                return mInstance;
            }
        }

        public Window GetWindow(WindowName name)
        {
            return mWindows[(int)name];
        }

        public void ShowWindow(WindowName name)
        {
            GetWindow( name ).Show();
        }

        public void CloseWindow(WindowName name)
        {
            GetWindow( name ).Close();
        }

        /// <summary>
        /// 表示切替
        /// </summary>
        /// <param name="name"></param>
        /// <param name="visibility"></param>
        public void SetVisible(WindowName name, Visibility visibility)
        {
            Window window = GetWindow(name);
            window.Visibility = visibility;
        }

    }
}
