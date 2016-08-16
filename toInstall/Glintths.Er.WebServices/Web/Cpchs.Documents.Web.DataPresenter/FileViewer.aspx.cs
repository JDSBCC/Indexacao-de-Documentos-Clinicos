using Cpchs.Common.Security;
using Cpchs.Security.Providers.Utilities;
using Glintths.Er.IPOPDownloader;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OracleClient;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.UI;

namespace Cpchs.Documents.Web.DataPresenter
{
    public partial class FileViewer : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "eResults";

            Dictionary<string, string> queryStr = new Dictionary<string, string>();

            if (Request.QueryString.Count == 0)
            {
                Response.TransmitFile("./Img/noRequest.png");
                return;
            }

            try
            {
                if (Request.QueryString["query"] == null)
                {
                    foreach (string key in Request.QueryString.Keys)
                    {
                        queryStr.Add(key, Request.QueryString[key]);
                    }
                }
                else
                {
                    EncryptionUtil.DecryptQueryString(Request.QueryString["query"].Replace(' ', '+'), ref queryStr);
                }
            }
            catch
            {
                Response.TransmitFile("./Img/image404.png");
                return;
            }

            if (queryStr.ContainsKey("attachResId") && !string.IsNullOrEmpty(queryStr["attachResId"]))
            {
                GetAttachStream(queryStr["attachResId"]);
            }
            else if (queryStr.ContainsKey("docId") && !string.IsNullOrEmpty(queryStr["docId"]) &&
                     queryStr.ContainsKey("elemId") && !string.IsNullOrEmpty(queryStr["elemId"]) &&
                     queryStr.ContainsKey("userSession") && queryStr.ContainsKey("userName") && queryStr.ContainsKey("appName"))
            {
                GetFileStream(queryStr["docId"], queryStr["elemId"], queryStr["userSession"], queryStr["userName"], queryStr["appName"]);
            }
            else if (queryStr.ContainsKey("docId") && !string.IsNullOrEmpty(queryStr["docId"]) &&
                     queryStr.ContainsKey("elemId") && !string.IsNullOrEmpty(queryStr["elemId"]))
            {
                GetFileStream(queryStr["docId"], queryStr["elemId"]);
            }
            else if (queryStr.ContainsKey("docId") && !string.IsNullOrEmpty(queryStr["docId"]))
            {
                GetFileStream(queryStr["docId"]);
            }
            else if (queryStr.ContainsKey("docRef") && !string.IsNullOrEmpty(queryStr["docRef"]))
            {
                GetDocRefFileStream(queryStr["docRef"]);
            }
            else
            {
                Response.TransmitFile("./Img/noRequest.png");
            }
            Response.End();
        }

        public void GetAttachStream(string attachResId)
        {
            OracleConnection con = ConnectionUtil.GetConnection();
            OracleDataAdapter datadap = new OracleDataAdapter();
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT c.nome, c.ficheiro ");
            sb.Append("FROM er_elemento a ");
            sb.Append("JOIN er_res_ana b ON (a.elemento_id = b.elem_id AND a.cod_versao = b.versao) ");
            sb.Append("JOIN er_res_anexo c ON (b.res_ana_id = c.res_ana_id) ");
            sb.Append("WHERE c.res_anexo_id = :attachResId ");
            sb.Append("AND a.versao_activa = 'S' ");
            datadap.SelectCommand = new OracleCommand(sb.ToString(), con);
            datadap.SelectCommand.Parameters.Add("attachResId", OracleType.Number);
            datadap.SelectCommand.Parameters["attachResId"].Value = long.Parse(attachResId);
            OracleDataReader reader = datadap.SelectCommand.ExecuteReader();
            using (reader)
            {
                try
                {
                    reader.Read();
                    OracleLob blob = reader.GetOracleLob(1);
                    SetResponseHeaders(reader.GetOracleString(0).ToString(), blob.Length.ToString(CultureInfo.InvariantCulture));
                    byte[] bytes = new byte[1024 * 128];
                    int bytesRead;
                    while ((bytesRead = blob.Read(bytes, 0, bytes.Length)) > 0)
                    {
                        Response.OutputStream.Write(bytes, 0, bytesRead);
                        Response.Flush();
                    }
                    blob.Close();
                }
                catch
                {
                    Response.TransmitFile("./Img/image404.png");
                    return;
                }
            }
            con.Close();
            con.Dispose();
        }

        public void GetFileStream(string docId)
        {
            OracleConnection con = ConnectionUtil.GetConnection();
            OracleDataAdapter datadap = new OracleDataAdapter();
            StringBuilder sb = new StringBuilder();
            sb.Append(@"
            select b.nome_original, b.file_stream,
            b.file_path || f.codigo || '\' || e.codigo || '\' || g.codigo || '\' || d.codigo || '\' || to_char(a.dt_cri, 'yyyy') || '\' || to_char(a.dt_cri, 'mm') || '\' || c.documento_id
            || '\' || a.elemento_id || '_' || a.cod_versao || '.' || substr(b.nome_original,(instr(b.nome_original,'.',-1,1)+1),length(b.nome_original))  FULL_FILE_PATH,
                    b.file_path,
            pck_documents_file.GetValueFromErFicheiroCaract(a.elemento_id, a.cod_versao, 'USE_FULL_FILE_PATH') caminho_completo,
            pck_documents_file.GetValueFromErFicheiroCaract(a.elemento_id, a.cod_versao, 'ENCRYPTION_KEY') chave_encript,
            enc.metodo metodo_encript,
            (SELECT valor FROM er_config WHERE chave = 'OUTPUT_FILE_DIR') outputfilepath
            from er_elemento a
            join er_ficheiro b on a.elemento_id = b.elemento_id and b.cod_versao = a.cod_versao
            join er_documento c on c.documento_id = a.documento_id
            join er_tipo_documento d on d.tipo_documento_id = c.tipo_documento_id and d.aplicacao_id = c.aplicacao_id
            join er_aplicacao g on g.aplicacao_id = c.aplicacao_id
            join er_local e on e.local_id = c.local_id
            join er_instituicao f on f.instituicao_id = c.instituicao_id
            left join er_metodo_encriptacao enc on enc.encript_id = b.encriptado
            where a.documento_id = :docId
                and a.versao_activa  = 'S' and rownum = 1");
            datadap.SelectCommand = new OracleCommand(sb.ToString(), con);
            datadap.SelectCommand.Parameters.Add("docId", OracleType.Number);
            datadap.SelectCommand.Parameters["docId"].Value = long.Parse(docId);
            OracleDataReader reader = datadap.SelectCommand.ExecuteReader();
            using (reader)
            {
                try
                {
                    reader.Read();
                    var fileName = reader.GetOracleString(0).ToString();
                    OracleLob blob = reader.GetOracleLob(1);
                    var fullFilePath = reader.GetString(2);
                    string filePath = null;
                    if (!reader.IsDBNull(3))
                    {
                        filePath = reader.GetString(3);
                    }
                    // START: NUNO MENDES
                    if (!string.IsNullOrEmpty(filePath))
                    {
                        bool useFullFilePath = false;
                        if (!reader.IsDBNull(4))
                            useFullFilePath = ("S".Equals(reader.GetString(4)) ? true : false);

                        //Determinar se deve usar o caminho completo ou não
                        if (useFullFilePath && !string.IsNullOrEmpty(filePath))
                        {
                            fullFilePath = filePath;
                            // remover a / que é colocada por trigger na coluna file_path
                            if ('\\'.Equals(fullFilePath[(fullFilePath.Length - 1)]))
                                fullFilePath = fullFilePath.Remove(fullFilePath.Length - 1);
                        }

                        //Entensão do ficheiro
                        int indexOfDot = fileName.LastIndexOf('.');
                        string fileExtension = indexOfDot < 0 ? string.Empty : fileName.Substring(indexOfDot);

                        //Método de encriptação
                        string encryptionMethod = string.Empty;
                        if (!reader.IsDBNull(6))
                        {
                            encryptionMethod = Convert.ToString(reader.GetOracleLob(6).Value);
                        }

                        //Diretório de output dos ficheiros desencriptados
                        string outputFilePath = string.Empty;
                        if (!reader.IsDBNull(7))
                        {
                            outputFilePath = reader.GetString(7);
                        }

                        //Caso esteja encriptado, desencriptar ficheiro
                        if (!string.IsNullOrEmpty(encryptionMethod))
                        {
                            string encryptionKey = reader.GetString(5);

                            if (string.IsNullOrEmpty(encryptionKey))
                            {
                                Response.Write("Chave de desencriptação vazia");
                                return;
                            } 

                            if ("ERESULTS_V1".Equals(encryptionMethod.ToUpper()))
                            {
                                outputFilePath = outputFilePath + "\\" + Guid.NewGuid() + "." + fileExtension;
                                if (EncryptionUtil.DecodeFile(encryptionKey, fullFilePath, outputFilePath))
                                    fullFilePath = outputFilePath;
                                else
                                {
                                    Response.Write("Erro ao desencriptar o ficheiro");
                                    return;
                                } 
                            }
                            else
                            {
                                Response.Write("Método de encriptação não definido");
                                return;
                            } 
                        }

                        try
                        {
                            byte[] buffer = GetFileBuffer(fullFilePath);
                            SetResponseHeaders(fileName, buffer.Length.ToString(CultureInfo.InvariantCulture));
                            SendFileStreamToClient(buffer);

                            if (!string.IsNullOrEmpty(outputFilePath))
                                File.Delete(outputFilePath);
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
                        SetResponseHeaders(reader.GetOracleString(0).ToString(), blob.Length.ToString(CultureInfo.InvariantCulture));
                        //send the data
                        byte[] bytes = new byte[1024 * 128];
                        int bytesRead;
                        while ((bytesRead = blob.Read(bytes, 0, bytes.Length)) > 0)
                        {
                            Response.OutputStream.Write(bytes, 0, bytesRead);
                            Response.Flush();
                        }
                        blob.Close();
                    }
                }
                catch
                {
                    Response.TransmitFile("./Img/image404.png");
                    return;
                }
            }
            con.Close();
            con.Dispose();
        }

        public void GetFileStream(string docId, string elemId)
        {
            OracleConnection con = ConnectionUtil.GetConnection();
            OracleDataAdapter datadap = new OracleDataAdapter();
            StringBuilder sb = new StringBuilder();
            sb.Append(@"
            select b.nome_original, b.file_stream,
            b.file_path || f.codigo || '\' || e.codigo || '\' || g.codigo || '\' || d.codigo || '\' || to_char(a.dt_cri, 'yyyy') || '\' || to_char(a.dt_cri, 'mm') || '\' || c.documento_id
            || '\' || a.elemento_id || '_' || a.cod_versao || '.' || substr(b.nome_original,(instr(b.nome_original,'.',-1,1)+1),length(b.nome_original))  FULL_FILE_PATH,
                    b.file_path,
            pck_documents_file.GetValueFromErFicheiroCaract(a.elemento_id, a.cod_versao, 'USE_FULL_FILE_PATH') caminho_completo,
            pck_documents_file.GetValueFromErFicheiroCaract(a.elemento_id, a.cod_versao, 'ENCRYPTION_KEY') chave_encript,
            enc.metodo metodo_encript,
            (SELECT valor FROM er_config WHERE chave = 'OUTPUT_FILE_DIR') outputfilepath
            from er_elemento a
            join er_ficheiro b on a.elemento_id = b.elemento_id and b.cod_versao = a.cod_versao
            join er_documento c on c.documento_id = a.documento_id
            join er_tipo_documento d on d.tipo_documento_id = c.tipo_documento_id and d.aplicacao_id = c.aplicacao_id
            join er_aplicacao g on g.aplicacao_id = c.aplicacao_id
            join er_local e on e.local_id = c.local_id
            join er_instituicao f on f.instituicao_id = c.instituicao_id
            left join er_metodo_encriptacao enc on enc.encript_id = b.encriptado
            where a.documento_id = :docId
                and a.elemento_id = :elemId
                and a.versao_activa  = 'S'");
            datadap.SelectCommand = new OracleCommand(sb.ToString(), con);
            datadap.SelectCommand.Parameters.Add("docId", OracleType.Number);
            datadap.SelectCommand.Parameters["docId"].Value = long.Parse(docId);
            datadap.SelectCommand.Parameters.Add("elemId", OracleType.Number);
            datadap.SelectCommand.Parameters["elemId"].Value = long.Parse(elemId);
            OracleDataReader reader = datadap.SelectCommand.ExecuteReader();
            using (reader)
            {
                try
                {
                    reader.Read();
                    if (reader.HasRows)
                    {
                        var fileName = reader.GetOracleString(0).ToString();
                        OracleLob blob = reader.GetOracleLob(1);
                        var fullFilePath = reader.GetString(2);
                        string filePath = null;
                        if (!reader.IsDBNull(3))
                        {
                            filePath = reader.GetString(3);
                        }

                        // START: NUNO MENDES
                        if (!string.IsNullOrEmpty(filePath))
                        {
                            bool useFullFilePath = false;
                            if (!reader.IsDBNull(4))
                                useFullFilePath = ("S".Equals(reader.GetString(4)) ? true : false);

                            //Determinar se deve usar o caminho completo ou não
                            if (useFullFilePath && !string.IsNullOrEmpty(filePath))
                            {
                                fullFilePath = filePath;
                                // remover a / que é colocada por trigger na coluna file_path
                                if ('\\'.Equals(fullFilePath[(fullFilePath.Length - 1)]))
                                    fullFilePath = fullFilePath.Remove(fullFilePath.Length - 1);
                            }

                            //Entensão do ficheiro
                            int indexOfDot = fileName.LastIndexOf('.');
                            string fileExtension = indexOfDot < 0 ? string.Empty : fileName.Substring(indexOfDot);

                            //Método de encriptação
                            string encryptionMethod = string.Empty;
                            if (!reader.IsDBNull(6))
                            {
                                encryptionMethod = Convert.ToString(reader.GetOracleLob(6).Value);
                            }

                            //Diretório de output dos ficheiros desencriptados
                            string outputFilePath = string.Empty;
                            if (!reader.IsDBNull(7))
                            {
                                outputFilePath = reader.GetString(7);
                            }

                            //Caso esteja encriptado, desencriptar ficheiro
                            if (!string.IsNullOrEmpty(encryptionMethod))
                            {
                                string encryptionKey = reader.GetString(5);

                                if (string.IsNullOrEmpty(encryptionKey))
                                {
                                    Response.Write("Chave de desencriptação vazia");
                                    return;
                                } 

                                if ("ERESULTS_V1".Equals(encryptionMethod.ToUpper()))
                                {
                                    outputFilePath = outputFilePath + "\\" + Guid.NewGuid() + "." + fileExtension;
                                    if (EncryptionUtil.DecodeFile(encryptionKey, fullFilePath, outputFilePath))
                                        fullFilePath = outputFilePath;
                                    else
                                    {
                                        Response.Write("Erro ao desencriptar o ficheiro");
                                        return;
                                    }  
                                }
                                else
                                {
                                    Response.Write("Método de encriptação não definido");
                                    return;
                                }
                            }


                            try
                            {
                                byte[] buffer = GetFileBuffer(fullFilePath);
                                SetResponseHeaders(fileName, buffer.Length.ToString(CultureInfo.InvariantCulture));
                                SendFileStreamToClient(buffer);

                                if (!string.IsNullOrEmpty(outputFilePath))
                                    File.Delete(outputFilePath);
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
                            SetResponseHeaders(reader.GetOracleString(0).ToString(), blob.Length.ToString(CultureInfo.InvariantCulture));
                            //send the data
                            byte[] bytes = new byte[1024 * 128];
                            int bytesRead;
                            while ((bytesRead = blob.Read(bytes, 0, bytes.Length)) > 0)
                            {
                                Response.OutputStream.Write(bytes, 0, bytesRead);
                                Response.Flush();
                            }
                            blob.Close();
                        }
                    }
                    else
                    {
                        //System.Net.WebClient client = new System.Net.WebClient();
                        //string url = "http://www.unifymatch.com/images/error_button.png";
                        //string url = "http://ipophisweb.ipoporto.min-saude.pt:7779/sgd/idcplg?cookieLogin=1&username=oasisipo&password=ora75nv3qds1x&redirecturl=http://ipophisweb.ipoporto.min-saude.pt:7779/sgd/groups/aca/sg/ipop/000/001/507/d/49-14-66924-1.pdf"; //GetFileURL(fileId, elemId);
                        //string url = "http://ipophisweb.ipoporto.min-saude.pt:7779/sgd/idcplg?cookieLogin=1&username=oasisipo&password=ora75nv3qds1x&redirecturl=http://ipophisweb.ipoporto.min-saude.pt:7779/sgd/idcplg?IdcService=IPOPORTO_DOCUMENT_VIEW%26xCPC_IdSequencial=66924%26xCPC_TipoDocumento=49%26xCPC_Origem=14%26xCPC_Numerador=1";

                        string url = GetFileURL(docId, elemId);
                        LinkDownloader lkDown = new LinkDownloader();
                        lkDown.GetFileFromUri(url, Response);
                        return;
                    }
                }
                catch (Exception e)
                {
                    StreamWriter sw = new StreamWriter(Response.OutputStream);
                    sw.Write("ex " + e.Message + " " + e.StackTrace);
                    sw.Flush();
                    return;
                }
            }
            con.Close();
            con.Dispose();
        }

        public void GetDocRefFileStream(string docRef)
        {
            OracleConnection con = ConnectionUtil.GetConnection();
            OracleDataAdapter datadap = new OracleDataAdapter();
            StringBuilder sb = new StringBuilder();
            sb.Append(@"
            select b.nome_original, b.file_stream,
            b.file_path || f.codigo || '\' || e.codigo || '\' || g.codigo || '\' || d.codigo || '\' || to_char(a.dt_cri, 'yyyy') || '\' || to_char(a.dt_cri, 'mm') || '\' || c.documento_id
            || '\' || a.elemento_id || '_' || a.cod_versao || '.' || substr(b.nome_original,(instr(b.nome_original,'.',-1,1)+1),length(b.nome_original))  FULL_FILE_PATH,
                    b.file_path,
            pck_documents_file.GetValueFromErFicheiroCaract(a.elemento_id, a.cod_versao, 'USE_FULL_FILE_PATH') caminho_completo,
            pck_documents_file.GetValueFromErFicheiroCaract(a.elemento_id, a.cod_versao, 'ENCRYPTION_KEY') chave_encript,
            enc.metodo metodo_encript,
            (SELECT valor FROM er_config WHERE chave = 'OUTPUT_FILE_DIR') outputfilepath
            from er_elemento a
            join er_ficheiro b on a.elemento_id = b.elemento_id and b.cod_versao = a.cod_versao
            join er_documento c on c.documento_id = a.documento_id
            join er_tipo_documento d on d.tipo_documento_id = c.tipo_documento_id and d.aplicacao_id = c.aplicacao_id
            join er_aplicacao g on g.aplicacao_id = c.aplicacao_id
            join er_local e on e.local_id = c.local_id
            join er_instituicao f on f.instituicao_id = c.instituicao_id
            left join er_metodo_encriptacao enc on enc.encript_id = b.encriptado
            where c.documento = :docRef
                and a.versao_activa  = 'S' and rownum = 1");
            datadap.SelectCommand = new OracleCommand(sb.ToString(), con);
            datadap.SelectCommand.Parameters.Add("docRef", OracleType.VarChar);
            datadap.SelectCommand.Parameters["docRef"].Value = docRef;
            OracleDataReader reader = datadap.SelectCommand.ExecuteReader();
            using (reader)
            {
                try
                {
                    reader.Read();
                    var fileName = reader.GetOracleString(0).ToString();
                    OracleLob blob = reader.GetOracleLob(1);
                    var fullFilePath = reader.GetString(2);
                    string filePath = null;
                    if (!reader.IsDBNull(3))
                    {
                        filePath = reader.GetString(3);
                    }
                    // START: NUNO MENDES
                    if (!string.IsNullOrEmpty(filePath))
                    {
                        bool useFullFilePath = false;
                        if (!reader.IsDBNull(4))
                            useFullFilePath = ("S".Equals(reader.GetString(4)) ? true : false);

                        //Determinar se deve usar o caminho completo ou não
                        if (useFullFilePath && !string.IsNullOrEmpty(filePath))
                        {
                            fullFilePath = filePath;
                            // remover a / que é colocada por trigger na coluna file_path
                            if ('\\'.Equals(fullFilePath[(fullFilePath.Length - 1)]))
                                fullFilePath = fullFilePath.Remove(fullFilePath.Length - 1);
                        }

                        //Entensão do ficheiro
                        int indexOfDot = fileName.LastIndexOf('.');
                        string fileExtension = indexOfDot < 0 ? string.Empty : fileName.Substring(indexOfDot);

                        //Método de encriptação
                        string encryptionMethod = string.Empty;
                        if (!reader.IsDBNull(6))
                        {
                            encryptionMethod = Convert.ToString(reader.GetOracleLob(6).Value);
                        }

                        //Diretório de output dos ficheiros desencriptados
                        string outputFilePath = string.Empty;
                        if (!reader.IsDBNull(7))
                        {
                            outputFilePath = reader.GetString(7);
                        }

                        //Caso esteja encriptado, desencriptar ficheiro
                        if (!string.IsNullOrEmpty(encryptionMethod))
                        {
                            string encryptionKey = reader.GetString(5);

                            if (string.IsNullOrEmpty(encryptionKey))
                            {
                                Response.Write("Chave de desencriptação vazia");
                                return;
                            }

                            if ("ERESULTS_V1".Equals(encryptionMethod.ToUpper()))
                            {
                                outputFilePath = outputFilePath + "\\" + Guid.NewGuid() + "." + fileExtension;
                                if (EncryptionUtil.DecodeFile(encryptionKey, fullFilePath, outputFilePath))
                                    fullFilePath = outputFilePath;
                                else
                                {
                                    Response.Write("Erro ao desencriptar o ficheiro");
                                    return;
                                }
                            }
                            else
                            {
                                Response.Write("Método de encriptação não definido");
                                return;
                            }
                        }

                        try
                        {
                            byte[] buffer = GetFileBuffer(fullFilePath);
                            SetResponseHeaders(fileName, buffer.Length.ToString(CultureInfo.InvariantCulture));
                            SendFileStreamToClient(buffer);

                            if (!string.IsNullOrEmpty(outputFilePath))
                                File.Delete(outputFilePath);
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
                        SetResponseHeaders(reader.GetOracleString(0).ToString(), blob.Length.ToString(CultureInfo.InvariantCulture));
                        //send the data
                        byte[] bytes = new byte[1024 * 128];
                        int bytesRead;
                        while ((bytesRead = blob.Read(bytes, 0, bytes.Length)) > 0)
                        {
                            Response.OutputStream.Write(bytes, 0, bytesRead);
                            Response.Flush();
                        }
                        blob.Close();
                    }
                }
                catch
                {
                    Response.TransmitFile("./Img/image404.png");
                    return;
                }
            }
            con.Close();
            con.Dispose();
        }

        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        private string GetFileURL(string docId, string elemId)
        {
            string url = string.Empty;
            using (OracleConnection connection = new OracleConnection(ConnectionUtil.GetConnection().ConnectionString))
            {
                OracleCommand command = new OracleCommand
                {
                    Connection = connection,
                    CommandText = "GetFileURL",
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                OracleParameter op = new OracleParameter("p_fileId", OracleType.NVarChar) { Direction = System.Data.ParameterDirection.Input, Value = docId };
                command.Parameters.Add(op);
                op = new OracleParameter("p_elemId", OracleType.NVarChar) { Direction = System.Data.ParameterDirection.Input, Value = elemId };
                command.Parameters.Add(op);
                op = new OracleParameter("p_url", OracleType.NVarChar) { Direction = System.Data.ParameterDirection.Output, Size = 1000 };
                command.Parameters.Add(op);
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    url = (string)command.Parameters[2].Value;
                }
                catch (Exception)
                {
                }
                connection.Close();
            }
            return url;
        }

        public void GetFileStream(string fileId, string elemId, string userSession, string userName, string appName)
        {
            OracleConnection con = ConnectionUtil.GetConnection();
            OracleDataAdapter datadap = new OracleDataAdapter();
            StringBuilder sb = new StringBuilder();
            sb.Append(@"
            select b.nome_original, b.file_stream, a.elemento_id, a.cod_versao,
            c.aplicacao_id, c.tipo_documento_id, c.documento_id, c.documento,
            b.file_path || f.codigo || '\' || e.codigo || '\' || g.codigo || '\' || d.codigo || '\' || to_char(a.dt_cri, 'yyyy') || '\' || to_char(a.dt_cri, 'mm') || '\' || c.documento_id
            || '\' || a.elemento_id || '_' || a.cod_versao || '.' || substr(b.nome_original,(instr(b.nome_original,'.',-1,1)+1),length(b.nome_original))  FULL_FILE_PATH,
                    b.file_path,
            pck_documents_file.GetValueFromErFicheiroCaract(a.elemento_id, a.cod_versao, 'USE_FULL_FILE_PATH') caminho_completo,
            pck_documents_file.GetValueFromErFicheiroCaract(a.elemento_id, a.cod_versao, 'ENCRYPTION_KEY') chave_encript,
            enc.metodo metodo_encript,
            (SELECT valor FROM er_config WHERE chave = 'OUTPUT_FILE_DIR') outputfilepath
            from er_elemento a
            join er_ficheiro b on a.elemento_id = b.elemento_id and b.cod_versao = a.cod_versao
            join er_documento c on c.documento_id = a.documento_id
            join er_tipo_documento d on d.tipo_documento_id = c.tipo_documento_id and d.aplicacao_id = c.aplicacao_id
            join er_aplicacao g on g.aplicacao_id = c.aplicacao_id
            join er_local e on e.local_id = c.local_id
            join er_instituicao f on f.instituicao_id = c.instituicao_id
            left join er_metodo_encriptacao enc on enc.encript_id = b.encriptado
            where a.documento_id = :docId
                and a.elemento_id = :elemId
                and a.versao_activa  = 'S'");
            datadap.SelectCommand = new OracleCommand(sb.ToString(), con);
            datadap.SelectCommand.Parameters.Add("docId", OracleType.Number);
            datadap.SelectCommand.Parameters["docId"].Value = long.Parse(fileId);
            datadap.SelectCommand.Parameters.Add("elemId", OracleType.Number);
            datadap.SelectCommand.Parameters["elemId"].Value = long.Parse(elemId);
            OracleDataReader reader = datadap.SelectCommand.ExecuteReader();
            using (reader)
            {
                try
                {
                    reader.Read();
                    var fileName = reader.GetOracleString(0).ToString();
                    OracleLob blob = reader.GetOracleLob(1);
                    long artifactId = reader.GetInt64(2);
                    long versionId = reader.GetInt64(3);
                    long appOrigin = reader.GetInt64(4);
                    long docType = reader.GetInt64(5);
                    long docId = reader.GetInt64(6);
                    string docRef = reader.GetString(7);
                    var fullFilePath = reader.GetString(8);
                    string filePath = null;
                    if (!reader.IsDBNull(9))
                    {
                        filePath = reader.GetString(9);
                    }
                    // START: NUNO MENDES
                    if (!string.IsNullOrEmpty(filePath))
                    {
                        bool useFullFilePath = false;
                        if (!reader.IsDBNull(10))
                            useFullFilePath = ("S".Equals(reader.GetString(10)) ? true : false);

                        //Determinar se deve usar o caminho completo ou não
                        if (useFullFilePath && !string.IsNullOrEmpty(filePath))
                        {
                            fullFilePath = filePath;
                            // remover a / que é colocada por trigger na coluna file_path
                            if ('\\'.Equals(fullFilePath[(fullFilePath.Length-1)]))
                                fullFilePath = fullFilePath.Remove(fullFilePath.Length - 1); 
                        }

                        //Entensão do ficheiro
                        int indexOfDot = fileName.LastIndexOf('.');
                        string fileExtension = indexOfDot < 0 ? string.Empty : fileName.Substring(indexOfDot);

                        //Método de encriptação
                        string encryptionMethod = string.Empty;
                        if (!reader.IsDBNull(12))
                        {
                            encryptionMethod = Convert.ToString(reader.GetOracleLob(12).Value);
                        }

                        //Diretório de output dos ficheiros desencriptados
                        string outputFilePath = string.Empty;
                        if (!reader.IsDBNull(13))
                        {
                            outputFilePath = reader.GetString(13);
                        }

                        //Caso esteja encriptado, desencriptar ficheiro
                        if (!string.IsNullOrEmpty(encryptionMethod))
                        {
                            string encryptionKey = reader.GetString(11);

                            if (string.IsNullOrEmpty(encryptionKey))
                            {
                                Response.Write("Chave de desencriptação vazia");
                                return;
                            }

                            if ("ERESULTS_V1".Equals(encryptionMethod.ToUpper()))
                            {
                                outputFilePath = outputFilePath + "\\" + Guid.NewGuid() + "." + fileExtension;
                                if (EncryptionUtil.DecodeFile(encryptionKey, fullFilePath, outputFilePath))
                                    fullFilePath = outputFilePath;
                                else
                                {
                                    Response.Write("Erro ao desencriptar o ficheiro");
                                    return;
                                }
                            }
                            else
                            {
                                Response.Write("Método de encriptação não definido");
                                return;
                            }
                        }

                        try
                        {
                            byte[] buffer = GetFileBuffer(fullFilePath);
                            SetResponseHeaders(fileName, buffer.Length.ToString(CultureInfo.InvariantCulture));
                            SendFileStreamToClient(buffer);

                            if (!string.IsNullOrEmpty(outputFilePath))
                                File.Delete(outputFilePath);
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
                        SetResponseHeaders(reader.GetOracleString(0).ToString(), blob.Length.ToString(CultureInfo.InvariantCulture));
                        //send the data
                        byte[] bytes = new byte[1024 * 128];
                        int bytesRead;
                        while ((bytesRead = blob.Read(bytes, 0, bytes.Length)) > 0)
                        {
                            Response.OutputStream.Write(bytes, 0, bytesRead);
                            Response.Flush();
                        }
                        blob.Close();
                    }

                    if (!(string.IsNullOrEmpty(userSession) && string.IsNullOrEmpty(userName) && string.IsNullOrEmpty(appName)))
                    {
                        RegisterDocumentAccess(userSession, userName, appName, artifactId, versionId, appOrigin, docType, docId, docRef);
                    }
                }
                catch
                {
                    Response.TransmitFile("./Img/image404.png");
                    return;
                }
            }
            con.Close();
            con.Dispose();
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

        private void RegisterDocumentAccess(string userSession, string userName, string appName, long artifactId, long versionId, long appOrigin, long docType, long docId, string docRef)
        {
            try
            {
                string regDocAccess = ConfigurationManager.AppSettings["RegisterDocumentAccess"].ToString(CultureInfo.InvariantCulture);
                if (regDocAccess == "enable")
                {
                    RegisterDocumentAccess(appName, userSession, userName, docId, docRef, artifactId, versionId, appOrigin, docType);
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        private void RegisterDocumentAccess(string applicationName, string sessionId, string userName, long docId, string docRef, long artifactId, long versionId, long appOrigin, long docType)
        {
            SecurityContext.Instance.CurrentApplication = new Application(applicationName);
            RegisterDocumentAccessAction(sessionId, userName, docId, docRef, artifactId, versionId, appOrigin, docType);
        }

        public static void RegisterDocumentAccessAction(string sessionId, string userName, long docId, string docRef, long artifactId, long versionId, long appOrigin, long docType)
        {
            new WCF.Proxy.DocumentsProxy().RegisterDocumentAccess(sessionId, userName, docId, docRef, artifactId, versionId, appOrigin, docType);
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

                case ".txt":
                    sContentType = "text/plain";
                    break;

                default:
                    sContentType = "Application/octet-stream";
                    break;
            }
            Response.AddHeader("Content-Disposition", "inline;filename=" + filename);
            Response.AddHeader("Content-Length", contentLength);
            Response.ContentType = sContentType;
        }

        public string DecryptFile(string encryptionKey, string encryptionMethod, string inputFile)
        {
            return "";
        }
    }
}