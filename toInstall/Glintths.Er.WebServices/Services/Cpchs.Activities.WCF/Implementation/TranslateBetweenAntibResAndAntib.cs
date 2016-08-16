using Cpchs.Activities.WCF.DataContracts;

namespace Cpchs.Activities.WCF.ServiceImplementation
{
    public static class TranslateBetweenAntibResAndAntib
    {
        public static Antib TranslateAntibResToAntib(Eresults.Common.WCF.BusinessEntities.AntibRes from)
        {
            Antib to = new Antib
            {
                antibName = from.Antib.Descr,
                antibAcronym = from.Antib.Code,
                antibSensitivity = from.Sens
            };
            return to;
        }
    }
}

