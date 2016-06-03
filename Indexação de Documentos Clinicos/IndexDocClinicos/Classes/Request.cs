using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Web;

namespace IndexDocClinicos.Classes
{
    public class Request
    {
        public static JObject data;
        public static string dataXML;

        public static void Get(string url, string queries, string token)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url+"?"+queries);
            request.Headers.Add("Authorization", "Bearer " + token);

            var response = (HttpWebResponse)request.GetResponse();

            string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            data = JObject.Parse(responseString);
        }

        public static void Post(string url, string queries, int tryNum)
        {
            int newTryNum = 0;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                var content = Encoding.ASCII.GetBytes(queries);

                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = content.Length;

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(content, 0, content.Length);
                    stream.Close();
                }
                var response = (HttpWebResponse)request.GetResponse();

                string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                data = JObject.Parse(responseString);
            } catch (WebException ex) {
                newTryNum = tryNum + 1;
                if (tryNum <= 5) {
                    Thread.Sleep(5000);
                    Post(url, queries, newTryNum);
                } else {
                    Debug.WriteLine("Não foi possível executar este pedido. Erro: " + ex);
                }
            }
        }

        public static void Post(string url, string queries, string token, string accept, int tryNum)
        {
            int newTryNum = 0;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Headers.Add("Authorization", "Bearer " + token);
                request.Accept = accept;

                var content = Encoding.ASCII.GetBytes(queries);

                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = content.Length;

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(content, 0, content.Length);
                    stream.Close();
                }
                var response = (HttpWebResponse)request.GetResponse();

                string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                data = JObject.Parse(responseString);
            } catch (WebException ex) {
                newTryNum = tryNum + 1;
                if (tryNum <= 5) {
                    Thread.Sleep(5000);
                    Post(url, queries, token, accept, newTryNum);
                } else {
                    Debug.WriteLine("Não foi possível executar este pedido. Erro: " + ex);
                }
            }
        }

        public static void Post(string url, string queries, string token, string accept, string body, int tryNum)
        {
            int newTryNum = 0;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + "?" + queries);
                request.Headers.Add("Authorization", "Bearer " + token);
                request.Accept = accept;

                var content = Encoding.UTF8.GetBytes(body);

                request.Method = "POST";
                request.ContentType = "text/xml";
                request.ContentLength = content.Length;

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(content, 0, content.Length);
                    stream.Close();
                }
                var response = (HttpWebResponse)request.GetResponse();

                string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                dataXML = responseString;
            } catch (WebException ex) {
                newTryNum = tryNum + 1;
                if (tryNum <= 5) {
                    Thread.Sleep(5000);
                    Post(url, queries, token, accept, body, newTryNum);
                } else {
                    Debug.WriteLine("Não foi possível executar este pedido. Erro: " + ex);
                }
            }
        }
    }
}