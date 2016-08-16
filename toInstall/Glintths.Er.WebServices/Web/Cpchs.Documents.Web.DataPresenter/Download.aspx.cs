using System;
using System.Globalization;
using System.Web.UI.WebControls;

namespace Cpchs.Documents.Web.DataPresenter
{
    public partial class Download : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string fileid = Request.QueryString["file"];

                bool forceDownload = Request.QueryString["forcedownload"] != null;

                long qty;
                if (long.TryParse(Request.QueryString["qty"], out qty) == false)
                    qty = 1;
                if (string.IsNullOrEmpty(fileid))
                {
                    Response.Write("Empty query.");
                }
                else
                {
                    bool fileExists = true;
                    if (!System.IO.File.Exists(Server.MapPath("TempFiles/" + fileid)))
                    {
                        System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"/Download\.aspx(.*)$");
                        var matches = reg.Matches(Request.RawUrl);
                        if (matches.Count > 0)
                        {
                            fileid = matches[0].Groups[1].Value.Split(new[] { '&' })[0];
                        }

                        if (!System.IO.File.Exists(Server.MapPath("TempFiles/" + fileid)))
                        {
                            fileExists = false;
                        }
                    }
                    if (fileExists)
                    {
                        byte[] bytes = new byte[1024 * 128];
                        System.IO.FileStream fs = new System.IO.FileStream(Server.MapPath("TempFiles/" + fileid), System.IO.FileMode.Open, System.IO.FileAccess.Read);
                        try
                        {

                            string fileExtension = fs.Name.Substring(fs.Name.Length - 4);

                            switch (fileExtension.ToLower())
                            {
                                case ".bmp":
                                case ".jpg":
                                case ".tif":
                                case ".jpeg":
                                case ".png":
                                case ".gif":
                                    Image img = new Image
                                                    {
                                                        ImageUrl =
                                                            "Handler.ashx?url=" + Server.MapPath("TempFiles/" + fileid)
                                                    };
                                    img.Attributes.Add("onLoad", "printAll('"+qty+"')");
                                    Page.Form.Controls.Add(
                                       img
                                    );

                                    break;
                                case ".pdf":
                                    const string sContentType = "application/pdf";
                                    if (forceDownload)
                                        Response.AddHeader("Content-Disposition",
                                            "attachment; filename=" + DateTime.Now.ToShortDateString().Replace("-", "_") + "_" + DateTime.Now.ToShortTimeString().Replace(":", "_") + "." + fileExtension);

                                    Response.AddHeader("Content-Length", fs.Length.ToString(CultureInfo.InvariantCulture));
                                    Response.ContentType = sContentType;
                                    Response.Charset = "UTF-8";

                                    int bytesRead;
                                    while ((bytesRead = fs.Read(bytes, 0, bytes.Length)) > 0)
                                    {
                                        Response.OutputStream.Write(bytes, 0, bytesRead);
                                        Response.Flush();
                                    }
                                    Response.Close();
                                    fs.Close();
                                    System.IO.File.Delete(Server.MapPath("TempFiles/" + fileid));

                                    break;

                                default:
                                    Response.Write("File not supported");

                                    break;

                            }
                        }
                        finally
                        {
                            fs.Close();
                        }
                    }
                    else
                    {
                        Response.Write("File does not exists ");
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
    }
}
