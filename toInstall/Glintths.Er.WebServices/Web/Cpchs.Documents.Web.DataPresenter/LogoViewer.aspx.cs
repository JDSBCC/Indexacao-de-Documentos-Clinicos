using System;
using System.Data.OracleClient;
using System.Globalization;

namespace Cpchs.Documents.Web.DataPresenter
{
    public partial class LogoViewer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["t_doc_id"] == null || Request.QueryString["a_orig_id"] == null)
            {
                Response.TransmitFile("Img/noRequest.png");
                return;
            }

            OracleConnection con = ConnectionUtil.GetConnection();

            OracleDataAdapter datadap = new OracleDataAdapter
                                            {
                                                SelectCommand = new OracleCommand("SELECT logotipo " +
                                                                                  "FROM er_tipo_documento " +
                                                                                  "WHERE tipo_documento_id = :t_doc_id " +
                                                                                  "AND aplicacao_id = :a_orig_id ", con)
                                            };


            datadap.SelectCommand.Parameters.Add("t_doc_id", OracleType.Number);
            datadap.SelectCommand.Parameters["t_doc_id"].Value = Request.QueryString["t_doc_id"]; 

            datadap.SelectCommand.Parameters.Add("a_orig_id", OracleType.Number);
            datadap.SelectCommand.Parameters["a_orig_id"].Value = Request.QueryString["a_orig_id"];

            OracleDataReader firstReader = datadap.SelectCommand.ExecuteReader();
            using (firstReader)
            {
                if (firstReader.HasRows)
                {
                    firstReader.Read();
                    OracleLob blob = firstReader.GetOracleLob(0);
                    if (blob.Length != 0)
                    {
                        SendImageToOutput(blob);
                        return;
                    }
                }
            }
            Response.TransmitFile("Img/image404.png");
        }

        protected void SendImageToOutput(OracleLob blob)
        {
            Response.AddHeader("Content-Length", blob.Length.ToString(CultureInfo.InvariantCulture));
            Response.ContentType = "image/jpeg";

            byte[] bytes = new byte[1024 * 128];
            int bytesRead;
            while ((bytesRead = blob.Read(bytes, 0, bytes.Length)) > 0)
            {
                Response.OutputStream.Write(bytes, 0, bytesRead);
                Response.Flush();
            }
            blob.Close();
            blob.Dispose();
        }
    }
}
