using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cpchs.ER2Indexer.WCF.BusinessEntities;

namespace Cpchs.Eresults.Common.WCF.Providers
{
    public interface IER2IndexerProvider
    {
        string IndexDocument(string companyDb, DocumentInfo documentInfo);

        string CancelDocument(string companyDb, string instCode, string placeCode, string appCode, string docTypeCode, string docId, string elemId);
    }
}