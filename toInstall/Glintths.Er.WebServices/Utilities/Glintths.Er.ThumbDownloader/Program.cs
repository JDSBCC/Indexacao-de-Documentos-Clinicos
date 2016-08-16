using System;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.IO;
using System.Linq;
using System.Text;

namespace Glintths.Er.ThumbDownloader
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = string.Empty;
            if (args != null && args.Length == 1)
            {
                connectionString = args[0];
            }
            else
            {
                Console.WriteLine("Provide the connectionString as a parameter on the console. Use quotation marks if you need spaces on the string");
                return;
            }
            Console.WriteLine("using connectionString: " + connectionString);

            try
            {
                Console.WriteLine("Start");
                DownloadAllImages(connectionString);
                Console.WriteLine("End");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }

        private static void DownloadAllImages(string connectionString)
        {
            if (!Directory.Exists(Configuration.Dir()))
            {
                Directory.CreateDirectory(Configuration.Dir());
            }

            OracleConnection con = new OracleConnection(connectionString);
            con.Open();
            OracleDataAdapter datadap = new OracleDataAdapter();

            datadap.SelectCommand = new OracleCommand("SELECT logotipo, aplicacao_id, tipo_documento_id " +
                                                        "FROM er_tipo_documento " +
                                                        "WHERE logotipo is not null ", con);
            OracleDataReader reader = datadap.SelectCommand.ExecuteReader();
            List<string> fileList = new List<string>();

            using (reader)
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        OracleLob blob = reader.GetOracleLob(0);
                        long appId = reader.GetInt64(1);
                        long docType = reader.GetInt64(2);
                        if (blob.Length != 0)
                        {
                            using (blob)
                            {
                                byte[] bytes = new byte[blob.Length];
                                blob.Read(bytes, 0, bytes.Length);
                                byte[] properThumb = ThumbGenerator.GetThumb(bytes);
                                string filename = Path.Combine(Configuration.Dir(), appId + "_" + docType + ".png");
                                FileStream fs = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write);
                                using (fs)
                                {
                                    fs.Write(properThumb, 0, properThumb.Length);
                                }

                                fileList.Add(filename);
                                Console.WriteLine("  " + filename);
                            }

                            //string filename = Path.Combine(Configuration.Dir, appId + "_" + docType + ".png");
                            //FileStream fs = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write);
                            //using (fs)
                            //{
                            //    byte[] bytes = new byte[1024 * 128];
                            //    int bytesRead;
                            //    while ((bytesRead = blob.Read(bytes, 0, bytes.Length)) > 0)
                            //    {
                            //        fs.Write(bytes, 0, bytesRead);
                            //    }
                            //    blob.Close();
                            //    blob.Dispose();                                
                            //}
                            //fileList.Add(filename);
                        }
                    }
                }
            }
            con.Close();
            con.Dispose();

            string[] existingFiles = Directory.GetFiles(Configuration.Dir(), "*.png", SearchOption.TopDirectoryOnly);
            foreach (var fi in existingFiles)
            {
                if (!fileList.Contains(fi) && !fi.StartsWith("sprite"))
                    File.Delete(fi);
            }

        }


    }


    public class Configuration
    {

        public static int ImageWidth = 67;
        public static int ImageHeight = 31;

        private static string dir = "output";


        //public static string conStr = "server=hsdev;uid=ERESULTS_V2_DEV;Password=ERESULTS_V2_DEV;";
        //public static string ConStr = "server=DEMOPRIV;uid=ERESULTS_V2_13R101;pwd=ERESULTS_V2_13R101";


        public static string Dir()
        {
            return Path.Combine(Directory.GetCurrentDirectory(), Configuration.dir);
        }

    }

    public class ThumbGenerator
    {
        public static byte[] GetThumb(byte[] imgBytes)
        {
            MemoryStream imgStream = new MemoryStream(imgBytes);

            System.Drawing.Image image = System.Drawing.Image.FromStream(imgStream);
            System.Drawing.Image thumbnailImage = image.GetThumbnailImage(Configuration.ImageWidth, Configuration.ImageHeight, new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback), IntPtr.Zero);

            MemoryStream thumbnailStream = new MemoryStream();

            thumbnailImage.Save(thumbnailStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            thumbnailStream.Position = 0;

            byte[] imageBytes = new byte[thumbnailStream.Length];
            thumbnailStream.Read(imageBytes, 0, (int)thumbnailStream.Length);

            imgStream.Dispose();
            image.Dispose();
            thumbnailImage.Dispose();
            thumbnailStream.Dispose();

            return imageBytes;
        }

        //necessário...
        public static bool ThumbnailCallback() { return true; }
    }
}
