using System;
using System.Data;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization;

using Microsoft.Practices.EnterpriseLibrary.Logging;

using CPCHS.Common.BusinessEntities;
using Cpchs.Eresults.Common.WCF.BusinessEntities.Generated;
	
namespace Cpchs.Eresults.Common.WCF.BusinessEntities
{
    /// <summary>
    /// Date Created: quarta-feira, 23 de Dezembro de 2009
    /// Created By: Generated by CodeSmith
	/// Template Created By: CPCHS psilva, 2005
    /// </summary>
    [DataContract]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores"), Serializable]
    public class Alert : Alert_GEN
	{
        public string alertInstDesc;
        public string alertPlaceDesc;
        public string alertAppDesc;
        public string alertDocTypeDesc;
        public string alertStatusDesc;
        public string alertAlertTypeDesc;
        public string alertHasLogs;
        public string alertIsCorrectable;
        public string alertIsIgnorable;
        public long totalNumber;
        
        /// <summary>
        /// Initialize an new empty Alert object.
        /// </summary>
        public Alert() : base()
        {
        }
		
		/// <summary>
        /// Initialize an new empty Alert object.
        /// </summary>

        public Alert(IDataReader reader, string companyDB)
            : base(reader, companyDB)
        {
            if (reader != null && !reader.IsClosed)
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    switch (reader.GetName(i).ToUpper(System.Globalization.CultureInfo.CurrentCulture))
                    {
                        case "ALERTINSTDESC":
                            if (!reader.IsDBNull(i)) this.alertInstDesc = Convert.ToString(reader.GetValue(i));
                            break;
                        case "ALERTPLACEDESC":
                            if (!reader.IsDBNull(i)) this.alertPlaceDesc = Convert.ToString(reader.GetValue(i));
                            break;
                        case "ALERTAPPDESC":
                            if (!reader.IsDBNull(i)) this.alertAppDesc = Convert.ToString(reader.GetValue(i));
                            break;
                        case "ALERTDOCTYPEDESC":
                            if (!reader.IsDBNull(i)) this.alertDocTypeDesc = Convert.ToString(reader.GetValue(i));
                            break;
                        case "ALERTSTATUSDESC":
                            if (!reader.IsDBNull(i)) this.alertStatusDesc = Convert.ToString(reader.GetValue(i));
                            break;
                        case "ALERTALERTTYPEDESC":
                            if (!reader.IsDBNull(i)) this.alertAlertTypeDesc = Convert.ToString(reader.GetValue(i));
                            break;
                        case "ALERTHASLOGS":
                            if (!reader.IsDBNull(i)) this.alertHasLogs = Convert.ToString(reader.GetValue(i));
                            break;
                        case "ALERTISCORRECTABLE":
                            if (!reader.IsDBNull(i)) this.alertIsCorrectable = Convert.ToString(reader.GetValue(i));
                            break;
                        case "ALERTISIGNORABLE":
                            if (!reader.IsDBNull(i)) this.alertIsIgnorable = Convert.ToString(reader.GetValue(i));
                            break;
                        case "TOTALNUMBER":
                            if (!reader.IsDBNull(i)) this.totalNumber = Convert.ToInt64(reader.GetValue(i));
                            break;                        
                    }

                    AlertSubscription = new AlertSubscription(reader, companyDB);
                }
            }
        }
		
        /// <summary>
        /// Initialize a new  Alert object with the given parameters.
        /// </summary>
        public  Alert(long alertSubsId, Nullable<DateTime> alertDate, long alertStatusId) : base(alertSubsId, alertDate, alertStatusId)
        {
        }
		
        public Alert(long alertId) : base(alertId)
        {
			
        }

	}
}


