using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyTimeScreenShotSystem
{
    public abstract class Singleton
    {
        protected static object mLockObj = new object();
        protected static Singleton mInstance;

        protected Singleton()
        {
        }

        public abstract Singleton Instance { get; }

    }
}
