using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AnyTimeScreenShot
{
    /// <summary>
    /// タスクトレイアイコン
    /// </summary>
    public partial class NotifyIcon : Component
    {
        public NotifyIcon()
        {
            InitializeComponent();

            // イベント設定
            toolStripMenuItem_Settings.Click += toolStripMenuItem_Settings_Click;
            toolStripMenuItem_Exit.Click += toolStripMenuItem_Exit_Click;
        }

        public NotifyIcon( IContainer container )
        {
            container.Add( this );

            InitializeComponent();
        }

        /// <summary>
        /// コンテキストメニュー Settingsクリック時のイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem_Settings_Click(object sender, EventArgs e)
        {
            OpenSettingsWindow();
        }

        /// <summary>
        /// コンテキストメニュー Exitクリック時のイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem_Exit_Click(object sender, EventArgs e)
        {
            ExitApplication();
        }

        /// <summary>
        /// 設定ウィンドウ表示
        /// </summary>
        private void OpenSettingsWindow()
        {
            WindowManager.SetVisible( WindowName.SETTING, Visibility.Visible );
        }

        /// <summary>
        /// アプリケーション終了
        /// </summary>
        private void ExitApplication()
        {
            Application.Current.Shutdown();
        }

        private void contextMenuStrip1_Opening( object sender, CancelEventArgs e )
        {

        }
    }
}
