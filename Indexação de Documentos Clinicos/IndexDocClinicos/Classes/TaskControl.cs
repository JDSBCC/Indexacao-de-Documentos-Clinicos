using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace IndexDocClinicos.Classes
{
    public class TaskControl
    {
        private static Semaphore semaphoreDB;
        private static Semaphore semaphoreEHR;

        public TaskControl()
        {
            semaphoreDB = new Semaphore(1, 1);
            semaphoreEHR = new Semaphore(20, 20);
        }

        public static void waitDB(){
            semaphoreDB.WaitOne();
        }

        public static void releaseDB(){
            semaphoreDB.Release();
        }

        public static void waitEHR()
        {
            semaphoreEHR.WaitOne();
        }

        public static void releaseEHR()
        {
            semaphoreEHR.Release();
        }
    }
}