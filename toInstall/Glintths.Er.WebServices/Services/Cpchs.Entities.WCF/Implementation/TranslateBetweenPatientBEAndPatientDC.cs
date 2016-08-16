using System;
using Cpchs.Eresults.Common.WCF.BusinessEntities;
using Cpchs.Entities.WCF.DataContracts;

namespace Cpchs.Entities.WCF.ServiceImplementation
{
    public static class TranslateBetweenPatientBEAndPatientDC
    {
        public static Cpchs.Eresults.Common.WCF.BusinessEntities.Patient TranslatePatientToPatient(Cpchs.Entities.WCF.DataContracts.Patient from)
        {
            Cpchs.Eresults.Common.WCF.BusinessEntities.Patient to = new Cpchs.Eresults.Common.WCF.BusinessEntities.Patient();
            to.PatEntity.EntAddress = from.Address;
            to.PatBenefNum = from.BenefNum;
            to.PatBirthDate = from.BirthDate;
            to.PatEntity.EntCity = from.City;
            to.PatEntity.EntFax = from.Fax;
            to.PatHealthCentre = from.HealthCentre;
            to.PatIdCardNum = from.IdCardNum;
            to.PatEntity.EntName = from.Name;
            to.PatEntity.EntPhone1 = from.Phone1;
            to.PatEntity.EntPhone2 = from.Phone2;
            to.PatEntity.EntPostalCode = from.PostalCode;
            to.PatProcessNum = from.ProcessNum;
            to.PatSnsNum = from.SnsNum;
            to.PatEntity.EntSocialNum = from.SocialNum;
            to.PatGender = TranslateBetweenGenderBEAndGenderDC.TranslateGenderToGender(from.Gender);
            to.PatMaritalStatus = TranslateBetweenMaritalStatusBEAndMaritalStatusDC.TranslateMaritalStatusToMaritalStatus(from.MaritalStatus);

            to.Episodes = new VisitList();
            for( int i = 0; i < from.PatientEpisodes.Count; i++ )
            {
                to.Episodes.Add( TranslateBetweenVisitBeAndPatientEpisodeDC.TranslateVisitToPatientEpisode(from.PatientEpisodes[i]));
            }

            return to;
        }

        public static Cpchs.Entities.WCF.DataContracts.Patient TranslatePatientToPatient(Cpchs.Eresults.Common.WCF.BusinessEntities.Patient from)
        {
            Cpchs.Entities.WCF.DataContracts.Patient to = new Cpchs.Entities.WCF.DataContracts.Patient();
            to.Address = from.PatEntity.EntAddress;
            to.BenefNum = from.PatBenefNum;
            to.BirthDate = from.PatBirthDate;
            to.City = from.PatEntity.EntCity;
            to.EntityIds = from.patEntIds;
            to.Fax = from.PatEntity.EntFax;
            to.HealthCentre = from.PatHealthCentre;
            to.IdCardNum = from.PatIdCardNum;
            to.Name = from.PatEntity.EntName;
            to.Phone1 = from.PatEntity.EntPhone1;
            to.Phone2 = from.PatEntity.EntPhone2;
            to.PostalCode = from.PatEntity.EntPostalCode;
            to.ProcessNum = from.PatProcessNum;
            to.SnsNum = from.PatSnsNum;
            to.SocialNum = from.PatEntity.EntSocialNum;
            to.Gender = TranslateBetweenGenderBEAndGenderDC.TranslateGenderToGender(from.PatGender);
            to.MaritalStatus = TranslateBetweenMaritalStatusBEAndMaritalStatusDC.TranslateMaritalStatusToMaritalStatus(from.PatMaritalStatus);
            to.LocalPatients = TranslateBetweenLocalPatientListAndLocalPatientCollection.TranslateLocalPatientsToLocalPatients(from.PatLocalPatients);

            to.PresentationPatient = from.PresentationPatient;
            to.PresentationNSC = from.PresentationNSC;
            to.PresentationNProc = from.PresentationNProc;

            to.SampleState = from.HighestPriorityDescription 
                            + (from.InvalidSamples.HasValue ?
                            (from.InvalidSamples.Value != 0 ? " - Com tubos inválidos":"")
                                              : "")
                            + (from.PendingSamples.HasValue ?
                            (from.PendingSamples.Value != 0 ? " - Com colheitas pendentes":"")
                                              : "");
            to.PresentationOrder = from.HighestPriority??int.MaxValue;


            to.PatientEpisodes = new PatientEpisodeCollection();
            for( int i = 0; from.Episodes != null &&  i < from.Episodes.Count; i++ )
            {
                to.PatientEpisodes.Add( TranslateBetweenVisitBeAndPatientEpisodeDC.TranslateVisitToPatientEpisode( from.Episodes[ i ] ) );
            }
            /*
            to.EntityIds = "";
            foreach (Cpchs.Eresults.Common.WCF.BusinessEntities.LocalPatient locpat in from.PatLocalPatients.Items)
            {
                to.EntityIds += locpat.LocpatEntityId + ",";
                if (from.PatLocalPatients.Items.IndexOf(locpat) == from.PatLocalPatients.Items.Count - 1)
                {
                    to.EntityIds = to.EntityIds.Remove(to.EntityIds.Length - 1, 1);
                }
            }
            */
            return to;
        }
    }
}