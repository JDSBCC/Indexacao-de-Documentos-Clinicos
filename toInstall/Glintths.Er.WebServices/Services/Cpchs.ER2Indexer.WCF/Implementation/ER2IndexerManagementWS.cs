using Cpchs.ER2Indexer.WCF.BusinessLogic;
using Cpchs.ER2Indexer.WCF.FaultContracts;
using Cpchs.ER2Indexer.WCF.MessageContracts;
using Cpchs.Eresults.Common.WCF.Providers;
using System;
using System.Configuration;
using System.IO;
using System.Runtime.Remoting.Messaging;
using System.ServiceModel;

namespace Cpchs.ER2Indexer.WCF.ServiceImplementation
{
    public partial class ER2IndexerManagementWS
    {
        public override void IndexDocument(IndexDocumentRequest request)
        {
            try
            {
                string indexMode = ConfigurationManager.AppSettings["IndexMode"] ?? "INTERNAL";

                if (indexMode == "EXTERNAL")
                {
                    string indexProvider = ConfigurationManager.AppSettings["IndexProvider"];
                    ProviderManager.Instance.SetProviderConfiguration(File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "/ProvidersConf.xml"));

                    if (indexProvider != null)
                    {
                        IER2IndexerProvider provider = (IER2IndexerProvider)ProviderManager.Instance.GetProvider(indexProvider.ToUpper());
                        provider.IndexDocument(request.CompanyDb, TranslateBetweenDocumentInfoBeAndDocumentInfoDc.TranslateDocumentInfoToDocumentInfo(request.DocumentData));
                    }
                    else
                    {
                        throw new Exception("Provider não configurado!");
                    }
                }
                else
                {
                    
                    ER2IndexerLogic.IndexDocument(request.CompanyDb, TranslateBetweenDocumentInfoBeAndDocumentInfoDc.TranslateDocumentInfoToDocumentInfo(request.DocumentData));
                }
            }
            catch (Exception e)
            {
                CouldNotIndexDocument ex = new CouldNotIndexDocument { ErrorMessage = "Não foi possível indexar o documento\n" + e.Message };
                throw new FaultException<CouldNotIndexDocument>(ex, ex.ErrorMessage);
            }
        }

        public override void IndexDocumentV2(IndexDocumentRequestV2 request)
        {
            CallContext.SetData("UserName", request.Username);

            IndexDocumentRequest i = new IndexDocumentRequest()
            {
                DocumentData = request.DocumentData,
                CompanyDb = request.CompanyDb
            };

            IndexDocument(i);

        }
    }
}