using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace IndexDocClinicos.Models
{
    public class Countdown : IDisposable
    {
        private readonly ManualResetEvent done;
        private volatile int current;

        public Countdown(int total)
        {
            current = total;
            done = new ManualResetEvent(false);
        }

        public void Signal()
        {
            lock (done)
            {
                if (current > 0 && --current == 0)
                    done.Set();
            }
        }

        public void Wait()
        {
            done.WaitOne();
        }

        public void Dispose()
        {
            ((IDisposable)done).Dispose();
        }
    }
}