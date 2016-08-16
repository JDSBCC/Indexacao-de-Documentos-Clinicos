using System;
using Cpchs.Eresults.Common.WCF.BusinessEntities;
using System.ServiceModel;

namespace Glintths.Base.WCF.ServiceImplementation
{
	public partial class CodificationManagementWS 
	{

        public override Glintths.Base.WCF.MessageContracts.GetConfigurationByAreaKeyResponse GetConfigurationByAreaKey(Glintths.Base.WCF.MessageContracts.GetConfigurationByAreaKeyRequest request)
        {
            try
            {
                Glintths.Base.WCF.MessageContracts.GetConfigurationByAreaKeyResponse response = new Glintths.Base.WCF.MessageContracts.GetConfigurationByAreaKeyResponse();

                Glintths.Base.WCF.DataContracts.Codifications conflist = new Glintths.Base.WCF.DataContracts.Codifications();

                conflist.CodificationList = new Glintths.Base.WCF.DataContracts.CodificationCollection();

                ERConfigurationList br_conflist = Glintths.Base.WCF.BusinessLogic.CodificationsLogic.GetConfigurationByAreaKey(request.CompanyDb, request.InstId, request.PlaceId, request.AppId, request.DocTypeId, request.Key);
                if (br_conflist.Count != 0)
                {
                    foreach (ERConfiguration c in br_conflist.Items)
                    {
                        conflist.CodificationList.Add
                            (TranslateBetweenERConfigurationAndConfiguration.TranslateERConfigurationToConfiguration(c));
                    }
                }

                response.ConfigurationList = conflist;

                return response;
            }
            catch (Exception e)
            {
                throw new FaultException(e.Message);
            }
        }

        public override Glintths.Base.WCF.MessageContracts.GetConfigurationByScopeResponse GetConfigurationByScope(Glintths.Base.WCF.MessageContracts.GetConfigurationByScopeRequest request)
        {
            try
            {
                Glintths.Base.WCF.MessageContracts.GetConfigurationByScopeResponse response = new Glintths.Base.WCF.MessageContracts.GetConfigurationByScopeResponse();

                Glintths.Base.WCF.DataContracts.Codifications conflist = new Glintths.Base.WCF.DataContracts.Codifications();

                conflist.CodificationList = new Glintths.Base.WCF.DataContracts.CodificationCollection();

                ERConfigurationList br_conflist = Glintths.Base.WCF.BusinessLogic.CodificationsLogic.GetConfigurationByScope(request.CompanyDb, request.InstId, request.PlaceId, request.AppId, request.DocTypeId, request.Scope);
                if (br_conflist.Count != 0)
                {
                    foreach (ERConfiguration c in br_conflist.Items)
                    {
                        conflist.CodificationList.Add
                            (TranslateBetweenERConfigurationAndConfiguration.TranslateERConfigurationToConfiguration(c));
                    }
                }

                response.ConfigurationList = conflist;

                return response;
            }
            catch (Exception e)
            {
                throw new FaultException(e.Message);
            }
        }
	}
	
}

