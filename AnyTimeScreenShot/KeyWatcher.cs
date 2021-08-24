using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyTimeScreenShot
{

    public class KeyWatcher
    {
        // キーが押された時のイベント
        public delegate void EventOnPressKey();
        public EventOnPressKey OnPressKey;

        // 別スレッド管理用のTask
        private Task mTask;

        // 動作フラグ
        private bool mStopFlag = true;

        // GetAsyncKeyStateの押下確認用ビットマスク
        private const Int64 mMask64 = (Int64)0x8000;

        [System.Runtime.InteropServices.DllImport( "user32.dll" )]
        private static extern short GetAsyncKeyState( int pKey );

        public KeyWatcher()
        {
            mStopFlag = true;
        }

        ~KeyWatcher()
        {
            WatchStop();
        }

        public void WatchStart()
        {
            if ( mTask != null && mTask.Status == TaskStatus.Running )
            {
                return;
            }

            mStopFlag = false;
            mTask = Task.Run( () =>
            {
                WatchKey();
            } );
        }

        public void WatchStop()
        {
            mStopFlag = true;
            mTask = null;
        }

        // TODO: どこからもらえないか、無ければちゃんと用意
        const int LShiftKey = 0xA0;
        const int RShiftKey = 0xA1;
        const int LCtrlKey = 0xA2;
        const int RCtrlKey = 0xA3;
        const int AltKey = 0x12;
        const int F12Key = 0x7B;

        private void WatchKey()
        {
            bool keyOneFlag = false;
            bool keyTwoFlag = false;
            bool shotFlag = false;
            bool releaseFlag = true;

            while(!mStopFlag)
            {
                keyOneFlag = false;
                keyTwoFlag = false;
                if( (GetAsyncKeyState(LShiftKey) & mMask64) != 0 )
                {
                    keyOneFlag = true;
                }

                if( (GetAsyncKeyState(LCtrlKey) & mMask64) != 0 )
                {
                    keyTwoFlag = true;
                }

                shotFlag = keyOneFlag && keyTwoFlag;
                if(releaseFlag == false)
                {
                    releaseFlag = !shotFlag;
                }

                if(shotFlag && releaseFlag)
                {
                    shotFlag = false;
                    releaseFlag = false;

                    OnPressKey();
                }
            }
        }
    }
}
