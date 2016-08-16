using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cpchs.Eresults.Common.WCF.BusinessEntities;
using Cpchs.Entities.WCF.DataContracts;

namespace Cpchs.Entities.WCF.ServiceImplementation
{
    class TranslateBetweenLocalPatientListAndLocalPatientCollection
    {
        public static LocalPatientCollection TranslateLocalPatientsToLocalPatients(LocalPatientList from)
        {
            LocalPatientCollection to = new LocalPatientCollection();
            foreach (Cpchs.Eresults.Common.WCF.BusinessEntities.LocalPatient locpat in from.Items)
            {
                to.Add(TranslateBetweenLocalPatientBEAndLocalPatientDC.TranslateLocalPatientToLocalPatient(locpat));
            }
            return to;
        }
    }
}