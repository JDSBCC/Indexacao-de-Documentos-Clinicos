using System.Linq;
using Cpchs.Entities.WCF.DataContracts;

namespace Cpchs.Entities.WCF.ServiceImplementation
{
    public static class TranslateBetweenExternalPatientBEAndPatientDC
    {
        public static Cpchs.Eresults.Common.WCF.BusinessEntities.ExternalPatient TranslatePatientToExternalPatient(Cpchs.Entities.WCF.DataContracts.Patient from)
        {
            Cpchs.Eresults.Common.WCF.BusinessEntities.ExternalPatient to = new Cpchs.Eresults.Common.WCF.BusinessEntities.ExternalPatient();
            to.PatName = from.Name;
            to.PatBirthDate = from.BirthDate;
            to.PatAddress = from.Address;
            to.PatPostalCode = from.PostalCode;
            to.PatPhone = from.Phone1;
            to.PatPhone2 = from.Phone2;
            to.PatProcNum = from.ProcessNum;
            to.PatSNS = from.SnsNum;
            to.PatBI = from.IdCardNum;
            to.PatCity = from.City;
            to.PatCivilState = from.MaritalStatus != null ? from.MaritalStatus.Code : null;
            to.PatNBenef = from.BenefNum;
            to.PatSocialNum = from.SocialNum;
            
            if(from.LocalPatients != null && from.LocalPatients.Count == 1)
            {
                Cpchs.Entities.WCF.DataContracts.LocalPatient localPat = from.LocalPatients.First();
                to.PatPatientType = localPat.PatientType != null ? localPat.PatientType.Code: string.Empty;
                to.PatPatient = localPat.PatientId;
            }
            if(from.Gender != null)
            {
                to.PatSex = from.Gender.Code;
            }
            return to;
        }

        public static Cpchs.Entities.WCF.DataContracts.Patient TranslateExternalPatientToPatient(Cpchs.Eresults.Common.WCF.BusinessEntities.ExternalPatient from)
        {
            Cpchs.Entities.WCF.DataContracts.Patient to = new Cpchs.Entities.WCF.DataContracts.Patient();
            to.Name = from.PatName;
            to.BirthDate = from.PatBirthDate;
            to.Address = from.PatAddress;
            to.PostalCode = from.PatPostalCode;
            to.Phone1 = from.PatPhone;
            to.Phone2 = from.PatPhone2;
            to.ProcessNum = from.PatProcNum;
            to.SnsNum = from.PatSNS;
            to.IdCardNum = from.PatBI;
            to.City = from.PatCity;
            to.BenefNum = from.PatNBenef;
            to.SocialNum = from.PatSocialNum;
            
            to.MaritalStatus = new MaritalStatus() { Code = from.PatCivilState };

            to.LocalPatients = new LocalPatientCollection();
            var patientType = new Cpchs.Entities.WCF.DataContracts.PatientType() { Code = from.PatPatientType};
            var localPat = new Cpchs.Entities.WCF.DataContracts.LocalPatient() { PatientType = patientType, PatientId = from.PatPatient };
            to.LocalPatients.Add(localPat);

            to.Gender = new Cpchs.Entities.WCF.DataContracts.Gender() { Code = from.PatSex };
            
            return to;
        }
    }
}

