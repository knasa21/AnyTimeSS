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

    static public class ATWindow
    {
        public static MainWindow GetSettingWindow()
        {
            return (MainWindow)WindowManager.Instance.GetWindow( WindowName.SETTING );
        }

        public static CaptureAreaWindow GetCaptureAreaWindow()
        {
            return (CaptureAreaWindow)WindowManager.Instance.GetWindow( WindowName.CAPTURE );
        }

        public static void ShowWindow(WindowName name)
        {
            WindowManager.Instance.ShowWindow(name);
        }

        public static void CloseWindow(WindowName name)
        {
            WindowManager.Instance.CloseWindow(name);
        }

        public static void SetVisible(WindowName name, Visibility visibility)
        {
            WindowManager.Instance.SetVisible(name, visibility);
        }
    }



    /// <summary>
    /// Windowを他クラスから統一してアクセスするためのクラス
    /// </summary>
    sealed class WindowManager
    {

        private static readonly Lazy<WindowManager> mInstance
            = new Lazy<WindowManager>(() => new WindowManager());

        private WindowManager()
        {
            mWindows = new Window[] {
                new MainWindow(),       // SETTING
                new CaptureAreaWindow() // CAPTURE
            };
        }

        public static WindowManager Instance
        {
            get => mInstance.Value;
        }

        private Window[] mWindows;

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
