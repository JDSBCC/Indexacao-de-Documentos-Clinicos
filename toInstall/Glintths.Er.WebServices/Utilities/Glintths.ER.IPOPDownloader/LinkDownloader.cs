using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
//using System.Threading.Tasks;

namespace Glintths.Er.IPOPDownloader
{
    public static class Extensions
    {
        public static void CopyTo(this Stream src, Stream dest)
        {
            int size = (src.CanSeek) ? Math.Min((int)(src.Length - src.Position), 0x2000) : 0x2000;
            byte[] buffer = new byte[size];
            int n;
            do
            {
                n = src.Read(buffer, 0, buffer.Length);
                dest.Write(buffer, 0, n);
            } while (n != 0);
        }

        public static void CopyTo(this MemoryStream src, Stream dest)
        {
            dest.Write(src.GetBuffer(), (int)src.Position, (int)(src.Length - src.Position));
        }

        public static void CopyTo(this Stream src, MemoryStream dest)
        {
            if (src.CanSeek)
            {
                int pos = (int)dest.Position;
                int length = (int)(src.Length - src.Position) + pos;
                dest.SetLength(length);

                while (pos < length)
                    pos += src.Read(dest.GetBuffer(), pos, length - pos);
            }
            else
                src.CopyTo((Stream)dest);
        }
    }

    public class LinkDownloader
    {
        public void GetFileFromUri(string uri, System.Web.HttpResponse httpResponse)
        {
            //GetFileFromUri2(uri, httpResponse, null);
            //return;

            CookieContainer cookieJar = new CookieContainer();
            System.Net.HttpWebRequest myReq = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(uri);
            myReq.CookieContainer = cookieJar;
            myReq.Method = "GET";
            myReq.AllowAutoRedirect = true;
            myReq.Timeout = 1000000000;
            myReq.AllowWriteStreamBuffering = true;

            System.Net.HttpWebResponse response1 = (HttpWebResponse)myReq.GetResponse();
            response1.Cookies = cookieJar.GetCookies(new Uri(uri));
            StreamReader sr = new StreamReader(response1.GetResponseStream());

            StreamWriter sw1 = new StreamWriter(httpResponse.OutputStream);

            response1.Cookies = cookieJar.GetCookies(new Uri(uri));

            string myPageSource = sr.ReadToEnd();

            if (myPageSource.Contains("src="))
            {
                int startPosition = myPageSource.IndexOf("src=\"") + 5;
                int endPosition = myPageSource.IndexOf('\"', startPosition);
                
                string itemUri = myPageSource.Substring(startPosition, endPosition - startPosition);

                if (!string.IsNullOrEmpty(itemUri))
                {
                    endPosition = uri.IndexOf("redirecturl=") + 12;
                    string urlBase = uri.Substring(0, endPosition);

                    urlBase = urlBase + itemUri;

                    System.Net.HttpWebRequest myReq2 = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(urlBase);
                    myReq2.CookieContainer = cookieJar;
                    myReq2.Method = "GET";
                    myReq2.AllowAutoRedirect = true;
                    myReq2.Timeout = 1000000000;
                    myReq2.AllowWriteStreamBuffering = true;

                    System.Net.HttpWebResponse response2 = (HttpWebResponse)myReq2.GetResponse();
                    response2.Cookies = cookieJar.GetCookies(new Uri(uri));
                    httpResponse.ContentType = response2.Headers["content-type"];

                    StreamReader sr2 = new StreamReader(response2.GetResponseStream());

                    Extensions.CopyTo(response2.GetResponseStream(), httpResponse.OutputStream);

                    sw1.Flush();
                }
            }
        }


