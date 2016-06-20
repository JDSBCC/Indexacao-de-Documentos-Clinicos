using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;

namespace IndexDocClinicos.Classes
{
    public class Request
    {
        public static JObject data;
        public static string dataXML;

        public static void Get(string url, string queries, string token)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url+"?"+queries);
                request.Headers.Add("Authorization", "Bearer " + token);

                var response = (HttpWebResponse)request.GetResponse();

                string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                data = JObject.Parse(responseString);
            } catch (WebException ex) {
                if (ex.Status == WebExceptionStatus.Timeout) {
                    Get(url, queries, token);
                } else {
                    Debug.WriteLine("Não foi possível executar este pedido. Erro: " + ex);
                    Debug.WriteLine("url=" + url + "|queries=" + queries);
                }
                //Get(url, queries, token);
            }
        }

        public static void Post(string url, string queries)
        {
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
                if (ex.Status == WebExceptionStatus.Timeout) {
                    Post(url, queries);
                } else {
                    Debug.WriteLine("Não foi possível executar este pedido. Erro: " + ex);
                    Debug.WriteLine("url=" + url + "|queries=" + queries);
                }
                //Post(url, queries);
            }
        }

        public static void Post(string url, string queries, string token, string accept)
        {
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
                if (ex.Status == WebExceptionStatus.Timeout) {
                    Post(url, queries, token, accept);
                } else {
                    Debug.WriteLine("Não foi possível executar este pedido. Erro: " + ex);
                    Debug.WriteLine("url=" + url + "|queries=" + queries + "|accept=" + accept);
                }
                //Post(url, queries, token, accept);
            }
        }

        public static void Post(string url, string queries, string token, string accept, string body)
        {
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
                if (ex.Status == WebExceptionStatus.Timeout) {
                    Post(url, queries, token, accept, body);
                } else {
                    Debug.WriteLine("Não foi possível executar este pedido. Erro: " + ex);
                    Debug.WriteLine("url="+ url + "|queries="+queries+"|accept="+accept+"|body="+body);
                }
                //Post(url, queries, token, accept, body);
            }
        }
    }
}