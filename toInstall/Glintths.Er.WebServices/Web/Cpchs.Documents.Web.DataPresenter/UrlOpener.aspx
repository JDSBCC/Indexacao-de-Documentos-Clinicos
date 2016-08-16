<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UrlOpener.aspx.cs" Inherits="Cpchs.Documents.Web.DataPresenter.UrlOpener" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <%  
        string tokenFromUrl = Request.QueryString["token"];
        if (!String.IsNullOrWhiteSpace(tokenFromUrl))
        {
            try
            {
                byte[] data = Convert.FromBase64String(tokenFromUrl);
                string decodedString = Encoding.UTF8.GetString(data);
                Uri validatedUri;

                if (Uri.TryCreate(decodedString, UriKind.RelativeOrAbsolute, out validatedUri))
                    Response.Redirect(decodedString);
            }
            catch (Exception e)
            {
                Response.Write("Não foi possível encontrar a página.");
            }
        }
                
         %> 
   <%--<%= decodedString %>--%>

</body>
</html>
