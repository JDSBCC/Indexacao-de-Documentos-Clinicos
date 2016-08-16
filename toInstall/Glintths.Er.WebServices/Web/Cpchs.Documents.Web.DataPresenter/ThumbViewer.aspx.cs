using System;
using System.Data.OracleClient;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.UI;

namespace Cpchs.Documents.Web.DataPresenter
{
    public partial class ThumbViewer : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string id = Request.QueryString["docId"];
                string elemid = Request.QueryString["elemId"];
                string docTypeId = Request.QueryString["docType"];
                string appId = Request.QueryString["app"];
                string elemType = Request.QueryString["elemType"];

                if (id == null && elemid == null && (docTypeId == null || appId == null))
                {
                    Response.TransmitFile("Img/noRequest.png");
                    return;
                }

                OracleConnection con = ConnectionUtil.GetConnection();

                if (docTypeId != null && appId != null)
                {
                    SendDocTypeThumb(con, docTypeId, appId);
                }
                else if (id != null && elemid == null)
                {
                    if (!SendPrimaryThumb(con, id))
                    {
                        SendDefaultThumb(con, id);
                    }
                }
                else if (elemType != null)
                {
                    if (elemType == "Video")
                    {
                        if (SendVideoPrimaryThumbElem(con, elemid) != true)
                        {
                            SendDefaultThumb(con, id);
                        }
                    }
                    else
                    {
                        if (SendFilePrimaryThumbElem(con, elemid) != true)
                        {
                            SendDefaultThumb(con, id);
                        }
                    }
                }
                else
                {
                    if (SendFilePrimaryThumbElem(con, elemid) != true)
                    {
                        SendDefaultThumb(con, id);
                    }
                }

