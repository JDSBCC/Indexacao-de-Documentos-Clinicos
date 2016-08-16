using System;
using Cpchs.Eresults.Common.WCF.BusinessEntities;
using Cpchs.Entities.WCF.DataContracts;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using System.Collections.Generic;
using System.Text;

namespace Cpchs.Entities.WCF.ServiceImplementation
{
    public static class TranslateBetweenDocumentTypeBEAndMyNodeDC
    {
        public static Cpchs.Entities.WCF.DataContracts.MyNode TranslateDocumentTypeToMyNode(Cpchs.Eresults.Common.WCF.BusinessEntities.DocumentType from)
        {
            Cpchs.Entities.WCF.DataContracts.MyNode to = new Cpchs.Entities.WCF.DataContracts.MyNode();
            to.MyNodeDescription = from.DocumentTypeDescription;
            to.MyNodeOriginalId = from.DocumentTypeId;

            MemoryStream memoryStream;
            XmlSerializer xs;
            XmlTextWriter xmlTextWriter;

            memoryStream = new MemoryStream();
            xs = new XmlSerializer(typeof(List<Cpchs.Eresults.Common.WCF.BusinessEntities.DocumentWrapper>));
            xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
            xs.Serialize(xmlTextWriter, from.DocumentTypeIds);
            memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
            to.MyNodeIds = new UTF8Encoding().GetString(memoryStream.ToArray()).Substring(1);

            to.MyNodeChilds = TranslateBetweenDocumentTypeListAndMyNodeCollection.TranslateDocumentTypesToMyNodes(from.DocumentTypeChilds);
            return to;
        }
    }
}

