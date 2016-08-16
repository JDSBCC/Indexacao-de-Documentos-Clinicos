using System;
using Cpchs.Eresults.Common.WCF.BusinessEntities;
using Cpchs.Entities.WCF.DataContracts;

namespace Cpchs.Entities.WCF.ServiceImplementation
{
    public static class TranslateBetweenLocalPatientBEAndLocalPatientDC
    {
        public static Cpchs.Eresults.Common.WCF.BusinessEntities.LocalPatient TranslateLocalPatientToLocalPatient(Cpchs.Entities.WCF.DataContracts.LocalPatient from)
        {
            Cpchs.Eresults.Common.WCF.BusinessEntities.LocalPatient to = new Cpchs.Eresults.Common.WCF.BusinessEntities.LocalPatient();
            to.LocpatEntityId = from.EntityId;
            to.LocpatPatientId = from.PatientId;
            return to;
        }

        public static Cpchs.Entities.WCF.DataContracts.LocalPatient TranslateLocalPatientToLocalPatient(Cpchs.Eresults.Common.WCF.BusinessEntities.LocalPatient from)
        {
            Cpchs.Entities.WCF.DataContracts.LocalPatient to = new Cpchs.Entities.WCF.DataContracts.LocalPatient();
            to.EntityId = from.LocpatEntityId;
            to.PatientId = from.LocpatPatientId;
            to.PatientType = TranslateBetweenPatientTypeBEAndPatientTypeDC.TranslatePatientTypeToPatientType(from.LocpatPatientType);
            return to;
        }
    }
}