                con.Close();
                con.Dispose();
            }
            catch
            {
                Response.TransmitFile("Img/image404.png");
            }
        }

        private bool SendFilePrimaryThumbElem(OracleConnection con, string elemid)
        {
            OracleDataAdapter datadap = new OracleDataAdapter();
            StringBuilder sb = new StringBuilder();
            sb.Append(@"
            select b.nome_original, b.thumb_stream,
            b.thumb_path || f.codigo || '\' || e.codigo || '\' || g.codigo || '\' || d.codigo || '\' || to_char(a.dt_cri, 'yyyy') || '\' || to_char(a.dt_cri, 'mm') || '\' || c.documento_id
            || '\thumbs\' || a.elemento_id || '_' || a.cod_versao || '.' || substr(b.nome_original,(instr(b.nome_original,'.',-1,1)+1),length(b.nome_original))  FULL_FILE_PATH,
                    b.thumb_path
            from er_elemento a
            join er_ficheiro b on a.elemento_id = b.elemento_id and b.cod_versao = a.cod_versao
            join er_documento c on c.documento_id = a.documento_id
            join er_tipo_documento d on d.tipo_documento_id = c.tipo_documento_id and d.aplicacao_id = c.aplicacao_id
            join er_aplicacao g on g.aplicacao_id = c.aplicacao_id
            join er_local e on e.local_id = c.local_id
            join er_instituicao f on f.instituicao_id = c.instituicao_id
            where a.elemento_id = :elemId
                and a.versao_activa  = 'S'");
            datadap.SelectCommand = new OracleCommand(sb.ToString(), con);
            datadap.SelectCommand.Parameters.Add("elemId", OracleType.Number);
            datadap.SelectCommand.Parameters["elemId"].Value = elemid;
            OracleDataReader reader = datadap.SelectCommand.ExecuteReader();
            using (reader)
            {
                try
                {
                    reader.Read();
                    var fileName = reader.GetOracleString(0).ToString();
                    OracleLob blob = reader.GetOracleLob(1);
                    var fullThumbPath = reader.GetString(2);
                    string thumbPath = null;
                    if (!reader.IsDBNull(3))
                    {
                        thumbPath = reader.GetString(3);
                    }
                    // START: NUNO MENDES
                    if (!string.IsNullOrEmpty(thumbPath))
                    {
                        try
                        {
                            if (!File.Exists(fullThumbPath))
                                return false;

                            byte[] buffer = GetFileBuffer(fullThumbPath);
                            SetResponseHeaders(fileName, buffer.Length.ToString(CultureInfo.InvariantCulture));
                            SendFileStreamToClient(buffer);
                        }
                        catch (UnauthorizedAccessException)
                        {
                            Response.Write("Não tem permissões de acesso ao ficheiro!");
                        }
                        catch (DirectoryNotFoundException)
                        {
                            Response.Write("Erro ao pesquisar o ficheiro!");
                        }
                        catch (IOException)
                        {
                            Response.Write("Erro ao aceder ao ficheiro!");
                        }
                    }
                    // END: NUNO MENDES
                    else
                    {
                        if (blob.Length == 0)
                        {
                            return false;
                        }
                        SendImageToOutput(blob);
                    }
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public void SetResponseHeaders(string filename, string contentLength)
        {
            int indexOfDot = filename.LastIndexOf('.');
            string fileExtension = indexOfDot < 0 ? string.Empty : filename.Substring(indexOfDot);
            string sContentType;
            switch (fileExtension.ToLower())
            {
                case ".dwf":
                    sContentType = "Application/x-dwf";
                    break;

                case ".pdf":
                    sContentType = "Application/pdf";
                    break;

                case ".doc":
                    sContentType = "application/msword";
                    break;

                case ".docx":
                    sContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    break;

                case ".ppt":
                case ".pps":
                    sContentType = "Application/vnd.ms-powerpoint";
                    break;

                case ".xls":
                    sContentType = "Application/vnd.ms-excel";
                    break;

                case ".jpg":
                case ".jpeg":
                    sContentType = "image/jpeg";
                    break;

                case ".bmp":
                    sContentType = "image/bmp";
                    break;

                case ".tif":
                case ".tiff":
                    sContentType = "image/tiff";
                    break;

                case ".png":
                    sContentType = "image/png";
                    break;

                case ".gif":
                    sContentType = "image/gif";
                    break;

                default:
                    sContentType = "Application/octet-stream";
                    break;
            }
            Response.AddHeader("Content-Disposition", "inline;filename=" + filename);
            //Response.AddHeader("Content-Length", contentLength);
            Response.ContentType = sContentType;
        }

        private void SendFileStreamToClient(byte[] buffer)
        {
            byte[] data = buffer;
            int i = 0;
            int window = 1024 * 128;
            if (null != data)
            {
                while (i < data.Length)
                {
                    if ((i + window) > data.Length)
                    {
                        window = data.Length - i;
                    }
                    byte[] part = new byte[window];
                    for (int j = 0; j < window; j++)
                    {
                        part[j] = data[i + j];
                    }
                    Response.OutputStream.Write(part, 0, part.Length);
                    i += window;
                }
            }
        }

        private static byte[] GetFileBuffer(string fullFilePath)
        {
            FileInfo fi = new FileInfo(fullFilePath);
            Stream stream = fi.OpenRead();
            byte[] buffer = new byte[stream.Length];
            stream.Read(buffer, 0, (int)stream.Length);
            stream.Dispose();
            stream.Close();
            return buffer;
        }

        private bool SendVideoPrimaryThumbElem(OracleConnection con, string elemid)
        {
            OracleDataAdapter datadap = new OracleDataAdapter();

            const string query = "SELECT v.url_preview_img " +
                                 "FROM er_elemento e " +
                                 "JOIN er_video v ON " +
                                 "( " +
                                 "   e.elemento_id = v.elemento_id " +
                                 "   AND e.cod_versao = v.cod_versao " +
                                 ") " +
                                 " where e.elemento_id = :elemId " +
                                 " AND e.versao_activa = 'S' ";

            datadap.SelectCommand = new OracleCommand(query, con);
            datadap.SelectCommand.Parameters.Add("elemId", OracleType.Number);
            datadap.SelectCommand.Parameters["elemId"].Value = elemid;

            OracleDataReader firstReader = datadap.SelectCommand.ExecuteReader();

            using (firstReader)
            {
                if (!firstReader.HasRows)
                {
                    return false;
                }
                firstReader.Read();
                OracleString str = firstReader.GetOracleString(0);
                if (str.IsNull || str.Length == 0)
                    return false;
                TransmitFile(str.Value);
                return true;
            }
        }

        private void TransmitFile(string fileUri)
        {
            Response.Redirect(fileUri);
        }

        protected void SendDefaultThumb(OracleConnection con, string docId)
        {
            OracleDataAdapter datadap = new OracleDataAdapter
                                            {
                                                SelectCommand = new OracleCommand("SELECT logotipo " +
                                                                                  "FROM er_tipo_documento, er_documento " +
                                                                                  "WHERE er_tipo_documento.tipo_documento_id = er_documento.tipo_documento_id " +
                                                                                  "AND er_tipo_documento.APLICACAO_ID = er_documento.APLICACAO_ID " +
                                                                                  "AND documento_id = :docId ", con)
                                            };

            datadap.SelectCommand.Parameters.Add("docId", OracleType.Number);
            datadap.SelectCommand.Parameters["docId"].Value = docId;

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

        protected bool SendPrimaryThumb(OracleConnection con, string docId)
        {
            OracleDataAdapter datadap = new OracleDataAdapter();
            StringBuilder sb = new StringBuilder();
            sb.Append(@"
            select b.nome_original, b.thumb_stream,
            b.thumb_path || f.codigo || '\' || e.codigo || '\' || g.codigo || '\' || d.codigo || '\' || to_char(a.dt_cri, 'yyyy') || '\' || to_char(a.dt_cri, 'mm') || '\' || c.documento_id
            || '\thumbs\' || a.elemento_id || '_' || a.cod_versao || '.' || substr(b.nome_original,(instr(b.nome_original,'.',-1,1)+1),length(b.nome_original))  FULL_FILE_PATH,
                    b.thumb_path
            from er_elemento a
            join er_ficheiro b on a.elemento_id = b.elemento_id and b.cod_versao = a.cod_versao
            join er_documento c on c.documento_id = a.documento_id
            join er_tipo_documento d on d.tipo_documento_id = c.tipo_documento_id and d.aplicacao_id = c.aplicacao_id
            join er_aplicacao g on g.aplicacao_id = c.aplicacao_id
            join er_local e on e.local_id = c.local_id
            join er_instituicao f on f.instituicao_id = c.instituicao_id
            where a.documento_id = :docId
                and a.versao_activa  = 'S'
            and rownum = 1");
            datadap.SelectCommand = new OracleCommand(sb.ToString(), con);
            datadap.SelectCommand.Parameters.Add("docId", OracleType.Number);
            datadap.SelectCommand.Parameters["docId"].Value = docId;
            OracleDataReader reader = datadap.SelectCommand.ExecuteReader();

            using (reader)
            {
                try
                {
                    reader.Read();
                    var fileName = reader.GetOracleString(0).ToString();
                    OracleLob blob = reader.GetOracleLob(1);
                    var fullThumbPath = reader.GetString(2);
                    string thumbPath = null;
                    if (!reader.IsDBNull(3))
                    {
                        thumbPath = reader.GetString(3);
                    }
                    // START: NUNO MENDES
                    if (!string.IsNullOrEmpty(thumbPath))
                    {
                        try
                        {
                            if (!File.Exists(fullThumbPath))
                                return false;

                            byte[] buffer = GetFileBuffer(fullThumbPath);
                            SetResponseHeaders(fileName, buffer.Length.ToString(CultureInfo.InvariantCulture));
                            SendFileStreamToClient(buffer);
                        }
                        catch (UnauthorizedAccessException)
                        {
                            Response.Write("Não tem permissões de acesso ao ficheiro!");
                        }
                        catch (DirectoryNotFoundException)
                        {
                            Response.Write("Erro ao pesquisar o ficheiro!");
                        }
                        catch (IOException)
                        {
                            Response.Write("Erro ao aceder ao ficheiro!");
                        }
                    }
                    // END: NUNO MENDES
                    else
                    {
                        if (blob.Length == 0)
                        {
                            return false;
                        }
                        SendImageToOutput(blob);
                    }
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        protected void SendDocTypeThumb(OracleConnection con, string docType, string app)
        {
            OracleDataAdapter datadap = new OracleDataAdapter
                                            {
                                                SelectCommand = new OracleCommand("SELECT logotipo " +
                                                                                  "FROM er_tipo_documento " +
                                                                                  "WHERE er_tipo_documento.tipo_documento_id = :docTypeId " +
                                                                                  "AND er_tipo_documento.APLICACAO_ID = :appId ",
                                                                                  con)
                                            };

            datadap.SelectCommand.Parameters.Add("docTypeId", OracleType.Number);
            datadap.SelectCommand.Parameters["docTypeId"].Value = docType;
            datadap.SelectCommand.Parameters.Add("appId", OracleType.Number);
            datadap.SelectCommand.Parameters["appId"].Value = app;

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
            Response.ContentType = "image/png";

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