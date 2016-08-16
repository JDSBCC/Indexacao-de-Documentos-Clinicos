using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cpchs.Entities.WCF.DataContracts;


namespace Cpchs.Entities.WCF.ServiceImplementation
{
    public static class TranslateBetweenPatientTypeListAndPatientTypeCollection
    {
        public static PatientTypeCollection TranslatePatientTypesToPatientTypes(Cpchs.Eresults.Common.WCF.BusinessEntities.PatientTypeList from)
        {
            Cpchs.Entities.WCF.DataContracts.PatientTypeCollection to = new Cpchs.Entities.WCF.DataContracts.PatientTypeCollection();
            foreach (Cpchs.Eresults.Common.WCF.BusinessEntities.PatientType patType  in from.Items)
            {

                to.Add(TranslateBetweenPatientTypeBEAndPatientTypeDC.TranslatePatientTypeToPatientType(patType));
            }
            return to;
        }
    }
}