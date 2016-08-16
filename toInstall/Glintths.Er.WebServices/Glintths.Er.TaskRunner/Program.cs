using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glintths.Er.Task;
using System.IO;
using System.Xml.Serialization;
using CPCHS.GenericWindowsService.Configuration;
//using CPCHS.Common.Utilities;

namespace Glintths.Er.TaskRunner
{
    class Program
    {
       

        static void Main(string[] args)
        {
            // -------

           try
            {
                string file = Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), @"TasksConf.xml");
                FileStream fstream = new FileStream(file, FileMode.Open, FileAccess.Read);
                XmlSerializer s = new XmlSerializer(typeof(TasksConf));
                TasksConf taskConf = (TasksConf)s.Deserialize(fstream);
                fstream.Close();

               var companyDb = taskConf.TaskConfCollection[0].Companies.CompanyConfCollection[0].company;
               var config = Glintths.Er.Task.DownloadFilesTaskConf.Deserialize(taskConf.TaskConfCollection[0].Companies.CompanyConfCollection[0].Any.OuterXml);
                

                Console.WriteLine("*** START ***");

                TaskManager tasksmng = new TaskManager(taskConf);



            //    DownloadFilesTask teste = new DownloadFilesTask(companyDb, config.Configuration.ExternalDocumentsUri, config.Configuration.ExternalFilesUri);
             //   teste.ExtractFiles(jobId, appId, docType, patientId, patientType, username);
                
                Console.WriteLine("Press enter to close...");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            } 
        }
    }
}
