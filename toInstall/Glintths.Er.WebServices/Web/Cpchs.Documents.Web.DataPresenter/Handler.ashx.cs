using System.Web;
using System.Web.Services;

namespace Cpchs.Documents.Web.DataPresenter
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class Handler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string url = context.Request.QueryString["url"];
            byte[] bytes = new byte[1024 * 128];
            int bytesRead;

            System.IO.FileStream fs = new System.IO.FileStream(url, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            string fileExtension = fs.Name.Substring(fs.Name.Length - 4);
            context.Response.ContentType = "image/" + fileExtension;

            while ((bytesRead = fs.Read(bytes, 0, bytes.Length)) > 0)
            {
                context.Response.OutputStream.Write(bytes, 0, bytesRead);
                context.Response.Flush();
            }
            
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
