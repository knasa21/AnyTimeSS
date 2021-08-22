using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyTimeScreenShot
{

    public class KeyWatcher
    {
        public delegate void EventOnPressKey();

        // 別スレッド管理用のTask
        private Task mTask;

        // 動作フラグ
        private bool mStopFlag = true;

        // GetAsyncKeyStateの押下確認用ビットマスク
        private const Int64 mMask64 = (Int64)0x8000;

        // キーが押された時のイベント
        public EventOnPressKey OnPressKey;

        [System.Runtime.InteropServices.DllImport( "user32.dll" )]
        private static extern IntPtr GetAsyncKeyState( IntPtr pKey );

        public KeyWatcher()
        {
            mStopFlag = true;
        }

        ~KeyWatcher()
        {
            Console.WriteLine("Destructer");
            WatchStop();
        }

        public void WatchStart()
        {
            Console.WriteLine("WatchStart");
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
            Console.WriteLine("WatchStop");
            mStopFlag = true;
            mTask = null;
        }

        private void WatchKey()
        {
            bool altFlag = false;
            bool f12Flag = false;
            bool shotFlag = false;
            bool releaseFlag = true;
            while(!mStopFlag)
            {
                altFlag = false;
                f12Flag = false;
                if( (GetAsyncKeyState((IntPtr)0x12).ToInt64() & mMask64) != 0 )
                {
                    altFlag = true;
                }

                if( (GetAsyncKeyState((IntPtr)0x7B).ToInt64() & mMask64) != 0 )
                {
                    f12Flag = true;
                }

                shotFlag = altFlag && f12Flag;
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
