using System;
using WCF = global::System.ServiceModel;

namespace Cpchs.Entities.WCF.MessageContracts
{
    [WCF::MessageContract(WrapperName = "GetPatientsBySpecsResponse", WrapperNamespace = "urn:Cpchs.Entities", IsWrapped = true)]
    public partial class GetPatientsBySpecsResponse
    {
        private Cpchs.Entities.WCF.DataContracts.Patients patientList;

        [WCF::MessageBodyMember(Name = "PatientList")]
        public Cpchs.Entities.WCF.DataContracts.Patients PatientList
        {
            get { return patientList; }
            set { patientList = value; }
        }
    }
}
