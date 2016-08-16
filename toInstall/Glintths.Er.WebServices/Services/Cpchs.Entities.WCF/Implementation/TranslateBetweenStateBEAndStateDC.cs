using System;
using Cpchs.Eresults.Common.WCF.BusinessEntities;
using Cpchs.Entities.WCF.DataContracts;

namespace Cpchs.Entities.WCF.ServiceImplementation
{
    public static class TranslateBetweenStateBEAndStateDC
    {
        public static Cpchs.Entities.WCF.DataContracts.State TranslateStateToState(Cpchs.Eresults.Common.WCF.BusinessEntities.State from)
        {
            Cpchs.Entities.WCF.DataContracts.State to = new Cpchs.Entities.WCF.DataContracts.State();
            to.Id = from.StateId;
            to.Code = from.StateCode;
            to.Acronym = from.StateTag;
            to.Description = from.StateDescription;
            to.Scope = from.StateScope;
            return to;
        }
    }
}