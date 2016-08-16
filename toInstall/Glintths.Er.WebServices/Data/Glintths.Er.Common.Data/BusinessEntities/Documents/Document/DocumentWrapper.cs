using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cpchs.Eresults.Common.WCF.BusinessEntities
{
    [Serializable]
    public class DocumentWrapper
    {
        public long documentInst { get; set; }
        public long documentPlace { get; set; }
        public long documentApp { get; set; }
        public long documentId { get; set; }
        public string documentName { get; set; }

        public DocumentWrapper()
        {
        }

        public DocumentWrapper(long inst, long place, long app, long id, string name)
        {
            this.documentId = id;
            this.documentName = name;
            this.documentInst = inst;
            this.documentPlace = place;
            this.documentApp = app;
        }
    }
}