        public void GetFileFromUri2(string uri, System.Web.HttpResponse httpResponse, string encoding)
        {
            CookieContainer cookieJar = new CookieContainer();
            //System.Net.HttpWebRequest myReq = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(uri);
            //myReq.CookieContainer = cookieJar;
            //myReq.Method = "GET";
            //myReq.AllowAutoRedirect = true;
            //myReq.Timeout = 1000000000;
            //myReq.AllowWriteStreamBuffering = true;

            //System.Net.HttpWebResponse response1 = (HttpWebResponse)myReq.GetResponse();
            //response1.Cookies = cookieJar.GetCookies(new Uri(uri));
            //StreamReader sr = new StreamReader(response1.GetResponseStream());

            //StreamWriter sw1 = new StreamWriter(httpResponse.OutputStream);

            //response1.Cookies = cookieJar.GetCookies(new Uri(uri));

            //sw1.Write("Response Link " + response1.ResponseUri);

            //sw1.WriteLine("");
            //sw1.WriteLine("#########################################################");
            // NEW 
            //string myPageSource = sr.ReadToEnd();

            //if (myPageSource.Contains("src="))
            //{
                //sw1.Write("POTENTE!");

                //int startPosition = myPageSource.IndexOf("src=\"") + 5;
                //int endPosition = myPageSource.IndexOf('\"', startPosition);
                //string itemUri = "http://localhost/teste.html";
                //string itemUri = myPageSource.Substring(startPosition, endPosition - startPosition);

                //sw1.Write(itemUri);

                //if (!string.IsNullOrEmpty(itemUri))
                //{
                    //endPosition = uri.IndexOf("redirecturl=") + 12;
                    //string urlBase = uri.Substring(0, endPosition);

                    //string urlBase = "http://ipophisweb.ipoporto.min-saude.pt:7779/sgd/idcplg?cookieLogin=1&username=oasisipo&password=ora75nv3qds1x&redirecturl=http://ipophisweb.ipoporto.min-saude.pt:7779/sgd/groups/aca/sg/ipop/000/001/507/d/49-14-66924-1.pdf";
            string urlBase = "http://portalipop.ipoporto.min-saude.pt/userfiles/Introducao%20medicamentos_FH.pdf";
                    //string urlBase = "http://localhost/teste.html";
                    //sw1.Write(urlBase);

                    System.Net.HttpWebRequest myReq2 = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(urlBase);
                    myReq2.CookieContainer = cookieJar;
                    myReq2.Method = "GET";
                    myReq2.AllowAutoRedirect = true;
                    myReq2.Timeout = 1000000000;
                    myReq2.AllowWriteStreamBuffering = true;
                    System.Net.HttpWebResponse response2 = (HttpWebResponse)myReq2.GetResponse();
                    //response2.Cookies = cookieJar.GetCookies(new Uri(uri));
                    httpResponse.ContentType = response2.Headers["content-type"];
                    

                    
                    //sw1.WriteLine("");
                    //sw1.WriteLine("#########################################################");

                    //sw1.Write("response2: " + response2.CharacterSet);
                    //sw1.Write("httpResponse: " + httpResponse.Charset);
                    //sw1.Write("response2.Headers[content-type]: " + response2.Headers["content-type"]);

                    //sw1.WriteLine("#########################################################");
                    
                    //httpResponse.Charset = response2.CharacterSet;
                    
                    // Encoding.GetEncoding("ISO-8859-1")
                    // "AL32UTF8"

                    //sw1.WriteLine("Count Keys" + response2.Headers.Keys.Count);

                   //StreamReader sr2 = new StreamReader(response2.GetResponseStream(), true);

                    
                    //sw1.WriteLine("sr2.CurrentEncoding : " + sr2.CurrentEncoding);
                    string path = Path.Combine(@"D:\Glintths\Dev\PDS\Cpchs.Documents.Web.DataPresenter", "ficheiro.pdf");
                    //using (FileStream fsw = File.Create(path))
                    //{
                    Extensions.CopyTo(response2.GetResponseStream(), httpResponse.OutputStream);
                    //}
                    
                    

                    //sw1.WriteLine(sr2.ReadToEnd());
                    //sw1.Flush();
                //}
            //}

            //sw1.WriteLine(sr.ReadToEnd());
            //sw1.Flush();

            //return;
        }


        public static byte[] ReadToEnd(System.IO.Stream stream)
        {
            long originalPosition = 0;

            if(stream.CanSeek)
            {
                 originalPosition = stream.Position;
                 stream.Position = 0;
            }

            try
            {
                byte[] readBuffer = new byte[4096];

                int totalBytesRead = 0;
                int bytesRead;

                while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
                {
                    totalBytesRead += bytesRead;

                    if (totalBytesRead == readBuffer.Length)
                    {
                        int nextByte = stream.ReadByte();
                        if (nextByte != -1)
                        {
                            byte[] temp = new byte[readBuffer.Length * 2];
                            Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                            Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
                            readBuffer = temp;
                            totalBytesRead++;
                        }
                    }
                }

                byte[] buffer = readBuffer;
                if (readBuffer.Length != totalBytesRead)
                {
                    buffer = new byte[totalBytesRead];
                    Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
                }
                return buffer;
            }
            finally
            {
                if(stream.CanSeek)
                {
                     stream.Position = originalPosition; 
                }
            }
        }
    }
}
