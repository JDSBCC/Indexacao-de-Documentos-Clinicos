
namespace Cpchs.Entities.WCF.ServiceImplementation
{
    public static class TranslateBetweenExternalPatientListBEAndPatientListDC
    {
        public static Cpchs.Entities.WCF.DataContracts.Patients TranslateExternalPatientListToPatientList(Cpchs.Eresults.Common.WCF.BusinessEntities.ExternalPatientList from)
        {
            Cpchs.Entities.WCF.DataContracts.Patients patients = new Cpchs.Entities.WCF.DataContracts.Patients();
            patients.PatientCollection = new Cpchs.Entities.WCF.DataContracts.PatientCollection();

            foreach (Cpchs.Eresults.Common.WCF.BusinessEntities.ExternalPatient p in from.Items)
            {
                patients.PatientCollection.Add(TranslateBetweenExternalPatientBEAndPatientDC.TranslateExternalPatientToPatient(p));
            }
            return patients;
        }
    }
}
