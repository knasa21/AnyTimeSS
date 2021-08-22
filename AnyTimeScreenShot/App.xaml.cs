using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace AnyTimeScreenShot
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        private NotifyIcon mNotifyIcon;
        KeyWatcher mKeyWatcher;

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
        }

    }
}
