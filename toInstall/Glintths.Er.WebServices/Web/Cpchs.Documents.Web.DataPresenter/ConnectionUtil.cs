using System.Configuration;
using System.Data.OracleClient;
using System.Globalization;

namespace Cpchs.Documents.Web.DataPresenter
{
    public class ConnectionUtil
    {
        private static OracleConnection Connect()
        {
            string connStr = ConfigurationManager.AppSettings["FileViewerConnectionString"].ToString(CultureInfo.InvariantCulture);
            OracleConnection con = new OracleConnection(connStr);
            con.Open();
            return con;
        }

        public static OracleConnection GetConnection() { 

            int parsedRetryCount,
                retryCount = 0;

            if (!int.TryParse(ConfigurationManager.AppSettings["RetryConnectionCount"].ToString(CultureInfo.InvariantCulture), out parsedRetryCount))
                parsedRetryCount = 3;
            do
            {
                try
                {
                    OracleConnection obtainedConnection = Connect();
                    return obtainedConnection;
                }
                catch
                { 
                    retryCount++;
                } 

            } while (retryCount < parsedRetryCount && retryCount < 20);
            return null;
        }
    }
}