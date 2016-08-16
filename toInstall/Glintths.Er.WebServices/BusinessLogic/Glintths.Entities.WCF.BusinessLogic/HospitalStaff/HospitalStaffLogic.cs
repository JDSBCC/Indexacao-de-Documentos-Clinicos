using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Glintths.Er.Common.BusinessEntities;

namespace Glintths.Er.Entities.BusinessLogic
{
    public class HospitalStaffLogic
    {
        public static HospitalStaffList GetAllHospitalStaff(string companyDB)
        { 
            return HospitalStaffManagementBER.Instance.GetAllHospitalStaff(companyDB); 
        }

        
            
        public static HospitalStaff GetHospitalStaffByUsername(string companyDB, string username)
        {
            return HospitalStaffManagementBER.Instance.GetHospitalStaffByUsername(companyDB, username);
        }


        public static bool SetHospitalStaffUser(string companyDB, string mechanNum, string hospStaffType, string username)
        {
            bool ret = false;
            try
            {

                Database dal = CPCHS.Common.Database.Database.GetDatabase("ErEntities", companyDB);
                using (DbConnection conn = dal.CreateConnection())
                {
                    conn.Open();
                    using (DbTransaction transaction = conn.BeginTransaction())
                    {
                        HospitalStaffManagementBER.Instance.SetHospitalStaffUser(companyDB, mechanNum, hospStaffType, username, transaction);
                        transaction.Commit();
                    }
                }
                ret = true;
            }
            
            catch (Exception)
            { 
                ret =  false; 
            }
            
            return ret; 
        }

        public static HospitalStaffList GetHospitalStaffByType(string companyDB, string type)
        {
            return HospitalStaffManagementBER.Instance.GetHospitalStaffByType(companyDB, type);
        }
    }
}
