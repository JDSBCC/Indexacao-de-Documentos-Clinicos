//using Glintths.Er.Common.BusinessEntities;
namespace Glintths.Er.Entities.ServiceImplementation
{
    public partial class HospitalStaffManagementWs
    {
        public override MessageContracts.GetAllHospitalStaffResponse GetAllHospitalStaff(MessageContracts.GetAllHospitalStaffRequest request)
        {
            Common.BusinessEntities.HospitalStaffList hospStaffList = Glintths.Er.Entities.BusinessLogic.HospitalStaffLogic.GetAllHospitalStaff(request.Configuration.CompanyDb);

            Common.DataContracts.HospitalStaffList hsl = new Common.DataContracts.HospitalStaffList();

            foreach(Glintths.Er.Common.BusinessEntities.HospitalStaff hs in hospStaffList.Items)
            {
                hsl.Add(TranslateBetweenHospitalStaffAndHospitalStaff.TranslateHospitalStaffToHospitalStaff(hs));
            }
            return new MessageContracts.GetAllHospitalStaffResponse() { HospitalStaff = hsl };
        }

        public override MessageContracts.GetHospitalStaffByUsernameResponse GetHospitalStaffByUsername(MessageContracts.GetHospitalStaffByUsernameRequest request)
        {
            Glintths.Er.Common.BusinessEntities.HospitalStaff hospStaffBe = Glintths.Er.Entities.BusinessLogic.HospitalStaffLogic.GetHospitalStaffByUsername(request.Configuration.CompanyDb, request.Username);
            if (hospStaffBe == null)
                return new MessageContracts.GetHospitalStaffByUsernameResponse();
            Common.DataContracts.HospitalStaff hospStaffDc = TranslateBetweenHospitalStaffAndHospitalStaff.TranslateHospitalStaffToHospitalStaff(hospStaffBe);
            return new MessageContracts.GetHospitalStaffByUsernameResponse() { HospitalStaff = hospStaffDc };
        }

        public override void SetHospitalStaffUser(MessageContracts.SetHospitalStaffUserRequest request)
        {
             bool success = Glintths.Er.Entities.BusinessLogic.HospitalStaffLogic.SetHospitalStaffUser(request.Configuration.CompanyDb, request.mechanNum, request.hospStaffType, request.Username);
        }

        public override Glintths.Er.Entities.MessageContracts.GetHospitalStaffByTypeResponse GetHospitalStaffByType(Glintths.Er.Entities.MessageContracts.GetHospitalStaffByTypeRequest request)
        {
            Glintths.Er.Common.BusinessEntities.HospitalStaffList hospStaffList = 
                Glintths.Er.Entities.BusinessLogic.HospitalStaffLogic.GetHospitalStaffByType(request.Configuration.CompanyDb, request.Type);

            Common.DataContracts.HospitalStaffList hsl = new Common.DataContracts.HospitalStaffList();
            foreach (Glintths.Er.Common.BusinessEntities.HospitalStaff hs in hospStaffList.Items)
            {
                hsl.Add(TranslateBetweenHospitalStaffAndHospitalStaff.TranslateHospitalStaffToHospitalStaff(hs));
            }
            return new MessageContracts.GetHospitalStaffByTypeResponse() { HospitalStaff = hsl };
        }
    }
}
