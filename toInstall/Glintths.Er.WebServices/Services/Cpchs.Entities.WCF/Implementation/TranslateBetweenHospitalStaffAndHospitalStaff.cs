using System;

namespace Glintths.Er.Entities.ServiceImplementation
{
    public static class TranslateBetweenHospitalStaffAndHospitalStaff
    {

        public static Common.BusinessEntities.HospitalStaff TranslateHospitalStaffToHospitalStaff(Common.DataContracts.HospitalStaff from)
        {
            Common.BusinessEntities.HospitalStaff to = new Common.BusinessEntities.HospitalStaff();
            to.HospStaffType = from.Type;
            to.HospStaffMechanNum = from.MechanNum;
            to.EntName = from.Name;
            to.HospStaffUsername = from.Username;
            to.HospStaffId = from.Id;
            return to;
        }

        public static Common.DataContracts.HospitalStaff TranslateHospitalStaffToHospitalStaff(Common.BusinessEntities.HospitalStaff from)
        {
            Common.DataContracts.HospitalStaff to = new Common.DataContracts.HospitalStaff();
            if(from != null)
            {
                if (from.HospStaffType != null)
                    to.Type = from.HospStaffType;
                if (from.HospStaffMechanNum != null)
                    to.MechanNum = from.HospStaffMechanNum;
                if (from.EntName != null)
                    to.Name = from.EntName;
                if (from.HospStaffUsername != null)
                    to.Username = from.HospStaffUsername;
                if (from.HospStaffId != null)
                    to.Id = from.HospStaffId;
                to.ReqServCode = from.ReqServiceCode;
                to.ReqServDescription = from.ReqServiceDescription;
                to.SpecialityCode = from.SpecCode;
                to.SpecialityDescription = from.SpecDescription;
            }
            return to;
        }
    
    }
}

