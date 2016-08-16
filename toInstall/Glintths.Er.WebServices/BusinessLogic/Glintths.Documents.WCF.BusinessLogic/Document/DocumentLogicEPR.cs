using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using Cpchs.Eresults.Common.WCF.BusinessEntities;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.IO;
using System.Xml.Linq;

namespace Cpchs.Documents.WCF.BusinessLogic
{
    public class DocumentLogicEPR
    {

        public static DocumentList GetDocumentsForEPR(string companyDb, string episodeType,
            string episodeId, string patientId, string patientType, long? pageNumber, long? itemsPerPage, string orderField, string orderType, string period, string eResultsVersion, ref long? resultsCount)
        {
            try
            {

                DocumentList docList;
                try
                {
                    docList = DocumentManagementEPRBER.Instance.GetDocs(companyDb, episodeType, episodeId, patientId, patientType, pageNumber, itemsPerPage, orderField, orderType, period, eResultsVersion, ref resultsCount);
                }
                catch (Exception e)
                {
                    throw new BusinessRulesException("Ocorreu um erro nas regras de negócio."+ e.Message + Environment.NewLine+e.StackTrace, e);
                }

                foreach (var item in docList.Items)
                {
                    if (item.Thumb != null)
                        item.ThumbUrlQuery = "data:image/png;base64," + Convert.ToBase64String(item.Thumb, Base64FormattingOptions.None);
                }

                DocumentList newDocList = new DocumentList();

                foreach (Document doc in docList.Items)
                {
                    var ndoc = newDocList.Items.Where(f=>f.DocumentId == doc.DocumentId).FirstOrDefault();
                    if (ndoc != null)
                    {
                        if (ndoc.DocumentElements != null && ndoc.DocumentElements.Count != 0)
                            ndoc.DocumentElements.Add(doc.DocumentElements[0]);
                    }
                    else
                        newDocList.Add(doc);
                }
                // para separar pais e filhos
                DocumentList parentDocList = new DocumentList();
                DocumentList childDocList = new DocumentList();
                foreach (Document doc in newDocList.Items)
                {
                    if (doc.DocumentParentId == 0)
                    {
                        parentDocList.Items.Add(doc);
                    }
                    else
                    {
                        childDocList.Items.Add(doc);
                    }
                }

                // tratamento da hierarquia
                InsertChilds(parentDocList, childDocList);

                return FinalizeChilds(parentDocList);
            }
            catch (Exception e)
            {
                throw new BusinessLogicException("Ocorreu um erro na lógica de negócio.", e);
            }
        }

        private static DocumentList FinalizeChilds(DocumentList parentDocList)
        {
            DocumentList finalList = new DocumentList();
            foreach (Document doc in parentDocList.Items)
            {
                if (doc.DocumentChilds != null && doc.DocumentChilds.Count == 1)
                {
                    Document thisDoc = doc.DocumentChilds[0];
                    thisDoc.DocumentParentId = 0;
                    finalList.Add(thisDoc);
                }
                else
                {
                    finalList.Add(doc);
                }
            }
            return finalList;
        }

        public static void InsertChilds(DocumentList parents, DocumentList allChilds)
        {
            foreach (Document paren in parents.Items)
            {
                foreach (Document child in allChilds.Items)
                {
                    if (paren.DocumentId != child.DocumentParentId)
                        continue;
                    paren.DocumentElements = new ElementList();
                    if (paren.DocumentChilds.Items != null)
                        paren.DocumentChilds.Items.Add(child);
                }
                if (paren.DocumentChilds.Items != null && paren.DocumentChilds.Items.Count > 0)
                {
                    InsertChilds(paren.DocumentChilds, allChilds);
                }
            }
        }


    }
}