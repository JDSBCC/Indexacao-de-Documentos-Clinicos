using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cpchs.Eresults.Common.WCF.BusinessEntities;
using Cpchs.Entities.WCF.DataContracts;

namespace Cpchs.Entities.WCF.ServiceImplementation
{
    public static class TranslateBetweenStateListAndStateCollection
    {
        public static StateCollection TranslateStatesToStates(StateList from)
        {
            StateCollection to = new StateCollection();
            foreach (Cpchs.Eresults.Common.WCF.BusinessEntities.State state in from.Items)
            {
                to.Add(TranslateBetweenStateBEAndStateDC.TranslateStateToState(state));
            }
            return to;
        }
    }
}