using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CPCHS.GenericWindowsService.Configuration;
using CPCHS.GenericWindowsService.Interfaces;
using System.Runtime.Remoting;

namespace Glintths.Er.TaskRunner
{
    public class TaskManager
    {
        List<ITask> RunnigTaskList;

        public TaskManager(TasksConf tasksConf)
        {
            this.RunnigTaskList = new List<ITask>();

            foreach (TaskConf taskConf in tasksConf.TaskConfCollection)
            {
                foreach (CompanyConf compConf in taskConf.Companies)
                {
                    ITask task = GetTask(taskConf);

                    task.Start(compConf.company, compConf.poolingInterval, compConf.Any);
                    this.RunnigTaskList.Add(task);
                }
            }
        }

        public void Stop()
        {
            foreach (ITask task in this.RunnigTaskList)
            {
                task.Stop();
                while (task.IsRunning())
                {
                    Thread.Sleep(5000);
                }
            }
        }

        public static ITask GetTask(TaskConf taskConf)
        {
            return GetTypeThroughReflection(taskConf.taskNameSpace, taskConf.taskClass) as ITask;
        }

        private static object GetTypeThroughReflection(string assemblyNamespace, string typeName)
        {
            ObjectHandle obj = Activator.CreateInstance(assemblyNamespace, assemblyNamespace + "." + typeName);
            object absOp = obj.Unwrap() as object;
            return absOp;
        }

    }
}
